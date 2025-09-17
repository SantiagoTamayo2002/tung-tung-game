using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionDoor : MonoBehaviour
{

    // necesitamos una mascara para que el rycast no choque con el player
    public LayerMask mascara;
    public float distancia = 4.5f;
    public string texto_consola;
    void Start()
    {
        mascara = LayerMask.GetMask("detectar raycast");
        texto_consola = "nada";
        Debug.Log(texto_consola);
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
                texto_consola = "puerta detectada";
                Debug.Log(texto_consola);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(2);
                }
            }
            
        }else
            {
                texto_consola = "nada";
                Debug.Log(texto_consola);
            }
    }
}
