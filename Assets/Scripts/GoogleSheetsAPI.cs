using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class GoogleSheetsAPI : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [SerializeField]
    private bool prueba = false;

    private string url_hoja1 = "https://api.sheety.co/9573aae31132721e38a3adef70b34366/tfgBbdd/hoja1";
    private string url_hoja2 = "https://api.sheety.co/9573aae31132721e38a3adef70b34366/tfgBbdd/hoja2";

    private int idsesion;
    private int idtiempos;

    private void Update()
    {
        if (prueba)
        {
            prueba = false;
            SendTimeData();
        }
    }

    public void SendTimeData()
    {
        dataManager.stopTimer();
        StartCoroutine(GenerateUserIdAndPostTimeData(dataManager.getFinalTime(), dataManager.getSLFinalTime(), dataManager.getCLFinalTime()));
    }

    IEnumerator GenerateUserIdAndPostTimeData(string tiempototal, string tiempot1, string tiempot2)
    {
        // Obtener el último ID de sesión desde la hoja de cálculo
        using (UnityWebRequest www = UnityWebRequest.Get(url_hoja1))
        {
            Debug.Log("1: Sending GET request");
            yield return www.SendWebRequest();
            Debug.Log("2: GET request completed");

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("GET request error: " + www.error);
                yield break;
            }

            Debug.Log("GET request successful. Parsing response...");
            var responseData = www.downloadHandler.text;
            Debug.Log("Response data: " + responseData); // Imprimir el JSON recibido

            SheetyResponse sheetData = null;
            try
            {
                sheetData = JsonConvert.DeserializeObject<SheetyResponse>(responseData);
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing JSON: " + e.Message);
                yield break;
            }

            if (sheetData.hoja1 != null && sheetData.hoja1.Count > 0)
            {
                idsesion = sheetData.hoja1[sheetData.hoja1.Count - 1].idsesion + 1;
            }
            else
            {
                idsesion = 1; // Si no hay datos, empezar con el idsesion 1
            }
            Debug.Log("New userId: " + idsesion);

            // Guardar el nuevo ID de sesion y el timestamp en la hoja de cálculo
            var values = new Dictionary<string, object>
            {
                { "idsesion", idsesion },
                { "timestamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };
            string json = JsonConvert.SerializeObject(new { hoja1 = values });
            Debug.Log("JSON data to send: " + json);

            using (UnityWebRequest postRequest = UnityWebRequest.Post(url_hoja1, ""))
            {
                postRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
                postRequest.uploadHandler.contentType = "application/json";
                postRequest.SetRequestHeader("Content-Type", "application/json");
                Debug.Log("Sending POST request to: " + url_hoja1);

                yield return postRequest.SendWebRequest();
                dataManager.setFinished(true);
                Debug.Log("POST request completed");

                if (postRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("POST request error: " + postRequest.error);
                    Debug.LogError("POST request response: " + postRequest.downloadHandler.text); // Mostrar cualquier respuesta de error
                }
                else
                {
                    Debug.Log("Data was sent successfully!");
                    Debug.Log("POST request response: " + postRequest.downloadHandler.text); // Mostrar la respuesta del servidor
                }
            }
        }

        // Obtener el último ID de tiempos desde la segunda hoja
        using (UnityWebRequest www = UnityWebRequest.Get(url_hoja2))
        {
            Debug.Log("1: Sending GET request");
            yield return www.SendWebRequest();
            Debug.Log("2: GET request completed");

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("GET request error: " + www.error);
                yield break;
            }

            Debug.Log("GET request successful. Parsing response...");
            var responseData = www.downloadHandler.text;
            Debug.Log("Response data: " + responseData); // Imprimir el JSON recibido

            SheetyResponse sheetData = null;
            try
            {
                sheetData = JsonConvert.DeserializeObject<SheetyResponse>(responseData);
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing JSON: " + e.Message);
                yield break;
            }

            if (sheetData.hoja2 != null && sheetData.hoja2.Count > 0)
            {
                idtiempos = sheetData.hoja2[sheetData.hoja2.Count - 1].idtiempos + 1;
            }
            else
            {
                idtiempos = 1; // Si no hay datos, empezar con el idtiempos 1
            }
            Debug.Log("New id_tiempos: " + idtiempos);

            // Guardar el nuevo ID de tiempos y los datos de tiempo en la hoja de cálculo
            var values = new Dictionary<string, object>
            {
                { "idtiempos", idtiempos },
                { "idsesion", idsesion },
                { "tiempototal", tiempototal },
                { "tiempot1", tiempot1 },
                { "tiempot2", tiempot2 }
            };
            string json = JsonConvert.SerializeObject(new { hoja2 = values });
            Debug.Log("JSON data to send: " + json);

            using (UnityWebRequest postRequest = UnityWebRequest.Post(url_hoja2, ""))
            {
                postRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
                postRequest.uploadHandler.contentType = "application/json";
                postRequest.SetRequestHeader("Content-Type", "application/json");
                Debug.Log("Sending POST request to: " + url_hoja2);

                yield return postRequest.SendWebRequest();
                dataManager.setFinished(true);
                Debug.Log("POST request completed");

                if (postRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("POST request error: " + postRequest.error);
                    Debug.LogError("POST request response: " + postRequest.downloadHandler.text); // Mostrar cualquier respuesta de error
                }
                else
                {
                    Debug.Log("Data was sent successfully!");
                    Debug.Log("POST request response: " + postRequest.downloadHandler.text); // Mostrar la respuesta del servidor
                }
            }
        }
    }
}

public class SheetyResponse
{
    [JsonProperty("hoja1")]
    public List<SheetData> hoja1 { get; set; }
    [JsonProperty("hoja2")]
    public List<SheetData> hoja2 { get; set; }
}

public class SheetData
{
    [JsonProperty("idsesion")]
    public int idsesion { get; set; }
    [JsonProperty("timestamp")]
    public string timestamp { get; set; }
    [JsonProperty("idtiempos")]
    public int idtiempos { get; set; }
    [JsonProperty("tiempototal")]
    public string tiempototal { get; set; }
    [JsonProperty("tiempot1")]
    public string tiempot1 { get; set; }
    [JsonProperty("tiempot2")]
    public string tiempot2 { get; set; }
}
