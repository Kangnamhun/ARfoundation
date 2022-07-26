using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SupportChecker : MonoBehaviour
{
    [SerializeField]
    ARSession m_Session;

    [SerializeField]
    Text m_LogText;

    [SerializeField]
    Button m_InstallButton;

    [SerializeField]
    GameObject panel;

    private void OnEnable()
    {
        StartCoroutine(CheckSupport());
    }

    void Log(string message)
    {
        m_LogText.text += $"{message}\n";
    }

    void SetInstallButtonActive(bool active)
    {
        if (m_InstallButton != null)
            m_InstallButton.gameObject.SetActive(active);
    }

    public void OnInstallButtonPressed()
    {
        StartCoroutine(Install());
    }

    IEnumerator Install()
    {
        SetInstallButtonActive(false);
        if(ARSession.state == ARSessionState.NeedsInstall)
        {
            Log("Attempting install..");
            yield return ARSession.Install();
            if(ARSession.state == ARSessionState.Ready)
            {
                Log("The sofrware update failed, or you edclined the update");
                SetInstallButtonActive(true);
            }
            else if (ARSession.state == ARSessionState.Ready)
            {
                Log("Success! Starting AR session...");
                m_Session.enabled = true;
                panel.SetActive(true);
            }
        }
        else
        {
            Log("Error: ARSession does not require install.");
        }
    }

    IEnumerator CheckSupport()
    {
        SetInstallButtonActive(false);
        Log("Checking for AR support...");
        yield return ARSession.CheckAvailability();

        if(ARSession.state == ARSessionState.NeedsInstall)
        {
            Log("Your device supports AR");
            Log("Requires a software update.");
            Log("Attempting install...");
            yield return ARSession.Install();
        }

        if (ARSession.state == ARSessionState.Ready)
        {
            Log("You device supports AR!");
            Log("Starting AR session...");

            m_Session.enabled = true;
            panel.SetActive(true);
        }
        else
        {
            switch(ARSession.state)
            {
                case ARSessionState.Unsupported:
                    Log("You device does not supprot AR.");
                    break;
                case ARSessionState.NeedsInstall:
                    Log("The software update failed, or you devlined the update.");
                    SetInstallButtonActive(true);
                    break;
            }
            Log("\n[Strat non-AR experience instead]");
        }
    }
}
