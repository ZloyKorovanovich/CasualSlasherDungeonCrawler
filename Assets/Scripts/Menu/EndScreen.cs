using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public CharacterMain player;
    public GameObject defeat;
    public GameObject victory;

    private void Start()
    {
        player.onDeath += Defeat;
    }

    public void Defeat()
    {
        defeat.SetActive(true);
        StartCoroutine(EndCallDown());
    }

    public void Win()
    {
        victory.SetActive(true);
        StartCoroutine(EndCallDown());
    }

    private IEnumerator EndCallDown()
    {
        yield return new WaitForSeconds(5f);
        Exit();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
