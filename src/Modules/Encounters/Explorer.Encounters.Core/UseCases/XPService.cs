using AutoMapper.Configuration.Conventions;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class XPService : IXPService
    {
        private IEncounterCompletionRepository _encounterCompletionRepository;
        private IInternalClubService _clubService;
        private IInternalClubFightService _clubFightService;

        public XPService(IEncounterCompletionRepository encounterCompletionRepository, IInternalClubService clubService, IInternalClubFightService clubFightService)
        {
            _encounterCompletionRepository = encounterCompletionRepository;
            _clubService = clubService;
            _clubFightService = clubFightService;   
        }

        public Result<ClubFightXPInfoDto> GetClubFightXPInfo(int clubFightId)
        {
            ClubFightDto fight = _clubFightService.Get(clubFightId).ValueOrDefault;
            if (fight == null) return Result.Fail(FailureCode.NotFound).WithError("Fight not found");

            ClubDto club1 = _clubService.GetWithMembers(fight.Club1Id).ValueOrDefault;
            ClubDto club2 = _clubService.GetWithMembers(fight.Club2Id).ValueOrDefault;
            if(club1 == null || club2 == null) return Result.Fail(FailureCode.NotFound).WithError("Club not found");

            ClubFightXPInfoDto clubFightXPInfo = new ClubFightXPInfoDto()
            {
                Club1ParticipantsInfo = new List<FightParticipantInfoDto>(),
                Club2ParticipantsInfo = new List<FightParticipantInfoDto>()
            };

            foreach (var member in club1.Members)
            {
                int memberXp = _encounterCompletionRepository.GetTotalXPInDateRangeByUser(member.UserId, fight.StartOfFight, fight.EndOfFight);

                clubFightXPInfo.Club1ParticipantsInfo.Add(ConvertToFightParticipant(member, memberXp));
            }
            clubFightXPInfo.Club1ParticipantsInfo = clubFightXPInfo.Club1ParticipantsInfo.OrderByDescending(pi => pi.XPInFight).ToList();
            clubFightXPInfo.club1TotalXp = clubFightXPInfo.Club1ParticipantsInfo.Select(pi => pi.XPInFight).Sum();

            foreach (var member in club2.Members)
            {
                int memberXp = _encounterCompletionRepository.GetTotalXPInDateRangeByUser(member.UserId, fight.StartOfFight, fight.EndOfFight);

                clubFightXPInfo.Club2ParticipantsInfo.Add(ConvertToFightParticipant(member, memberXp));
            }
            clubFightXPInfo.Club2ParticipantsInfo = clubFightXPInfo.Club2ParticipantsInfo.OrderByDescending(pi => pi.XPInFight).ToList();
            clubFightXPInfo.club2TotalXp = clubFightXPInfo.Club2ParticipantsInfo.Select(pi => pi.XPInFight).Sum();

            return clubFightXPInfo;
        }

        public FightParticipantInfoDto ConvertToFightParticipant(PersonDto person, int xp)
        {
            FightParticipantInfoDto fightParticipantInfoDto = new FightParticipantInfoDto()
            {
                Username = person.Name + " " + person.Surname, // it's just name + surname because I'm lazy to pull username from User table, and person is not connected to user >:(
                ProfileImage = person.ProfileImage,
                Level = person.Level,
                XPInFight = xp
            };

            return fightParticipantInfoDto;
        }
    }
}
