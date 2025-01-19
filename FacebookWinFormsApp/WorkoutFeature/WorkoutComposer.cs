using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class WorkoutComposer
    {
        private readonly IWorkoutBuilder r_Builder;

        public WorkoutComposer()
        {
             r_Builder = new WorkoutBuilder();
        }
        public Workout ComposeWorkout(decimal duration, string category, DateTime date, decimal calories)
        {
            r_Builder.SetDuration(duration);
            r_Builder.SetCategory(category);
            r_Builder.SetDate(date);
            r_Builder.SetCalories(calories);
            return r_Builder.GetWorkout();
        }
    }
}
