#region Using declarations
using System;
using System.Text;
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
    /// Implementation of traderclutch but only trade certain days of week... wed and fri
    /// </summary>
    [Description("Implementation of traderclutch but only trade certain days of week... wed and fri")]
    public class ZZTraderClutchDaysofweek : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int profitTarget1 = 16; // Default setting for ProfitTarget1
        private int profitTarget2 = 14; // Default setting for ProfitTarget2
        private int stopLoss = 14; // Default setting for StopLoss
        private int mALen = 50; // Default setting for MALen
        private int swingSize = 6; // Default setting for SwingSize
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(SMA(Open, MALen));
            SetProfitTarget("long", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("long", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("short", CalculationMode.Ticks, ProfitTarget1);
            SetStopLoss("short", CalculationMode.Ticks, StopLoss, false);

            CalculateOnBarClose = false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			string todaydate;
			
			todaydate = Time[0].Date.ToString("ddd");
			//Print("todaydate = " + todaydate);
			if( !(String.Equals(todaydate,"Wed") || String.Equals(todaydate,"Fri")) )
			{
				//Print("Wed " + todaydate);
				return;
			}
            // Condition set 1
            if (CrossAbove(Close, ZZSwingIdentifierHigh(4).SwingPointHigh, 1)
                && Close[0] > SMA(Open, MALen)[0]
                && Position.MarketPosition == MarketPosition.Flat)
            {
                EnterLong(2, "Long");
            }

            // Condition set 2
            if (CrossBelow(Close, ZZSwingIdentifierHigh(4).SwingPointLow, 1)
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
