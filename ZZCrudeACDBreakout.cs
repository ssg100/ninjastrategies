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
    /// ACD system for CL
    /// </summary>
    [Description("ACD system for CL")]
    public class ZZCrudeACDBreakout : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int oRTimeRange = 5; // Default setting for ORTimeRange
        private int aOffset = 1; // Default setting for AOffset
        private int cOffset = 1; // Default setting for COffset
        private double aWaitTime = 2.5; // Default setting for AWaitTime
        private int stopLoss = 1; // Default setting for StopLoss
        private int target = 1; // Default setting for Target
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
        }

        #region Properties
        [Description("Opening Range Time Range")]
        [GridCategory("Parameters")]
        public int ORTimeRange
        {
            get { return oRTimeRange; }
            set { oRTimeRange = Math.Max(1, value); }
        }

        [Description("TIcks of A point buffer")]
        [GridCategory("Parameters")]
        public int AOffset
        {
            get { return aOffset; }
            set { aOffset = Math.Max(1, value); }
        }

        [Description("Ticks of C point buffer")]
        [GridCategory("Parameters")]
        public int COffset
        {
            get { return cOffset; }
            set { cOffset = Math.Max(1, value); }
        }

        [Description("How long waiting for price to stay around A")]
        [GridCategory("Parameters")]
        public double AWaitTime
        {
            get { return aWaitTime; }
            set { aWaitTime = Math.Max(2.00, value); }
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
        public int Target
        {
            get { return target; }
            set { target = Math.Max(1, value); }
        }
        #endregion
    }
}

#region Wizard settings, neither change nor remove
/*@
<?xml version="1.0" encoding="utf-16"?>
<NinjaTrader>
  <Name>ZZCrudeACDBreakout</Name>
  <CalculateOnBarClose>False</CalculateOnBarClose>
  <Description>ACD system for CL</Description>
  <Parameters>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>5</Default2>
      <Default3>
      </Default3>
      <Description>Opening Range Time Range</Description>
      <Minimum>1</Minimum>
      <Name>ORTimeRange</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>1</Default2>
      <Default3>
      </Default3>
      <Description>TIcks of A point buffer</Description>
      <Minimum>1</Minimum>
      <Name>AOffset</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>1</Default2>
      <Default3>
      </Default3>
      <Description>Ticks of C point buffer</Description>
      <Minimum>1</Minimum>
      <Name>COffset</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>2.5</Default2>
      <Default3>
      </Default3>
      <Description>How long waiting for price to stay around A</Description>
      <Minimum>2.00</Minimum>
      <Name>AWaitTime</Name>
      <Type>double</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>1</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>StopLoss</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>1</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>Target</Name>
      <Type>int</Type>
    </Parameter>
  </Parameters>
  <State>
    <CurrentState>
      <StrategyWizardState xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <Name>Flat</Name>
        <Sets />
        <StopTargets />
      </StrategyWizardState>
    </CurrentState>
  </State>
</NinjaTrader>
@*/
#endregion
