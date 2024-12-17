using System.Diagnostics;
using System.Numerics; // For using Vector3
using System.Data.SQLite;


namespace VoxelStoragePerformanceComparison
{
    public static class VoxelStorage
    {
        public static Voxel[] GenerateRandomVoxelList(int voxelCount, out float totalSecondsItTookToGenerate)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Random random = new Random();
            Voxel[] voxels = new Voxel[voxelCount];

            for (int i = 0; i < voxelCount; i++)
            {
                Vector3 position = new Vector3(random.Next(0, 1000), random.Next(0, 1000), random.Next(0, 1000));
                Vector3 rotation = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                Vector3 scale = new Vector3(random.Next(1, 10), random.Next(1, 10), random.Next(1, 10));

                byte materialType = (byte)random.Next(0, 256);
                float roughness = (float)random.NextDouble();
                float metalness = (float)random.NextDouble();

                byte density = (byte)random.Next(0, 256);
                float mass = (float)random.Next(1, 100);

                bool isStatic = random.Next(0, 2) == 1;
                bool isCollidable = random.Next(0, 2) == 1;

                Vector3 velocity = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                Vector3 acceleration = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

                string color = $"#{random.Next(0x1000000):X6}";
                string texturePath = $"texture_{random.Next(1, 10)}.png";

                float emission = (float)random.NextDouble();
                float opacity = (float)random.NextDouble();

                int durability = random.Next(0, 1000);
                bool isDestructible = random.Next(0, 2) == 1;

                bool isAffectedByGravity = random.Next(0, 2) == 1;
                bool isFlammable = random.Next(0, 2) == 1;

                voxels[i] = new Voxel(position, rotation, scale, materialType, roughness, metalness,
                                      density, mass, isStatic, isCollidable, velocity, acceleration,
                                      color, texturePath, emission, opacity, durability, isDestructible,
                                      isAffectedByGravity, isFlammable);
            }

            stopwatch.Stop();
            totalSecondsItTookToGenerate = (float)stopwatch.Elapsed.TotalSeconds;

            return voxels;
        }

        public static void WriteVoxelDataToBinaryFile(Voxel[] voxels, string fileName, out float totalSecondsItTookToWrite)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                // Write the number of voxels at the beginning
                writer.Write(voxels.Length);

