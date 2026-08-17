using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Ghost : MonoBehaviour, IPooledObject<Ghost>
{
	private IObjectPool<Ghost> _pool;

	private MeshFilter _meshFilter;
	private MeshRenderer _meshRenderer;
	private Mesh _bakedMesh;
	private MaterialPropertyBlock _propBlock;
	private static readonly int ColorProp = Shader.PropertyToID("_BaseColor");

	private float _alpha;
	private float _fadeSpeed;

	private void Awake()
	{
		_meshFilter = GetComponent<MeshFilter>();
		_meshRenderer = GetComponent<MeshRenderer>();
		_propBlock = new MaterialPropertyBlock();

		_bakedMesh = new Mesh
		{
			name = "GhostMesh"
		};
		_meshFilter.mesh = _bakedMesh;
	}

	private void Update()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;

		if (_alpha <= 0f)
		{
			ReturnToPool();
		}
		else
		{
			_meshRenderer.GetPropertyBlock(_propBlock);
			Color c = Color.white;
			c.a = _alpha;
			_propBlock.SetColor(ColorProp, c);
			_meshRenderer.SetPropertyBlock(_propBlock);
		}
	}

	public void Setup(SkinnedMeshRenderer skinnedMesh, Material ghostMaterial, float duration)
	{
		skinnedMesh.BakeMesh(_bakedMesh);

		_meshRenderer.material = ghostMaterial;
		_meshRenderer.GetPropertyBlock(_propBlock);

		_alpha = 1f;
		_fadeSpeed = 1f / duration;
	}

	public void ReturnToPool()
	{
		_pool?.Release(this);
	}

	public void SetPool(IObjectPool<Ghost> pool)
	{
		_pool = pool;
	}
}
