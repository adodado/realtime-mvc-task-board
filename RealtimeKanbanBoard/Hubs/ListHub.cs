#region

using Microsoft.AspNet.SignalR;
using RealtimeKanbanBoard.QuerysCommands.BoardList;
using RealtimeKanbanBoard.QuerysCommands.MoveList;
using RealtimeKanbanBoard.QuerysCommands.MoveTask;
using ShortBus;
using StructureMap;

#endregion

namespace RealtimeKanbanBoard.Hubs
{
    public class ListHub : Hub
    {
        /// <summary>
        ///     The _mediator
        /// </summary>
        private readonly IMediator _mediator = ObjectFactory.GetInstance<IMediator>();

        /// <summary>
        ///     Gets all lists.
        /// </summary>
        /// <param name="boardId">The board identifier.</param>
        public void getAllLists(int boardId)
        {
            var response = _mediator.Request(new GetBoardListsQuery {Id = boardId});
            Clients.Caller.allLists(response.Data);
        }

        /// <summary>
        ///     Moveds the list.
        /// </summary>
        /// <param name="boardId">The board identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="targetIndex">Index of the target.</param>
        public void movedList(int boardId, int listId, int targetIndex)
        {
            var response =
                _mediator.Send(new MoveListCommand {Id = boardId, ListId = listId, TargetIndex = targetIndex});
            if (!response.HasException())
            {
                Clients.Others.syncListMove(listId, targetIndex);
            }
        }

        /// <summary>
        ///     Moveds the task.
        /// </summary>
        /// <param name="boardId">The board identifier.</param>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="sourceListId">The source list identifier.</param>
        /// <param name="destinationListId">The destination list identifier.</param>
        /// <param name="targetIndex">Index of the target.</param>
        public void movedTask(int boardId, int taskId, int sourceListId, int destinationListId, int targetIndex)
        {
            var response =
                _mediator.Send(new MoveTaskCommand
                {
                    BoardId = boardId,
                    DestinationListId = destinationListId,
                    SourceListId = sourceListId,
                    TargetIndex = targetIndex,
                    TaskId = taskId
                });
            if (!response.HasException())
            {
                Clients.Others.syncTaskMove(taskId, sourceListId, destinationListId, targetIndex);
            }
        }
    }
}