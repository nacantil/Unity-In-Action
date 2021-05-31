using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{ 
    [SerializeField] private GameObject enemyPrefab;
    public int minEnemies = 4;
    public int maxEnemies = 8;


    // Start is called before the first frame update
    void Start()
    {
        int numOfEnemies = Random.Range(minEnemies, maxEnemies);
        for (int i = 0; i < numOfEnemies; i++)
        {
            spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // static for now ...
    public void spawn()
    {
        GameObject _enemy = Instantiate(enemyPrefab) as GameObject;
        //_enemy.transform.position = new Vector3(0, 1, 0); // I'll control this from Unity ...
        float angle = Random.Range(0, 360);
        _enemy.transform.Rotate(0, angle, 0);
    }
}
