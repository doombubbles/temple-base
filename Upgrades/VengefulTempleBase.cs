using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

namespace TempleBase.Upgrades;

public class VengefulTempleBase : ModParagonUpgrade<TempleBase>
{
    public override int Cost => 600000;

    public override string DisplayName => "Vengeful Temple Base";

    public override bool RemoveAbilities => false;

    public override string Description =>
        $"{TempleBaseUpgrade.TheTower}s combine forces atop this ancient monument to unbridled vengeance.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        // Ensure all 3 towers in range
        towerModel.range = 20;
        
        // Use dark display
        var displayModel = towerModel.GetBehavior<DisplayModel>();
        var display = CosmeticHelper.SwapDarkTempleAsset(displayModel.display);
        towerModel.display = displayModel.display = display;
        var paragonTowerModel = towerModel.GetBehavior<ParagonTowerModel>();
        foreach (var displayDegreePath in paragonTowerModel.displayDegreePaths)
        {
            displayDegreePath.assetPath = display;
        }

        // Hack to counteract display delay
        paragonTowerModel.effectDuringModel.assetId = display;
        paragonTowerModel.endEffectModel.assetId = display;
        
        // Buff icons
        foreach (var supportModel in towerModel.GetBehaviors<SupportModel>())
        {
            supportModel.ApplyBuffIcon<VengefulTempleBaseBuffIcon>();
        }
        var icon = GetInstance<VengefulTempleBaseBuffIcon>();
        var discountZoneModel = towerModel.GetBehavior<DiscountZoneModel>();
        discountZoneModel.buffLocsName = icon.Icon;
        discountZoneModel.buffIconName = icon.Id;
        
        // Daybreak
        towerModel.GetBehavior<DamageSupportModel>().mutatorId += "Vengeful";
        
        // Ability
        var ability = towerModel.GetBehavior<AbilityModel>();
        ability.icon = IconReference;
        ability.GetBehavior<ActivateRangeSupportZoneModel>().mutatorId += "Vengeful";
    }
}