using Il2CppAssets.Scripts.Models.Towers;

namespace TempleBase.Upgrades.Bot;

public class TempleBlessings : TempleBaseUpgrade
{
    public override int Path => Bottom;
    public override int Tier => 4;
    public override int Cost => 50000;

    public override string Description =>
        $"Activate the rest of the Sun Temple sacrifice attacks for the {TheTower}'s tower set.";
    
    public override void ApplyUpgrade(TowerModel towerModel)
    {
    }
}