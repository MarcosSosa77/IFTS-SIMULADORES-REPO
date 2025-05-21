/*using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName ="NewObjSO", menuName ="Interactable/Object")]
public class ObjSO : ScriptableObject
{
    public string objectName;
    //public GameObject prefab;

    public List<Material> customMaterialsToEdit;//SI ESTA LISTA TIENE ALGUN MATERIAL 
                                                // EL SISTEMA ME VA A EDITAR ESE MATERIAL EXCLUSIVO
                                                //SI LA LISTA DE MATERIALES ESTA VACIA (ME VA A EDITAR TODOS LOS MATERIALES QUE ENCUENTRE)



    public bool isTextured = false; //determinar si el objeto utilza al menos una textura
    public Material texturedMaterial; // material que va a recibir la texture
    public List<Texture> availableTextures; //listado de texturas disponibles
   // public List<Quaternion> rotationVariants = new List<Quaternion>(); // NEW

    //public Quaternion customRot;
    // public Vector3 customScale = new Vector3(1,1,1);
    //public Vector3 customOffset;

}
*/


using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewObjSO", menuName = "Interactable/Object")]
public class ObjSO : ScriptableObject
{
    public string objectName;

    [Header("Material Settings")]
    public List<Material> customMaterialsToEdit; // Optional: specific materials to edit   

    [Header("Texture Settings")]
    public bool isTextured = false;
    public Material texturedMaterial; // Reference to identify target material
    public List<TexturedElement> texturedElements; // replaces available

    [Header("Material Settings")]
    public Vector2 defaultTiling = Vector2.one;
}



[System.Serializable]
public class TexturedElement
{
    public Texture mainTexture;
    public Texture normalMap;

    // Opcional.. agregar diferentes tipos de texturas (metallic, height,etc)
}