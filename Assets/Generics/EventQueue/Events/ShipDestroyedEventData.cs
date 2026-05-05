public class ShipDestroyedEventData : EventData
{
    public readonly int ScoreToAdd;
    public readonly ETeams Team;
    public readonly int InstanceId;

    public ShipDestroyedEventData(int scoreToAdd, ETeams team, int instanceId) : base(EEventIds.ShipDestroyed)
    {
        this.ScoreToAdd = scoreToAdd;
        Team = team;
        InstanceId = instanceId;
    }
}
