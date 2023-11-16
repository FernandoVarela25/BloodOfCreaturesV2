using UnityEngine;
using TMPro;

public class CuracionItem : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textoCuracion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Leslie"))
        {
            CurarJugador(other.gameObject);
            gameObject.SetActive(false);
            MostrarTextoCuracion();

            if (animator != null)
            {
                animator.SetBool("RecogerCuracion", true);
            }
        }
    }

    void CurarJugador(GameObject jugador)
    {
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.CurarVidaAl100();
            Debug.Log("Jugador curado al 100%");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no está presente en el jugador.");
        }
    }

    void MostrarTextoCuracion()
    {
        if (textoCuracion != null)
        {
            textoCuracion.text = "Curado al 100%";
            Invoke("OcultarTextoCuracion", 3f); // Cambia el 2f al tiempo que desees que aparezca el texto.
        }
    }

    void OcultarTextoCuracion()
    {
        if (textoCuracion != null)
        {
            textoCuracion.text = "";
        }
    }
}
