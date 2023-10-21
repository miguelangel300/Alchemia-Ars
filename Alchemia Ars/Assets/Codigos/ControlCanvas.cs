using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    public Sprite[] notasDeExamen;
    public Sprite[] imagenesSonidos;
    public static Sprite[] notasDeExamenI;
    public static AnimarPapelera papelera;
    public static bool aletorizar = false;
    public static GameObject check;
    public AudioMixer audios;
    public AudioClip[] audiosVD;
    public static AudioClip[] audiosVDS;
    private static AudioSource audioSource;
    private float volumenMusica = 0;
    private float volumenSonidos = 0;
    UnityEngine.Color colorInicio;
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

        audioSource = pantallaVictoria.GetComponent<AudioSource>();
        audiosVDS = audiosVD;

        check = pantallaInterfazBotones.transform.GetChild(5).gameObject;

        notasDeExamenI = notasDeExamen;
        GameObject[] pos = GameObject.FindGameObjectsWithTag("SitioPociones");
        for (int i = 0; i < pos.Length; i++)
        {
            almacenes.Add(pos[i].GetComponent<AlmacenarPocion>());
        }
        papelera = GameObject.FindWithTag("Papelera").GetComponent<AnimarPapelera>();
        pociones = control.GetComponent<Control>().pocionFinal.pociones;
        RellenarLista();


        audios.SetFloat("VolumenSonidos", -80f);
        volumenSonidos = -80f;

    }
    public void RellenarLista()
    {
        pociones = control.GetComponent<Control>().pocionFinal.pociones;
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

                    texto.text += nombreString[l] + " ";
                }
            }
        }

    }
    public void Lista()
    {
        pantallaListaIngredientes.SetActive(!pantallaListaIngredientes.activeSelf);
        BotonIngredientes.SetActive(!BotonIngredientes.activeSelf);
        if (BotonIngredientes.activeSelf)
        {
            BotonIngredientes.GetComponent<AudioSource>().Play();

        }
        if (pantallaListaIngredientes.activeSelf)
        {
            pantallaListaIngredientes.GetComponent<AudioSource>().Play();

        }
    }
    public void Examinar()
    {
        InvokeRepeating("Aclarar", 0f, 0.0125f);
        BotonSonido.GetComponent<AudioSource>().Play();
        Sonido();


    }
    public void Aclarar()
    {
        UnityEngine.Color color = pantallaInicio.GetComponent<Image>().color;
        color = new Vector4(0, 0, 0, color.a - 0.01f);
        pantallaInicio.GetComponent<Image>().color = color;
        pantallaInicio.transform.GetChild(0).gameObject.GetComponent<Image>().color = color;
        Imagen.GetComponent<RawImage>().color = color;
        pantallaInterfaz.GetComponent<Image>().color = color;
        if (color.a <= 0f)
        {
            aletorizar = check.GetComponent<Toggle>().isOn;
            if (aletorizar)
            {
                control.GetComponent<Control>().Aletorizar();
            }
            else
            {
                control.GetComponent<Control>().Demo();

            }

            RellenarLista();
            check.SetActive(false);
            BotonSalir.SetActive(false);
            BotonIngredientes.SetActive(true);
            BotonCasa.SetActive(true);
            Sonidos(true);
            CancelInvoke();
            pantallaInicio.SetActive(false);
            Seleccionar.Inicio(true);
        }
    }
    public void Oscurecer()
    {
        pantallaInicio.SetActive(true);
        BotonCasa.SetActive(false);
        BotonIngredientes.SetActive(false);
        pantallaListaIngredientes.SetActive(false);
        check.SetActive(true);
        pantallaInicio.GetComponent<Image>().color = colorInicio;
        pantallaInicio.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        Imagen.GetComponent<RawImage>().color = colorInicio;
        pantallaInterfaz.GetComponent<Image>().color = colorInicio;
        Sonidos(false);
        Seleccionar.Inicio(false);

    }
    public void Casa()
    {
        BotonSonido.GetComponent<AudioSource>().Play();
        Oscurecer();
        LimpiarInventario();
        Seleccionar.Inicio(false);
        control.GetComponent<Control>().puntajeFinal = 100;
        BotonSalir.SetActive(true);
        pantallaVictoria.SetActive(false);
        Sonido();
    }
    public void Salir()
    {
        BotonSonido.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public static void PantallaVictoria()
    {
        LimpiarInventario();
        BotonIngredientes.SetActive(false);
        BotonCasa.SetActive(true);
        BotonSalir.SetActive(false);
        pantallaListaIngredientes.SetActive(false);
        Sonidos(false);
        pantallaVictoria.SetActive(true);
        puntos = control.GetComponent<Control>().puntajeFinal;
        if (puntos >= 100)
        {
            InsertarPuntos(notasDeExamenI[5]);
            audioSource.clip = audiosVDS[0];
            audioSource.Play();
        }
        else if (puntos >= 70)
        {
            InsertarPuntos(notasDeExamenI[4]);
            audioSource.clip = audiosVDS[0];
            audioSource.Play();
        }
        else if (puntos >= 50)
        {
            InsertarPuntos(notasDeExamenI[3]);
            audioSource.clip = audiosVDS[0];
            audioSource.Play();
        }
        else if (puntos >= 20)
        {
            InsertarPuntos(notasDeExamenI[2]);
            audioSource.clip = audiosVDS[1];
            audioSource.Play();
        }
        else if (puntos >= 10)
        {
            InsertarPuntos(notasDeExamenI[1]);
            audioSource.clip = audiosVDS[1];
            audioSource.Play();
        }
        else
        {
            InsertarPuntos(notasDeExamenI[0]);
            audioSource.clip = audiosVDS[1];
            audioSource.Play();
        }

    }

    private static void InsertarPuntos(Sprite sprite)
    {
        pantallaVictoria.SetActive(true);
        pantallaVictoria.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + puntos;
        pantallaVictoria.GetComponent<Image>().sprite = sprite;
    }
    public static void Sonidos(bool sonido)
    {
        if (sonido)
        {
            papelera.Maullar();

        }
        else
        {

            papelera.CancelarMaullar();
        }

    }

    public void Musica()
    {
        BotonSonido.GetComponent<AudioSource>().Play();
        if (volumenMusica == 0)
        {
            audios.SetFloat("VolumenMusica", -80f);
            volumenMusica = -80f;
        }
        else
        {
            audios.SetFloat("VolumenMusica", 0);
            volumenMusica = 0;

        }
    }
    public void Sonido()
    {
        BotonSonido.GetComponent<AudioSource>().Play();

        if (volumenSonidos == 0)
        {
            audios.SetFloat("VolumenSonidos", -80f);
            volumenSonidos = -80f;

            BotonSonido.GetComponent<Image>().sprite = imagenesSonidos[3];
            SpriteState estados = BotonSonido.GetComponent<Button>().spriteState;
            estados.highlightedSprite = imagenesSonidos[5];
            estados.pressedSprite = imagenesSonidos[4];
            BotonSonido.GetComponent<Button>().spriteState = estados;
        }
        else
        {
            audios.SetFloat("VolumenSonidos", 0);
            volumenSonidos = 0;

            BotonSonido.GetComponent<Image>().sprite = imagenesSonidos[0];
            SpriteState estados = BotonSonido.GetComponent<Button>().spriteState;
            estados.highlightedSprite = imagenesSonidos[2];
            estados.pressedSprite = imagenesSonidos[1];
            BotonSonido.GetComponent<Button>().spriteState = estados;

        }

    }

    public static void LimpiarInventario()
    {

        Caldero.EliminarInventario();
        BotonCasa.SetActive(false);
        for (int i = 0; i < almacenes.Count; i++)
        {
            almacenes[i].EliminarInventario();
        }
        GameObject[] objetoI = GameObject.FindGameObjectsWithTag("Ingrediente");
        GameObject[] objetoP = GameObject.FindGameObjectsWithTag("Pocion");
        GameObject objetoPF = GameObject.FindGameObjectWithTag("PocionFinal");
        Destroy(objetoPF);
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
