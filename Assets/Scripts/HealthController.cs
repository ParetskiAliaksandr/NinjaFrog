using System;
using UnityEngine;

[RequireComponent (typeof(MovementController))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    private readonly float _timeAnimDeath = 1;
    private readonly float _deathImpulseStrength = 50f;

    private readonly int _layer = 6;

    private MovementController _movementController;

    public static event Action OnCharacterDeath;
    public static event Action<int, int> OnUpdateHealth;

    private void OnEnable()
    {
        _movementController = GetComponent<MovementController>();

        _currentHealth = _maxHealth;
    }

    public void Hurt(int damage)
    {
        _currentHealth = _currentHealth - damage;

        OnUpdateHealth?.Invoke(_currentHealth, _maxHealth);

        AnimHitPlayer();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void AddHealth(int healAmount)
    {
        if(_currentHealth >= _maxHealth)
        {
            return;
        }

        _currentHealth = _currentHealth + healAmount;

        OnUpdateHealth?.Invoke(_currentHealth, _maxHealth);
    }

    public void Death()
    {
        _movementController.Animator.SetTrigger("isDeath");

        Destroy(gameObject, _timeAnimDeath);

        SetDeathBehavoir();

        OnCharacterDeath?.Invoke();
    }

    private void SetDeathBehavoir()
    {
        _movementController.Body2D.AddForce(Vector2.up * _deathImpulseStrength, ForceMode2D.Impulse);
        _movementController.SpriteRenderer.sortingOrder = _layer;
        _movementController.BoxCol2D.enabled = false;
    }

    private void AnimHitPlayer()
    {
        _movementController.Animator.SetTrigger("isHit");
    }
}
