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
    /// Implementation of traderclutch
    /// </summary>
    [Description("Implementation of traderclutch")]
    public class ZZKinlayScalp : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int PTLVticks = 30; // Default setting for PTLVticks
        private int PTSVticks = 2;  // Default setting for PTSVticks
		private int SLLVticks = 2;  // Default setting for SLLVticks
		private int SLSVticks = 30;	// Default setting for 
        private int stopLoss = 16;  // Default setting for StopLoss
        private int mALen = 20; 	// Default setting for MALen
        private int swingSize = 4; 	// Default setting for SwingSize
		
        // User defined variables (add any user defined variables below)
		private int BN=1;
		private int Len=10;
		private double PT = 0;
		private double SL = 0;

		private double  TrueRange=0;
		private double upsideVol=0;
		private double downsideVol=0;
		private double upsideFCSTVOL=0;
		private double downsideFCSTVOL=0;
		private double upperVolThreshold=3; 
		private double lowerVolThreshold=0.25;
		
		private double plusTrueRange_1;
		private double minusTrueRange_1;
		private double plusTrueRange_0;
		private double minusTrueRange_0;
		
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
			
            SetProfitTarget("BuyLongVol", CalculationMode.Ticks, PTLVticks);
            SetStopLoss("BuyLongVol", CalculationMode.Ticks, SLLVticks, false);
            
			SetProfitTarget("SellLongVol", CalculationMode.Ticks, PTLVticks);
            SetStopLoss("SellLongVol", CalculationMode.Ticks, SLLVticks, false);

			SetProfitTarget("BuyShortVol", CalculationMode.Ticks, PTSVticks);
            SetStopLoss("BuyShortVol", CalculationMode.Ticks, SLSVticks, false);
			
			SetProfitTarget("SellShortVol", CalculationMode.Ticks, PTSVticks);
            SetStopLoss("SellShortVol", CalculationMode.Ticks, SLSVticks, false);
			
            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {		
			if(Bars.FirstBarOfSession) 
			{
				BN=1;
				plusTrueRange_0 = High[0]-Low[0];
				minusTrueRange_0 = High[0]-Low[0];
			} 
			else
			{
				BN = BN + 1;
			}
			
			TrueRange = High[0] - Low[0];
			
			Print("New Bar");
			if (Close[0] > Close[1]) {
				upsideFCSTVOL = TrueRange + (TrueRange-plusTrueRange_1)*0.5;
				plusTrueRange_1 = TrueRange;
				//plusTrueRange_0 = TrueRange;
				Print("upsideFCSTVOL=" + upsideFCSTVOL);
			}
			
			if (Close[0] < Close[1]) { 
				downsideFCSTVOL = TrueRange + (TrueRange - minusTrueRange_1)*0.5; 
				minusTrueRange_1 = TrueRange;
				//minusTrueRange_0 = TrueRange; 
				Print("downsideFCSTVOL=" + downsideFCSTVOL);
			}
			
			if ((BN >= Len - 1) 
				&& Position.MarketPosition == MarketPosition.Flat
				&& upsideFCSTVOL > upperVolThreshold )
			{
				EnterLong(1,"BuyLongVol"); 
				PT = PTLVticks; 
				SL = SLLVticks; 			
			}

			if ((BN >= Len - 1) 
				&& Position.MarketPosition == MarketPosition.Flat
				&& downsideFCSTVOL > upperVolThreshold )
			{
				EnterShort(1,"SellLongVol"); 
				PT = PTLVticks; 
				SL = SLLVticks; 			
			}

			if ((BN >= Len - 1) 
				&& Position.MarketPosition == MarketPosition.Flat
				&& upsideFCSTVOL < lowerVolThreshold )
			{
				EnterShortLimit(1,GetCurrentAsk(),"SellShortVol"); 
				PT = PTSVticks; 
				SL = SLSVticks; 			
			}			
			
			if ((BN >= Len - 1) 
				&& Position.MarketPosition == MarketPosition.Flat
				&& downsideFCSTVOL < lowerVolThreshold )
			{
				EnterLongLimit(1,GetCurrentBid(),"BuyShortVol"); 
				PT = PTSVticks; 
				SL = SLSVticks; 			
			}	

        }

        #region Properties
        [Description("Ticks")]
        [GridCategory("Parameters")]
        public int pTLVticks
        {
            get { return PTLVticks; }
            set { PTLVticks = Math.Max(1, value); }
        }

        [Description("Ticks")]
        [GridCategory("Parameters")]
        public int pTSVticks
        {
            get { return PTSVticks; }
            set { PTSVticks = Math.Max(1, value); }
        }

		[Description("Ticks")]
        [GridCategory("Parameters")]
        public int sLLVticks
        {
            get { return SLLVticks; }
            set { SLLVticks = Math.Max(1, value); }
        }
	
		[Description("Ticks")]
        [GridCategory("Parameters")]
        public int sLSVticks
        {
            get { return SLSVticks; }
            set { SLSVticks = Math.Max(1, value); }
        }
		
        [Description("")]
        [GridCategory("Parameters")]
        public double UpperVolThreshold
        {
            get { return upperVolThreshold; }
            set { upperVolThreshold = Math.Max(0, value); }
        }


        [Description("")]
        [GridCategory("Parameters")]
        public double LowerVolThreshold
        {
            get { return lowerVolThreshold; }
            set { lowerVolThreshold = Math.Max(0, value); }
        }

        [Description("swing size in ticks")]
        [GridCategory("Parameters")]
        public int SwingSize
        {
            get { return swingSize; }
            set { swingSize = Math.Max(1, value); }
        }
        #endregion
    }
}