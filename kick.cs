using System;
using System.IO;

class kick
{
    static void Main(string[] args)
    {
        string fileToTimeBlock = null;

        if (args.Length > 0)
        {
            fileToTimeBlock = args[0];
        }

        string directoryPath = "d:\\Hyper-V\\Virtual Hard Disks\\";
//      string directoryPath = "c:\\t\\";

        try
        {
            // Get all files in the directory.
            string[] files = Directory.GetFiles(directoryPath);

            // Iterate through each file in the directory.
            foreach (string filePath in files)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                // Check if the file is read-only and if its last modified date is in the past.
                if (fileInfo.IsReadOnly && fileInfo.LastWriteTime < DateTime.Now)
                    fileInfo.Attributes &= ~FileAttributes.ReadOnly;

                if (null == fileToTimeBlock)
                {
                    if (fileInfo.Name.Contains(".avhdx"))
                        Console.WriteLine("{0} {1}", fileInfo.IsReadOnly ? "BLOCKED" : "       ", fileInfo.Name);
                }
            }

            if (null != fileToTimeBlock)
            {
                // Get the current time and add one hour to it.
                int iHours = 1;
                if (args.GetLength(0) > 1)
                    iHours = Int32.Parse(args[1]);

                DateTime newLastModifiedTime = DateTime.Now.AddHours(iHours);
                FileInfo fileInfo = new FileInfo(directoryPath + fileToTimeBlock);
                fileInfo.LastWriteTime = newLastModifiedTime;
                fileInfo.Attributes |= FileAttributes.ReadOnly;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
