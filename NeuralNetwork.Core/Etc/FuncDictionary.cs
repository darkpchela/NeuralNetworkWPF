using NeuralNetwork.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Core.Etc
{
    public static class FuncDictionary
    {
        private static Dictionary<string, Func<float, float>> _FuncName = new Dictionary<string, Func<float, float>>
        {
            {"Sigmoid",  MathFuncs.Sigmoid },
            {"SigmoidRev", MathFuncs.SigmoidReverse }
        };

        public static bool TryGetFunc(string funcName, out Func<float, float> func)
        {
            return _FuncName.TryGetValue(funcName, out func);
        }

        public static string GetFuncName(Func<float, float> func)
        {
            return _FuncName.First(f => f.Value == func).Key ?? "Unknown function";
        }
    }
}