//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using IsoTools;

namespace AssemblyCSharp
{
	public class moveCube : MonoBehaviour
	{
		// Use this for initialization
		void Start () {
			
		
			// gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;
			
			// posiçao inicial do caipora
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);
			
		}
		
		// Update is called once per frame
		void Update () {
			
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {
				
				if (Input.GetKey (KeyCode.LeftArrow)) {
					
					gameObject.GetComponent<IsoObject> ().position += new Vector3 (-0.1f, 0, 0);

				} 
				if (Input.GetKey (KeyCode.RightArrow)) {
					
					gameObject.GetComponent<IsoObject> ().position += new Vector3 (0.1f, 0, 0);
				
					
				} 
				if (Input.GetKey (KeyCode.DownArrow)) {
					
					gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -0.1f, 0);

					
				}
				if (Input.GetKey (KeyCode.UpArrow)) {
					
					gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0.1f, 0);
				
					
				}
				
			}
			
			
		}
	}
}

