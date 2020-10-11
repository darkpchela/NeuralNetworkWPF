using System;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal class PropertyDependency
    {
        internal object Source { get; set; }
        internal string SourcePropName { get; set; }
        internal WeakReference Target { get; set; }
        internal string TargetPropName { get; set; }
        internal Func<object, object> MappingFunc { get; set; }
    }
}