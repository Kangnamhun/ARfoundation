using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public void ChangeScene(string _name)
    {
        //SceneManager.LoadScene("ARSupportCheck", LoadSceneMode.Single
        SceneManager.LoadScene(_name, LoadSceneMode.Single);
    }
}
