using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;

namespace TempleBase;

internal static class AllowTempleSacrificing
{
    public static bool sacrificing;

    [HarmonyPatch(typeof(MonkeyTemple), nameof(MonkeyTemple.StartSacrifice))]
    internal static class MonkeyTemple_StartSacrifice
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            sacrificing = true;
        }

        [HarmonyPostfix]
        private static void Postfix()
        {
            sacrificing = false;
        }
    }

    [HarmonyPatch(typeof(TowerModel), nameof(TowerModel.isPowerTower), MethodType.Getter)]
    internal static class TowerModel_isPowerTower
    {
        [HarmonyPrefix]
        private static bool Prefix(TowerModel __instance, ref bool __result)
        {
            if (sacrificing && __instance.GetModTower() is TempleBase)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}

internal static class ParagonSacrificing
{
    public static int index;
    public static ParagonTower? paragonTower;

    [HarmonyPatch(typeof(ParagonTower), nameof(ParagonTower.StartSacrifice))]
    internal static class ParagonTower_StartSacrifice
    {
        [HarmonyPrefix]
        private static void Prefix(ParagonTower __instance)
        {
            if (__instance.tower.towerModel.GetModTower() is not TempleBase) return;

            paragonTower = __instance;
            index = 0;

            var towerOnTop = __instance.tower.GetTowerOnTop();
            if (towerOnTop != null)
            {
                var pos = __instance.tower.Position.ToVector2();
                towerOnTop.PositionTower(new Vector2(pos.x, pos.y + 10));
            }
        }

        [HarmonyPostfix]
        private static void Postfix()
        {
            paragonTower = null;
        }
    }

    [HarmonyPatch(typeof(TowerManager), nameof(TowerManager.TowerSacrificed))]
    internal static class TowerManager_TowerSacrificed
    {
        [HarmonyPrefix]
        private static void Prefix(Tower tower)
        {
            if (tower.towerModel.GetModTower() is TempleBase &&
                paragonTower != null &&
                tower.GetTowerOnTop().Is(out Tower towerOnTop))
            {
                var pos = paragonTower.tower.Position.ToVector2();
                towerOnTop.PositionTower(new Vector2(pos.x - 10 + (index * 20), pos.y - 10));

                index++;
            }

        }
    }
}