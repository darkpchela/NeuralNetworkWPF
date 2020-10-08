using System;
using System.Windows.Data;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal class PropertyDependency
    {
        internal object Source { get; set; }
        internal string SourcePropName { get; set; }
        internal object Target { get; set; }
        internal string TargetPropName { get; set; }
        internal IValueConverter Converter { get; set; }
    }
}