using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class WorkoutBuilder
    {
        private Workout m_Workout;

        public WorkoutBuilder()
        {
            m_Workout = new Workout();
        }
        public void SetDuration(decimal duration)
        {
            m_Workout.Duration = duration;
        }
        public void SetCategory(string category)
        {
            m_Workout.Category = category;
        }

        public void SetDate(DateTime date)
        {
            m_Workout.Date = date;
        }
        public void SetCalories(decimal calories)
        {
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
