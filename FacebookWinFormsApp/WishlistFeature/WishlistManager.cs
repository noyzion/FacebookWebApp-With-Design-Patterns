// WishlistLogic.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class WishlistManager : IWishlistManager
    {
        public List<CategoryListWrapper> WishlistValues { get; set; }
        public WishlistManager()
        {
            WishlistValues = new List<CategoryListWrapper>();
        }
        public void AddWishToWishlistValues(string i_Category, string i_ItemName, string i_PhotoUrl)
        {
            WishListItem newItem = i_PhotoUrl != null
                ? WishListItemFactory.CreateWithPhoto(i_ItemName, i_PhotoUrl)
                : WishListItemFactory.CreateWithoutPhoto(i_ItemName);
            CategoryListWrapper existingCategoryList = WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == i_Category);

            if (existingCategoryList == null)
            {
                WishlistValues.Add(new CategoryListWrapper(i_Category, new List<WishListItem> { newItem }));
            }
            else
            {
                if (!existingCategoryList.ListOfWishlists.Contains(newItem))
                {
                    existingCategoryList.ListOfWishlists.Add(newItem);
                }
                else
                {
                    throw new Exception("You can't add two items with the same name to the same list!");
                }
            }
        }
        public void RemoveWishFromWishlistValues(string i_Category, WishListItem i_ItemToRemove)
        {
            CategoryListWrapper existingCategory = WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == i_Category);

            if (existingCategory != null)
            {
                existingCategory.ListOfWishlists.Remove(i_ItemToRemove);
                if (existingCategory.ListOfWishlists.Count == 0)
                {
                    WishlistValues.Remove(existingCategory);
                }
            }
        }
        public List<WishListItem> GetItemsByCategory(string i_Category)
        {
            List<WishListItem> items;

            if (WishlistValues == null)
            {
                items = null;
            }

            items = WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == i_Category)?.ListOfWishlists ?? new List<WishListItem>();

            return items;
        }
        public WishListItem FindWishListItemByName(string i_Category, string i_ItemName)
        {
            return WishlistValues
                .FirstOrDefault(kvp => kvp.KeyCategory.Equals(i_Category, StringComparison.OrdinalIgnoreCase))
                ?.ListOfWishlists
                .FirstOrDefault(item => item.Text.Equals(i_ItemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

