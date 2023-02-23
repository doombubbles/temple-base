using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Support;

public class Support6 : Support5
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override int Tier => 6;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors() =>
        base.GetBehaviors().Select(model => model.Paragon());
}