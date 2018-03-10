using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    public float zoomSpeed = 2f;

    public static Main Instance { set; get; }
    public float latitude;
    public float longitude;

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
        if(!Debug.isDebugBuild)
            StartCoroutine(StartLocationServices());
    }

    private IEnumerator StartLocationServices()
    {
        if (!Input.location.isEnabledByUser)
        {
            SceneManager.LoadScene(3);
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;

        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
               yield return new WaitForSeconds(1);
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

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the canvas size based on the change in distance between the touches.
            Images[currentMap].rectTransform.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;

            // Make sure the canvas size never drops below 0.1
            if(Images[currentMap].rectTransform.localScale.magnitude < 1f)
            {
                Images[currentMap].rectTransform.localScale = Vector3.one;
            }
            if(Images[currentMap].rectTransform.localScale.magnitude > 5f){
                Images[currentMap].rectTransform.localScale = new Vector3 (5f, 5f, 5f);
            }
        }
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
