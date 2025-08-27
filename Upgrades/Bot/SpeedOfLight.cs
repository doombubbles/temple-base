using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Bot;

public class SpeedOfLight : TempleBaseUpgrade
{
    public override int Path => Bottom;
    public override int Tier => 1;
    public override int Cost => 500;

    public override string Description => $"Increases {TheTower}'s attack speed.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var rateSupportModel = new RateSupportModel("RateSupportModel_TempleBase", .85f, true, Id, false, 0,
            new Il2CppReferenceArray<TowerFilterModel>(0), "", "");
        rateSupportModel.ApplyBuffIcon<TempleBaseBuffIcon>();
        towerModel.AddBehavior(rateSupportModel);
    }
}