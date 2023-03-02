using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;

namespace Systems
{
	public class CreateBusinessImprovementSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<ConfigUtils> _configUtils;

		public void Init(IEcsSystems systems)
		{
			var dataManager = new DataGameManager();
			
			if (dataManager.TryLoadBusinessImprovementsData(out var businessImprovementComponents))
			{
				CreateLoadedBusinessImprovements(systems, businessImprovementComponents);
			}
			else
			{
				CreateNewBusinessImprovements(systems);
			}
		}

		private void CreateLoadedBusinessImprovements(IEcsSystems systems, IReadOnlyList<BusinessImprovementComponent> businessImprovementComponents)
		{
			var world = systems.GetWorld();
			
			var improvementPool = world.GetPool<BusinessImprovementComponent>();

			foreach (var loadedImprovementComponent in businessImprovementComponents)
			{
				var improvementEntity = world.NewEntity();
				ref var improvementComponent = ref improvementPool.Add(improvementEntity);

				improvementComponent.Id = loadedImprovementComponent.Id;
				improvementComponent.BusinessEntityId = loadedImprovementComponent.BusinessEntityId;
				improvementComponent.Cost = loadedImprovementComponent.Cost;
				improvementComponent.IncomeMultiply = loadedImprovementComponent.IncomeMultiply;
			}
		}

		private void CreateNewBusinessImprovements(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var businessFilter = world.Filter<BusinessComponent>().End();
			var improvementPool = world.GetPool<BusinessImprovementComponent>();

			foreach (var businessEntity in businessFilter)
			{
				var businessesPool = world.GetPool<BusinessComponent>();
				ref var businessComponent = ref businessesPool.Get(businessEntity);

				foreach (var improvementId in businessComponent.ImprovementIds)
				{
					if (_configUtils.Value.TryGetComponent(improvementId, out BusinessImprovementBaseComponent improvementBaseComponent))
					{
						var improvementEntity = world.NewEntity();
						ref var improvementComponent = ref improvementPool.Add(improvementEntity);

						improvementComponent.Id = improvementBaseComponent.Id;
						improvementComponent.BusinessEntityId = businessEntity;
						improvementComponent.Cost = improvementBaseComponent.Cost;
						improvementComponent.IncomeMultiply = improvementBaseComponent.IncomeMultiply;
					}
				}
			}
		}
	}
}