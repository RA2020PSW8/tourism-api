using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubFight : Entity
{
    public int? WinnerId { get; init; }
    public DateTime StartOfFight { get; init; }
    public DateTime EndOfFight { get; init; }
    public long Club1Id { get; init; }
    public long Club2Id { get; init; }
    public Club? Club1 { get; init; }
    public Club? Club2 { get; init; }
    public bool IsInProgress { get; init; }

    public ClubFight(int? winnerId, DateTime startOfFight, long club1Id, long club2Id, bool isInProgress)
    {
        WinnerId = winnerId;
        StartOfFight = startOfFight;
        EndOfFight = startOfFight.AddDays(3);
        Club1Id = club1Id;
        Club2Id = club2Id;
        IsInProgress = isInProgress;
    }
}