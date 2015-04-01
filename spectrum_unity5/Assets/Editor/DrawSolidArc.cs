using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SolidArcExample))]
public class DrawSolidArc : Editor
{
	
	void OnSceneGUI()
	{
		SolidArcExample myTarget = (SolidArcExample)target;
		
		Handles.color = new Color(1f, 1f, 1f, 0.2f);
		
		Handles.DrawSolidArc(myTarget.transform.position,
		                     myTarget.transform.up,
		                     -myTarget.transform.right,
		                     180,
		                     myTarget.shieldArea);
		
		Handles.color = Color.white;
		
		myTarget.shieldArea = Handles.ScaleValueHandle(myTarget.shieldArea,
		                                               myTarget.transform.position + myTarget.transform.forward * myTarget.shieldArea,
		                                               myTarget.transform.rotation,
		                                               1f,
		                                               Handles.ConeCap,
		                                               1f);
	}
}