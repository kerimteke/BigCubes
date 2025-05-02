using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kup : MonoBehaviour
{
    [HideInInspector] public Color _KupRengi;
    [HideInInspector] public int _KupNumarasi;
    [HideInInspector] public Rigidbody _KupRb;
    [HideInInspector] public bool _AnaKup;
    public GameObject _Kupizi;
    MeshRenderer _KupMeshRenderer;
    float _UygulanacakGuc = 3f; // yeni kup oluştuğunda uygulanacak güç

    private void Awake()
    {
        _KupMeshRenderer = GetComponent<MeshRenderer>();
        _KupRb = GetComponent<Rigidbody>();
        _AnaKup = false;
    }
    public void RenkTanimla(Color renk)
    {
        _KupRengi = renk;
        _KupMeshRenderer.material.color = renk;
    }
    public void NumaraOlustur(int numara)
    {
        _KupNumarasi = numara;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Kup _CarpilanKup = collision.gameObject.GetComponent<Kup>();

        if (_CarpilanKup != null && _KupNumarasi == _CarpilanKup._KupNumarasi && _KupNumarasi != 11) 
        {
            Vector3 TemasNoktasi = collision.contacts[0].point;

            GameManager.Instance._KupYonetimi.KupOlustur(++_KupNumarasi, TemasNoktasi + Vector3.up * 1.5f);
            Kup YeniKup = GameManager.Instance._KupYonetimi.OlusanKupuGetir();
            // Güç
            YeniKup._KupRb.AddForce(new Vector3(0, .2f, 1.5f) * _UygulanacakGuc, ForceMode.Impulse);
            // Tork - opsiyonel
            Vector3 TorkYonu = Vector3.one * Random.Range(-30f, 30f);   // one = Vector3(1,1,1) // demektir.
            YeniKup._KupRb.AddTorque(TorkYonu);

            // YÖNTEM 2 - ANA YÖNTEM
            int MaksimumColliderSayisi = 10;

            Collider[] KapsamaAlani = new Collider[MaksimumColliderSayisi];

            int EtkilesimdekiKupler = Physics.OverlapSphereNonAlloc(TemasNoktasi, 3f, KapsamaAlani);
            float PatlamaGucu = 400f;
            float PatlamaMenzili = 2.5f;

            for (int i = 0; i < EtkilesimdekiKupler; i++)
            {
                if (KapsamaAlani[i].attachedRigidbody != null)
                    KapsamaAlani[i].attachedRigidbody.AddExplosionForce(PatlamaGucu, TemasNoktasi, PatlamaMenzili);
            }
// YÖNTEM 1 AŞAĞIDA
            
            GameManager.Instance.EfektOynat(TemasNoktasi, _KupRengi);
            GameManager.Instance.SesCal(4);

            GameManager.Instance._KupYonetimi.KupPasiflestir(gameObject.GetComponent<Kup>());
            GameManager.Instance._KupYonetimi.KupPasiflestir(_CarpilanKup);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BolumKontrol"))
        {
            if (!_AnaKup)
                GameManager.Instance.Kaybettin();
            
        }
    }

}
/* // YÖNTEM 1
           Collider[] Kupler = Physics.OverlapSphere(TemasNoktasi, 2f);
           float PatlamaGucu = 400f;
           float PatlamaMenzili = 1.5f;

           foreach (Collider coll in Kupler)
           {
               if (coll.attachedRigidbody != null)
                   coll.attachedRigidbody.AddExplosionForce(PatlamaGucu, TemasNoktasi, PatlamaMenzili);
           }
           */