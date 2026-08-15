using UnityEngine;

public class AnimationVariantController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private EntityData entityData;

	private AnimationVariant[] _instances;

	private void Awake()
	{
		if (entityData == null || animator == null)
		{
			return;
		}

		_instances = new AnimationVariant[entityData.AnimationVariants.Count];

		for (var i = 0; i < entityData.AnimationVariants.Count; i++)
		{
			_instances[i] = new AnimationVariant(entityData.AnimationVariants[i], animator);
		}
	}

	private void Update()
	{
		if (_instances == null) return;

		for (var i = 0; i < _instances.Length; i++)
		{
			_instances[i]?.Tick(Time.deltaTime);
		}
	}

	private void OnDestroy()
	{
		if (_instances == null) return;

		for (var i = 0; i < _instances.Length; i++)
		{
			_instances[i]?.Dispose();
		}
	}
}
