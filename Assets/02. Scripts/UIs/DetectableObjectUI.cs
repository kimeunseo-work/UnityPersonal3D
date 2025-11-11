using System.Collections;
using TMPro;
using UnityEngine;

public class DetectableObjectUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _tag;
    [SerializeField] TextMeshProUGUI _desc;

    public void Init(SO_Detectable data)
    {
        gameObject.SetActive(false);

        _name.text = data.Name;
        _tag.text = data.InterativeType.ToString();
        _desc.text = data.Desc;
    }

    public IEnumerator DisplayPopupForSeconds()
    {
        gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }
}