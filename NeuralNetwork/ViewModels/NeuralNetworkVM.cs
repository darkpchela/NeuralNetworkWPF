using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class NeuralNetworkVM
    {


        public string CurrentFolder { get; set; }
        public Guid CurrentNrlNetId { get; set; }
        public List<float> CurrentInputs { get; set; } = new List<float> { 0.4f , 0.5f, 0.6f };
        public List<float> CurrentOutputs { get; set; } = new List<float>();
        public string CurrentTrainFile { get; set; }

    }
}
