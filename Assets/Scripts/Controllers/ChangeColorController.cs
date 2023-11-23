using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColorController : MonoBehaviour
{
    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor() {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            Ray rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayMouse, out RaycastHit hit)) {
                GameObject objetFinded = hit.collider.gameObject;
                if (hit.collider.gameObject.CompareTag(GameParametres.TagName.OBJECT)) 
                    objetFinded.GetComponent<MeshRenderer>().material.color = GetRandomColor();
            }
        }
    }

    private Color GetRandomColor() {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        return new Color(r, g, b);
    }
}
