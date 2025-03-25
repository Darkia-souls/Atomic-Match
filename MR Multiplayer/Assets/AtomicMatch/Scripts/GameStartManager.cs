using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AtomicMatch.Scripts
{
    public class GameStartManager : MonoBehaviour
    {
        public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
        public Button playButton;        // Reference to the Play button
        public GameObject[] objectsToActivate; // Objects to activate
        public Canvas uiCanvas;          // Reference to the UI canvas

        void Start()
        {
            // Deactivate objects at the start
            DeactivateObjects();
        
            videoPlayer.Play();
        
            // Set up the Play button click event
            playButton.onClick.AddListener(OnPlayButtonClicked);
        
        }
        void OnPlayButtonClicked()
        {
            // Activate the objects when the Play button is clicked
            ActivateObjects();

            // Destroy the UI canvas
            Destroy(uiCanvas.gameObject);

            // the video stops
            videoPlayer.Stop();

        }
        void DeactivateObjects()
        {
            // Deactivate all objects in the array at the beginning
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(false);
            }
        }

        void ActivateObjects()
        {
            // Activate all objects when the button is clicked
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
