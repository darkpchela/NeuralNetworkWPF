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
        public float[] CurrentInputs { get; set; }
        public float[] CurrentOutputs { get; set; }
        public string CurrentTrainFile { get; set; }

    }
}
