using TMPro;
using UnityEngine;

public class CrystalCounter : MonoBehaviour
{
    public TMP_Text text;
    private CrystalManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<CrystalManager>();
        UpdateText();
        _manager.onAdd += UpdateText;
    }

    private void OnDisable()
    {
        if(_manager)
            _manager.onAdd -= UpdateText;
    }

    private void UpdateText()
    {
        text.text = _manager.Crystals.ToString();
    }
}
