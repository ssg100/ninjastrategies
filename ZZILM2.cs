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
    [Description("ILM strat  take")]
    public class ZZILM2 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private bool tradeTue = true; // Default setting for TradeTue
        private bool tradeHugeRange = true; // Default setting for TradeHugeRange
        private int numContracts = 3; // Default setting for NumContracts
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Condition set 1
            if (CrossAbove(Close, Variable0, 1))
            {
                EnterLong(NumContracts, "Long1");
                EnterLong(NumContracts, "Long2");
                EnterLong(NumContracts, "Long3");
            }

            // Condition set 2
            if (CrossAbove(Close, Variable1, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
                ExitLong("ExitLong1", "Long1");
            }

            // Condition set 3
            if (Position.MarketPosition == MarketPosition.Long
                && CrossAbove(Close, Variable2, 1))
            {
                ExitLong("ExitLong2", "Long2");
            }

            // Condition set 4
            if (CrossAbove(Close, Variable3, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
                ExitLong("ExitLong3", "Long3");
            }

            // Condition set 5
            if (CrossBelow(Close, Variable4, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
                ExitLong("StoppedOut1", "Long1");
            }

            // Condition set 6
            if (CrossBelow(Close, Variable5, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
                ExitLong("StoppedOut2", "Long2");
            }

            // Condition set 7
            if (CrossBelow(Close, Variable6, 1)
                && Position.MarketPosition == MarketPosition.Long)
            {
                ExitLong("StoppedOut3", "Long3");
            }
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
