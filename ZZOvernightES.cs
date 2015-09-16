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
        private int endTime = 010000; // Default setting for EndTime
        private int stopLoss = 80; // Default setting for StopLoss
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
                EnterLong(DefaultQuantity, "Long");
            }
	
            // Condition set 2
            if (
					( time >= endTime
						&& time <= endTime + 500 )
                		&& Position.MarketPosition == MarketPosition.Long
				)
            {
				Print("EXIT ToTime() = " + ToTime(Time[0]));
                ExitLong("ExitLong", "Long");
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
        #endregion
    }
}
