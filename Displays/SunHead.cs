using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Utils;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;

namespace TempleBase.Displays;

public class SunHead : ModDisplay
{
    public override PrefabReference BaseDisplayReference => Game.instance.model
        .GetTower(TowerType.SuperMonkey, 4)
        .GetAttackModel()
        .GetBehavior<DisplayModel>().display;

    public override float Scale => .85f;
}