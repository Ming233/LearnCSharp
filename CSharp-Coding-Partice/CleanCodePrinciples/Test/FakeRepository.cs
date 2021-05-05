using CleanCodePrinciples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLuau.Tests
{
    class FakeRepository : IRepository
    {
        public int SaveSpeaker(Speaker speaker)
        {
            // Simulate saving a speaker and returning the ID since this is a simiple fake repository.
            return 1;
        }
    }
}
