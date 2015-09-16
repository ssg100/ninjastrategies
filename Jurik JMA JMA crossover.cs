// ==============================================
// NinjaTrader module by Jurik Research Software
// © 2010 Jurik Research   ;   www.jurikres.com
// ==============================================
// DEMONSTRATION CODE SHOWING HOW TO PLOT AN INDICATOR
// AND ALSO USE IT AS A FUNCTION WITHIN A STRATEGY.
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
		[Description("simple JMA/JMA crossover")]
		public class Jurik_JMA_JMA_xover : Strategy
		#endregion
    {
		#region Variables	// default values
			private double fast_jma_len = 21; 	
			private double fast_jma_phase = -100; 
			private int    slow_jma_lag = 2;
			private double slow_jma_len = 25; 
			private double slow_jma_phase = 75; 
			// ---------------------------------
			private DataSeries JMAseries1;
			private DataSeries JMAseries2;
			#endregion
		
		#region Input Parameters
			[Description("fast JMA length, any value >= 1")]
			[GridCategory("Parameters")]
			public double fast_JMA_len
			{
				get { return fast_jma_len; }
				set { fast_jma_len = Math.Max(1, value); }
			}
			
			[Description("fast JMA phase, any value between -100 and +100")]
			[GridCategory("Parameters")]
			public double fast_JMA_phase
			{
				get { return fast_jma_phase; }
				set { fast_jma_phase = Math.Max(-100, Math.Min(100,value)); }
			}	
	
			[Description("slow JMA lag, any integer >= 0")]
			[GridCategory("Parameters")]
			public int slow_JMA_lag
			{
				get { return slow_jma_lag; }
				set { slow_jma_lag = Math.Max(-100, Math.Min(100,value)); }
			}	
			
			[Description("slow JMA length, any value >= 1")]
			[GridCategory("Parameters")]
			public double slow_JMA_len
			{
				get { return slow_jma_len; }
				set { slow_jma_len = Math.Max(1, value); }
			}
			
			[Description("slow JMA phase, any value between -100 and +100")]
			[GridCategory("Parameters")]
			public double slow_JMA_phase
			{
				get { return slow_jma_phase; }
				set { slow_jma_phase = Math.Max(-100, Math.Min(100,value)); }
			}	
			#endregion

		protected override void Initialize()
        {
			#region Chart Features
				Add(Jurik_JMA_custom( 0, fast_JMA_len , fast_JMA_phase));
				Add(Jurik_JMA_custom( slow_JMA_lag, slow_JMA_len , slow_JMA_phase ));
				Jurik_JMA_custom( 0, fast_JMA_len , fast_JMA_phase).Plots[0].Pen.Color = Color.Blue;
				Jurik_JMA_custom( slow_JMA_lag, slow_JMA_len , slow_JMA_phase).Plots[0].Pen.Color = Color.Black;
				CalculateOnBarClose = true;
				#endregion
			
			#region Series Initialization
				JMAseries1 = new DataSeries(this);	// sync dataseries to historical data bars
				JMAseries2 = new DataSeries(this);	// sync dataseries to historical data bars
				#endregion
		}

        protected override void OnBarUpdate()
        {
			#region Strategy Formula
				JMAseries1.Set( Jurik_JMA_custom( 0, fast_JMA_len , fast_JMA_phase ).JMA_Series[0] );
				JMAseries2.Set( Jurik_JMA_custom( slow_JMA_lag, slow_JMA_len , slow_JMA_phase ).JMA_Series[0] );
				
				if (CrossAbove(JMAseries1, JMAseries2, 1))
					EnterLong(1, "L");
				else if (CrossBelow(JMAseries1, JMAseries2, 1))
					EnterShort(1, "S");
				#endregion			
        }
    }
}
