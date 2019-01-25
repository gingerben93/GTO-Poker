using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpRequest : MonoBehaviour {


    public const string URL = "http://www.google.com";
    public const string URI = "http://10.0.0.36:5000";
    public Text ResponseText;

    public static HttpRequest HttpRequestSingle;

    //game variables
    public string value1;
    public string suit1;
    public string value2;
    public string suit2;
    public string value3;
    public string suit3;
    public string value4;
    public string suit4;
    public string value5;
    public string suit5;
    public string value6;
    public string suit6;
    public string value7;
    public string suit7;

    void Awake()
    {
        if (HttpRequestSingle == null)
        {
            HttpRequestSingle = this;
        }
        else if (HttpRequestSingle != this)
        {
            Destroy(gameObject);
        }
    }

    //private void Start()
    //{
    //    StartCoroutine(GetText());
    //}

    public void OnRequest()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        //must be of this form
        //string jsonString = "{\"top\": {\"card1\": {\"value\": \"" + value1 + "\" ,\"suit\": \"" + suit1 + "\"}}," +
        //                    "\"middle\": {\"card1\": {\"value\": \"" + value2 + "\" ,\"suit\": \"" + suit2 + "\"},\"card2\": {\"value\": \"" + value3 + "\" ,\"suit\": \"" + suit3 + "\"}}," +
        //                    "\"Bottom\": {\"card1\": {\"value\": \"" + value4 + "\" ,\"suit\": \"" + suit4 + "\"},\"card2\": {\"value\": \"" + value5 + "\" ,\"suit\": \"" + suit5 + "\"},\"card3\": {\"value\": \"" + value6 + "\" ,\"suit\": \"" + suit6 + "\"},\"card4\": {\"value\": \"" + value7 + "\" ,\"suit\": \"" + suit7 + "\"}}}";
        
        ////encode for put request
        //var formData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        //var request = new UnityWebRequest(URI, "POST");
        //byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //request.SetRequestHeader("Content-Type", "application/json");

        //yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        //ResponseText.text = request.downloadHandler.text;

        ////GET works fine
        UnityWebRequest wwwRequest = UnityWebRequest.Get(URI);

        //UnityWebRequest wwwRequest = UnityWebRequest.Post(URI, jsonString);
        ////must set header
        //wwwRequest.SetRequestHeader("Content-Type", "application/json");

        ////if you want to appear as a post request
        ////wwwRequest.method = UnityWebRequest.kHttpVerbPOST;
        //wwwRequest.timeout = 2;
        yield return wwwRequest.SendWebRequest();
        if (wwwRequest.isNetworkError || wwwRequest.isHttpError)
        {
            Debug.Log(wwwRequest.error);
        }
        else
        {
            // Show results as text
            Debug.Log(wwwRequest.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = wwwRequest.downloadHandler.data;
            ResponseText.text = wwwRequest.downloadHandler.text;
        }
    }
    
    //public void OnRequest()
    //{
    //    StartCoroutine(GetText());
    //}

    //IEnumerator GetText()
    //{
    //    string suit1 = "spades";
    //    string value1 = "3";
    //    string suit2 = "hearts";
    //    string value2 = "12";

    //    string jsonStringWithVariables = "{ \"" + value1 + "\" : \"" + suit1 + "\", \"" + value2 + "\" : \"" + suit2 + "\"}";

    //    //must be of this form
    //    string jsonString = "{ \"name\" : \"ben\"}";
    //    //encode for put request
    //    var formData = System.Text.Encoding.UTF8.GetBytes(jsonStringWithVariables);

    //    //GET works fine
    //    //UnityWebRequest wwwRequest = UnityWebRequest.Get(URI);

    //    UnityWebRequest wwwRequest = UnityWebRequest.Put(URI, formData);
    //    //must set header
    //    wwwRequest.SetRequestHeader("Content-Type", "application/json");

    //    yield return wwwRequest.SendWebRequest();

    //    if (wwwRequest.isNetworkError || wwwRequest.isHttpError)
    //    {
    //        Debug.Log(wwwRequest.error);
    //    }
    //    else
    //    {
    //        // Show results as text
    //        Debug.Log(wwwRequest.downloadHandler.text);

    //        // Or retrieve results as binary data
    //        byte[] results = wwwRequest.downloadHandler.data;
    //        ResponseText.text = wwwRequest.downloadHandler.text;
    //    }
    //}
}
