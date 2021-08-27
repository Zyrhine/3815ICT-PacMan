using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject ConfigurePanel;


    // Start is called before the first frame update
    void Start()
    {
        ShowMainPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainPanel()
    {
        MainPanel.SetActive(true);
        ConfigurePanel.SetActive(false);
    }

    public void ShowConfigurePanel()
    {
        MainPanel.SetActive(false);
        ConfigurePanel.SetActive(true);
    }
}
