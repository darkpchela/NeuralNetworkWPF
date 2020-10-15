using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services.Strategies.Etc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    internal class CsvMNISTFileReadStrategy : IFileReadStrategy<IEnumerable<MNISTTrainData28x28>>
    {
        public Task<IEnumerable<MNISTTrainData28x28>> ReadFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName, Encoding.UTF8);
            var datas = new List<MNISTTrainData28x28>();
            foreach (var l in lines)
            {
                var chars = l.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var data = new MNISTTrainData28x28();

                data.Marker = int.Parse(chars[0]);
                data.PixelsValues = new int[784];

                for (int i = 1; i < chars.Length; i++)
                {
                    data.PixelsValues[i - 1] = int.Parse(chars[i]);
                }

                datas.Add(data);
            }

            return Task.FromResult((IEnumerable<MNISTTrainData28x28>)datas);
        }
    }
}