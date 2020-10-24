using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.XR;

public class HandPressence : MonoBehaviour
{
    private GameObject spawnedHandModel;
    private InputDevice targetDevice;
    private GameObject spawnedController;

    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public bool showController = false;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        
        foreach (var item in devices)
        {
            UnityEngine.Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
            //Sets the prefab for the controller the prefab that has the same name as the controller (deveice name must match the game object 3d model)
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            //Incase controller is not found
            else
            {
                UnityEngine.Debug.LogError("Did not find controller");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(showController)
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
        }
    }
}
