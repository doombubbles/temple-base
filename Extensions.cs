using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Mutators;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper.Extensions;

namespace TempleBase;

public static class Extensions
{
    public static void SetSelectable(this TargetSupplierModel m, bool value)
    {
        if (m.Is(out TargetFirstModel first))
        {
            first.isSelectable = value;
        }
        else if (m.Is(out TargetLastModel last))
        {
            last.isSelectable = value;
        }
        else if (m.Is(out TargetStrongModel strong))
        {
            strong.isSelectable = value;
        }
        else if (m.Is(out TargetCloseModel close))
        {
            close.isSelectable = value;
        }
        else if (m.Is(out TargetFirstPrioCamoModel firstCamo))
        {
            firstCamo.isSelectable = value;
        }
        else if (m.Is(out TargetLastPrioCamoModel lastCamo))
        {
            lastCamo.isSelectable = value;
        }
        else if (m.Is(out TargetStrongPrioCamoModel strongCamo))
        {
            strongCamo.isSelectable = value;
        }
        else if (m.Is(out TargetClosePrioCamoModel closeCamo))
        {
            closeCamo.isSelectable = value;
        }
        else if (m.Is(out TargetEliteTargettingModel elite))
        {
            elite.isSelectable = value;
        }
    }

    public static AttackModel FixedSacAttack(this AttackModel attackModel)
    {
        var getFixedBoi = attackModel.Duplicate();
        getFixedBoi.RemoveBehavior<UseTowerRangeModel>();
        getFixedBoi.GetBehaviors<TargetSupplierModel>().ForEach(model => model.SetSelectable(true));
        getFixedBoi.weapons.ForEach(weaponModel => weaponModel.RemoveBehavior<UseParentEjectModel>());
        getFixedBoi.weapons.ForEach(weaponModel => weaponModel.RemoveBehavior<CheckTempleCanFireModel>());
        return getFixedBoi;
    }
    
    public static SupportModel FixedSacSupport(this SupportModel supportModel)
    {
        var getFixedBoi = supportModel.Duplicate();
        getFixedBoi.isGlobal = true;
        getFixedBoi.onlyShowBuffIfMutated = true;
        return getFixedBoi;
    }

    public static Tower? GetTowerOnTop(this Tower tower)
    {
        var towerManger = InGame.instance.GetTowerManager();
        var towerModel = tower.towerModel;
        return towerManger
            .GetClosestTowers(tower.Position.ToVector2(), -1, null, towerModel.range)
            .Where(t => t.parentTowerId.Id == -1)
            .FirstOrDefault(t => t.Id != tower.Id);
    }

    public static void ApplyToAttack(this TempleTowerMutatorGroupModel templeTowerMutatorGroupModel,
        AttackModel attackModel)
    {
        if (templeTowerMutatorGroupModel.mutators == null) return;
        var weapon = attackModel.weapons[0];
        var proj = weapon.projectile;
        var damage = proj.GetDamageModel();
        foreach (var mutator in templeTowerMutatorGroupModel.mutators.Where(mutator =>
                     mutator.conditionalId == null))
        {
            if (mutator.Is(out PierceTowerMutatorModel pierceTower))
            {
                proj.pierce += pierceTower.pierce;
            }
            else if (mutator.Is(out DamageTowerMutatorModel damageTower))
            {
                damage.damage += damageTower.damage;
            }
            else if (mutator.Is(out ReloadTimeTowerMutatorModel reloadTime))
            {
                weapon.Rate *= 1 - reloadTime.multiplier;
            }
            else if (mutator.Is(out ProjectileSizeTowerMutatorModel projectileSize))
            {
                proj.radius *= 1 + projectileSize.sizeModifier;
                proj.scale *= 1 + projectileSize.assetSizeModifier;
            }
            else if (mutator.Is(out ProjectileSpeedTowerMutatorModel projectileSpeed))
            {
                proj.GetBehavior<TravelStraitModel>().Speed *= 1 + projectileSpeed.speedModifier;
            }
            else if (mutator.Is(out RangeTowerMutatorModel range))
            {
                attackModel.range += range.rangeIncrease;
            }
            else if (mutator.Is(out WindChanceTowerMutatorModel windChance))
            {
                proj.GetBehavior<WindModel>().chance += windChance.windChance;
            }
            else if (mutator.Is(out AddBehaviorToTowerMutatorModel addBehavior))
            {
                foreach (var addedBehavior in addBehavior.behaviors)
                {
                    if (addedBehavior.Is(out RateSupportModel rateSupport))
                    {
                        weapon.Rate *= rateSupport.multiplier;
                    }
                    else if (addedBehavior.Is(out PierceSupportModel pierceSupport))
                    {
                        proj.pierce += pierceSupport.pierce;
                    }
                    else if (addedBehavior.Is(out RangeSupportModel rangeSupport))
                    {
                        attackModel.range += rangeSupport.additive;
                        attackModel.range *= 1 + rangeSupport.multiplier;
                    }
                    else if (addedBehavior.Is(out DamageSupportModel damageSupport))
                    {
                        proj.GetDamageModel().damage += damageSupport.increase;
                    }
                }
            }
        }
    }
}