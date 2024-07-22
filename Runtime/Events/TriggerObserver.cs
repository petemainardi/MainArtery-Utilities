using UnityEngine;
using UnityEngine.Events;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /// <summary>
    /// Notify listeners when collision trigger events are fired on this GameObject.
    /// </summary>
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public UnityEvent<Collider> Entered { get; } = new UnityEvent<Collider>();
        public UnityEvent<Collider> Stayed { get; } = new UnityEvent<Collider>();
        public UnityEvent<Collider> Exited { get; } = new UnityEvent<Collider>();

        private void OnTriggerEnter(Collider other) => Entered.Invoke(other);
        private void OnTriggerStay(Collider other) => Stayed.Invoke(other);
        private void OnTriggerExit(Collider other) => Exited.Invoke(other);
    }
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}