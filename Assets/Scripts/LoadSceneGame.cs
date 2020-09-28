using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGame : MonoBehaviour
{
    public int indexScene;

    void OnEnable(){
        SceneManager.LoadScene(indexScene);
        if (indexScene == 4)
        {
            MusicController.Instance.PlayBuriel();
        }
    }
}
