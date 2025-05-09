using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjetoInteractuable : MonoBehaviour
{
    public Transform spawnPoint;//punto donde aparecen los objetos

    [Header("SETEADO DE OBJETOS")]
    public ObjSO[] objetos; //lista de objetos scriptable

    private int currentIndex = 0;

    //MATERIALES
    [HideInInspector] public GameObject currentInstance;
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();


    public Vector3 offset;

    private ObjSO currentObjectSelected;

    void Start() 
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;

        SpawnCurrentObject();
    }



    public void CambiarObjeto(int direccion) 
    {
        if(objetos == null || objetos.Length == 0) return;

        currentIndex += direccion;
        if (currentIndex >= objetos.Length) currentIndex = 0;


        if (currentIndex < 0) currentIndex = objetos.Length -1;

        SpawnCurrentObject();

    }


    void SpawnCurrentObject()
    {
        if (spawnPoint == null) return;

        //Destruir instancia anterior
        if (currentInstance != null)
            Destroy(currentInstance);

        //INSTANCIAR UN NUEVO OBJETO
        ObjSO obj = objetos[currentIndex];
        currentInstance = Instantiate(obj.prefab, spawnPoint.position + offset, spawnPoint.rotation, spawnPoint);


        Renderer rend = currentInstance.GetComponent<Renderer>();

        if(rend != null) 
        {
            currentObjectSelected = objetos[currentIndex];
            //CLONAR LOS MATERIALES DEL OBJETO INSTANCIADO
            Material[] originalMats = rend.sharedMaterials;
            currentMaterials = new Material[originalMats.Length];

            for(int i=0; i< originalMats.Length; i++) 
            {
                if (originalMats[i] != null)
                {
                    currentMaterials[i] = new Material(originalMats[i]);//CLONANDO LOS MATERIALES ORIGINALES Y GUARDANDOLOS
                }
                else 
                {
                    currentMaterials[i] = null;//NO TIENE MATERIALES ORIGINALES
                }
            }


            //guardar referencias a los materiales originales

            rend.materials = currentMaterials;
            //DETERMINAR QUE MATERIALES QUIERO EXPONER
            editableMaterials.Clear();

            if (currentObjectSelected.customMaterialsToEdit != null && currentObjectSelected.customMaterialsToEdit.Count > 0)
            {
                foreach (Material matToEdit in currentObjectSelected.customMaterialsToEdit)
                {
                    foreach (Material clonado in currentMaterials)
                    {
                        if (clonado.name.StartsWith(matToEdit.name))
                        {
                            editableMaterials.Add(clonado);
                        }
                    }
                }
            }
            else 
            {
                editableMaterials.AddRange(currentMaterials);
            }

        }
    
    
    }

   

    public void Interactuar() 
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }
}
