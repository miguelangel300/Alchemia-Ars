using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlCanvas : MonoBehaviour
{
    private static int puntos = 0;
    public static GameObject pantallaVictoria;
    public static GameObject pantallaDerrota;
    public static GameObject pantallaInicio;
    public static GameObject pantallaInterfaz;
    public static GameObject pantallaInterfazBotones;
    public static GameObject Imagen;
    public static GameObject BotonCasa;
    public static GameObject BotonMusica;
    public static GameObject BotonSonido;
    public static GameObject BotonSalir;
    public static List<AlmacenarPocion> almacenes = new List<AlmacenarPocion>();
    static GameObject control;
    Color colorInicio;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Escenario");
        pantallaVictoria = transform.GetChild(2).gameObject;
        pantallaDerrota = transform.GetChild(3).gameObject;
        pantallaInicio = transform.GetChild(4).gameObject;
        pantallaInterfaz = transform.GetChild(0).gameObject;
        pantallaInterfazBotones = transform.GetChild(1).gameObject;
        Imagen = transform.GetChild(5).gameObject;

        BotonCasa = transform.GetChild(1).GetChild(0).gameObject;
        BotonMusica = transform.GetChild(1).GetChild(1).gameObject;
        BotonSonido = transform.GetChild(1).GetChild(2).gameObject;
        BotonSalir = transform.GetChild(1).GetChild(3).gameObject;
        colorInicio = pantallaInicio.GetComponent<Image>().color;

        GameObject[] pos = GameObject.FindGameObjectsWithTag("SitioPociones");
        for (int i = 0; i < pos.Length; i++)
        {
            almacenes.Add(pos[i].GetComponent<AlmacenarPocion>());
        }

    }
    public void Examinar()
    {
        InvokeRepeating("Aclarar", 0f, 0.0125f);
    }
    public void Aclarar()
    {
        Color color = pantallaInicio.GetComponent<Image>().color;
        color = new Vector4(0, 0, 0, color.a - 0.01f);
        pantallaInicio.GetComponent<Image>().color = color;
        Imagen.GetComponent<RawImage>().color = color;
        pantallaInterfaz.GetComponent<Image>().color = color;
        if (color.a <= 0f)
        {
            pantallaInicio.SetActive(false);
            BotonCasa.SetActive(true);
            CancelInvoke();
            Seleccionar.Inicio();
        }
    }
    public void Oscurecer()
    {
        pantallaInicio.SetActive(true);
        BotonCasa.SetActive(false);
        pantallaInicio.GetComponent<Image>().color = colorInicio;
        Imagen.GetComponent<RawImage>().color = colorInicio;
        pantallaInterfaz.GetComponent<Image>().color = colorInicio;
        Seleccionar.Inicio();

    }
    public void Casa()
    {
        Oscurecer();
        LimpiarInventario();
    }
    public void Salir()
    {
        Application.Quit();
    }
    public static void PantallaVictoria()
    {
        LimpiarInventario();

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
    public static void LimpiarInventario()
    {

        Seleccionar.Inicio();
        Caldero.EliminarInventario();
        BotonCasa.SetActive(false);
        for (int i = 0; i < almacenes.Count; i++)
        {
            almacenes[i].EliminarInventario();
        }
        GameObject[] objetoI = GameObject.FindGameObjectsWithTag("Ingrediente");
        GameObject[] objetoP = GameObject.FindGameObjectsWithTag("Pocion");
        for (int i = 0; i < objetoI.Length; i++)
        {
            Destroy(objetoI[i]);
        }
        for (int i = 0; objetoP.Length > 0; i++)
        {
            Destroy(objetoP[i]);
        }
    }
}
