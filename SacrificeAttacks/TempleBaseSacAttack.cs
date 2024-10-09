using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace TempleBase.SacrificeAttacks;

public abstract class TempleBaseSacAttack : ModContent
{
    public abstract TowerSet TowerSet { get; }
    public abstract int Tier { get; }

    protected List<TowerBehaviorModel> Behaviors { get; private set; } = null!;

    public sealed override void Register()
    {
        Behaviors = GetBehaviors().ToList();
        for (var i = 0; i < Behaviors.Count; i++)
        {
            Behaviors[i].name = Id + "-" + i;
        }
    }

    public void Remove(Tower tower)
    {
        var towerModel = tower.rootModel.Cast<TowerModel>();
        var didRemove = false;

        var existing = towerModel.behaviors.FirstOrDefault(model => model.name.Contains(Id));
        while (existing != null)
        {
            if (!didRemove)
            {
                towerModel = towerModel.Duplicate();
                didRemove = true;
            }

            towerModel.RemoveBehavior(existing);
            
            existing = towerModel.behaviors.FirstOrDefault(model => model.name.Contains(Id));
        }

        if (didRemove)
        {
            towerModel.UpdateTargetProviders();
            tower.UpdateRootModel(towerModel);
            OnUpdate(tower);
        }
    }

    public void Apply(Tower tower)
    {
        var towerModel = tower.rootModel.Cast<TowerModel>();
        var didApply = false;

        foreach (var behavior in Behaviors)
        {
            var existing = towerModel.behaviors.FirstOrDefault(model => model.name.Contains(behavior.name));
            if (existing == null)
            {
                if (!didApply)
                {
                    towerModel = towerModel.Duplicate();
                    didApply = true;
                }

                existing = behavior.Duplicate();
                towerModel.AddBehavior(existing);
            }
        }

        if (didApply)
        {
            towerModel.UpdateTargetProviders();
            tower.UpdateRootModel(towerModel);
            OnUpdate(tower);
        }
    }

    protected static void OnUpdate(Tower tower)
    {
        var simulation = InGame.instance.bridge.Simulation;
        simulation.factory.GetUncast<Tower>().ForEach(t =>
        {
            if (t.ParentId == tower.Id)
            {
                simulation.DestroyTower(t, t.owner);
            }
        });
    }

    protected static TowerModel SunTemple => Game.instance.model.GetTower(TowerType.SuperMonkey, 4);
    protected static TowerModel SunGod => Game.instance.model.GetTower(TowerType.SuperMonkey, 5);

    protected TempleTowerMutatorGroupModel GetSacrificeEffect(TowerModel towerModel, int cost) => towerModel
        .GetBehaviors<TempleTowerMutatorGroupModel>()
        .First(model => model.towerSet == TowerSet && model.cost == cost);

    protected abstract IEnumerable<TowerBehaviorModel> GetBehaviors();
}