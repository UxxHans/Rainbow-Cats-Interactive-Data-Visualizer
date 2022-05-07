using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotPlot : MonoBehaviour
{
    public GameObject dotAPrefab;
    public GameObject dotBPrefab;
    public GameObject linePrefab;
    public GameObject iconPrefab;

    [Range(0, 10)] public float heightSize = 1;
    [Range(0, 10)] public float widthSize = 1;

    public float minValue = 0;
    public float maxValue = 100;

    public DataSet dataSetA;
    public DataSet dataSetB;

    private bool centered;
    private GameObject[] dotAs;
    private GameObject[] dotBs;
    private GameObject[] icons;

    private static float iconDistance = 4;

    //Check if two data sets has the same amount of data to compare.
    public bool isDataLengthEqual(DataSet a, DataSet b)
    {
        if(a.Data.Length != b.Data.Length)
        {
            return false;
        }
        return true;
    }

    //Spawn dots with x distributed and y equals to 0;
    public void SpawnDots()
    {
        //Get a parent for the spawning game object.
        GameObject dotAsParent = new GameObject("DotAs");
        GameObject dotBsParent = new GameObject("DotBs");
        GameObject linesParent = new GameObject("Lines");
        GameObject IconsParent = new GameObject("Icons");

        //If the length is not equal, cant compare.
        if (!isDataLengthEqual(dataSetA, dataSetB)) return;

        dotAs = new GameObject[dataSetA.Data.Length];
        dotBs = new GameObject[dataSetB.Data.Length];
        icons = new GameObject[dataSetA.Data.Length];

        //Dot A Spawn.
        for(int a = 0; a < dotAs.Length; a++)
        {
            Vector3 position = transform.position + new Vector3(a * widthSize, 0, 0);
            GameObject instance = Instantiate(dotAPrefab, position, Quaternion.identity, dotAsParent.transform);
            dotAs[a] = instance;
        }

        //Dot B Spawn.
        for (int b = 0; b < dotBs.Length; b++)
        {
            Vector3 position = transform.position + new Vector3(b * widthSize, 0, 0);
            GameObject instance = Instantiate(dotBPrefab, position, Quaternion.identity, dotBsParent.transform);
            dotBs[b] = instance;
        }

        //Line Spawn.
        for(int c = 0; c < dotAs.Length; c++)
        {
            //Connect position with a line.
            GameObject instance = Instantiate(linePrefab, linesParent.transform);

            Transform[] transforms = new Transform[2];
            transforms[0] = dotAs[c].transform;
            transforms[1] = dotBs[c].transform;

            instance.GetComponent<Line>().Transforms = transforms;
        }

        //Icon Spawn
        for (int d = 0; d < dotAs.Length; d++)
        {
            Vector3 position = transform.position + new Vector3(d * widthSize, 0, 0);
            GameObject instance = Instantiate(iconPrefab, position, Quaternion.identity, IconsParent.transform);
            instance.GetComponent<Icon>().Set(d);
            icons[d] = instance;
        }

    }

    //Put columns at the center.
    public void ToggleCentered()
    {
        centered = !centered;
        Camera.main.GetComponent<CameraDirect>().StartLeadCamera(centered ? 1 : 0);
        UpdateDots();
    }

    //Update dot positions according to the data.
    public void UpdateDots()
    {
        if (!isDataLengthEqual(dataSetA, dataSetB)) return;

        //For each column
        for (int i = 0; i < dataSetA.Data.Length; i++)
        {
            //Set the current value. If the value is none, skip this chart.
            float currentValueA = dataSetA.Data[i];
            float currentValueB = dataSetB.Data[i];

            //Set height.
            float heightA = currentValueA - minValue;
            float heightB = currentValueB - minValue;
            float difference = (heightA - heightB) / 2;
            float centerOfData = (heightA + heightB) / 2;

            //Set position.
            Vector3 finalPositionA = transform.position + new Vector3(i * widthSize, heightA * heightSize, 0);
            Vector3 finalPositionB = transform.position + new Vector3(i * widthSize, heightB * heightSize, 0);
            Vector3 iconPosition = transform.position + new Vector3(i * widthSize, (centerOfData - Mathf.Abs(difference)) * heightSize - iconDistance, 0);


            if (centered)
            {
                float middleOfThePlot = (maxValue - minValue) / 2;
                
                finalPositionA = transform.position + new Vector3(i * widthSize, (middleOfThePlot + difference) * heightSize, 0);
                finalPositionB = transform.position + new Vector3(i * widthSize, (middleOfThePlot - difference) * heightSize, 0);
                iconPosition = transform.position + new Vector3(i * widthSize, (middleOfThePlot - Mathf.Abs(difference)) * heightSize - iconDistance, 0);

            }

            icons[i].GetComponent<Icon>().TargetPosition = iconPosition;

            //Set dot data and next positions.
            dotAs[i].GetComponent<Dot>().DataValue = currentValueA;
            dotAs[i].GetComponent<Dot>().TargetPosition = finalPositionA;

            dotBs[i].GetComponent<Dot>().DataValue = currentValueB;
            dotBs[i].GetComponent<Dot>().TargetPosition = finalPositionB;
        }
    }

    //Draw the XY axis to get boundary view.
    private void OnDrawGizmos()
    {
        Vector3 currentPosition = transform.position;

        Vector3 heightIndication = currentPosition + new Vector3(0, heightSize * (maxValue - minValue), 0);
        Vector3 widthIndication = currentPosition + new Vector3(widthSize * dataSetA.Data.Length, 0, 0);

        //Draw Y Axis.
        Gizmos.DrawLine(currentPosition, heightIndication);
        //Draw X Axis.
        Gizmos.DrawLine(currentPosition, widthIndication);
    }

    //Start and spawn.
    private void Start()
    {
        SpawnDots();
        UpdateDots();
    }
}
