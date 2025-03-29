using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
public class BarrelsManager : MonoBehaviourSingleton<BarrelsManager>
{
   public static List<ExplosiveBarrel> barrels = new List<ExplosiveBarrel>();
   public ExplosiveBarrel barrelBlueprint;


   public ExplosiveBarrel CreateBarrel(string barrelName, Vector3 position )
   {
      ExplosiveBarrel barrel = Instantiate<ExplosiveBarrel>(barrelBlueprint);
      barrel.transform.position = position;
      return barrel;
   }
   
   public void DropBarrel(Transform barrel)
   {
      if (barrel == null)
         return;
      
      Physics.Raycast(barrel.position, -Vector3.up, out RaycastHit hit);
      barrel.transform.position = hit.point + Vector3.up*0.5f;
   }

   public void DropAllBarrels()
   {
      foreach (ExplosiveBarrel barrel in barrels)
      {
         DropBarrel(barrel.transform);
      }
   }
   
   public void OnDrawGizmos()
   {
      GUIStyle style = new GUIStyle();
      style.normal.textColor = Color.white;
      style.fontSize = 22;
      Handles.Label(transform.position+Vector3.up, $"Explosive Barrels {barrels.Count}", style);
      Gizmos.DrawIcon(transform.position, "Assets/Textures/Icons/bomb.png");
   }

   public void OnDrawGizmosSelected()
   {
      foreach (ExplosiveBarrel barrel in barrels)
      {
         Handles.DrawBezier( transform.position, 
                              barrel.transform.position, 
                              transform.position+Vector3.up*-1, 
                              barrel.transform.position+Vector3.up*3,
                              Color.white, 
                              EditorGUIUtility.whiteTexture, 
                              2f);
      }
   }
}
