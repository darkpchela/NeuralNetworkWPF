using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeuralNetwork.CustomUI
{
    public class TreeViewItemWithCommand : TreeViewItem, ICommandSource
    {
        public ICommand Command 
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter 
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public IInputElement CommandTarget { get; }

        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;

        static TreeViewItemWithCommand()
        {
            CommandProperty = DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(TreeViewItemWithCommand)
                );
            CommandParameterProperty = DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(TreeViewItemWithCommand)
                );
        }
    }
}
