using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Card : MonoBehaviour
{
    protected CardManager _manager;
    protected Button _button;

    public void Init(CardManager manager)
    {
        _manager = manager;
        _manager.onDeactivate += OnDeactivate;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(Use);
    }

    protected virtual void Use()
    {
        _manager.onDeactivate?.Invoke();
    }

    protected virtual void OnDeactivate()
    {
        _button.interactable = false;
    }
}
