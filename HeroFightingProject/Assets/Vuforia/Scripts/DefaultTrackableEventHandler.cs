/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
        Transform CameraTransform;
        public Vector3 offset;
        public Transform target;
       
        private bool isShow=false;
        private Transform playerTransform;
       
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            playerTransform = transform.GetChild(0).transform;
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS


      
        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                //CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
                //offset = CameraTransform.position - playerTransform.position;
                GameController._instance.gameState = GameState.Start;
                EnemySource._instance.isMake=true;
                    isShow = true;
                    playerTransform.parent = transform;
                    playerTransform.localPosition =Vector3.zero;
                    OnTrackingFound();
            }
            else
            {
                if (isShow)
                {
                    target.localPosition = Vector3.zero;
                    target.localRotation = Quaternion.Euler(Vector3.zero);
                    playerTransform.parent = target;
                   
                }
                else
                {
                    OnTrackingLost();
                }
               
            }
        }

        #endregion // PUBLIC_METHODS
        //public void FollowPlayer()
        //{
        //    CameraTransform.position = offset + playerTransform.position;
        //}


        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

        }

        #endregion // PRIVATE_METHODS
    }
}
