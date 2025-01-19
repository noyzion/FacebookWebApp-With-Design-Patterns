using FacebookWrapper.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FacebookFacade
    {
        private readonly FacebookManager r_FacebookLogic;
        private readonly FacebookManagerUI r_FacebookUI;

        public FacebookFacade(FacebookWrapper.LoginResult i_LoginResult)
        {
            r_FacebookLogic = new FacebookManager(i_LoginResult);
            r_FacebookUI = new FacebookManagerUI(r_FacebookLogic);
        }

        public void FetchGroups(ListBox dataListBox)
        {
            r_FacebookUI.FetchGroups(dataListBox);
        }

        public void FetchAlbums(ListBox dataListBox)
        {
            r_FacebookUI.FetchAlbums(dataListBox);
        }

        public void FetchFriends(ListBox dataListBox)
        {
            r_FacebookUI.FetchFriends(dataListBox);
        }

        public void FetchPosts(ListBox dataListBox)
        {
            r_FacebookUI.FetchPosts(dataListBox);
        }

        public void FetchLikedPages(ListBox dataListBox)
        {
            r_FacebookUI.FetchLikedPages(dataListBox);
        }

        public void FetchEvents(ListBox dataListBox)
        {
            r_FacebookUI.FetchEvents(dataListBox);
        }

        public void PostStatus(string message, TextBox statusTextBox)
        {
            r_FacebookUI.PostStatus(message, statusTextBox);
        }

        public void PostPhoto(string filePath)
        {
            r_FacebookUI.PostPhoto(filePath);
        }

        public void PostVideo(string filePath)
        {
            r_FacebookUI.PostVideo(filePath);
        }

        public string SelectPhotoFile()
        {
            return r_FacebookUI.SelectPhotoFile();
        }

        public string SelectVideoFile()
        {
            return r_FacebookUI.SelectVideoFile();
        }

        // Panel Creation Methods
        public void MakeFriendsPanel(ref TableLayoutPanel io_DataPanel, User i_User, PictureBox i_PictureBoxProfile)
        {
            r_FacebookUI.MakeFriendsPanel(ref io_DataPanel, i_User, i_PictureBoxProfile);
        }

        public void MakeGroupsPanel(ref TableLayoutPanel io_DataPanel, Group i_Group, PictureBox i_PictureBoxProfile)
        {
            r_FacebookUI.MakeGroupsPanel(ref io_DataPanel, i_Group, i_PictureBoxProfile);
        }

        public void MakePagePanel(ref TableLayoutPanel io_DataPanel, Page i_Page, PictureBox i_PictureBoxProfile)
        {
            r_FacebookUI.MakePagePanel(ref io_DataPanel, i_Page, i_PictureBoxProfile);
        }

        public void MakeEventPanel(ref TableLayoutPanel io_DataPanel, Event i_FbEvent)
        {
            r_FacebookUI.MakeEventPanel(ref io_DataPanel, i_FbEvent);
        }

        public void MakeAlbumPanel(ref TableLayoutPanel io_DataPanel,ListBox i_DataListBox, Album i_Album, PictureBox i_PictureBoxProfile)
        {
            r_FacebookUI.MakeAlbumPanel( ref io_DataPanel, i_Album,i_DataListBox,i_PictureBoxProfile);
        }

        public void MakePostPanel(ref TableLayoutPanel io_DataPanel, Post i_Post)
        {
            r_FacebookUI.MakePostPanel(ref io_DataPanel, i_Post);
        }
    }
}
