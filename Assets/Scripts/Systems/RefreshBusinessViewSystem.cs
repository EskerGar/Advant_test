using Components;
using DefaultNamespace;
using Events;
using Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class RefreshBusinessViewSystem : IEcsRunSystem
	{
		private readonly EcsPoolInject<BusinessComponent> _businessComponentPool;
		private readonly EcsPoolInject<BusinessViewComponent> _businessViewComponentPool;
		private readonly EcsPoolInject<BusinessImprovementComponent> _improvementComponentPool;

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var levelUpFilter = world.Filter<LevelUpBusinessEvent>().End();
			var businessFilter = world.Filter<BusinessComponent>().Inc<BoughtBusinessComponent>().End();
			var boughtImprovementFilter = world.Filter<BuyImprovementEvent>().Inc<BoughtBusinessImprovementComponent>().End();
			
			var boughtImprovementEntity = boughtImprovementFilter.GetFirstEntity();

			var levelUpBusinessEntity = levelUpFilter.GetFirstEntity();

			if (boughtImprovementEntity != EcsManager.InvalidEntity)
			{
				RefreshIncomeWithImprove(boughtImprovementEntity);
			}

			if (levelUpBusinessEntity != EcsManager.InvalidEntity)
			{
				RefreshLevel(levelUpBusinessEntity);
			}

			RefreshSlider(businessFilter);
		}

		private void RefreshLevel(int businessEntity)
		{
			ref var businessComponent = ref _businessComponentPool.Value.Get(businessEntity);
			ref var businessViewComponent = ref _businessViewComponentPool.Value.Get(businessEntity);

			businessViewComponent.BusinessUiView.SetLevel(businessComponent.Level);
			businessViewComponent.BusinessUiView.SetLevelUpCost(businessComponent.Cost);
			businessViewComponent.BusinessUiView.SetIncome(businessComponent.Income);
		}

		private void RefreshSlider(EcsFilter businessFilter)
		{
			foreach (var entity in businessFilter)
			{
				ref var businessComponent = ref _businessComponentPool.Value.Get(entity);
				ref var businessViewComponent = ref _businessViewComponentPool.Value.Get(entity);

				var sliderValue = 1 - businessComponent.RemainingEarningTimeInSeconds / businessComponent.EarningTimeInSeconds;
				businessViewComponent.BusinessUiView.SetSliderValue(sliderValue);
			}
		}

		private void RefreshIncomeWithImprove(int boughtImprovementEntity)
		{
			ref var  improvementComponent = ref _improvementComponentPool.Value.Get(boughtImprovementEntity);
			ref var businessViewComponent = ref _businessViewComponentPool.Value.Get(improvementComponent.BusinessEntityId);
			ref var businessComponent = ref _businessComponentPool.Value.Get(improvementComponent.BusinessEntityId);
			
			businessViewComponent.BusinessUiView.SetIncome(businessComponent.Income);
		}
	}
}