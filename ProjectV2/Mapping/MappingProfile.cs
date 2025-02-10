using AutoMapper;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Patient mappings
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords))
                .ForMember(dest => dest.Checkups, opt => opt.MapFrom(src => src.Checkups))
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

            CreateMap<PatientCreateDTO, Patient>();

            // MedicalRecord mappings
            CreateMap<MedicalRecord, MedicalRecordDTO>();
            CreateMap<MedicalRecordCreateDTO, MedicalRecord>();

            // Checkup mappings
            CreateMap<Checkup, CheckupDTO>()
                .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)));

            CreateMap<CheckupCreateDTO, Checkup>();
        }
    }
}