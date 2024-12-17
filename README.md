# Voxel Storage Performance Comparison

## Overview

This project compares the performance of storing voxel data in **binary files** versus an **SQLite database**. It allows you to generate voxel data, save it, read it back, and log performance diagnostics.

## How to Use

1. **Generate Voxels**:  
   Enter the number of voxels to generate in the application.

2. **Choose Storage Method**:  
   - Check **"Save/Read from Binary File"** to use binary file storage.  
   - Check **"Save/Read from Database"** to use SQLite database storage.  
   - Check both to test both storage methods.

3. **Run the Process**:  
   Click the **"Generate and Process Voxels"** button.

## Diagnostics File Location

Performance diagnostics are saved to:  

bin\Debug\net8.0-windows\Data\diagnostics.txt

## Project Files

- **`Data/`**: Contains `voxelsData.txt`, `VoxelDatabase.db`, and `diagnostics.txt`.  
- **`VoxelStorage.cs`**: Handles binary file and database operations.  
- **`Form1.cs`**: Windows Forms interface for the app.
