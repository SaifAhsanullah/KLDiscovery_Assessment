using System;
using System.IO;
using System.Security.Cryptography;

namespace KLDiscover_Assessment
{
    class Program
    {
        //public static StreamWriter outputFile;
        static FileWriter outputFile = new FileWriter();
        static void Main(string[] args)
        {
            Console.WriteLine("Enter directory to be analyzed: ");
            string inputDirectory = @""+Console.ReadLine();

            Console.WriteLine("Enter directory for output file: ");
            string outputDirectory = @""+Console.ReadLine();

            Console.WriteLine("Do you want to analyze subdirectories? (Yes/No) or (Y/N): ");
            bool subDirectories = ConvertBoolInput(Console.ReadLine());

            //outputFile = File.CreateText(outputDirectory + "Output.csv");


            //FileProcessor(inputDirectory, outputDirectory, subDirectories);

            DirectoryProcess(inputDirectory, outputDirectory, subDirectories);

            //outputFile.Close();
        }

        static void FileProcessor(string inputPath, string outputPath, bool subdirectories)
        {
            if (File.Exists(inputPath))
            {

                string[] files = Directory.GetFiles(inputPath);
                foreach (var file in files)
                {
                    ProcessFile(file, outputPath);
                }
            }
            else if (Directory.Exists(inputPath))
            {
                DirectoryProcess(inputPath, outputPath, subdirectories);
            }
        }

        private static void DirectoryProcess(string path, string outputPath, bool subdirectories)
        {
            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                ProcessFile(file, outputPath);
            }

            if (subdirectories)
            {
                string[] subDirectoryFiles = Directory.GetDirectories(path);
                foreach (var subDirectories in subDirectoryFiles)
                {
                    DirectoryProcess(subDirectories, outputPath, subdirectories);
                }
            }
        }

        private static void ProcessFile(string file, string outputPath)
        {
            var fileType = VerifyMagicNumber(file);

            if (fileType == "JPG" || fileType == "PDF")
            {
                //outputFile.WriteLine(file + "," + fileType + "," + ConvertFileToMDF(file));
                //outputFile.Flush();
                string[] outputInfo = new string[] {file, fileType, ConvertFileToMDF(file)};
                outputFile.WriteFile(outputInfo,outputPath);
            }
        }

        private static string VerifyMagicNumber(string file)
        {
            byte[] buffer;

            using (var a = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(a))
                buffer = reader.ReadBytes(4);

            var hexadecimal = BitConverter.ToString(buffer).ToUpper().Replace("-","");

            if (hexadecimal.StartsWith("FFD8"))
                return "JPG";
            if (hexadecimal.StartsWith("2550"))
                return "PDF";
            return "invalid";
        }

        private static string ConvertFileToMDF(string inputFile)
        {
            byte[] hash = MD5.Create().ComputeHash(File.ReadAllBytes(inputFile));
            return BitConverter.ToString(hash).Replace("-", "");
        }

        static bool ConvertBoolInput(string input)
        {
            switch (input.ToLower())
            {
                case "yes":
                    return true;
                case "y":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        }
    }
}
