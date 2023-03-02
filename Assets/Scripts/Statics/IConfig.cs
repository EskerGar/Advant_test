using System;
using System.Collections.Generic;

namespace Statics
{
	public interface IConfig<T>
	{
		IEnumerable<T> AllComponents {get;}
		Type GetComponentType();

		bool TryGetComponent(int id, out T component);
	}
}