using System.Collections.Generic;
using System.IO;
using Components;
using UnityEngine;

namespace DefaultNamespace
{
	public class DataGameManager
	{
		public void SavePlayerData(PlayerComponent playerComponent)
		{
			var json = JsonUtility.ToJson(playerComponent);
			File.WriteAllText(Application.persistentDataPath + "/PlayerSaveData.json", json);
		}

		public void SaveBusinesses(List<BusinessComponent> businessComponents)
		{
			var serializableList = new SerializableList<BusinessComponent>(businessComponents);
			var json = JsonUtility.ToJson(serializableList);
			File.WriteAllText(Application.persistentDataPath + "/BusinessesSaveData.json", json);
		}

		public void SaveBusinessImprovements(List<BusinessImprovementComponent> businessImprovementComponents)
		{
			var serializableList = new SerializableList<BusinessImprovementComponent>(businessImprovementComponents);
			var json = JsonUtility.ToJson(serializableList);
			File.WriteAllText(Application.persistentDataPath + "/BusinessImprovementsSaveData.json", json);
		}

		public bool TryLoadPlayerData(out PlayerComponent playerComponent)
		{
			playerComponent = default;
			var filePath = Application.persistentDataPath + "/PlayerSaveData.json";

			if (File.Exists(filePath))
			{
				var data = File.ReadAllText(filePath);
        
				playerComponent = JsonUtility.FromJson<PlayerComponent>(data);
			}
			else
			{
				return false;
			}

			return true;
		}

		public bool TryLoadBusinessesData(out IReadOnlyList<BusinessComponent> businessComponents)
		{
			businessComponents = new List<BusinessComponent>();
			var filePath = Application.persistentDataPath + "/BusinessesSaveData.json";

			if (File.Exists(filePath))
			{
				var data = File.ReadAllText(filePath);
				var serializableList = default(SerializableList<BusinessComponent>);
        
				serializableList = JsonUtility.FromJson<SerializableList<BusinessComponent>>(data);
				businessComponents = serializableList.List;
			}
			else
			{
				return false;
			}

			return true;
		}

		public bool TryLoadBusinessImprovementsData(out IReadOnlyList<BusinessImprovementComponent> businessImprovementComponents)
		{
			businessImprovementComponents = new List<BusinessImprovementComponent>();
			var filePath = Application.persistentDataPath + "/BusinessImprovementsSaveData.json";

			if (File.Exists(filePath))
			{
				var data = File.ReadAllText(filePath);
				var serializableList = default(SerializableList<BusinessImprovementComponent>);
        
				serializableList = JsonUtility.FromJson<SerializableList<BusinessImprovementComponent>>(data);
				businessImprovementComponents = serializableList.List;
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}