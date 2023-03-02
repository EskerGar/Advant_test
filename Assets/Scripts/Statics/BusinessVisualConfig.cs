using System;
using System.Collections.Generic;
using UnityEngine;

namespace Statics
{
	[Serializable]
	public struct BusinessVisualComponent
	{
		public int Id;
		public string Name;
	}
	
	[CreateAssetMenu(fileName = "ConfigBusinessVisual", menuName = "Configs/BusinessVisual")]
	public class BusinessVisualConfig : ScriptableObject, ISerializationCallbackReceiver, IConfig<BusinessVisualComponent>
	{
		[SerializeField] private BusinessVisualComponent[] _businessArray;
		
		public IEnumerable<BusinessVisualComponent> AllComponents => _businessDictionary.Values;

		private readonly Dictionary<int, BusinessVisualComponent> _businessDictionary = new();

		public void OnBeforeSerialize()
		{

		}

		public void OnAfterDeserialize()
		{
			_businessDictionary.Clear();
			
			foreach (var businessVisualComponent in _businessArray)
			{
				if (_businessDictionary.ContainsKey(businessVisualComponent.Id) == false)
				{
					_businessDictionary.Add(businessVisualComponent.Id, businessVisualComponent);
				}
				else
				{
					Debug.LogError("ConfigBusinessVisual have non unique ids");
				}
			}
		}

		public Type GetComponentType()
		{
			return typeof(BusinessVisualComponent);
		}

		public bool TryGetComponent(int id, out BusinessVisualComponent component)
		{
			if (_businessDictionary.TryGetValue(id, out component))
			{
				return true;
			}

			component = default;

			return false;
		}
	}
}