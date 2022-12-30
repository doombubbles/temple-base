using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using TempleBase;
using TempleBase.SacrificeAttacks;

[assembly: MelonInfo(typeof(TempleBaseMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TempleBase;

public class TempleBaseMod : BloonsTD6Mod
{
    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        HandleSacAttacks();
    }

    public override void OnTowerDestroyed(Tower tower)
    {
        HandleSacAttacks();
    }

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        HandleSacAttacks();
    }

    private static void HandleSacAttacks()
    {
        if (InGame.instance == null) return;

        var simulation = InGame.instance.bridge?.Simulation;
        if (simulation == null) return;

        var sacAttacks = ModContent.GetContent<TempleBaseSacAttack>();

        foreach (var tower in simulation.factory.GetUncast<Tower>().ToList())
        {
            var towerModel = tower.towerModel;
            if (towerModel.GetModTower() is TempleBase)
            {
                var towerOnTop = tower.GetTowerOnTop();

                foreach (var sacAttack in sacAttacks)
                {
                    if (towerOnTop == null ||
                        sacAttack.Tier != towerModel.tiers[2] ||
                        towerOnTop.towerModel.towerSet != sacAttack.TowerSet)
                    {
                        sacAttack.Remove(tower);
                    }
                    else
                    {
                        sacAttack.Apply(tower);
                    }
                }

                // Only allow one tower at a time
                var rootModel = tower.rootModel.Cast<TowerModel>();
                var area = rootModel.GetBehavior<AddMakeshiftAreaModel>();
                if (towerOnTop == null && area == null)
                {
                    rootModel = rootModel.Duplicate();
                    rootModel.AddBehavior(TempleBase.GetAreaModel());
                    tower.UpdateRootModel(rootModel);
                }
                else if (towerOnTop != null && area != null)
                {
                    rootModel = rootModel.Duplicate();
                    rootModel.RemoveBehavior<AddMakeshiftAreaModel>();
                    tower.UpdateRootModel(rootModel);
                }
            }
        }
    }
}