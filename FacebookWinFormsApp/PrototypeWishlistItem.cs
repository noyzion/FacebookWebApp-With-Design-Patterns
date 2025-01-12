using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class PrototypeWishlistItem : ICloneable
    {
        public string Text { get; set; }
        public string PhotoUrl { get; set; }
        public bool Checked { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}