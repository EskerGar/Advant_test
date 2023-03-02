using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;

namespace Systems
{
	public class CreatBusinessesSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<ConfigUtils> _configUtils;

		public void Init(IEcsSystems systems)
		{
			var dataManager = new DataGameManager();

			if (dataManager.TryLoadBusinessesData(out var loadedAllBusinessBaseComponents))
			{
				CreateLoadedBusinesses(systems, loadedAllBusinessBaseComponents);
			}
			else
			{
				CreateNewBusinesses(systems);
			}
		}

		private void CreateLoadedBusinesses(IEcsSystems systems, IReadOnlyList<BusinessComponent> loadedAllBusinessBaseComponents)
		{
			var world = systems.GetWorld();
			var businessesPool = world.GetPool<BusinessComponent>();
			var boughtBusinessesPool = world.GetPool<BoughtBusinessComponent>();
			
			foreach (var loadedBusinessComponent in loadedAllBusinessBaseComponents)
			{
				var businessEntity = world.NewEntity();
					
				businessesPool.Add(businessEntity);
				ref var businessComponent = ref businessesPool.Get(businessEntity);

				businessComponent.Id = loadedBusinessComponent.Id;
				businessComponent.BaseCost = loadedBusinessComponent.BaseCost;
				businessComponent.BaseIncome = loadedBusinessComponent.BaseIncome;
				businessComponent.ImprovementIds = loadedBusinessComponent.ImprovementIds;
				businessComponent.EarningTimeInSeconds = loadedBusinessComponent.EarningTimeInSeconds;
				businessComponent.RemainingEarningTimeInSeconds = loadedBusinessComponent.RemainingEarningTimeInSeconds;
				businessComponent.Level = loadedBusinessComponent.Level;
				businessComponent.Income = loadedBusinessComponent.Income;
				businessComponent.Cost = loadedBusinessComponent.Cost;
				businessComponent.ImprovementEntities = loadedBusinessComponent.ImprovementEntities;

				if (businessComponent.Level != 0)
				{
					boughtBusinessesPool.Add(businessEntity);
				}
			}
		}

		private void CreateNewBusinesses(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var businessesPool = world.GetPool<BusinessComponent>();
			var boughtBusinessesPool = world.GetPool<BoughtBusinessComponent>();
			var allBusinessBaseComponents = _configUtils.Value.GetAllComponents<BusinessBaseComponent>();
				
			foreach (var businessBaseComponent in allBusinessBaseComponents)
			{
				var businessEntity = world.NewEntity();

				businessesPool.Add(businessEntity);
				ref var businessComponent = ref businessesPool.Get(businessEntity);

				businessComponent.Id = businessBaseComponent.Id;
				businessComponent.BaseCost = businessBaseComponent.BaseCost;
				businessComponent.BaseIncome = businessBaseComponent.BaseIncome;
				businessComponent.ImprovementIds = businessBaseComponent.ImprovementIds;
				businessComponent.EarningTimeInSeconds = businessBaseComponent.EarningTimeInSeconds;
				businessComponent.RemainingEarningTimeInSeconds = businessBaseComponent.EarningTimeInSeconds;
				businessComponent.Level = 0;
				businessComponent.Income = businessComponent.BaseIncome;

				if (businessBaseComponent.Id == 0)
				{
					businessComponent.Level = 1;
					boughtBusinessesPool.Add(businessEntity);
				}

				businessComponent.Cost = (businessComponent.Level + 1) * businessComponent.BaseCost;
			}
		}
	}
}