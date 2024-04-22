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
                // Path to the named pipe
                string pipeName = "testpipe";

                // Start the Python process
                using (var process = new System.Diagnostics.Process())
                {
                    process.StartInfo.FileName = "python"; // Assuming 'python' is in PATH
                    process.StartInfo.Arguments = @"C:\Users\Cypher\source\repos\Airhub\python\va.py"; // Path to your Python script
                    ///process.StartInfo.Arguments = @"D:\python\test.py"; // Path to your Python script
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;

                    /*
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Output.Text += e.Data + Environment.NewLine; // Append output to existing text
                            });
                        }
                    };
                    
                    process.Start();

                    // Begin asynchronous read of standard output
                    process.BeginOutputReadLine();

                    // Create the named pipe server
                    using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In))
                    {
                        // Connect to the named pipe client (Python script)
                        Debug.WriteLine("before async");
                        await pipeServer.WaitForConnectionAsync();

                        // Read output from the named pipe client
                        using (StreamReader reader = new StreamReader(pipeServer))
                        {
                            Debug.WriteLine("in while");
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                Debug.WriteLine("please");
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    Debug.WriteLine("fuckyou");
                                    Output.Text += line + Environment.NewLine; // Append output to existing text
                                });
                            }
                            Debug.WriteLine("fuck you while loop");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }
        */


                    process.Start();

                    // Create the named pipe server
                    using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In))
                    {
                        // Connect to the named pipe client (Python script)
                        Debug.WriteLine("before async");
                        await pipeServer.WaitForConnectionAsync();

                        // Read output from the named pipe client and update the UI
                        using (StreamReader reader = new StreamReader(pipeServer))
                        {
                            Debug.WriteLine("in while");
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                Debug.WriteLine("please");
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    Debug.WriteLine("fuckyou");
                                    Output.Text += line + Environment.NewLine; // Append output to existing text
                                });
                            }
                            Debug.WriteLine("fuck you while loop");
                        }
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
