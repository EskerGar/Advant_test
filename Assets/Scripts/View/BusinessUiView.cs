using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class BusinessUiView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _name;
		[SerializeField] private TMP_Text _level;
		[SerializeField] private TMP_Text _income;
		[SerializeField] private TMP_Text _levelUpCost;
		[SerializeField] private Slider _slider;
		[SerializeField] private Button _levelUpButton;
		[SerializeField] private Transform _improvementsContainer;

		public Transform ImprovementsContainer => _improvementsContainer;

		public void SetName(string value)
		{
			_name.SetText(value);
		}

		public void SetLevel(int value)
		{
			_level.SetText($"LVL: {value.ToString()}");
		}

		public void SetIncome(float value)
		{
			_income.SetText($"Income: {value.ToString()}");
		}

		public void SetLevelUpCost(float value)
		{
			_levelUpCost.SetText($"Cost: {value.ToString()}");
		}

		public void SetSliderValue(float value)
		{
			_slider.value = value;
		}

		public void SetLevelUpButtonListener(UnityAction action)
		{
			_levelUpButton.onClick.RemoveAllListeners();
			_levelUpButton.onClick.AddListener(action);
		}
	}
}