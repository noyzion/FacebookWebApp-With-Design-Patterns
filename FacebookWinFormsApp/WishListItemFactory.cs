using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public static class WishListItemFactory
    {
        public static WishListItem CreateWithPhoto(string i_Text, string i_PhotoUrl)
        {
            if (string.IsNullOrEmpty(i_PhotoUrl))
            {
                throw new ArgumentException("Photo URL cannot be null or empty when creating an item with a photo.", nameof(i_PhotoUrl));
            }

            return new WishListItem(i_Text, i_PhotoUrl);
        }
        public static WishListItem CreateWithoutPhoto(string i_Text)
        {
            return new WishListItem(i_Text);
        }
    }
}
