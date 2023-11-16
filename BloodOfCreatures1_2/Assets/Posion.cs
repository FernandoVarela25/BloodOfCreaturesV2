using System.Collections;
using UnityEngine;

public class Posion : MonoBehaviour
{
    [SerializeField] GameObject[] posionObjects;
    [SerializeField] float minSpawnX = -5f; // Valor mínimo en el eje X
    [SerializeField] float maxSpawnX = 5f;  // Valor máximo en el eje X
    [SerializeField] float fallSpeed = 5f;   // Velocidad de caída
    [SerializeField] float spawnInterval = 5f; // Intervalo de tiempo entre generaciones

    void Start()
    {
        StartCoroutine(PosionSpawn());
    }

    IEnumerator PosionSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minSpawnX, maxSpawnX);
            var position = new Vector3(wanted, transform.position.y);

            // Selecciona un objeto aleatorio de las posibles
            GameObject selectedObject = posionObjects[Random.Range(0, posionObjects.Length)];

            // Instancia una copia del objeto en la posición deseada
            GameObject instantiatedObject = Instantiate(selectedObject, position, Quaternion.identity);

            // Agrega fuerza hacia abajo para simular la gravedad
            Rigidbody2D rb2D = instantiatedObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
            }

            // Espera el tiempo especificado antes de la próxima generación
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
