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
    /// Buy the dip of SMAs sloping up
    /// </summary>
    [Description("Buy the dip of SMAs sloping up")]
    public class ZZBuyTheDip : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int target = 20; // Default setting for Target
        private int stop = 20; // Default setting for Stop
        private int shortMALength = 20; // Default setting for ShortMALength
        private int mediumMALength = 50; // Default setting for MediumMALength
        private int longMALenght = 200; // Default setting for LongMALenght
        private int stochPeriod = 12; // Default setting for StochPeriod
        private double slopeUpThres = 0.02; // Default setting for SlopeUpThres
        private double slopeDownThres = -0.02; // Default setting for SlopeDownThres
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(SMA(ShortMALength));
            Add(SMA(Close, MediumMALength));
            Add(SMA(LongMALenght));
            SetProfitTarget("long", CalculationMode.Ticks, Target);
            SetStopLoss("long", CalculationMode.Ticks, Stop, false);
            SetProfitTarget("short", CalculationMode.Ticks, Target);
            SetStopLoss("short", CalculationMode.Ticks, Stop, false);

            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Condition set 1
            if (Slope(SMA(ShortMALength), 3, 0) > 0.02
                && Slope(SMA(Close, MediumMALength), 3, 0) > 0.02
                && CrossBelow(Low, SMA(ShortMALength), 1) == true
                && Slope(SMA(LongMALenght), 2, 0) > 0.02)
            {
                EnterLong(1, "long");
            }

            // Condition set 2
            if (Slope(SMA(ShortMALength), 3, 0) < -0.02
                && Slope(SMA(Close, MediumMALength), 3, 0) < -0.02
                && CrossAbove(High, SMA(ShortMALength), 1) == true
                && Slope(SMA(LongMALenght), 2, 0) < -0.002)
            {
                EnterShort(1, "short");
            }
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int Target
        {
            get { return target; }
            set { target = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Stop
        {
            get { return stop; }
            set { stop = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int ShortMALength
        {
            get { return shortMALength; }
            set { shortMALength = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MediumMALength
        {
            get { return mediumMALength; }
            set { mediumMALength = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int LongMALenght
        {
            get { return longMALenght; }
            set { longMALenght = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int StochPeriod
        {
            get { return stochPeriod; }
            set { stochPeriod = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double SlopeUpThres
        {
            get { return slopeUpThres; }
            set { slopeUpThres = Math.Max(0, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double SlopeDownThres
        {
            get { return slopeDownThres; }
            set { slopeDownThres = Math.Max(0, value); }
        }
        #endregion
    }
}

#region Wizard settings, neither change nor remove
/*@
<?xml version="1.0" encoding="utf-16"?>
<NinjaTrader>
  <Name>ZZBuyTheDip</Name>
  <CalculateOnBarClose>True</CalculateOnBarClose>
  <Description>Buy the dip of SMAs sloping up</Description>
  <Parameters>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>20</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>Target</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>20</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>Stop</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>20</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>ShortMALength</Name>
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
      <Name>MediumMALength</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>200</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>LongMALenght</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>12</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>StochPeriod</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>0.02</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>0</Minimum>
      <Name>SlopeUpThres</Name>
      <Type>double</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>-0.02</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>0</Minimum>
      <Name>SlopeDownThres</Name>
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
                  <string>"long"</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(ShortMALength)</string>
                    <string>3</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>ShortMALength</string>
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
                          <DisplayName>ShortMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>ShortMALength</MemberName>
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
                    <string>0.02</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(Close, MediumMALength)</string>
                    <string>3</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>Close</string>
                        <string>MediumMALength</string>
                        <string>True</string>
                      </Values>
                      <WizardItems>
                        <StrategyWizardItem>
                          <DisplayName>Close</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>false</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>Close</MemberName>
                          <Parameters />
                          <Values />
                          <WizardItems />
                        </StrategyWizardItem>
                        <StrategyWizardItem>
                          <DisplayName>MediumMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>MediumMALength</MemberName>
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
                    <string>0.02</string>
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
                  <DisplayName>Cross below</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>CrossBelow</MemberName>
                  <Parameters>
                    <string>series1</string>
                    <string>series2</string>
                    <string>lookbackPeriod</string>
                  </Parameters>
                  <Values>
                    <string>Low</string>
                    <string>SMA(ShortMALength)</string>
                    <string>1</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>Low</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Low</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>ShortMALength</string>
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
                          <DisplayName>ShortMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>ShortMALength</MemberName>
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
                <Operator>==</Operator>
                <Right>
                  <DisplayName>True</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>true</MemberName>
                  <Parameters />
                  <Values>
                    <string>true</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(LongMALenght)</string>
                    <string>2</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>LongMALenght</string>
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
                          <DisplayName>LongMALenght</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>LongMALenght</MemberName>
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
                    <string>0.02</string>
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
                  <string>"short"</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(ShortMALength)</string>
                    <string>3</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>ShortMALength</string>
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
                          <DisplayName>ShortMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>ShortMALength</MemberName>
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
                    <string>-0.02</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(Close, MediumMALength)</string>
                    <string>3</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>Close</string>
                        <string>MediumMALength</string>
                        <string>False</string>
                      </Values>
                      <WizardItems>
                        <StrategyWizardItem>
                          <DisplayName>Close</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>false</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>Close</MemberName>
                          <Parameters />
                          <Values />
                          <WizardItems />
                        </StrategyWizardItem>
                        <StrategyWizardItem>
                          <DisplayName>MediumMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>MediumMALength</MemberName>
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
                    <string>-0.02</string>
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
                  <DisplayName>Cross above</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>CrossAbove</MemberName>
                  <Parameters>
                    <string>series1</string>
                    <string>series2</string>
                    <string>lookbackPeriod</string>
                  </Parameters>
                  <Values>
                    <string>High</string>
                    <string>SMA(ShortMALength)</string>
                    <string>1</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>High</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>High</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>ShortMALength</string>
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
                          <DisplayName>ShortMALength</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>ShortMALength</MemberName>
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
                <Operator>==</Operator>
                <Right>
                  <DisplayName>True</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>true</MemberName>
                  <Parameters />
                  <Values>
                    <string>true</string>
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
                  <DisplayName>Slope</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Slope</MemberName>
                  <Parameters>
                    <string>series</string>
                    <string>startBar</string>
                    <string>endBar</string>
                  </Parameters>
                  <Values>
                    <string>SMA(LongMALenght)</string>
                    <string>2</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>SMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>SMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Period</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>LongMALenght</string>
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
                          <DisplayName>LongMALenght</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>LongMALenght</MemberName>
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
                    <string>-0.002</string>
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
              <string>"long"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>Target</string>
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
                <DisplayName>Target</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>Target</MemberName>
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
              <string>"long"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>Stop</string>
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
                <DisplayName>Stop</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>Stop</MemberName>
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
              <string>"short"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>Target</string>
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
                <DisplayName>Target</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>Target</MemberName>
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
              <string>"short"</string>
              <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
              <string>Stop</string>
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
                <DisplayName>Stop</DisplayName>
                <IsIndicator>false</IsIndicator>
                <IsInt>true</IsInt>
                <IsMethod>false</IsMethod>
                <IsSet>true</IsSet>
                <MemberName>Stop</MemberName>
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
