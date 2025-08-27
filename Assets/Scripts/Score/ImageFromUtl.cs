using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // Required for Image component

public class ImageFromURL : MonoBehaviour
{
    public string imageUrl;
    public RawImage targetImage;

    void Start()
    {
        StartCoroutine(GetImageFromURL());
    }

    IEnumerator GetImageFromURL()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            targetImage.texture = texture;
        }
    }
}