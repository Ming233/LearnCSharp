using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    public class Part5_Uri
    {
        #region Uri

        [Fact]
        public void GetUploadUri()
        {
            FileProcessor sut = new FileProcessor();

            string uri = sut.GetUploadUri();

            //Assert.Equal("http://dontcodetired.com/", uri);
            Assert.Equal("http://dontcodetired.com/blog", uri);
        }

        [Fact]
        public void NonHttpUris()
        {
            Uri localFile = new Uri(@"c:\temp\somefile.bin");
            Uri uncLanFile = new Uri(@"\\somepc\shareddocs\somefile.txt");
        }

        [Fact]
        public void CreatingRelativeAndAbsolute()
        {
            Uri dct1 = new Uri("http://dontcodetired.com"); // assumes absolute
            Uri dct2 = new Uri("http://dontcodetired.com", UriKind.Absolute);

            Uri relativeUri = new Uri("/index.html", UriKind.Relative);

            Uri relativeOrAbsolute = new Uri("/blog/", UriKind.RelativeOrAbsolute);

            Uri baseUri = new Uri("http://dontcodetired.com");
            Uri fullUri = new Uri(baseUri, relativeUri);
        }

        [Fact]
        public void UriParts()
        {
            Uri dct = new Uri("http://dontcodetired.com:8080/blog/?tag=code#somefragment");

            string scheme = dct.Scheme;

            string authority = dct.Authority; // Host name + port number (if non default port)
            string authorityHost = dct.Host; // Domain name or IP address (no port)
            int port = dct.Port;

            string pathAndQuery = dct.PathAndQuery;
            string absolutePath = dct.AbsolutePath;
            string query = dct.Query;

            string fragment = dct.Fragment;
        }


        [Fact]
        public void ModifyingAUri()
        {
            Uri dct = new Uri("http://dontcodetired.com:8080/blog/?tag=code#somefragment");

            // dct.Fragment = "newfrag"; // read only
            // dct.Port = 9090; // read only

            UriBuilder builder = new UriBuilder(dct);
            builder.Port = 9090;
            builder.Fragment = "newfrag";

            Uri modifiedDct = builder.Uri;
            string modified = modifiedDct.ToString();
        }


        [Fact]
        public void SomeOtherUsefulThings()
        {
            Uri dct = new Uri("http://dontcodetired.com/blog/");
            bool isDefaultPort = dct.IsDefaultPort;
            bool isFile = dct.IsFile;
            bool isUnc = dct.IsUnc;

            Uri localFile = new Uri("file:///c:/temp/somefile.bin");
            string path = localFile.LocalPath;
            isFile = localFile.IsFile;
            isUnc = localFile.IsUnc;
        }

        #endregion

    }
}
