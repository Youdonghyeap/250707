using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class DoTweenExample : MonoBehaviour
{
    //public Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //transform.DOMove(new Vector3(3, 5, 0), 2f);
        //transform.DOScale(new Vector3(2, 2, 2), 1f);
        //transform.DORotate(new Vector3(0, 0, 180), 3f, RotateMode.FastBeyond360);
        //transform.DOMoveX(5, 1f).SetLoops(-1, LoopType.Yoyo.Yoyo);
        //img.DOFade(0.2f, 2f);

        DG.Tweening.Sequence seq = DOTween.Sequence();

        //seq.Append(transform.DOMoveZ(3f, 2f));
        //seq.Append(transform.DOMoveY(2f, 1f));
        //seq.Append(transform.DORotate(new Vector3(0, 180, 0), 1f, RotateMode.Fast)
        //seq.Append(transform.DOScale(Vector3.one, 1f));
        //seq.OnComplete(() => Debug.Log("모든 애니메이션 완료!"));
        
        seq.Append(transform.DOMoveZ(3f, 2f));
        seq.Append(transform.DOMoveY(2f, 1f))
            .Join(transform.DOScale(new Vector3(2, 2, 2), 1f))
            .Join(transform.DORotate(new Vector3(0, 180, 0), 1f));
        seq.OnComplete(() => Debug.Log("모든 애니메이션 완료!"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
