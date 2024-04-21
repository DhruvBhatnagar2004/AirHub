using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Airhub.pages
{
    /// <summary>
    /// Interaction logic for Bot.xaml
    /// </summary>
    public partial class Bot : Window
    {
        public Bot()
        {
            InitializeComponent();
            Loaded += Bot_Loaded;
        }
        private void Bot_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the screen dimensions
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Set the window position
            Left = screenWidth - Width;
            Top = screenHeight - Height;
            Task.Run(async () => await RunPythonScript());
        }
        private async Task RunPythonScript()
        {
            try
            {
                string pythonExe = "python"; // Assuming 'python' is in PATH, adjust if necessary
                string pythonScript = @"D:\test.py"; // Path to your Python script

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = pythonScript,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    Debug.WriteLine("In process");
                    // Read the standard output of the Python process
                    using (StreamReader reader = process.StandardOutput)
                    {
                        Debug.WriteLine("in using");
                        string output = await reader.ReadToEndAsync();

                        // Update text block with output
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Output.Text = output; // Replace 'outputTextBlock' with the actual name of your TextBlock element
                        });
                    }

                    // Handle errors if needed
                    string error = await process.StandardError.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Error occurred: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }

    }
}
