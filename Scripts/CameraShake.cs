using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float boostPower;
    public float deathPower;
    public float duration = 1;
    public Transform camera;
    public float slowDownAmount = 1;
    public static bool boostShake = false;
    public static bool deathShake = false;

    Vector3 startPosition;
    float initialDuration;

    public CanvasGroup fadeGroup;
    public float fadeInSpeed = 0.33f;

    // Use this for initialization
    void Start () {
        camera = Camera.main.transform;
        startPosition = camera.localPosition;
        initialDuration = duration;

        fadeGroup = FindObjectOfType<CanvasGroup>();
    }
	
	// Update is called once per frame
	void Update () {
        if (boostShake)
        {
            if (duration > 0)
            {
                camera.localPosition = startPosition + Random.insideUnitSphere * boostPower;
                duration -= Time.deltaTime * slowDownAmount;
            }

            else
            {
                boostShake = false;
                duration = initialDuration;
                camera.localPosition = startPosition;
            }
        }

        if (deathShake)
        {
            if (duration > 0)
            {
                fadeGroup.alpha = duration - Time.deltaTime * slowDownAmount;
                camera.localPosition = startPosition + Random.insideUnitSphere * deathPower;
                duration -= Time.deltaTime * slowDownAmount;
            }

            else
            {
                deathShake = false;
                duration = initialDuration;
                camera.localPosition = startPosition;
            }
        }
    }
}
