using NeuralNetwork.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.BLL.Interfaces
{
    public interface INeuralNetworkFactory
    {
        NrlNet GetNewInstance();
        NrlNet LoadInstance(NrlNetData nrlNetData);
    }
}
