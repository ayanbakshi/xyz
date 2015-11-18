using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace ExcelInterface
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
           

        }

       

        int topnumber;
        string Mstring="male", Fstring="female";

        MainCode obj = new MainCode();
       
       


        private void btnsearch_Click(object sender, EventArgs e)
        {

            if (Mstring!=null)
            {
                Mstring += comboBox1.Text.ToString();
            }
            if (Fstring != null)
            {
                Fstring += comboBox1.Text.ToString();
            }
            topnumber=Convert.ToInt32(textBox1.Text);

           
            DataSet ds = obj.myFilter(Mstring,Fstring,topnumber);
            Mstring = "male"; Fstring = "female";
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void MainMenuCheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Enabled = true; dataGridView1.Enabled=true;
                dataGridView1.Visible = true;
                panel1.Visible = true;
                panel2.Visible = false;
                panel2.Enabled = false;
                label4.Enabled = false;
                label4.Visible = false;
                comboBox2.Enabled = false;
                comboBox2.Visible = false;
                chart1.Enabled = false;
                chart1.Visible = false;
                chart2.Enabled = true;
                chart2.Visible = false;
            }
            else if (radioButton2.Checked)
            {
                panel1.Enabled = false;
                panel1.Visible = false;
                dataGridView1.Enabled = false;
                dataGridView1.Visible = false;
                panel2.Visible = true;
                panel2.Enabled = true;
                label4.Enabled = true;
                
                
            }
        }

        private void btnchart_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtyear.Text) <= 2013 && Convert.ToInt32(txtyear.Text) >= 1944)
            {
                comboBox2.Enabled = true;
                comboBox2.Visible = true;
                string Mstring1 = Mstring, Fstring1 = Fstring;
                if (Mstring != null)
                {
                    Mstring1 += txtyear.Text;
                }
                if (Fstring != null)
                {
                    Fstring1 += txtyear.Text;
                }



                DataSet ds = obj.nameSearch(Mstring1, Fstring1, txtname.Text);
                comboBox2.DataSource = ds.Tables[0];
                comboBox2.DisplayMember = "Given_Name";
                comboBox2.ValueMember = "Given_Name";
                label4.Visible = true;
                if (comboBox2.Items.Count == 0)
                {
                    MessageBox.Show("No Valid Entry In Database.Try A Different Search.");
                }
            }
            else
            {
                MessageBox.Show("Use 1994 to 2013 as valid years");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string name = comboBox2.SelectedValue.ToString();
            chart1.Enabled = true;
            chart1.Visible = true;
            DataSet ds2 = new DataSet();
            DataSet ds=obj.chartplot(name, Convert.ToInt32(txtyear.Text), Mstring, Fstring,ref ds2);
            chart1.Series.Clear();
            if(Mstring!=null && Fstring!=null)
            {
                chart2.Enabled = true;
                chart2.Visible = true;
                chart2.Height =275;
                chart2.Width =528;
                chart2.Series.Clear(); chart2.Series.Add("Male");
                chart2.DataSource = ds.Tables["Male"];
                
                chart2.Series["Male"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart2.Series["Male"].ChartArea = "ChartArea1";
                chart2.Series["Male"].XValueMember = ds.Tables["Male"].Columns["Year"].ColumnName.ToString();
                chart2.Series["Male"].YValueMembers = ds.Tables["Male"].Columns["Position"].ColumnName.ToString();
                chart2.DataBind();
                chart1.Height =260;
                chart1.Width =528;
                chart1.DataSource = ds2.Tables["Female"];
                chart1.Series.Add("female");
               
                chart1.Series["female"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart1.Series["female"].ChartArea = "ChartArea1";

                chart1.Series["female"].XValueMember = ds2.Tables["female"].Columns["Year"].ColumnName.ToString();
                chart1.Series["female"].YValueMembers = ds2.Tables["female"].Columns["Position"].ColumnName.ToString();
                chart1.DataBind();
            }
            else if (Mstring != null)
            {
                chart1.Height = 380;
                chart1.Width = 528;
                chart2.Enabled = false;
                chart2.Visible = false;
                chart1.DataSource = ds.Tables["Male"];
                chart1.Series.Add("Male");
                chart1.Series["Male"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart1.Series["Male"].ChartArea = "ChartArea1";
                chart1.Series["Male"].XValueMember = ds.Tables["Male"].Columns["Year"].ColumnName.ToString();
                chart1.Series["Male"].YValueMembers= ds.Tables["Male"].Columns["Position"].ColumnName.ToString();
                chart1.DataBind();
            }
            else if (Fstring != null)
            {
                chart1.Height = 380;
                chart1.Width = 528;
                chart2.Enabled = false;
                chart2.Visible = false;
                chart1.DataSource = ds2.Tables["Female"];
                chart1.Series.Add("female");
                chart1.Series["female"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart1.Series["female"].ChartArea = "ChartArea1";

                chart1.Series["female"].XValueMember = ds2.Tables["female"].Columns["Year"].ColumnName.ToString();
                chart1.Series["female"].YValueMembers = ds2.Tables["female"].Columns["Position"].ColumnName.ToString();
                chart1.DataBind();
            }

            


        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MaleRButton.Checked)
            {
                Mstring = "male";
                Fstring = null;
            }
            else if (FemaleRButton.Checked)
            {
                Mstring = null;
                Fstring = "female";
            }
            else
            {
                Mstring = "male";
                Fstring = "female";
            }
        }




    }

}
