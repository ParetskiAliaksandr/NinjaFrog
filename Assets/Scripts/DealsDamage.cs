using UnityEngine;

public class DealsDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HealthController>(out var player))
        {
            player.Hurt(_damage);
        }
    }
}
