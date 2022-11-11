/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */
/**
 * Sample for reading using polling by yourself, and writing too.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    public SerialController serialController;
    int currentMode = 1;
    AnalogRemap analogRemap = new AnalogRemap();
    //Camera
    public GameObject u_camera;
    //Stir Action
    public GameObject stirDevice; 
    public float rotationAngle;
    string recieveMessage="Turned";
    //Push Action
    public GameObject pushDevice;
    float blockLoc;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        Debug.Log(" ");
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.R) && currentMode == 1)
        {
            Debug.Log("Switching Mode : Grumbo");
            u_camera.transform.Rotate(0.0f, 180.0f, 0.0f);
            serialController.SendSerialMessage("B");
            currentMode = 2;
        } 
        else if (Input.GetKeyDown(KeyCode.R) && currentMode == 2)
        {
            Debug.Log("Switching Mode : Sumbo");
            u_camera.transform.Rotate(0.0f, 180.0f, 0.0f);
            serialController.SendSerialMessage("A");
            currentMode = 1;
        }
        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);

        //-------------
        if (message == recieveMessage && currentMode==1)
        {
            stirDevice.transform.Rotate(0.0f, rotationAngle, 0.0f);
        }
        if(currentMode==2)
        {
            blockLoc = ConvertString(message);
            blockLoc = RemapValues(blockLoc);
            UpdateBlock(blockLoc);
            Debug.Log(blockLoc);
        }
    }

    float ConvertString(string values)
    {
        return float.Parse(values);
    }
    float RemapValues(float value)
    {
        return analogRemap.Remap(1.0f,1023.0f,-5.8f,0.614f,value);
    }
    void UpdateBlock(float location)
    {
       // float newLocation;
       // newLocation = RemapValues(location);
        pushDevice.transform.position = new Vector3(-18.03f, -5.58f, location);
    }
}
