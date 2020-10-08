using System;

namespace NeuralNetwork.Infrastructure.Etc
{
    public enum Source
    {
        Networks,
        Storages
    }

    public delegate void WorkshopSourceChangedEventHandler(object sender, WorkshopSourceChangedEventArgs e);

    public class WorkshopSourceChangedEventArgs : EventArgs
    {
        public Source SourceName { get; }
        public string SourceId { get; }

        public WorkshopSourceChangedEventArgs(Source source, string sourceId)
        {
            SourceName = source;
            SourceId = sourceId;
        }
    }
}