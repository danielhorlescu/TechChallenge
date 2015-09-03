using AutoMapper;
using Gambling.Domain;
using Gambling.Persistance;
using Gambling.Portal.Models;

namespace Gambling.Portal
{
    public class PresentationMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BetDto, Bet>()
                .ForMember(dest => dest.Status, src => src.Ignore());

            Mapper.CreateMap<Bet, UnSettledBetViewModel>()
                .ForMember(dest => dest.IsRisky, src => src.MapFrom(s=>s.Status.IsRisky))
                .ForMember(dest => dest.IsUnusual, src => src.MapFrom(s=>s.Status.IsUnusual))
                .ForMember(dest => dest.IsHighlyUnusual, src => src.MapFrom(s => s.Status.IsHighlyUnusual))
                .ForMember(dest => dest.HasHighWinAmount, src => src.MapFrom(s => s.Status.HasHighWinAmount))
                .ForMember(dest => dest.CustomerId, src => src.Ignore());

            Mapper.CreateMap<BetDto, SettledBetViewModel>();
                
            base.Configure();
        }
    }
}