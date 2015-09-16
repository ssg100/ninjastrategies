#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Strategy;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// ILM strat first take
    /// </summary>
    [Description("ILM strat first take")]
    public class ZZILMStrat : Strategy
    {
        #region Variables
        // Wizard generated variables
        private bool tradeTue = true; // Default setting for TradeTue
        private bool tradeHugeRange = true; // Default setting for TradeHugeRange
        private int numContracts = 1; // Default setting for NumContracts
		private double mondayRange = 0; 
		private double stopSize = 0;
		private double mondayHigh = 0;
		private double mondayLow = 0;
		private bool hugeRange = false;
		private bool[] position;
		private double trailingStop;
		private bool newWeek=false;
		private DateTime[] noTradeWeek;
		bool noTradeDay = false;
		int noTradeLen;
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = false;
			//position = new bool[3];
			 position = new bool[] {false,false,false};
			
			noTradeWeek = new DateTime[5] ;
			noTradeWeek[0] = DateTime.Parse("01/20/2015");
			noTradeWeek[1] = DateTime.Parse("12/23/2014");
			noTradeWeek[2] = DateTime.Parse("01/21/2014");
			noTradeLen = 3;
			Print("Array len: " + noTradeWeek.Length);
			
			
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			
			// Check if this is tuesday
			if (Time[1].Date != Time[0].Date && Time[1].DayOfWeek == DayOfWeek.Monday)
			{
				if (Bars.GetDayBar(1) != null) 
				{
					mondayRange = Bars.GetDayBar(1).High - Bars.GetDayBar(1).Low;
					Print("mondayrange : " + mondayRange + "  " + Time[0].Date);
					
					// Calc stop size
					if(mondayRange >16 ) {
						hugeRange = true;
						stopSize = mondayRange/2;
					}
					else {
						stopSize = mondayRange;	
						hugeRange = false;
					}
					
					mondayHigh = Bars.GetDayBar(1).High;
					mondayLow = Bars.GetDayBar(1).Low;
					
					noTradeDay = false;
				}
			}
			
			// if not monday or friday then can trade
		if( Time[0].DayOfWeek != DayOfWeek.Monday ) 	
		{	
            // Condition set 1 - ALL entry
            if (CrossAbove(Close, mondayHigh, 1) &&  Position.MarketPosition == MarketPosition.Flat 
				&& Time[0].DayOfWeek != DayOfWeek.Friday )
            {
				int i =0;
				//Check if this is a short week and not tradeable
				for ( i=0; i<noTradeLen; i++) 
				{
					if( DateTime.Compare(noTradeWeek[i],Time[0].Date) == 0 ) {
						noTradeDay = true;
						Print("No trade: " + noTradeWeek[i].ToString() + "  " + Time[0].Date);
					}
				}
				
				if(noTradeDay == false) {
                	EnterLong(NumContracts, "Long1");
                	EnterLong(NumContracts, "Long2");
                	EnterLong(NumContracts, "Long3");
					//EnterLongLimit(NumContracts, mondayHigh, "Long1");
					//EnterLongLimit(NumContracts, mondayHigh, "Long1");
					//EnterLongLimit(NumContracts, mondayHigh, "Long1");
					position[0] = true;
					position[1] = true;
					position[2] = true;
				
					trailingStop = mondayHigh - stopSize;
				}
			}
            // Condition set 2 -Exit 1
            if (CrossAbove(Close, mondayHigh + stopSize, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
				position[0] = false;
                ExitLong("ExitLong1", "Long1");
				
				// Trail 2 more contracts at monday high
				trailingStop = mondayHigh;
					
            }

            // Condition set 3 - Exit 2
            if (Position.MarketPosition == MarketPosition.Long
                && CrossAbove(Close, mondayHigh + 2* stopSize, 1))
            {
				position[1] = false;
                ExitLong("ExitLong2", "Long2");
				trailingStop = mondayHigh + stopSize;
            }

            // Condition set 4 - Exit 3
            if (CrossAbove(Close, mondayHigh + 3* stopSize, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
				position[2] = false;
                ExitLong("ExitLong3", "Long3");
            }

            // Condition set 5 - STOP out
            if (CrossBelow(Close, trailingStop, 1)
                && Position.MarketPosition == MarketPosition.Long && position[0] == true 
																	&& position[1] == true
																	&& position[2] == true )
            {
				Print("Stopped out all " + Time[0].Date.ToString());
                ExitLong("StoppedOut1", "Long1");
				position[0] = false;

				ExitLong("StoppedOut2", "Long2");
				position[1] = false;
				
				ExitLong("StoppedOut3", "Long3");
				position[2] = false;
            }

			// Condition set 6 - trail 2nd contract
            if (CrossBelow(Close, trailingStop, 1)
                && Position.MarketPosition == MarketPosition.Long && position[0] == false 
																	&& position[1] == true
																	&& position[2] == true )
            {
				ExitLong("StoppedOut2", "Long2");
				position[1] = false;
				
				ExitLong("StoppedOut3", "Long3");
				position[2] = false;
            }

			// Condition set 6 - trail 2nd contract
            if (CrossBelow(Close, trailingStop, 1)
                && Position.MarketPosition == MarketPosition.Long && position[0] == false 
																	&& position[1] == false
																	&& position[2] == true )
            {
				ExitLong("StoppedOut3", "Long3");
				position[2] = false;
            }
			
			// If Friday,then close all at end of day
			if ( Time[0].DayOfWeek == DayOfWeek.Friday && Time[0].Hour >= 12 && 
					Time[0].Minute >= 45) //and if friday is not holiday...
			{				
				if( position[0] == true ) {
                	ExitLong("StoppedOut1", "Long1");
					position[0] = false;
				}
				if( position[1] == true ) {
					ExitLong("StoppedOut2", "Long2");
					position[1] = false;
				}
				if( position[2] == true ) {
					ExitLong("StoppedOut3", "Long3");
					position[2] = false;
				}
			}
			
			//if( Time[0].DayOfWeek == DayOfWeek.Friday )
				//newWeek = false;
			
		} //newWeek	
			
			
        }

        #region Properties
        [Description("If monday is holiday, trade tue breakout")]
        [GridCategory("Parameters")]
        public bool TradeTue
        {
            get { return tradeTue; }
            set { tradeTue = value; }
        }

        [Description("If huge range on monday, trade it")]
        [GridCategory("Parameters")]
        public bool TradeHugeRange
        {
            get { return tradeHugeRange; }
            set { tradeHugeRange = value; }
        }

        [Description("Default contracts")]
        [GridCategory("Parameters")]
        public int NumContracts
        {
            get { return numContracts; }
            set { numContracts = Math.Max(1, value); }
        }
        #endregion
    }
}
