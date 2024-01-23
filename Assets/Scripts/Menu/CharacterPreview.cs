using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public GameObject character;
    public float sensetivity = 30f;
    public float targetAngle;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        character.transform.rotation = Quaternion.Euler(Vector3.up * Mathf.LerpAngle(character.transform.eulerAngles.y, targetAngle, Time.deltaTime * sensetivity));
    }
}
