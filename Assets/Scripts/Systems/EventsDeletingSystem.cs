using Events;
using Leopotam.EcsLite;

namespace Systems
{
	public class EventsDeletingSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var buyFilter = world.Filter<BuyImprovementEvent>().End();
			var levelUpFilter = world.Filter<LevelUpBusinessEvent>().End();
			
			var buyPool = world.GetPool<BuyImprovementEvent>();
			var levelUpPool = world.GetPool<LevelUpBusinessEvent>();

			foreach (var buyEntity in buyFilter)
			{
				buyPool.Del(buyEntity);
			}

			foreach (var levelUpEntity in levelUpFilter)
			{
				levelUpPool.Del(levelUpEntity);
			}
		}
	}
}