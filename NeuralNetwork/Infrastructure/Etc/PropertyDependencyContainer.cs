using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal static class PropertyDependencyContainer
    {
        private static Dictionary<(string, object), PropertyDependency> _dependencies = new Dictionary<(string, object), PropertyDependency>();

        internal static void Regist(string sourcePropName, object source, string targetPropName, object target, IValueConverter converter = null)
        {
            var registForm = new PropertyDependency();
            registForm.SourcePropName = sourcePropName;
            registForm.Source = source;
            registForm.Target = target;
            registForm.TargetPropName = targetPropName;
            registForm.Converter = converter;

            ((INotifyPropertyChanged)registForm.Source).PropertyChanged += UpdateProperty;

            _dependencies.Add((sourcePropName, source), registForm);
        }
        private static void UpdateProperty(object sender, PropertyChangedEventArgs e)
        {
            if (!_dependencies.ContainsKey((e.PropertyName, sender)))
                return;

            var dependency = _dependencies[(e.PropertyName, sender)];
            var propValue = dependency.Source.GetType().GetProperty(e.PropertyName).GetValue(sender, null);

            if (dependency.Converter != null)
                propValue = dependency.Converter.Convert(propValue, null, null, null);

            dependency.Target.GetType().GetProperty(dependency.TargetPropName).SetValue(dependency.Target, propValue);
        }
    }
}