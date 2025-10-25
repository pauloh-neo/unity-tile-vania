using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        yield return StartCoroutine("LoadNextLevel");
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(2);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
