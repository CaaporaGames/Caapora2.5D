using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Caapora
{


public class UIInterface : MonoBehaviour {

    private static UIInterface _instance;
        private Text TotalChamas;
        private Text Timer;
        public Text timeGUI;
        public GameObject winnerModal;
        public GameObject loserModal;
        public GameObject pauseModal;

        public static UIInterface instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIInterface>();

                }

                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {

                _instance = this;

            }
            else
            {

                if (this != _instance)
                    Destroy(this.gameObject);
            }

            winnerModal = GameObject.Find("Winner");
            loserModal = GameObject.Find("GameOver");
            pauseModal = GameObject.Find("Pause");


            instance.winnerModal.SetActive(false);
            instance.loserModal.SetActive(false);
            instance.pauseModal.SetActive(false);

            TotalChamas = GameObject.Find("TotalChamas").GetComponent<Text>();

            Timer = GameObject.Find("Tempo").GetComponent<Text>();

            timeGUI = GameObject.Find("Hora").GetComponent<Text>();


        }


    private void Update()
        {

            
            TotalChamas.text = "Chamas: " + GameManager.totalOfFlames;

            Timer.text = "Tempo : " + Mathf.Round(GameManager.instance.CurrentTimeLeft);


        }


    public void Hide()
    {

       _instance.gameObject.SetActive(false);

    }

        public void Show()
        {
            _instance.gameObject.SetActive(true);
        }
    }

}