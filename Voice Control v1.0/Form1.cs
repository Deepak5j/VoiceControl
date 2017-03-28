using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition; //1

namespace Voice_Control_v1._0
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            btnEnable.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hello", "hell", "hail", "my name", "notepad","cmd","calculator","explorer", "cdrive"});
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            switch (e.Result.Text)
            {
                case "hello":
                    richTextBox1.Text += "\nHello";
                    //MessageBox.Show("Hello Sir");
                    richTextBox1.Text += "\n"+e.Result.Text;
                    break;
                case "calculator":
                    richTextBox1.Text += "\ncalculator";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    //proc.EnableRaisingEvents=false;
                    proc.StartInfo.FileName = "calc";
                    proc.Start();
                    break;
                case "hell":
                    richTextBox1.Text += "\nHell";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    break;
                case "hail":
                    richTextBox1.Text += "\nHail";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    break;
                case "my name":
                    richTextBox1.Text += "\nDeepak Gautam";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    break;
                case "cmd":
                    richTextBox1.Text += "\nCommand Prompt";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    proc.StartInfo.FileName = "cmd";
                    proc.Start();
                    break;
                case "notepad":
                    richTextBox1.Text += "\nNotepad";
                    proc.StartInfo.FileName = "notepad";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    proc.Start();
                    break;
                case "cdrive":
                    richTextBox1.Text += "\nC Drive";
                    proc.StartInfo.FileName = "c:";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    proc.Start();
                    break;
                default:
                    //Console.WriteLine("Default case");
                    richTextBox1.Text += "\nCommand Not Found";
                    richTextBox1.Text += "\n" + e.Result.Text;
                    break;
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
            btnEnable.Enabled = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    } 
}