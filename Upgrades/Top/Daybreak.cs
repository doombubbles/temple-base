﻿using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2Cpp;

namespace TempleBase.Upgrades.Top;

public class Daybreak : TempleBaseUpgrade
{
    public override int Path => TOP;
    public override int Tier => 5;
    public override int Cost => 90000;

    public override string Description => "The power of the sun in the palm of your hand...";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetBehavior<DamageSupportModel>().mutatorId = Id;
    }

    [HarmonyPatch(typeof(DamageSupport.MutatorTower), nameof(DamageSupport.MutatorTower.Mutate))]
    internal static class DamageSupport_Mutate
    {
        [HarmonyPostfix]
        private static void Postfix(DamageSupport.MutatorTower __instance, Model model)
        {
            if (__instance.id.Contains(nameof(Daybreak)))
            {
                foreach (var projectileModel in model.GetDescendants<ProjectileModel>().ToList())
                {
                    if (projectileModel.HasBehavior(out DamageModel damageModel))
                    {
                        var bomb = Game.instance.model.GetTower(TowerType.BombShooter).GetWeapon().projectile
                            .Duplicate();
                        var solarCircle = SunGod.GetAttackModel().weapons[0].projectile.display;
                        var pb = bomb.GetBehavior<CreateProjectileOnContactModel>();
                        var effect = bomb.GetBehavior<CreateEffectOnContactModel>().effectModel;
                        effect.assetId = solarCircle;
                        effect.lifespan /= 20f;

                        pb.projectile.pierce = 5 + projectileModel.pierce;
                        pb.projectile.GetDamageModel().damage = 1 + damageModel.damage;
                        pb.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;

                        foreach (var behavior in projectileModel.behaviors)
                        {
                            if (behavior.Is(out DamageModifierModel damageModifierModel))
                            {
                                pb.projectile.AddBehavior(damageModifierModel.Duplicate());
                            }
                        }

                        var createProj = new CreateProjectileOnExhaustFractionModel(
                            "CreateProjectileOnExhaustFractionModel_",
                            pb.projectile, pb.emission, .01f, 1f, true, false);
                        projectileModel.AddBehavior(createProj);

                        var createEffect = new CreateEffectOnExhaustFractionModel("CreateEffectOnExhaustFractionModel_",
                            effect.assetId, effect, 0, false, .01f, 1f, false);
                        projectileModel.AddBehavior(createEffect);
                    }
                }
            }
        }
    }
}