using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class FacebookManager
    {
        private readonly FacebookWrapper.LoginResult r_LoginResult;

        public FacebookManager(FacebookWrapper.LoginResult i_LoginResult)
        {
            r_LoginResult = i_LoginResult;
        }

        public FacebookObjectCollection<Group> FetchGroups()
        {
            return r_LoginResult.LoggedInUser.Groups;
        }

        public FacebookObjectCollection<Album> FetchAlbums()
        {
            return r_LoginResult.LoggedInUser.Albums;
        }

        public FacebookObjectCollection<User> FetchFriends()
        {
            return r_LoginResult.LoggedInUser.Friends;
        }

        public FacebookObjectCollection<Post> FetchPosts()
        {
            return r_LoginResult.LoggedInUser.Posts;
        }

        public FacebookObjectCollection<Page> FetchLikedPages()
        {
            return r_LoginResult.LoggedInUser.LikedPages;
        }

        public FacebookObjectCollection<Event> FetchEvents()
        {
            return r_LoginResult.LoggedInUser.Events;
        }

        public Status PostStatus(string message)
        {
            return r_LoginResult.LoggedInUser.PostStatus(message);
        }

        public Post PostPhoto(string filePath)
        {
            return r_LoginResult.LoggedInUser.PostPhoto(filePath);
        }

        public void PostVideo(string filePath)
        {
            r_LoginResult.LoggedInUser.PostPhoto(filePath);
        }
    }
}
