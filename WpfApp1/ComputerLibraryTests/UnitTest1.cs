using System.Collections.Generic;
using Xunit;
using ComputerLibrary;
using System;

namespace ComputerLibraryTests
{
    public class UnitTest1
    {
        [Fact]
        public void CalculateFinishAmount_DataIsCorrect_Then_ResultIs720()
        {
            //������������� �������� ������
            ComputerAmountCalculate testClass = new ComputerAmountCalculate();
            double result = 720.0;
            List<double> componentsCost = new List<double> { 100, 200, 300 };
            int percent = 20;

            //����� ������
            var actualResult = testClass.CalculateFinishAmount(componentsCost, percent);


            //�������� �����������
            Assert.Equal(result, actualResult);
        }

        [Fact]
        public void CalculateFinishAmount_ListIsNull_Then_ResultIsException()
        {
            //������������� �������� ������
            ComputerAmountCalculate testClass = new ComputerAmountCalculate();
            var result = "componentsCost must not be null";
            int percent = 20;
            try
            {
                //����� ������
                testClass.CalculateFinishAmount(null, percent);
            }
            catch (Exception e)
            {
                //�������� �����������
                Assert.Contains(e.Message, result);

                return;
            }

        }

        [Fact]
        public void CalculateFinishAmount_ListIsEmpty_Then_ResultIsException()
        {
            //������������� �������� ������
            ComputerAmountCalculate testClass = new ComputerAmountCalculate();
            var result = "componentsCost must not be empty";
            int percent = 20;

            try
            {
                //����� ������
                testClass.CalculateFinishAmount(new List<double>(), percent);
            }
            catch (Exception e)
            {
                //�������� �����������
                Assert.Contains(e.Message, result);

                return;
            }


        }
    }
}
