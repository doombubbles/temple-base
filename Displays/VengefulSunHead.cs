using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;

namespace TempleBase.Displays;

public class VengefulSunHead : ModDisplay
{
    public override PrefabReference BaseDisplayReference => Game.instance.model
        .GetTower(TowerType.SuperMonkey, 4)
        .GetAttackModel()
        .GetBehavior<DisplayModel>().display;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        SetMeshTexture(node, Name);
        SetMeshOutlineColor(node, new Color(90, 90, 95));
    }
}