using System.Collections.Generic;

namespace PixiuTracker.Forms.Out
{
    public class PortfolioCoinForm
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }
        
        public double Value 
        {
            get 
            {
                return Amount * Price;
            }
        }
        // 24 hs
    }
}
