using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject objectPanelPrefab;
    public GameObject parent;
    public Material[] materials;
    public GameObject[] transparencyToggles;
    private bool panelActive = false;
    private GameObject[] objects;
    private GameObject[] viewToggles;
    private GameObject[] selectedToogles;
    private Transform objectPanelT;
    private List<GameObject> panelObjects;
    private List<int> selectedObjects;

    void Start()
    {
        panel.SetActive(panelActive);
        panelObjects = new List<GameObject>();
        selectedObjects = new List<int>();
    }

    void Update() 
    {
        CreatePanel();
        SelectedToogleOn();
        AllToggleViewOn();
        TransparencyToggleSwitch();
    }

    public void OpenClosePanel()
    {
        panelActive = !panelActive;
        panel.SetActive(panelActive);
    }

    private void CreatePanel()
    {
        objects = GameObject.FindGameObjectsWithTag("object");
        GameObject temporaryObject;

        while(panelObjects.Count != objects.Length)
        {
            temporaryObject = Instantiate(objectPanelPrefab, objectPanelPrefab.transform.position, Quaternion.identity) as GameObject;
            temporaryObject.transform.SetParent(parent.transform);
            temporaryObject.transform.localPosition = new Vector3(0, 125 - 78 * panelObjects.Count, 0);
            panelObjects.Add(temporaryObject);
        }
    }

    private void ViewToggleOn()
    {
        viewToggles = GameObject.FindGameObjectsWithTag("ViewToggle");

        for(int i = 0; i < viewToggles.Length; i++)
        {
            Material material;
            if(viewToggles[i].GetComponent<Toggle>().isOn)
            {
                material = materials[5];
            }
            else
            {
                material = materials[0];
            }
            objects[i].GetComponent<MeshRenderer>().material = material;
        }
    }

    private void SelectedToogleOn()
    {
        selectedToogles = GameObject.FindGameObjectsWithTag("panelObject");

        for(int i = 0; i < selectedToogles.Length; i++)
        {
            if(selectedToogles[i].GetComponent<Toggle>().isOn)
            {
                if(selectedObjects.Count == 0)
                {
                    selectedObjects.Add(i);
                }
                else
                {
                    bool IsPresent = false;
                    for(int j = 0; j < selectedObjects.Count; j++)
                    {
                        if(i == selectedObjects[j])
                        {
                            IsPresent = true;
                        }
                    }

                    if(!IsPresent)
                    {
                        selectedObjects.Add(i);
                    }
                }
            }
            else
            {
                for(int j = 0; j < selectedObjects.Count; j++)
                {
                    if(i == selectedObjects[j])
                    {
                        selectedObjects.RemoveAt(j);
                    }
                }
            }
        }
    }

    public void AllToggleOn()
    {
        GameObject toggle = GameObject.FindGameObjectWithTag("AllToggle");

        for(int i = 0; i < selectedToogles.Length; i++)
        {
            if(toggle.GetComponent<Toggle>().isOn)
            {
                selectedToogles[i].GetComponent<Toggle>().isOn = true;
            }
            else if(selectedObjects.Count == 0 || selectedObjects.Count == selectedToogles.Length)
            {
                selectedToogles[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }

    private void AllToggleViewOn()
    {
        GameObject toggle = GameObject.FindGameObjectWithTag("AllToggleView");

        for(int i = 0; i < selectedObjects.Count; i++)
        {
            if(toggle.GetComponent<Toggle>().isOn)
            {
                viewToggles[selectedObjects[i]].GetComponent<Toggle>().isOn = true;
            }
            else
            {
                viewToggles[selectedObjects[i]].GetComponent<Toggle>().isOn = false;
            }
        }
    }

    private void TransparencyToggleSwitch()
    {
        Material material = materials[5];
        if(transparencyToggles[0].GetComponent<Toggle>().isOn)
        {
            ViewToggleOn();
        }
        else
        {
            for(int i = 0; i < selectedObjects.Count; i++)
            {
                if(transparencyToggles[1].GetComponent<Toggle>().isOn)
                {
                    material = materials[4];
                }
                else if(transparencyToggles[2].GetComponent<Toggle>().isOn)
                {
                    material = materials[3];
                }
                else if(transparencyToggles[3].GetComponent<Toggle>().isOn)
                {
                    material = materials[2];
                }
                else if(transparencyToggles[4].GetComponent<Toggle>().isOn)
                {
                    material = materials[1];
                }

                objects[selectedObjects[i]].GetComponent<MeshRenderer>().material = material;
            }
        }

    }
}
