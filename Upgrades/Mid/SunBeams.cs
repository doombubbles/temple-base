using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Mid;

public class SunBeams : TempleBaseUpgrade
{
    public override int Path => Middle;
    public override int Tier => 3;
    public override int Cost => 2000;

    public override string Description => $"{TheTower} can damage all Bloon types.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var support = new DamageTypeSupportModel("DamageTypeSupportModel_TempleBase", true, Id, BloonProperties.None,
            new Il2CppReferenceArray<TowerFilterModel>(0), "", "");
        support.ApplyBuffIcon<TempleBaseBuffIcon>();
        towerModel.AddBehavior(support);
    }
}