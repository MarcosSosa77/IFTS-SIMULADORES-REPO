using UnityEngine;
using UnityEngine.SceneManagement;


public class CambiarEscena : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ManagerJuego.instancia.HolaMundo();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
