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
    /// Implementation of traderclutch
    /// </summary>
    [Description("Implementation of traderclutch")]
    public class ZZTraderClutchHacked : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int profitTarget1 = 16; // Default setting for ProfitTarget1
        private int profitTarget2 = 8; // Default setting for ProfitTarget2
        private int stopLoss = 16; // Default setting for StopLoss
        private int mALen = 20; // Default setting for MALen
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(ZZSwingIdentifierHigh(4));
            Add(SMA(Open, MALen));
            SetProfitTarget("long", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("short", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);
			
			// Calculated on close
            CalculateOnBarClose = false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			//Print(" swing point high = " +ZZSwingIdentifierHigh(4).SwingPointLow[0]);
            // Condition set 1
            if (CrossAbove(Close, ZZSwingIdentifierHigh(4).SwingPointHigh[0], 1)
                && Close[0] > SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat)
            {
                EnterLong(2, "long");
            }

            // Condition set 2
            if (CrossBelow(Close, ZZSwingIdentifierHigh(4).SwingPointLow[0], 1)
                && Close[0] < SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat)
            {
                EnterShort(2, "short");
            }
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
        #endregion
    }
}
