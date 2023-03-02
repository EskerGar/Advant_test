using Components;
using Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class EarningSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var filter = world.Filter<BusinessComponent>().Inc<BoughtBusinessComponent>().End();
			var businessComponentPool = world.GetPool<BusinessComponent>();

			foreach (var entity in filter)
			{
				ref var businessComponent = ref businessComponentPool.Get(entity);
				businessComponent.RemainingEarningTimeInSeconds -= Time.deltaTime;

				if (businessComponent.RemainingEarningTimeInSeconds <= 0)
				{
					var playerFilter = world.Filter<PlayerComponent>().End();
					var playerPool = world.GetPool<PlayerComponent>();
					ref var playerComponent = ref playerPool.Get(playerFilter.GetFirstEntity());

					playerComponent.Money += businessComponent.Income;

					businessComponent.RemainingEarningTimeInSeconds = businessComponent.EarningTimeInSeconds;
				}
			}
		}
	}
}