namespace VoxelStoragePerformanceComparison
{
    public partial class Form1 : Form
    {
        // Declare UI controls
        private Label lblVoxelCount;
        private TextBox txtVoxelCount;
        private CheckBox chkBinaryFile;
        private CheckBox chkDatabase;
        private Button btnExecute;
        private RichTextBox rtbOutput;

        public Form1()
        {
            InitializeComponent();
            InitializeDynamicControls();
        }

        private void InitializeDynamicControls()
        {
            // Form properties
            this.Text = "Voxel Generator and Storage";
            this.Width = 600;
            this.Height = 500;

            // Label: Enter number of voxels
            lblVoxelCount = new Label
            {
                Text = "Enter number of voxels to generate:",
                Location = new System.Drawing.Point(20, 20),
                Width = 250
            };
            this.Controls.Add(lblVoxelCount);

            // TextBox: Input for voxel count
            txtVoxelCount = new TextBox
            {
                Location = new System.Drawing.Point(280, 18),
                Width = 100
            };
            this.Controls.Add(txtVoxelCount);

            // CheckBox: Save/Read from Binary File
            chkBinaryFile = new CheckBox
            {
                Text = "Save/Read from Binary File",
                Location = new System.Drawing.Point(20, 60),
                Width = 200
            };
            this.Controls.Add(chkBinaryFile);

            // CheckBox: Save/Read from Database
            chkDatabase = new CheckBox
            {
                Text = "Save/Read from Database",
                Location = new System.Drawing.Point(280, 60),
                Width = 200
            };
            this.Controls.Add(chkDatabase);

            // Button: Execute the process
            btnExecute = new Button
            {
                Text = "Generate and Process Voxels",
                Location = new System.Drawing.Point(20, 100),
                Width = 360,
                Height = 30
            };
            btnExecute.Click += btnExecute_Click; // Attach event handler
            this.Controls.Add(btnExecute);

            // RichTextBox: Output display
            rtbOutput = new RichTextBox
            {
                Location = new System.Drawing.Point(20, 150),
                Width = 540,
                Height = 280
            };
            this.Controls.Add(rtbOutput);
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            btnExecute.Enabled = false;
            rtbOutput.AppendText("It will take some time....\n");

            // Get the base directory of the running application
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the relative path to the data folder
            string dataFolder = Path.Combine(baseDirectory, "Data");

            // Ensure the Data folder exists
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            // File paths relative to the Data folder
            string binaryFileName = Path.Combine(dataFolder, "voxelsData.txt");
            string logFile = Path.Combine(dataFolder, "diagnostics.txt");
            string dbFileName = Path.Combine(dataFolder, "VoxelDatabase.db");

            string connectionString = $"Data Source={dbFileName};";

            // Initialize database if the checkbox is checked
            if (chkDatabase.Checked)
            {
                VoxelStorage.InitializeDatabase(connectionString);
            }

            // Variables to store the time taken for each operation
            float totalSecondsItTookToGenerate = 0;
            float totalSecondsItTookToWriteBinary = 0;
            float totalSecondsItTookToReadBinary = 0;
            float totalSecondsItTookToWriteDB = 0;
            float totalSecondsItTookToReadDB = 0;
            bool isVoxelReadAndConvertedFromDBCorrectly = false;
            bool isVoxelReadAndConvertedFromBinaryFileCorrectly = false;

            // Get user input for voxel count
            if (!int.TryParse(txtVoxelCount.Text, out int voxelCount) || voxelCount <= 0)
            {
                rtbOutput.AppendText("Invalid input. Please enter a valid number.\n");
                btnExecute.Enabled = true;
                return;
            }

            rtbOutput.AppendText($"Generating {voxelCount} voxels...\n");

            // Perform the operations asynchronously
            await Task.Run(() =>
            {
                // Generate random voxels
                Voxel[] voxels = VoxelStorage.GenerateRandomVoxelList(voxelCount, out totalSecondsItTookToGenerate);
                Invoke(new Action(() => rtbOutput.AppendText($"Voxel list created. It took {totalSecondsItTookToGenerate:F2} seconds.\n")));

                if (chkBinaryFile.Checked)
                {
                    // Write to binary file
                    VoxelStorage.WriteVoxelDataToBinaryFile(voxels, binaryFileName, out totalSecondsItTookToWriteBinary);
                    Invoke(new Action(() => rtbOutput.AppendText($"Data written to the Binary File. It took {totalSecondsItTookToWriteBinary:F2} seconds.\n")));

                    // Read from binary file
                    Voxel[] loadedVoxels = VoxelStorage.ReadVoxelDataFromBinaryFile(binaryFileName, out totalSecondsItTookToReadBinary);
                    Invoke(new Action(() => rtbOutput.AppendText($"Data read from Binary File. It took {totalSecondsItTookToReadBinary:F2} seconds.\n")));

                    isVoxelReadAndConvertedFromBinaryFileCorrectly = voxels[0].Equals(loadedVoxels[0]);
                    Invoke(new Action(() => rtbOutput.AppendText($"Voxel read from binary file was {(isVoxelReadAndConvertedFromBinaryFileCorrectly ? "correctly" : "not correctly")} converted.\n")));
                }

                if (chkDatabase.Checked)
                {
                    // Write to database
                    VoxelStorage.WriteVoxelDataToDatabase(voxels, connectionString, out totalSecondsItTookToWriteDB);
                    Invoke(new Action(() => rtbOutput.AppendText($"Data written to database. It took {totalSecondsItTookToWriteDB:F2} seconds.\n")));

                    // Read from database
                    Voxel[] loadedVoxelsFromDB = VoxelStorage.ReadVoxelDataFromDatabase(connectionString, out totalSecondsItTookToReadDB);
                    Invoke(new Action(() => rtbOutput.AppendText($"Data read from database. It took {totalSecondsItTookToReadDB:F2} seconds.\n")));

                    isVoxelReadAndConvertedFromDBCorrectly = voxels[0].Equals(loadedVoxelsFromDB[0]);
                    Invoke(new Action(() => rtbOutput.AppendText($"Voxel read from database was {(isVoxelReadAndConvertedFromDBCorrectly ? "correctly" : "not correctly")} converted.\n")));
                }

                // Log diagnostics
                VoxelStorage.LogDiagnostics(logFile, voxelCount, totalSecondsItTookToGenerate, totalSecondsItTookToWriteBinary, totalSecondsItTookToReadBinary,
                                            totalSecondsItTookToWriteDB, totalSecondsItTookToReadDB, isVoxelReadAndConvertedFromBinaryFileCorrectly, isVoxelReadAndConvertedFromDBCorrectly, binaryFileName, dbFileName, chkDatabase.Checked, chkBinaryFile.Checked);

                Invoke(new Action(() => rtbOutput.AppendText("\nDiagnostics logged successfully." +
                    "\n------------------------------------------------------------\n")));
            });

            btnExecute.Enabled = true;
        }
    }
}
