using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            //game object does not have component yat implements IHasProgress
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //when event fired off in cutting counter it sends progress (currentProgress/maxProgress)
        barImage.fillAmount = e.progressNormalised;

        //empty or full (bar filling is normalise dos it ranges from 0-1)
        if (e.progressNormalised == 1f)
        {
            Hide();
        }
        else
        {   
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
