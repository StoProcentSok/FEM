using FEM.FEMCalculationsTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FEM
{
    public class DataReader
    {

        public void ReadData(ref IData inputType, string pathToDataConfig)
        {
            //TODO: implement dynamic types, i.e. IData should not define any specific properties
            //-handle varying cross sections, enclose proper parts in 'sections of data'
            var propsInType = GetTypeProperties(inputType);
            foreach (var line in File.ReadAllLines(pathToDataConfig))
            {
                if (propsInType.Any(e=> line.Contains(e)))
                {
                    var propertyToSet = GetPropertyNameFromLine(line);
                    var valueToSet = GetPropertyValueFromLine(line);
                    SetTypeProperty(inputType, propertyToSet, valueToSet);
                }
            }
        }

        public string GetPropertyNameFromLine(string line)
        {
            var nameAndValue = line.Split(':');
            var result = nameAndValue[0].Trim();
            return result;
        }

        public double GetPropertyValueFromLine(string line)
        {
            var nameAndValue = line.Split(':');
            var valueText = nameAndValue[nameAndValue.Length - 1].Trim();
            var result = double.Parse(valueText);
            return result;
        }

        public static List<string> GetTypeProperties(object inputType)
        {
            List<string> result = new List<string>();
            if (inputType == null) return result;

            Type t = inputType.GetType();
            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo prp in props)
            {
                result.Add(prp.Name);
            }
            return result;
        }

        public void SetTypeProperty(object inputType, string propertyName, double value)
        {
            Type t = inputType.GetType();
            PropertyInfo[] props = t.GetProperties();
            int intValue = (int)value;
            props.Where(e => e.Name == propertyName).FirstOrDefault().SetValue(inputType, intValue);
        }

    }

    
}
