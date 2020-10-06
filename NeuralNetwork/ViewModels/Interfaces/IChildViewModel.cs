namespace NeuralNetwork.ViewModels.Interfaces
{
    public interface IChildViewModel<T>
    {
        T ParentViewModel { get; }
    }
}