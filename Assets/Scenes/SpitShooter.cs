using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitShooter : MonoBehaviour
{
    [SerializeField] private GameObject mySpitPrefab;
    private PlayerSFX playerSFX;
    public float minSpeed = 3.0f;
    public float maxSpeed = 9.0f;
    public int minSpitballs = 5;
    public int maxSpitballs = 25;
    // Start is called before the first frame update
    void Start()
    {
        playerSFX = GetComponent<PlayerSFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerSFX.playSneezeAudioSource();
            int numOfSpitDropplets = Random.Range(minSpitballs, maxSpitballs);
            for (int i = 0; i < numOfSpitDropplets; i++)
            {
                GameObject _mySpit = Instantiate(mySpitPrefab) as GameObject;
                float speed = Random.Range(minSpeed, maxSpeed);
                _mySpit.GetComponent<MySpit>().setSpeed(speed);
                _mySpit.transform.position =
                    transform.TransformPoint(Vector3.forward);
                _mySpit.transform.rotation = transform.rotation;

                float scaleX = Random.Range(0.1f, 0.4f);
                float scaleY = Random.Range(0.1f, 0.4f);
                float scaleZ = Random.Range(0.1f, 0.4f);
                _mySpit.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

                float deltaX = Random.Range(-2.0f, 2.0f);
                float deltaY = Random.Range(-2.0f, 2.0f);
                float deltaZ = Random.Range(-2.0f, 2.0f);
                _mySpit.transform.Translate(deltaX, deltaY, deltaZ);
            }
        }
    }
}
