using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    public class FileProcessorShould
    {
        #region Path

        [Fact]
        public void GetInputFilePath()
        {
            FileProcessor sut = new FileProcessor();

            string path = sut.GetInputFilePath();

            Assert.Equal(@"C:\temp\pspathdemo\test.txt", path);
        }

        [Fact]
        public void UsefulPathMethods()
        {
            string path = @"c:\temp\pspathdemo\test.txt";

            path = Path.ChangeExtension(path, "bak");

            string dirName = Path.GetDirectoryName(path);

            string ext = Path.GetExtension(path);

            string file = Path.GetFileName(path);

            string fileNoExt = Path.GetFileNameWithoutExtension(path);

            bool hasExt = Path.HasExtension(path);
        }

        [Fact]
        public void UsefulGeneralMethods()
        {
            char[] invalidNameChars = Path.GetInvalidFileNameChars();

            string rndFileName = Path.GetRandomFileName();

            string rndTempFile = Path.GetTempFileName();

            string userTempPath = Path.GetTempPath();

            char platformSpecificDirSeparater = Path.DirectorySeparatorChar;
        }

        [Fact]
        public void PathCombinePeculiarities()
        {
            string result = Path.Combine(@"\data", @"c:\temp");

            result = Path.Combine(@"c:\temp", @"\data");

            result = Path.Combine(@"c:\temp", @"data");

            result = Path.Combine(@"c:\temp",
                                  @"\data".TrimStart(Path.DirectorySeparatorChar));


            // using ".." to refer to parent dir
            result = Path.Combine(@"c:\temp\data", @"..");

            //Check this path 
            result = Path.GetFullPath(result);
        }

        #endregion

     

        #region zip files


        private const string inputDirectory = @"c:\psdata\somefiles";
        private const string outputZipFile = @"c:\psdata\somefiles1.zip";

        [Fact]
        public void ZipFiles()
        {
            File.Delete(outputZipFile);

            FileProcessor sut = new FileProcessor();

            sut.ZipDirectory(inputDirectory, outputZipFile);

            Assert.True(File.Exists(outputZipFile));
        }

        [Fact]
        public void UnzipFiles()
        {
            ZipFiles();

            string unzipDestinationDir = Path.Combine(inputDirectory, @"..\unzip");
            unzipDestinationDir = Path.GetFullPath(unzipDestinationDir);

            if (Directory.Exists(unzipDestinationDir))
            {
                Directory.Delete(unzipDestinationDir, true);
            }

            FileProcessor sut = new FileProcessor();

            sut.UnZip(outputZipFile, unzipDestinationDir);

            // Asserts omitted
        }

        [Fact]
        public void AddFile()
        {
            ZipFiles();

            FileProcessor sut = new FileProcessor();

            sut.AddToZip(outputZipFile, @"c:\psdata\AnExtraFile.txt");

            // Asserts omitted
        }

        [Fact]
        public void RemoveFile()
        {
            ZipFiles();

            FileProcessor sut = new FileProcessor();

            sut.RemoveFromZip(outputZipFile, @"somefiles\text1.txt");

            // Asserts omitted
        }

        #endregion


        #region in-memory compression
        [Fact]
        public void CompressDecompress()
        {
            const string originalString = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce dignissim vitae nulla a vehicula. Quisque ut diam suscipit, accumsan turpis ac, feugiat lorem. Nam malesuada consequat mi, vitae vulputate diam gravida quis. Aenean a neque aliquet, luctus quam vel, mattis massa. Pellentesque placerat nec sapien quis lacinia. Sed non dui vel sapien tempus laoreet ut vel ligula. Donec mattis semper erat, porttitor facilisis tortor aliquet non.
                                         Proin vitae sollicitudin lorem, fermentum convallis risus. Aliquam tellus risus, porta id interdum ac, tincidunt nec nisl. Nulla in blandit nulla, bibendum volutpat eros. Praesent tincidunt risus nec venenatis vehicula. Praesent urna odio, tempor quis justo sed, semper auctor nisi. Nulla auctor blandit mauris sit amet varius. Cras suscipit fermentum libero ac dictum. Donec vulputate eros elementum tincidunt dapibus. Fusce eget elit quam. Vestibulum ut porta ante. Mauris quis sapien vitae nulla mattis dapibus. Curabitur cursus odio sit amet tellus iaculis faucibus. Nunc sed eleifend velit, eget tincidunt orci. Curabitur ornare felis id dolor porta interdum eget a nisi. Nam ut adipiscing dui. Duis eu sapien nisl.
                                         Maecenas vulputate cursus nulla et commodo. Morbi a rhoncus tellus. Ut hendrerit quis augue ultricies eleifend. Phasellus a feugiat urna, sed mattis lorem. Sed in rutrum sem. Praesent fermentum est erat. Etiam luctus velit a nulla semper interdum.
                                         Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Quisque magna neque, lacinia sit amet consectetur non, viverra vel ligula. Phasellus accumsan quis metus accumsan aliquam. Nam posuere lorem ligula, eu mattis enim mollis non. In hac habitasse platea dictumst. Integer tristique malesuada urna at fermentum. In eget pretium massa. Donec magna nisl, suscipit ut viverra a, vehicula eu nulla. In auctor lobortis aliquam. Vestibulum adipiscing est in mi tristique, nec aliquet elit vulputate. Etiam sollicitudin malesuada semper.
                                         Sed ornare, enim ut luctus cursus, mauris ligula semper libero, sed suscipit nisi lectus quis mi. Suspendisse et auctor odio. Proin vestibulum molestie ligula, ut dapibus quam euismod id. Donec consequat purus nec tellus vulputate sagittis. Nunc eu justo in elit commodo tincidunt sit amet eu erat. Vestibulum sed lacinia magna. Duis faucibus scelerisque sapien, non vulputate augue dignissim condimentum. Fusce gravida elementum cursus. Maecenas imperdiet, nulla sagittis bibendum aliquam, velit risus pharetra erat, vel adipiscing neque magna at erat";

            byte[] originalBytes = Encoding.ASCII.GetBytes(originalString);

            FileProcessor sut = new FileProcessor();

            // Compress
            byte[] compressedBytes = sut.Compress(originalBytes);

            int originalSize = originalBytes.Length;
            int compressedSize = compressedBytes.Length;
            int sizeDifference = originalSize - compressedSize;


            // Decompress
            byte[] decompressedBytes = sut.Decompress(compressedBytes);
            string decompressedString = Encoding.ASCII.GetString(decompressedBytes);

            Assert.Equal(originalBytes, decompressedBytes);
            Assert.Equal(originalString, decompressedString);
        }


        #endregion
    }

    public class FileProcessor
    {
        public void Process()
        {
            string inputFilePath = GetInputFilePath();

            // Perform some file processing

            string uploadUri = GetUploadUri();

            // upload file
        }

        public string GetInputFilePath()
        {
            var drive = @"C:\";
            var dir = @"temp\pspathdemo\";
            var file = "test.txt";


            //var fullPath = drive;

            //fullPath += dir;

            //if (!dir.EndsWith(@"\"))
            //{
            //    fullPath += @"\";
            //}

            //fullPath += file;

            //return fullPath;

            return Path.Combine(drive, dir, file);
        }

        public string GetUploadUri()
        {
            //Uri uploadUri = new Uri("http://dontcodetired.com");
            Uri uploadUri = new Uri("http://dontcodetired.com/blog");

            return uploadUri.ToString();
        }

        public void ZipDirectory(string dirToZip, string outputZipFilePath)
        {
            ZipFile.CreateFromDirectory(dirToZip,
                                        outputZipFilePath,
                                        CompressionLevel.Fastest,
                                        true);
        }

        public void UnZip(string zipFilePath, string outputDir)
        {
            ZipFile.ExtractToDirectory(zipFilePath, outputDir);
        }

        public void AddToZip(string zipFilePath, string extraFilePath)
        {
            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
            {
                zip.CreateEntryFromFile(extraFilePath, Path.GetFileName(extraFilePath));
            }
        }

        public void RemoveFromZip(string zipFilePath, string fileToRemove)
        {
            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
            {
                var f1 = zip.GetEntry(fileToRemove);
                f1.Delete();
            }
        }

        public byte[] Compress(byte[] originalBytes)
        {
            using (var compressionStream = new MemoryStream())
            {
                using (var gzs = new GZipStream(compressionStream, CompressionMode.Compress))
                {
                    gzs.Write(originalBytes, 0, originalBytes.Length);
                }

                byte[] compressedBytes = compressionStream.ToArray();

                return compressedBytes;
            }
        }

        public byte[] Decompress(byte[] compressedBytes)
        {
            using (var compressedStream = new MemoryStream(compressedBytes))
            using (var decompressedStream = new MemoryStream())
            using (var gzs = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                gzs.CopyTo(decompressedStream);

                byte[] decompressedBytes = decompressedStream.ToArray();

                return decompressedBytes;
            }
        }
    }
}
