using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Enum que define los tipos de objetos que se pueden pooler.
/// Agrega más tipos según lo necesites.
/// </summary>
public enum ObjectType
{
    None,   
    Enemigos, 
    Bala, 
    PowerUp, 
    efectoTierra,
    efectoFuego,
    efectoSangre
}


/// <summary>
/// Clase que define un objeto dentro del pool.
/// Cada objeto tiene una cantidad inicial, un prefab y la opción de expandirse.
/// </summary>
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool; // Prefab del objeto que será pooleado.
    public int amountToPool; // Cantidad inicial de objetos en el pool.
    public bool shouldExpand; // Si es true, el pool se expande si no hay más disponibles.
    public ObjectType type; // Tipo de objeto (usamos el enum).
}


/// <summary>
/// Administrador de Object Pooling.
/// Almacena y administra objetos desactivados para mejorar el rendimiento.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance; // Instancia única para acceder fácilmente.


    public List<ObjectPoolItem> itemsToPool; // Lista de objetos a pooler.
    private List<GameObject> pooledObjects; // Lista donde almacenamos los objetos creados.


    /// <summary>
    /// Se ejecuta antes que Start, asegurando que SharedInstance esté disponible desde el inicio.
    /// </summary>
    void Awake()
    {
        SharedInstance = this;
    }


    /// <summary>
    /// Inicializa el pool de objetos en el inicio del juego.
    /// </summary>
    void Start()
    {
        pooledObjects = new List<GameObject>(); // Inicializamos la lista.


        // Recorremos la lista de objetos que queremos pooler.
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool); // Instanciamos el objeto.
                obj.SetActive(false); // Lo desactivamos para que no aparezca en la escena de inmediato.
                pooledObjects.Add(obj); // Lo agregamos a la lista.
                obj.AddComponent<PooledObject>().Type = item.type; // Asignamos su tipo.
            }
        }
    }


    /// <summary>
    /// Obtiene un objeto del pool según el tipo especificado.
    /// Si no hay objetos disponibles y el objeto es expandable, se crea uno nuevo.
    /// </summary>
    /// <param name="type">Tipo de objeto a obtener.</param>
    /// <returns>GameObject disponible del pool o null si no hay disponible y no puede expandirse.</returns>
    public GameObject GetPooledObject(ObjectType type)
    {
        // Buscamos un objeto inactivo en el pool que coincida con el tipo solicitado.
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            PooledObject pooledObj = pooledObjects[i].GetComponent<PooledObject>();
            if (!pooledObjects[i].activeInHierarchy && pooledObj.Type == type)
            {
                return pooledObjects[i]; // Retornamos el objeto disponible.
            }
        }


        // Si no hay objetos disponibles, verificamos si el pool puede expandirse.
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type && item.shouldExpand)
            {
                GameObject obj = Instantiate(item.objectToPool); // Creamos un nuevo objeto.
                obj.SetActive(false); // Lo desactivamos antes de usarlo.
                pooledObjects.Add(obj); // Lo agregamos al pool.
                obj.AddComponent<PooledObject>().Type = type; // Asignamos su tipo.
                return obj; // Lo retornamos.
            }
        }


        return null; // No hay objetos disponibles y el pool no puede expandirse.
    }
}


/// <summary>
/// Componente auxiliar que almacena el tipo de cada objeto en el pool.
/// </summary>
public class PooledObject : MonoBehaviour
{
    public ObjectType Type; // Almacena el tipo del objeto.
}


