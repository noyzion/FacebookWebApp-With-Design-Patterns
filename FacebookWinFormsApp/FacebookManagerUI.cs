using CefSharp.DevTools.IndexedDB;
using FacebookWrapper.ObjectModel;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FacebookManagerUI
    {
        private readonly FacebookManager r_FacebookLogic;
        private const int k_pictureSize = 150;
        private const int k_AlbumFormWidth = 450;
        private const int k_AlbumFormHeight = 600;
        private const int k_PictureInAlbumSize = 200;
        private readonly object fetchLock = new object();  // Lock object for thread synchronization

        public FacebookManagerUI(FacebookManager i_FacebookLogic)
        {
            r_FacebookLogic = i_FacebookLogic;
        }

        public void FetchGroups(ListBox i_DataListBox)
        {
            Thread fetchGroupsThread = new Thread(() =>
            {
                try
                {
                    lock (fetchLock)
                    {
                        var groups = r_FacebookLogic.FetchGroups();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "Name";

                            foreach (var group in groups)
                            {
                                i_DataListBox.Items.Add(group);
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No groups to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching groups: {ex.Message}");
                    }));
                }
            });
            fetchGroupsThread.Start();
        }

        public void FetchAlbums(ListBox i_DataListBox)
        {
            Thread fetchAlbumsThread = new Thread(() =>
            {
                try
                {
                    lock (fetchLock)
                    {
                        var albums = r_FacebookLogic.FetchAlbums();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "Name";

                            foreach (var album in albums)
                            {
                                i_DataListBox.Items.Add(album);
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No albums to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching albums: {ex.Message}");
                    }));
                }
            });
            fetchAlbumsThread.Start();
        }

        public void FetchFriends(ListBox i_DataListBox)
        {
            Thread fetchFriendsThread = new Thread(() =>
            {
                try
                {
                    lock (fetchLock)
                    {
                        var friends = r_FacebookLogic.FetchFriends();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "Name";

                            foreach (var friend in friends)
                            {
                                i_DataListBox.Items.Add(friend);
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No friends to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching friends: {ex.Message}");
                    }));
                }
            });
            fetchFriendsThread.Start();
        }

        public void FetchPosts(ListBox i_DataListBox)
        {
            Thread fetchPostsThread = new Thread(() =>
            {
                try
                {
                    lock (fetchLock)
                    {
                        var posts = r_FacebookLogic.FetchPosts();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "UpdateTime";

                            foreach (var post in posts)
                            {
                                if (post.Message != null || post.PictureURL != null)
                                {
                                    i_DataListBox.Items.Add(post);
                                }
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No posts to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching posts: {ex.Message}");
                    }));
                }
            });
            fetchPostsThread.Start();
        }
        public void FetchLikedPages(ListBox i_DataListBox)
        {
            Thread fetchLikedPagesThread = new Thread(() =>
            {
                try
                {
                    // Locking to ensure thread safety
                    lock (fetchLock)
                    {
                        var likedPages = r_FacebookLogic.FetchLikedPages();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "Name";

                            foreach (var page in likedPages)
                            {
                                i_DataListBox.Items.Add(page);
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No liked pages to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching liked pages: {ex.Message}");
                    }));
                }
            });
            fetchLikedPagesThread.Start();
        }

        public void FetchEvents(ListBox i_DataListBox)
        {
            Thread fetchEventsThread = new Thread(() =>
            {
                try
                {
                    // Locking to ensure thread safety
                    lock (fetchLock)
                    {
                        var events = r_FacebookLogic.FetchEvents();

                        i_DataListBox.Invoke(new Action(() =>
                        {
                            i_DataListBox.DataSource = null;
                            i_DataListBox.Items.Clear();
                            i_DataListBox.DisplayMember = "Name";

                            foreach (var fbEvent in events)
                            {
                                i_DataListBox.Items.Add(fbEvent);
                            }

                            if (i_DataListBox.Items.Count == 0)
                            {
                                MessageBox.Show("No events to retrieve :(");
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    i_DataListBox.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error fetching events: {ex.Message}");
                    }));
                }
            });
            fetchEventsThread.Start();
        }


        public void PostStatus(string i_Message, TextBox i_StatusTextBox)
        {
            try
            {
                Status postedStatus = r_FacebookLogic.PostStatus(i_Message);
                MessageBox.Show($"Status posted! ID: {postedStatus.Id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                i_StatusTextBox.Clear();
            }
        }

        public void PostPhoto(string i_FilePath)
        {
            try
            {
                r_FacebookLogic.PostPhoto(i_FilePath);
                MessageBox.Show("Photo posted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error posting photo: {ex.Message}");
            }
        }

        public string SelectPhotoFile()
        {
            string selectedFilePath = null;

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Title = "Select a Picture to Upload";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFilePath = openFileDialog.FileName;
                    }
                }

                return selectedFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string SelectVideoFile()
        {
            string selectedFilePath = null;

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Video Files|*.mp4;*.avi;*.mov;*.wmv;*.flv";
                    openFileDialog.Title = "Select a Video File to Upload";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFilePath = openFileDialog.FileName;
                    }
                }

                return selectedFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void PostVideo(string i_FilePath)
        {
            try
            {
                r_FacebookLogic.PostVideo(i_FilePath);
                MessageBox.Show("Video posted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error posting video: {ex.Message}");
            }
        }

        // Panel Creation Methods
        public void MakeFriendsPanel(ref TableLayoutPanel io_DataPanel, User i_User, PictureBox i_PictureBoxProfile)
        {
            try
            {
                Label nameLabel = new Label { Text = $"Name: {i_User.Name}", AutoSize = true };
                Label birthdayLabel = new Label { Text = $"Birthday: {i_User.Birthday}", AutoSize = true };

                io_DataPanel.Controls.Add(nameLabel);
                io_DataPanel.Controls.Add(birthdayLabel);
                PictureBox userPictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(k_pictureSize, k_pictureSize)
                };

                if (!string.IsNullOrEmpty(i_User.PictureNormalURL))
                {
                    userPictureBox.ImageLocation = i_User.PictureNormalURL;
                }
                else
                {
                    userPictureBox.Image = i_PictureBoxProfile.ErrorImage;
                }

                io_DataPanel.Controls.Add(userPictureBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying friend details: {ex.Message}");
            }
        }

        public void MakeGroupsPanel(ref TableLayoutPanel io_DataPanel, Group i_Group, PictureBox i_PictureBoxProfile)
        {
            try
            {
                if (i_Group != null)
                {
                    Label nameLabel = new Label { Text = $"Group Name: {i_Group.Name}", AutoSize = true };
                    Label membersLabel = new Label { Text = $"Members: {i_Group.Members.Count}", AutoSize = true };
                    Label privacyLabel = new Label { Text = $"Privacy: {i_Group.Privacy}", AutoSize = true };

                    io_DataPanel.Controls.Add(nameLabel);
                    io_DataPanel.Controls.Add(membersLabel);
                    io_DataPanel.Controls.Add(privacyLabel);
                    PictureBox groupPicture = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(k_pictureSize, k_pictureSize)
                    };

                    if (!string.IsNullOrEmpty(i_Group.PictureNormalURL))
                    {
                        groupPicture.ImageLocation = i_Group.PictureNormalURL;
                    }
                    else
                    {
                        groupPicture.Image = i_PictureBoxProfile.ErrorImage;
                    }

                    io_DataPanel.Controls.Add(groupPicture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying group details: {ex.Message}");
            }
        }

        public void MakePagePanel(ref TableLayoutPanel io_DataPanel, Page i_Page, PictureBox i_PictureBoxProfile)
        {
            try
            {
                if (i_Page != null)
                {
                    Label nameLabel = new Label { Text = $"Page Name: {i_Page.Name}", AutoSize = true };
                    Label categoryLabel = new Label { Text = $"Category: {i_Page.Category}", AutoSize = true };
                    Label likesLabel = new Label { Text = $"Likes: {i_Page.LikesCount}", AutoSize = true };

                    io_DataPanel.Controls.Add(nameLabel);
                    io_DataPanel.Controls.Add(categoryLabel);
                    io_DataPanel.Controls.Add(likesLabel);
                    PictureBox pagePicture = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(k_pictureSize, k_pictureSize)
                    };

                    if (!string.IsNullOrEmpty(i_Page.PictureURL))
                    {
                        pagePicture.ImageLocation = i_Page.PictureURL;
                    }
                    else
                    {
                        pagePicture.Image = i_PictureBoxProfile.ErrorImage;
                    }

                    io_DataPanel.Controls.Add(pagePicture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying page details: {ex.Message}");
            }
        }

        public void MakeEventPanel(ref TableLayoutPanel io_DataPanel, Event i_FbEvent)
        {
            try
            {
                if (i_FbEvent != null)
                {
                    Label nameLabel = new Label { Text = $"Event Name: {i_FbEvent.Name}", AutoSize = true };
                    Label descriptionLabel = new Label { Text = $"Description: {i_FbEvent.Description}", AutoSize = true };
                    Label startTimeLabel = new Label { Text = $"Start Time: {i_FbEvent.StartTime}", AutoSize = true };
                    Label endTimeLabel = new Label { Text = $"End Time: {i_FbEvent.EndTime}", AutoSize = true };
                    Label locationLabel = new Label { Text = $"Location: {i_FbEvent.Location}", AutoSize = true };

                    io_DataPanel.Controls.Add(nameLabel);
                    io_DataPanel.Controls.Add(descriptionLabel);
                    io_DataPanel.Controls.Add(startTimeLabel);
                    io_DataPanel.Controls.Add(endTimeLabel);
                    io_DataPanel.Controls.Add(locationLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying event details: {ex.Message}");
            }
        }

        public void MakeAlbumPanel(ref TableLayoutPanel io_DataPanel, Album i_Album, PictureBox i_PictureBoxProfile)
        {
            try
            {
                Label nameLabel = new Label { Text = $"Album Name: {i_Album.Name}", AutoSize = true };
                Label countLabel = new Label { Text = $"Photos: {i_Album.Photos.Count}", AutoSize = true };

                io_DataPanel.Controls.Add(nameLabel);
                io_DataPanel.Controls.Add(countLabel);

                PictureBox albumPicture = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(k_pictureSize, k_pictureSize)
                };

                if (!string.IsNullOrEmpty(i_Album.PictureAlbumURL))
                {
                    albumPicture.ImageLocation = i_Album.PictureAlbumURL;
                }
                else
                {
                    albumPicture.Image = i_PictureBoxProfile.ErrorImage;
                }

                io_DataPanel.Controls.Add(albumPicture);
                Button openAlbumButton = new Button
                {
                    Text = "Open Album",
                    AutoSize = true
                };

                io_DataPanel.Controls.Add(openAlbumButton);
                openAlbumButton.Click += (sender, e) => OpenAlbumPhotos(i_Album, i_PictureBoxProfile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying album details: {ex.Message}");
            }
        }

        public void MakePostPanel(ref TableLayoutPanel io_DataPanel, Post i_Post)
        {
            try
            {
                if (i_Post != null)
                {
                    Label messageLabel = new Label { Text = $"Message: {i_Post.Message}", AutoSize = true };
                    // Label likesLabel = new Label { Text = $"Likes: {post.LikedBy.Count}", AutoSize = true }; // no access
                    // Label commentsLabel = new Label { Text = $"Comments: {post.Comments.Count}", AutoSize = true };

                    io_DataPanel.Controls.Add(messageLabel);
                    // dataPanel.Controls.Add(likesLabel); // no access
                    // dataPanel.Controls.Add(commentsLabel);

                    PictureBox thisPostPicture = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(k_pictureSize, k_pictureSize)
                    };

                    if (!string.IsNullOrEmpty(i_Post.PictureURL))
                    {
                        thisPostPicture.ImageLocation = i_Post.PictureURL;
                    }

                    io_DataPanel.Controls.Add(thisPostPicture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying post details: {ex.Message}");
            }
        }

        private void OpenAlbumPhotos(Album i_Album, PictureBox i_PictureBoxProfile)
        {
            Form albumForm = new Form
            {
                Text = i_Album.Name,
                Width = k_AlbumFormWidth,
                Height = k_AlbumFormHeight
            };

            albumForm.StartPosition = FormStartPosition.CenterScreen;
            FlowLayoutPanel photoPanel = new FlowLayoutPanel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            foreach (Photo photo in i_Album.Photos)
            {
                PictureBox photoPicture = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(k_PictureInAlbumSize, k_PictureInAlbumSize)
                };

                if (!string.IsNullOrEmpty(photo.PictureNormalURL))
                {
                    photoPicture.ImageLocation = photo.PictureNormalURL;
                }
                else
                {
                    photoPicture.Image = i_PictureBoxProfile.ErrorImage;
                }

                photoPanel.Controls.Add(photoPicture);
            }

            albumForm.Controls.Add(photoPanel);
            albumForm.ShowDialog();
        }
    }
}
