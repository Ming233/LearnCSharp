using System.Collections.Generic;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    public partial class DataModel
    {
        public string Status { get; private set; }

        public DataModel()
        {
            Status = "New";
        }

        public void ClearStatus()
        {
            Status = "";
            AfterStatusCleared();
        }

        partial void AfterStatusCleared(); // No implementation or {} 
    }

    public partial class DataModel
    {
        public void AHandCodedMethod() { }

        //partial void AfterStatusCleared()
        //{
        //    // Implementation goes here

        //    throw new Exception("Simulated exception for demo purposes.");
        //}
    }

    public class Part6_PartialClass
    {
        [Fact]
        public void HaveDefaultStatus()
        {
            var sut = new DataModel();

            Assert.Equal("New", sut.Status);
        }

        [Fact]
        public void ClearStatus()
        {
            var sut = new DataModel();

            sut.ClearStatus();

            Assert.Equal("", sut.Status);
        }
    }
}
