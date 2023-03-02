using Components;
using DefaultNamespace;
using Leopotam.EcsLite;

namespace Systems
{
	public class CreatePlayerSystem : IEcsInitSystem
	{
		public void Init(IEcsSystems systems)
		{
			var dataManager = new DataGameManager();
			
			var world = systems.GetWorld();
			var playerEntity = world.NewEntity();
			var playersPool = world.GetPool<PlayerComponent>();
			ref var playerComponent = ref playersPool.Add(playerEntity);

			dataManager.TryLoadPlayerData(out playerComponent);
		}
	}
}