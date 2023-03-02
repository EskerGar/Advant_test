using System;
using System.Collections.Generic;
using System.Linq;

namespace Statics
{
	public class ConfigUtils
	{
		private Configs _configs;
		private Dictionary<Type, object> _configByType;

		public void Init(Configs configs)
		{
			_configs = configs;
			
			_configByType = new Dictionary<Type, object>
			{
				{_configs.BusinessBaseConfig.GetComponentType(), _configs.BusinessBaseConfig},
				{_configs.BusinessVisualConfig.GetComponentType(), _configs.BusinessVisualConfig},
				{_configs.BusinessImprovementBaseConfig.GetComponentType(), _configs.BusinessImprovementBaseConfig},
				{_configs.BusinessImprovementVisualConfig.GetComponentType(), _configs.BusinessImprovementVisualConfig}
			};
		}

		public bool TryGetComponent<T>(int id, out T component)
			where T : struct
		{
			if (_configByType.TryGetValue(typeof(T), out var config))
			{
				((IConfig<T>)config).TryGetComponent(id, out component);
				
				return true;
			}

			component = default;

			return false;
		}

		public IReadOnlyList<T> GetAllComponents<T>()
		{
			if (_configByType.TryGetValue(typeof(T), out var config))
			{
				return ((IConfig<T>)config).AllComponents.ToList();
			}

			return default;
		}
	}
}