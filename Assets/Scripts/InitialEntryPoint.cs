using Statics;
using UnityEngine;

namespace DefaultNamespace
{
	public class InitialEntryPoint : MonoBehaviour
	{
		[SerializeField] private Configs _configs;
		[SerializeField] private EcsManager _ecsManager;
		[SerializeField] private ViewsKeeper _viewsKeeper;

		private void Start()
		{
			var configUtils = new ConfigUtils();
			configUtils.Init(_configs);
			_ecsManager.Init(configUtils, _viewsKeeper);
		}
	}
}