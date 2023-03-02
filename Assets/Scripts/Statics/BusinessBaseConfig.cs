using System;
using System.Collections.Generic;
using UnityEngine;

namespace Statics
{
    [Serializable]
    public struct BusinessBaseComponent
    {
        public int Id;
        public float EarningTimeInSeconds;
        public int BaseCost;
        public int BaseIncome;
        public int[] ImprovementIds;
    }
    
    [CreateAssetMenu(fileName = "ConfigBusinessBase", menuName = "Configs/BusinessBase")]
    public class BusinessBaseConfig : ScriptableObject, ISerializationCallbackReceiver, IConfig<BusinessBaseComponent>
    { 
        [SerializeField] private BusinessBaseComponent[] _businessArray;
        
        public IEnumerable<BusinessBaseComponent> AllComponents => _businessDictionary.Values;

        private readonly Dictionary<int, BusinessBaseComponent> _businessDictionary = new();
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _businessDictionary.Clear();
            
            foreach (var businessBaseComponent in _businessArray)
            {
                if (_businessDictionary.ContainsKey(businessBaseComponent.Id) == false)
                {
                    _businessDictionary.Add(businessBaseComponent.Id, businessBaseComponent);
                }
                else
                {
                    Debug.LogError("ConfigBusinessBase have non unique ids");
                }
            }
        }

        public Type GetComponentType()
        {
            return typeof(BusinessBaseComponent);
        }

        public bool TryGetComponent(int id, out BusinessBaseComponent component)
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
