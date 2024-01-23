using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    // Método para cambiar a la escena de juego
    public void CambiarAEscenaPlay()
    {
        SceneManager.LoadScene("Game");
    }

    // Método para cambiar a la escena de controles
    public void CambiarAEscenaControls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void EscenaPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
    // Método para salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("salir");
        // Puedes agregar aquí cualquier lógica de cierre que necesites
        // Por ahora, simplemente saldremos de la aplicación en el editor de Unity
        Application.Quit();
    }
}
