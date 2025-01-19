using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class WorkoutManagerUI
    {
        public void DisplayWorkoutsPopup(List<Workout> i_Workouts)
        {
            Form popupForm = new Form
            {
                Text = "🏋️ My Workouts 🏋️",
                Size = new Size(900, 600),
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.WhiteSmoke
            };

            FlowLayoutPanel mainPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20)
            };

            Label headerLabel = new Label
            {
                Text = "Here's a summary of your workouts:",
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Margin = new Padding(10)
            };
            mainPanel.Controls.Add(headerLabel);

            if (i_Workouts.Count > 0)
            {
                foreach (Workout workout in i_Workouts)
                {
                    Panel workoutPanel = createWorkoutPanel(workout);
                    mainPanel.Controls.Add(workoutPanel);
                }
            }
            else
            {
                Label emptyMessage = createEmptyMessageLabel();
                mainPanel.Controls.Add(emptyMessage);
            }

            popupForm.Controls.Add(mainPanel);
            popupForm.ShowDialog();
        }
        private Panel createWorkoutPanel(Workout i_Workout)
        {
            Panel workoutPanel = new Panel
            {
                Size = new Size(800, 120),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Lavender,
                Margin = new Padding(10),
                Padding = new Padding(10)
            };

            Label workoutDetailsLabel = createWorkoutDetailsLabel(i_Workout);
            PictureBox workoutIcon = createWorkoutIcon();

            workoutPanel.Controls.Add(workoutIcon);
            workoutPanel.Controls.Add(workoutDetailsLabel);

            return workoutPanel;
        }
        private Label createWorkoutDetailsLabel(Workout i_Workout)
        {
            return new Label
            {
                Text = $"🏋️ Category: {i_Workout.Category}\n" +
                       $"⏱ Duration: {i_Workout.Duration} mins\n" +
                       $"🔥 Calories Burned: {i_Workout.Calories} cal\n" +
                       $"📅 Date: {i_Workout.Date:yyyy-MM-dd}",
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.DarkSlateGray,
                Margin = new Padding(10),
                Location = new Point(80, 10)
            };
        }
        private PictureBox createWorkoutIcon()
        {
            PictureBox workoutIcon = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(10, 30),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            return workoutIcon;
        }
        private Label createEmptyMessageLabel()
        {
            return new Label
            {
                Text = "No workouts available. Start working out to see your progress here! 💪",
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Italic),
                ForeColor = Color.Gray,
                Margin = new Padding(20)
            };
        }
    }
}
