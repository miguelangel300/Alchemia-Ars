using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SeguirCursor : MonoBehaviour
{
    float tiempo = 0;
    public bool seguir = true;
    // Update is called once per frame
    void Update()
    {

        if (seguir)
        {
            Seguir();
        }

        if (Input.GetMouseButtonDown(0) && tiempo > 0.125f && seguir)
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);

            Debug.DrawRay(ray.origin,ray.direction * 15,Color.yellow, 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15))
            {
                
                if (!hit.collider.transform.CompareTag("SitioPreparados"))
                {
                    Invoke("Destruir", 0.25f);
                }
                else if (hit.collider.transform.CompareTag("SitioPreparados"))
                {
                    transform.position = hit.collider.transform.position;
                    seguir = false;
                }
            }
        }
        tiempo += Time.deltaTime;
    }
    private void Seguir()
    {

        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -5);
    }
    private void Destruir()
    {
        Destroy(gameObject);

    }
    private void OnMouseDown()
    {
        if (!seguir)
        {
            seguir = true;
            tiempo = 0;
        }
    }
}
