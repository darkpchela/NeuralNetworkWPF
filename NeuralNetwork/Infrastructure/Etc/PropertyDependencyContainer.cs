using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace NeuralNetwork.Infrastructure.Etc
{
    internal static class PropertyDependencyContainer
    {
        private static int _counter = 0;
        public static Dictionary<(string, object), PropertyDependency> _dependencies = new Dictionary<(string, object), PropertyDependency>();
        internal static void Regist(string sourcePropName, object source, string targetPropName, object target, Func<object, object> mappingFunc = null)
        {
            var registForm = new PropertyDependency();
            registForm.SourcePropName = sourcePropName;
            registForm.Source = source;
            registForm.Target = new WeakReference(target, false);
            registForm.TargetPropName = targetPropName;
            registForm.MappingFunc = mappingFunc;

            ((INotifyPropertyChanged)registForm.Source).PropertyChanged += UpdateProperty;

            if (!_dependencies.ContainsKey((sourcePropName, source)))
                _dependencies.Add((sourcePropName, source), registForm);

            _counter++;

            if (_counter == 5)
            {
                CollectGarbage();
                _counter = 0;
            }
        }

        private static void UpdateProperty(object sender, PropertyChangedEventArgs e)
        {
            if (!_dependencies.ContainsKey((e.PropertyName, sender)))
                return;

            var dependency = _dependencies[(e.PropertyName, sender)];
            var propValue = dependency.Source.GetType().GetProperty(e.PropertyName).GetValue(sender, null);

            if (dependency.MappingFunc != null)
                propValue = dependency.MappingFunc(propValue);

            dependency.Target.Target.GetType().GetProperty(dependency.TargetPropName).SetValue(dependency.Target.Target, propValue);
        }

        private static void CollectGarbage()
        {
            var deadKeys = new List<(string, object)>();

            foreach (var item in _dependencies)
            {
                if (!item.Value.Target.IsAlive)
                   deadKeys.Add(item.Key);
            }

            foreach (var key in deadKeys)
            {
                _dependencies.Remove(key);
            }
        }

    }
}