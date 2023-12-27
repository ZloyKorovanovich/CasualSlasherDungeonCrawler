using System.Collections;
using UnityEngine;

public enum FramesPerSecond
{
    fps_15 = 15,
    fps_17 = 15,
    fps_27 = 27,
    fps_30 = 30,
    fps_60 = 60,
    fps_120 = 120
}

public abstract class CustomAnimation : MonoBehaviour
{
    [SerializeField]
    protected FramesPerSecond _frameRate = FramesPerSecond.fps_60;
    [SerializeField]
    protected float _time = 1.0f;
}

public abstract class FillAnimation : CustomAnimation
{
    protected bool _isCourutine;

    public virtual void Fill(float amount)
    {
        if (_isCourutine)
            StopCoroutine(Animate(0.0f));

        StartCoroutine(Animate(amount));
    }

    protected abstract IEnumerator Animate(float amount);

    private void OnDisable()
    {
        StopAllCoroutines();
        _isCourutine = false;
    }
}
