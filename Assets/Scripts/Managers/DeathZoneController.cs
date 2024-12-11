using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HealthController>(out var player))
        {
            player.Death();
        }
    }
}

