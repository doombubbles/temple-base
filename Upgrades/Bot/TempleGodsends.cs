using Il2CppAssets.Scripts.Models.Towers;

namespace TempleBase.Upgrades.Bot;

public class TempleGodsends : TempleBaseUpgrade
{
    public override int Path => BOTTOM;
    public override int Tier => 5;
    public override int Cost => 100000;

    public override string Description =>
        $"Activate the full True Sun God sacrifice attacks for {TheTower}'s tower set.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
    }
}