using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace SoSoManyStrPut
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string StrucList;
        int StaDist = 0;
        int Endist = 0;
        string StName;
        int RepDist = 0;
        int SameStr = 0;
        int SameStrD = 0;
        string Sent;
        private void DropB(object sender, RoutedEventArgs e)
        {
            try
            {
                StrucList = StrList.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                StaDist = Convert.ToInt32(StartD.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                Endist = Convert.ToInt32(EndD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                StName = StrName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                RepDist = Convert.ToInt32(Kank.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                SameStr = Convert.ToInt32(SameS.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            try
            {
                SameStrD = Convert.ToInt32(SamesD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            if(StaDist==Endist||RepDist==0||(Endist - StaDist>0&&RepDist<0)|| (Endist - StaDist < 0 && RepDist > 0) || SameStrD <= 0 || SameStr < 0)
            {
                MessageBox.Show("数値を確認してください");
                goto retry;
            }
            Sent = "BveTs Map 2.02:shift_jis\nStructure.Load('";
            Sent = Sent + StrucList + "');\n";
            if (Kaigyo.IsChecked == true)
            {
                for (double i = StaDist; i <= Endist; i = i + RepDist)
                {
                    string Dist = i.ToString() + ";\n";
                    string Str="";
                    for(double s = 0; s <= SameStr * SameStrD; s = s + SameStrD)
                    {
                        Str = Str + StrPut(s) + "\n";
                    }
                    Sent = Sent + Dist + Str;
                }
            }
            else
            {
                for (double i = StaDist; i <= Endist; i = i + RepDist)
                {
                    string Dist = i.ToString() + "; ";
                    string Str = "";
                    for (double s = 0; s <= SameStr * SameStrD; s = s + SameStrD)
                    {
                        Str = Str + StrPut(s) + " ";
                    }
                    Sent = Sent + Dist + Str + "\n";
                }
            }
            //参考:http://dobon.net/vb/dotnet/file/writefile.html
            try
            {
                StreamWriter s = new StreamWriter("map.txt", false, Encoding.GetEncoding("shift_jis"));
                s.Write(Sent);
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto retry;
            }
            MessageBox.Show("完了");
            retry:
            return;
            
        }
        string StrPut(double x)
        {
            string Nm = 
                "Structure['"+
                StName+
                "'].Put(0, "+
                x.ToString()+
                ", 0, 0, 0, 0, 0, "+
                RepDist.ToString()+
                ", "+
                RepDist.ToString()+
                ");";
            return Nm;
        }

        
    }
}
