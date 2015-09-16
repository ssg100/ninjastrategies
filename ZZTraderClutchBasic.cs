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


// LOGS:
//
// 12/1/2011:  Added bollinger to filter out trades that is too streched below that probability of trend continuation is less
//             Added daily stop loss - this improves drawdown and added better profits
//
// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{

    /// <summary>
    /// Implementation of traderclutch
    /// </summary>
    [Description("Implementation of traderclutch")]
    public class ZZTraderClutchBasic : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int profitTarget1 = 30; // Default setting for ProfitTarget1
        private int profitTarget2 = 8; // Default setting for ProfitTarget2
        private int stopLoss = 10; // Default setting for StopLoss
        private int mALen = 171; // Default setting for MALen
        private int swingSize = 8; // Default setting for SwingSize
        // User defined variables (add any user defined variables below)
		private double aTRLen = 2.0;
		private int stopLossTemp = 0;
		private int		priorTradesCount		= 0;
		private double	priorTradesCumProfit	= 0;
		private int bbLen = 114;
		private double dailyPntLossStop = 8;
		private double bbstddev = 2.0;

		private int trailStop = 10;			// this is actually the target to hit before moving stopFromEntry ticks from entry level
		private int stopFromEntry = 4;		// how much to move stop from entry

        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(ZZSwingIdentifierHigh(swingSize));
            Add(SMA(Open, MALen));
			Add(Bollinger(Close, bbstddev, bbLen));
            SetProfitTarget("long", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("short", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
			
			// Position 2, scalper
            SetProfitTarget("longscalp", CalculationMode.Ticks, ProfitTarget2);
            SetStopLoss("longscalp", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("shortscalp", CalculationMode.Ticks, ProfitTarget2);
            SetStopLoss("shortscalp", CalculationMode.Ticks, StopLoss, false);
			
            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			// At the start of a new session
			if (Bars.FirstBarOfSession)
			{
				// Store the strategy's prior cumulated realized profit and number of trades
				priorTradesCount = Performance.AllTrades.Count;
				priorTradesCumProfit = Performance.AllTrades.TradesPerformance.Points.CumProfit;
				
           		SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
           
            	SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
				//Print("priorTradesCumProfit = " + priorTradesCumProfit);
				/* NOTE: Using .AllTrades will include both historical virtual trades as well as real-time trades.
				If you want to only count profits from real-time trades please use .RealtimeTrades. */
			}
/*
			if( (Position.GetProfitLoss(Close[0], PerformanceUnit.Points) / TickSize) > trailStop
				&& Position.MarketPosition == MarketPosition.Long )
			{
				SetStopLoss( "long", CalculationMode.Ticks, stopFromEntry, false);
			}	
			if( (Position.GetProfitLoss(Close[0], PerformanceUnit.Points) / TickSize) > trailStop
				&& Position.MarketPosition == MarketPosition.Short )
			{
				SetStopLoss( "short", CalculationMode.Ticks, stopFromEntry, false);
			}			
*/			

			
			/*
			if( (CrossBelow(Close, EMA(adxxLen)[0], 2))
				&& Position.MarketPosition == MarketPosition.Long
				
			  )
				
			{
				ExitLong("long");
			}
			
			
			if( (CrossAbove(Close, EMA(adxxLen)[0], 2))
				&& Position.MarketPosition == MarketPosition.Short )
			{
				ExitShort("short");
			}			
			*/	
			
			// ATR BASED STOP
			/*
			stopLossTemp = Convert.ToInt32((aTRLen*ATR(Close,10)[0])/TickSize);
			//Print("stopLossTemp = " + stopLossTemp);
			//Print("stopLoss = " + stopLoss);
			if(stopLossTemp < stopLoss)
				stopLoss = stopLossTemp;  // if stopLoss from ATR is too big, use max stopLoss
			SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("longscalp", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("shortscalp", CalculationMode.Ticks, StopLoss, false);
			*/
            // Condition set 1
            if (CrossAbove(Close, ZZSwingIdentifierHigh(swingSize).SwingPointHigh[0], 1)
                && Close[0] > SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat

				&& High[0] < Bollinger(Close, bbstddev, bbLen ).Upper[0] 
				&& Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit >= (-1*dailyPntLossStop)

				)
            {
                EnterLong(1, "long");
				EnterLong(1, "longscalp");
            }

            // Condition set 2
            if (CrossBelow(Close, ZZSwingIdentifierHigh(swingSize).SwingPointLow[0], 1)
                && Close[0] < SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat

				&& Low[0] > Bollinger(Close, bbstddev,  bbLen).Lower[0] 
				&& Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit >= (-1*dailyPntLossStop)

				)
            {
                EnterShort(1, "short");
				EnterShort(1, "shortscalp");
            }
			
			//Print("current = " + (Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit));
			//Print("logic = " + ((Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit) <= dailyPntLossStop));
			
        }

        #region Properties
        [Description("Ticks")]
        [GridCategory("Parameters")]
        public int ProfitTarget1
        {
            get { return profitTarget1; }
            set { profitTarget1 = Math.Max(1, value); }
        }

        [Description("Ticks")]
        [GridCategory("Parameters")]
        public int ProfitTarget2
        {
            get { return profitTarget2; }
            set { profitTarget2 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int StopLoss
        {
            get { return stopLoss; }
            set { stopLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MALen
        {
            get { return mALen; }
            set { mALen = Math.Max(1, value); }
        }
		
        [Description("")]
        [GridCategory("Parameters")]
        public int StopFromEntry
        {
            get { return stopFromEntry; }
            set { stopFromEntry = value; }
        }	
		 [Description("")]
        [GridCategory("Parameters")]
        public double ATRLen
        {
            get { return aTRLen; }
            set { aTRLen = Math.Max(1, value); }
        }

		[Description("")]
        [GridCategory("Parameters")]
        public int TrailStop
        {
            get { return trailStop; }
            set { trailStop = Math.Max(1, value); }
        }
		
		[Description("")]
        [GridCategory("Parameters")]
	 	public int BbLen
        {
            get { return bbLen; }
            set { bbLen = Math.Max(1, value); }
        }
		
		
		
		[Description("")]
        [GridCategory("Parameters")]
	 	public double Bbstddev
        {
            get { return bbstddev; }
            set { bbstddev = Math.Max(1, value); }
        }	

		[Description("")]
        [GridCategory("Parameters")]
	 	public double DailyPntLossStop
        {
            get { return dailyPntLossStop; }
            set { dailyPntLossStop = Math.Max(1, value); }
        }
		
        [Description("swing size in ticks")]
        [GridCategory("Parameters")]
        public int SwingSize
        {
            get { return swingSize; }
            set { swingSize = Math.Max(1, value); }
        }
        #endregion
    }
}
