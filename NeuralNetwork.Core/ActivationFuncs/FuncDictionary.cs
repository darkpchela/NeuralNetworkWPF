using System.Collections.Generic;

namespace NeuralNetwork.Core.ActivationFuncs
{
    public static class FuncDictionary
    {
        public static Dictionary<string, IActivationFunc> FuncName = new Dictionary<string, IActivationFunc>
        {
            {"Sigmoid", new SigmoidFunc() }
        };
    }
}