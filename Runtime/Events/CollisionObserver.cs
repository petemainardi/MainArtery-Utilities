using UnityEngine;
using UnityEngine.Events;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /// <summary>
    /// Notify listeners when collision events are fired on this GameObject.
    /// </summary>
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    [RequireComponent(typeof(Collider))]
    public class CollisionObserver : MonoBehaviour
    {
		public UnityEvent<Collision> Entered { get; } = new UnityEvent<Collision>();
		public UnityEvent<Collision> Stayed { get; } = new UnityEvent<Collision>();
		public UnityEvent<Collision> Exited { get; } = new UnityEvent<Collision>();

		private void OnCollisionEnter(Collision collision) => Entered.Invoke(collision);
		private void OnCollisionStay(Collision collision) => Stayed.Invoke(collision);
		private void OnCollisionExit(Collision collision) => Exited.Invoke(collision);
    }
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}