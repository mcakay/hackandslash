using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private AbilityEffectPayload _payload;
    private GameObject _caster;
    private bool _isFired;

    public void Fire(GameObject caster, AbilityEffectPayload payload, float speed, Vector3 direction)
    {
        _caster = caster;
        _payload = payload;

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = direction.normalized * speed;
        }

        _isFired = true;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isFired) return;

        if (other.gameObject == _caster || other.transform.root.gameObject == _caster)
        {
            return;
        }

        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            hurtbox.ReceiveHit(_payload, transform.position);
            _payload.OnFirstImpact(gameObject, transform.position);
            Destroy(gameObject);
        }
    }
}
