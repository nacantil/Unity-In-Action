using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball; 
    public float speed = 4.0f;
    public int minFireballs = 5;
    public int maxFireballs = 15;
    private float obstacleRange = 5.0f;
    private bool _alive;
    private ArrayList stuckGameObjects = new ArrayList();
    private Animator _animator;
    private EnemySFX enemySFX;

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
        _animator = GetComponent<Animator>();
        enemySFX = GetComponent<EnemySFX >();
        SetTrigger("triggerWalk");
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            float forwardMotion = speed * Time.deltaTime;
            transform.Translate(0, 0, forwardMotion);

            // Maybe here update all the spit (maybe fireballs) ...
            foreach (GameObject gameObject in stuckGameObjects)
            {
                try
                {
                    gameObject.transform.rotation = transform.rotation;
                    gameObject.transform.localRotation = transform.localRotation;
                    gameObject.transform.Translate(0, 0, forwardMotion);
                }
                catch (Exception e)
                {
                    //Debug.Log(e);
                    stuckGameObjects.Remove(gameObject);
                }
            }

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
            bool isHit = false;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    SetTrigger("triggerSpit");
                    if (!isHit)
                    {
                        //enemySFX.playHitAudioSource();
                        enemySFX.play();
                        isHit = true;
                    }


                    int numOfFireballs = UnityEngine.Random.Range(minFireballs, maxFireballs);
                    for (int j = 0; j < numOfFireballs; j++)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position =
                            transform.TransformPoint(Vector3.forward * speed);
                        _fireball.transform.rotation = transform.rotation;

                        float scaleX = UnityEngine.Random.Range(0.1f, 0.5f);
                        float scaleY = UnityEngine.Random.Range(0.1f, 0.5f);
                        float scaleZ = UnityEngine.Random.Range(0.1f, 0.5f);
                        _fireball.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

                        float deltaX = UnityEngine.Random.Range(-4.0f, 4.0f);
                        float deltaY = UnityEngine.Random.Range(-4.0f, 4.0f);
                        float deltaZ = UnityEngine.Random.Range(-4.0f, 4.0f);
                        _fireball.transform.Translate(deltaX, deltaY, deltaZ);
                    }
                }
                // I don't want it to get stuck on Fireballs and spitballs ... for now ...
                else if (!hitObject.name.Equals("Fireball(Clone)") && !hitObject.name.Equals("MySpit(Clone)") && hit.distance < obstacleRange)
                {
                    float angle = UnityEngine.Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                    // Maybe here update all the spit (maybe fireballs) ...
                    foreach (GameObject gameObject in stuckGameObjects)
                    {
                        gameObject.transform.Rotate(0, angle, 0);
                    }
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public bool GetAlive()
    {
        return _alive;
    }

    public void addStuckGameObject(GameObject gameObject)
    {
        stuckGameObjects.Add(gameObject);
    }

    public int countOfStuckGameObjects()
    {
        return stuckGameObjects.Count;
    }

    public void removeAllStuckGameObjects()
    {
        foreach (GameObject gameObject in stuckGameObjects)
        {
            Destroy(gameObject);
        }
        stuckGameObjects.Clear();
    }

    public void SetTrigger(String action)
    {
        float value = UnityEngine.Random.Range(0.0f, 1.0f);
        String offset = "offsetWalk";
        if (action.Contains("Hit"))
        {
            offset = "offsetHit";
        }
        else if (action.Contains("Spit"))
        {
            offset = "offsetSpit";
        }
        GetComponent<Animator>().SetFloat(offset, value);
        GetComponent<Animator>().SetTrigger(action);
    }
}