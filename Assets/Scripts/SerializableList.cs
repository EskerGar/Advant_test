using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	[Serializable]
	public class SerializableList<T>
	{
		[SerializeField] private List<T> _list;

		public List<T> List => _list;

		public SerializableList(List<T> list)
		{
			_list = list;
		}
	}
}