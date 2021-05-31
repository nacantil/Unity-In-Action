using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    SceneController sceneController;
    private int _tooMany = 20;

    // Start is called before the first frame update
    void Start()
    {
        if (sceneController == null)
        {
            GameObject controller = GameObject.Find("Controller");
            if (controller != null)
            {
                sceneController = controller.GetComponent<SceneController>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit(GameObject gameObject)
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null && behavior.GetAlive())
        { 
            if (gameObject.name.Equals("MySpit(Clone)") || gameObject.name.Equals("Sphere"))
            {
                behavior.SetTrigger("triggerHit");
                behavior.addStuckGameObject(gameObject);
                Debug.Log("# of times hit: " + behavior.countOfStuckGameObjects());
            }
            if (behavior.countOfStuckGameObjects() > _tooMany)
            {
                Debug.Log(behavior + " is DEAD!!!!!");
                behavior.SetTrigger("triggerDead");
                behavior.SetAlive(false);
                StartCoroutine(Die(behavior));
            }

        }
    }

    private IEnumerator Die(WanderingAI behavior)
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        behavior.removeAllStuckGameObjects();
        //Destroy(this.gameObject);
        sceneController.spawn();
    }
}
