using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    float minLoadTime = 2f;
    float timer;
    WaitForSeconds globalDelay = new WaitForSeconds(2f);

    public BootupGraphicSystem bootupGraphicSystem;

    [Header("Panels")]
    public GameObject firstScene;
    public GameObject secondScene;
    public GameObject thirdScene;

    private void Awake()
    {
        firstScene.SetActive(false);
        secondScene.SetActive(false);
        thirdScene.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadGameSystem());
    }

    IEnumerator LoadGameSystem()
    {
        firstScene.SetActive(true);

        yield return globalDelay;

        firstScene.SetActive(false);
        secondScene.SetActive(true);

        yield return globalDelay;

        secondScene.SetActive(false);
        thirdScene.SetActive(true);


        AsyncOperation operation = SceneManager.LoadSceneAsync(bootupGraphicSystem.sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            if (operation.progress >= 0.9f && timer >= minLoadTime)
            {
                operation.allowSceneActivation = true;
                thirdScene.SetActive(false);
            }
            yield return null;
        }
    }
}
