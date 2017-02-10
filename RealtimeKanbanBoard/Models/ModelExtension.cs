#region

using System.Linq;

#endregion

namespace RealtimeKanbanBoard.Models
{
    public static class ModelExtension
    {
        /// <summary>
        ///     Inserts the task.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="position">The position.</param>
        /// <param name="task">The task.</param>
        public static void InsertTask(this List list, int position, Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Insert(position, task);
            list.Tasks = tasks.ToArray();
        }

        /// <summary>
        ///     Removes the task.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="task">The task.</param>
        public static void RemoveTask(this List list, Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Remove(task);
            list.Tasks = tasks.ToArray();
        }
    }
}