using Components;
using DefaultNamespace;
using Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;
using UnityEngine;

namespace Systems
{
	public class CreateBusinessImprovementViewSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<ConfigUtils> _configUtils;
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var improvementFilter = world.Filter<BusinessImprovementComponent>().End();
			
			var improvementViewPool = world.GetPool<BusinessImprovementViewComponent>();
			var businessViewPool = world.GetPool<BusinessViewComponent>();
			var improvementPool = world.GetPool<BusinessImprovementComponent>();
			var buyPool = world.GetPool<BuyImprovementEvent>();
			var boughtImprovementPool = world.GetPool<BoughtBusinessImprovementComponent>();

			foreach (var entity in improvementFilter)
			{
				ref var improvementComponent = ref improvementPool.Get(entity);

				if (_configUtils.Value.TryGetComponent(improvementComponent.Id, out BusinessImprovementVisualComponent visualComponent))
				{
					improvementViewPool.Add(entity);
					
					ref var businessViewComponent = ref businessViewPool.Get(improvementComponent.BusinessEntityId);
					ref var improvementViewComponent = ref improvementViewPool.Get(entity);
					
					var improvementUiView = Object.Instantiate(_viewsKeeper.Value.ImprovementUiViewPrefab, businessViewComponent.BusinessUiView.ImprovementsContainer);
					improvementUiView.SetName(visualComponent.Name);
					improvementUiView.SetIncomeIncrease(improvementComponent.IncomeMultiply);
					improvementUiView.SetCost(improvementComponent.Cost);
					improvementUiView.SetImproveButtonListener(() =>
					{
						if (boughtImprovementPool.Has(entity) == false)
						{
							buyPool.Add(entity);
						}
					});
					
					improvementViewComponent.ImprovementUiView = improvementUiView;
				}
			}
		}
	}
}