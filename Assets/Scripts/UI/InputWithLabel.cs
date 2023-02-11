using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputWithLabel : MonoBehaviour
{
	public bool checkBoxIsChecked;
	public bool radioSelectIsChecked;

	[Header("References")]
	public TextMeshProUGUI labelText;
	public TMP_InputField primaryInputField;
	public TMP_InputField secondaryInputField;
	public TMP_Dropdown dropdown;
	public Button button;
	public Button checkBox;
	public Image checkBoxFilledImage;
	public Button radioSelect;
	public Image radioSelectFilledImage;
	public GameObject scrollView;
	public Transform scrollViewContent;

	public void SetCheckBox()
	{
		checkBoxIsChecked = !checkBoxIsChecked;
		checkBoxFilledImage.gameObject.SetActive(checkBoxIsChecked);
	}
	public void SetCheckBox(bool setChecked)
	{
		checkBoxIsChecked = setChecked;
		checkBoxFilledImage.gameObject.SetActive(checkBoxIsChecked);
	}
	public void SetRadioSelect()
	{
		radioSelectIsChecked = !radioSelectIsChecked;
		checkBoxFilledImage.gameObject.SetActive(radioSelectIsChecked);
	}
	public void SetRadioSelect(bool setChecked)
	{
		radioSelectIsChecked = !setChecked;
		checkBoxFilledImage.gameObject.SetActive(radioSelectIsChecked);
	}
}
