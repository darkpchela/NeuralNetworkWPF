﻿using NeuralNetwork.Infrastructure.Interfaces;
using System;

namespace NeuralNetwork.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public bool DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public T ReadFromFile<T>(string fileName)
        {
            throw new NotImplementedException();
        }

        public T[] ReadFromFiles<T>(string[] fileNames)
        {
            throw new NotImplementedException();
        }

        public bool SaveToFile<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public bool SaveToFiles<T>(T[] objects)
        {
            throw new NotImplementedException();
        }
    }
}