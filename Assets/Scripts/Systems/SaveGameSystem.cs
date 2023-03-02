using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Leopotam.EcsLite;

namespace Systems
{
	public class SaveGameSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var playerFilter = world.Filter<PlayerComponent>().End();
			var businessesFilter = world.Filter<BusinessComponent>().End();
			var businessImprovementsFilter = world.Filter<BusinessImprovementComponent>().End();
			
			var playerPool = world.GetPool<PlayerComponent>();
			var businessesPool = world.GetPool<BusinessComponent>();
			var businessImprovementsPool = world.GetPool<BusinessImprovementComponent>();

			var businessComponents = new List<BusinessComponent>();
			var businessImprovementComponents = new List<BusinessImprovementComponent>();
			var dataGameManager = new DataGameManager();

			foreach (var playerEntity in playerFilter)
			{
				ref var playerComponent = ref playerPool.Get(playerEntity);
				
				dataGameManager.SavePlayerData(playerComponent);
			}

			foreach (var businessEntity in businessesFilter)
			{
				ref var businessComponent = ref businessesPool.Get(businessEntity);

				businessComponents.Add(businessComponent);
			}
			
			foreach (var businessImprovementEntity in businessImprovementsFilter)
			{
				ref var businessImprovementComponent = ref businessImprovementsPool.Get(businessImprovementEntity);

				businessImprovementComponents.Add(businessImprovementComponent);
			}
			
			dataGameManager.SaveBusinesses(businessComponents);
			dataGameManager.SaveBusinessImprovements(businessImprovementComponents);
		}
	}
}