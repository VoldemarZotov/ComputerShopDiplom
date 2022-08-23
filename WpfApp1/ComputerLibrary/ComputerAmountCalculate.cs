using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ComputerLibrary
{
    public class ComputerAmountCalculate
    {
        public double CalculateFinishAmount(List<double> componentsCost, int percent ) 
        {
            if (componentsCost == null)
                throw new Exception("componentsCost must not be null");

            if (componentsCost.Count == 0)
                throw new Exception("componentsCost must not be empty");

            var doublePercent = percent / 100.0;
            var fullCost = componentsCost.Sum();

           return (fullCost * doublePercent) + fullCost;
        }

        public double CalculateFinishAmount(List<double> componentsCost)
        {
            return CalculateFinishAmount(componentsCost, 20);
        }


    }
}
