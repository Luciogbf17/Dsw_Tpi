namespace TpiDSW.Api.Models.Auth
{
    public class PatientLoginRequest
    {
        public string Email { get; set; } = string.Empty;

        public int Dni { get; set; }
    }
}