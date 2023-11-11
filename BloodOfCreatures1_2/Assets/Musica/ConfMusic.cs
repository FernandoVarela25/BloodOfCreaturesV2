using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    // Nombre de la escena en la que no se reproducirá la música
    public string sceneToMute = "SampleScene";

    // Nombre de la escena en la que la música no se reiniciará
    public string sceneToPauseMusic = "Creditos";

    private AudioSource audioSource;

    // Nombre del archivo de música en el Inspector
    public string musicFileName = "Night of the Streets";

    // Nombre del archivo de música para la escena "SampleScene"
    public string musicFileNameSampleScene = "MusicaCapituloUno";

    private void Awake()
    {
        if (instance == null)
        {
            // Si no hay instancia, esta será la instancia actual
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

        // Suscribirse al evento SceneManager.sceneLoaded después de inicializar audioSource
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Carga la música inicial desde la carpeta Resources (asegúrate de crear esta carpeta)
        LoadMusic(musicFileName);
    }

    // Método invocado cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es la que se debe silenciar
        if (scene.name == sceneToMute)
        {
            // Si es la escena que se debe silenciar, detener la reproducción de la música actual
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            // Cargar la música específica de "SampleScene"
            LoadMusic(musicFileNameSampleScene);
        }
        else if (scene.name != sceneToPauseMusic)
        {
            // Si no es la escena de pausa de música, cargar la música inicial
            LoadMusic(musicFileName);
        }
    }

    // Método para cargar y reproducir una nueva música
    private void LoadMusic(string fileName)
    {
        AudioClip backgroundMusic = Resources.Load<AudioClip>(fileName);

        if (backgroundMusic != null)
        {
            // Asigna la música al componente AudioSource
            audioSource.clip = backgroundMusic;

            // Reproduce la música si no está en pausa
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("No se pudo cargar la música. Asegúrate de que el nombre del archivo sea correcto y que esté en la carpeta Resources.");
        }
    }
}
