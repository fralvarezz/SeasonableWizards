using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("BigLevelBrin");
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("BigLevelBrin");
    }
}
