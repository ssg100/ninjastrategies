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
    /// Just using one jurik jma
    /// </summary>
    [Description("Just using one jurik jma")]
    public class ZZJurikOneJMA : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int jMALEN = 50; // Default setting for JMALEN
        private double slopeSize = 0.05; // Default setting for SlopeSize
        private int target = 1; // Default setting for Target
        private int stop = 1; // Default setting for Stop
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
            if (Slope(JurikJMA(50, JMALEN), 5, 0) >= SlopeSize
                && Close[0] > JurikJMA(50, JMALEN)[0]
                && Position.MarketPosition == MarketPosition.Flat
                && Rising(JurikJMA(50, JMALEN)) == true)
            {
                EnterLong(DefaultQuantity, "Long");
            }

            // Condition set 2
            if (Position.MarketPosition == MarketPosition.Long
                && CrossBelow(Close, JurikJMA(50, JMALEN), 1))
            {
                ExitLong("CloseLong", "Long");
            }
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int JMALEN
        {
            get { return jMALEN; }
            set { jMALEN = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double SlopeSize
        {
            get { return slopeSize; }
            set { slopeSize = Math.Max(0.0001, value); }
        }

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
        #endregion
    }
}

#region Wizard settings, neither change nor remove
/*@
<?xml version="1.0" encoding="utf-16"?>
<NinjaTrader>
  <Name>ZZJurikOneJMA</Name>
  <CalculateOnBarClose>True</CalculateOnBarClose>
  <Description>Just using one jurik jma</Description>
  <Parameters>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>50</Default2>
      <Default3>
      </Default3>
      <Description>
      </Description>
      <Minimum>1</Minimum>
      <Name>JMALEN</Name>
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
      <Minimum>0.0001</Minimum>
      <Name>SlopeSize</Name>
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
      <Name>Target</Name>
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
      <Name>Stop</Name>
      <Type>int</Type>
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
                  <string>DefaultQuantity</string>
                  <string>"Long"</string>
                </Values>
                <WizardItems>
                  <StrategyWizardItem>
                    <DisplayName>DefaultQuantity</DisplayName>
                    <IsIndicator>false</IsIndicator>
                    <IsInt>true</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName>DefaultQuantity</MemberName>
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
                    <string>JurikJMA(50, JMALEN)</string>
                    <string>5</string>
                    <string>0</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>JurikJMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>JurikJMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Phase</string>
                        <string>Smoothing</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>50</string>
                        <string>JMALEN</string>
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
                          <DisplayName>50</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>false</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>50</MemberName>
                          <Parameters />
                          <Values />
                          <WizardItems />
                        </StrategyWizardItem>
                        <StrategyWizardItem>
                          <DisplayName>JMALEN</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>JMALEN</MemberName>
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
                <Operator>&gt;=</Operator>
                <Right>
                  <DisplayName>SlopeSize</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>SlopeSize</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
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
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&gt;</Operator>
                <Right>
                  <DisplayName>JurikJMA</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>JurikJMA</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>Phase</string>
                    <string>Smoothing</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>50</string>
                    <string>JMALEN</string>
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
                      <DisplayName>50</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>50</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>JMALEN</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>JMALEN</MemberName>
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
                  <DisplayName>Rising</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Rising</MemberName>
                  <Parameters>
                    <string>series</string>
                  </Parameters>
                  <Values>
                    <string>JurikJMA(50, JMALEN)</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>JurikJMA</DisplayName>
                      <IsIndicator>true</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>true</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>JurikJMA</MemberName>
                      <Parameters>
                        <string>	inputSeries</string>
                        <string>Phase</string>
                        <string>Smoothing</string>
                        <string>	plotOnChart</string>
                      </Parameters>
                      <Values>
                        <string>DefaultInput</string>
                        <string>50</string>
                        <string>JMALEN</string>
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
                          <DisplayName>50</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>false</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>50</MemberName>
                          <Parameters />
                          <Values />
                          <WizardItems />
                        </StrategyWizardItem>
                        <StrategyWizardItem>
                          <DisplayName>JMALEN</DisplayName>
                          <IsIndicator>false</IsIndicator>
                          <IsInt>true</IsInt>
                          <IsMethod>false</IsMethod>
                          <IsSet>true</IsSet>
                          <MemberName>JMALEN</MemberName>
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
            </Conditions>
          </StrategyWizardStateSet>
          <StrategyWizardStateSet>
            <Actions>
              <StrategyWizardAction>
                <DisplayName>Exit long position</DisplayName>
                <Help />
                <MemberName>ExitLong</MemberName>
                <Parameters>
                  <string>signalName</string>
                  <string>fromEntrySignal</string>
                </Parameters>
                <Values>
                  <string>"CloseLong"</string>
                  <string>"Long"</string>
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
                </WizardItems>
              </StrategyWizardAction>
            </Actions>
            <Conditions>
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
                  <DisplayName>Long</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>MarketPosition.Long</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Right>
              </StrategyWizardCondition>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
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
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>CrossBelow</Operator>
                <Right>
                  <DisplayName>JurikJMA</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>JurikJMA</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>Phase</string>
                    <string>Smoothing</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>50</string>
                    <string>JMALEN</string>
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
                      <DisplayName>50</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>50</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>JMALEN</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>JMALEN</MemberName>
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
                </Right>
              </StrategyWizardCondition>
            </Conditions>
          </StrategyWizardStateSet>
        </Sets>
        <StopTargets />
      </StrategyWizardState>
    </CurrentState>
  </State>
</NinjaTrader>
@*/
#endregion
