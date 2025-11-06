using CrimsofallTechnologies.XR.Interaction;
using UnityEngine;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    [DefaultExecutionOrder(2)]
    public class Lamp : Interactable
    {
        public GameObject onObject, offObject;
        public Material onMat, offMat;

        private MeshRenderer mRenderer;

        public override void Start()
        {
            base.Start();
            mRenderer = GetComponent<MeshRenderer>();

            Material[] mats = mRenderer.materials;
            if(GameManager.Instance.IsDay)
            {
                onObject.SetActive(false);
                offObject.SetActive(true);
                mats[2] = offMat;
            }
            else
            {
                onObject.SetActive(true);
                offObject.SetActive(false);
                mats[2] = onMat;
            }
            mRenderer.materials = mats;
        }

        public override void OnInteract()
        {
            base.OnInteract();
            GameManager.Instance.SetDayNightValue(!GameManager.Instance.IsDay);

            Material[] mats = mRenderer.materials;
            if(GameManager.Instance.IsDay)
            {
                onObject.SetActive(false);
                offObject.SetActive(true);
                mats[2] = offMat;
            }
            else
            {
                onObject.SetActive(true);
                offObject.SetActive(false);
                mats[2] = onMat;
            }
            mRenderer.materials = mats;
        }
    }
}
