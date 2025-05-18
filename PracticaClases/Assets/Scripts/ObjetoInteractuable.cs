using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjetoInteractuable : MonoBehaviour
{
    public Transform spawnPoint;//punto donde aparecen los objetos

    [Header("SETEADO DE OBJETOS")]
    public ObjSO[] objetos; //lista de objetos scriptable

    public int currentIndex = 0;

    //MATERIALES
    [HideInInspector] public GameObject currentInstance;
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();


    public Vector3 offset;

    private ObjSO currentObjectSelected;

    private int currentTextureIndex = 0;

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
        // currentInstance = Instantiate(obj.prefab, spawnPoint.position + offset, obj.customRot , spawnPoint);

        currentInstance = Instantiate(obj.prefab, spawnPoint.position + offset, obj.customRot);
        currentInstance.gameObject.transform.localScale = obj.customScale;

        // currentInstance.transform.localScale = Vector3.one; // Reset local scale
        currentInstance.transform.SetParent(spawnPoint, true); // Keep world position

        Renderer rend = currentInstance.GetComponent<Renderer>();

        if(rend != null) 
        {
            currentObjectSelected = obj;
            //CLONAR LOS MATERIALES DEL OBJETO INSTANCIADO
            Material[] originalMats = rend.sharedMaterials;
            currentMaterials = new Material[originalMats.Length];

            for(int i=0; i< originalMats.Length; i++) 
            {
                currentMaterials[i] = originalMats[i] != null ? new Material(originalMats[i]) : null;
               
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


            //SISTEMA DE TEXTURAS
            if(currentObjectSelected.isTextured  && currentObjectSelected.texturedMaterial != null && currentObjectSelected.availableTextures.Count > 0) 
            {
                foreach(Material mat in currentMaterials) 
                {
                    if(mat != null && mat.name.StartsWith(currentObjectSelected.texturedMaterial.name)) 
                    {
                        mat.mainTexture = currentObjectSelected.availableTextures[0];
                        break;
                    }
                }
            }

        }
    
    
    }

   

    public void Interactuar() 
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }

    public void SetTexture(int textureIndex)
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            textureIndex < 0 ||
            textureIndex >= currentObjectSelected.availableTextures.Count) 
        {
            return;
        }

        currentTextureIndex = textureIndex;
        ApplyTextureToTexturedMaterial();
     }

    public void NextTexture() 
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0)
            return;

        currentTextureIndex++;
        if(currentTextureIndex >= currentObjectSelected.availableTextures.Count) 
        {
            currentTextureIndex = 0;
        }

        ApplyTextureToTexturedMaterial();
    }

    public void PreviousTexture()
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0)
            return;

        currentTextureIndex--;
        if (currentTextureIndex < 0)
        {
            currentTextureIndex = currentObjectSelected.availableTextures.Count - 1;
        }

        ApplyTextureToTexturedMaterial();
    }

    void ApplyTextureToTexturedMaterial() 
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0 ||
            currentObjectSelected.texturedMaterial == null)
        {
            return;
        }

        foreach(Material mat in currentMaterials) 
        {
            if(mat != null && mat.name.StartsWith(currentObjectSelected.texturedMaterial.name)) 
            {
                mat.mainTexture = currentObjectSelected.availableTextures[currentTextureIndex];
                break;
            }
        }

    }


}
