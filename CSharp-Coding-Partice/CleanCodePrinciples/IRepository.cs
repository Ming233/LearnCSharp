using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodePrinciples
{
	public interface IRepository
	{
		int SaveSpeaker(Speaker speaker);
	}
}
