using System.Linq;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

namespace TempleBase.Upgrades.Mid;

public class SolIncarnate : TempleBaseUpgrade
{
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 70000;

    private const int Duration = 15;
    private const int Cooldown = 45;

    public override string Description =>
        $"Ability: {TheTower} becomes Sol Incarnate for {Duration}s. {Cooldown}s cooldown.";

    public override string Icon => VanillaSprites.TrueSonGodUpgradeIcon;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var abilityModel = towerModel.GetAbility();

        abilityModel.name = $"AbilityModel_{Name}";
        abilityModel.description = Description;
        abilityModel.displayName = DisplayName;
        abilityModel.icon = IconReference;
        abilityModel.Cooldown = Cooldown;

        var supportZoneModel = abilityModel.GetBehavior<ActivateRangeSupportZoneModel>();
        supportZoneModel.mutatorId = Id;
        supportZoneModel.lifespan = Duration;
    }

    [HarmonyPatch(typeof(RangeMutator), nameof(RangeMutator.Mutate))]
    internal static class RangeMutator_Mutate
    {
        [HarmonyPostfix]
        private static void Postfix(RangeMutator __instance, Model model)
        {
            if (__instance.id.Contains(nameof(SolIncarnate)) && model.Is(out TowerModel towerModel))
            {
                var display = SunTemple.GetAttackModel().GetBehavior<DisplayModel>().display;
                var attackModel = SunGod.GetAttackModel().Duplicate();
                attackModel.GetBehavior<DisplayModel>().display = display;
                attackModel.weapons[0].ejectZ /= 2;
                attackModel.GetBehavior<RotateToTargetModel>().onlyRotateDuringThrow = false;
                var proj = attackModel.weapons[0].projectile;
                proj.radius *= 1.5f;
                proj.scale *= 1.25f;
                attackModel.weapons[0].RemoveBehavior<CheckTempleCanFireModel>();
                
                foreach (var templeTowerMutatorGroupModel in SunTemple.GetBehaviors<TempleTowerMutatorGroupModel>())
                {
                    templeTowerMutatorGroupModel.ApplyToAttack(attackModel);
                }

                foreach (var templeTowerMutatorGroupModel in SunGod.GetBehaviors<TempleTowerMutatorGroupModel>()
                             .Where(groupModel => groupModel.towerSet == towerModel.towerSet))
                {
                    templeTowerMutatorGroupModel.ApplyToAttack(attackModel);
                }

                towerModel.AddBehavior(attackModel);
            }
        }
    }
}