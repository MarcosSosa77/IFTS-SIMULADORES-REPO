using UnityEngine;

public class SFX : MonoBehaviour
{
   public void Cerrar()
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_CLOSE, 2.0f);
    }
}
