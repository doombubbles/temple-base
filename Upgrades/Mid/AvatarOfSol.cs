using System.Linq;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using TempleBase.Displays;

namespace TempleBase.Upgrades.Mid;

public class AvatarOfSol : TempleBaseUpgrade
{
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override int Cost => 20000;

    private const int Duration = 15;
    private const int Cooldown = 60;

    public override string Description =>
        $"Ability: {TheTower} becomes The Avatar of Sol for {Duration}s. {Cooldown}s cooldown.";

    public override string Icon => VanillaSprites.SunTempleUpgradeIcon;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var ability = new AbilityModel($"AbilityModel_{Name}", DisplayName, Description, 1, 0, IconReference, Cooldown,
            new Il2CppReferenceArray<Model>(0), false, false, null, 0, 0, 9999, false, false);
        towerModel.AddBehavior(ability);

        var activate = new ActivateRangeSupportZoneModel($"ActivateRangeSupportZoneModel_{Id}", Id, true, 0, 65,
            20, 99, false, Duration, new Il2CppReferenceArray<TowerFilterModel>(0));

        ability.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel_",
            new SoundModel("SoundModel_", SunTemple.GetBehavior<CreateSoundOnAttachedModel>().sound.assetId), null,
            null));

        ability.AddBehavior(activate);
    }

    [HarmonyPatch(typeof(RangeMutator), nameof(RangeMutator.Mutate))]
    internal static class RangeMutator_Mutate
    {
        [HarmonyPostfix]
        private static void Postfix(RangeMutator __instance, Model model)
        {
            if (__instance.id.Contains(nameof(AvatarOfSol)) &&
                model.Is(out TowerModel towerModel) &&
                !towerModel.isSubTower)
            {
                var attackModel = SunTemple.GetAttackModel().Duplicate();
                attackModel.GetBehavior<DisplayModel>().ApplyDisplay<SunHead>();
                attackModel.GetBehavior<RotateToTargetModel>().onlyRotateDuringThrow = false;
                attackModel.weapons[0].RemoveBehavior<CheckTempleCanFireModel>();

                foreach (var templeTowerMutatorGroupModel in SunTemple.GetBehaviors<TempleTowerMutatorGroupModel>()
                             .Where(groupModel => groupModel.towerSet == towerModel.towerSet))
                {
                    templeTowerMutatorGroupModel.ApplyToAttack(attackModel);
                }

                towerModel.AddBehavior(attackModel);
            }
        }
    }
}