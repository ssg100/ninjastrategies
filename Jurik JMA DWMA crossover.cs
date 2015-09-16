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
		[Description("simple JMA/DWMA crossover")]
		public class Jurik_JMA_DWMA_xover : Strategy
		#endregion
	{
		#region Variables	// default values
			private int d_len = 4; 
			private double j_len = 10; 
			private double j_phase = -100; 
			// --------------------------------
			private DataSeries JMAseries;
			private DataSeries DWMAseries;
			#endregion
		
		#region Input Parameters    
			[Description("DWMA length, any integer > 2")]
			[GridCategory("Parameters")]
			public int DWMA_len
			{
				get { return d_len; }
				set { d_len = Math.Max(3, value); }
			}
	
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
			#endregion

        protected override void Initialize()
        {
			#region Chart Features
				Add(Jurik_JMA_DWMA_crossover(DWMA_len, JMA_len, JMA_phase));			
				CalculateOnBarClose	= true;
				#endregion
			
			#region Series Initialization
				JMAseries = new DataSeries(this);	// sync dataseries to historical data bars
				DWMAseries = new DataSeries(this);	// sync dataseries to historical data bars
				#endregion
        }

        protected override void OnBarUpdate()
        {
			#region Strategy Formula
				JMAseries.Set(Jurik_JMA_DWMA_crossover(DWMA_len, JMA_len, JMA_phase).JMA_Series[0]);
				DWMAseries.Set(Jurik_JMA_DWMA_crossover(DWMA_len, JMA_len, JMA_phase).DWMA_Series[0]);
				
				if (CrossAbove(JMAseries, DWMAseries, 1))
					EnterLong(1, "L");
				else if (CrossBelow(JMAseries, DWMAseries, 1))
					EnterShort(1, "S");
				#endregion			
        }
    }
}
