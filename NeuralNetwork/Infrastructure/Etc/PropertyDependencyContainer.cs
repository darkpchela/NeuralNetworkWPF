using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal static class PropertyDependencyContainer
    {
        private static Dictionary<(string, object), PropertyDependency> _dependencies = new Dictionary<(string, object), PropertyDependency>();

        internal static void Regist(string sourcePropName, object source, string targetPropName, object target, Func<object, object> mappingFunc = null)
        {
            var registForm = new PropertyDependency();
            registForm.SourcePropName = sourcePropName;
            registForm.Source = source;
            registForm.Target = target;
            registForm.TargetPropName = targetPropName;
            registForm.MappingFunc = mappingFunc;

            ((INotifyPropertyChanged)registForm.Source).PropertyChanged += UpdateProperty;

            if (!_dependencies.ContainsKey((sourcePropName, source)))
                _dependencies.Add((sourcePropName, source), registForm);
        }

        private static void UpdateProperty(object sender, PropertyChangedEventArgs e)
        {
            if (!_dependencies.ContainsKey((e.PropertyName, sender)))
                return;

            var dependency = _dependencies[(e.PropertyName, sender)];
            var propValue = dependency.Source.GetType().GetProperty(e.PropertyName).GetValue(sender, null);

            if (dependency.MappingFunc != null)
                propValue = dependency.MappingFunc(propValue);

            dependency.Target.GetType().GetProperty(dependency.TargetPropName).SetValue(dependency.Target, propValue);
        }
    }
}