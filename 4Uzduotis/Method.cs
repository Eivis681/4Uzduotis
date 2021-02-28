using System;
using System.Collections.Generic;
using System.Linq;

namespace _4Uzduotis
{
    public class Method
    {
        public List<DataFormat> Calculate(List<DataFormat> selectedPointList, List<DataFormat> baseList)
        {
            List<DataFormat> resultList = new List<DataFormat>();
            for (int i = 0; i < selectedPointList.Count; i++)
            {
                for (int z = 0; z < baseList.Count; z++)
                {
                    double res = Math.Sqrt((Math.Pow(selectedPointList[i].x1 - baseList[z].x1, 2)) + (Math.Pow(selectedPointList[i].x2 - baseList[z].x2, 2)) + (Math.Pow(selectedPointList[i].x3 - baseList[z].x3, 2)) + (Math.Pow(selectedPointList[i].x4 - baseList[z].x4, 2)) + (Math.Pow(selectedPointList[i].x5 - baseList[z].x5, 2)) + (Math.Pow(selectedPointList[i].x6 - baseList[z].x6, 2)) + (Math.Pow(selectedPointList[i].x7 - baseList[z].x8, 2)) + (Math.Pow(selectedPointList[i].x9 - baseList[z].x9, 2)) + (Math.Pow(selectedPointList[i].x10 - baseList[z].x10, 2)));
                    baseList[z].result = res;
                    baseList[z].ikiTaskoVardas = selectedPointList[i].vardas;
                    resultList.Add(new DataFormat {vardas = baseList[z].vardas, x1= baseList[z].x1, x2 = baseList[z].x2, x3 = baseList[z].x3, x4 = baseList[z].x4, x5 = baseList[z].x5, x6 = baseList[z].x6, x7 = baseList[z].x7, x8 = baseList[z].x8, x9 = baseList[z].x9, x10 = baseList[z].x10, ikiTaskoVardas = selectedPointList[i].vardas, result = res }); 
                }
            }

            for (int i = 0; i < baseList.Count; i++)
            {
                for (int z = 0; z < resultList.Count; z++)
                {
                    if (baseList[i].vardas == resultList[z].vardas)
                    {
                        if (baseList[i].result > resultList[z].result)
                        {
                            baseList[i].ikiTaskoVardas = resultList[z].ikiTaskoVardas;
                            baseList[i].result = resultList[z].result;
                        }
                    }
                }
            }
            List<DataFormat> klasterList = new List<DataFormat>();
            
            for (int i = 0; i< selectedPointList.Count;i++)
            {
                klasterList.Add(new DataFormat { ikiTaskoVardas = selectedPointList[i].vardas, x1 = selectedPointList[i].x1, x2 = selectedPointList[i].x2, x3 = selectedPointList[i].x3, x4 = selectedPointList[i].x4, x5 = selectedPointList[i].x5, x6 = selectedPointList[i].x6, x7 = selectedPointList[i].x7, x8 = selectedPointList[i].x8, x9 = selectedPointList[i].x9, x10 = selectedPointList[i].x10, });
            }
            for (int i = 0; i < baseList.Count; i++)
            {
                for (int z = 0; z< klasterList.Count;z++)
                {
                    if (klasterList[z].ikiTaskoVardas == baseList[i].ikiTaskoVardas)
                    {
                        klasterList[z].x1 = klasterList[z].x1 + baseList[i].x1;
                        klasterList[z].x2 = klasterList[z].x2 + baseList[i].x2;
                        klasterList[z].x3 = klasterList[z].x3 + baseList[i].x3;
                        klasterList[z].x4 = klasterList[z].x4 + baseList[i].x4;
                        klasterList[z].x5 = klasterList[z].x5 + baseList[i].x5;
                        klasterList[z].x6 = klasterList[z].x6 + baseList[i].x6;
                        klasterList[z].x7 = klasterList[z].x7 + baseList[i].x7;
                        klasterList[z].x8 = klasterList[z].x8 + baseList[i].x8;
                        klasterList[z].x9 = klasterList[z].x9 + baseList[i].x9;
                        klasterList[z].x9 = klasterList[z].x9 + baseList[i].x9;
                    }
                }
            }
            for (int i =0; i< klasterList.Count;i++)
            {
                int number = 0;
                for (int z = 0; z < baseList.Count; z++)
                {
                    if (klasterList[i].ikiTaskoVardas == baseList[z].ikiTaskoVardas)
                    {
                        number++;
                    }
                }
                if (number == 0)
                {
                    continue;
                }
                klasterList[i].x1 = klasterList[i].x1 / number;
                klasterList[i].x2 = klasterList[i].x2 / number;
                klasterList[i].x3 = klasterList[i].x3 / number;
                klasterList[i].x4 = klasterList[i].x4 / number;
                klasterList[i].x5 = klasterList[i].x5 / number;
                klasterList[i].x6 = klasterList[i].x6 / number;
                klasterList[i].x7 = klasterList[i].x7 / number;
                klasterList[i].x8 = klasterList[i].x8 / number;
                klasterList[i].x9 = klasterList[i].x9 / number;
                klasterList[i].x9 = klasterList[i].x9 / number;
            }
            return klasterList;

        }
    }
}
