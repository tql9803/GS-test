using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


using Google.Cloud.Speech.V1;

namespace GS_test_dotnet
{
    public partial class Form1 : Form
    {

        public string DEMO_FILE = @"G:\Code\GS test dotnet\Audio test.wav";
        private string OutPut;
        public Form1()
        {
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 8000,
                LanguageCode = "en",
            }, RecognitionAudio.FromFile(DEMO_FILE));

            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    OutPut += alternative.Transcript;
                }
            }

            label1.Text = OutPut;
            //string path = @"G:\Code\GS test dotnet";
            //// This text is added only once to the file.
            //if (!File.Exists(path))
            //{
            //    // Create a file to write to.
            //    using (StreamWriter sw = File.CreateText(path))
            //    {
            //        sw.WriteLine("Hello");
            //        sw.WriteLine("And");
            //        sw.WriteLine("Welcome");
            //    }
            //}

        }
    }

    
}
