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
    /// nanex strategy to buy at 9pm PST exit 1AM
    /// </summary>
    [Description("nanex strategy to buy at 9pm PST exit 1AM")]
    public class ZZOvernightES : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int startTime = 210000; // Default setting for StartTime
        private int endTime = 010000; //235900; // Default setting for EndTime
        private int stopLoss = 500; // Default setting for StopLoss
		private int tradeSize = 1;	// # of contract
		
		private int		priorTradesCount		= 0;
		private double	priorTradesCumProfit	= 0;
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            SetTrailStop("Long", CalculationMode.Ticks, StopLoss, false);

            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			int time = ToTime(Time[0]);
			double curr_realized_pnl;
			double curr_total_pnl;
			double open_pnl;

			// Add guard, if weekly loss exceeds a $ amount, stop trading for the weekn
			if ((Time[0].DayOfWeek == DayOfWeek.Sunday) && Bars.FirstBarOfSession && Time[0].Hour < 18 )
			{
				// Guard with hour < 18 because we do get 2 true conditions fired - one at 15:00 and 21:01
				Print("Start of sunday " + Time[0]);
			
				// Store the strategy's prior cumulated realized profit and number of trades
				priorTradesCount = Performance.AllTrades.Count;
				priorTradesCumProfit = Performance.AllTrades.TradesPerformance.Currency.CumProfit;
				
				Print("priorTradesCount = " + priorTradesCount);
				Print("priorTradesCumProfit = " + priorTradesCumProfit);
				/* NOTE: Using .AllTrades will include both historical virtual trades as well as real-time trades.
				If you want to only count profits from real-time trades please use .RealtimeTrades. */
			}
			
			/* Prevents further trading if  if realized losses exceed $400.
			Also prevent trading if 10 trades have already been made in this session. */
			curr_realized_pnl = Performance.AllTrades.TradesPerformance.Currency.CumProfit - priorTradesCumProfit;
			// Have to use open trade pnl as well - if not, an open trade can exceeds $400 and still keep losing
			open_pnl = Position.GetProfitLoss(Close[0], PerformanceUnit.Currency);
			curr_total_pnl = curr_realized_pnl + open_pnl;
			
			//Print("quantity = " + Position.Quantity);
			
			if (curr_total_pnl <= -stopLoss * tradeSize)
//				|| Performance.AllTrades.Count - priorTradesCount > 10) // if want # of trade counts also
			{
				//Print("Weekly loss exceed!!");
				
				
				// Exit all position if weekly loss exceeded
				if (Position.MarketPosition == MarketPosition.Long)
            	{
					Print("Weekly loss exceed EXIT = " + ToTime(Time[0]));
					Print("Loss= " + curr_total_pnl);
                	ExitLong("ExitLong", "Long");
            	}
				
				return;
			}
			
            // Condition set 1
            if (
					(
						(time >= startTime) 
						&& (time < startTime + 500)
                	)
					&& Position.MarketPosition == MarketPosition.Flat
					//&&  (Time[0].DayOfWeek != DayOfWeek.Sunday)
					//&&  (Time[0].DayOfWeek != DayOfWeek.Monday)
				)
            {
				Print("Entry ToTime() = " + ToTime(Time[0]));
                EnterLong(TradeSize, "Long");
            }
	
            // Condition set 2
            if (
					( time >= endTime
						&& time <= endTime + 500 )
                		&& Position.MarketPosition == MarketPosition.Long
				)
            {
				Print("EXIT ToTime() = " + ToTime(Time[0]));
                ExitLong("ExitLong", "Long");	// if quantity is not specified, then all exit
            }
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int StartTime
        {
            get { return startTime; }
            set { startTime = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int EndTime
        {
            get { return endTime; }
            set { endTime = Math.Max(1, value); }
        }

        [Description("stop loss")]
        [GridCategory("Parameters")]
        public int StopLoss
        {
            get { return stopLoss; }
            set { stopLoss = Math.Max(2, value); }
        }
		
		[Description("Trade Size")]
        [GridCategory("Parameters")]
        public int TradeSize
        {
            get { return tradeSize; }
            set { tradeSize = Math.Max(1, value); }
        }
        #endregion
    }
}
