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
            Console.WriteLine("------------------ Dane pobrane z pliku ------------------");
            Console.WriteLine(@"K = " + thermalConductivity);
            Console.WriteLine(@"Alfa = " + convectionCoefficient);
            Console.WriteLine(@"Przekroj (s) = " + crossSection);
            Console.WriteLine(@"Strumien (q) = " + heatFlux);
            Console.WriteLine(@"Temperatura otoczenia = " + ambientTemperature);
            Console.WriteLine(@"Ilosc elementow = " + _noOfElements);
            Console.WriteLine(@"Ilosc wezlow = " + (_noOfElements + 1));
            Console.WriteLine(@"Dlugosc calego elementu (l) = " + totalLength);
            Console.WriteLine(@"Dlugosc pojedynczego elementu = " + totalLength/_noOfElements);

            Console.WriteLine("--------------------- Rozwiazanie MES ---------------------");
            
            FEMGrid grid = new FEMGrid(_data);
            grid.createLocalMatrix();
            grid.createLocalVector();
            grid.createGlobalMatrix();
            grid.createGlobalVector();

            GaussEliminator gauss = new GaussEliminator();
            gauss.printEquationsMatrix(grid.H);

            //var result = gauss.calculate(grid.H, grid.P, grid.nodes.Length);


            return new double[1];
        }


    }

    

   

    
}
