using System.Collections;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
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

    public void PickUpAnimation()
    {
        StartCoroutine(Destroing());
    }

    private IEnumerator Destroing()
    {
        int iterations = 15;
        var step = transform.localScale / iterations;
        for(int i = 0; i < iterations; i++)
        {
            transform.localScale -= step;
            yield return new WaitForSeconds(0.5f / iterations);
        }

        Destroy(gameObject);
    }
}