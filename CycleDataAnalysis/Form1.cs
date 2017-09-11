using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CycleDataAnalysis
{
    public partial class Form1 : Form
    {
        string VersionType;
        string MonitorType;

        string ModeTypeA;
        string ModeTypeB;
        string ModeTypeC;
        string ModeTypeD;
        string ModeTypeE;
        string ModeTypeF;
        string ModeTypeG;
        string ModeTypeH;
        string ModeTypeI;

        int SpeedType;
        int CadenceType;
        int AltType;
        int PowerType;
        int PowerLRBType;
        int PowerPIType;
        int HRCCType;
        int EuroUsType;
        int AirPType;

        string DateYear;
        string DateMonth;
        string DateDay;
        string Fulldate;

        string StartHr;
        string StartMin;
        string StartSec;
        string FullStartTime;

        string LengthHr;
        string LengthMin;
        string LengthSec;
        double FullLengthInSecs;
        int HRRowCount;

        string Intervaltype;
        string SModetype;

        int MaxHR; //Max Heart Rate
        int MinHR; //Min Heart Rate
        double AvgHR; //Average Heart Rate

        int MaxPW; //Max Power
        double AvgPW; //Average power
        int MinPW; //Min Power

        int MaxALT; //Max Altitude
        double AvgALT; //Average Altitude

        double MaxSP; //Max Speed
        public double AvgSP; //Average Speed

        int intervalVal;
        int NP;
        double IF;
        int Versionval;
        double Distance;
        string filetext;
        double FTP;
        int NP1;

        public double[] Larray, Rarray, PIarray;
        public string result;
        long PowerB, leftMask, PedIndMask;
        string PBbinary, leftBin, PiBin;
        public double lvalue, pvalue, rvalue;
        double left, pedindex, right;
        public double rows;

        public GraphPane GraphPane;
        public GraphPane GraphPane2;

        int[] intervals = new int[20] { 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800, 0, 3800 };
        int intcounter = 0;

        Form2 newgraph = new Form2();


        public Form1()
        {
            InitializeComponent();
            zedGraphControl1.Visible = false; //Graph panel not visible until file is loaded
        }

        /// <summary>
        /// Loads open file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //Filters file format to .hrm only
                openFileDialog1.Filter = "hrm files (*.hrm)|*.hrm";
                openFileDialog1.FileName = "ASDBExampleCycleComputerData.hrm";
                openFileDialog1.ShowDialog();

                string strfilename = openFileDialog1.FileName;
                filetext = File.ReadAllText(strfilename);
                richTextBox1.Text = filetext;

                filehead();
            }
            catch (Exception etc)
            {
                MessageBox.Show("An error Ocurred: " + etc.Message);
            }
        }

        public void filehead()
        {
            // Finds the "version" and prints the value
            String FindVersion = "Version";
            int indexofVersion;
            indexofVersion = filetext.IndexOf(FindVersion);
            VersionType = filetext.Substring(indexofVersion + 8, 3);
            Versionval = Int32.Parse(VersionType);
            double version = double.Parse(VersionType);
            versionLabel.Text = "Version: " + version.ToString();

            // Finds the "SMode" and prints the value
            String FindSMode = "SMode";
            int indexofSMode;
            indexofSMode = filetext.IndexOf(FindSMode);
            SModetype = filetext.Substring(indexofSMode + 6, 9);
            int SModeval = Int32.Parse(SModetype);
            SModeLabel.Text = "SMode: " + SModetype.ToString();


            // Find the Monitor model number and prints the appropriate value
            String FindMonitor = "Monitor";
            int indexofMonitor;
            indexofMonitor = filetext.IndexOf(FindMonitor);
            MonitorType = filetext.Substring(indexofMonitor + 8, 2);
            int Monitorval = Int32.Parse(MonitorType);
            if (Monitorval == 1) { modelLabel.Text = "Model: " + "Polar Sport Tester / Vantage XL"; }
            if (Monitorval == 2) { modelLabel.Text = "Model: " + "Polar Vantage NV (VNV)"; }
            if (Monitorval == 3) { modelLabel.Text = "Model: " + "Polar Accurex Plus"; }
            if (Monitorval == 4) { modelLabel.Text = "Model: " + "Polar XTrainer Plus"; }
            if (Monitorval == 6) { modelLabel.Text = "Model: " + "Polar S520"; }
            if (Monitorval == 7) { modelLabel.Text = "Model: " + "Polar Coach"; }
            if (Monitorval == 8) { modelLabel.Text = "Model: " + "Polar S210"; }
            if (Monitorval == 9) { modelLabel.Text = "Model: " + "Polar S410"; }
            if (Monitorval == 10) { modelLabel.Text = "Model: " + "Polar S610 / S610i"; }
            if (Monitorval == 12) { modelLabel.Text = "Model: " + "Polar S710 / S710i / S720i"; }
            if (Monitorval == 13) { modelLabel.Text = "Model: " + "Polar S810 / S810i"; }
            if (Monitorval == 15) { modelLabel.Text = "Model: " + "Polar E600"; }
            if (Monitorval == 20) { modelLabel.Text = "Model: " + "Polar AXN500"; }
            if (Monitorval == 21) { modelLabel.Text = "Model: " + "Polar AXN700"; }
            if (Monitorval == 22) { modelLabel.Text = "Model: " + "Polar S625X / S725X"; }
            if (Monitorval == 23) { modelLabel.Text = "Model: " + "Polar S725"; }
            if (Monitorval == 33) { modelLabel.Text = "Model: " + "Polar CS400"; }
            if (Monitorval == 34) { modelLabel.Text = "Model: " + "Polar CS600X"; }
            if (Monitorval == 35) { modelLabel.Text = "Model: " + "Polar CS600"; }
            if (Monitorval == 36) { modelLabel.Text = "Model: " + "Polar RS400"; }
            if (Monitorval == 37) { modelLabel.Text = "Model: " + "Polar RS800"; }
            if (Monitorval == 38) { modelLabel.Text = "Model: " + "Polar RS800X"; }

            // Finds and sets the date in dd-mm-yyyy format
            String FindDate = "Date";
            int indexofDate = filetext.IndexOf(FindDate);
            DateYear = filetext.Substring(indexofDate + 5, 4);
            DateMonth = filetext.Substring(indexofDate + 9, 2);
            DateDay = filetext.Substring(indexofDate + 11, 2);
            Fulldate = DateDay + "-" + DateMonth + "-" + DateYear;
            dateLabel.Text = "Date: " + Fulldate;

            // Finds and sets "StartTime" values in hr:mm:ss format
            String FindStartTime = "StartTime=";
            int indexofStartTime = filetext.IndexOf(FindStartTime);
            StartHr = filetext.Substring(indexofStartTime + 10, 2);
            StartMin = filetext.Substring(indexofStartTime + 13, 2);
            StartSec = filetext.Substring(indexofStartTime + 16, 4);
            FullStartTime = StartHr + ":" + StartMin + ":" + StartSec;
            StartTimelabel.Text = "Start Time: " + FullStartTime;

            // Finds and sets length value
            String FindLength = "Length=";
            int indexofLength = filetext.IndexOf(FindLength);
            LengthHr = filetext.Substring(indexofLength + 7, 2);
            LengthMin = filetext.Substring(indexofLength + 10, 2);
            LengthSec = filetext.Substring(indexofLength + 13, 5);
            int LengthHrval = Int32.Parse(LengthHr);
            int LengthMinval = Int32.Parse(LengthMin);
            double LengthSecval = double.Parse(LengthSec);
            FullLengthInSecs = ((LengthHrval * 3600) + (LengthMinval * 60) + (LengthSecval));
            lengthLabel.Text = "Length: " + LengthHr + ":" + LengthMin + ":" + LengthSec;

            // Finds and sets interval value
            String FindInterval = "Interval";
            int indexofInterval;
            indexofInterval = filetext.IndexOf(FindInterval);
            Intervaltype = filetext.Substring(indexofInterval + 9, 2);
            int intervalVal = Int32.Parse(Intervaltype);
            HRRowCount = (int)Math.Ceiling(FullLengthInSecs / intervalVal);
            intervalLabel.Text = "Interval: " + Intervaltype.ToString();
            /////

            DateTime date1 = new DateTime(2008, 8, 29, 0, 0, 0);

            // Reads Time, Heart rate and Speed from the file and adds a column to the datagridview
            string[] Data = Regex.Split(filetext, "HRData]");
            Data[1] = Data[1].Trim();
            string[] HRDatalines = Regex.Split(Data[1], "\r\n|\r|\n");
            dataGridView1.Columns.Add("Column", "Time");

            dataGridView1.Columns.Add("Column", "Heart Rate (BPM)");

            //Depending on the unit selection on the Euro/US box, the units switches between km and miles
            if (EuroUsType == 0)
            {
                dataGridView1.Columns.Add("Column", "Speed (KMPH)");
                mph.Checked = false;
                kmph.Checked = true;
            }

            else if (EuroUsType == 1)
            {
                dataGridView1.Columns.Add("Column", "Speed (MPH");
                kmph.Checked = false;
                mph.Checked = true;
            }

            //If the file contains data for version 1.05
            if (Versionval <= 105)
            {
                // Finds all the "modes" in the file
                String FindMode = "Mode";
                int indexofMode = filetext.IndexOf(FindMode);
                ModeTypeA = filetext.Substring(indexofMode + 5, 1);
                ModeTypeB = filetext.Substring(indexofMode + 6, 1);
                ModeTypeC = filetext.Substring(indexofMode + 7, 1);

                int ModeTypeA1 = Int32.Parse(ModeTypeA);
                if (ModeTypeA1 == 0)
                {
                    CadenceType = 1;
                    // Adds Cadence column to the datagridview
                    dataGridView1.Columns.Add("Column", "Cadence");
                }
                else if (ModeTypeA1 == 1)
                {
                    CadenceType = 0;
                    AltType = 1;
                    // Adds Altitude column to the datagridview
                    dataGridView1.Columns.Add("Column", "Altitude");
                }

                HRCCType = Int32.Parse(ModeTypeB);
                EuroUsType = Int32.Parse(ModeTypeC);

                //Adds the time to each column
                for (int i = 0; i < HRDatalines.Length; i++)
                {
                    date1 = date1.AddSeconds(intervalVal);
                    string[] HRDataColumn = Regex.Split(HRDatalines[i], "\t");
                    double speed123 = Int32.Parse(HRDataColumn[1]);
                    speed123 = speed123 / 10;
                    HRDataColumn[1] = speed123.ToString();
                    dataGridView1.Rows.Add(date1.ToString("HH:mm:ss"), HRDataColumn[0], HRDataColumn[1], HRDataColumn[2], HRDataColumn[3]);
                }

                //Displays graph controls box to hide/show particular column data
                GraphControlsBox.Visible = true;

                //Displays the required buttons and boxes
                HRShowBtn.Checked = true;
                SpeedShowBtn.Checked = true;
                groupBox9.Visible = true;
                groupBox11.Visible = true;

                groupBox4.Visible = true;
                groupBox5.Visible = true;
                //groupBox6.Visible = true;

                label15.Visible = true;
                label16.Visible = true;
                ftpbox.Visible = true;
                tssbtn.Visible = true;
                labeltss.Visible = true;
                labelif.Visible = true;
                Calculatebtn.Visible = true;

            }
            //If the file contains data for version 1.06
            if (Versionval == 106)
            {
                //Finds all the different modes in the file
                String FindMode = "Mode";
                int indexofMode = filetext.IndexOf(FindMode);
                ModeTypeA = filetext.Substring(indexofMode + 5, 1);
                ModeTypeB = filetext.Substring(indexofMode + 6, 1);
                ModeTypeC = filetext.Substring(indexofMode + 7, 1);
                ModeTypeD = filetext.Substring(indexofMode + 8, 1);
                ModeTypeE = filetext.Substring(indexofMode + 9, 1);
                ModeTypeF = filetext.Substring(indexofMode + 10, 1);
                ModeTypeG = filetext.Substring(indexofMode + 11, 1);
                ModeTypeH = filetext.Substring(indexofMode + 12, 1);

                SpeedType = Int32.Parse(ModeTypeA);
                CadenceType = Int32.Parse(ModeTypeB);
                AltType = Int32.Parse(ModeTypeC);
                PowerType = Int32.Parse(ModeTypeD);
                PowerLRBType = Int32.Parse(ModeTypeE);
                PowerPIType = Int32.Parse(ModeTypeF);
                HRCCType = Int32.Parse(ModeTypeG);
                EuroUsType = Int32.Parse(ModeTypeH);

                //Sets the columns
                dataGridView1.Columns.Add("Column", "Cadence (RPM)");
                dataGridView1.Columns.Add("Column", "Altitude(M/FT)");
                dataGridView1.Columns.Add("Column", "Power(Watts)");
                dataGridView1.Columns.Add("Column", "Power Balance & Pedalling Index");

                //Adds the time for each column
                for (int i = 0; i < HRDatalines.Length; i++)
                {
                    date1 = date1.AddSeconds(intervalVal);
                    string[] HRDataColumn = Regex.Split(HRDatalines[i], "\t");
                    double speed123 = Int32.Parse(HRDataColumn[1]);
                    speed123 = speed123 / 10;
                    HRDataColumn[1] = speed123.ToString();
                    dataGridView1.Rows.Add(date1.ToString("HH:mm:ss"), HRDataColumn[0], HRDataColumn[1], HRDataColumn[2], HRDataColumn[3], HRDataColumn[4], HRDataColumn[5]);
                }

                Averages();
                //Displays graph controls box to hide/show particular column data
                GraphControlsBox.Visible = true;

                //Displays all the required buttons and boxes
                groupBox9.Visible = true;
                groupBox11.Visible = true;
                groupBox7.Visible = true;
                groupBox8.Visible = true;
                groupBox10.Visible = true;

                HRShowBtn.Checked = true;
                SpeedShowBtn.Checked = true;
                PowerShowBtn.Checked = true;
                RPMShowBtn.Checked = true;
                AltitudeShowBtn.Checked = true;

                groupBox4.Visible = true;
                groupBox5.Visible = true;
                groupBox3.Visible = true;
                groupBox6.Visible = true;

                label15.Visible = true;
                label16.Visible = true;
                ftpbox.Visible = true;
                tssbtn.Visible = true;
                labeltss.Visible = true;
                labelif.Visible = true;
                Calculatebtn.Visible = true;

            }
            //If the file contains data for version 1.07
            if (Versionval >= 107)
            {
                //Finds all the "modes" in the files
                String FindMode = "Mode";
                int indexofMode = filetext.IndexOf(FindMode);

                ModeTypeA = filetext.Substring(indexofMode + 5, 1);
                ModeTypeB = filetext.Substring(indexofMode + 6, 1);
                ModeTypeC = filetext.Substring(indexofMode + 7, 1);
                ModeTypeD = filetext.Substring(indexofMode + 8, 1);
                ModeTypeE = filetext.Substring(indexofMode + 9, 1);
                ModeTypeF = filetext.Substring(indexofMode + 10, 1);
                ModeTypeG = filetext.Substring(indexofMode + 11, 1);
                ModeTypeH = filetext.Substring(indexofMode + 12, 1);
                ModeTypeI = filetext.Substring(indexofMode + 13, 1);

                SpeedType = Int32.Parse(ModeTypeA);
                CadenceType = Int32.Parse(ModeTypeB);
                AltType = Int32.Parse(ModeTypeC);
                PowerType = Int32.Parse(ModeTypeD);
                PowerLRBType = Int32.Parse(ModeTypeE);
                PowerPIType = Int32.Parse(ModeTypeF);
                HRCCType = Int32.Parse(ModeTypeG);
                EuroUsType = Int32.Parse(ModeTypeH);
                AirPType = Int32.Parse(ModeTypeI);

                //Sets the columns
                dataGridView1.Columns.Add("Column", "Cadence (RPM)");
                dataGridView1.Columns.Add("Column", "Altitude(M/FT)");
                dataGridView1.Columns.Add("Column", "Power(Watts)");
                dataGridView1.Columns.Add("Column", "Power Balance & Pedalling Index");
                dataGridView1.Columns.Add("Column", "Air Pressure");

                //Adds time to each of the columns
                for (int i = 0; i < HRDatalines.Length; i++)
                {
                    date1 = date1.AddSeconds(intervalVal);
                    string[] HRDataColumn = Regex.Split(HRDatalines[i], "\t");
                    double speed123 = Int32.Parse(HRDataColumn[1]);
                    speed123 = speed123 / 10;
                    HRDataColumn[1] = speed123.ToString();
                    dataGridView1.Rows.Add(date1.ToString("HH:mm:ss"), HRDataColumn[0], HRDataColumn[1], HRDataColumn[2], HRDataColumn[3], HRDataColumn[4], HRDataColumn[5]);
                }
                Averages();
                //Displays graph controls box to hide/show particular column data
                GraphControlsBox.Visible = true;

                //Displays all the required buttons and boxes
                groupBox9.Visible = true;
                groupBox11.Visible = true;
                groupBox7.Visible = true;
                groupBox8.Visible = true;
                groupBox10.Visible = true;

                HRShowBtn.Checked = true;
                SpeedShowBtn.Checked = true;
                PowerShowBtn.Checked = true;
                RPMShowBtn.Checked = true;
                AltitudeShowBtn.Checked = true;

                groupBox4.Visible = true;
                groupBox5.Visible = true;
                groupBox3.Visible = true;
                groupBox6.Visible = true;

                label15.Visible = true;
                label16.Visible = true;
                ftpbox.Visible = true;
                tssbtn.Visible = true;
                labeltss.Visible = true;
                labelif.Visible = true;
                Calculatebtn.Visible = true;
            }


            AverageHR();

            AverageSpeed();
            //Plots the graph
            CreateGraph(zedGraphControl1);

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //////////////////////Pedal index/////////////////////////////////////
            left = pedindex = right = 0;
            Larray = new double[dataGridView1.Rows.Count];
            Rarray = new double[dataGridView1.Rows.Count];
            PIarray = new double[dataGridView1.Rows.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[6].Value != null)
                {

                    PowerB = Convert.ToInt64(dataGridView1.Rows[i].Cells[6].Value);
                    pedalBal(PowerB);
                    Larray[i] = lvalue;
                    left += lvalue;
                    Rarray[i] = rvalue;
                    right += rvalue;
                    PIarray[i] = pvalue;
                    pedindex += pvalue;

                }
            }
            rows = dataGridView1.Rows.Count;
            left = Math.Round(left, 2);
            double averLeft = left / rows;
            averLeft = Math.Round(averLeft, 2);
            PBLeft.Text = averLeft.ToString();

            right = Math.Round(right, 2);
            double averRight = right / rows;
            averRight = Math.Round(averRight, 2);
            PBRight.Text = averRight.ToString();

            pedindex = Math.Round(pedindex, 2);
            double averpedInd = pedindex / rows;
            averpedInd = Math.Round(averpedInd, 2);
            PILabel.Text = averpedInd.ToString();

        }



       
        public void AverageHR()
        {
            //Calculates average heart rate
            AvgHR = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                AvgHR += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            }
            AvgHR = AvgHR / dataGridView1.Rows.Count;

            //Calculates maximum heart rate
            MaxHR = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToInt32(r.Cells[1].Value));

            //Calculates maximum heart rate
            MinHR = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Min(r => Convert.ToInt32(r.Cells[1].Value));

            //Prints max,min and avg heart rate to the labels
            MaxHRLabel.Text = "Max: " + MaxHR.ToString("0");
            AvgHRLabel.Text = "Avg: " + AvgHR.ToString("0");
            MinHRLabel.Text = "Min: " + MinHR.ToString("0");
        }

        /// <summary>
        /// Calculates average speed and print the max and avg on the labels
        /// </summary>
        public void AverageSpeed()
        {
            AvgSP = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                AvgSP += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            AvgSP = AvgSP / dataGridView1.Rows.Count;

            MaxSP = dataGridView1.Rows.Cast<DataGridViewRow>()
                       .Max(r => Convert.ToDouble(r.Cells[2].Value));
            MaxSPLabel.Text = "Max: " + MaxSP.ToString("0");
            AvgSPLabel.Text = "Avg: " + AvgSP.ToString("0");

            //Calculates the total distance travelled
            Distance = ((AvgSP / 60) / 60) * FullLengthInSecs;
            if (mph.Checked == true) //if mph button is clicked, prints value in mph
            {
                DistanceLabel.Text = "Total Distance: " + Distance.ToString("0.00") + " MPH";
            }
            if (kmph.Checked == true) //if kmph button is clicked, prints value in kmph
            {
                DistanceLabel.Text = "Total Distance: " + Distance.ToString("0.00") + " KMPH";
            }
        }
        /// <summary>
        /// Calculates average power and altitude
        /// </summary>
        public void Averages()
        {
            //Calculates average power
            AvgPW = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                AvgPW += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            AvgPW = AvgPW / dataGridView1.Rows.Count;

            //Maximum power
            MaxPW = dataGridView1.Rows.Cast<DataGridViewRow>()
                      .Max(r => Convert.ToInt32(r.Cells[5].Value));
            //Minimum power
            MinPW = dataGridView1.Rows.Cast<DataGridViewRow>()
                       .Min(r => Convert.ToInt32(r.Cells[5].Value));

            //Prints max and avg power on the labels
            MaxPWLabel.Text = "Max: " + MaxPW.ToString("0");
            AvgPWLabel.Text = "Avg: " + AvgPW.ToString("0");


            //Calculates average altitude
            AvgALT = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                AvgALT += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            AvgALT = AvgALT / dataGridView1.Rows.Count;

            //Calculates maximum altitude
            MaxALT = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToInt32(r.Cells[4].Value));

            //Prints max and average altitude to the labels
            MaxAltLabel.Text = "Max: " + MaxALT.ToString("0");
            AvgAltLabel.Text = "Avg: " + AvgALT.ToString("0");

            ///// Calculating normalized power        
            List<int> initialPower = new List<int>();
            List<double> list = new List<double>();
            double total = 1;
            double average;

            try
            {
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    for (int n = 0; n < i + 30; n++)
                    {
                        initialPower.Add(Convert.ToInt32(dataGridView1.Rows[i + n].Cells[5].Value));
                    }
                    average = initialPower.Average();
                    total = Math.Pow(average, 4);
                    list.Add(total);
                    initialPower.Clear();
                }

            }
            catch (Exception)
            {
                average = list.Average();
                NP = Convert.ToInt32(Math.Pow(average, 1.0 / 4.00));
                labelnp.Text = NP.ToString("0 watts");
                labelnp.Visible = true;
            }
        }

       
        /// <summary>
        /// This methods creates the graph panel, sets the titles and plots the graph from data in the heart rate,
        /// speed, RPM, altitude and power columns.
        /// Plots only heart rate and speed for version 1.05
        /// </summary>
        /// <param name="graph"></param>
        private void CreateGraph(ZedGraphControl graph)
        {
            graph.GraphPane.CurveList.Clear();
            // get a reference to the GraphPane
            GraphPane = graph.GraphPane;

            // Set the Titles
            GraphPane.Title.Text = "Cycle Data Graph";
            GraphPane.XAxis.Title.Text = "Time In Seconds";
            GraphPane.YAxis.Title.Text = "Vaule";

            //inc = Convert.ToDouble(intervalbox.Text);

            //Creates data arrays based on the Sine function
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();

            
            for (int i = 0; i < dataGridView1.Rows.Count; i = i + 30)
            {
                x = i;
                y1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                y2 = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);

                list1.Add(x, y1);
                list2.Add(x, y2);
            }

            //If show button is selected, plots the heart rate graph in blue
            if (HRShowBtn.Checked == true)
            {
                LineItem curve;
                curve = GraphPane.AddCurve("Heart Rate", list1, Color.Blue);
                curve.Symbol.Fill = new Fill(Color.White);
                curve.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve.Line.IsSmooth = true;
                curve.Line.SmoothTension = 1F;
            }

            //If show button is selected, plots the speed graph in red
            if (SpeedShowBtn.Checked == true)
            {
                LineItem curve1;
                curve1 = GraphPane.AddCurve("Speed", list2, Color.Red);
                curve1.Symbol.Fill = new Fill(Color.White);
                curve1.Symbol.Size = 1;
                //Smoothes the cardinal spline 
                curve1.Line.IsSmooth = true;
                curve1.Line.SmoothTension = 1F;
            }

            //For files with version greater than 1.06, along with the previous graph, prints three additional lines
            if (Versionval >= 106)
            {
                Graphdata();
            }

            //Refreshes the axes due to change in data
            graph.AxisChange();
            graph.Invalidate();
            zedGraphControl1.Visible = true;
            //Get values from zoom event
            //graph.ZoomEvent += chart_ZoomEvent;
            intervalbox.Text = Convert.ToString(intcounter);
            FindIntervals();
            
        }

        /// <summary>
        /// This methods plots graphs from data in the RPM, Altitude and Power columns along with heart rate and speed
        /// from the previous method
        /// </summary>
        private void Graphdata()
        {
            double x, y3, y4, y5;
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            PointPairList list5 = new PointPairList();

                for (int ii = 0; ii < dataGridView1.Rows.Count; ii = ii + 30)
                {
                    x = ii;
                    y3 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[3].Value);
                    y4 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[4].Value);
                    y5 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[5].Value);

                    list3.Add(x, y3);
                    list4.Add(x, y4);
                    list5.Add(x, y5);
                }
            
            //If show button is selected, plots RPM graph in green
            if (RPMShowBtn.Checked == true)
            {
                LineItem curve2;
                curve2 = GraphPane.AddCurve("Cadence", list3, Color.Green);
                curve2.Symbol.Fill = new Fill(Color.White);
                curve2.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve2.Line.IsSmooth = true;
                curve2.Line.SmoothTension = 1F;
            }

            //If show button is selected, plots altitude graph in skyblue
            if (AltitudeShowBtn.Checked == true)
            {
                LineItem curve3;
                curve3 = GraphPane.AddCurve("Altitude", list4, Color.SkyBlue);
                curve3.Symbol.Fill = new Fill(Color.White);
                curve3.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve3.Line.IsSmooth = true;
                curve3.Line.SmoothTension = 1F;
            }

            //If show button is selected, plots power graph in dark orange
            if (PowerShowBtn.Checked == true)
            {
                LineItem curve4;
                curve4 = GraphPane.AddCurve("Power", list5, Color.DarkOrange);
                curve4.Symbol.Fill = new Fill(Color.White);
                curve4.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve4.Line.IsSmooth = true;
                curve4.Line.SmoothTension = 1F;
            }
            intervalbox.Text = Convert.ToString(intcounter);
            FindIntervals();
            string inc;
            inc = Convert.ToString(intervalbox.Text);
            x = 0;
            //GraphPane.XAxis.Scale.Max = x + 20;
            x = x + Convert.ToDouble(inc);
        }

        /// <summary>
        /// Refreshes the graph
        /// </summary>
        public void UpdateGraph()
        {
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();
        }

        /// <summary>
        /// Displays all the speed units in kmph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kmph_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value); ;
                value = (value / 0.621371);
                string speed = value.ToString();
                dataGridView1[2, i].Value = value.ToString("0.00");
                dataGridView1.Columns[2].HeaderCell.Value = "Speed (KMPH)";
            }
            AverageSpeed();
            CreateGraph(zedGraphControl1); //Plots the graph (with new data)
        }

        /// <summary>
        /// Displays all the speed units in mph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mph_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value); ;
                value = (value * 0.621371);
                string speed = value.ToString();
                dataGridView1[2, i].Value = value.ToString("0.00");
                dataGridView1.Columns[2].HeaderCell.Value = "Speed (MPH)";
            }
            AverageSpeed();
            CreateGraph(zedGraphControl1); //Plots the graph (with new data)
        }

        /// <summary>
        /// Quits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// If show button is selected, plots heart rate graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HRShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        /// <summary>
        /// If show button is selected, plots speed graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedShowBtn_CheckedChanged_1(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        /// <summary>
        /// If show button is selected, plots power graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        /// <summary>
        /// If show button is selected, plots RPM graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RPMShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        /// <summary>
        /// If show button is selected, plots altitude graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AltitudeShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string content = richTextBox1.Text;
                string path = @"F:\Visual-Studio-2015\Projects\CycleDataAnalysis-ASEB-c3442857\CycleDataAnalysis\" + DateDay + "-" + DateMonth + "-" + DateYear + ".hrm";
                File.WriteAllText(path, content);
                MessageBox.Show("Your day has been saved!");
            }
            catch (Exception etc)
            {
                MessageBox.Show("An error Ocurred: " + etc.Message);
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }


        /// <summary>
        /// Loads file from date selected on calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                var streamReader = new StreamReader(@"F:\Visual-Studio-2015\Projects\CycleDataAnalysis-ASEB-c3442857\CycleDataAnalysis\" + monthCalendar1.SelectionRange.Start.ToString("dd-MM-yyyy") + ".hrm", Encoding.UTF8);
                filetext = streamReader.ReadToEnd();
                richTextBox1.Text = filetext;
                dataGridView1.Rows.Clear();
                filehead();
            }
            catch (Exception etc)
            {
                MessageBox.Show("An error Ocurred: " + etc.Message);
            }
            monthCalendar1.Visible = false;
            
            zedGraphControl1.Refresh();
        }

    
        
        private void Calculatebtn_Click_1(object sender, EventArgs e)
        {
            SelectableData();
        }

        /// <summary>
        /// Displays updated information and graph of the selected data from the table
        /// </summary>
        private void SelectableData()
        {

            Int32 selectedRowCount =
         dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            Double AvgSP1 = 0;
            for (int i = 0; i < selectedRowCount; ++i)
            {
                AvgSP1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            AvgSP1 = AvgSP1 / selectedRowCount;

            Double MaxSP1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToDouble(r.Cells[2].Value));

            Double MinSP1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Min(r => Convert.ToDouble(r.Cells[2].Value));
            newgraph.groupBox14.Visible = true;
            newgraph.label7.Text = "Max: " + MaxSP1.ToString("0.0");
            newgraph.label6.Text = "Avg: " + AvgSP1.ToString("0.0");
            newgraph.label11.Text = "Min: " + MinSP1.ToString("0.0");

            Double AvgHR1 = 0;
            for (int i = 0; i < selectedRowCount; ++i)
            {
                AvgHR1 += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[1].Value);
            }
            AvgHR1 = AvgHR1 / selectedRowCount;

            /////////////////////
            Double MaxHR1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToInt32(r.Cells[1].Value));

            Double MinHR1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Min(r => Convert.ToInt32(r.Cells[1].Value));

            newgraph.groupBox15.Visible = true;
            newgraph.label10.Text = "Max: " + MaxHR1.ToString("0");
            newgraph.label9.Text = "Avg: " + AvgHR1.ToString("0");
            newgraph.label8.Text = "Min: " + MinHR1.ToString("0");

            if (Versionval >= 106)
            {
                Double AvgPW1 = 0;
                for (int i = 0; i < selectedRowCount; ++i)
                {
                    AvgPW1 += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                }
                AvgPW1 = AvgPW1 / selectedRowCount;
                /////////////////////
                Double AvgALT1 = 0;
                for (int i = 0; i < selectedRowCount; ++i)
                {
                    AvgALT1 += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                }
                AvgALT1 = AvgALT1 / selectedRowCount;

                Double MaxPW1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                          .Max(r => Convert.ToInt32(r.Cells[5].Value));
                Double MaxALT1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                            .Max(r => Convert.ToInt32(r.Cells[4].Value));

                Double MinPW1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                            .Min(r => Convert.ToInt32(r.Cells[5].Value));
                Double MinALT1 = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                            .Min(r => Convert.ToInt32(r.Cells[4].Value));

                newgraph.groupBox12.Visible = true;
                newgraph.label3.Text = "Max: " + MaxPW1.ToString("0.0");
                newgraph.label2.Text = "Avg: " + AvgPW1.ToString("0.0");
                newgraph.label13.Text = "Min: " + MinPW1.ToString("0.0");

                newgraph.groupBox13.Visible = true;
                newgraph.label5.Text = "Max: " + MaxALT1.ToString("0");
                newgraph.label4.Text = "Avg: " + AvgALT1.ToString("0");
                newgraph.label12.Text = "Min: " + MinALT1.ToString("0");

                ///// Calculating normalized power for new graph        
                List<int> initialPower1 = new List<int>();
                List<double> list1 = new List<double>();
                double total1 = 1;
                double average1;

                try
                {
                    for (int ii = 1; ii < dataGridView1.SelectedRows.Count; ii++)
                    {
                        for (int n = 0; n < ii + 30; n++)
                        {
                            initialPower1.Add(Convert.ToInt32(dataGridView1.Rows[ii + n].Cells[5].Value));
                        }
                        average1 = initialPower1.Average();
                        total1 = Math.Pow(average1, 4.00);
                        list1.Add(total1);
                        initialPower1.Clear();
                    }

                }
                catch (Exception)
                {
                    average1 = list1.Average();
                    NP1 = Convert.ToInt32(Math.Pow(average1, 1.0 / 4.00));
                    newgraph.np2.Text = NP1.ToString("0 watts");
                    newgraph.np2.Visible = true;
                }


                //////////////////////Selectable Data - Pedal index/////////////////////////////////////
                left = pedindex = right = 0;
                Larray = new double[dataGridView1.SelectedRows.Count];
                Rarray = new double[dataGridView1.SelectedRows.Count];
                PIarray = new double[dataGridView1.SelectedRows.Count];
                for (int i = 0; i < dataGridView1.SelectedRows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[6].Value != null)
                    {

                        PowerB = Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[6].Value);
                        pedalBal(PowerB);
                        Larray[i] = lvalue;
                        left += lvalue;
                        Rarray[i] = rvalue;
                        right += rvalue;
                        PIarray[i] = pvalue;
                        pedindex += pvalue;

                    }
                }
                rows = dataGridView1.SelectedRows.Count;
                left = Math.Round(left, 2);
                double averLeft = left / rows;
                averLeft = Math.Round(averLeft, 2);
                newgraph.PBLeft2.Text = averLeft.ToString();

                right = Math.Round(right, 2);
                double averRight = right / rows;
                averRight = Math.Round(averRight, 2);
                newgraph.PBRight2.Text = averRight.ToString();

                pedindex = Math.Round(pedindex, 2);
                double averpedInd = pedindex / rows;
                averpedInd = Math.Round(averpedInd, 2);
                newgraph.PILabel2.Text = averpedInd.ToString();

        }
            CreateGraph2(newgraph.zedGraphControl11);
            newgraph.Show();

        }

        /// <summary>
        /// Draws the new pop-up window graph with updated data
        /// </summary>
        /// <param name="graph2"></param>
        private void CreateGraph2(ZedGraphControl graph2)
        {
            graph2.GraphPane.CurveList.Clear();
            // get a reference to the GraphPane
            GraphPane2 = graph2.GraphPane;
            // Set the Titles
            GraphPane2.Title.Text = "Cycle Data Graph";
            GraphPane2.YAxis.Title.Text = "Value";

            double x, y1, y2, y3, y4, y5;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            PointPairList list5 = new PointPairList();

            x = 0;

            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {

                x = i + intervalVal;
                GraphPane2.XAxis.Title.Text = "Time In Seconds";
                y1 = Convert.ToDouble(dataGridView1.SelectedRows[i].Cells[1].Value);
                y2 = Convert.ToDouble(dataGridView1.SelectedRows[i].Cells[2].Value);
                list1.Add(x, y1);
                list2.Add(x, y2);
            }


            if (HRShowBtn.Checked == true)
            {
                LineItem curve;
                curve = GraphPane2.AddCurve("Heart Rate", list1, Color.Blue, SymbolType.None);
                curve.Line.Width = 0.1F;
                curve.Symbol.Fill = new Fill(Color.White);
                curve.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve.Line.IsSmooth = true;
                curve.Line.SmoothTension = 1F;
            }
            if (SpeedShowBtn.Checked == true)
            {
                LineItem curve1;
                curve1 = GraphPane2.AddCurve("Speed", list2, Color.Red, SymbolType.None);
                curve1.Line.Width = 0.1F;
                curve1.Symbol.Fill = new Fill(Color.White);
                curve1.Symbol.Size = 1;
                //Smoothes the cardinal spline
                curve1.Line.IsSmooth = true;
                curve1.Line.SmoothTension = 1F;
            }


            //if the version is over 1.06 then run the extened Graph Method
            if (Versionval >= 106)
            {

                for (int ii = 0; ii < dataGridView1.SelectedRows.Count; ii++)
                {
                    x = ii + intervalVal;
                    y3 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[3].Value);
                    y4 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[4].Value);
                    y5 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[5].Value);

                    list3.Add(x, y3);
                    list4.Add(x, y4);
                    list5.Add(x, y5);


                }
                if (RPMShowBtn.Checked == true)
                {
                    LineItem curve2;
                    curve2 = GraphPane2.AddCurve("Cadence", list3, Color.Green, SymbolType.None);
                    curve2.Line.Width = 0.1F;
                    curve2.Symbol.Fill = new Fill(Color.White);
                    curve2.Symbol.Size = 1;
                    //Smoothes the cardinal spline
                    curve2.Line.IsSmooth = true;
                    curve2.Line.SmoothTension = 1F;
                }
                if (AltitudeShowBtn.Checked == true)
                {
                    LineItem curve3;
                    curve3 = GraphPane2.AddCurve("Altitude", list4, Color.SkyBlue, SymbolType.None);
                    curve3.Line.Width = 0.1F;
                    curve3.Symbol.Fill = new Fill(Color.White);
                    curve3.Symbol.Size = 1;
                    //Smoothes the cardinal spline
                    curve3.Line.IsSmooth = true;
                    curve3.Line.SmoothTension = 1F;
                }
                if (PowerShowBtn.Checked == true)
                {
                    LineItem curve4;
                    curve4 = GraphPane2.AddCurve("Power", list5, Color.DarkOrange, SymbolType.None);
                    curve4.Line.Width = 0.1F;
                    curve4.Symbol.Fill = new Fill(Color.White);
                    curve4.Symbol.Size = 1;
                    //Smoothes the cardinal spline
                    curve4.Line.IsSmooth = true;
                    curve4.Line.SmoothTension = 1F;
                }
            }

            graph2.AxisChange();
            graph2.Invalidate();
        }

        private void tssbtn_Click(object sender, EventArgs e)
        {
            IntensityFactor();
            TSS();
        }

        /// <summary>
        /// Toggles intervals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalfwd_Click(object sender, EventArgs e)
        {
            intcounter++;
            if (intcounter > 10)
            {
                intcounter = 10;
            }
            intervalpicker();
        }

        private void intervalbck_Click(object sender, EventArgs e)
        {
            intcounter--;
            if (intcounter == 0 || intcounter < 0)
            {
                intcounter = 1;
            }
            intervalpicker();
        }

        /// <summary>
        /// Calculates Intensity Factor
        /// </summary>
        private void IntensityFactor() {
        
            FTP = Convert.ToInt32(ftpbox.Text);
            IF = NP / FTP;
            labelif.Text = IF.ToString("0.00");
            labelif.Visible = true;
            label15.Visible = true;
        }

        /// <summary>
        /// Calculate Training Stress Score
        /// </summary>
        private void TSS() {
            double TSS;
            TSS = (FullLengthInSecs * NP * IF) / (FTP * 3600) * 100;
            labeltss.Text = TSS.ToString("0.00");
            labeltss.Visible = true;
        }


        /// <summary>
        /// Detects intervals
        /// </summary>
        private void FindIntervals()
        {
            bool Over = false;
            int loc = 0;
            int timer = 0;
            int power;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                //Finds row number where is power over 150
                power = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
                if (power > 150 && Over == false)
                {
                    if (loc < 20)
                    {
                        intervals[loc] = i;
                        loc++;
                        Over = true;
                    }
                }
                if (power < 150 && Over == true)
                {
                    if (timer > 29)
                    {
                        intervals[loc] = i;
                    }
                    if (timer < 29)
                    {
                        loc = loc - 2;
                    }
                    loc++;
                    Over = false;
                    timer = 0;
                }
                if (Over)
                {
                    timer++;
                }
            }
        }


        private void intervalpicker()
        {
            int high = 0;
            int low = 0;
            switch (intcounter)
            {
                case 1:
                    low = intervals[0];
                    high = intervals[1];
                    break;
                case 2:
                    low = intervals[2];
                    high = intervals[3];

                    break;
                case 3:
                    low = intervals[4];
                    high = intervals[5];
                    break;
                case 4:
                    low = intervals[6];
                    high = intervals[7];
                    break;
                case 5:
                    low = intervals[8];
                    high = intervals[9];
                    break;

                case 6:
                    low = intervals[10];
                    high = intervals[11];
                    break;
                case 7:
                    low = intervals[12];
                    high = intervals[13];
                    break;
                case 8:
                    low = intervals[14];
                    high = intervals[15];
                    break;
                case 9:
                    low = intervals[16];
                    high = intervals[17];
                    break;
                case 10:
                    low = intervals[18];
                    high = intervals[19];

                    break;
                default:
                    break;
            }
            zedGraphControl1.GraphPane.XAxis.Scale.Min = Convert.ToDouble(low);
            zedGraphControl1.GraphPane.XAxis.Scale.Max = Convert.ToDouble(high);
            zedGraphControl1.Refresh();

            intervalbox.Text = Convert.ToString(intcounter);

        }

        /// <summary>
        /// For pedal balance unit test
        /// </summary>
        /// <param name="value"></param>
        public void pedalBal(long value)
        {
            PBbinary = Convert.ToString(value, 2).PadLeft(16, '0');
            leftMask = PowerB & 255;
            leftBin = Convert.ToString(leftMask, 2).PadLeft(16, '0');
            lvalue = Convert.ToInt32(leftBin, 2);
            rvalue = 100 - lvalue;
            PedIndMask = value & 65280;
            PedIndMask = PedIndMask >> 8;
            PiBin = Convert.ToString(PedIndMask, 2).PadLeft(16, '0');
            pvalue = Convert.ToInt32(PiBin, 2);

        }

        /// <summary>
        /// For Power balance unit test
        /// </summary>
        /// <param name="averRight"></param>
        /// <param name="averLeft"></param>
        public void PowerBalTest(double averRight, double averLeft)
        {

            if (averRight + averLeft != 100)
            {
                result = "Power Balance Wrong";
            }
            else if (averRight + averLeft == 100)
            {
                result = "Power Balance Values correct";
            }
        }

        /// <summary>
        /// For speed unit test
        /// </summary>
        /// <param name="testSpeed"></param>
        /// <param name="testRows"></param>
        public void SpeedTest(double testSpeed, double testRows)
        {

            // calulates average speed, total of all speed is divided by rows. All of this is divided by 10 to calculate Kmph
            AvgSP = (testSpeed / testRows) / 10;
            
            // converts equation to miles, if miles button is cheked
            if (EuroUsType == 1)
            {
                AvgSP = AvgSP * 0.621371192;
            }

            //rounded to 2 decimal places
            AvgSP = Math.Round(AvgSP, 2);
            AvgSPLabel.Text = "Average Speed: " + AvgSP.ToString();
        }

    }

} 