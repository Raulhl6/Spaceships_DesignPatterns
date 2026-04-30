public interface IDamageable
{
        ETeams Team { get; }
        void AddDamage(int amount);
}