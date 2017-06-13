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

namespace TimblMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StringBuilder> results;
        public MainWindow()
        {
            InitializeComponent();
            results = new List<StringBuilder>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (
            MultiVarCheckedDetect(new List<CheckBox> { VarCheckbox1, VarCheckbox2, VarCheckbox3, VarCheckbox4, VarCheckbox5, VarCheckbox6, VarCheckbox7, VarCheckbox8, VarCheckbox9 }))
            {
                MessageBox.Show("Multi Var Checkbox checked.");
                return;
            }

            results.Clear();

            string baseCommand = CommandBox.Text + " " + FileParameterBox1.Text + " " + FileNameBox1.Text + " " + FileParameterBox2.Text + " " + FileNameBox2.Text + " ";

            results.Add(new StringBuilder(baseCommand));

            // -a
            IncreaseIntVarOperation(ParameterCheckbox1, VarCheckbox1, ParameterBox1Max, ParameterBox1Min, ParameterBox1);
            // -m
            MultiCahrVarOperation(ParameterCheckbox2, VarCheckbox2, ParameterBox2List, ParameterBox2);
            // -w
            IncreaseIntVarOperation(ParameterCheckbox3, VarCheckbox3, ParameterBox3Max, ParameterBox3Min, ParameterBox3);
            // -k
            IncreaseIntVarOperation(ParameterCheckbox4, VarCheckbox4, ParameterBox4Max, ParameterBox4Min, ParameterBox4);
            // -d
            MultiCahrVarOperation(ParameterCheckbox5, VarCheckbox5, ParameterBox5List, ParameterBox5);
            // -L
            IncreaseIntVarOperation(ParameterCheckbox6, VarCheckbox6, ParameterBox6Max, ParameterBox6Min, ParameterBox6);
            // -b
            IncreaseIntVarOperation(ParameterCheckbox7, VarCheckbox7, ParameterBox7Max, ParameterBox7Min, ParameterBox7);
            // -q
            IncreaseIntVarOperation(ParameterCheckbox8, VarCheckbox8, ParameterBox8Max, ParameterBox8Min, ParameterBox8);
            // -R
            IncreaseIntVarOperation(ParameterCheckbox9, VarCheckbox9, ParameterBox9Max, ParameterBox9Min, ParameterBox9);

            if (ResultFilterCheckbox.IsChecked == true)
            {
                ResultsAppend(ResultFilterTextbox.Text);
            }

            ResultBox.Text = "";
            foreach (var command in results)
            {
                ResultBox.Text += command.ToString() + "\n";
            }

            if (CopyToClipCheckbox.IsChecked == true)
            {
                Clipboard.SetText(ResultBox.Text);
            }
        }

        private bool MultiVarCheckedDetect(List<CheckBox> list)
        {
            int isCheckedSum = 0;
            foreach (var item in list)
            {
                if (item.IsChecked == true)
                {
                    isCheckedSum += 1;
                }
            }
            if (isCheckedSum > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void IncreaseIntVarOperation(CheckBox ParameterCheckbox, CheckBox VarCheckbox, TextBox ParameterBoxMax, TextBox ParameterBoxMin, TextBox ParameterBox)
        {
            if (ParameterCheckbox.IsChecked == true)
            {
                if (VarCheckbox.IsChecked == true)
                {
                    int min = Int32.Parse(ParameterBoxMin.Text);
                    int max = Int32.Parse(ParameterBoxMax.Text);
                    GenerateCommands(min, max);
                    ResultsAppend(ParameterCheckbox.Content.ToString(), (min));
                }
                else
                {
                    ResultsAppend(ParameterCheckbox.Content.ToString(), ParameterBox.Text);
                }
            }
        }

        private void MultiCahrVarOperation(CheckBox ParameterCheckbox, CheckBox VarCheckbox, TextBox ParameterBoxList, TextBox ParameterBox)
        {
            if (ParameterCheckbox.IsChecked == true)
            {
                if (VarCheckbox.IsChecked == true)
                {
                    string[] appendParas = ParameterBoxList.Text.Split('|');
                    GenerateCommands(0, appendParas.Length - 1);
                    ResultsAppend(ParameterCheckbox.Content.ToString(), appendParas);
                }
                else
                {
                    ResultsAppend(ParameterCheckbox.Content.ToString(), ParameterBox.Text);
                }
            }
        }

        private void ResultsAppend(string appendString1, string appendString2)
        {
            foreach (var command in results)
            {
                command.Append(appendString1 + appendString2 + " ");
            }
        }
        private void ResultsAppend(string appendString)
        {
            foreach (var command in results)
            {
                command.Append(appendString + " ");
            }
        }

        private void ResultsAppend(string appendString1, int appendMinInt)
        {
            int va = appendMinInt;
            foreach (var command in results)
            {
                command.Append(appendString1 + va + " ");
                va += 1;
            }
        }

        private void ResultsAppend(string appendString1, string[] appendStrings)
        {
            int va = 0;
            foreach (var command in results)
            {
                command.Append(appendString1 + appendStrings[va] + " ");
                va += 1;
            }
        }


        private void GenerateCommands(int min, int max)
        {
            int num = max - min;
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    results.Add(new StringBuilder(results[0].ToString()));
                }
            }
        }
    }
}
