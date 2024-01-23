using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }
}