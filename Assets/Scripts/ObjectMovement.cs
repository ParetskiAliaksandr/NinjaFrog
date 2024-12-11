using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _finishPos;

    [SerializeField] private float _time;

    private float _currentTime;

    private void Update()
    {
        _currentTime = _currentTime + Time.deltaTime;
        var normilezedTime = _currentTime / _time;
        transform.position = Vector2.Lerp(_startPos.position, _finishPos.position, Mathf.PingPong(normilezedTime, 1.0f));
    }
}
