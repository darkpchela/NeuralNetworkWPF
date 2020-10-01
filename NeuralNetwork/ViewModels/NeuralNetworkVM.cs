using NeuralNetwork.Infrastructure;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class NeuralNetworkVM
    {
        private INeuralNetworkDefaultService _nrlNetService;
        private RelayCommand _query;

        public string CurrentFolder { get; set; }

        public Guid CurrentNrlNetId { get; set; }

        public List<float> CurrentInputs { get; set; } = new List<float> { 0.4f , 0.5f, 0.6f };

        public List<float> CurrentOutputs { get; set; } = new List<float>();

        public string CurrentTrainFile { get; set; }

        public RelayCommand Query
        {
            get
            {
                return _query ?? (_query = new RelayCommand(obj=> 
                {
                    var results = _nrlNetService.Query(CurrentInputs.ToArray(), CurrentNrlNetId);
                }));
            }
        }
        
        public NeuralNetworkVM()
        {
            _nrlNetService = new NeuralNetworkDefaultService();
        }

    }
}
