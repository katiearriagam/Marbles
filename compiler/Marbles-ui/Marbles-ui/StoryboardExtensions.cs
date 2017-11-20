using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace Marbles
{
    /// <summary>
    /// Class with extension utilities for <see cref="Storyboard"/> objects.
    /// </summary>
    public static class StoryboardExtensions
    {
        /// <summary>
        /// Runs animations asynchronically that can be awaited.
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public static Task BeginAsync(this Storyboard storyboard)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            if (storyboard == null)
                tcs.SetException(new ArgumentNullException());
            else
            {
                EventHandler<object> onComplete = null;
                onComplete = (s, e) => {
                    storyboard.Completed -= onComplete;
                    tcs.SetResult(true);
                };
                storyboard.Completed += onComplete;
                storyboard.Begin();
            }
            return tcs.Task;
        }
    }
}
