using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField] private EntitySet entityList;

	public IDamageable Damageable { get; private set; }
	public Transform Transform => transform;

	private void Awake()
	{
		Damageable = GetComponent<IDamageable>();
	}

	private void OnEnable()
	{
		if (entityList != null)
		{
			entityList.Add(this);
		}
	}

	private void OnDisable()
	{
		if (entityList != null)
		{
			entityList.Remove(this);
		}
	}
}
