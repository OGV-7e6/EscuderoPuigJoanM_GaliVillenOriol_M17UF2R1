using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    // M�todo para cambiar a la escena de juego
    public void CambiarAEscenaPlay()
    {
        SceneManager.LoadScene("Game");
    }

    // M�todo para cambiar a la escena de controles
    public void CambiarAEscenaControls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void EscenaPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
    // M�todo para salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("salir");
        // Puedes agregar aqu� cualquier l�gica de cierre que necesites
        // Por ahora, simplemente saldremos de la aplicaci�n en el editor de Unity
        Application.Quit();
    }
}
