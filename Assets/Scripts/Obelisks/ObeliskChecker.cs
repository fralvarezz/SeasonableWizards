using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskChecker : MonoBehaviour
{
    private bool _complete = false;
    private bool _gameEnding = false;
    private bool _flag = false;
    private float _timer = 0f;

    public List<CenterObelisk> obelisks;
    public GameObject player;
    public GameObject seasonManger;
    public GameObject wizardsAnimation;
    public GameObject pressSpace;


    // Update is called once per frame
    void Update()
    {
        if (_flag)
        {
            _timer += Time.deltaTime;
            if (_timer > 1)
            {
                wizardsAnimation.SetActive(true);
                pressSpace.SetActive(true);
            }
        }

        if (_gameEnding && !_flag)
        {
            _flag = true;
            var camScript = Camera.main.GetComponent<CameraScript>();
            camScript.finishedGame();
            player.SetActive(false);
            //seasonManger.SetActive(false);
            return;
        }

        _complete = true;
        foreach (CenterObelisk ob in obelisks)
        {
            if (ob.GetActive() == false) _complete = false;
        }

        if (_complete)
        {
            Debug.Log("All obelisks active");
            _gameEnding = true;
        }
    }
}
