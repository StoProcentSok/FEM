using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM.FEMCalculationsTypes
{
    public class HeatTransferCalculationsData : IData
    {
        public int noOfElements { get; set; }
        public int noOfNodes { get; set; }
        public double totalLengthOfElement { get; set; }
        public double crossSection { get; set; }
        public double singleSubelementLength { get; set; }
        public double heatFlux { get; set; } //gestosc strumienia ciepla q
        public double thermalConductivity { get; set; } //k / lambda
        public double convectionCoefficient { get; set; } // Alpha
        public double ambientTemperature { get; set; } //tx




        public List<string> getDataProperties()
        {
            var dataProps = this.GetType().GetProperties();

            List<string> result = new List<string>();
            foreach (var prop in dataProps)
            {
                result.Add(prop.Name);
            }

            return result;
        }
    }
}
