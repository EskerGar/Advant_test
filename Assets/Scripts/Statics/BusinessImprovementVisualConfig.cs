using System;
using System.Collections.Generic;
using UnityEngine;

namespace Statics
{
	[Serializable]
	public struct BusinessImprovementVisualComponent
	{
		public int Id;
		public string Name;
	}
	
	[CreateAssetMenu(fileName = "ConfigBusinessImprovementVisual", menuName = "Configs/BusinessImprovementVisual")]
	public class BusinessImprovementVisualConfig : ScriptableObject, ISerializationCallbackReceiver, IConfig<BusinessImprovementVisualComponent>
	{
		[SerializeField] private BusinessImprovementVisualComponent[] _businessImprovementArray;
		
		public IEnumerable<BusinessImprovementVisualComponent> AllComponents => _businessImprovementDictionary.Values;

		private readonly Dictionary<int, BusinessImprovementVisualComponent> _businessImprovementDictionary = new();

		public void OnBeforeSerialize()
		{
			
		}

		public void OnAfterDeserialize()
		{
			_businessImprovementDictionary.Clear();
			
			foreach (var businessImprovementVisualComponent in _businessImprovementArray)
			{
				if (_businessImprovementDictionary.ContainsKey(businessImprovementVisualComponent.Id) == false)
				{
					_businessImprovementDictionary.Add(businessImprovementVisualComponent.Id, businessImprovementVisualComponent);
				}
				else
				{
					Debug.LogError("ConfigBusinessImprovementVisual have non unique ids");
				}
			}
		}

		public Type GetComponentType()
		{
			return typeof(BusinessImprovementVisualComponent);
		}

		public bool TryGetComponent(int id, out BusinessImprovementVisualComponent component)
		{
			if (_businessImprovementDictionary.TryGetValue(id, out component))
			{
				return true;
			}

			component = default;

			return false;
		}
	}
}