using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject rulesPanel;
    [SerializeField] Button startButton;
    [SerializeField] Button infoButton;

    void Start()
    {
        rulesPanel.SetActive(false);
        infoButton.interactable = true;
        startButton.interactable = true;
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
        infoButton.interactable = false;
        startButton.interactable = false;
    }

    public void HideRules()
    {
        rulesPanel.SetActive(false);
        infoButton.interactable = true;
        startButton.interactable = true;
    }
}
