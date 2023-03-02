using UnityEngine;

namespace Statics
{
	[CreateAssetMenu(fileName = "Configs", menuName = "Configs/Configs")]
	public class Configs : ScriptableObject
	{
		[SerializeField] private BusinessBaseConfig _businessBaseConfig;
		[SerializeField] private BusinessVisualConfig _businessVisualConfig;
		[SerializeField] private BusinessImprovementBaseConfig _businessImprovementBaseConfig;
		[SerializeField] private BusinessImprovementVisualConfig _businessImprovementVisualConfig;

		public IConfig<BusinessBaseComponent> BusinessBaseConfig => _businessBaseConfig;
		public IConfig<BusinessVisualComponent> BusinessVisualConfig => _businessVisualConfig;
		public IConfig<BusinessImprovementBaseComponent> BusinessImprovementBaseConfig => _businessImprovementBaseConfig;
		public IConfig<BusinessImprovementVisualComponent> BusinessImprovementVisualConfig => _businessImprovementVisualConfig;
	}
}