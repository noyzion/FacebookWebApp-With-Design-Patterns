﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private const string k_User = "C:/Users/noyzi/Downloads/UserData.xml";
        private const int k_CollectionLimit = 70;  // If the limit is bigger, it works but very slow
        private readonly AppSettings r_AppSettings;
        private readonly WishlistFacade r_WishlistManager;
        private readonly WorkoutManager r_WorkoutManager;
        private FacebookWrapper.LoginResult m_LoginResult;
        private FormAppSettings m_FormAppSettings = null;
        private bool m_IsTextBoxChanged = false;
        private bool m_IsComboBoxChanged = false;
        private FacebookFacade m_FacebookFacade;
        private readonly DataGridView r_WorkoutTable;
        private const int k_HoverEffect = 5;
        private const int k_ShowProfileSize = 600;

        public FormMain()
        {
            InitializeComponent();
            r_AppSettings = AppSettings.LoadFromFile(k_User);
            this.rememberMeCheckBox.Checked = r_AppSettings.RememberUser;
            FacebookWrapper.FacebookService.s_CollectionLimit = k_CollectionLimit;

            if(r_AppSettings.WishlistFacade == null)
            {
               r_AppSettings.WishlistFacade = new WishlistFacade();
                r_AppSettings.WishlistManager = r_AppSettings.WishlistFacade.r_WishlistManager;
            }
            if (r_AppSettings.WorkoutManager == null)
            {
                r_AppSettings.WorkoutManager = new WorkoutManager();
            }

            r_WishlistManager= r_AppSettings.WishlistFacade;
            r_WishlistManager.r_WishlistManager = r_WishlistManager.r_WishlistManager;
            r_WorkoutManager = r_AppSettings.WorkoutManager;
            r_WorkoutTable = r_WorkoutManager.InitializeWorkoutTable();
            panelWorkouts.Controls.Add(r_WorkoutTable);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.Width += k_HoverEffect;
                pictureBox.Height += k_HoverEffect;
                pictureBox.BackColor = System.Drawing.Color.LightGray;
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                pictureBox.BorderStyle = BorderStyle.None;
                pictureBox.Width -= k_HoverEffect;
                pictureBox.Height -= k_HoverEffect;
                pictureBox.BackColor = System.Drawing.Color.Transparent;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            r_AppSettings.RememberUser = this.rememberMeCheckBox.Checked;
            if (r_AppSettings.RememberUser)
            {
                r_AppSettings.LastAccessToken = m_LoginResult.AccessToken;
                r_AppSettings.WishlistFacade = r_WishlistManager;
                r_AppSettings.WorkoutManager = r_WorkoutManager;
            }
            else
            {
                r_AppSettings.LastAccessToken = null;
            }

            r_AppSettings.SaveToFile(k_User);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (r_AppSettings.RememberUser && !string.IsNullOrEmpty(r_AppSettings.LastAccessToken))
            {
                m_LoginResult = FacebookService.Connect(r_AppSettings.LastAccessToken);
                m_FacebookFacade = new FacebookFacade(m_LoginResult);
                populateUIFromFacebookData();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (m_LoginResult == null || string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                Login();
                m_FacebookFacade = new FacebookFacade(m_LoginResult);
            }
        }

        private void buttonsAfterLogin()
        {
            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
            buttonFriends.Enabled = true;
            buttonGroups.Enabled = true;
            buttonLikesPages.Enabled = true;
            buttonAlbums.Enabled = true;
            buttonPosts.Enabled = true;
            postsPicture.Enabled = true;
            photosPicture.Enabled = true;
            friendsPicture.Enabled = true;
            groupsPicture.Enabled = true;
            pictureBoxEventes.Enabled = true;
            eventsButton.Enabled = true;
            buttonAddPicture.Enabled = true;
            textBoxStatus.Enabled = true;
            buttonAddVideo.Enabled = true;
            buttonSettings.Enabled = true;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            m_LoginResult = null;
            logoutUIChanges();
        }

        private void logoutUIChanges()
        {
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            buttonLogin.ForeColor = buttonLogout.ForeColor;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
            pictureBoxProfile.ImageLocation = null;
            labelBirthday.Text = "Birthday: ";
            labelEmail.Text = "Email: ";
            r_WorkoutTable.Rows.Clear();
            r_WishlistManager.ResetUI(checkedListBoxFood, checkedListBoxPets,
                                              checkedListBoxActivities, checkedListBoxShopping);
        }

        private void groups_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchGroups(dataListBox);
        }

        private void friends_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchFriends(dataListBox);
        }

        private void posts_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchPosts(dataListBox);
        }

        private void photos_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchAlbums(dataListBox);
        }

        private void pages_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchLikedPages(dataListBox);
        }

        private void events_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.FetchEvents(dataListBox);
        }

        public void Login()
        {
            m_LoginResult = FacebookService.Login(
                            "914564353962957",
                            "email",
                            "public_profile",
                            "user_posts",
                            "user_photos",
                            "user_events",
                            "user_friends",
                            "user_likes"
                            );
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                populateUIFromFacebookData();
            }
        }

        private void dataListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataListBox.SelectedItems.Count == 1)
            {
                dataPanel.Controls.Clear();
                if (dataListBox.SelectedItem is Post selectedPost)
                {
                    m_FacebookFacade.MakePostPanel(ref dataPanel, selectedPost);
                }
                else if (dataListBox.SelectedItem is Album selectedAlbum)
                {
                    m_FacebookFacade.MakeAlbumPanel(ref dataPanel, selectedAlbum, pictureBoxProfile);
                }
                else if (dataListBox.SelectedItem is Group selectedGroup)
                {
                    m_FacebookFacade.MakeGroupsPanel(ref dataPanel, selectedGroup, pictureBoxProfile);
                }
                else if (dataListBox.SelectedItem is Page selectedPage)
                {
                    m_FacebookFacade.MakePagePanel(ref dataPanel, selectedPage, pictureBoxProfile);
                }
                else if (dataListBox.SelectedItem is Event selectedEvent)
                {
                    m_FacebookFacade.MakeEventPanel(ref dataPanel, selectedEvent);
                }
                else if (dataListBox.SelectedItem is User selectedUser)
                {
                    m_FacebookFacade.MakeFriendsPanel(ref dataPanel, selectedUser, pictureBoxProfile);
                }
            }
        }

        public void populateUIFromFacebookData()
        {
            this.Text = $"{m_LoginResult.LoggedInUser.Name} Facebook App";
            buttonLogin.Text = $"Logged in as {m_LoginResult.LoggedInUser.Name}";
            buttonLogin.BackColor = Color.LightGreen;
            pictureBoxProfile.ImageLocation = m_LoginResult.LoggedInUser.PictureNormalURL;
            labelBirthday.Text = $"Birthday:  {m_LoginResult.LoggedInUser.Birthday}";
            labelEmail.Text = $"Email: {m_LoginResult.LoggedInUser?.Email}";
            for (int i = 0; i < Enum.GetValues(typeof(EWishlistCategories)).Length; i++)
            {
                EWishlistCategories category = (EWishlistCategories)i;
                populateCheckBoxListOfWishlist((EWishlistCategories)i);
            }

            buttonsAfterLogin();
            r_WorkoutManager.FetchWorkoutData(r_WorkoutTable);
        }

        private void addPostButton_Click(object sender, EventArgs e)
        {
            m_FacebookFacade.PostStatus(textBoxStatus.Text, textBoxStatus);
        }

        private void addPictureButton_Click(object sender, EventArgs e)
        {
            string photoPath = m_FacebookFacade.SelectPhotoFile();

            if (string.IsNullOrEmpty(photoPath))
            {
                MessageBox.Show("No photo selected. Please select a photo to post.");
                return;
            }

            m_FacebookFacade.PostPhoto(photoPath);
        }

        private void textBoxStatus_TextChanged(object sender, EventArgs e)
        {
            buttonAddPost.Enabled = !string.IsNullOrWhiteSpace(textBoxStatus.Text);
        }

        private void buttonVideo_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedFilePath = m_FacebookFacade.SelectVideoFile();

                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("No video selected. Please select a video to upload.");
                    return;
                }

                m_FacebookFacade.PostVideo(selectedFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBoxProfile_Click(object sender, EventArgs e)
        {
            using (OpenProfilePicture optionsForm = new OpenProfilePicture())
            {
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    switch (optionsForm.SelectedOption)
                    {
                        case EProfileOption.ShowProfile:
                            ShowProfile();
                            break;

                        case EProfileOption.ChangeProfile:
                            addPictureButton_Click(sender, e);
                            break;
                    }
                }
            }
        }

        private void ShowProfile()
        {
            try
            {
                string profilePictureUrl = m_LoginResult.LoggedInUser.PictureLargeURL;

                PictureBox profilePictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(k_ShowProfileSize, k_ShowProfileSize),
                    Location = new Point(0, 0),
                    BorderStyle = BorderStyle.FixedSingle
                };

                profilePictureBox.Load(profilePictureUrl);

                Form profileForm = new Form
                {
                    Text = "Profile Picture",
                    Size = new Size(k_ShowProfileSize, k_ShowProfileSize),
                };

                profileForm.Controls.Add(profilePictureBox);
                profileForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load the profile picture: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (m_FormAppSettings == null)
            {
                m_FormAppSettings = new FormAppSettings(r_AppSettings);
            }

            if (m_FormAppSettings.ShowDialog() == DialogResult.OK)
            {
                r_AppSettings.SaveToFile(k_User);
                DialogResult reLoginDialog = MessageBox.Show(
                    "Permissions have been updated. You need to log in again to apply the changes. Proceed?",
                    "Re-login Required",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (reLoginDialog == DialogResult.Yes)
                {
                    Login();
                }
            }
        }

        private void checkItemInList(CheckedListBox i_CheckedListBox, WishListItem i_CheckedItem)
        {
            if (i_CheckedItem.Checked)
            {
                int indexInList = i_CheckedListBox.Items.IndexOf(i_CheckedItem);

                if (indexInList >= 0)
                {
                    i_CheckedListBox.SetItemChecked(indexInList, true);
                }
            }
        }

        private void textBoxWishlistName_TextChanged(object sender, EventArgs e)
        {
            m_IsTextBoxChanged = !string.IsNullOrWhiteSpace(textBoxName.Text);
            updateAddButtonState();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_IsComboBoxChanged = (comboBoxCategory.SelectedItem is EWishlistCategories selectedCategory);
            updateAddButtonState();
        }

        private void updateAddButtonState()
        {
            buttonAddPhoto.Enabled = m_IsComboBoxChanged && m_IsTextBoxChanged;
            buttonAdd.Enabled = m_IsTextBoxChanged && m_IsComboBoxChanged;
        }

        private void populateCheckBoxListOfWishlist(EWishlistCategories i_Category)
        {
            List<WishListItem> items = r_AppSettings.WishlistFacade.GetItemsByCategory(i_Category.ToString()) ?? new List<WishListItem>();

            foreach (WishListItem item in items)
            {
                switch (i_Category)
                {
                    case EWishlistCategories.Food:
                        checkedListBoxFood.Items.Add(item);
                        checkItemInList(checkedListBoxFood, item);
                        break;
                    case EWishlistCategories.Shopping:
                        checkedListBoxShopping.Items.Add(item);
                        checkItemInList(checkedListBoxShopping, item);
                        break;
                    case EWishlistCategories.Activities:
                        checkedListBoxActivities.Items.Add(item);
                        checkItemInList(checkedListBoxActivities, item);
                        break;
                    case EWishlistCategories.Pets:
                        checkedListBoxPets.Items.Add(item);
                        checkItemInList(checkedListBoxPets, item);
                        break;
                }
            }
        }

        private void buttonAddWithPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                string photoURL = m_FacebookFacade.SelectPhotoFile();
                string itemName = textBoxName.Text.Trim();
                string category = comboBoxCategory.Text;

                if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(itemName))
                {
                    MessageBox.Show("Please provide both a category and item name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                r_WishlistManager.AddWish(category, itemName, photoURL);
                WishListItem newItem = new WishListItem { Text = itemName, PhotoUrl = photoURL };
                r_WishlistManager.UpdateUI(checkedListBoxFood, checkedListBoxPets, checkedListBoxActivities, checkedListBoxShopping, category, newItem);

                textBoxName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAddWithoutPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                string itemName = textBoxName.Text.Trim();
                string category = comboBoxCategory.Text;

                if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(itemName))
                {
                    MessageBox.Show("Please provide both a category and item name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                r_WishlistManager.AddWish(category, itemName, null);
                WishListItem newItem = new WishListItem { Text = itemName, PhotoUrl = null };
                r_WishlistManager.UpdateUI(checkedListBoxFood, checkedListBoxPets, checkedListBoxActivities, checkedListBoxShopping, category, newItem);

                textBoxName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkedListBoxFood_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBox_ItemCheck(checkedListBoxFood,pictureBoxFood, EWishlistCategories.Food);
        }

        private void checkedListBoxShopping_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBox_ItemCheck(checkedListBoxShopping, pictureBoxShopping, EWishlistCategories.Shopping);
        }

        private void checkedListBoxActivities_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBox_ItemCheck(checkedListBoxActivities,pictureBoxActivities, EWishlistCategories.Activities);
        }

        private void checkedListBoxPets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBox_ItemCheck(checkedListBoxPets,pictureBoxPets, EWishlistCategories.Pets);
        }

        private void checkedListBox_ItemCheck(CheckedListBox i_List, PictureBox i_PictureBox, EWishlistCategories i_Category)
        {
            string itemName = i_List.Text;
            WishListItem wishListItemChecked = findWishListItemByName(i_Category,i_PictureBox, itemName);

            if (wishListItemChecked != null)
            {
                if (wishListItemChecked.Checked)
                {
                    wishListItemChecked.Checked = false;
                }
                else
                {
                    wishListItemChecked.Checked = true;
                }
            }
        }


        private void checkedListBoxFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            WishListItem wishListItemOfSelectedItem = r_WishlistManager.FindWishListItemByName(EWishlistCategories.Food.ToString(), checkedListBoxFood.Text);

            if (wishListItemOfSelectedItem != null)
            {
               r_WishlistManager.LoadImageForPictureBoxInList(wishListItemOfSelectedItem, pictureBoxFood);
                buttonDeleteItem.Enabled = true;
            }
        }

        private void checkedListBoxShopping_SelectedIndexChanged(object sender, EventArgs e)
        {
            WishListItem wishListItemOfSelectedItem = r_WishlistManager.FindWishListItemByName(EWishlistCategories.Shopping.ToString(), checkedListBoxShopping.Text);

            if (wishListItemOfSelectedItem != null)
            {
                r_WishlistManager.LoadImageForPictureBoxInList(wishListItemOfSelectedItem, pictureBoxShopping);
                buttonDeleteItem.Enabled = true;
            }
        }

        private void checkedListBoxPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            WishListItem wishListItemOfSelectedItem = r_WishlistManager.FindWishListItemByName(EWishlistCategories.Pets.ToString(), checkedListBoxPets.Text);

            if (wishListItemOfSelectedItem != null)
            {
                r_WishlistManager.LoadImageForPictureBoxInList(wishListItemOfSelectedItem, pictureBoxPets);
                buttonDeleteItem.Enabled = true;
            }
        }

        private void buttonPostWishlist_Click(object sender, EventArgs e)
        {
            try
            {
                string postData = r_WishlistManager.ShareWishlist(checkedListBoxFood, checkedListBoxActivities, checkedListBoxPets, checkedListBoxShopping);
                m_FacebookFacade.PostStatus(postData, textBoxStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while posting the wishlist: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkedListBoxActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            WishListItem wishListItemOfSelectedItem = findWishListItemByName(EWishlistCategories.Activities,pictureBoxActivities, checkedListBoxActivities.Text);

            r_WishlistManager.LoadImageForPictureBoxInList(wishListItemOfSelectedItem, pictureBoxActivities);
            buttonDeleteItem.Enabled = true;
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            bool deleteFood = deleteSelectedItem(checkedListBoxFood, pictureBoxFood, EWishlistCategories.Food);
            bool deleteActivities = deleteSelectedItem(checkedListBoxActivities, pictureBoxActivities, EWishlistCategories.Activities);
            bool deletePets = deleteSelectedItem(checkedListBoxPets, pictureBoxPets, EWishlistCategories.Pets);
            bool deleteShopping = deleteSelectedItem(checkedListBoxShopping, pictureBoxShopping, EWishlistCategories.Shopping);

            if (deleteFood || deletePets || deleteShopping || deleteActivities)
            {
                MessageBox.Show("Item deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool deleteSelectedItem(CheckedListBox i_CheckedListBox, PictureBox i_PictureBox, EWishlistCategories i_Category)
        {
            bool deleted = false;

            if (i_CheckedListBox.SelectedIndex >= 0)
            {
                WishListItem selectedItem = (WishListItem)i_CheckedListBox.Items[i_CheckedListBox.SelectedIndex];
                i_CheckedListBox.Items.RemoveAt(i_CheckedListBox.SelectedIndex);
                i_PictureBox.Image = null;
                r_WishlistManager.RemoveWish(i_Category.ToString(), selectedItem);
                deleted = true;
            }

            return deleted;
        }


        private WishListItem findWishListItemByName(EWishlistCategories i_Category, PictureBox i_PictureBox, string i_ItemName)
        {
            return r_WishlistManager.FindAndHighlightItem(i_Category.ToString(), i_ItemName, i_PictureBox, buttonDeleteItem);
        }


        private void buttonAddWorkout_Click(object sender, EventArgs e)
        {
            AddWorkoutForm addWorkoutForm = new AddWorkoutForm(r_WorkoutTable, r_WorkoutManager);

            addWorkoutForm.ShowDialog();
        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            StatisicsForm statisicsForm = new StatisicsForm(r_WorkoutTable);

            statisicsForm.ShowDialog();
        }
    }
}
