using AutoMapper;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords))
                .ForMember(dest => dest.Checkups, opt => opt.MapFrom(src => src.Checkups))
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

            CreateMap<PatientCreateDTO, Patient>();

            CreateMap<MedicalRecord, MedicalRecordDTO>();
            CreateMap<MedicalRecordCreateDTO, MedicalRecord>();
        }
    }
}
