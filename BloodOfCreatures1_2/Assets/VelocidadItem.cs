using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VelocidadItem : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de las posiciones.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisionó con el objeto de velocidad.
        if (other.CompareTag("Leslie"))
        {
            // Acciones de velocidad
            AplicarVelocidad(other.gameObject);

            // Desactiva el objeto para simular la recolección.
            gameObject.SetActive(false);

            // Activa la transición en el Animator de las posiciones.
            if (animator != null)
            {
                animator.SetBool("RecogerVelocidad", true);
            }
        }
    }

    void AplicarVelocidad(GameObject jugador)
    {
        // Acciones de velocidad aquí (aumenta la velocidad del jugador).
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.AumentarVelocidad();
            Debug.Log("Velocidad aplicada al jugador");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no está presente en el jugador.");
        }
    }
}
