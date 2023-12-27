using System.Collections;
using UnityEngine;

public class Spikes : FillAnimation
{
    [SerializeField]
    private Vector3 _downPos;
    [SerializeField]
    private Vector3 _upPos;

    protected override IEnumerator Animate(float amount)
    {
        int iterations = Mathf.RoundToInt((int)_frameRate * _time);
        var smoothStep = amount / iterations;
        _isCourutine = true;

        for (int i = 0; i < iterations; i++)
        {
            transform.localPosition += smoothStep * _upPos - _downPos;
            yield return new WaitForSeconds(_time / iterations);
        }

        _isCourutine = false;
    }
}
