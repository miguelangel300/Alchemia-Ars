using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacionTrabajo : MonoBehaviour
{
    public Transform posicionFinal;
    public Estacion estacion;
    GameObject ingrediente;
    public List<GameObject> imagenes;

    private void OnMouseDown()
    {
        ingrediente = GameObject.FindWithTag("Ingrediente");
        if (ingrediente.transform.position != posicionFinal.position)
        {
            Ingrediente ingredienteAux = ingrediente.GetComponent<Ingrediente>();
            SpriteRenderer imagen = ingrediente.GetComponent<SpriteRenderer>();
            if (ingredienteAux.proceso == Ingrediente.Proceso.Ninguno)
            {
                switch (estacion)
                {
                    case Estacion.Destileria:
                        ingredienteAux.proceso = Ingrediente.Proceso.Destilado;
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                    case Estacion.Cuchillo:
                        ingredienteAux.proceso = Ingrediente.Proceso.Cortado;
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                    case Estacion.Mortero:
                        ingredienteAux.proceso = Ingrediente.Proceso.Machacado;
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                }
            }
            ingrediente.GetComponent<SeguirCursor>().seguir = false;
            ingrediente.transform.position = posicionFinal.position;
            Instantiate(ingrediente);
            Destroy(ingrediente);
        }
    }
    private Sprite BuscarImagen(Ingrediente.IngredienteN nombre, Ingrediente.Proceso proceso)
    {
        for (int i = 0; i < imagenes.Count; i++)
        {
            if (imagenes[i].GetComponent<IngredienteProcesado>().nombre == nombre && imagenes[i].GetComponent<IngredienteProcesado>().proceso == proceso)
            {
                return imagenes[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
        return null;
    }
    public enum Estacion { Destileria, Cuchillo, Mortero }
}
