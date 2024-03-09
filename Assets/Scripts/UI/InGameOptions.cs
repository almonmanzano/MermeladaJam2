using UnityEngine;

public class InGameOptions : MonoBehaviour
{
    [SerializeField] private GameObject m_options;
    [SerializeField] private GameObject m_gameController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOptions();
        }
    }

    public void ShowOptions()
    {
        m_options.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideOptions()
    {
        m_options.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
