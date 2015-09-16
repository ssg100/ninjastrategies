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
		[Description("trade signals from RSX on RSX on price")]
		public class Jurik_RSX_on_RSX : Strategy
		#endregion
    {
		#region Variables	// default values
			private double bandwidth = 15;
			private double r_len = 45;
			private double blend = 0.7;
			private double phaseshift = 0.4;
			// --------------------------------
			private double RSX_blend = 0;
			private double phased_blend0 = 0;
			private double phased_blend1 = 0;		
			#endregion
		
		#region Input Parameters
			[Description("RSX channel width, any value between 0 and 100")]
			[GridCategory("Parameters")]
			public double __ChannelWidth
			{
				get { return bandwidth; }
				set { bandwidth = Math.Min(100, Math.Max(-50, value)); }
			}
					
			[Description("RSX length, any value >= 2")]
			[GridCategory("Parameters")]
			public double _RSX_len
			{
				get { return r_len; }
				set { r_len = Math.Max(2, value); }
			}
			
			[Description("RSX blend factor, any value between -2 and +2")]
			[GridCategory("Parameters")]
			public double Blend
			{
				get { return blend; }
				set { blend = Math.Min(2, Math.Max(-2, value)); }
			}
			
			[Description("color phase shift, any value between -2 and +2")]
			[GridCategory("Parameters")]
			public double PhaseShift
			{
				get { return phaseshift; }
				set { phaseshift =  Math.Min(2, Math.Max(-2, value)); }
			}
			#endregion

        protected override void Initialize()
        {
			#region Chart Features		
				Add(Jurik_RSX_on_RSX(__ChannelWidth, -__ChannelWidth, _RSX_len, Blend, PhaseShift));		
				CalculateOnBarClose	= true;
				#endregion
			
			#region Series Initialization
				// none
				#endregion
        }

        protected override void OnBarUpdate()
        {
			#region Strategy Formula
				RSX_blend     = Jurik_RSX_on_RSX(0, 0, _RSX_len, Blend, PhaseShift).RSX_blend[0];
				phased_blend0 = Jurik_RSX_on_RSX(0, 0, _RSX_len, Blend, PhaseShift).Phased_blend[0];
				phased_blend1 = Jurik_RSX_on_RSX(0, 0, _RSX_len, Blend, PhaseShift).Phased_blend[1];
				
				if (phased_blend0 > 0)
				{
					ExitShort();
					if (phased_blend1 <= 0 && RSX_blend < __ChannelWidth) 
						EnterLong(1, "L");
				}
				
				else if (phased_blend0 < 0)
				{
					ExitLong();
					if (phased_blend1 >= 0 && RSX_blend > -__ChannelWidth) 
						EnterShort(1, "S");
				}
				#endregion
        }
    }
}
