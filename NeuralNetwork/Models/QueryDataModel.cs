using NeuralNetwork.ViewModels;

namespace NeuralNetwork.Models
{
    public class QueryDataModel
    {
        public virtual string Marker { get; set; }

        public virtual float[] InputValues { get; set; }

        public virtual float[] OutputValues { get; set; }

        public QueryDataVM GetViewModel()
        {
            var viewModel = new QueryDataVM(this);
            viewModel.Marker = Marker;

            return viewModel;
        }
    }
}