using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Text;

public class FloatingDamage : MonoBehaviour, IPooledObject<FloatingDamage>
{
    [SerializeField] private FloatingDamageConfig _config;
    [SerializeField] private TextMeshPro _textMesh;

    private Color _textColor;
    private Camera _mainCamera;
    private IObjectPool<FloatingDamage> _pool;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.position += _config.MoveSpeed * Time.deltaTime * Vector3.up;
        transform.forward = _mainCamera.transform.forward;

        _textColor.a -= _config.FadeSpeed * Time.deltaTime;
        _textMesh.color = _textColor;

        if (_textColor.a <= 0)
        {
            _pool?.Release(this);
        }
    }

    public void SetPool(IObjectPool<FloatingDamage> pool)
    {
        _pool = pool;
    }

    public void Setup(float damageAmount)
    {
        _textMesh.text = ZString.Format("{0:0}", damageAmount);

        _textColor = _textMesh.color;
        _textColor.a = 1f;
        _textMesh.color = _textColor;
    }

    public void ReturnToPool()
    {
        _pool?.Release(this);
    }
}
