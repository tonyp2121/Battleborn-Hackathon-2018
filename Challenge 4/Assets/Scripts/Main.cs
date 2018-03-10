using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public Image[] Images;
    public int imageCount = 12;
    public int currentMap = 0;
    public Button nextMap;
    public Button previousMap;

    // Use this for initialization
    void Start() {
        for (int i = 1; i < imageCount; i++)
        {
            var tempColor = Images[i].color;
            tempColor.a = 0f;
            Images[i].color = tempColor;
        }
        nextMap.onClick.AddListener(NextMap);
        previousMap.onClick.AddListener(PreviousMap);
    }
	
	// Update is called once per frame
	void Update () {
		
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
