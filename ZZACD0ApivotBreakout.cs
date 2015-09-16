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
    /// ACD A pivot only
    /// </summary>
    [Description("ACD A pivot only")]
    public class ZZACD0ApivotBreakout : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int stopLoss = 12; // Default setting for StopLoss
        private double slopeUpValue = 0.05; // Default setting for SlopeUpValue
        private int profitTarget = 40; // Default setting for ProfitTarget
        private int oRTimeRange = 4; // Default setting for ORTimeRange
        private int aTickBuffer = 1; // Default setting for ATickBuffer
        private int aTime = 2; // Default setting for ATime
        private int mAtrend = 50; // Default setting for MAtrend
        private double slopeDownValue = -0.050; // Default setting for SlopeDownValue
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(ZZACDwith3params(ATime, ORTimeRange, ATickBuffer));
            SetProfitTarget("Long", CalculationMode.Ticks, ProfitTarget);
            SetStopLoss("Long", CalculationMode.Ticks, StopLoss, false);
            SetProfitTarget("Short", CalculationMode.Ticks, ProfitTarget);
            SetStopLoss("Short", CalculationMode.Ticks, StopLoss, false);

            CalculateOnBarClose = false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Condition set 1
            if (ZZACDwith3params(ATime, ORTimeRange, ATickBuffer).Signal[0] > 5
                && Position.MarketPosition == MarketPosition.Flat
                && SMA(MAtrend)[0] < Close[0])
            {
                EnterLong(1, "Long");
            }

            // Condition set 2
            if (ZZACDwith3params(ATime, ORTimeRange, ATickBuffer).Signal[0] < -5
                && Position.MarketPosition == MarketPosition.Flat
                && SMA(MAtrend)[0] > Close[0])
            {
                EnterShort(1, "Short");
            }
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int StopLoss
        {
            get { return stopLoss; }
            set { stopLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double SlopeUpValue
        {
            get { return slopeUpValue; }
            set { slopeUpValue = Math.Max(0, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int ProfitTarget
        {
            get { return profitTarget; }
            set { profitTarget = Math.Max(1, value); }
        }

        [Description("how many bars to determine OR")]
        [GridCategory("Parameters")]
        public int ORTimeRange
        {
            get { return oRTimeRange; }
            set { oRTimeRange = Math.Max(1, value); }
        }

        [Description("Tick buffer to filter A")]
        [GridCategory("Parameters")]
        public int ATickBuffer
        {
            get { return aTickBuffer; }
            set { aTickBuffer = Math.Max(1, value); }
        }

        [Description("how many bars for price to stay above A")]
        [GridCategory("Parameters")]
        public int ATime
        {
            get { return aTime; }
            set { aTime = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MAtrend
        {
            get { return mAtrend; }
            set { mAtrend = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double SlopeDownValue
        {
            get { return slopeDownValue; }
            set { slopeDownValue = Math.Max(0.0, value); }
        }
        #endregion
    }
}

#region Wizard settings, neither change nor remove
/*@
<?xml version="1.0" encoding="utf-16"?>
<NinjaTrader>
  <Name>ZZACD0ApivotBreakout</Name>
  <CalculateOnBarClose>False</CalculateOnBarClose>
  <Description>ACD A pivot only</Description>
  <Parameters>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>12</Default2>
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
      <Default2>0.05</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>0</Minimum>
      <Name>SlopeUpValue</Name>
      <Type>double</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>40</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>ProfitTarget</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>4</Default2>
      <Default3>
      </Default3>
      <Description>how many bars to determine OR</Description>
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
      <Description>Tick buffer to filter A</Description>
      <Minimum>1</Minimum>
      <Name>ATickBuffer</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>2</Default2>
      <Default3>
      </Default3>
      <Description>how many bars for price to stay above A</Description>
      <Minimum>1</Minimum>
      <Name>ATime</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>50</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>MAtrend</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>-0.050</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>0.0</Minimum>
      <Name>SlopeDownValue</Name>
      <Type>double</Type>
    </Parameter>
  </Parameters>
  <State>
    <CurrentState>
      <StrategyWizardState xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <Name>Flat</Name>
        <Sets>
          <StrategyWizardStateSet>
            <Actions>
              <StrategyWizardAction>
                <DisplayName>Enter long position</DisplayName>
                <Help />
                <MemberName>EnterLong</MemberName>
                <Parameters>
                  <string>quantity</string>
                  <string>signalName</string>
                </Parameters>
                <Values>
                  <string>1</string>
                  <string>"Long"</string>
                </Values>
                <WizardItems>
                  <StrategyWizardItem>
                    <DisplayName>Numeric value</DisplayName>
                    <IsIndicator>false</IsIndicator>
                    <IsInt>true</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName>1</MemberName>
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName />
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName />
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                </WizardItems>
              </StrategyWizardAction>
            </Actions>
            <Conditions>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>ZZACDwith3params</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>ZZACDwith3params</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>NumBarAsignal</string>
                    <string>ORNumBars</string>
                    <string>TickBuffer</string>
                    <string>	plot</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>ATime</string>
                    <string>ORTimeRange</string>
                    <string>ATickBuffer</string>
                    <string>"Signal"</string>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                    <string>True</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>DefaultInput</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>DefaultInput</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ATime</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ATime</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ORTimeRange</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ORTimeRange</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ATickBuffer</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ATickBuffer</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&gt;</Operator>
                <Right>
                  <DisplayName>Numeric value</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName />
                  <Parameters>
                    <string>Value</string>
                  </Parameters>
                  <Values>
                    <string>5</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>Current market position</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Position.MarketPosition</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>==</Operator>
                <Right>
                  <DisplayName>Flat</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>MarketPosition.Flat</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>SMA</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>SMA</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>Period</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>MAtrend</string>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                    <string>False</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>DefaultInput</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>DefaultInput</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>MAtrend</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>MAtrend</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&lt;</Operator>
                <Right>
                  <DisplayName>Close</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Close</MemberName>
                  <Parameters>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                  </Parameters>
                  <Values>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Right>
              </StrategyWizardCondition>
            </Conditions>
          </StrategyWizardStateSet>
          <StrategyWizardStateSet>
            <Actions>
              <StrategyWizardAction>
                <DisplayName>Enter short position</DisplayName>
                <Help />
                <MemberName>EnterShort</MemberName>
                <Parameters>
                  <string>quantity</string>
                  <string>signalName</string>
                </Parameters>
                <Values>
                  <string>1</string>
                  <string>"Short"</string>
                </Values>
                <WizardItems>
                  <StrategyWizardItem>
                    <DisplayName>Numeric value</DisplayName>
                    <IsIndicator>false</IsIndicator>
                    <IsInt>true</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName>1</MemberName>
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName />
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName />
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                </WizardItems>
              </StrategyWizardAction>
            </Actions>
            <Conditions>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>ZZACDwith3params</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>ZZACDwith3params</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>NumBarAsignal</string>
                    <string>ORNumBars</string>
                    <string>TickBuffer</string>
                    <string>	plot</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>ATime</string>
                    <string>ORTimeRange</string>
                    <string>ATickBuffer</string>
                    <string>"Signal"</string>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                    <string>False</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>DefaultInput</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>DefaultInput</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ATime</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ATime</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ORTimeRange</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ORTimeRange</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>ATickBuffer</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>ATickBuffer</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&lt;</Operator>
                <Right>
                  <DisplayName>Numeric value</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName />
                  <Parameters>
                    <string>Value</string>
                  </Parameters>
                  <Values>
                    <string>-5</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>Current market position</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Position.MarketPosition</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>==</Operator>
                <Right>
                  <DisplayName>Flat</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>MarketPosition.Flat</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>SMA</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>SMA</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>Period</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>MAtrend</string>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                    <string>False</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>DefaultInput</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>DefaultInput</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>MAtrend</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>MAtrend</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&gt;</Operator>
                <Right>
                  <DisplayName>Close</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Close</MemberName>
                  <Parameters>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                  </Parameters>
                  <Values>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Right>
              </StrategyWizardCondition>
            </Conditions>
          </StrategyWizardStateSet>
        </Sets>
        <StopTargets>
          <StrategyWizardAction>
            <DisplayName>Profit target</DisplayName>
            <Help />
            <MemberName>SetProfitTarget</MemberName>
            <Parameters>
              <string>fromEntrySignal</string>
              <string>mode</string>
              <string>value</string>
            </Parameters>
            <Values>
              <string>"Long"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>ProfitTarget</string>
            </Values>
            <WizardItems>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName>ProfitTarget</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>ProfitTarget</MemberName>
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
            </WizardItems>
          </StrategyWizardAction>
          <StrategyWizardAction>
            <DisplayName>Stop loss</DisplayName>
            <Help />
            <MemberName>SetStopLoss</MemberName>
            <Parameters>
              <string>fromEntrySignal</string>
              <string>mode</string>
              <string>value</string>
              <string>simulated</string>
            </Parameters>
            <Values>
              <string>"Long"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>StopLoss</string>
              <string>False</string>
            </Values>
            <WizardItems>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName>StopLoss</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>StopLoss</MemberName>
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
            </WizardItems>
          </StrategyWizardAction>
          <StrategyWizardAction>
            <DisplayName>Profit target</DisplayName>
            <Help />
            <MemberName>SetProfitTarget</MemberName>
            <Parameters>
              <string>fromEntrySignal</string>
              <string>mode</string>
              <string>value</string>
            </Parameters>
            <Values>
              <string>"Short"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>ProfitTarget</string>
            </Values>
            <WizardItems>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName>ProfitTarget</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>ProfitTarget</MemberName>
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
            </WizardItems>
          </StrategyWizardAction>
          <StrategyWizardAction>
            <DisplayName>Stop loss</DisplayName>
            <Help />
            <MemberName>SetStopLoss</MemberName>
            <Parameters>
              <string>fromEntrySignal</string>
              <string>mode</string>
              <string>value</string>
              <string>simulated</string>
            </Parameters>
            <Values>
              <string>"Short"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>StopLoss</string>
              <string>False</string>
            </Values>
            <WizardItems>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName>StopLoss</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>StopLoss</MemberName>
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
              <StrategyWizardItem>
                <DisplayName />
                <IsIndicator>false</IsIndicator>
                <IsInt>false</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName />
                <Parameters />
                <Values />
                <WizardItems />
              </StrategyWizardItem>
            </WizardItems>
          </StrategyWizardAction>
        </StopTargets>
      </StrategyWizardState>
    </CurrentState>
  </State>
</NinjaTrader>
@*/
#endregion
