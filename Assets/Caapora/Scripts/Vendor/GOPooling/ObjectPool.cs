using System;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;

/// <summary>
/// Repository of commonly used prefabs.
/// </summary>
[AddComponentMenu("Gameplay/ObjectPool")]
public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance { get; private set; }

    #region member
    /// <summary>
    /// Member class for a prefab entered into the object pool
    /// </summary>
    [Serializable]
    public class ObjectPoolEntry
    {
        /// <summary>
        /// the object to pre instantiate
        /// </summary>
        [SerializeField]
        public GameObject Prefab;
        

        /// <summary>
        /// quantity of object to pre-instantiate
        /// </summary>
        [SerializeField]
        public int Count;


    }
    #endregion


    public ObjectPoolEntry[] Entries;


    [HideInInspector]
    public List<GameObject>[] Pool;

   
    private GameObject ContainerObject;

    void OnEnable()
    {
        instance = this;
        ContainerObject = new GameObject("ObjectPool");

      
        Pool = new List<GameObject>[Entries.Length];


        for (int i = 0; i < Entries.Length; i++)
        {
            var objectPrefab = Entries[i];

            Pool[i] = new List<GameObject>();


            for (int n = 0; n < objectPrefab.Count; n++)
            {
               
                var newObj = Instantiate(objectPrefab.Prefab) as GameObject;

                newObj.name = objectPrefab.Prefab.name;

                PoolObject(newObj);
            } 
        }
    }


    public void PoolObject(GameObject obj)
    {

        for (int i = 0; i < Entries.Length; i++)
        {

            if (Entries[i].Prefab.name != obj.name)
                continue;

            SetActiveRecursively(obj, false);


            obj.transform.parent = ContainerObject.transform;

            Pool[i].Add(obj);

            return;
        }
    }


    // Use this for initialization
    void Start()
    {
        
    }



    /// <summary>
    /// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
    /// then null will be returned.
    /// </summary>
    /// <returns>
    /// The object for type.
    /// </returns>
    /// <param name='objectType'>
    /// Object type.
    /// </param>
    /// <param name='onlyPooled'>
    /// If true, it will only return an object if there is one currently pooled.
    /// </param>
    public GameObject GetObjectForType(string objectType, bool onlyPooled)
    {

        for (int i = 0; i < Entries.Length; i++)
        {

         
            var prefab = Entries[i].Prefab;

            if (prefab.name != objectType)
                continue;

            if (Pool[i].Count > 0)
            {

               // Pega o ultimo elemento da estrutura
                GameObject pooledObject = Pool[i][0];
                // Remove o último elemento da estrutura
                Pool[i].RemoveAt(0);

                pooledObject.transform.parent = null;

                SetActiveRecursively(pooledObject, true);
                //pooledObject(true);

                return pooledObject;
            }

            if (!onlyPooled)
            {
               
                GameObject newObj = Instantiate(Entries[i].Prefab) as GameObject;
                newObj.name = Entries[i].Prefab.name;
                return newObj;
            }
        }

       
        return null;
    }



    public static void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);

        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }





}