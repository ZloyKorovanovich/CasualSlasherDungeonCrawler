using System.Collections;
using UnityEngine;

public class ItemAnimation : CustomAnimation
{
    [SerializeField]
    private float _rotateSpeed = 10.0f;
    [SerializeField]
    private float _movingSpeed = 5.0f;
    [SerializeField]
    private float _movingAmplitude = 0.3f;
    [SerializeField]
    private float _offset = 1.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x,
            Mathf.Sin(Time.time * _movingSpeed) * _movingAmplitude + _offset, transform.position.z);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void PickUpAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(Destroing());
    }

    public void StartAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(Starting());
    }

    private IEnumerator Destroing()
    {
        int iterations = Mathf.RoundToInt(_time * (int)_frameRate);
        var step = transform.localScale / iterations;
        for(int i = 0; i < iterations; i++)
        {
            transform.localScale -= step;
            yield return new WaitForSeconds(_time / iterations);
        }

        Destroy(gameObject);
    }

    private IEnumerator Starting()
    {
        int iterations = Mathf.RoundToInt(_time * (int)_frameRate);
        var step = transform.localScale / iterations;
        transform.localScale = Vector3.zero;
        for (int i = 0; i < iterations; i++)
        {
            transform.localScale += step;
            yield return new WaitForSeconds(_time / iterations);
        }
    }
}
