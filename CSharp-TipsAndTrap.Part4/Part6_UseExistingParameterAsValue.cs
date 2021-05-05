using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_TipsAndTrap.Part4
{
    class Part6_UseExistingParameterAsValue
    {

        public byte[] Bytes { get; }

        public Part6_UseExistingParameterAsValue(string path)
        {
            Bytes = File.ReadAllBytes(path);

            // validation / error checking code omitted
        }

        //Use byte as value
        public void SetFirstByte(byte @byte)
        {
            Bytes[0] = @byte;
        }

        //public byte this[int i]
        //{
        //    get
        //    {
        //        return Bytes[i];
        //    }
        //    set
        //    {
        //        Bytes[i] = value;
        //    }
        //}

        // C# >=7.0 expression-bodied member support for indexers
        public byte this[int i]
        {
            get => Bytes[i];
            set => Bytes[i] = value;
        }
    }
}
