using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _4Uzduotis
{
    public partial class Form1 : Form
    {
        private List<int> selected = new List<int>();
        public Form1()
        {
            InitializeComponent();
            UpdateInterface();
        }
        public void UpdateInterface()
        {
            listViewRawData.Items.Clear();
            Database database = new Database();
            List<DataFormat> results = database.GetData();
            for (int i = 0; i < results.Count; i++)
            {
                listViewRawData.Items.Add(results[i].vardas);
                listViewRawData.Items[i].SubItems.Add(results[i].x1.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x2.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x3.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x4.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x5.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x6.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x7.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x8.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x9.ToString());
                listViewRawData.Items[i].SubItems.Add(results[i].x10.ToString());
            }
        }

        private void addPoints_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            List<DataFormat> results = database.GetData();
            if (listViewRawData.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pasirinkite taškus iš pacientu informacijos lentelės");
                return;
            }
            if (listViewRawData.SelectedItems.Count > results.Count / 2)
            {
                MessageBox.Show($"Pasirinkta per daug taškų maksimalus skaicius: {results.Count / 2}");
                return;
            }
            List<DataFormat> selectedPoints = new List<DataFormat>();
            var data = listViewRawData.SelectedIndices;
            foreach (int i in data)
            {
                selectedPoints.Add(results[i]);

            }
            for (int i = 0; i < data.Count; i++)
            {
                selected.Add(data[i]);
            }
            for (int i = 0; i < selectedPoints.Count; i++)
            {
                listViewSelected.Items.Add(selectedPoints[i].vardas);
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x1.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x2.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x3.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x4.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x5.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x6.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x7.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x8.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x9.ToString());
                listViewSelected.Items[i].SubItems.Add(selectedPoints[i].x10.ToString());
            }
        }

        private void clearList_Click(object sender, EventArgs e)
        {
            listViewSelected.Items.Clear();
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Database database = new Database();
            List<DataFormat> results = database.GetData();
            Method method = new Method();
            if (listViewSelected.Items.Count != 0)
            {
                List<DataFormat> selectedPoints = new List<DataFormat>();
                for (int i = 0; i < selected.Count; i++)
                {
                    selectedPoints.Add(results[selected[i]]);
                }
                selectedPoints.Where(p => results.Remove(p)).ToList();
                labelCenter.Text = selectedPoints.Count.ToString();
                List<DataFormat> klasterResultList = Calculate(selectedPoints, results);
                List<DataFormat> comparingList = Calculate(klasterResultList, results);
                if (klasterResultList.SequenceEqual(comparingList))
                {
                    UpdateFinalList(klasterResultList);
                    return;
                }

                bool endCalculation = false;
                while (endCalculation == false)
                {
                    int equality = 0; 
                    klasterResultList = Calculate(comparingList, results);
                    for (int i = 0; i< klasterResultList.Count;i++)
                    {
                        if (klasterResultList[i].x1 == comparingList[i].x1 && klasterResultList[i].x2 == comparingList[i].x2 && klasterResultList[i].x3 == comparingList[i].x3 && klasterResultList[i].x4 == comparingList[i].x4 && klasterResultList[i].x5 == comparingList[i].x5 && klasterResultList[i].x6 == comparingList[i].x6 && klasterResultList[i].x7 == comparingList[i].x7 && klasterResultList[i].x8 == comparingList[i].x8 && klasterResultList[i].x9 == comparingList[i].x9 && klasterResultList[i].x10 == comparingList[i].x10)
                        {
                            equality++;
                        }
                    }
                    if (equality == klasterResultList.Count)
                    {
                        endCalculation = true;
                        UpdateFinalList(klasterResultList);
                        return;
                    }
                    else
                    {
                        comparingList = klasterResultList;
                    }
                }
            }
            else
            {
                Random rnd = new Random();
                int centers = rnd.Next(1, results.Count / 2);
                List<DataFormat> randomSelectedPonts = new List<DataFormat>();
                int[] array = new int[centers];
                for (int i = 0; i < centers; i++)
                {
                    int point = rnd.Next(1, results.Count);
                    if (array.Contains(point))
                    {
                        i--;
                        continue;
                    }
                    array[i] = point;
                    randomSelectedPonts.Add(results[point]);
                }
                randomSelectedPonts.Where(p => results.Remove(p)).ToList();
                labelCenter.Text = randomSelectedPonts.Count.ToString();
                List<DataFormat> klasterResultList = Calculate(randomSelectedPonts, results);
                List<DataFormat> comparingList = Calculate(klasterResultList, results);
                if (klasterResultList.SequenceEqual(comparingList))
                {
                    UpdateFinalList(klasterResultList);
                    return;
                }

                bool endCalculation = false;
                while (endCalculation == false)
                {
                    int equality = 0;
                    klasterResultList = Calculate(comparingList, results);
                    for (int i = 0; i < klasterResultList.Count; i++)
                    {
                        if (klasterResultList[i].x1 == comparingList[i].x1 && klasterResultList[i].x2 == comparingList[i].x2 && klasterResultList[i].x3 == comparingList[i].x3 && klasterResultList[i].x4 == comparingList[i].x4 && klasterResultList[i].x5 == comparingList[i].x5 && klasterResultList[i].x6 == comparingList[i].x6 && klasterResultList[i].x7 == comparingList[i].x7 && klasterResultList[i].x8 == comparingList[i].x8 && klasterResultList[i].x9 == comparingList[i].x9 && klasterResultList[i].x10 == comparingList[i].x10)
                        {
                            equality++;
                        }
                    }
                    if (equality == klasterResultList.Count)
                    {
                        endCalculation = true;
                        UpdateFinalList(klasterResultList);
                        return;
                    }
                    else
                    {
                        comparingList = klasterResultList;
                    }

                }
            }
        }

        public void updateClasterList(List<DataFormat> klasterList)
        {
            //listBox1.Items.Clear();
            listBox1.Items.Add("--------------------------------------");
            for (int i = 0; i < klasterList.Count; i++)
            {
                listBox1.Items.Add("Taškas: " + klasterList[i].vardas + " priskirtas taškui: " + klasterList[i].ikiTaskoVardas);
            }
        }

        public void UpdateFinalList(List<DataFormat> list)
        {
            listView1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listView1.Items.Add("Centro: " + list[i].vardas + " kordinatės: " + " X1: " + list[i].x1 + " X2: " + list[i].x2 + " X3: " + list[i].x3 + " X4: " + list[i].x4 + " X5: " + list[i].x5 + " X6: " + list[i].x6 + " X7: " + list[i].x7 + " X8: " + list[i].x8 + " X9: " + list[i].x9 + " X10: " + list[i].x10);
            }
        }


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
                    resultList.Add(new DataFormat { vardas = baseList[z].vardas, x1 = baseList[z].x1, x2 = baseList[z].x2, x3 = baseList[z].x3, x4 = baseList[z].x4, x5 = baseList[z].x5, x6 = baseList[z].x6, x7 = baseList[z].x7, x8 = baseList[z].x8, x9 = baseList[z].x9, x10 = baseList[z].x10, ikiTaskoVardas = selectedPointList[i].vardas, result = res });
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

            for (int i = 0; i < selectedPointList.Count; i++)
            {
                klasterList.Add(new DataFormat { vardas = selectedPointList[i].vardas, x1 = selectedPointList[i].x1, x2 = selectedPointList[i].x2, x3 = selectedPointList[i].x3, x4 = selectedPointList[i].x4, x5 = selectedPointList[i].x5, x6 = selectedPointList[i].x6, x7 = selectedPointList[i].x7, x8 = selectedPointList[i].x8, x9 = selectedPointList[i].x9, x10 = selectedPointList[i].x10, });
            }
            for (int i = 0; i < baseList.Count; i++)
            {
                for (int z = 0; z < klasterList.Count; z++)
                {
                    if (klasterList[z].vardas == baseList[i].ikiTaskoVardas)
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
            for (int i = 0; i < klasterList.Count; i++)
            {
                int number = 0;
                for (int z = 0; z < baseList.Count; z++)
                {
                    if (klasterList[i].vardas == baseList[z].ikiTaskoVardas)
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
            updateClasterList(baseList);
            return klasterList;

        }



    }
}
