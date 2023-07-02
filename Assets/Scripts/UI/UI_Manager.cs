using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    //TODO: Get Point from Singletone
    [SerializeField]
    private int _Points;
    [SerializeField]
    private TextMeshProUGUI _PointsValue;

    //TODO: Get Hearts from Singletone
    [SerializeField]
    private int _Hearts;

    [SerializeField]
    private GameObject _Heart1;
    [SerializeField]
    private GameObject _Heart2;
    [SerializeField]
    private GameObject _Heart3;

    [SerializeField]
    private GameObject _NoHeart1;
    [SerializeField]
    private GameObject _NoHeart2;
    [SerializeField]
    private GameObject _NoHeart3;

    [SerializeField]
    private OptionsViewController _OptionsViewPrefab;

    private OptionsViewController _optionsViewController;


    // Update is called once per frame
    void Update()
    {
        _PointsValue.SetText(_Points.ToString());

        switch (_Hearts) {
            case 0:
                //No hearts
                _NoHeart1.SetActive(true);
                _NoHeart2.SetActive(true);
                _NoHeart3.SetActive(true);

                //Hearts
                _Heart1.SetActive(false);
                _Heart2.SetActive(false);
                _Heart3.SetActive(false);
                break;
            case 1:
                //No hearts
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(true);
                _NoHeart3.SetActive(true);

                //Hearts
                _Heart1.SetActive(true);
                _Heart2.SetActive(false);
                _Heart3.SetActive(false);
                break;
            case 2:
                //No hearts
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(false);
                _NoHeart3.SetActive(true);

                //Hearts
                _Heart1.SetActive(true);
                _Heart2.SetActive(true);
                _Heart3.SetActive(false);
                break;
            case 3:
                //No hearts
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(false);
                _NoHeart3.SetActive(false);

                //Hearts
                _Heart1.SetActive(true);
                _Heart2.SetActive(true);
                _Heart3.SetActive(true);
                break; ;
        }
    }

    [ContextMenu("Options")]
    public void OpenOptions() {
        Debug.Log("Options view instantiating");
        if (_optionsViewController) return;
        _optionsViewController = Instantiate(_OptionsViewPrefab);
    }
}
