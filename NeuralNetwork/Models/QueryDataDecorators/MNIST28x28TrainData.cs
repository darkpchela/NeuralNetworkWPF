namespace NeuralNetwork.Models.QueryDataDecorators
{
    public class MNIST28x28TrainData : QueryDataModel
    {
        protected QueryDataModel _sourceDataModel;

        public MNIST28x28TrainData(QueryDataModel queryDataModel)
        {
            _sourceDataModel = queryDataModel;

            base.InputValues = new float[784];
            for (int i = 0; i < queryDataModel.InputValues.Length; i++)
            {
                base.InputValues[i] = (queryDataModel.InputValues[i] / 255.0f * 0.99f) + 0.01f;
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