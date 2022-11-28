using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NL_Shake : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float duration = 1;

    private Coroutine shakeRoutine;

    void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
        */
    }

    public void Shake()
    {
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float _duration = 0;

        Vector3 startPosition = _transform.localPosition;

        while (_duration < duration)
        {
            Vector3 randomPosition = startPosition + Random.insideUnitSphere * _radius;
            Vector3 lastPosition = _transform.localPosition;

            _duration += Time.deltaTime * 10;

            float time = 0f;

            while (time < 1f)
            {
                _transform.localPosition = Vector3.Lerp(lastPosition, randomPosition, time);

                time += Time.deltaTime * _speed;
                yield return null;
            }

            if (_duration >= duration)
            {
                _transform.localPosition = startPosition;
                StopCoroutine(shakeRoutine);
            }

            yield return null;
        }
    }
}
