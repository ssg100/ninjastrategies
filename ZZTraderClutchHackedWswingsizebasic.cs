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
    public class ZZTraderClutchHackedWswingsizebasic : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int profitTarget1 = 16; // Default setting for ProfitTarget1
        private int profitTarget2 = 8; // Default setting for ProfitTarget2
        private int stopLoss = 16; // Default setting for StopLoss
        private int mALen = 20; // Default setting for MALen
        private int swingSize = 4; // Default setting for SwingSize
        // User defined variables (add any user defined variables below)
		private double aTRLen = 2.0;
		private int stopLossTemp = 0;
		private int		priorTradesCount		= 0;
		private double	priorTradesCumProfit	= 0;
		private int adxxLen = 14;
		private double dailyPntLossStop = 4;
		private double bbstddev = 2.0;
		private int timeStopBars = 7; // time stop with # of barssincenetry
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(ZZSwingIdentifierHigh(swingSize));
            Add(SMA(Open, MALen));
            SetProfitTarget("long", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("short", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
			
			// Position 2, scalper
            SetProfitTarget("longscalp", CalculationMode.Ticks, ProfitTarget2);
            SetStopLoss("longscalp", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("shortscalp", CalculationMode.Ticks, ProfitTarget2);
            SetStopLoss("shortscalp", CalculationMode.Ticks, StopLoss, false);
			
            CalculateOnBarClose = false;
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
				//Print("priorTradesCumProfit = " + priorTradesCumProfit);
				/* NOTE: Using .AllTrades will include both historical virtual trades as well as real-time trades.
				If you want to only count profits from real-time trades please use .RealtimeTrades. */
			}

			// EXIT WITH TIME STOP
			if(  Position.MarketPosition == MarketPosition.Long )
			{
		 		if (BarsSinceEntry() > timeStopBars) {
					ExitLong("long");
					ExitLong("longscalp");
				}
			}
			
			
			if( Position.MarketPosition == MarketPosition.Short )
			{
		 		if (BarsSinceEntry() > timeStopBars) 
				{
					ExitShort("short");
					ExitShort("shortscalp");
				}
			}			
	
			
			/*
			if( (CrossBelow(Close, EMA(adxxLen)[0], 2))
				&& Position.MarketPosition == MarketPosition.Long )
			{
				ExitLong("long");
			}
			
			
			if( (CrossAbove(Close, EMA(adxxLen)[0], 2))
				&& Position.MarketPosition == MarketPosition.Short )
			{
				ExitShort("short");
			}			
			*/	
			
			//stopLossTemp = Convert.ToInt32((aTRLen*ATR(Close,10)[0])/TickSize);
			//Print("stopLossTemp = " + stopLossTemp);
			//Print("stopLoss = " + stopLoss);
			//if(stopLossTemp < stopLoss)
			//	stopLoss = stopLossTemp;  // if stopLoss from ATR is too big, use max stopLoss
			SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("longscalp", CalculationMode.Ticks, StopLoss, false);
			SetStopLoss("shortscalp", CalculationMode.Ticks, StopLoss, false);
			
            // Condition set 1
            if (CrossAbove(Close, ZZSwingIdentifierHigh(swingSize).SwingPointHigh[0], 1)
                && Close[0] > SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat
				/*&& ADX(adxxLen)[0] > 20*/
				/*&& ( (Bollinger(Close, 2.0, 20).Upper[0] - Bollinger(Close, 2.0, 20).Lower[0]) < adxxLen )*/
				/*&& MACD(Close,adxxLen,26,9)[0] > MACD(Close,adxxLen,26,9).Avg[0]*/
				//&& High[0] < Bollinger(Close, bbstddev, adxxLen ).Upper[0] 
				//&& Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit >= (-1*dailyPntLossStop)
				)
            {
                EnterLong(1, "long");
				EnterLong(1, "longscalp");
            }

            // Condition set 2
            if (CrossBelow(Close, ZZSwingIdentifierHigh(swingSize).SwingPointLow[0], 1)
                && Close[0] < SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat
				/*&& ADX(adxxLen)[0] > 20*/
				/*&& ( (Bollinger(Close, 2.0, 20).Upper[0] - Bollinger(Close, 2.0, 20).Lower[0]) < adxxLen )*/
				/*&& MACD(Close,adxxLen,26,9)[0] < MACD(Close,adxxLen,26,9).Avg[0]*/
				//&& Low[0] > Bollinger(Close, bbstddev,  adxxLen).Lower[0] 
				//&& Performance.AllTrades.TradesPerformance.Points.CumProfit - priorTradesCumProfit >= (-1*dailyPntLossStop)
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
        public double ATRLen
        {
            get { return aTRLen; }
            set { aTRLen = Math.Max(1, value); }
        }

		[Description("")]
        [GridCategory("Parameters")]
	 	public int ADXXLen
        {
            get { return adxxLen; }
            set { adxxLen = Math.Max(1, value); }
        }
		
		
		[Description("")]
        [GridCategory("Parameters")]
	 	public int TimeStopBars
        {
            get { return timeStopBars; }
            set { timeStopBars = Math.Max(1, value); }
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
