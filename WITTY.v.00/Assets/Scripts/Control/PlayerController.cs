using RPG.Movement;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using GameDevTV.Inventories;


namespace RPG.Control
{

 public class PlayerController : MonoBehaviour
 { 
    Health health;
   ActionStore actionStore;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float raycastRadius =1f;
        [SerializeField] int numberOfAbilities = 6;
       
       bool isDraggingUI=false;
        //haritada ne kadar uzağa gidebileceğinin değeri

    private void Awake()
    {
        health=GetComponent<Health>();
      actionStore=GetComponent<ActionStore>();
    }
    private void Update()
    {  //CheckSpecialAbilityKeys();
        if(InteractWithUI()) return;
         if(health.IsDead())
    {
         SetCursor(CursorType.None);
         return;
    } 
    UseAbilities();
        if(InteractWithComponent()) return;
        
        if(InteractWithMovement()) return;
       
        SetCursor(CursorType.None);
        //print("nothing to do");
    }
   /* private void CheckSpecialAbilityKeys()
        {
            var actionStore = GetComponent<ActionStore>();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionStore.Use(0, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                actionStore.Use(1, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                actionStore.Use(2, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                actionStore.Use(3, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                actionStore.Use(4, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                actionStore.Use(5, gameObject);
            }
        }*/
    private bool InteractWithUI()
    {
        if(Input.GetMouseButtonUp(0))
        {
            isDraggingUI=false;
        }
        if( EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0))
            {
                isDraggingUI=true;
            }
            SetCursor(CursorType.UI);
            return true; 
        }
        if(isDraggingUI){return true;}
        return false;
    }
     private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    actionStore.Use(i, gameObject);
                }
            }
        }
    private bool InteractWithComponent()
    {
        RaycastHit[] hits = RaycastAllSorted();
        foreach (RaycastHit hit in hits)
        {
            IRaycastable[] raycastables= hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if(raycastable.HandleRaycast(this))
                {
                    SetCursor(raycastable.GetCursorType());
                    return true;
                }
            }
        }
        return false;
    }
    

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits= Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i]=hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }
    
    
        private bool InteractWithMovement()
        {
        Vector3 target;
        bool hasHit=RaycastNavMesh(out target);
        if(hasHit)
        { 
            if(!GetComponent<Mover>().CanMoveTo(target)) return false;
            if(Input.GetMouseButton(0))
        {
            GetComponent<Mover>().StartMoveAction(target,1f);
        }
        SetCursor(CursorType.Movement);

        return true;
        }
        return false;

    }
     private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            //if no nav mesh you cant move
            UnityEngine.AI.NavMeshHit navMeshHit;
            bool hasCastToNavMesh = UnityEngine.AI.NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, UnityEngine.AI.NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;
            
            return true;
        }
      
     private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
    public static Ray GetMouseRay(){
        return Camera.main.ScreenPointToRay(Input.mousePosition);}
}

}