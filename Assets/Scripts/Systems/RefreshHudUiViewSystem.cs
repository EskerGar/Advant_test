using Components;
using DefaultNamespace;
using Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class RefreshHudUiViewSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var filter = world.Filter<PlayerComponent>().End();
			var playerComponentPool = world.GetPool<PlayerComponent>();
			
			var playerEntity = filter.GetFirstEntity();

			if (playerEntity != EcsManager.InvalidEntity)
			{
				ref var playerComponent = ref playerComponentPool.Get(playerEntity);

				_viewsKeeper.Value.HUDUiView.SetBalance(playerComponent.Money);
			}
		}
	}
}