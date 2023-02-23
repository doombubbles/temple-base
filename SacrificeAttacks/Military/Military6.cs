using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Military;

public class Military6 : Military5
{
    public override TowerSet TowerSet => TowerSet.Military;
    public override int Tier => 6;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors() =>
        base.GetBehaviors().Select(model => model.Paragon());
}