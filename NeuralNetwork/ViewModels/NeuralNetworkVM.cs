using NeuralNetwork.Infrastructure;
using NeuralNetwork.Model;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class NeuralNetworkVM
    {
        private NeuralNetworkWorkshopModel _nrlNetWorkshopModel;

        public string CurrentFolder { get; set; }

        private string _currentNrlNetId;
        public string CurrentNrlNetId 
        {
            get 
            {
                return string.IsNullOrEmpty(_currentNrlNetId) ? "No selected NeuralNetwork" : _currentNrlNetId;
            }
            set
            {
                _currentNrlNetId = value;
            }
        }

        public List<float> CurrentInputs { get; set; } = new List<float> { 0.0f , 0.0f, 0.0f };

        public List<float> CurrentOutputs { get; set; } = new List<float> { 0.0f, 0.0f, 0.0f };

        public string CurrentTrainFile { get; set; }

        private RelayCommand _create;
        public RelayCommand Create
        {
            get
            {
                return _create ?? new RelayCommand(obj=> 
                { 

                });
            }
        }

        private RelayCommand _query;
        public RelayCommand Query
        {
            get
            {
                return _query ?? (_query = new RelayCommand(obj=> 
                {
                    var results = _nrlNetWorkshopModel.Query(CurrentInputs.ToArray(), CurrentNrlNetId);
                }));
            }
        }
        
        public NeuralNetworkVM()
        {
            _nrlNetWorkshopModel = new NeuralNetworkWorkshopModel();
        }

    }
}
