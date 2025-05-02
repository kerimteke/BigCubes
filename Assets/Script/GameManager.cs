using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("----EFEKT YONETİMİ")]
    [SerializeField] ParticleSystem _KupBirlestirmeEfekti;
    ParticleSystem.MainModule _KupEfektAnaModulu;

    [Header("----KUP YONETİMİ")]
    public KupYonetimi _KupYonetimi;

    [Header("----SES YONETİMİ")]
    [SerializeField] AudioSource[] _Sesler;

    [Header("----UI YONETİMİ")]
    [SerializeField] GameObject[] _Paneller;

    int _SahneIndex;    // Tekrar tekrar kod yazmamak için sahne şeysi çağırmamak için baştan bir kerede çağırmak.. [0]

    private void Awake()
    {
        _SahneIndex = SceneManager.GetActiveScene().buildIndex; // Tekrar tekrar kod yazmamak için sahne şeysi çağırmamak için baştan bir kerede çağırmak.. [1]

        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    void Start()
    {
        _KupEfektAnaModulu = _KupBirlestirmeEfekti.main;
    }

    public void SesCal(int Index)
    {
        _Sesler[Index].Play();
    }

    public void Kaybettin()
    {
        Debug.Log("Kaybettin");
        _Sesler[2].Play();
    }
    public void Kazandin()
    {
        PlayerPrefs.SetInt("SonBolum", _SahneIndex + 1);
        Debug.Log("kazandin");
        _Sesler[3].Play();
    }

    public void ButonIslemi(string ButonDeger)
    {
        switch (ButonDeger)
        {
            case "Durdur":
                SesCal(1);
                PanelAc(2);
                Time.timeScale = 0;             // Oyunu durdur..
                break;
            case "DevamEt":
                SesCal(1);
                PanelKapat(2);
                Time.timeScale = 1;
                break;
            case "OyunaBasla":
                SesCal(1);
                PanelKapat(1);
                PanelAc(0);
              //  Time.timeScale = 1;     // Opsiyon olarak bıraktı, şu an gerekli değil.
                break;
            case "Tekrar":
                SesCal(1);
                SceneManager.LoadScene(_SahneIndex);// Tekrar tekrar kod yazmamak için sahne şeysi çağırmamak için baştan bir kerede çağırmak.. [2]
                Time.timeScale = 1;
                break;
            case "SonrakiLevel":
                SceneManager.LoadScene(_SahneIndex + 1); // Tekrar tekrar kod yazmamak için sahne şeysi çağırmamak için baştan bir kerede çağırmak.. [3]
                Time.timeScale = 1;
                break;
            case "Cikis":
                SesCal(1);
                PanelAc(5);
                break;
            case "Evet":
                SesCal(1);
                Debug.Log("Çıkış");
                Application.Quit();
                break;
            case "Hayir":
                SesCal(1);
                PanelKapat(5);
                break;
        }
    }

    public void PanelAc(int PanelIndex)
    {
        _Paneller[PanelIndex].SetActive(true);
    }
    public void PanelKapat(int PanelIndex)
    {
        _Paneller[PanelIndex].SetActive(false);
    }

    public void EfektOynat(Vector3 pozisyon, Color Renk)
    {
        _KupEfektAnaModulu.startColor = Renk;
        _KupBirlestirmeEfekti.transform.position = pozisyon;
        _KupBirlestirmeEfekti.Play();
    }

}
