using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;

namespace TempleBase.Upgrades.Bot;

public class LightOnCash : TempleBaseUpgrade
{
    public override int Path => BOTTOM;
    public override int Tier => 2;
    public override int Cost => 750;
    
    public override string Description => $"Reduces the cost of {TheTower}'s upgrades by 5%.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var buffIcon = GetInstance<TempleBaseBuffIcon>();
        var discount = new DiscountZoneModel("DiscountZoneModel_", .05f, 1, Id, Id,
             false, 5, buffIcon.Icon, buffIcon.Id, "", "", false);
        towerModel.AddBehavior(discount);
    }
}