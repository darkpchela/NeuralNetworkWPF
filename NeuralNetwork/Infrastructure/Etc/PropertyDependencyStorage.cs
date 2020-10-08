using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal class PropertyDependencyStorage
    {
        private Dictionary<(string, object), PropertyDependency> keyValuePairs = new Dictionary<(string, object), PropertyDependency>();

        internal void Regist(string sourcePropName, object source, string targetPropName, object target, IValueConverter converter = null)
        {
            var registForm = new PropertyDependency();
            registForm.SourcePropName = sourcePropName;
            registForm.Source = source;
            registForm.Target = target;
            registForm.TargetPropName = targetPropName;
            registForm.Converter = converter;

            keyValuePairs.Add((sourcePropName, source), registForm);
        }

        internal PropertyDependency GetPropDependency(string sourcePropName, object sourceType)
        {
            return keyValuePairs[(sourcePropName, sourceType)];
        }

        internal bool ContainsDependency(string sourcePropName, object sourceType)
        {
            return keyValuePairs.ContainsKey((sourcePropName, sourceType));
        }
    }
}