                foreach (var voxel in voxels)
                {
                    writer.Write(voxel.Position.X);
                    writer.Write(voxel.Position.Y);
                    writer.Write(voxel.Position.Z);

                    writer.Write(voxel.Rotation.X);
                    writer.Write(voxel.Rotation.Y);
                    writer.Write(voxel.Rotation.Z);

                    writer.Write(voxel.Scale.X);
                    writer.Write(voxel.Scale.Y);
                    writer.Write(voxel.Scale.Z);

                    writer.Write(voxel.MaterialType);
                    writer.Write(voxel.Roughness);
                    writer.Write(voxel.Metalness);

                    writer.Write(voxel.Density);
                    writer.Write(voxel.Mass);

                    writer.Write(voxel.IsStatic);
                    writer.Write(voxel.IsCollidable);

                    writer.Write(voxel.Velocity.X);
                    writer.Write(voxel.Velocity.Y);
                    writer.Write(voxel.Velocity.Z);

                    writer.Write(voxel.Acceleration.X);
                    writer.Write(voxel.Acceleration.Y);
                    writer.Write(voxel.Acceleration.Z);

                    writer.Write(voxel.Color);
                    writer.Write(voxel.TexturePath);

                    writer.Write(voxel.Emission);
                    writer.Write(voxel.Opacity);

                    writer.Write(voxel.Durability);
                    writer.Write(voxel.IsDestructible);

                    writer.Write(voxel.IsAffectedByGravity);
                    writer.Write(voxel.IsFlammable);
                }
            }

            stopwatch.Stop();
            totalSecondsItTookToWrite = (float)stopwatch.Elapsed.TotalSeconds;
        }

        public static Voxel[] ReadVoxelDataFromBinaryFile(string fileName, out float totalSecondsItTookToRead)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Voxel[] voxels;

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                // Read the number of voxels at the beginning
                int voxelCount = reader.ReadInt32();
                voxels = new Voxel[voxelCount];

                for (int i = 0; i < voxelCount; i++)
                {
                    Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Vector3 rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Vector3 scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    byte materialType = reader.ReadByte();
                    float roughness = reader.ReadSingle();
                    float metalness = reader.ReadSingle();

                    byte density = reader.ReadByte();
                    float mass = reader.ReadSingle();

                    bool isStatic = reader.ReadBoolean();
                    bool isCollidable = reader.ReadBoolean();

                    Vector3 velocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Vector3 acceleration = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    string color = reader.ReadString();
                    string texturePath = reader.ReadString();

                    float emission = reader.ReadSingle();
                    float opacity = reader.ReadSingle();

                    int durability = reader.ReadInt32();
                    bool isDestructible = reader.ReadBoolean();

                    bool isAffectedByGravity = reader.ReadBoolean();
                    bool isFlammable = reader.ReadBoolean();

                    voxels[i] = new Voxel(position, rotation, scale, materialType, roughness, metalness, density, mass,
                                          isStatic, isCollidable, velocity, acceleration, color, texturePath,
                                          emission, opacity, durability, isDestructible, isAffectedByGravity, isFlammable);
                }
            }

            stopwatch.Stop();
            totalSecondsItTookToRead = (float)stopwatch.Elapsed.TotalSeconds;
            return voxels;
        }

        public static void WriteVoxelDataToDatabase(Voxel[] voxels, string connectionString, out float totalSecondsItTookToSave)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"
        INSERT INTO Voxels (PositionX, PositionY, PositionZ, RotationX, RotationY, RotationZ, 
                            ScaleX, ScaleY, ScaleZ, MaterialType, Roughness, Metalness, 
                            Density, Mass, IsStatic, IsCollidable, 
                            VelocityX, VelocityY, VelocityZ, AccelerationX, AccelerationY, AccelerationZ, 
                            Color, TexturePath, Emission, Opacity, Durability, IsDestructible, 
                            IsAffectedByGravity, IsFlammable) 
        VALUES (@PositionX, @PositionY, @PositionZ, @RotationX, @RotationY, @RotationZ, 
                @ScaleX, @ScaleY, @ScaleZ, @MaterialType, @Roughness, @Metalness, 
                @Density, @Mass, @IsStatic, @IsCollidable, 
                @VelocityX, @VelocityY, @VelocityZ, @AccelerationX, @AccelerationY, @AccelerationZ, 
                @Color, @TexturePath, @Emission, @Opacity, @Durability, @IsDestructible, 
                @IsAffectedByGravity, @IsFlammable)";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    foreach (var voxel in voxels)
                    {
                        cmd.Parameters.Clear();  // Clear parameters for each iteration

                        cmd.Parameters.AddWithValue("@PositionX", voxel.Position.X);
                        cmd.Parameters.AddWithValue("@PositionY", voxel.Position.Y);
                        cmd.Parameters.AddWithValue("@PositionZ", voxel.Position.Z);
                        cmd.Parameters.AddWithValue("@RotationX", voxel.Rotation.X);
                        cmd.Parameters.AddWithValue("@RotationY", voxel.Rotation.Y);
                        cmd.Parameters.AddWithValue("@RotationZ", voxel.Rotation.Z);
                        cmd.Parameters.AddWithValue("@ScaleX", voxel.Scale.X);
                        cmd.Parameters.AddWithValue("@ScaleY", voxel.Scale.Y);
                        cmd.Parameters.AddWithValue("@ScaleZ", voxel.Scale.Z);
                        cmd.Parameters.AddWithValue("@MaterialType", voxel.MaterialType);
                        cmd.Parameters.AddWithValue("@Roughness", voxel.Roughness);
                        cmd.Parameters.AddWithValue("@Metalness", voxel.Metalness);
                        cmd.Parameters.AddWithValue("@Density", voxel.Density);
                        cmd.Parameters.AddWithValue("@Mass", voxel.Mass);
                        cmd.Parameters.AddWithValue("@IsStatic", voxel.IsStatic);
                        cmd.Parameters.AddWithValue("@IsCollidable", voxel.IsCollidable);
                        cmd.Parameters.AddWithValue("@VelocityX", voxel.Velocity.X);
                        cmd.Parameters.AddWithValue("@VelocityY", voxel.Velocity.Y);
                        cmd.Parameters.AddWithValue("@VelocityZ", voxel.Velocity.Z);
                        cmd.Parameters.AddWithValue("@AccelerationX", voxel.Acceleration.X);
                        cmd.Parameters.AddWithValue("@AccelerationY", voxel.Acceleration.Y);
                        cmd.Parameters.AddWithValue("@AccelerationZ", voxel.Acceleration.Z);
                        cmd.Parameters.AddWithValue("@Color", voxel.Color);
                        cmd.Parameters.AddWithValue("@TexturePath", voxel.TexturePath);
                        cmd.Parameters.AddWithValue("@Emission", voxel.Emission);
                        cmd.Parameters.AddWithValue("@Opacity", voxel.Opacity);
                        cmd.Parameters.AddWithValue("@Durability", voxel.Durability);
                        cmd.Parameters.AddWithValue("@IsDestructible", voxel.IsDestructible);
                        cmd.Parameters.AddWithValue("@IsAffectedByGravity", voxel.IsAffectedByGravity);
                        cmd.Parameters.AddWithValue("@IsFlammable", voxel.IsFlammable);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            stopwatch.Stop();
            totalSecondsItTookToSave = (float)stopwatch.Elapsed.TotalSeconds;
        }

        public static Voxel[] ReadVoxelDataFromDatabase(string connectionString, out float totalSecondsItTookToRead)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Voxel> voxels = new List<Voxel>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Voxels";

                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vector3 position = new Vector3(reader.GetFloat(reader.GetOrdinal("PositionX")),
                                                       reader.GetFloat(reader.GetOrdinal("PositionY")),
                                                       reader.GetFloat(reader.GetOrdinal("PositionZ")));

                        Vector3 rotation = new Vector3(reader.GetFloat(reader.GetOrdinal("RotationX")),
                                                       reader.GetFloat(reader.GetOrdinal("RotationY")),
                                                       reader.GetFloat(reader.GetOrdinal("RotationZ")));

                        Vector3 scale = new Vector3(reader.GetFloat(reader.GetOrdinal("ScaleX")),
                                                    reader.GetFloat(reader.GetOrdinal("ScaleY")),
                                                    reader.GetFloat(reader.GetOrdinal("ScaleZ")));

                        byte materialType = (byte)reader.GetInt32(reader.GetOrdinal("MaterialType"));
                        float roughness = reader.GetFloat(reader.GetOrdinal("Roughness"));
                        float metalness = reader.GetFloat(reader.GetOrdinal("Metalness"));

                        byte density = (byte)reader.GetInt32(reader.GetOrdinal("Density"));
                        float mass = reader.GetFloat(reader.GetOrdinal("Mass"));

                        bool isStatic = reader.GetBoolean(reader.GetOrdinal("IsStatic"));
                        bool isCollidable = reader.GetBoolean(reader.GetOrdinal("IsCollidable"));

                        Vector3 velocity = new Vector3(reader.GetFloat(reader.GetOrdinal("VelocityX")),
                                                       reader.GetFloat(reader.GetOrdinal("VelocityY")),
                                                       reader.GetFloat(reader.GetOrdinal("VelocityZ")));

                        Vector3 acceleration = new Vector3(reader.GetFloat(reader.GetOrdinal("AccelerationX")),
                                                           reader.GetFloat(reader.GetOrdinal("AccelerationY")),
                                                           reader.GetFloat(reader.GetOrdinal("AccelerationZ")));

                        string color = reader.GetString(reader.GetOrdinal("Color"));
                        string texturePath = reader.GetString(reader.GetOrdinal("TexturePath"));

                        float emission = reader.GetFloat(reader.GetOrdinal("Emission"));
                        float opacity = reader.GetFloat(reader.GetOrdinal("Opacity"));

                        int durability = reader.GetInt32(reader.GetOrdinal("Durability"));
                        bool isDestructible = reader.GetBoolean(reader.GetOrdinal("IsDestructible"));

                        bool isAffectedByGravity = reader.GetBoolean(reader.GetOrdinal("IsAffectedByGravity"));
                        bool isFlammable = reader.GetBoolean(reader.GetOrdinal("IsFlammable"));

                        // Create the voxel object and add it to the list
                        voxels.Add(new Voxel(position, rotation, scale, materialType, roughness, metalness,
                                             density, mass, isStatic, isCollidable, velocity, acceleration,
                                             color, texturePath, emission, opacity, durability, isDestructible,
                                             isAffectedByGravity, isFlammable));
                    }
                }
            }

            stopwatch.Stop();
            totalSecondsItTookToRead = (float)stopwatch.Elapsed.TotalSeconds;
            return voxels.ToArray();
        }

        public static void InitializeDatabase(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Voxels (
                    VoxelId INTEGER PRIMARY KEY AUTOINCREMENT,
                    PositionX REAL, PositionY REAL, PositionZ REAL,
                    RotationX REAL, RotationY REAL, RotationZ REAL,
                    ScaleX REAL, ScaleY REAL, ScaleZ REAL,
                    MaterialType INTEGER, Roughness REAL, Metalness REAL,
                    Density INTEGER, Mass REAL,
                    IsStatic INTEGER, IsCollidable INTEGER,
                    VelocityX REAL, VelocityY REAL, VelocityZ REAL,
                    AccelerationX REAL, AccelerationY REAL, AccelerationZ REAL,
                    Color TEXT, TexturePath TEXT,
                    Emission REAL, Opacity REAL,
                    Durability INTEGER, IsDestructible INTEGER,
                    IsAffectedByGravity INTEGER, IsFlammable INTEGER
                );";

                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Clear all data in the table
                string clearTableQuery = "DELETE FROM Voxels";

                using (var clearCmd = new SQLiteCommand(clearTableQuery, connection))
                {
                    clearCmd.ExecuteNonQuery();
                }
            }
        }

        public static void LogDiagnostics(string fileName, int voxelCount, float generateTime, float writeTimeToBinaryFile,
                                  float readTimeFromBinaryFile, float writeDBTime, float readDBTime,
                                  bool isVoxelReadAndConvertedFromBinaryFileCorrectly,
                                  bool isVoxelReadAndConvertedFromDBCorrectly, string binaryFile, string databaseFile, bool didCheckDB, bool didCheckBinaryFile)
        {
            try
            {
                Console.WriteLine("Attempting to write diagnostics...");

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string systemInfo = GetSystemInfo();

                // Create or open the file to write diagnostics
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("------------------------------------------------------------");
                    writer.WriteLine($"Timestamp: {timestamp}");
                    writer.WriteLine(systemInfo);
                    writer.WriteLine($"Generated {voxelCount} voxels");
                    writer.WriteLine($"Time to Generate: {generateTime:F2} seconds");

                    if (didCheckBinaryFile)
                    {
                        writer.WriteLine($"\nBinary File:");
                        writer.WriteLine($"Time to Write to Binary File: {writeTimeToBinaryFile:F2} seconds");
                        writer.WriteLine($"Time to Read from Binary File: {readTimeFromBinaryFile:F2} seconds");
                        writer.WriteLine($"The voxel was {(isVoxelReadAndConvertedFromBinaryFileCorrectly ? "" : "not ")}correctly read and converted from binary file.");
                        writer.WriteLine($"File size: {GetFileSizeInMB(binaryFile):F2} MB");
                    }
                    else
                    {
                        writer.WriteLine($"Binary File write/read was not tested on this turn");
                    }

                    if (didCheckDB)
                    {
                        writer.WriteLine($"\nDatabase:");
                        writer.WriteLine($"Time to Write to Database: {writeDBTime:F2} seconds");
                        writer.WriteLine($"Time to Read from Database: {readDBTime:F2} seconds");
                        writer.WriteLine($"The voxel was {(isVoxelReadAndConvertedFromDBCorrectly ? "" : "not ")}correctly read and converted from database.");
                        writer.WriteLine($"File size: {GetFileSizeInMB(databaseFile):F2} MB");
                    }
                    else
                    {
                        writer.WriteLine($"Database write/read was not tested on this turn");
                    }

                    writer.WriteLine("------------------------------------------------------------\n");
                }

                Console.WriteLine("Diagnostics written successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing diagnostics: {ex.Message}");
            }
        }

        public static string GetSystemInfo()
        {
            return $"System Information:\n" +
                   $"------------------------------\n" +
                   $"Machine Name    : {Environment.MachineName}\n" +
                   $"OS Version      : {Environment.OSVersion}\n" +
                   $"Processor Count : {Environment.ProcessorCount}\n" +
                   $"CLR Version     : {Environment.Version}\n" +
                   $"System Uptime   : {TimeSpan.FromMilliseconds(Environment.TickCount64)}\n";
        }

        static void LogVoxel(Voxel voxelToLog)
        {
            if (voxelToLog != null)
            {
                Console.WriteLine("-----------------------------------------------------------\n");
                Console.WriteLine(voxelToLog.ToString());
                Console.WriteLine("-----------------------------------------------------------\n");
            }
        }

        static double GetFileSizeInMB(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo != null)
                return fileInfo.Length / (1024.0 * 1024.0);  // Convert bytes to MB

            return 0;
        }
    }
}

