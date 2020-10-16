using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Etc
{
    public class ObservableTask :IDisposable
    {

        public event ObservableTaskEventHandler TaskCompleted;
        private void OnTaskCompleted()
        {
            TaskCompleted?.Invoke(this, new ObservableTaskEventArgs(_task));
        }

        public event ObservableTaskEventHandler TaskFaulted;
        private void OnTaskFaulted()
        {
            TaskFaulted?.Invoke(this, new ObservableTaskEventArgs(_task));
        }

        public event ObservableTaskEventHandler TaskCanceled;
        private void OnTaskCanceled()
        {
            TaskCanceled?.Invoke(this, new ObservableTaskEventArgs(_task));
        }

        public event ObservableTaskEventHandler TaskRedied;
        private void OnTaskRedied()
        {
            TaskRedied?.Invoke(this, new ObservableTaskEventArgs(_task));
        }

        public event ObservableTaskEventHandler TaskStarted;
        private void OnTaskStarted()
        {
            TaskStarted?.Invoke(this, new ObservableTaskEventArgs(_task));
        }

        private Task _task;
        public ObservableTask(Task task)
        {
            _task = task;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (_task.IsCompleted)
                    {
                        OnTaskCompleted();
                        break;
                    }

                    if (_task.IsFaulted)
                    {
                        OnTaskFaulted();
                        break;
                    }

                    if (_task.IsCanceled)
                    {
                        OnTaskCanceled();
                        break;
                    }
                }
            });

            OnTaskRedied();

            _task.Start();

            OnTaskStarted();
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _task.Dispose();
            }

            _disposed = true;
        }

        ~ObservableTask() => Dispose(false);
    }
}