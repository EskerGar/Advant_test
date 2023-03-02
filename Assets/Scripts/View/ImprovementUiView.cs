using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class ImprovementUiView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _name;
		[SerializeField] private TMP_Text _incomeIncrease;
		[SerializeField] private TMP_Text _cost;
		[SerializeField] private Button _improveButton;

		public void SetName(string value)
		{
			_name.SetText(value);
		}

		public void SetIncomeIncrease(float value)
		{
			var valueInPercent = value * 100;
			_incomeIncrease.SetText($"Income: +{valueInPercent.ToString()}%");
		}

		public void SetCost(int value)
		{
			var text = $"Cost: {value.ToString()}$";
			
			if (value == 0)
			{
				text = "Soled";
			}
			_cost.SetText(text);
		}

		public void SetImproveButtonListener(UnityAction action)
		{
			_improveButton.onClick.RemoveAllListeners();
			_improveButton.onClick.AddListener(action);
		}
	}
}