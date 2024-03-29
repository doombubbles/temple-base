﻿using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TempleBase.Upgrades.Top;

public class SunBurn : TempleBaseUpgrade
{
    public override int Path => TOP;
    public override int Tier => 2;
    public override int Cost => 1500;

    public override string Description => $"Increases the damage of {TheTower} by 1";

    public override string Icon => VanillaSprites.FireballUpgradeIcon;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var damageSupport = new DamageSupportModel("DamageSupportModel_TempleBase", true, 1, Id,
            new Il2CppReferenceArray<TowerFilterModel>(0), false, false, 0);
        damageSupport.ApplyBuffIcon<TempleBaseBuffIcon>();
        towerModel.AddBehavior(damageSupport);
    }
}