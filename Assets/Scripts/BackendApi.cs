using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BackendApi : MonoBehaviour
{
    private const string ServerUrl = "http://localhost:3000";

    void Awake()
    {
        GetScores();
    }

    // Función para OBTENER datos
    public void GetScores()
    {
        StartCoroutine(GetScoresCoroutine());
    }

    private IEnumerator GetScoresCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ServerUrl + "/api/scores"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener puntuaciones: " + request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                string wrappedJson = "{\"data\":" + jsonResponse + "}";
                Debug.Log("Respuesta del servidor: " + wrappedJson);
                ScoreListResponse responseData = JsonUtility.FromJson<ScoreListResponse>(wrappedJson);

                foreach (var score in responseData.data)
                {
                    Debug.Log($"Puntuación: {score.points} - {score.username}");
                }
            }
        }
    }

}

