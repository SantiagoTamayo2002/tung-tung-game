using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionDoor : MonoBehaviour
{

    // necesitamos una mascara para que el rycast no choque con el player
    public LayerMask mascara;
    public float distancia = 4.5f;
    public GameObject textDoor;
    private bool puertaDetectada;

    void Start()
    {
        mascara = LayerMask.GetMask("detectar raycast");
        puertaDetectada = false;
        textDoor.SetActive(puertaDetectada);

    }

    /* 
        Raycast(origen, direccion, out hit, distancia, mascara)
        origen: desde donde sale el rayo (nosotros)
        direccion: hacia donde va el rayo (como los rayos del modo espectador del valorant)
        out hit: informacion del objeto con el que choca el rayo
        distancia: hasta donde llega el rayo
        mascara: que capas puede detectar el rayo (si no se envía este parámetro, detecta todas las capas)
    */
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit objetoImpactado, distancia, mascara))
        {
            if (objetoImpactado.collider.CompareTag("puerta_interactuable"))
            {
                puertaDetectada = true;
                textDoor.SetActive(puertaDetectada);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(2);
                }
            }
            
        }else
            {
                puertaDetectada = false;
                textDoor.SetActive(puertaDetectada);
            }
    }
}