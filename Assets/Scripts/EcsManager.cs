using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statics;
using Systems;
using UnityEngine;

namespace DefaultNamespace
{
	public class EcsManager : MonoBehaviour
	{
		public const int InvalidEntity = -1;

		private ConfigUtils _configUtils;
		private EcsWorld _ecsWorld;
		private EcsSystems _initSystems;
		private EcsSystems _updateSystems;
		private EcsSystems _onPauseSystems;

		public void Init(ConfigUtils configUtils, ViewsKeeper viewsKeeper)
		{
			_configUtils = configUtils;

			_ecsWorld = new EcsWorld();

			_initSystems = new EcsSystems(_ecsWorld);
			_updateSystems = new EcsSystems(_ecsWorld);
			_onPauseSystems = new EcsSystems(_ecsWorld);

			_initSystems
				.Add(new CreatBusinessesSystem())
				.Add(new CreateBusinessImprovementSystem())
				.Add(new CreatePlayerSystem())
				.Add(new CreateBusinessesViewSystem())
				.Add(new CreateBusinessImprovementViewSystem())
				.Inject(_configUtils)
				.Inject(viewsKeeper)
				.Init();

			_updateSystems
				.Add(new EarningSystem())
				.Add(new LevelUpBusinessSystem())
				.Add(new BuyImprovementSystem())
				.Add(new RefreshHudUiViewSystem())
				.Add(new RefreshBusinessViewSystem())
				.Add(new RefreshBoughtBusinessImprovementViewSystem())
				.Add(new EventsDeletingSystem())
				.Inject(viewsKeeper)
				.Init();

			_onPauseSystems
				.Add(new SaveGameSystem())
				.Init();
		}

		private void Update()
		{
			_updateSystems?.Run();
		}

		private void OnDestroy()
		{
			_initSystems?.Destroy();
			_updateSystems?.Destroy();
			_onPauseSystems?.Destroy();
			_initSystems = null;
			_updateSystems = null;
			_onPauseSystems = null;
			_ecsWorld?.Destroy();
			_ecsWorld = null;
		}

#if UNITY_ANDROID && UNITY_EDITOR == false
		private void OnApplicationPause(bool pauseStatus)
		{
			_onPauseSystems?.Run();
		}
#endif

#if UNITY_EDITOR
		private void OnApplicationQuit()
		{
			_onPauseSystems?.Run();
		}
#endif
	}
}