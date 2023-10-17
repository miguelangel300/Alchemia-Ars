using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlCanvas : MonoBehaviour
{
    private int puntos = 0;
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;
    GameObject control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Escenario");

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PantallaVictoria()
    {
        puntos = control.GetComponent<Control>().puntajeFinal;
        if (puntos < 50)
        {
            pantallaDerrota.SetActive(true);
            pantallaDerrota.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Nota Final: " + puntos;
        }
        else
        {
            pantallaVictoria.SetActive(true);
            pantallaVictoria.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Nota Final: " + puntos;
        }

    } 
}
