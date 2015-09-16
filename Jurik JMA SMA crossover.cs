// ==============================================
// NinjaTrader module by Jurik Research Software
// © 2010 Jurik Research   ;   www.jurikres.com
// ==============================================
//
// DEMONSTRATION CODE SHOWING HOW TO PLOT AN INDICATOR
// AND ALSO USE IT AS A FUNCTION WITHIN A STRATEGY.
//
// ==============================================

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

namespace NinjaTrader.Strategy
{
	#region Header
		[Description("simple JMA/SMA crossover")]
		public class Jurik_JMA_SMA_xover : Strategy
		#endregion
	{
		#region Variables	// default values
			private double j_len = 7; 
			private double j_phase = 50; 
			private int s_len = 15; 
			// --------------------------------
			private DataSeries JMAseries;
			private DataSeries SMAseries;
			#endregion
		
		#region Input Parameters
			[Description("JMA length, any value >= 1")]
			[GridCategory("Parameters")]
			public double JMA_len
			{
				get { return j_len; }
				set { j_len = Math.Max(1, value); }
			}
	
			[Description("JMA phase, any value between -100 and +100")]
			[GridCategory("Parameters")]
			public double JMA_phase
			{
				get { return j_phase; }
				set { j_phase = Math.Max(-100, Math.Min(100,value)); }
			}	
			
			[Description("SMA length, any integer > 1")]
			[GridCategory("Parameters")]
			public int SMA_len
			{
				get { return s_len; }
				set { s_len = Math.Max(2, value); }
			}
			#endregion

        protected override void Initialize()
        {
			#region Chart Features
				Add(Jurik_JMA_custom( 0, JMA_len, JMA_phase));		
				Add(SMA(SMA_len));					
	        	CalculateOnBarClose	= true;
				#endregion
			
			#region Series Initialization
				JMAseries = new DataSeries(this);	// sync dataseries to historical data bars
				SMAseries = new DataSeries(this);	// sync dataseries to historical data bars
				#endregion
        }

        protected override void OnBarUpdate()
        {
			#region Strategy Formula
			
				JMAseries.Set(Jurik_JMA_custom(0, JMA_len, JMA_phase).JMA_Series[0]);
				SMAseries.Set(SMA(SMA_len)[0]);
				
				if (CrossAbove(JMAseries, SMAseries, 1))
					EnterLong(1, "L");
				else if (CrossBelow(JMAseries, SMAseries, 1))
					EnterShort(1, "S");
				#endregion
        }
    }
}
