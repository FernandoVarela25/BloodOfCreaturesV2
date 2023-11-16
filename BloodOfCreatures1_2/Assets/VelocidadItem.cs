using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VelocidadItem : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textoVelocidad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Leslie"))
        {
            AplicarVelocidad(other.gameObject);
            gameObject.SetActive(false);
            MostrarTextoVelocidad();

            if (animator != null)
            {
                animator.SetBool("RecogerVelocidad", true);
            }
        }
    }

    void AplicarVelocidad(GameObject jugador)
    {
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.AumentarVelocidad(20f); // 20 segundos de velocidad
            Debug.Log("Velocidad aplicada al jugador");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no está presente en el jugador.");
        }
    }

    void MostrarTextoVelocidad()
    {
        if (textoVelocidad != null)
        {
            textoVelocidad.text = "¡Velocidad aumentada!";
            Invoke("OcultarTextoVelocidad", 2f); // Cambia el 2f al tiempo que desees que aparezca el texto.
        }
    }

    void OcultarTextoVelocidad()
    {
        if (textoVelocidad != null)
        {
            textoVelocidad.text = "";
        }
    }
}
