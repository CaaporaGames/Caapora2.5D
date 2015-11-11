using UnityEngine;
using System.Collections;

public class DragBehavior : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;


    public RectTransform rend;


    void Start()
    {

        rend = GetComponent<RectTransform>();
    }

    void OnMouseDown()
    {
        offset = GetComponent<RectTransform>().position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {

        Debug.Log("Entrou em Drag");

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        GetComponent<RectTransform>().position = curPosition;
    }
}
