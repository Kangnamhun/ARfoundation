using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionController : MonoBehaviour
{
    // 맴버변수추가
    [SerializeField]
    Text m_TogglePlaneDetectionText;

    ARPlaneManager m_ARPlaneManager;

    void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }
	public void TogglePlaneDetection()
	{
		m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;
		string planeDetectionMessage = "";
		if (m_ARPlaneManager.enabled)
		{
			planeDetectionMessage = "Disable Plane Detection and Hide Existing";
			SetAllPlanesActive(true);
		}
		else
		{
			planeDetectionMessage = "Enable Plane Detection and Show Existing";
			SetAllPlanesActive(false);
		}
		if (m_TogglePlaneDetectionText != null)
			m_TogglePlaneDetectionText.text = planeDetectionMessage;
	}

	void SetAllPlanesActive(bool value)
	{
		foreach (var plane in m_ARPlaneManager.trackables)
			plane.gameObject.SetActive(value);
	}
}
