using System;

namespace NeuralNetwork.Models.QueryDataDecorators
{
    public class MNIST28x28BackQueryData : QueryDataModel
    {
        protected QueryDataModel _model;

        public MNIST28x28BackQueryData(QueryDataModel dataModel)
        {
            base.InputValues = dataModel.InputValues;
            base.Marker = dataModel.Marker;
            base.OutputValues = dataModel.OutputValues;

            for (int i = 0; i < dataModel.InputValues.Length; i++)
            {
                base.InputValues[i] = (dataModel.InputValues[i] * 255.0f / 0.99f) - 0.01f;
            }
        }
    }
}