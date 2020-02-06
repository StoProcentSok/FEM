using FEM.FEMCalculationsTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FEM
{
    public class Solver
    {
        static void Main(string[] args)
        {
            IData data = new HeatTransferCalculationsData();
            var path = Assembly.GetExecutingAssembly().Location;
            var resourcesPath = @"..\..\..\Resources\Data.txt";
            path =  Path.GetFullPath(Path.Combine(path, resourcesPath));

            var reader = new DataReader();
            reader.ReadData(ref data, path);

            data.noOfNodes = data.noOfElements + 1;
            data.singleSubelementLength = data.totalLengthOfElement / data.noOfElements;

            var result = CalculateFEMValues(data, data.noOfElements, data.totalLengthOfElement, data.heatFlux,
                data.crossSection, data.thermalConductivity, data.convectionCoefficient, data.ambientTemperature);

            Console.ReadLine();
        }
        public static double[] CalculateFEMValues(IData _data, double _noOfElements, double totalLength, double heatFlux, 
            double crossSection, double thermalConductivity, double convectionCoefficient, double ambientTemperature)
        {
            Console.WriteLine("Input Data:");
            Console.WriteLine(@"Thermal Conductivity     K   = " + thermalConductivity);
            Console.WriteLine(@"Convection Coefficient Alpha = " + convectionCoefficient);
            Console.WriteLine(@"Element Cross Section   (s)  = " + crossSection);
            Console.WriteLine(@"Heat Flux               (q)  = " + heatFlux);
            Console.WriteLine(@"Ambient Temperature    (t)a  = " + ambientTemperature);
            Console.WriteLine(@"Number of Elements           = " + _noOfElements);
            Console.WriteLine(@"Number of Nodes              = " + (_noOfElements + 1));
            Console.WriteLine(@"Total length             (l) = " + totalLength);
            Console.WriteLine(@"Single Element Length        = " + totalLength/_noOfElements);

            Console.WriteLine("");
            Console.WriteLine("FEM calculations result:");
            Console.WriteLine("");

            FEMGrid grid = new FEMGrid(_data);
            grid.createLocalMatrix();
            grid.createLocalVector();
            grid.createGlobalMatrix();
            grid.createGlobalVector();

            GaussEliminator gauss = new GaussEliminator();
            Console.WriteLine("Global elements matrix:");
            gauss.generateAndPrintMatrixRepresentation(grid.H);
            Console.WriteLine("\nEquations matrix:");
            
            var result = gauss.calculate(grid.H, grid.P, grid.nodes.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"Temperature in {i + 1} element: {result[i]}.");
            }

            return new double[1];
        }
    }
}
