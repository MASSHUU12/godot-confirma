using System.Collections.Generic;

namespace Confirma.Helpers;

public static class ArrayHelper
{
	public static string ConvertToString(object?[]? array, uint depth = 0, uint maxDepth = 1)
	{
		if (depth > maxDepth) return string.Empty;
		if (array is null || array.Length == 0) return string.Empty;

		List<string> list = new();

		foreach (object? item in array)
		{
			if (item is null)
			{
				list.Add("null");
				continue;
			}

			if (item.GetType().IsArray)
			{
				if (item is object[] arr)
				{
					var result = ConvertToString(arr, depth + 1u, maxDepth);
					list.Add($"[{result}]");
				}
				else list.Add(item.ToString() ?? "null");

				continue;
			}

			list.Add(item.ToString() ?? "null");
		}

		return string.Join(", ", list);
	}
}
