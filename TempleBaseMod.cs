using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.SMath;
using TempleBase;
using TempleBase.SacrificeAttacks;

[assembly: MelonInfo(typeof(TempleBaseMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TempleBase;

public class TempleBaseMod : BloonsTD6Mod
{
    public static readonly ModSettingBool NerfedIncome = new(true)
    {
        description = "Somewhat nerfs the income generation of the Bottom Path Upgrades when used with a Support Tower"
    };
    
    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        HandleAllModelChanges();
    }

    public override void OnTowerDestroyed(Tower tower)
    {
        HandleAllModelChanges();
    }

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        HandleAllModelChanges();
    }

    private static void HandleAllModelChanges()
    {
        if (InGame.instance == null) return;

        var simulation = InGame.instance.bridge?.Simulation;
        if (simulation == null) return;

        var templeBases = simulation.factory.GetUncast<Tower>()
            .ToList()
            .Where(model => model.towerModel.GetModTower() is TempleBase);

        foreach (var tower in templeBases)
        {
            HandleSacAttacks(tower);
            EnforceOneTowerPolicy(tower);
            EmbiggenTempleTemples(tower);
        }
    }

    private static void HandleSacAttacks(Tower tower)
    {
        var towerOnTop = tower.GetTowerOnTop();
        var paragon = tower.towerModel.isParagon;
        var templeBaseTier = paragon ? 6 : tower.towerModel.tiers[2];
        var sacAttacks = ModContent.GetContent<TempleBaseSacAttack>();

        var allowedSets = new List<TowerSet>();
        if (paragon)
        {
            allowedSets.AddRange(tower.GetAllTowersOnTop().Select(t => t.towerModel.towerSet));
        }
        else if (towerOnTop != null)
        {
            allowedSets.Add(towerOnTop.towerModel.towerSet);
        }

        foreach (var sacAttack in sacAttacks)
        {
            if (towerOnTop == null ||
                sacAttack.Tier != templeBaseTier ||
                !allowedSets.Contains(sacAttack.TowerSet))
            {
                sacAttack.Remove(tower);
            }
            else
            {
                sacAttack.Apply(tower);
            }
        }
    }

    private static void EnforceOneTowerPolicy(Tower tower)
    {
        var towerOnTop = tower.GetTowerOnTop();

        var rootModel = tower.rootModel.Cast<TowerModel>();
        var hasArea = rootModel.GetBehavior<AddMakeshiftAreaModel>() != null;
        var hasTower = towerOnTop != null && !towerOnTop.towerModel.HasBehavior<MonkeyTempleModel>();

        if (hasTower == hasArea)
        {
            rootModel = rootModel.Duplicate();
            if (hasArea) rootModel.RemoveBehavior<AddMakeshiftAreaModel>();
            else rootModel.AddBehavior(TempleBase.GetAreaModel());
            tower.UpdateRootModel(rootModel);
        }
    }

    private static void EmbiggenTempleTemples(Tower tower)
    {
        var towerOnTop = tower.GetTowerOnTop();
        var big = towerOnTop != null && towerOnTop.towerModel.HasBehavior<MonkeyTempleModel>();

        var scale = InGame.instance.bridge.Model.globalTowerScale * (big ? 1.2f : 1f);
        if (tower.Scale.X != scale)
        {
            tower.Scale = new Vector3Boxed(scale, scale, scale);
        }
    }
}