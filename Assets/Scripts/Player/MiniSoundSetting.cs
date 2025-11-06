using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Jump;

public class MiniSoundSetting : MonoBehaviour
{
	public InputActionProperty action;
	public InputActionProperty jumpAction;
	public GameObject panel;
	public GravityProvider gravityProvider;

	public AudioSource jumpSource;
	
	private void Update()
	{
		if(action.action.WasPressedThisFrame())
		{
			if(panel.activeSelf)
				GameManager.ui.OnSettingUiClosed();

			panel.SetActive(!panel.activeSelf);
		}

		if(jumpAction.action.WasPressedThisFrame() && gravityProvider.isGrounded)
        {
            jumpSource.PlayOneShot(jumpSource.clip, jumpSource.volume);
        }
	}
}
