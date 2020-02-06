using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM.FEMCalculationsTypes
{
    //TODO: create base interface with repetitive properties
    public interface IData
    {
        int noOfElements { get; set; }
        int noOfNodes { get; set; }
        double totalLengthOfElement { get; set; }
        double crossSection { get; set; }
        double singleSubelementLength { get; set; }
        double heatFlux { get; set; } //gestosc strumienia ciepla q
        double thermalConductivity { get; set; } //k / lambda
        double convectionCoefficient { get; set; } // Alpha
        double ambientTemperature { get; set; } //tx
    }
}
