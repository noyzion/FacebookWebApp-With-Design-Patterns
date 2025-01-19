using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public interface IWorkoutBuilder
    {
        void SetDuration(decimal duration);
        void SetCategory(string category);
        void SetDate(DateTime date);
        void SetCalories(decimal calories);
        Workout GetWorkout();
    }
}
