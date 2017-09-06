using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryGameLogicLib.DataReader
{
	public static class TakeRandomEnumberableExtension
	{
		/// <summary>
		/// Liefert eine bestimmte Anzahl an Elementen zufällig aus der Liste ausgewählt
		/// </summary>		
		public static IList<T> TakeRandom<T>(this IList<T> list, ushort amount)
		{
			if (amount > list.Count) throw new ArgumentOutOfRangeException();
			Random r = new Random();
			List<T> result = new List<T>();
			while (result.Count < amount)
			{
				T next = list.ElementAt(r.Next(0, list.Count));
				if (!result.Contains(next))
					result.Add(next);
			}
			return result;
		}
	}
}
