using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VelocidadItem : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de las posiciones.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colision� con el objeto de velocidad.
        if (other.CompareTag("Leslie"))
        {
            // Acciones de velocidad
            AplicarVelocidad(other.gameObject);

            // Desactiva el objeto para simular la recolecci�n.
            gameObject.SetActive(false);

            // Activa la transici�n en el Animator de las posiciones.
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
            // Modifica el m�todo AumentarVelocidad en LeslieMovement para incluir la duraci�n del efecto.
            jugadorScript.AumentarVelocidad(20f); // 20 segundos de velocidad
            Debug.Log("Velocidad aplicada al jugador");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no est� presente en el jugador.");
        }
    }
}
