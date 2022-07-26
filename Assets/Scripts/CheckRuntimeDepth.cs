using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CheckRuntimeDepth : MonoBehaviour
{
    [SerializeField]
    AROcclusionManager m_OcculusionManager;

    [SerializeField]
    Text m_DepthAvailabilityInfo;
    void Update()
    {
        Debug.Assert(m_OcculusionManager != null, "no occlusion manager");
        Debug.Assert(m_DepthAvailabilityInfo != null, "no text box");
        m_DepthAvailabilityInfo.enabled =
            ((m_OcculusionManager.descriptor?.supportsHumanSegmentationStencilImage == false)
            && (m_OcculusionManager.descriptor?.supportsHumanSegmentationDepthImage == false)
            && (m_OcculusionManager.descriptor?.supportsEnvironmentDepthImage == false));
    }
}
