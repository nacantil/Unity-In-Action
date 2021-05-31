using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpit : MonoBehaviour
{
    private float speed = 10.0f;
    private int damage = 1;
    private bool isStuck = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStuck)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        ReactiveTarget reactiveTarget = other.GetComponent<ReactiveTarget>();
        if (reactiveTarget != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //Debug.Log("Point of contact: " + hit.point);
            }
            gameObject.transform.position = hit.point;

            reactiveTarget.ReactToHit(this.gameObject);
            isStuck = true;
            //Destroy(this.gameObject); // Do I want to delay this?
            //StartCoroutine(Delay());
            return;
        }

        if (other.gameObject.name.Equals("Inner Wall")
            || other.gameObject.name.Equals("Outer Wall")
            || other.gameObject.name.Equals("Floor"))
        {
            isStuck = true;
        }
        //Destroy(this.gameObject);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject); // Do I want to delay this?
    }
}
