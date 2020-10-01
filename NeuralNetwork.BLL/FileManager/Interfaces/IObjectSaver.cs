namespace NeuralNetwork.Services.FileManager.Interfaces
{
    public interface IObjectSaver<T>
    {
        IObjectSaveStrategy<T> ObjectSaveStrategy { get; set; }
        bool SaveObject(T obj);
    }
}