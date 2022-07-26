using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void ChangeScene(string _name)
    {
        SceneManager.LoadScene(_name, LoadSceneMode.Single);
    }

}
