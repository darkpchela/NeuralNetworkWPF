namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileService
    {
        T ReadFromFile<T>(string fileName);

        T[] ReadFromFiles<T>(string[] fileNames);

        bool DeleteFile(string fileName);

        bool SaveToFile<T>(T obj);

        bool SaveToFiles<T>(T[] objects);
    }
}