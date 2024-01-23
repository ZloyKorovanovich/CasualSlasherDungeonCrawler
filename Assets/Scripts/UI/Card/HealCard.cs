public class HealCard : Card
{
    protected override void Use()
    {
        var health = _manager.player.GetComponent<CharacterHealth>();
        health?.Heal(health.maxHealth);
        base.Use();
    }
}
