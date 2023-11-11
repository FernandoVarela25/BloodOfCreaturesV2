using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    // Nombre de la escena en la que no se reproducir� la m�sica
    public string sceneToMute = "SampleScene";

    // Nombre de la escena en la que la m�sica no se reiniciar�
    public string sceneToPauseMusic = "Creditos";

    private AudioSource audioSource;

    // Nombre del archivo de m�sica en el Inspector
    public string musicFileName = "Night of the Streets";

    // Nombre del archivo de m�sica para la escena "SampleScene"
    public string musicFileNameSampleScene = "MusicaCapituloUno";

    private void Awake()
    {
        if (instance == null)
        {
            // Si no hay instancia, esta ser� la instancia actual
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            // Si ya hay una instancia, destruye este objeto
            Destroy(gameObject);
        }

        // Busca el AudioSource en el objeto actual o en sus hijos
        audioSource = GetComponentInChildren<AudioSource>();

        // Si no se encuentra, agrega un componente AudioSource al objeto actual
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Suscribirse al evento SceneManager.sceneLoaded despu�s de inicializar audioSource
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Carga la m�sica inicial desde la carpeta Resources (aseg�rate de crear esta carpeta)
        LoadMusic(musicFileName);
    }

    // M�todo invocado cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es la que se debe silenciar
        if (scene.name == sceneToMute)
        {
            // Si es la escena que se debe silenciar, detener la reproducci�n de la m�sica actual
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            // Cargar la m�sica espec�fica de "SampleScene"
            LoadMusic(musicFileNameSampleScene);
        }
        else if (scene.name != sceneToPauseMusic)
        {
            // Si no es la escena de pausa de m�sica, cargar la m�sica inicial
            LoadMusic(musicFileName);
        }
    }

    // M�todo para cargar y reproducir una nueva m�sica
    private void LoadMusic(string fileName)
    {
        AudioClip backgroundMusic = Resources.Load<AudioClip>(fileName);

        if (backgroundMusic != null)
        {
            // Asigna la m�sica al componente AudioSource
            audioSource.clip = backgroundMusic;

            // Reproduce la m�sica si no est� en pausa
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("No se pudo cargar la m�sica. Aseg�rate de que el nombre del archivo sea correcto y que est� en la carpeta Resources.");
        }
    }
}
