using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    public PlayerMotor playerMotor;
    public static float boostVal = 1;

    // Use this for initialization
    void Start () {
        playerMotor = GameObject.Find("Player").GetComponent<PlayerMotor>();
	}

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        //boost!
        playerMotor.boostCnt += 1;
        if (playerMotor.boostCnt < 3)
        {
            CameraShake.boostShake = true;
            boostVal = Time.timeScale + 1;
            Time.timeScale = boostVal;
            Debug.Log("Boost " + playerMotor.boostCnt);
        }
        if (playerMotor.boostCnt >= 3)
        {
            playerMotor.boostCnt = 3;
            Debug.Log("Boost is MAX!");
        }
    }

}
