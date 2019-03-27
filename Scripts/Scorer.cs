using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorer : MonoBehaviour {

    public float scorer;
    public TextMeshProUGUI score;
    public TextMeshProUGUI hs;
    public MainMenu mainMenu;
    public float hsc;
    
    // Use this for initialization
	void Start () {
        score = GetComponent<TextMeshProUGUI>();
        scorer = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (!mainMenu.paused)
        {
            scorer += Mathf.RoundToInt(Time.timeSinceLevelLoad);
            score.text = Mathf.RoundToInt(scorer * 0.01f).ToString();
            hsc = Mathf.RoundToInt(scorer * 0.01f);
        }
	}
}
