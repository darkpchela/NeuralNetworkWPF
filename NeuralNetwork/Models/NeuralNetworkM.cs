using NeuralNetwork.BLL.Interfaces;
using NeuralNetwork.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Models
{
    public class NeuralNetworkM
    {
        private INeuralNetworkDefaultService _neuralNetworkService;
        private IFileService _fileService;
        
        public string CurrentFolder { get; private set; }

        public NeuralNetworkM(INeuralNetworkDefaultService neuralNetworkDefaultService, IFileService fileService)
        {
            _neuralNetworkService = neuralNetworkDefaultService;
            _fileService = fileService;
        }
    }
}
