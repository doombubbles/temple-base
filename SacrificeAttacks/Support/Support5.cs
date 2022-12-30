﻿using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Support;

public class Support5 : TempleBaseSacAttack
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override int Tier => 5;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors()
    {
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<PierceSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<RangeSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 25001).GetDescendant<PerRoundCashBonusTowerModel>().Duplicate();
        yield return GetSacrificeEffect(SunTemple, 25001).GetDescendant<RateSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 50001).GetDescendant<DamageSupportModel>().FixedSacSupport();

        
        yield return GetSacrificeEffect(SunGod, 7501).GetDescendant<PierceSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunGod, 7501).GetDescendant<RangeSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunGod, 25001).GetDescendant<PerRoundCashBonusTowerModel>().Duplicate();
        yield return GetSacrificeEffect(SunGod, 25001).GetDescendant<RateSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunGod, 50001).GetDescendant<DamageSupportModel>().FixedSacSupport();
    }
}