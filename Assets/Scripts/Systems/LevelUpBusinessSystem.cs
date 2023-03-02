using Components;
using Events;
using Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;

namespace Systems
{
	public class LevelUpBusinessSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<ConfigUtils> _configUtils;

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var businessFilter = world.Filter<LevelUpBusinessEvent>().Inc<BusinessComponent>().End();
			var playerFilter = world.Filter<PlayerComponent>().End();
			
			var businessComponentPool = world.GetPool<BusinessComponent>();
			var boughtBusinessesPool = world.GetPool<BoughtBusinessComponent>();
			var improvementComponentPool = world.GetPool<BusinessImprovementComponent>();
			var playerPool = world.GetPool<PlayerComponent>();
			
			ref var playerComponent = ref playerPool.Get(playerFilter.GetFirstEntity());

			foreach (var entity in businessFilter)
			{
				ref var businessComponent = ref businessComponentPool.Get(entity);

				if (!(playerComponent.Money >= businessComponent.Cost))
				{
					continue;
				}
				
				if (businessComponent.Level == 0)
				{
					boughtBusinessesPool.Add(entity);
				}
					
				businessComponent.Level += 1;
				playerComponent.Money -= businessComponent.Cost;
					
				var allImprovementsMultiply = 1f;

				if (businessComponent.ImprovementEntities != null)
				{
					foreach (var improvementEntity in businessComponent.ImprovementEntities)
					{
						ref var tempImprovementComponent = ref improvementComponentPool.Get(improvementEntity);

						allImprovementsMultiply += tempImprovementComponent.IncomeMultiply;
					}
				}

				businessComponent.Income = businessComponent.Level * businessComponent.BaseIncome * allImprovementsMultiply;
				businessComponent.Cost = (businessComponent.Level + 1) * businessComponent.BaseCost;
			}
		}
	}
}