using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    public float speed;
    public float rotationSpeed;
    private Rigidbody rigid;
    public bool crashed = true;
    public bool work = true;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private float deathTime;
    public float deathDuration;

    public GameObject[] deathExplosion;

    public MainMenu mainMenu;
    public GameObject restartMenu;

    public Scorer scorer;
    //public Boost boost;

    public GameObject plasmaBoost;
    public List<GameObject> activeBoost;

    public int boostCnt = 0;
    private float collTime;

    private GameObject[] boostDel;



    // Use this for initialization
    void Start () {
        //rigid = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        activeBoost = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {

        // Move player after crash
        if (crashed)
        {
            transform.Translate ((Vector3.forward * speed) * Time.deltaTime);
            // After 2 sec resume normal collision
            if (Time.time > collTime + 2)
            {
                crashed = false;
                GetComponent<Collider>().enabled = true;
            }
        }
            // If the player is dead
            if (deathTime != 0)
        {
            // Wait X seconds, then restart the level
            if (Time.time - deathTime > deathDuration)
            {
                restartMenu.SetActive(true);
            }
            return;
        }

         if (controller.enabled)
        controller.Move ((Vector3.forward * speed) * Time.deltaTime);


        //rotate player
        float x = GetPlayerInput().x;
        float z = GetPlayerInput().z;
        
        // Keep rotating
        if (!mainMenu.paused)// & (x < -0.25 | x > 0.25))
            gameObject.transform.Rotate(Vector3.forward * x * rotationSpeed);

        //if (activeBoost.Count == 0)
        //    return;
        //else
        if (activeBoost.Count != 0 && transform.position.z > activeBoost[0].transform.position.z)
            DeleteBoost();

        // If boostCnt = 3 destroy all boost;
        if (boostCnt >= 3)
        {
            boostDel = GameObject.FindGameObjectsWithTag("boost");
            for (int i = 0; i < boostDel.Length; i++)
            {
                Destroy(boostDel[i]);
                activeBoost.Clear();
            }
            
        }
        
    }

    public static Vector3 GetPlayerInput()
    {

        Vector3 a = Input.acceleration;
        //a.x = a.z;
        return a;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "obs")//&& boostCnt == 0)
        {
            //if BOOST don't die!
            if (boostCnt == 0)
            {
                // Set a death timestamp
                deathTime = Time.time;

                // Player explosion effect
                GameObject go = Instantiate(deathExplosion[0]) as GameObject;
                go.transform.SetParent(transform);
                go.transform.position = transform.position + new Vector3(0, -3, 0);
                go.transform.rotation = Quaternion.Euler(180, 0, 0);

                GameObject go1 = Instantiate(deathExplosion[1]) as GameObject;
                go1.transform.SetParent(transform);
                go1.transform.position = transform.position + new Vector3(0, -3, 0);

                // Death camera
                transform.GetChild(0).gameObject.transform.position =
                    Vector3.Lerp(
                        transform.GetChild(0).gameObject.transform.position,
                        transform.GetChild(0).gameObject.transform.position + new Vector3(0, 0, -4), 1);
                mainMenu.paused = true;
                if (scorer.hsc > PlayerPrefs.GetFloat("HighScore", 0))
                {
                    PlayerPrefs.SetFloat("HighScore", scorer.hsc);
                    scorer.hs.text = PlayerPrefs.GetFloat("HighScore").ToString();
                }
                else
                    scorer.hs.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
            else
            {
                CameraShake.deathShake = true;
                GetComponent<Collider>().enabled = false;
                collTime = Time.time;
                crashed = true;
                boostCnt = 0;
                Debug.Log("Boost is " + boostCnt);
                Boost.boostVal = 1;
                Time.timeScale = Boost.boostVal;

            }
        }
    }

    

    public void RandomBoost()
    {
        if (boostCnt < 3 && activeBoost.Count < 3)
        {
            GameObject go;
            go = Instantiate(plasmaBoost) as GameObject;
            go.transform.position = new Vector3(0, 0, (transform.position.z + Random.Range(100, 300)));
            go.transform.rotation = transform.rotation;
            activeBoost.Add(go);
        }
    }

    public void DeleteBoost()
    {

        Destroy(activeBoost[0]);
        activeBoost.RemoveAt(0);

    }
}
