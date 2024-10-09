using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;
using TempleBase.Upgrades;

namespace TempleBase;

public class TempleBase : ModTower
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.MonkeyVillage;
    public override ParagonMode ParagonMode => ParagonMode.Base555;

    public override int Cost => 500;

    public override string Icon => Name;
    public override string Portrait => Name;

    public const float Height = 40f;
    public const float Width = 13f;

    public override string Description =>
        $"Place a tower on top of this to designate it '{TempleBaseUpgrade.TheTower}'.\n" +
        $"By default, increases {TempleBaseUpgrade.TheTower}'s range.";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var footprint = new RectangleFootprintModel("RectangleFootprintModel_", 40, 40, false, false, false);
        towerModel.footprint = footprint;
        towerModel.AddBehavior(footprint);
        towerModel.range = Width;
        towerModel.GetAttackModel().range = Width;

        towerModel.AddBehavior(GetAreaModel());

        var temple = Game.instance.model.GetTower(TowerType.SuperMonkey, towerModel.tier >= 5 ? 5 : 4);
        var templeDisplay = temple.GetBehavior<DisplayModel>().Duplicate();
        towerModel.RemoveBehaviors<DisplayModel>();
        towerModel.AddBehavior(templeDisplay);
        towerModel.display = templeDisplay.display;


        // towerModel.RemoveBehaviors<RangeSupportModel>();
        var rangeSupport = towerModel.GetBehavior<RangeSupportModel>();
        rangeSupport.mutatorId = "TempleBase-Range";
        rangeSupport.ApplyBuffIcon<TempleBaseBuffIcon>();

        towerModel.RemoveBehavior<TransformTowerXpModel>();
    }

    public static AddMakeshiftAreaModel GetAreaModel() => new("AddMakeshiftAreaModel_", new Vector3[]
        {
            new(-Width, -Width, Height), new(Width, -Width, Height), new(Width, Width, Height),
            new(-Width, Width, Height)
        }, AreaType.land, new[] { "small", "medium", "large", "XL" }, 0, true, new string[] { }, true,
        null, true);

    public override bool IsValidCrosspath(int[] tiers) =>
        ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
}