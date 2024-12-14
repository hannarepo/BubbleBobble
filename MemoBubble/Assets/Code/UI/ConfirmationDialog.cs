using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoBubble
{
    public class ConfirmationDialog : MonoBehaviour
    {
        public GameObject dialogPanel;
        public Button yesButton;            //The WHOLE SCRIPT IS NOT IN USE
                                            //KEEPING IT FOR FUTURE REFERENCE/JUST IN CASE
        public Button noButton;

        private System.Action onYesAction;

        // Start is called before the first frame update
        void Start()
        {
            dialogPanel.SetActive(false);

            yesButton.onClick.AddListener(OnYesClicked);
            noButton.onClick.AddListener(OnNoClicked);
        }
    // TODO: FUCK START AND UPDATE, MIMICK OPTIONS
        public void Show(System.Action onYes)
        {
            onYesAction = onYes;
            dialogPanel.SetActive(true);
        }

        private void OnYesClicked()
        {
            dialogPanel.SetActive(false);
            onYesAction?.Invoke();
        }

        private void OnNoClicked()
        {
            dialogPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
