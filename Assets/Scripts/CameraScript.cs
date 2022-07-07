using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Transform cameraObj;
    public float Radius = 4.5f;

    public GameObject screenFade;
    public GameObject hearts;
    public GameObject seasonClock;

    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //finishedGame();
    }

    // Update is called once per frame
    void Update()
    {   
        SmoothFollow();
    }

    private void FixedUpdate()
    {
     
    }

    private void SmoothFollow()
    {
        float maxScreenPoint = 0.2f;
        Vector3 mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxScreenPoint) * 0.5f);
        //Vector3 position = (target.position + GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)) / 2f;
        Vector3 position = (target.position + GetComponent<Camera>().ScreenToWorldPoint(mousePos)) / 2f;
        Vector3 destination = new Vector3(position.x, position.y, -10);
        cameraObj.transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.1f);
    }

    public void finishedGame()
    {

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color c = screenFade.GetComponent<Image>().color;
        while (elapsedTime < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / 1f);
            screenFade.GetComponent<Image>().color = c;
        }
    }

}
