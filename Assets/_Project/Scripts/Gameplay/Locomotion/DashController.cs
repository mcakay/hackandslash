using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    private Rigidbody _rb;
    private bool _isDashing;

    private Vector3 _dashDirection;
    private float _dashSpeed;

    private readonly Timer _dashTimer = new();
    private readonly Timer _delayTimer = new();

    private GhostFactorySO _ghostFactory;
    private Material _ghostMaterial;
    private float _ghostFadeDuration;

    private int _maxGhosts;
    private int _spawnedGhosts;
    private Vector3 _lastGhostSpawnPos;
    private float _sqrDistanceBetweenGhosts;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void StartDash(Vector3 direction, in DashSettings settings)
    {
        if (_isDashing) return;

        _isDashing = true;
        _dashDirection = direction.normalized;
        _dashSpeed = settings.Speed;

        _rb.useGravity = false;

        _ghostFactory = settings.Factory;
        _ghostMaterial = settings.Material;
        _ghostFadeDuration = settings.FadeDuration;

        _maxGhosts = settings.Count;
        _sqrDistanceBetweenGhosts = settings.DistanceBetween * settings.DistanceBetween;
        _spawnedGhosts = 0;

        _dashTimer.Start(settings.Duration);

        if (settings.SpawnDelay > 0f)
        {
            _delayTimer.Start(settings.SpawnDelay);
        }
        else
        {
            _delayTimer.Stop();

            if (_ghostFactory != null && _maxGhosts > 0)
            {
                SpawnGhost();
                _lastGhostSpawnPos = transform.position;
                _spawnedGhosts++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing) return;

        float dt = Time.fixedDeltaTime;
        _dashTimer.Tick(dt);

        if (_dashTimer.IsRunning)
        {
            _rb.linearVelocity = new Vector3(_dashDirection.x * _dashSpeed, 0f, _dashDirection.z * _dashSpeed);
            HandleGhostSpawning(dt);
        }
        else
        {
            _isDashing = false;
            _rb.useGravity = true;
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
        }
    }

    private void HandleGhostSpawning(float dt)
    {
        if (_ghostFactory == null || _spawnedGhosts >= _maxGhosts) return;

        if (_delayTimer.IsRunning)
        {
            _delayTimer.Tick(dt);

            if (!_delayTimer.IsRunning)
            {
                SpawnGhost();
                _lastGhostSpawnPos = transform.position;
                _spawnedGhosts++;
            }
            return;
        }

        Vector3 currentPos = transform.position;

        if ((currentPos - _lastGhostSpawnPos).sqrMagnitude >= _sqrDistanceBetweenGhosts)
        {
            SpawnGhost();
            _lastGhostSpawnPos = currentPos;
            _spawnedGhosts++;
        }
    }

    private void SpawnGhost()
    {
        if (skinnedMesh == null) return;

        Ghost ghost = _ghostFactory.Get(skinnedMesh.transform.position, skinnedMesh.transform.rotation);
        ghost.Setup(skinnedMesh, _ghostMaterial, _ghostFadeDuration);
    }
}
