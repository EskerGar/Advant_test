using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Events;
using Extensions;
using Leopotam.EcsLite;

namespace Systems
{
	public class BuyImprovementSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var improvementFilter = world.Filter<BuyImprovementEvent>().Inc<BusinessImprovementComponent>().End();
			var playerFilter = world.Filter<PlayerComponent>().End();
			
			var improvementComponentPool = world.GetPool<BusinessImprovementComponent>();
			var businessComponentPool = world.GetPool<BusinessComponent>();
			var playerPool = world.GetPool<PlayerComponent>();
			var boughtImprovementPool = world.GetPool<BoughtBusinessImprovementComponent>();

			var improvementEntity = improvementFilter.GetFirstEntity();

			if (improvementEntity != EcsManager.InvalidEntity)
			{
				ref var playerComponent = ref playerPool.Get(playerFilter.GetFirstEntity());
				ref var improvementComponent = ref improvementComponentPool.Get(improvementEntity);
				ref var businessComponent = ref businessComponentPool.Get(improvementComponent.BusinessEntityId);
			
				if (playerComponent.Money >= improvementComponent.Cost)
				{
					playerComponent.Money -= improvementComponent.Cost;

					businessComponent.ImprovementEntities ??= new List<int>();
					businessComponent.ImprovementEntities.Add(improvementEntity);

					var allImprovementsMultiply = 1f;

					foreach (var entity in businessComponent.ImprovementEntities)
					{
						ref var tempImprovementComponent = ref improvementComponentPool.Get(entity);

						allImprovementsMultiply += tempImprovementComponent.IncomeMultiply;
					}

					improvementComponent.Cost = 0;
					businessComponent.Income = businessComponent.Level * businessComponent.BaseIncome * allImprovementsMultiply;
					boughtImprovementPool.Add(improvementEntity);
				}
			}
		}
	}
}