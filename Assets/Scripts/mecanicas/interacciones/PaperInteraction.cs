using UnityEngine;
using TMPro;

public class PaperInteraction : MonoBehaviour
{
    public TextMeshPro text;
    public Transform playerCamera; // arrastra la cámara del jugador aquí

    void Start()
    { 
        if(text != null)
            text.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        // Hacer que el texto siempre mire a la cámara
        if(text != null && playerCamera != null)
        {
            text.transform.rotation = Quaternion.LookRotation(text.transform.position - playerCamera.position);
        }
    }

    public void ShowText(bool estado)
    {
        if (text != null)
            text.gameObject.SetActive(estado);
    }
}
