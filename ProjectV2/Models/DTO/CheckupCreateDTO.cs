namespace ProjectV2.Models.DTO
{
    public class CheckupCreateDTO
    {
        public DateTime CheckupDate { get; set; }
        public string ProcedureCode { get; set; }
        public int PatientId { get; set; }
    }
}
