using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Top;

public class PiercingLight : TempleBaseUpgrade
{
    public override int Path => TOP;
    public override int Tier => 1;
    public override int Cost => 750;

    public override string Description => $"Increases {TheTower}'s pierce by 2.";

    public override string Icon => VanillaSprites.TheLongArmofLightAA;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var pierceSupportModel = new PierceSupportModel("PierceSupportModel_TempleBase", true, 2, Id,
            new Il2CppReferenceArray<TowerFilterModel>(0), false, "", "");
        pierceSupportModel.ApplyBuffIcon<TempleBaseBuffIcon>();
        towerModel.AddBehavior(pierceSupportModel);
    }
}