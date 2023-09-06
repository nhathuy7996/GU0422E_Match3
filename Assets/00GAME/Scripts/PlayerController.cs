using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _target;
    Tween _tween;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(_movement * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
           _tween = this.transform.DOMove(_target.position,2);
            _tween.SetEase(Ease.InOutExpo);
            _tween.OnUpdate(() =>
            {

                Debug.LogError("ve dich");
               
            });

            _tween.OnStart(() =>
            {

            });


            
            _tween.SetLoops(30);
            _tween.Play();

           
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _tween.Kill();
             
        }
    }
}
