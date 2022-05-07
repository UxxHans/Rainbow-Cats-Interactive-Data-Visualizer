using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirect : MonoBehaviour
{
    [System.Serializable]
    public struct CameraLead
    {
        public Transform target;
        public float FOV;
    }

    public CameraLead[] cameraLeads;

    private int currentIndex = -1;
    private static float LEAD_TIME = 0.8f;
    private static float SMOOTHNESS = 10f;
    
    public void StartLeadCamera(int index)
    {
        CancelInvoke();
        LeadCamera(index);
        Invoke(nameof(StopCamera), LEAD_TIME);
    }

    public void LeadCamera(int index) => currentIndex = index;

    public void StopCamera() => currentIndex = -1;

    public void Update()
    {
        if (currentIndex < 0) return;
        Camera.main.transform.position += (cameraLeads[currentIndex].target.position - Camera.main.transform.position) * SMOOTHNESS * Time.deltaTime;
        Camera.main.fieldOfView += (cameraLeads[currentIndex].FOV - Camera.main.fieldOfView) * SMOOTHNESS * Time.deltaTime;
    }
}
