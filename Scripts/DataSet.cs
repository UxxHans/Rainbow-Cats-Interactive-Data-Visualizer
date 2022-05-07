using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataSet : ScriptableObject
{
    [SerializeField] private string subject;
    [SerializeField] private float[] data;

    public DataSet(string subject, float[] data)
    {
        this.subject = subject;
        this.data = data;
    }

    public string Subject { get => subject; set => subject = value; }
    public float[] Data { get => data; set => data = value; }
}
