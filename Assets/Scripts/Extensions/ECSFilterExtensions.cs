using Leopotam.EcsLite;

namespace Extensions
{
	public static class ECSFilterExtensions
	{
		public static int GetFirstEntity(this EcsFilter ecsFilter)
		{
			foreach (var entity in ecsFilter)
			{
				return entity;
			}

			return -1;
		}
	}
}