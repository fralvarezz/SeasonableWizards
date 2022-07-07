using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimateWizardEnd : MonoBehaviour
{
    public Image[] imgs;
    private TextMeshProUGUI _winText;
    public GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _winText = GetComponentInChildren<TextMeshProUGUI>();
        _player.transform.position = Vector3.zero;

        //DOLocalMove
        imgs[0].transform.DOMove(new Vector3(-1.4f, 0), 1, false);
        imgs[1].transform.DOMove(new Vector3(-0.5f, 0), 1, false);
        imgs[2].transform.DOMove(new Vector3(1.35f, 0), 1, false);
        imgs[3].transform.DOMove(new Vector3(0.5f, 0), 1, false);
        
        _winText.transform.localScale = Vector3.zero;
        _winText.transform.DOScale(Vector3.one, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
