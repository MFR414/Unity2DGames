using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScripts : MonoBehaviour
{
    public GameObject loadingBar;
    public Slider load;
    public Text progressText;

    public void loadGame(int SceneIndex)
    {
        StartCoroutine(loadAsynchronously(SceneIndex));
    }

    IEnumerator loadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingBar.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            load.value =progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
