using System;
using System.Collections.Generic;

namespace Components
{
	[Serializable]
	public struct BusinessComponent
	{
		public int Id;
		public float EarningTimeInSeconds;
		public float RemainingEarningTimeInSeconds;
		public int BaseCost;
		public int BaseIncome;
		public float Income;
		public float Cost;
		public int[] ImprovementIds;
		public List<int> ImprovementEntities;
		public int Level;
	}
}