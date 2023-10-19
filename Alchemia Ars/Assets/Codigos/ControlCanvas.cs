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
    public static GameObject pantallaListaIngredientes;
    public static GameObject Imagen;
    public static GameObject BotonCasa;
    public static GameObject BotonMusica;
    public static GameObject BotonSonido;
    public static GameObject BotonSalir;
    public static GameObject BotonIngredientes;
    public static List<AlmacenarPocion> almacenes = new List<AlmacenarPocion>();
    static GameObject control;
    Color colorInicio;
    private List<Pocion> pociones = new List<Pocion>();
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Escenario");
        pantallaVictoria = transform.GetChild(0).gameObject;
        pantallaDerrota = transform.GetChild(1).gameObject;
        pantallaInicio = transform.GetChild(2).gameObject;
        pantallaInterfaz = transform.GetChild(5).gameObject;
        pantallaInterfazBotones = transform.GetChild(6).gameObject;
        pantallaListaIngredientes = transform.GetChild(4).gameObject;
        Imagen = transform.GetChild(3).gameObject;

        BotonCasa = pantallaInterfazBotones.transform.GetChild(0).gameObject;
        BotonMusica = pantallaInterfazBotones.transform.GetChild(1).gameObject;
        BotonSonido = pantallaInterfazBotones.transform.GetChild(2).gameObject;
        BotonSalir = pantallaInterfazBotones.transform.GetChild(3).gameObject;
        BotonIngredientes = pantallaInterfazBotones.transform.GetChild(4).gameObject;
        colorInicio = pantallaInicio.GetComponent<Image>().color;


        GameObject[] pos = GameObject.FindGameObjectsWithTag("SitioPociones");
        for (int i = 0; i < pos.Length; i++)
        {
            almacenes.Add(pos[i].GetComponent<AlmacenarPocion>());
        }

        pociones = control.GetComponent<Control>().pocionFinal.pociones;
        RellenarLista();

    }
    public void RellenarLista()
    {
        TextMeshProUGUI texto = pantallaListaIngredientes.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        texto.text = "La pocion final ha realizar se llama " +
            control.GetComponent<Control>().pocionFinal.nombre + ", y Las pociones a mezclar para craftearla son:";
        for (int i = 0; i < pociones.Count; i++)
        {
            texto.text += "\nPocion De " + pociones[i].nombre;
            for (int j = 0; j < pociones[i].ingredientes.Count; j++)
            {
                string[] nombreString = pociones[i].ingredientes[j].nombre.ToString().Split("_");
                texto.text += "\n\t";
                for (int l = 0; l < nombreString.Length; l++)
                {

                    texto.text += nombreString[l]+" ";
                }
            }
        }

    }
    public void Lista()
    {
        pantallaListaIngredientes.SetActive(!pantallaListaIngredientes.activeSelf);
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
            BotonIngredientes.SetActive(true);
            BotonCasa.SetActive(true);
            CancelInvoke();
            Seleccionar.Inicio();
        }
    }
    public void Oscurecer()
    {
        pantallaInicio.SetActive(true);
        BotonCasa.SetActive(false);
        BotonIngredientes.SetActive(false);
        pantallaListaIngredientes.SetActive(false);
        pantallaInicio.GetComponent<Image>().color = colorInicio;
        Imagen.GetComponent<RawImage>().color = colorInicio;
        pantallaInterfaz.GetComponent<Image>().color = colorInicio;
        Seleccionar.Inicio();

    }
    public void Casa()
    {
        Oscurecer();
        LimpiarInventario();
        control.GetComponent<Control>().puntajeFinal = 100;
    }
    public void Salir()
    {
        Application.Quit();
    }
    public static void PantallaVictoria()
    {
        LimpiarInventario();
        BotonIngredientes.SetActive(false);
        pantallaListaIngredientes.SetActive(false);
        puntos = control.GetComponent<Control>().puntajeFinal;
        if (puntos < 50)
        {
            pantallaDerrota.SetActive(true);
            pantallaDerrota.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Nota Final: " + puntos;
        }
        else
        {
            pantallaVictoria.SetActive(true);
            pantallaVictoria.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Nota Final: " + puntos;
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
