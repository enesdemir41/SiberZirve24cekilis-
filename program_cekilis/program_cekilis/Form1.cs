using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.IO;
using ExcelDataReader;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace program_cekilis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ArrayList katilimcilar = new ArrayList();                   
        string katilimciismi;                                       
        int yedekno;                                                
        int kazananno;                                              
        Random rastgelesayi = new Random();                        
        int kackisi;                                                
        int talihli;                                                
        ArrayList yedekKazananlar = new ArrayList();                
        string cikis;                                               


        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            ıconButton2.Visible = false;
            listBox2.Visible = false;
            label2.Visible = false;
            label6.Visible = false;
            listBox3.Visible = false;
            label5.Visible = false;
            textBox2.Visible = false;
            ıconButton2.Visible = false;






        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void ıconButton1_Click(object sender, EventArgs e)
        {
            try
            {


                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = openFileDialog.FileName;
                        using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });

                                DataTable table = result.Tables[0]; 

                                foreach (DataRow row in table.Rows)
                                {
                                    string katilimci = row[1].ToString(); 
                                    katilimcilar.Add(katilimci);
                                    listBox1.Items.Add(katilimci);
                                    ıconButton2.Visible = true;
                                }
                            }
                        }
                    }
                }
                label8.Text = katilimcilar.Count.ToString();






            }
            catch (FormatException)
            {
                MessageBox.Show("Geçerli Formatta Sayı giriniz");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Girilen Tanımlı aralıkta değil");
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Belirtilenden fazla giriş yapıldı");
            }



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label5.Visible = true;
            textBox2.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
            textBox2.Visible = false;
        }

        private void ıconButton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (radioButton1.Checked) 
                {

                    kackisi = int.Parse(textBox1.Text); 
                    talihli = int.Parse(textBox2.Text); 
                    string[] yedekler = new string[talihli]; 
                    string[] kazananlar = new string[kackisi];

                    if (talihli > kackisi) 
                    {
                        MessageBox.Show("Yedek Talihliler kazanacak sayısından fazla olamaz", "Uyarı", MessageBoxButtons.OK);
                    }
                    else   
                    {
                        for (int i = 0; i <= kackisi - 1; i++) 
                        {

                            kazananno = rastgelesayi.Next(0, katilimcilar.Count);
                            kazananlar[i] = katilimcilar[kazananno].ToString();
                            katilimcilar.RemoveAt(kazananno);
                            listBox2.Items.Add(kazananlar[i]);



                        }






                        for (int i = 0; i < talihli; i++)  
                        {

                            

                            yedekno = rastgelesayi.Next(0, katilimcilar.Count);
                            yedekler[i] = katilimcilar[yedekno].ToString();    
                            //katilimcilar.RemoveAt(yedekno);                    
                            listBox3.Items.Add(yedekler[i]);                   

                        }

                       
                        ıconButton2.Visible = true;
                    }


                }
                else 
                {

                    kackisi = int.Parse(textBox1.Text);
                    string[] kazananlar = new string[kackisi];
                    for (int i = 0; i <= kackisi - 1; i++)
                    {

                        kazananno = rastgelesayi.Next(0, katilimcilar.Count);
                        kazananlar[i] = katilimcilar[kazananno].ToString();
                        katilimcilar.RemoveAt(kazananno);
                        listBox2.Items.Add(kazananlar[i]);


                    }

                   

                    ıconButton2.Visible = true;

                }

                label8.Text = katilimcilar.Count.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Geçerli Formatta Sayı giriniz");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Girilen Tanımlı aralıkta değil");
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Belirtilenden fazla giriş yapıldı");
            }

        }

        private void ıconButton3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }
    }
}
