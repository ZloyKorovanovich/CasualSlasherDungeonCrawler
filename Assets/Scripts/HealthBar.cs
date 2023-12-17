using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Material _materialReference;

    private MeshRenderer _renderer;
    private float _currentFill;

    private bool _isCourutine;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = new Material(_materialReference);
        _renderer.material.SetFloat("_Amount", 0.0f);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void FillBar(float amount)
    {
        if(_isCourutine)
            StopCoroutine(FillSmothely(0.0f));

        StartCoroutine(FillSmothely(amount));
    }

    private IEnumerator FillSmothely(float amount)
    {
        int iterations = 15;
        var smoothStep = (amount - _currentFill) / iterations;
        _isCourutine = true;

        for (int i = 0; i < iterations; i++)
        {
            _currentFill += smoothStep;
            _renderer.material.SetFloat("_Amount", _currentFill);

            yield return new WaitForSeconds(0.5f / iterations);
        }

        _isCourutine = false;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        _isCourutine = false;
    }
}
