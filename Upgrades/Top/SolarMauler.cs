using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Top;

public class SolarMauler : TempleBaseUpgrade
{
    public override int Path => Middle;
    public override int Tier => 3;
    public override int Cost => 3000;

    public override string Description => $"{TheTower} does further increased damage to Moab class bloons.";

    public override string Icon => VanillaSprites.MoabMaulerUpgradeIcon;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var damageModifier =
            new DamageModifierForTagModel("DamageModifierForTagModel_", BloonTag.Moabs, 1.1f, 1, false, true);
        var damageSupport = new DamageModifierSupportModel("DamageModifierSupportModel_", true, Id,
            new Il2CppReferenceArray<TowerFilterModel>(0), false, damageModifier);
        damageSupport.ApplyBuffIcon<TempleBaseBuffIcon>();
        damageSupport.AddChildDependant(damageModifier);
        towerModel.AddBehavior(damageSupport);
    }
}