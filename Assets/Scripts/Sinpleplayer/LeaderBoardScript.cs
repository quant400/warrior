using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LeaderBoardScript : MonoBehaviour
{
    [SerializeField]
    Button defaultButton;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private CanvasGroup fighterCanvas;
    internal void Activate()
    {
        SetAllButtonsInteractable();
        transform.DOScale(Vector3.one, 1f);
        defaultButton.interactable=false;
        fighterCanvas.alpha = 1;
    }

    internal void Deactivate()
    {
        transform.DOScale(Vector3.zero, 1f);

    }

    public void SetAllButtonsInteractable()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OnButtonClicked(Button clickedButton)
    {
        int buttonIndex = System.Array.IndexOf(buttons, clickedButton);

        if (buttonIndex == -1)
            return;

        if(clickedButton == buttons[3])
        {
            fighterCanvas.alpha = 0;
        }
        else
        {
            fighterCanvas.alpha = 1;
        }

        SetAllButtonsInteractable();

        clickedButton.interactable = false;
    }

}