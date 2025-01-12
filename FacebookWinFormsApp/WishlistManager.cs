// WishlistLogic.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class WishlistManager
    {
        public List<CategoryListWrapper> WishlistValues { get; set; }

        public WishlistManager()
        {
            WishlistValues = new List<CategoryListWrapper>();
        }

        public void AddWishToWishlistValues(string category, string itemName, string photoUrl)
        {
            WishListItem newItem = photoUrl != null
                ? WishListItemFactory.CreateWithPhoto(itemName, photoUrl)
                : WishListItemFactory.CreateWithoutPhoto(itemName);

            CategoryListWrapper existingCategoryList = WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == category);

            if (existingCategoryList == null)
            {
                WishlistValues.Add(new CategoryListWrapper(category, new List<WishListItem> { newItem }));
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

        public void RemoveWishFromWishlistValues(string category, WishListItem itemToRemove)
        {
            CategoryListWrapper existingCategory = WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == category);

            if (existingCategory != null)
            {
                existingCategory.ListOfWishlists.Remove(itemToRemove);
                if (existingCategory.ListOfWishlists.Count == 0)
                {
                    WishlistValues.Remove(existingCategory);
                }
            }
        }

        public List<WishListItem> GetItemsByCategory(string category)
        {
            if (WishlistValues == null)
                return null;
            return WishlistValues.FirstOrDefault(kvp => kvp.KeyCategory == category)?.ListOfWishlists ?? new List<WishListItem>();
        }
        public WishListItem FindWishListItemByName(string category, string itemName)
        {
            return WishlistValues
                .FirstOrDefault(kvp => kvp.KeyCategory.Equals(category, StringComparison.OrdinalIgnoreCase))
                ?.ListOfWishlists
                .FirstOrDefault(item => item.Text.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

    }

}

