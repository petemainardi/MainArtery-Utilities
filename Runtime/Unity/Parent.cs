using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
//using Sirenix.OdinInspector;

//#pragma warning disable 0649    // Variable declared but never assigned to

namespace MainArtery.Utilities.Unity
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     * Component that treats all of its child gameobjects as an ordered list.
     * Its purpose is to facilitate operations performed upon such a list so that containing
     * classes can just manipulate the set of child objects with out having to go through Unity's
     * interface for doing so, or tracking order themselves.
     * 
     * An example use case of this could be managing a collection of like objects that need to be
     * spatially associated with each other, particularly for easily managing how they are layered.
     * 
     * Note that this class expects to be the only one that controls whether a Transform is its
     * child, and adding children by other means (e.g. in the editor at runtime) will cause
     * unexpected behavior.
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    [Serializable]
    public class Parent : MonoBehaviour
    {
        // Fields =================================================================================
        //[SerializeField, OnValueChanged("ResetOrder")]
        [Tooltip("Whether the list starts or ends at sibling index 0, i.e. whether later nodes are drawn on top of earlier ones")]
        private bool beginAtFirst;
        public bool BeginAtTopOfHierarchy => this.beginAtFirst;

        //[SerializeField, BoxGroup("Naming Convention")]
        private string prefix = "Node";
        public string Prefix => this.prefix;
        //[SerializeField, ReadOnly, BoxGroup("Naming Convention")]
        private string separator = " ";
        public string Separator => this.separator;
        //[SerializeField, ReadOnly, BoxGroup("Naming Convention")]
        private string postfix = "({0})";
        public string Postfix => string.Format(this.postfix, "#");

        // TODO: If postfix is going to be editable, this needs to be too...
        // TODO: And then we'd also need to account for an editable separator here...
        private readonly Regex regex = new Regex(@"(?<=\()\d+(?=\)$)", RegexOptions.RightToLeft);     // Regex match for node naming convention - Get int value at end of name in parentheses
        // ========================================================================================

        // Properties =============================================================================
        public int Count => this.transform.childCount;
        public Transform Last => this[this.Count - 1];
        public Transform First => this[0];

        public Transform this[int index]
        {
            get => Mathf.Clamp(index, 0, this.Count) == index
                ? this.transform.GetChild(this.beginAtFirst ? index : (this.Count - 1) - index)
                : null;
        }
        public int this[Transform child]
        {
            get => this.Owns(child) == this.transform
                ? (this.beginAtFirst ? child.GetSiblingIndex() : (this.Count - 1) - child.GetSiblingIndex())
                : -1;
        }

        public List<Transform> Children => this.ChildrenBefore(this.Count);
        // ========================================================================================

        // Initialization =========================================================================
        public void Awake()
        {
            this.ResetOrder();
        }
        public void OnValidate()
        {
            for (int i = 0; i < this.Count; i++)
            {
                // TODO: Validate that postfix has a numerical value associated with it from which we can index
                if (!this.postfix.Any(c => char.IsDigit(c)))
                    Debug.LogWarning($"Postfix of Parent component on {this.transform.name} does not contain a numerical value for indexing.");

                Transform child = this[i];
                if (!regex.IsMatch(child.name) || !Int32.TryParse(regex.Match(child.name).Value, out int index))
                    Debug.LogWarning($"Invalid child name {child.name} under Parent {this.transform.name}.");
            }
        }
        // ========================================================================================

        // Child Accessors ========================================================================

        public bool Owns(Transform child) => child.parent == this.transform;

        public List<Transform> ChildrenBefore(Transform child) => this.ChildrenBefore(this[child]);
        public List<Transform> ChildrenAfter(Transform child) => this.ChildrenAfter(this[child]);
        public List<Transform> ChildrenBetween(Transform childFirst, Transform childLast) =>
            this.ChildrenBetween(this[childFirst], this[childLast], false, false);

        public List<Transform> ChildrenBefore(int index) => this.ChildrenBetween(0, index);
        public List<Transform> ChildrenAfter(int index) => this.ChildrenBetween(index, this.Count, false, false);
        public List<Transform> ChildrenBetween(int start, int end, bool startInclusive = true, bool endInclusive = false)
        {
            start = startInclusive ? start : start + 1;
            start = Mathf.Clamp(start, 0, this.Count);

            end = endInclusive ? end + 1 : end;
            end = Mathf.Clamp(end, start, this.Count);

            List<Transform> children = new List<Transform>();
            for (int i = start; i < end; i++)
                children.Add(this[i]);
            return children;
        }

        public bool ChildrenHave<T>() where T : MonoBehaviour
        {
            return this.Children.All(c => c.GetComponent<T>());
        }
        public List<T> ChildrenAs<T>() where T : MonoBehaviour
        {
            return this.Children.Select(c => c.GetComponent<T>()).ToList();
        }
        // ========================================================================================

        // Child Manipulation =====================================================================

        private int ChildIndexName(Transform child) => Int32.TryParse(this.regex.Match(child.name).Value, out int index) ? index : -1;
        private void RenameChild(Transform child, int index)
        {
            //child.name = $"{child.name.Remove(this.regex.Match(child.name).Index)}{string.Format(this.postfix, index)}"; //TODO: Currently not accounting for separator...
            child.name = $"{this.Prefix}{this.Separator}{string.Format(this.postfix, index)}";
        }


        public Transform Add(Transform node, bool controlNaming = true) => this.Insert(node, this.Count, controlNaming);
        public Transform Insert(Transform node, int index, bool controlNaming = true)
        {
            if (this.Owns(node)) return this.Move(node, index);

            index = Mathf.Clamp(index, 0, this.Count);
            node.SetParent(this.transform);
            node.SetSiblingIndex(this.beginAtFirst ? index : (this.Count - 1) - index);
            node.transform.name = $"{(controlNaming ? this.prefix : node.name)}{this.separator}{string.Format(this.postfix, index)}";

            foreach (Transform c in this.ChildrenAfter(index))
                this.RenameChild(c, this[c]);

            //this.ResetOrder();  // TODO: Do we always need to do a full reset?
            return node;
        }
        public Transform Move(Transform child, int index)
        {
            if (!this.Owns(child)) return this.Insert(child, index);

            int oldIndex = this[child];
            index = Mathf.Clamp(index, 0, this.Count);

            if (oldIndex != index)
            {
                child.SetSiblingIndex(this.beginAtFirst ? index : (this.Count - 1) - index);
                List<Transform> affectedChildren = oldIndex < index ? this.ChildrenBetween(oldIndex, index, true, true) : this.ChildrenAfter(index);
                foreach (Transform c in affectedChildren)
                    this.RenameChild(c, index);
                //this.ResetOrder();  // TODO: Do we always need to do a full reset?
            }
            return child;
        }
        public Transform Remove(Transform child) => this.Owns(child) ? this.Remove(this[child]) : child;
        public Transform Remove(int index)
        {
            Transform child = this[index];
            child.SetParent(null);

            foreach (Transform c in this.ChildrenBetween(index, this.Count))
                this.RenameChild(c, this[c]);

            //this.ResetOrder();  // TODO: Do we always need to do a full reset?
            return child;
        }



        // Really only necessary if this.beginAtFirst changed or if external forces have messed with our children
        public void ResetOrder()
        {
            List<Transform> children = this.Children;
            if (children.Any(c => !regex.IsMatch(c.name) || this.ChildIndexName(c) == -1))
            {
                Debug.LogError($"Parent {this.name} has invalidly named children. Could not reorder.");
                return;
            }

            // Reorder the nodes in the list by their name
            children = this.beginAtFirst
                ? children.OrderBy(n => Int32.Parse(regex.Match(n.name).Value)).ToList()
                : children.OrderByDescending(n => Int32.Parse(regex.Match(n.name).Value)).ToList();

            // Reset indeces of the nodes for their new positions and rename in case their exact order is no longer correct
            for (int i = 0; i < children.Count; i++)
            {
                Transform child = children[i];
                child.SetSiblingIndex(i);
                this.RenameChild(child, this[child]);
            }
        }
        // ========================================================================================
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}