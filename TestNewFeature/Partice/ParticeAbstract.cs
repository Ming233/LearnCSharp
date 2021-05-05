using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TestNewFeature
{
    class ParticeAbstract
    {
        public class A2ndAbstuct : A1stAbstruct
        {
            private readonly string _url;

            public A2ndAbstuct(string url)
            {
                _url = url;
            }

            protected override string[] GetData()
            {
                var client = new WebClient();
                return client.DownloadString(new Uri(_url)).Split('\n');
            }
        }

        public abstract class A1stAbstruct
        {
            protected abstract string[] GetData();

            public IEnumerable<ListofData> Load()
            {
                return
                    from line in GetData().Skip(1)
                    let data = line.Replace("-", "/").Split(',')
                    where data[0].Length > 0
                    select new ListofData()
                    {
                        Data1 = data[0],
                        Data2 = decimal.Parse(data[1]),
                        Data3 = DateTime.Parse(data[2]),
                        Data4 = Double.Parse(data[3]),
                        Data5 = int.Parse(data[4])
                    };
            }
        }
    }
}
