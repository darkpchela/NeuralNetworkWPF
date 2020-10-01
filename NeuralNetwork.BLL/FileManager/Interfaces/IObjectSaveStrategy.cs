namespace NeuralNetwork.Services.FileManager.Interfaces
{
    public interface IObjectSaveStrategy<T>
    {
        bool SaveToFile(T obj);
    }
}