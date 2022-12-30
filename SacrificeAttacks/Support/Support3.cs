﻿using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Support;

public class Support3 : TempleBaseSacAttack
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override int Tier => 3;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors()
    {
        yield return GetSacrificeEffect(SunTemple, 2001).GetDescendant<RateSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<PierceSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<RangeSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<PerRoundCashBonusTowerModel>().Duplicate();
    }
}