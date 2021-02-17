using System;
using System.Drawing;
using System.Windows.Forms;

namespace Plate_Compensation_Converter
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Plate compensation converter.
        /// Designer helper utility
        /// Finds desired percentage of color to use another compensation curve and get the same result
        /// Values array contains data based on RIP processing results measure
        /// </summary>
        private double[,] values = {
            //0,  1,   2,   3,   4,   5,   6,   7,   8,   9,   10,  11,   12,   13,   14,   15,   16,   17,   18,   19,   20,   21,   22,   23,   24,   25,   26,   27,   28,   29,   30,   31,   32,   33,   34,   35,   36,   37,   38,   39,   40,   41,   42,   43,   44,   45,   46,   47,   48,   49,   50,   51,   52,   53,   54,   55,   56,   57,   58,   59,   60,   61,   62,   63,   64,   65,   66,   67,   68,   69,   70,   71,   72,   73,   74,   75,   76,   77,   78,   79,   80,   81,   82,   83,   84,   85,   86,   87,   88,   89,   90,   91,   92,   93,   94,   95,   96,   97,   98,   99,   100
            { 0,  0.7, 1.1, 1.7, 2.1, 2.8, 3.2, 4,   4.5, 5.3, 6.2, 6.9,  7.9,  8.6,  9.9,  10.8, 12.5, 13.6, 15.5, 16.7, 18.5, 20.2, 21.2, 23,   24,   25.5, 26.5, 28.1, 29.1, 30.4, 31.8, 32.6, 33.9, 34.7, 36,   36.7, 37.8, 38.6, 39.6, 40.4, 41.7, 42.3, 43.1, 44,   44.7, 45.7, 46.3, 47.3, 47.9, 48.9, 49.9, 50.6, 51.6, 52.3, 53.4, 54.1, 55.2, 56,   57.1, 57.9, 59,   60.2, 61,   62.3, 63.1, 64.3, 65,   66.2, 66.9, 68.1, 68.9, 70,   71.1, 71.9, 73.1, 73.8, 74.9, 75.6, 76.7, 77.3, 78.3, 79.3, 79.8, 80.8, 81.4, 82.2, 82.9, 83.9, 84.6, 85.6, 87.2, 87.5, 88.9, 89.8, 91.3, 92.4, 94.2, 95.4, 97.3, 98.5, 100}, // 0 - 2017 obr 150 CX 20
            { 0,  0.7, 1.1, 1.7, 2.1, 2.6, 3,   3.4, 3.7, 4.1, 4.5, 5,    6,    7.4,  9.6,  10.6, 11.6, 12.1, 12.9, 13.4, 14.2, 15.3, 16.1, 17.6, 18.6, 20.1, 21.2, 22.7, 23.7, 25.1, 26.5, 27.4, 28.8, 29.7, 31,   32,   33.4, 34.3, 35.6, 36.6, 38,   39.5, 40.4, 41.9, 42.8, 44.3, 45.2, 46.6, 47.5, 48.9, 50.3, 51.3, 52.7, 53.7, 55.1, 56.1, 57.6, 58.6, 60.1, 61,   62.4, 63.8, 65.5, 65.9, 66.8, 68,   68.9, 70.1, 71.9, 72.2, 73.1, 74.4, 75.6, 76.3, 77.5, 78.1, 79.2, 79.9, 80.9, 81.7, 82.8, 83.9, 84.6, 85.7, 86.4, 87.3, 88,   88.9, 89.5, 90.4, 91,   91.8, 92.7, 93.3, 94.2, 94.7, 95.7, 96.3, 97.4, 98.3, 100}, //1 - 2017 obr 140 CX 30
            { 0,  0,   1.5, 2.5, 3.3, 4.4, 5.2, 6.4, 7.3, 8.5, 9.8, 10.7, 12.1, 12.8, 14.1, 15.1, 16.5, 17.6, 19,   19.9, 21.5, 23,   23.8, 25.4, 26.3, 27.9, 28.8, 30.5, 31.6, 33,   34.6, 35.7, 37.3, 38.3, 40,   41.1, 42.6, 43.6, 45.2, 46.2, 47.6, 49,   50,   51.3, 52.3, 53.6, 54.4, 55.6, 56.4, 57.6, 58.8, 59.6, 60.7, 61.5, 62.7, 63.4, 64.5, 65.3, 66.4, 67.1, 68.2, 69.2, 70,   71,   71.7, 73.6, 73.3, 74.1, 74.8, 75.7, 76.3, 77.3, 78.3, 78.9, 80,   80.7, 81.6, 82.3, 83.4, 84.1, 85,   86,   86.7, 87.6, 88.2, 89.1, 89.7, 90.7, 91.3, 92.3, 92.9, 94,   95.1, 95.8, 96.8, 97.5, 98.3, 98.8, 99.4, 99.7, 100}, //2 - 2017 pr 140 CX 30
            { 0,  0.9, 1.4, 2,   2.4, 3.1, 3.6, 4.3, 4.9, 5.6, 6.3, 6.8,  7.4,  7.9,  8.5,  8.9,  9.6,  10,   10.6, 11.1, 11.8, 12.6, 13.1, 13.9, 14.5, 15.6, 16,   16.9, 17.5, 18.5, 19.5, 20.1, 21.2, 21.8, 22.9, 23.6, 24.6, 25.3, 26.4, 27.1, 28.1, 29.1, 29.8, 30.9, 31.6, 32.7, 33.5, 34.6, 35.3, 36.5, 37.7, 38.5, 39.7, 40.5, 41.7, 42.6, 43.8, 44.7, 45.9, 47.4, 48.1, 49.4, 50.3, 51.7, 52.7, 54.2, 55.2, 56.8, 57.9, 59.6, 60.7, 62.3, 63.9, 65,   66.6, 67.7, 69.4, 70.4, 72.1, 73.2, 74.8, 76.5, 77.6, 79.2, 80.3, 81.9, 83,   84.7, 85.8, 87.4, 88.5, 90.1, 91.6, 92.6, 94,   94.9, 96.1, 97,   98.1, 98.9, 100}, //3 - 2019 obr 141 CX 30
            { 0,  0.7, 1.1, 1.7, 2.1, 2.6, 3,   3.5, 3.8, 4.4, 4.9, 5.2,  5.7,  6.1,  6.6,  7,    7.5,  7.9,  8.4,  8.8,  9.6,  10.1, 10.5, 11.2, 11.6, 12.4, 12.9, 13.7, 14.3, 15.1, 16.1, 16.7, 17.7, 18.3, 19.3, 20,   21,   21.6, 22.6, 23.2, 24.2, 25.1, 25.8, 26.7, 27.4, 28.4, 29.1, 30.1, 30.8, 31.9, 33.1, 33.9, 35.1, 36,   37.2, 38.1, 39.5, 41.2, 41.7, 42.6, 43.9, 45.3, 46.1, 47.5, 48.4, 50.9, 50.7, 52.1, 53.1, 54.6, 55.7, 57.3, 59,   60.2, 62,   63.3, 65.2, 66.5, 68.5, 69.8, 71.9, 73.9, 75.2, 77.1, 74.8, 80.2, 81.4, 83.1, 84.2, 85.8, 86.8, 88.4, 90,   91,   92.6, 93.7, 95.2, 96.2, 97.7, 98.6, 100}, //4 - 2020 pr (pp pe)
        };

        private double In_Percent //Get double IN percentage based on textbox value
        {
            get
            {
                try //input filter and parser
                {
                    double o = Convert.ToDouble(IN_P.Text.Replace('.', ','));
                    if (o >= 0 && o <= 100)
                    {
                        IN_P.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        return o;
                    }

                }
                catch { }
                IN_P.ForeColor = Color.Red;
                return -1;
            }
        }

        private int In_cmp //int In compensation value based on radio button selection
        {
            get
            {
                if (CI_1.Checked) return 0;
                else if (CI_2.Checked) return 1;
                else if (CI_3.Checked) return 2;
                else if (CI_4.Checked) return 3;
                else if (CI_5.Checked) return 4;
                else return -1;
            }
        }

        private int Out_cmp  //int Out compensation value based on radio button selection
        {
            get
            {
                if (CO_1.Checked) return 0;
                else if (CO_2.Checked) return 1;
                else if (CO_3.Checked) return 2;
                else if (CO_4.Checked) return 3;
                else if (CO_5.Checked) return 4;
                else return -1;
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }
               
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void Changed(object sender, EventArgs e) //Textbox change event processor
        {
            S_P.Text = "";
            O_P.Text = "";

            if (In_Percent != -1) //text input parsed correctly
            {
                double TIF_Percent = In_Percent;
                
                if (In_cmp >= 0) TIF_Percent = values[In_cmp, Convert.ToInt32(System.Math.Round(In_Percent, 0))];
                
                double Out_Percent = TIF_Percent;
                
                if (Out_cmp >= 0) Out_Percent = find_source(TIF_Percent);

                S_P.Text = TIF_Percent.ToString();
                O_P.Text = Out_Percent.ToString();
            }
        }

        private int find_source (double output) //find source percent based on desired percent
        {
            double difference = 100; //difference between previous value in array and desired value
            int retvalue = -1; //return value
            int compensation = Out_cmp; //out compensation

            for (int c = 0; c < 101; c++) //walk through array
            {
                double difr = System.Math.Abs(values[compensation, c] - output); //get value current difference
                if (difr < difference)  //value is closer to desired
                {
                    difference = difr;
                    retvalue = c;
                }
                else if (difr > difference) //difference more than previous. no reasons to continue
                {
                    return retvalue;
                }
            }
            return retvalue;
        }

    }
}
