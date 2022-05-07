using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndustryOption : MonoBehaviour
{
    public DataSet dataSet;
    public DataSetID dataSetID;

    public enum DataSetID { A, B }

    //Set option text.
    public void Start() => GetComponentInChildren<Text>().text = dataSet.Subject;

    public void SetData()
    {
        DotPlot dotPlot = FindObjectOfType<DotPlot>();

        switch (dataSetID)
        {
            case DataSetID.A:
                GetComponentInParent<IndustryMenu>().currentIndustryAText.text = dataSet.Subject;
                dotPlot.dataSetA = dataSet;
                break;
            case DataSetID.B:
                GetComponentInParent<IndustryMenu>().currentIndustryBText.text = dataSet.Subject;
                dotPlot.dataSetB = dataSet;
                break;
        }
        
        dotPlot.UpdateDots();
    }
}
