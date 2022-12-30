using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Utils;
using BTD_Mod_Helper.Api.Towers;

namespace TempleBase.Upgrades;

public abstract class TempleBaseUpgrade : ModUpgrade<TempleBase>
{
    public override string Icon => Name;

    public const string TheTower = "The Chosen";

    public override SpriteReference IconReference => GetSpriteReferenceOrDefault(Icon);

    protected static TowerModel SunTemple => Game.instance.model.GetTower(TowerType.SuperMonkey, 4);
    protected static TowerModel SunGod => Game.instance.model.GetTower(TowerType.SuperMonkey, 5);

}