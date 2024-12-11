using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameObject : MonoBehaviour
{
    [SerializeField] private int _numberKeys;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Pocket>(out var pocket))
        {
            int amount = pocket.GetKeyAmount();

            if(amount == _numberKeys)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } 
        }
    }
}
