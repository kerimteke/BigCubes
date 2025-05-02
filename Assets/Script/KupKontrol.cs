using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupKontrol : MonoBehaviour
{
    [SerializeField] float _HareketHizi;
    [SerializeField] float _GidisHizi;
    [SerializeField] float _HareketSiniri;      // kenarlara sınır yapıyoruz ki yapmasak da olur ancak hatanın önüne geçiyoruz, küp duvarlardan dışarı hiç taşmasın..
    [SerializeField] TouchSlider _TouchSlider;

    Kup _Kup;
    bool _DokunmaBasladi, _HareketEdiyor;
    Vector3 _KupPos;

    void Start()
    {
        KupOlustur();
        _HareketEdiyor = true;

        _TouchSlider.OnPointerDownEvent += OnPointerDown;
        _TouchSlider.OnPointerDragEvent += OnPointerDrag;
        _TouchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (_DokunmaBasladi && _HareketEdiyor)    // Dokunma başladı ise, benim küpümü hareket ettir..(bu küpün pozisyonunu değiştirmem gerekiyor)
        {
            // Lerp, yumuşak bir geçiş için.
            _Kup.transform.position = Vector3.Lerp(
                _Kup.transform.position,    // Küpümün mevcut pozisyonundan....
                _KupPos,                    // Küp pozisyonuma hareket et....
                _HareketHizi * Time.deltaTime   // ..ile frame'e göre hareket ederek bir hesaplama yapmasını istiyoruz.
                );
        }
    }

    private void OnPointerDown()
    {
        _DokunmaBasladi = true;
    }

    private void OnPointerDrag(float HareketDegeri)
    {
        if (_DokunmaBasladi && _HareketEdiyor)
        {
            _KupPos = _Kup.transform.position;
            _KupPos.x = HareketDegeri * _HareketSiniri;
        }
    }

    private void OnPointerUp()
    {
        if (_DokunmaBasladi && _HareketEdiyor)
        {
            _DokunmaBasladi = false;
            _HareketEdiyor = false;

            _Kup._KupRb.AddForce(Vector3.forward * _GidisHizi, ForceMode.Impulse);
            _Kup._Kupizi.SetActive(false);
            Invoke("YeniKupOlustur", .3f);  // küpü gönderdikten 0,3sn sonra yeni küpü oluştur.. 
        }
    }

    void YeniKupOlustur()
    {
        _Kup._AnaKup = false;   // gönderdikten sonra anaKüp olmaktan çıkarıyoruz ki gittiği yerde sorun çıkmasın..
        _Kup = null;
        KupOlustur();
        
    }

    void KupOlustur()   // Yeni küp mantığı..
    {
        _Kup = GameManager.Instance._KupYonetimi.RastGeleKupOlustur() ;   // Sistemden-Havuzdan yeni bir küp istiyoruz..Ona özgü değişken ayarlamaları alt satırlar ve en son kupPozisyonunu da atayarak tamamlıyoruz..
        _Kup._AnaKup = true;
        _Kup._Kupizi.SetActive(true);

        _KupPos = _Kup.transform.position;
        _HareketEdiyor = true;

    }

    private void OnDestroy()        //dinleyicilerin abonelikten çıkarılması.
    {
        _TouchSlider.OnPointerDownEvent -= OnPointerDown;
        _TouchSlider.OnPointerDragEvent -= OnPointerDrag;
        _TouchSlider.OnPointerUpEvent -= OnPointerUp;
    }

}
