using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetServerInfo : MonoBehaviour
{

    public const string URI = "http://10.0.0.36:5000/get_free_cards";

    //unity button
    public Text ResponseText;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(GetFreeCards());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetFreeCardsFromServer()
    {

    }

    public class MyClass
    {
        public string values;
        public string suits;
    }

    IEnumerator GetFreeCards()
    {
        UnityWebRequest wwwRequest = UnityWebRequest.Get(URI);
        yield return wwwRequest.SendWebRequest();
        if (wwwRequest.isNetworkError || wwwRequest.isHttpError)
        {
            Debug.Log(wwwRequest.error);
        }
        else
        {
            // Show results as text
            Debug.Log(wwwRequest.downloadHandler.text);
            ResponseText.text = wwwRequest.downloadHandler.text;

            string[] words = wwwRequest.downloadHandler.text.Split('[',']');
            string valuesList = words[2];
            string[] valuesString = valuesList.Split(',');
            Debug.Log(valuesList.GetType());

            List<int> values = new List<int>();
            foreach (var c in valuesString)
            {
                values.Add(Int32.Parse(c));
            }

            foreach( var i in values)
            {
                Debug.Log(i);
            }

            // Or retrieve results as binary data
            byte[] results = wwwRequest.downloadHandler.data;
            var str = System.Text.Encoding.Default.GetString(results);
            Debug.Log(str);
            
            
        }

    }
}
