using Components;
using DefaultNamespace;
using Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;
using UnityEngine;
using View;

namespace Systems
{
	public class CreateBusinessesViewSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;
		private readonly EcsCustomInject<ConfigUtils> _configUtils;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var businessFilter = world.Filter<BusinessComponent>().End();
			
			var businessComponentPool = world.GetPool<BusinessComponent>();
			var businessViewPool = world.GetPool<BusinessViewComponent>();
			var levelUpPool = world.GetPool<LevelUpBusinessEvent>();

			foreach (var entity in businessFilter)
			{
				ref var businessComponent = ref businessComponentPool.Get(entity);

				if (_configUtils.Value.TryGetComponent(businessComponent.Id, out BusinessVisualComponent visualComponent))
				{
					businessViewPool.Add(entity);
					
					ref var businessViewComponent = ref businessViewPool.Get(entity);
					
					var businessUiView = CreateBusinessUiView(visualComponent, businessComponent);
					businessViewComponent.BusinessUiView = businessUiView;
					
					businessUiView.SetLevelUpButtonListener(() => levelUpPool.Add(entity));
				}
			}
		}

		private BusinessUiView CreateBusinessUiView(BusinessVisualComponent visualComponent, BusinessComponent businessComponent)
		{
			var businessUiView = Object.Instantiate(_viewsKeeper.Value.BusinessUiViewPrefab, _viewsKeeper.Value.HUDUiView.Container);

			businessUiView.SetName(visualComponent.Name);
			businessUiView.SetLevel(businessComponent.Level);
			businessUiView.SetIncome(businessComponent.Income);
			businessUiView.SetLevelUpCost(businessComponent.Cost);

			return businessUiView;
		}
	}
}