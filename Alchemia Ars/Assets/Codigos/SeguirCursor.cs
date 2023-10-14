using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SeguirCursor : MonoBehaviour
{
    float tiempo = 0;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -5);



        if (Input.GetMouseButtonDown(0) && tiempo > 0.125f)
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);

            Debug.DrawRay(ray.origin,ray.direction * 15,Color.yellow, 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15))
            {
                Invoke("Destruir", 0.25f);
            }
        }
        tiempo += Time.deltaTime;
    }
    private void Destruir()
    {
        Destroy(gameObject);

    }
}
