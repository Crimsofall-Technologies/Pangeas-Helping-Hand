using UnityEngine;
using UnityEngine.InputSystem;

public class MiniSoundSetting : MonoBehaviour
{
	public InputActionProperty action;
	public GameObject panel;
	
	private void Update()
	{
		if(action.action.WasPressedThisFrame())
		{
			if(panel.activeSelf)
				GameManager.ui.OnSettingUiClosed();

			panel.SetActive(!panel.activeSelf);
		}
	}
}
