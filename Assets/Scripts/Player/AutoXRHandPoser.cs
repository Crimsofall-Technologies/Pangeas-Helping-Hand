using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

namespace CrimsofallTechnologies.XR 
{
    //this animates the hand into poses when pressing special buttons on the controller.
    public class AutoXRHandPoser : MonoBehaviour
    {
        public XRInputValueReader<float> gripInput, triggerButton;

        public float animatorSpeed = 1f;
		private Animator handAnimator;

        private void Start() {
			handAnimator = GetComponentInChildren<Animator>();
			
			if(handAnimator != null)
				handAnimator.speed = animatorSpeed;
        }

        private void OnEnable()
        {
            gripInput?.EnableDirectActionIfModeUsed();
            triggerButton?.EnableDirectActionIfModeUsed();
        }

        private void OnDisable()
        {
            gripInput?.DisableDirectActionIfModeUsed();
            triggerButton?.DisableDirectActionIfModeUsed();
        }

        private void Update()
        {
            if(handAnimator == null) return;

            var grip = gripInput.ReadValue();
            var trigger = triggerButton.ReadValue();

            handAnimator.SetFloat("Grip", grip);
            handAnimator.SetFloat("Trigger", trigger);
        }
    }
}
