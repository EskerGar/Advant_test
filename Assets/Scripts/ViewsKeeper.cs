using UnityEngine;
using View;

namespace DefaultNamespace
{
	public class ViewsKeeper : MonoBehaviour
	{
		[SerializeField] private HudUiView _hudUiView;
		[SerializeField] private BusinessUiView _businessUiViewPrefab;
		[SerializeField] private ImprovementUiView _improvementUiViewPrefab;

		public HudUiView HUDUiView => _hudUiView;
		public BusinessUiView BusinessUiViewPrefab => _businessUiViewPrefab;
		public ImprovementUiView ImprovementUiViewPrefab => _improvementUiViewPrefab;
	}
}