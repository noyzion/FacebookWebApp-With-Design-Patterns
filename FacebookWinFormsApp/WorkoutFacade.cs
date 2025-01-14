using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class WorkoutFacade
    {
        public  WorkoutManager WorkoutManager { get; set; }
        private readonly DataGridView r_WorkoutTable;
        private readonly WorkoutManagerUI r_WorkoutManagerUI;

        public WorkoutFacade()
        {
            WorkoutManager = new WorkoutManager();
            r_WorkoutManagerUI = new WorkoutManagerUI();
            r_WorkoutTable = WorkoutManager.InitializeWorkoutTable();
        }


        public DataGridView GetWorkoutTable()
        {
            return r_WorkoutTable;
        }

        public void ResetWorkoutTable()
        {
            r_WorkoutTable.Rows.Clear();
        }
        public void FetchWorkoutData()
        {
            WorkoutManager.FetchWorkoutData(r_WorkoutTable);
        }

        public void AddWorkout(Workout workout)
        {
            if (WorkoutManager.Workouts == null)
            {
                WorkoutManager.Workouts = new List<Workout>();
            }
            WorkoutManager.Workouts.Add(workout);
            FetchWorkoutData();
        }

        public void RemoveWorkout(Workout workout)
        {
            if (WorkoutManager.Workouts != null)
            {
                WorkoutManager.Workouts.Remove(workout);
                FetchWorkoutData();
            }
        }

        public List<Workout> GetWorkouts()
        {
            return WorkoutManager.Workouts;
        }
        public string PreparePostWorkouts()
        {
            r_WorkoutManagerUI.DisplayWorkoutsPopup(WorkoutManager.Workouts);
            return WorkoutManager.GenerateWorkoutsSummary();
        }

    }
}
