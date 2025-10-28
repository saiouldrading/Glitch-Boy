// using System;
// using UnityEngine;

// public class FadeAnimation : MonoBehaviour
// {
//     [SerializeField] private CanvasGroup myUIGroup;
//     private Action onFadeOutComplete;
//     private Action onFadeInComplete;

//     private bool isFadeIn = false;
//     private bool isFadeOut = false;

//     public void ShowUI(Action onComplete = null)
//     {
//         isFadeIn = true;
//         onFadeInComplete = onComplete;
//     }

//     public void HideUI(Action onComplete = null)
//     {
//         isFadeOut = true;
//         onFadeOutComplete = onComplete;
//     }

//     void Update()
//     {
//         if (isFadeIn)
//         {
//             if (myUIGroup.alpha < 1f)
//             {
//                 myUIGroup.alpha += Time.deltaTime;
//                 if (myUIGroup.alpha >= 1f)
//                 {
//                     myUIGroup.alpha = 1f;
//                     isFadeIn = false;
//                     onFadeInComplete?.Invoke();
//                 }
//             }
//         }

//         if (isFadeOut)
//         {
//             if (myUIGroup.alpha > 0f)
//             {
//                 myUIGroup.alpha -= Time.deltaTime;
//                 if (myUIGroup.alpha <= 0f)
//                 {
//                     myUIGroup.alpha = 0f;
//                     isFadeOut = false;
//                     onFadeOutComplete?.Invoke();
//                 }
//             }
//         }
//     }
// }

using UnityEngine;

using System;


public class FadeAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup myUIGroup;
    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private Action onFadeInComplete;
    private Action onFadeOutComplete;


    public void ShowUI(Action oncomplete = null)
    {
        isFadeIn = true;
        onFadeInComplete = oncomplete;
    }

    public void HideUI(Action oncomplete = null)
    {
        isFadeOut = true;
        onFadeOutComplete = oncomplete;

    }


    void Update()
    {
        if (isFadeIn)
        {
            if (myUIGroup.alpha < 1)

            {
                myUIGroup.alpha += Time.deltaTime;
                if (myUIGroup.alpha >= 1)
                {
                    isFadeIn = false;
                    myUIGroup.alpha = 1f;
                    onFadeInComplete?.Invoke();
                }
            }
        }

        if (isFadeOut)
        {
            if (myUIGroup.alpha > 0)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha <= 0)
                {
                    isFadeOut = false;
                    myUIGroup.alpha = 0f;
                    onFadeOutComplete?.Invoke();
                }
            }
        }
    }

}

