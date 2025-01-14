using System;

namespace BasicFacebookFeatures
{
    public class WorkoutBuilder : IWorkoutBuilder
    {
        private Workout m_Workout;

        public WorkoutBuilder()
        {
            m_Workout = new Workout();
        }

        public void SetDuration(decimal duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("Duration must be greater than 0.");
            }

            m_Workout.Duration = duration;
        }

        public void SetCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Category cannot be null or empty.");
            }

            m_Workout.Category = category;
        }

        public void SetDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the future.");
            }

            m_Workout.Date = date;
        }

        public void SetCalories(decimal calories)
        {
            if (calories <= 0)
            {
                throw new ArgumentException("Calories must be greater than 0.");
            }

            m_Workout.Calories = calories;
        }

        public Workout GetWorkout()
        {
            Workout result = m_Workout;
            m_Workout = new Workout(); 
            return result;
        }
    }
}
