using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodePrinciples
{
	public class RegisterResponse
	{
		public RegisterResponse(int speakerId)
		{
			this.SpeakerId = speakerId;
		}

		public RegisterResponse(RegisterError? error)
		{
			this.Error = error;
		}

		public int? SpeakerId { get; set; }
		public RegisterError? Error { get; set; }
	}

	public enum RegisterError
	{
		FirstNameRequired,
		LastNameRequired,
		EmailRequired,
		NoSessionsProvided,
		NoSessionsApproved,
		SpeakerDoesNotMeetStandards
	};
}
