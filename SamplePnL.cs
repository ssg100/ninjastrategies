// 
// Copyright (C) 2007, NinjaTrader LLC <www.ninjatrader.com>.
// NinjaTrader reserves the right to modify or overwrite this NinjaScript component with each release.
//
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
using NinjaTrader.Strategy;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// Sample strategy utilizing PnL statistics
    /// </summary>
    [Description("Sample strategy utilizing PnL statistics")]
    public class SamplePnL : Strategy
    {
        #region Variables
		private int		priorTradesCount		= 0;
		private double	priorTradesCumProfit	= 0;
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
			// Profit target is 10 ticks above entry price
			SetProfitTarget(CalculationMode.Ticks, 10);
			
			// Stop loss is 4 ticks below entry price
			SetStopLoss(CalculationMode.Ticks, 4);
			
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
				priorTradesCumProfit = Performance.AllTrades.TradesPerformance.Currency.CumProfit;
				
				/* NOTE: Using .AllTrades will include both historical virtual trades as well as real-time trades.
				If you want to only count profits from real-time trades please use .RealtimeTrades. */
			}
			
			/* Prevents further trading if the current session's realized profit exceeds $1000 or if realized losses exceed $400.
			Also prevent trading if 10 trades have already been made in this session. */
			if (Performance.AllTrades.TradesPerformance.Currency.CumProfit - priorTradesCumProfit >= 1000
				|| Performance.AllTrades.TradesPerformance.Currency.CumProfit - priorTradesCumProfit <= -400
				|| Performance.AllTrades.Count - priorTradesCount > 10)
			{
				/* TIP FOR EXPERIENCED CODERS: This only prevents trade logic in the context of the OnBarUpdate() method. If you are utilizing
				other methods like OnOrderUpdate() or OnMarketData() you will need to insert this code segment there as well. */
				
				// Returns out of the OnBarUpdate() method. This prevents any further evaluation of trade logic in the OnBarUpdate() method.
				return;
			}
			
			// ENTRY CONDITION: If current close is greater than previous close, enter long
			if (Close[0] > Close[1])
			{
				EnterLong();
			}
        }

        #region Properties
        #endregion
    }
}
