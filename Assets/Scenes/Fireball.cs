using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    public bool isStuck = false;

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

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Player hit");
            player.Hurt(damage);
            Destroy(this.gameObject);
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
}
