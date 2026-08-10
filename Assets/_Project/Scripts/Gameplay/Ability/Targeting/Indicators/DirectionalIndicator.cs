using UnityEngine;

public class DirectionalIndicator : TargetingIndicator
{
	[SerializeField] private Transform _visualTransform;

	public override void UpdateAim(Vector3 origin, Vector3 worldPos, float range, float size, float chargeRatio = 1f)
	{
		transform.position = origin;

		Vector3 aimDir = (worldPos - origin).normalized;
		aimDir.y = 0;

		if (aimDir != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(aimDir);
		}

		if (_visualTransform != null)
		{
			float currentLength = range * chargeRatio;

			_visualTransform.localScale = new Vector3(size, currentLength, _visualTransform.localScale.z);
			_visualTransform.localPosition = new Vector3(0, 0.15f, currentLength / 2f);
		}
	}
}
