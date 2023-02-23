﻿using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Magic;

public class Magic6 : Magic5
{
    public override TowerSet TowerSet => TowerSet.Magic;
    public override int Tier => 6;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors() =>
        base.GetBehaviors().Select(model => model.Paragon());
}