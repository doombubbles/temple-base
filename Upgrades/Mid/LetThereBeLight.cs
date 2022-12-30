using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;

namespace TempleBase.Upgrades.Mid;

public class LetThereBeLight : TempleBaseUpgrade
{
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override int Cost => 1000;

    public override string Description =>
        $"Further increases {TheTower}'s range and allows it to attack through walls.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetBehavior<RangeSupportModel>().multiplier *= 2;

        towerModel.GetBehavior<VisibilitySupportModel>().mutatorId += "XRAY";
    }

    [HarmonyPatch(typeof(VisibilitySupport.MutatorTower), nameof(VisibilitySupport.MutatorTower.Mutate))]
    internal static class VisibilitySupport_MutatorTower
    {
        [HarmonyPostfix]
        private static void Postfix(VisibilitySupport.MutatorTower __instance, Model model)
        {
            if (__instance.id.Contains("XRAY"))
            {
                model.GetDescendants<AttackModel>().ForEach(attackModel => attackModel.attackThroughWalls = true);
                model.GetDescendants<ProjectileModel>().ForEach(projectile => projectile.ignoreBlockers = true);
            }
        }
    }
}