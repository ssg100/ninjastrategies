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
		[Description("trades triggered by T3 on DMX")]
		public class Jurik_DMX_and_T3 : Strategy
		#endregion
	{
		#region Variables	// default values
			private int dmx_len = 10 ; 
			private double t3boost = 0.0; 
			private double t3len = 37; 
			// --------------------------------
			private double T3_value = 0;	
			private DataSeries T3_series;
			#endregion
		
		#region Input Parameters
			[Description("DMX length, any integer > 2")]
			[GridCategory("Parameters")]
			public int DMX_len
			{
				get { return dmx_len; }
				set { dmx_len = Math.Max(3, value); }
			}
			
			[Description("T3 boost, any value between 0 and 1")]
			[GridCategory("Parameters")]
			public double T3_boost
			{
				get { return t3boost; }
				set { t3boost = Math.Min(Math.Max(0, value),1); }
			}
			
			[Description("T3 length, any value >= 1")]
			[GridCategory("Parameters")]
			public double T3_length
			{
				get { return t3len; }
				set { t3len = Math.Max(1, value); }
			}
			#endregion

        protected override void Initialize()
        {
			#region Chart Features
				Add(Jurik_DMX_and_T3( DMX_len, T3_boost, T3_length));		
				CalculateOnBarClose	= false ;
				#endregion
			
			#region Series Initialization
				T3_series = new DataSeries(this);	// sync dataseries to historical data bars
				#endregion
        }

        protected override void OnBarUpdate()
        {
			#region Strategy Formula
				T3_value = Jurik_DMX_and_T3( DMX_len, T3_boost, T3_length).T3_series[0];
				T3_series.Set(T3_value);
				
				if (CrossAbove(T3_series, 0, 1))
					EnterLong(1, "L");
				else if (CrossBelow(T3_series, 0, 1))
					EnterShort(1, "S");
				#endregion
        }
    }
}
