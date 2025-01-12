using BasicFacebookFeatures;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class WishlistUIManager
    {
        public void UpdateCheckedListBox(CheckedListBox foodListBox, CheckedListBox petsListBox,
                                         CheckedListBox activitiesListBox, CheckedListBox shoppingListBox,
                                         string category, WishListItem wishlistItem)
        {
            switch (category)
            {
                case nameof(EWishlistCategories.Food):
                    foodListBox.Items.Add(wishlistItem);
                    break;
                case nameof(EWishlistCategories.Shopping):
                    shoppingListBox.Items.Add(wishlistItem);
                    break;
                case nameof(EWishlistCategories.Activities):
                    activitiesListBox.Items.Add(wishlistItem);
                    break;
                case nameof(EWishlistCategories.Pets):
                    petsListBox.Items.Add(wishlistItem);
                    break;
                default:
                    throw new ArgumentException("Invalid category.");
            }
        }

        public void ResetWishlistUI(CheckedListBox foodListBox, CheckedListBox petsListBox,
                                     CheckedListBox activitiesListBox, CheckedListBox shoppingListBox)
        {
            foodListBox.Items.Clear();
            petsListBox.Items.Clear();
            activitiesListBox.Items.Clear();
            shoppingListBox.Items.Clear();
        }
        public void HighlightItemInList(WishListItem item, PictureBox pictureBox, Button deleteButton)
        {
            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.PhotoUrl))
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(item.PhotoUrl);
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

                deleteButton.Enabled = true;
            }
            else
            {
                pictureBox.Image = null;
                deleteButton.Enabled = false;
            }
        }

    }
}
