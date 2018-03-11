using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    public float zoomSpeed = .1f;

    public static Main Instance { set; get; }
    public float latitude;
    public float longitude;

    public Text gpsText;

    public Image[] Images;
    public int imageCount = 12;
    public int currentMap = 0;
    public Button nextMap;
    public Button previousMap;

    // Use this for initialization
    void Start() {
        Instance = this;
        for (int i = 1; i < imageCount; i++)
        {
            var tempColor = Images[i].color;
            tempColor.a = 0f;
            Images[i].color = tempColor;
        }
        nextMap.onClick.AddListener(NextMap);
        previousMap.onClick.AddListener(PreviousMap);
        if (!Debug.isDebugBuild)
        {
            StartCoroutine(StartLocationServices());
        }
    }

    private IEnumerator StartLocationServices()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("user hasnt enabled gps");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;

        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if(maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
    }

    // Update is called once per frame
    void Update () {
        
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        gpsText.text = "LAT: " + latitude.ToString() + Environment.NewLine + "LON: " + longitude.ToString();
    
    }

    void NextMap()
    {
        var tempColor = Images[currentMap].color;
        tempColor.a = 0f;
        Images[currentMap].color = tempColor;
        currentMap = (currentMap + 1) % imageCount;
        tempColor = Images[currentMap].color;
        tempColor.a = 1f;
        Images[currentMap].color = tempColor;
    }

    void PreviousMap()
    {
        var tempColor = Images[currentMap].color;
        tempColor.a = 0f;
        Images[currentMap].color = tempColor;
        currentMap--;
        if (currentMap < 0) { currentMap = 11;}
        tempColor = Images[currentMap].color;
        tempColor.a = 1f;
        Images[currentMap].color = tempColor;
    }
}
