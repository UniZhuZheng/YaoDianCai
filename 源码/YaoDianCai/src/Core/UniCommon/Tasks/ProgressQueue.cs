using System;
using System.Linq;


namespace Uni.Core.Common.Tasks
{
    public class ProgressQueue : WorkerQueue<IProgressItem>
    {
        private readonly bool removeAfterCompleted;


        public ProgressQueue(int workerCount, TimeSpan waitInterval)
            : this(workerCount, waitInterval, false)
        {
        }

        public ProgressQueue(int workerCount, TimeSpan waitInterval, bool removeAfterCompleted)
            : base(workerCount, waitInterval, 0, false)
        {
            this.removeAfterCompleted = removeAfterCompleted;
            Start(x => x.RunJob());
        }

        public override void Add(IProgressItem item)
        {
            if (GetStatus(item.Id) == null)
            {
                base.Add(item);
            }
        }

        public IProgressItem GetStatus(object id)
        {
            IProgressItem item;
            lock (SynchRoot)
            {
                item = GetItems().Where(x => Equals(x.Id, id)).SingleOrDefault();
                if (item != null)
                {
                    if (removeAfterCompleted && item.IsCompleted)
                    {
                        Remove(item);
                    }
                    return item.Clone() as IProgressItem;
                }
            }
            return item;
        }

        public void PostComplete(object id)
        {
            lock (SynchRoot)
            {
                var item = GetItems().Where(x => Equals(x.Id, id)).SingleOrDefault();

                if (item != null)
                {
                    item.IsCompleted = true;

                    if (removeAfterCompleted)
                    {
                        Remove(item);
                    }
                }
            }
        }

        protected override WorkItem<IProgressItem> Selector()
        {
            return Items
                .Where(x => !x.IsProcessed && !x.IsCompleted)
                .OrderBy(x => x.Added)
                .FirstOrDefault();
        }

        protected override void PostComplete(WorkItem<IProgressItem> item)
        {
            item.Completed = DateTime.UtcNow;
            item.IsCompleted = true;
        }

        protected override void ErrorLimit(WorkItem<IProgressItem> item)
        {
            PostComplete(item);
        }

        protected override void Error(WorkItem<IProgressItem> workItem, Exception exception)
        {
            workItem.Item.Error = exception;
            workItem.Item.IsCompleted = true;

            base.Error(workItem, exception);
        }
    }
}
