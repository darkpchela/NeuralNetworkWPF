namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IObjectSaveStrategy<T>
    {
        bool SaveToFile(T obj);
    }
}