﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private PlayerSFX playerSFX;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerSFX = GetComponent<PlayerSFX>();
    }

    public void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerSFX.playJemiAudioSource();
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //Get the Renderer component from the new cube
                    var sphereRenderer = sphere.GetComponent<Renderer>();
                    //Call SetColor using the shader property name "_Color" and setting the color to red
                    sphereRenderer.material.SetColor("_Color", Color.red);
                    sphere.transform.position = hit.point;
                    //Debug.Log("Target hit");
                    //target.ReactToHit(hitObject);
                    target.ReactToHit(sphere);
;                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator (Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //Get the Renderer component from the new cube
        var sphereRenderer = sphere.GetComponent<Renderer>();

        //Call SetColor using the shader property name "_Color" and setting the color to red
        sphereRenderer.material.SetColor("_Color", Color.red);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        //Destroy(sphere);
    }

}