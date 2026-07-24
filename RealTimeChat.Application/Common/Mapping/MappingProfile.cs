using AutoMapper;
using RealTimeChat.Application.Conversations;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Conversation, ConversationResponse>();

            CreateMap<ConversationMember, ConversationMemberResponse>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.User.FullName))
                .ForMember(d => d.ProfilePictureUrl, o => o.MapFrom(s => s.User.ProfilePictureUrl))
                .ForMember(d => d.IsOnline, o => o.MapFrom(s => s.User.IsOnline));
        }
    }
}
