using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public interface IWishlistManager
    {
        void AddWishToWishlistValues(string category, string itemName, string photoUrl);
        void RemoveWishFromWishlistValues(string category, WishListItem itemToRemove);
        List<WishListItem> GetItemsByCategory(string category);
        WishListItem FindWishListItemByName(string category, string itemName);
    }
}
