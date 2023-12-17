using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = Mathf.RoundToInt(1.0f / Time.deltaTime).ToString();
    }
}
