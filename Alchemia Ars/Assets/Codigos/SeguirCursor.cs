using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SeguirCursor : MonoBehaviour
{
    float tiempo = 0;
    //variable que nos indica si el ingrediente esta siguiendo a la poción
    public bool seguir = true;

    // Update is called once per frame
    void Update()
    {
        //si lo esta siguiendo hacemos que el ingrediente se mueva
        if (seguir)
        {
            Seguir();
        }

        //si ha pasado un tiempo para que no se confunda el juego y el item nos sigue y hemos pulsado el botón izquierdo entramos
        if (Input.GetMouseButtonDown(0) && tiempo > 0.125f && seguir)
        {
            //creamos un rayo para ver que se esta pulsando
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit;

            // si detecta algo entramos
            if (Physics.Raycast(ray, out hit, 15))
            {
                ////si no esta pulsando el sitio de preparados destruye el objeto
                if (!hit.collider.transform.CompareTag("SitioPreparados") && !hit.collider.transform.CompareTag("SitioPociones") && tag == "Ingrediente")
                {
                    Invoke("Destruir", 0.25f);
                }
                //si es el sitio de preparados lo dejamos en ese sitio y le decimos que no nos siga
                if (hit.collider.transform.CompareTag("SitioPreparados"))
                {
                    transform.position = hit.collider.transform.position;
                    seguir = false;
                }
                //si es la papelera lo destruimos
                if (hit.collider.transform.CompareTag("Papelera") || hit.collider.transform.CompareTag("SitioIngredientes"))
                {
                    Invoke("Destruir", 0.25f);
                }
            }
        }
        tiempo += Time.deltaTime;
    }
    private void Seguir()
    {
        //calculamos la posición del cursor mediante la camara
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -5);
    }
    private void Destruir()
    {
        Destroy(gameObject);

    }
    public void onSeguir()
    {
        seguir = !seguir;
    }
    private void OnMouseDown()
    {
        //si al pulsar el objeto no nos esta siguiendo
        if (!seguir)
        {
            //le decimos que nos sigua y ponemos el tiempo a cero para que no se lie unity
            seguir = true;
            tiempo = 0;
        }
    }
}
