using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;

    void Start()
    {
        StartCoroutine(LoadDataAndScene());
    }

    private IEnumerator LoadDataAndScene()
    {
        // Ensure BackendApi exists
        if (BackendApi.sharedInstance == null)
        {
            GameObject backendApiObject = new GameObject("BackendApi");
            backendApiObject.AddComponent<BackendApi>();
        }

        // Wait for data to load
        while (!BackendApi.sharedInstance.IsDataLoaded)
        {
            Debug.Log("Loading Data");
            yield return null;
        }


        // Load the scene
        SceneManager.LoadScene(sceneToLoad);
    }
}