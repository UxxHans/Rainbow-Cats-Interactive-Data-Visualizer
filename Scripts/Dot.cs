using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public Text valueUI;                //The UI of the value.
    private float dataValue;            //The value of the data.
    private Vector3 targetPosition;     //The target position to move.
    private static float LERP = 0.1f;   //Smoothness value for movement.

    public float DataValue { get => dataValue; set => dataValue = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    // Update is called once per frame.
    void Update()
    {
        //Set the text of the UI.
        valueUI.text = dataValue.ToString();

        //Move the dot smoothly to the target position.
        Vector3 positionDifference = targetPosition - transform.position;
        transform.position += (positionDifference / LERP) * Time.deltaTime;
    }
}
