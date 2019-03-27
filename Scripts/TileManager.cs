using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;
    public Transform playerTransform;
    public float spawnZ = 0f;
    public float tileLenght = 26.5f;
    public int amnTilesOnScreen;
    public float safeZone = 15f;
    private List<GameObject> activeTiles;
    private int lastPrefabIndex = 0;
    public GameObject tunelRotation;
    public float speed;
    public MainMenu mainMenu;
    public GameObject pauseMenu;
    public PlayerMotor playerMotor;

    public float[] rotationTunel;

    // Use this for initialization
    void Start () {

        pauseMenu.SetActive(false);

        activeTiles = new List<GameObject>();
        for (int i=0; i< amnTilesOnScreen; i++)
        {
            if (i < 4)
                SpawnTile(0);
            else
            SpawnTile();
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if(playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLenght))
        {
            SpawnTile();
            DeleteTile();
            playerMotor.RandomBoost();
            //playerMotor.DeleteBoost();

        }

        //float x = PlayerMotor.GetPlayerInput().x;
        //float z = PlayerMotor.GetPlayerInput().z;

        /*if (!mainMenu.paused)
        {
            tunelRotation.transform.localRotation = Quaternion.Lerp(tunelRotation.transform.localRotation,
                Quaternion.Euler(0, 0, (x * speed) * -1), 0.07f);
        }
        */
        // Pause
        if (!mainMenu.paused)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Pause");
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                    mainMenu.paused = true;
                }

            }
        }

    }

    void SpawnTile (int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        go.transform.localRotation = Quaternion.Euler(0, 0, rotationTunel[RandomRotationIndex()]);
        spawnZ += tileLenght;
        activeTiles.Add(go);
        
    }

    void DeleteTile()
    {

        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;

        
    }

    //pick random element of rotation List
    private int RandomRotationIndex ()
    {
        int randomRotationIndex = Random.Range(0, rotationTunel.Length);
        return randomRotationIndex;
    }


}
