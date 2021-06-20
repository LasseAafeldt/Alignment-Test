using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayData : MonoBehaviour
{
    public FetchMetaData metaData;
    [SerializeField] private Canvas dataCanvas;
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI info;
    [SerializeField] private TextMeshProUGUI percentage;
    [SerializeField] private TextMeshProUGUI lastEdit;
    [SerializeField] private TextMeshProUGUI description;

    public void OnDisplayData(int _id) //would probably be smarter if this worked with recognizing the name, but this is what we get right now
    {
        dataCanvas.gameObject.SetActive(true); //TODO: use canvas group to make visible/invisible when needed. make invisible when new alignment is requested
        metaData.DisplayQRData(_id-1); //printing to console incase i don't get Ui to work

        QRData data = metaData.qrData[_id-1];

        id.text = "ID: " + data.id.ToString();
        name.text = "Name: " + data.name;
        info.text = "Info: " + data.info;
        percentage.text = data.percentage.ToString() + "%";
        lastEdit.text = "Edite date: " + data.lastEditDate;
        description.text = "Description: " + data.description;
    }
}
