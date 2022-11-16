using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;

namespace TempleBase;

public class TempleBase : ModTower
{
    public override string TowerSet => TowerSetType.Support;
    public override string BaseTower => TowerType.MonkeyVillage;

    public override int Cost => 500;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;

    public override string Icon => Name;
    public override string Portrait => Name;

    private const float Height = 40f;
    private const float Width = 9f;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var footprint = new RectangleFootprintModel("RectangleFootprintModel_", 40, 40, false, false, false);
        towerModel.footprint = footprint;
        towerModel.AddBehavior(footprint);
        towerModel.range = 20;
        towerModel.GetAttackModel().range = 20;

        var area = new AddMakeshiftAreaModel("AddMakeshiftAreaModel_", new Vector3[]
            {
                new(-Width, -Width, Height), new(Width, -Width, Height), new(Width, Width, Height),
                new(-Width, Width, Height)
            }, AreaType.land, new[] { "small", "medium", "large", "XL" }, 0, true, new string[] { }, true,
            new string[] { }, true);
        towerModel.AddBehavior(area);

        var temple = Game.instance.model.GetTower(TowerType.SuperMonkey, 4);
        var templeDisplay = temple.GetBehavior<DisplayModel>().Duplicate();
        towerModel.RemoveBehaviors<DisplayModel>();
        towerModel.AddBehavior(templeDisplay);
        towerModel.display = templeDisplay.display;


        // towerModel.RemoveBehaviors<RangeSupportModel>();
        var rangeSupport = towerModel.GetBehavior<RangeSupportModel>();
        rangeSupport.mutatorId = "TempleBase-Range";
    }
}