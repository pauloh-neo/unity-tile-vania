using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
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

        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
