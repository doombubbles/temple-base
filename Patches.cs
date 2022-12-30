namespace TempleBase;

/*[HarmonyPatch(typeof(MonkeyTemple), nameof(MonkeyTemple.StartSacrifice))]
internal static class MonkeyTemple_StartSacrifice
{
    public static bool sacrificing;
    
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
        if (MonkeyTemple_StartSacrifice.sacrificing && __instance.GetModTower() is TempleBase)
        {
            __result = true;
            return false;
        }
        return true;
    }
}*/