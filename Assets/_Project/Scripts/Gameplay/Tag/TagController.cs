using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour
{
	private readonly HashSet<TagSO> _activeTags = new();

	public void AddTag(TagSO tag)
	{
		if (tag != null) _activeTags.Add(tag);
	}

	public void RemoveTag(TagSO tag)
	{
		if (tag != null) _activeTags.Remove(tag);
	}

	public bool HasTag(TagSO tag)
	{
		return tag != null && _activeTags.Contains(tag);
	}

	public bool HasAnyTag(List<TagSO> tagsToCheck)
	{
		if (tagsToCheck == null || tagsToCheck.Count == 0) return false;

		int count = tagsToCheck.Count;
		for (var i = 0; i < count; i++)
		{
			if (_activeTags.Contains(tagsToCheck[i]))
			{
				return true;
			}
		}
		return false;
	}


	public bool HasAllTags(List<TagSO> tagsToCheck)
	{
		if (tagsToCheck == null || tagsToCheck.Count == 0) return true;

		int count = tagsToCheck.Count;
		for (var i = 0; i < count; i++)
		{
			if (!_activeTags.Contains(tagsToCheck[i]))
			{
				return false;
			}
		}
		return true;
	}
}
