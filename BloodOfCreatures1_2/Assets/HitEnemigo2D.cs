using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemigo"))  // Aseg�rate de que el enemigo tiene el tag correcto
        {
            print("Da�o al enemigo");
            // Puedes agregar aqu� cualquier l�gica adicional cuando la bala colisiona con el enemigo
        }
    }

    void Start()
    {
        // Cualquier inicializaci�n necesaria
    }

    void Update()
    {
        // Cualquier l�gica de actualizaci�n necesaria
    }
}
