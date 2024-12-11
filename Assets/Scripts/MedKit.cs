using UnityEngine;

public class MedKit : MonoBehaviour, IGameItem
{
    [SerializeField] private int _heal;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HealthController>(out var player))
        {
            player.AddHealth(_heal);

            Destroy(this.gameObject);
        }
    }
}
