using BasicFacebookFeatures;
using System.Linq;
using System.Text;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
namespace BasicFacebookFeatures
{
    public class WishlistFacade
    {
       public WishlistManager r_WishlistManager { get; set; }
        private readonly WishlistUIManager r_WishlistUIManager;

        public WishlistFacade()
        {
            r_WishlistManager = new WishlistManager();
            r_WishlistUIManager = new WishlistUIManager();
        }

        public void AddWish(string category, string itemName, string photoUrl)
        {
            r_WishlistManager.AddWishToWishlistValues(category, itemName, photoUrl);
        }

        public void RemoveWish(string category, WishListItem itemToRemove)
        {
            r_WishlistManager.RemoveWishFromWishlistValues(category, itemToRemove);
        }

        public void UpdateUI(CheckedListBox foodListBox, CheckedListBox petsListBox,
                             CheckedListBox activitiesListBox, CheckedListBox shoppingListBox,
                             string category, WishListItem wishlistItem)
        {
            r_WishlistUIManager.UpdateCheckedListBox(foodListBox, petsListBox, activitiesListBox, shoppingListBox, category, wishlistItem);
        }

        public void ResetUI(CheckedListBox foodListBox, CheckedListBox petsListBox,
                            CheckedListBox activitiesListBox, CheckedListBox shoppingListBox)
        {
            r_WishlistUIManager.ResetWishlistUI(foodListBox, petsListBox, activitiesListBox, shoppingListBox);
        }
        public WishListItem FindWishListItemByName(string category, string itemName)
        {
            return r_WishlistManager.GetItemsByCategory(category).FirstOrDefault(item => item.Text == itemName);
        }

        public void LoadImageForPictureBoxInList(WishListItem wishListItem, PictureBox pictureBox)
        {
            if (!string.IsNullOrEmpty(wishListItem?.PhotoUrl))
            {
                try
                {
                    pictureBox.Image = Image.FromFile(wishListItem.PhotoUrl);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex.Message}");
                }
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        public string ShareWishlist(CheckedListBox foodListBox, CheckedListBox activitiesListBox,
                                    CheckedListBox petsListBox, CheckedListBox shoppingListBox)
        {
            var wishlistSummary = new StringBuilder("My Wishlist:\n");
            wishlistSummary.AppendLine($"Food: {GetCheckedItems(foodListBox)}");
            wishlistSummary.AppendLine($"Activities: {GetCheckedItems(activitiesListBox)}");
            wishlistSummary.AppendLine($"Pets: {GetCheckedItems(petsListBox)}");
            wishlistSummary.AppendLine($"Shopping: {GetCheckedItems(shoppingListBox)}");
            r_WishlistUIManager.DisplayCombinedWishlistPopup(foodListBox,activitiesListBox, petsListBox, shoppingListBox);
            return wishlistSummary.ToString();
        }


        private string GetCheckedItems(CheckedListBox listBox)
        {
            return string.Join(", ", listBox.Items.Cast<WishListItem>().Where(item => item.Checked).Select(item => item.Text));
        }
        public WishListItem FindAndHighlightItem(string category, string itemName, PictureBox pictureBox, Button deleteButton)
        {
            WishListItem item = r_WishlistManager.FindWishListItemByName(category, itemName);
            r_WishlistUIManager.HighlightItemInList(item, pictureBox, deleteButton);
            return item;
        }
        public List<WishListItem> GetItemsByCategory(string category)
        {
            return r_WishlistManager.GetItemsByCategory(category);
        }

    }
}