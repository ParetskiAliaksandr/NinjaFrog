using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator { get { if (_animator == null) _animator = GetComponent<Animator>(); return _animator; } }

    private readonly float _timeStopAnim = 0.5f;

    private IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(_timeStopAnim);

        Animator.SetBool("isCollision", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator.SetBool("isCollision", true);

        StartCoroutine(StopAnim());
    }
}
