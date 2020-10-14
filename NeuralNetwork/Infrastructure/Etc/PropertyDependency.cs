using System;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal class PropertyDependency
    {
        internal WeakReference SourceRef { get; set; }
        internal string SourcePropName { get; set; }
        internal WeakReference TargetRef { get; set; }
        internal string TargetPropName { get; set; }
        internal Func<object, object> MappingFunc { get; set; }
    }
}