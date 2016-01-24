using UnityEngine;
using System.Collections;
using IsoTools;

public class TesteFisica : MonoBehaviour {

    public static TesteFisica _instance;
    public IsoRigidbody iso_rigidyBody;
    public IsoObject iso_object;

    public void Awake()
    {


        if (_instance == null)
        {

            DontDestroyOnLoad(gameObject);
            

            _instance = this;

        }
        else
        {

            if (this != _instance)
                Destroy(gameObject);
        }


        iso_object = GetComponent<IsoObject>();
        


    }


    public static TesteFisica instance
    {
        get
        {
            if (_instance == null)
            {


                DontDestroyOnLoad(_instance);


                _instance = FindObjectOfType<TesteFisica>() as TesteFisica;
            }

            return _instance;
        }
    }

    void Start () {
	
	}
	

	void Update () {

        iso_rigidyBody = GetComponent<IsoRigidbody>();

        



        if (Input.GetKey(KeyCode.R))
        {

            iso_object.position += Vector3.one;

            Debug.Log("VAlor da gravidade = " + iso_rigidyBody.useGravity);

            Debug.Log("VAlor de isoWorld = " + iso_object.isoWorld._tileSize);

        }
    }
}
