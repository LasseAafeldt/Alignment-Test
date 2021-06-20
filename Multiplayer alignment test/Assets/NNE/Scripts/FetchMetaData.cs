using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;
using System;
using System.Globalization;

public class FetchMetaData : MonoBehaviour
{
    private string filename = "QRMetaData.csv"; //not serialized on purpose so we don't accidentally mess it up
    public List<QRData> qrData; //TODO: should be safety checked 
    string[] headers;


    private void Start()
    {
        qrData = new List<QRData>();
        StartCoroutine(GetText(filename));
    }

    IEnumerator GetText(string file_name)
    {
        string url = @"https://nnedigitaldesignstorage.blob.core.windows.net/candidatetasks/Metadata.csv?sp=r&st=2021-03-15T09:12:39Z&se=2024-11-05T17:12:39Z&spr=https&sv=2020-02-10&sr=b&sig=oyj3Qyg4W42%2BO0d7YqmjxmKk0k%2BLVmE243ixdLaq3gk%3D";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                string savePath = string.Format("{0}/{1}", Application.persistentDataPath, file_name);
                File.WriteAllText(savePath, webRequest.downloadHandler.text); //if file already exists it is overwritten
            }
        }
    }

    public void ReadFile(string file_name)
    {
        qrData.Clear(); //make sure we have a clean slate

        string filepath = string.Format("{0}/{1}", Application.persistentDataPath, file_name);
        Debug.Log("Reading file " + filepath);

        string[] headers;

        List<string> lines = new List<string>();
        if (File.Exists(filepath))
        {
            lines = File.ReadAllLines(filepath).ToList();
            Debug.Log("Amount of lines = " + lines.Count);
            headers = lines.ElementAt(0).Split(';');
        }
        else
        {
            Debug.LogError("File at path " + filepath + " doesn't exist.");
            return;
        }

        //split up each line into proper data types
        foreach (string line in lines)
        {
            QRData data;
            string[] entries = line.Split(';');

            // read  and store all the log data
            //int id;
            //string name;
            //string info;
            //float percentage;
            //string lastEditDate;
            //string description;
            try
            {
                data.id = int.Parse(entries[0], CultureInfo.InvariantCulture);
                data.name = entries[1].ToString();
                data.info = entries[2].ToString();
                data.percentage = float.Parse(entries[3], CultureInfo.InvariantCulture);
                data.lastEditDate = entries[4].ToString();
                data.description = entries[5].ToString();

                //add data so it's useable
                qrData.Add(data);
            }
            catch (FormatException)
            {
                Debug.Log("Unable to parse data");
            }           
        }
    }

    public void DisplayQRData(int id)
    {
        ReadFile(filename);
        string print = string.Format("ID = {0}, " + " Name = {1}, " + " Info = {2}, " + " % = {3}, " + " Last edit date = {4}, " + " Description = {5}",
            qrData[id].id, qrData[id].name, qrData[id].info, qrData[id].percentage, qrData[id].lastEditDate, qrData[id].description);
        Debug.Log(print);
        //Debug.Log("QR-data element = " + qrData.ElementAt(id).name);
    }
}
