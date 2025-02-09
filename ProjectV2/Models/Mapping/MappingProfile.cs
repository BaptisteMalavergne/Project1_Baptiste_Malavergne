/*using AutoMapper;
using ProjectV2.Models;
using ProjectV2.Models.DTO;

namespace ProjectV2.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientReadDTO>()
                .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords)); // Mapping des MedicalRecords

            CreateMap<MedicalRecord, MedicalRecordDTO>();
        }
    }
}
    */