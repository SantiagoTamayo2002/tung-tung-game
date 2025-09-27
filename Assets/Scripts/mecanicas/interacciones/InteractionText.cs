using UnityEngine;

public class InteractionText : MonoBehaviour
{
    ///////////////////// VARIABLES /////////////////////////
    [Header("Raycast Settings")]
    public float distancia = 3f;
    public LayerMask mascara;
    public Camera camaraJugador;
    private PaperInteraction ultimoImpactado = null;
    //////////////////////////////////////////////////////7


    void Update()
    {
        if (Physics.Raycast(camaraJugador.transform.position, camaraJugador.transform.forward, out RaycastHit objetoImpactado, distancia, mascara))
        {
            if (objetoImpactado.collider.CompareTag("interact"))
            {
                ultimoImpactado = objetoImpactado.collider.GetComponent<PaperInteraction>();
                ultimoImpactado.ShowText(true);
            }
        }
        else
        {
            if (ultimoImpactado != null)
            {
                ultimoImpactado.ShowText(false);
                ultimoImpactado = null;
            }
        }
    }
}
