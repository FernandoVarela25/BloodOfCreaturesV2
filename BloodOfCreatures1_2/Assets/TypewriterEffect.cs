using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public float buttonDelay = 0.5f; // Nuevo: retraso para la aparición del botón.
    public string fullText;
    private string currentText = "";

    public Button myButton;

    void Start()
    {
        myButton.gameObject.SetActive(false); // Asegúrate de que el botón esté desactivado al inicio.
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        // Espera antes de activar el botón.
        yield return new WaitForSeconds(buttonDelay);

        // Al finalizar la escritura, activa el botón.
        myButton.gameObject.SetActive(true);
    }
}
