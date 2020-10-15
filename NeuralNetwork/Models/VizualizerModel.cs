using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Infrastructure.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Models
{
    public class VizualizerModel
    {
        public static VizualizerModel Instance { get; } = new VizualizerModel();

        private IFileService _fileService;

        private VizualizerModel()
        {
            _fileService = new FileService();
        }
    }
}
