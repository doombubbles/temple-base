using Il2CppAssets.Scripts.Models.Towers;

namespace TempleBase.Upgrades.Bot;

public class TempleBoons : TempleBaseUpgrade
{
    public override int Path => Bottom;
    public override int Tier => 3;
    public override int Cost => 20000;

    public override string Description =>
        $"Activate some of the Sun Temple sacrifice attacks for the {TheTower}'s tower set.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
    }
}