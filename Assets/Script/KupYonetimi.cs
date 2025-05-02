using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupYonetimi : MonoBehaviour
{
    public Kup[] _KupHavuzu;
    [SerializeField] Color[] _KupRenkleri;
    [SerializeField] Transform _KupOlusmaNoktasi;

    Kup _OlusanKup;

    public void KupOlustur(int KupNumarasi, Vector3 pozisyon)
    {
        foreach (var item in _KupHavuzu)
        {
            if (!item.gameObject.activeInHierarchy) //sırası gelen obje (havuzdaki) pasif ise onu al..
            {
                _OlusanKup = item;
                _OlusanKup.transform.position = pozisyon;
                _OlusanKup.NumaraOlustur(KupNumarasi);
                _OlusanKup.gameObject.SetActive(true);
                _OlusanKup.RenkTanimla(RenkBelirle(KupNumarasi));

                break;  // pasif objeyi bulduğun an döngüyü kes.
            }
        }
    }


    public void KupPasiflestir(Kup _PasiflesecekKup)
    {
        _PasiflesecekKup._KupRb.velocity = Vector3.zero;
        _PasiflesecekKup._KupRb.angularVelocity = Vector3.zero;
        _PasiflesecekKup.transform.rotation = Quaternion.identity;
        _PasiflesecekKup._AnaKup = false;
        _PasiflesecekKup.gameObject.SetActive(false);


    }

    public Kup RastGeleKupOlustur()
    {
        KupOlustur(RastgeleSayiOlustur(), _KupOlusmaNoktasi.position);

        return _OlusanKup;
    }

    public Kup OlusanKupuGetir()
    {
        return _OlusanKup;
    }

    public int RastgeleSayiOlustur()
    {
        return UnityEngine.Random.Range(0, 5);
    }

    private Color RenkBelirle(int number)
    {
        return _KupRenkleri[number];
    }
}
