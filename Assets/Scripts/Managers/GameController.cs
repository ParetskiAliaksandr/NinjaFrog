using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("InputManagerScriptableObject")]
    [SerializeField] private InputManagerSO _inputManagerSO;

    [SerializeField] private Transform _prefab;

    private void Start()
    {
        _inputManagerSO.EnableMovementActionMap();

        CreatePrefab();
    }

    private void OnEnable()
    {
        HealthController.OnCharacterDeath += CharacterDeath;
    }

    private void CharacterDeath()
    {
        _inputManagerSO.DisableMovementActionMap();

        StartCoroutine(DelaySceneRestart());
    }

    private IEnumerator DelaySceneRestart()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CreatePrefab()
    {
        var rotation = Quaternion.identity;
        var position = new Vector2(0.0f, 0.0f);

        if(_prefab != null)
        {
            Instantiate(_prefab, position, rotation);
        }
        else
        {
            Debug.LogError("Prefab is null!");
        }
    }

    private void OnDisable()
    {
        HealthController.OnCharacterDeath -= CharacterDeath;
    }
}
