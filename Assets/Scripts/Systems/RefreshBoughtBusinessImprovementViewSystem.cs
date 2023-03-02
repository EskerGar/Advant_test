using Components;
using DefaultNamespace;
using Events;
using Extensions;
using Leopotam.EcsLite;

namespace Systems
{
	public class RefreshBoughtBusinessImprovementViewSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var boughtImprovementFilter = world.Filter<BuyImprovementEvent>().Inc<BoughtBusinessImprovementComponent>().End();
			var improvementViewComponentPool = world.GetPool<BusinessImprovementViewComponent>();

			var improvementEntity = boughtImprovementFilter.GetFirstEntity();

			if (improvementEntity != EcsManager.InvalidEntity)
			{
				ref var improvementViewComponent = ref improvementViewComponentPool.Get(improvementEntity);

				improvementViewComponent.ImprovementUiView.SetCost(0);
			}
		}
	}
}