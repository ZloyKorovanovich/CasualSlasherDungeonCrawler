using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FillBar : FillAnimation
{
    public Material materialReference;

    private MeshRenderer _renderer;
    private float _currentFill;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = new Material(materialReference);
        _renderer.material.SetFloat("_Amount", 0.0f);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }

    protected override IEnumerator Animate(float amount)
    {
        int iterations = Mathf.RoundToInt((int)frameRate * time);
        var smoothStep = (amount - _currentFill) / iterations;
        _isCourutine = true;

        for (int i = 0; i < iterations; i++)
        {
            _currentFill += smoothStep;
            _renderer.material.SetFloat("_Amount", _currentFill);

            yield return new WaitForSeconds(time / iterations);
        }

        _isCourutine = false;
    }
}
