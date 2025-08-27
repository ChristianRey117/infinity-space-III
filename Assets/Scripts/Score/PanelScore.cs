using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelScore : MonoBehaviour
{
    public GameObject scorePanel;
    public float panelSpacing = 30f;
    private BackendApi _backendApi;

    void Awake()
    {
        _backendApi = BackendApi.sharedInstance;
    }
    void Start()
    {
        if (_backendApi != null && _backendApi.scoreListResponse != null && _backendApi.scoreListResponse.data != null)
        {

            Vector3 currentPosition = new Vector3(0, -100, 0);
            foreach (var scoreData in _backendApi.scoreListResponse.data)
            {
                GameObject panel = Instantiate(scorePanel, transform.position, transform.rotation, transform);

                panel.GetComponent<RectTransform>().anchoredPosition = currentPosition;

                Debug.Log("currentPosition: " + panel.GetComponent<RectTransform>().anchoredPosition);

                TextMeshProUGUI usernameText = panel.transform.Find("Username").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI pointsText = panel.transform.Find("Points").GetComponent<TextMeshProUGUI>();
                ImageFromURL flagImage = panel.transform.Find("FlagImage").GetComponent<ImageFromURL>();
                if (usernameText != null && pointsText != null && flagImage != null)
                {
                    flagImage.imageUrl = scoreData.flag;
                    usernameText.text = scoreData.username;
                    pointsText.text = scoreData.points.ToString(); // Convert points to string
                }
                currentPosition -= new Vector3(0, panelSpacing, 0);

                // Aquí deberías acceder a los elementos del panel (ej: Text) y asignar los valores de scoreData
                // Ejemplo: panel.GetComponentInChildren<Text>().text = scoreData.username + " - " + scoreData.points;
            }

        }
        else
        {
            Debug.LogWarning("No se recibieron datos de puntuación del backend.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
