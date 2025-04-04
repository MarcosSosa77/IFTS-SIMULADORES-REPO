using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject prefabBala;//OBJETO BALA
    public Transform spawnPoint;//LUGAR DONDE NACE LA BALA

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            DispararBala();
            SoundManager.instance.PlaySound(SoundType.DISPARO_1, 2f);
        }
    }


    void DispararBala() 
    {
        // Instantiate(prefabBala, spawnPoint.position, spawnPoint.rotation);

        GameObject objeto = ObjectPooler.SharedInstance.GetPooledObject(ObjectType.Bala);
        objeto.transform.position = spawnPoint.position;
        objeto.transform.rotation = spawnPoint.rotation;
        objeto.SetActive(true);

    }
}
