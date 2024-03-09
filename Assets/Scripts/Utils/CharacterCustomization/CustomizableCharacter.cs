using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizableCharacter : MonoBehaviour
{
    [SerializeField] private CustomizedCharacter m_character;

    [ContextMenu("Randomize All")]
    public void Randomize()
    {
        var elements = GetComponentsInChildren<CustomizableElement>();
        foreach (var element in elements)
        {
            element.Randomize();
        }
    }

    public void StoreCustomizationInformation()
    {
        var elements = GetComponentsInChildren<CustomizableElement>();
        m_character.Data.Clear();
        foreach (var element in elements)
        {
            m_character.Data.Add(element.GetCustomizationData());
        }

        SceneManager.LoadScene("GameScene");
    }
}
