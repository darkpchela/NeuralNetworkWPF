namespace NeuralNetwork.Model.FileManager.Interfaces
{
    public interface IObjectSaveStrategy<T>
    {
        bool SaveToFile(T obj);
    }
}