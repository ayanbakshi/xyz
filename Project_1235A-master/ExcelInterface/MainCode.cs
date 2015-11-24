using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace ExcelInterface
{
    class MainCode
    {
        

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetDirectoryName(Application.ExecutablePath)+"\\BabyNames.xlsx;Extended Properties=\"Excel 8.0;HDR={1}\"");

        

        public DataSet myFilter(string Mstring, string Fstring, int topnumber)
        {
            
            con.Open();
           
            DataSet objdata= new DataSet();
            try
            {
                OleDbCommand objcommand;
                if (Fstring != null && Mstring != null)
                {
                    objcommand = new OleDbCommand("SELECT TOP " + topnumber + " * FROM (SELECT [Given_Name],[Amount] FROM " + Fstring + " UNION ALL SELECT [Given_Name],[Amount] FROM " + Mstring + " ORDER BY [Amount] DESC) ", con);

                }
                else
                {
                    objcommand = new OleDbCommand("SELECT TOP " + topnumber + " [Given_Name],[Amount] FROM " + Fstring + Mstring + " ORDER BY [Amount] DESC", con);
                }
                OleDbDataAdapter objadapter = new OleDbDataAdapter();
                objadapter.SelectCommand = objcommand;
                
                objadapter.Fill(objdata);

            }
            catch (Exception e)
            {
                MessageBox.Show("Too high number in TOP ENTRIES section.Try a smaller number like 10, 100,1000");
            }
            con.Close();
            return objdata;

        }

        public DataSet nameSearch(string Mstring, string Fstring, string name)
        {
           
            con.Open();
            DataSet objdata = new DataSet();
            try
            {
                OleDbCommand namesearch;
                if (Fstring != null && Mstring != null)
                {
                    namesearch = new OleDbCommand("SELECT [Given_Name] FROM (SELECT a.[Given_name] FROM " + Fstring + " a," + Mstring + " b WHERE a.[Given_Name]=b.[Given_Name]) WHERE [Given_Name] LIKE \'%" + name + "%\'", con);
                }
                else
                {
                    namesearch = new OleDbCommand("SELECT [Given_Name] FROM " + Fstring + Mstring + " WHERE [Given_Name] LIKE \'%" + name + "%\'", con);
                }
                OleDbDataAdapter objadapter = new OleDbDataAdapter();
                objadapter.SelectCommand = namesearch;
                
                objadapter.Fill(objdata);
            }
            catch (Exception e)
            {
                MessageBox.Show("Use Only Captital Letters For NAME.");
            }
            con.Close();
            return objdata;

        }

        public DataSet chartplot(string name,int year,string Mstring,string Fstring,ref DataSet ds2 )
        {
            
            DataSet objdata = new DataSet("ChartSheet");
            DataTable maletable = objdata.Tables.Add("Male");
            DataTable femaletable = ds2.Tables.Add("Female");
            maletable.Columns.Add("Position", typeof(Int32));
            maletable.Columns.Add("Year", typeof(Int32));
            femaletable.Columns.Add("Position", typeof(Int32));
            femaletable.Columns.Add("Year", typeof(Int32));
            con.Open();
            OleDbCommand namesearch;
            if (Mstring != null)
            {

                string ms;
                for (int i = year,k=0; i <= 2013; k++,i++)
                {
                    ms = Mstring;
                    namesearch = new OleDbCommand(" SELECT [Position] FROM " + ms + i.ToString() + " WHERE [Given_Name]=\'" + name + "\'", con);
                    OleDbDataAdapter objadapter = new OleDbDataAdapter();
                    objadapter.SelectCommand = namesearch;
                    
                    objadapter.Fill(objdata.Tables["Male"]);
                    int c = objdata.Tables["Male"].Rows.Count;
                    if(c!=0)
                    objdata.Tables["Male"].Rows[c - 1]["Year"] = i;
                   
                } 
            }
            if (Fstring != null)
            {

                string fs;
                for (int i = year, k = 0; i <= 2013; k++, i++)
                {
                    fs = Fstring;
                    namesearch = new OleDbCommand(" SELECT [Position] FROM " + fs + i.ToString() + " WHERE [Given_Name]=\'" + name + "\'", con);
                    OleDbDataAdapter objadapter2 = new OleDbDataAdapter();
                    objadapter2.SelectCommand = namesearch;

                    objadapter2.Fill(ds2.Tables["Female"]);
                    int c = ds2.Tables["Female"].Rows.Count;
                    if(c!=0)
                    ds2.Tables["Female"].Rows[c - 1]["Year"] = i;

                }
            }
            con.Close();
            return objdata;


        }
    }

        
    
}
