using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Mid;

public class SolarFlares : TempleBaseUpgrade
{
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 500;

    public override string Description =>
        $"Allows {TheTower} to target Camo Bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var visibility = new VisibilitySupportModel("VisibilitySupportModel_TempleBase", true, Id,
            false, new Il2CppReferenceArray<TowerFilterModel>(0), "", "");
        visibility.ApplyBuffIcon<TempleBaseBuffIcon>();
        towerModel.AddBehavior(visibility);
    }
}