using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[System.Serializable]
public struct ToggleMapping
{
    public GameObject button;
    public List<GameObject> hideables;
}

public class ToggleController : MonoBehaviour
{
    public Material expandMaterial;
    public Material collapseMaterial;
    public List<ToggleMapping> toggleableDisplayMapping;
    Dictionary<String,bool> collapsed = new Dictionary<String,bool>();

    private void Start()
    {
        // Set all toggle buttons to have the expand texture
        foreach (ToggleMapping key in toggleableDisplayMapping)
        {
            collapsed[key.button.name] = true;
            key.button.GetComponent<MeshRenderer>().material = expandMaterial;
            foreach (GameObject c in key.hideables)
            {
                c.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // Raycast from touch point on main camera to see if it touches a button
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string btnName = hit.transform.name;
                // Toggle the state of the touched button
                foreach (ToggleMapping key in toggleableDisplayMapping)
                {
                    if (key.button.name.Equals(btnName))
                    {
                        // Toggle state
                        if (collapsed[key.button.name])
                        {
                            // If collapsed, expand
                            Debug.Log("Expand");
                            collapsed[key.button.name] = false;
                            key.button.GetComponent<MeshRenderer>().material = collapseMaterial;
                            foreach (GameObject c in key.hideables)
                            {
                                c.SetActive(true);
                            }
                        }
                        else
                        {
                            // If expanded, collapse
                            Debug.Log("Collapse");
                            key.button.GetComponent<MeshRenderer>().material = expandMaterial;
                            collapsed[key.button.name] = true;
                            foreach (GameObject c in key.hideables)
                            {
                                c.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}