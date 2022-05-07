using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public TextMesh text;               //The UI of the pillar name.
    public GameObject popUpUI;          //The UI of the details.

    private Vector3 targetPosition;     //The target position to move.
    private static float LERP = 0.1f;   //Smoothness value for movement.

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    private void OnMouseEnter() => popUpUI.SetActive(true);

    private void OnMouseExit() => popUpUI.SetActive(false);

    public static string[] iconNames = new string[]
    {
        "Integration",
        "Balance",
        "Ethics",
        "Mentalreadiness",
        "Intuition",
        "Awareness",
        "Influence",
        "Adaptability",
        "Inspiration",
        "Communication",
        "Generosity",
        "Courage",
        "Imagination",
        "Drive",
        "Curiosity",
        "Attitude"
    };

    public void Set(int index)
    {
        //Set the text of the UI.
        text.text = iconNames[index];

        //Set the icon of the UI
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/Icon " + index);
    }

    // Update is called once per frame.
    void Update()
    {
        //Move the dot smoothly to the target position.
        Vector3 positionDifference = targetPosition - transform.position;
        transform.position += (positionDifference / LERP) * Time.deltaTime;
    }
}
