using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks.Support;

public class Support4 : TempleBaseSacAttack
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override int Tier => 4;

    protected override IEnumerable<TowerBehaviorModel> GetBehaviors()
    {
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<PierceSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<RangeSupportModel>().FixedSacSupport();
        if (TempleBaseMod.NerfedIncome)
        {
            yield return GetSacrificeEffect(SunTemple, 7501).GetDescendant<PerRoundCashBonusTowerModel>().Duplicate();
        }
        else
        {
            yield return GetSacrificeEffect(SunTemple, 25001).GetDescendant<PerRoundCashBonusTowerModel>().Duplicate();
        }

        yield return GetSacrificeEffect(SunTemple, 25001).GetDescendant<RateSupportModel>().FixedSacSupport();
        yield return GetSacrificeEffect(SunTemple, 50001).GetDescendant<DamageSupportModel>().FixedSacSupport();
    }
}