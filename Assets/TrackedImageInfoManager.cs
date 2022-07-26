using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    [SerializeField]
    Camera m_WorldSpaceCanvasCamera;
    [SerializeField]
    Texture2D m_DefaultTexture;
    ARTrackedImageManager m_TrackedIamgeManager;

    private void Awake()
    {
        m_TrackedIamgeManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        m_TrackedIamgeManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedIamgeManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void UpdateInfo(ARTrackedImage aRTrackedImage)
    {
        var canvas = aRTrackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = m_WorldSpaceCanvasCamera;

        var text = canvas.GetComponentInChildren<Text>();
        text.text = string.Format(
            "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3}\nDetected size: {4} cm",
            aRTrackedImage.referenceImage.name,
            aRTrackedImage.trackingState,
            aRTrackedImage.referenceImage.guid,
            aRTrackedImage.referenceImage.size * 100f,
            aRTrackedImage.size * 100f
            );
        var planeParentGo = aRTrackedImage.transform.GetChild(0).gameObject;
        var planeGo = planeParentGo.transform.GetChild(0).gameObject;
        if(aRTrackedImage.trackingState != TrackingState.None)
        {
            planeGo.SetActive(true);
            aRTrackedImage.transform.localScale = new Vector3(aRTrackedImage.size.x, 1f, aRTrackedImage.size.y);
            var material = planeGo.transform.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture =
                (aRTrackedImage.referenceImage.texture == null) ? m_DefaultTexture : aRTrackedImage.referenceImage.texture;

        }
        else
        {
            planeGo.SetActive(false);
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            UpdateInfo(trackedImage);

        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateInfo(trackedImage);
        }
    }
}
