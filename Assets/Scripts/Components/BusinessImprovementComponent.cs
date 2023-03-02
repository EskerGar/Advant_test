using System;

namespace Components
{
	[Serializable]
	public struct BusinessImprovementComponent
	{
		public int Id;
		public int BusinessEntityId;
		public int Cost;
		public float IncomeMultiply;
	}
}