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
        // Acciones de velocidad aqu� (aumenta la velocidad del jugador).
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.AumentarVelocidad();
            Debug.Log("Velocidad aplicada al jugador");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no est� presente en el jugador.");
        }
    }
}
