using System;
using System.Collections.Generic;
using UnityEngine;

namespace Statics
{
	[Serializable]
	public struct BusinessImprovementBaseComponent
	{
		public int Id;
		public int Cost;
		public float IncomeMultiply;
	}
	
	[CreateAssetMenu(fileName = "ConfigBusinessImprovementBase", menuName = "Configs/BusinessImprovementBase")]
	public class BusinessImprovementBaseConfig : ScriptableObject, ISerializationCallbackReceiver, IConfig<BusinessImprovementBaseComponent>
	{
		[SerializeField] private BusinessImprovementBaseComponent[] _businessImprovementArray;
		
		public IEnumerable<BusinessImprovementBaseComponent> AllComponents => _businessImprovementDictionary.Values;

		private readonly Dictionary<int, BusinessImprovementBaseComponent> _businessImprovementDictionary = new();

		public void OnBeforeSerialize()
		{
			
		}

		public void OnAfterDeserialize()
		{
			_businessImprovementDictionary.Clear();
			
			foreach (var businessImprovementBaseComponent in _businessImprovementArray)
			{
				if (_businessImprovementDictionary.ContainsKey(businessImprovementBaseComponent.Id) == false)
				{
					_businessImprovementDictionary.Add(businessImprovementBaseComponent.Id, businessImprovementBaseComponent);
				}
				else
				{
					Debug.LogError("ConfigBusinessImprovementBase have non unique ids");
				}
			}
		}

		public Type GetComponentType()
		{
			return typeof(BusinessImprovementBaseComponent);
		}

		public bool TryGetComponent(int id, out BusinessImprovementBaseComponent component)
		{
			if ( _businessImprovementDictionary.TryGetValue(id, out var baseComponent))
			{
				component = baseComponent;
				
				return true;
			}

			component = default;

			return false;
		}
	}
}