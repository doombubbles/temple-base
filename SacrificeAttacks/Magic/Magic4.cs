﻿using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Magic;

public class Magic4 : TempleBaseSacAttack
{
    public override TowerSet TowerSet => TowerSet.Magic;
    public override int Tier => 4;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors()
    {
        yield return GetSacrificeEffect(SunTemple, 15001).GetDescendant<AttackModel>().FixedSacAttack();
        yield return GetSacrificeEffect(SunTemple, 25001).GetDescendant<AttackModel>().FixedSacAttack();
        yield return GetSacrificeEffect(SunTemple, 50001).GetDescendant<AttackModel>().FixedSacAttack();
    }
}