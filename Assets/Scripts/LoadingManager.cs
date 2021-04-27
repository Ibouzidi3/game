using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;
    public Text loadingText;
    public GameObject loadingPanel;
    public Canvas menuCanvas;


    private string scene;
    private CanvasGroup canvasGroup;

    void Awake()
    {

        scene = SceneManager.GetActiveScene().name; 
        if (scene == "LoadingScene")
        {
            LoadScene("Menu");
        }
        else
        {
            canvasGroup = menuCanvas.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        
    }

    public void LoadScene(string sceneName)
    { 
        if (scene == "EndOfRace")
        { 
            loadingPanel.SetActive(true); 
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;


        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            progress = Mathf.Round(progress * 100f) / 100f;
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";
            yield return null;
        }
    }
}