using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{

    public GameObject howToPlayImage;

    int state = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state == 1)
        {
            SceneManager.LoadScene("BigLevelBrin");
        }
        if (Input.GetMouseButtonDown(0) && state == 0)
        {
            //SceneManager.LoadScene(sceneName: "HowToPlay");
            state++;
            howToPlayImage.GetComponent<Image>().enabled = true;
            showEntryGame();
        }


    }

    void showEntryGame()
    {

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color c = howToPlayImage.GetComponent<Image>().color;
        while (elapsedTime < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / 1f);
            howToPlayImage.GetComponent<Image>().color = c;
        }
    }
}
