using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Military;

public class Military3 : TempleBaseSacAttack
{
    public override TowerSet TowerSet => TowerSet.Military;
    public override int Tier => 3;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors()
    {
        yield return GetSacrificeEffect(SunTemple, 2001).GetDescendant<AttackModel>().FixedSacAttack();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<AttackModel>().FixedSacAttack();
    }
}