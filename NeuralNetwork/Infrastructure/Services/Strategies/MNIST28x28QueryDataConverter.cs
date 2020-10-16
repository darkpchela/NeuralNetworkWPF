using NeuralNetwork.Models;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class MNIST28x28ReadyTrainData : QueryDataModel
    {
        protected QueryDataModel _sourceDataModel;

        public MNIST28x28ReadyTrainData(QueryDataModel queryDataModel)
        {
            _sourceDataModel = queryDataModel;

            InputValues = new float[784];
            for (int i = 0; i < queryDataModel.InputValues.Length; i++)
            {
                InputValues[i] = (queryDataModel.InputValues[i] / 255.0f * 0.99f) + 0.01f;
            }

            OutputValues = new float[10];
            int rightAnswer = int.Parse(queryDataModel.Marker);
            for (int i = 0; i < 10; i++)
            {
                if (i == rightAnswer)
                    OutputValues[i] = 0.99f;
                else
                    OutputValues[i] = 0.01f;
            }
        }
    }
}