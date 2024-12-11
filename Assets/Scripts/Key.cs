using UnityEngine;

public class Key : MonoBehaviour, IGameItem
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Pocket>(out var pocket))
        {
            pocket.AddKey(this);

            Destroy(this.gameObject);
        }
    }
}
