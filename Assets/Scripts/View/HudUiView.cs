using TMPro;
using UnityEngine;

namespace View
{
	public class HudUiView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _balance;
		[SerializeField] private Transform _container;

		public Transform Container => _container;

		public void SetBalance(float value)
		{
			_balance.SetText(value.ToString());
		}
	}
}