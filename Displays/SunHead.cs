using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace TempleBase.Displays;

public class SunHead : ModDisplay
{
    public override PrefabReference BaseDisplayReference => Game.instance.model
        .GetTower(TowerType.SuperMonkey, 4)
        .GetAttackModel()
        .GetBehavior<DisplayModel>().display;

    public override float Scale => .85f;
}