namespace NeuralNetwork.BLL.FileManager.Interfaces
{
    public interface IObjectSaveStrategy<T>
    {
        bool SaveToFile(T obj);
    }
}