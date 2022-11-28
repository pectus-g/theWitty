using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class NL_TimeOfDayController : MonoBehaviour {

    public float transitionSpeed = 0.1f;

    [Header("LIGHTING")]

    public Light mainLight;

    [Space(5)]
    public float startLightIntensity = 1.3f;
    public float targetLightIntensity = 5;

    [Space(5)]
    [ColorUsage(false, false)]
    public Color startLightColor = new Color(0.24f, 0.73f, 1);
    [ColorUsage(false, false)]
    public Color targetLightColor = new Color(0.99f, 0.455f, 0.13f, 1);

    [Space(5)]
    public Vector3 startLightRotation = new Vector3(23, 206, 20);
    public Vector3 targetLightRotation = new Vector3(12, 280, 20);

    [Space(5)]
    [ColorUsage(false, true)]
    public Color startEquatorColor = new Color32(31, 30, 41, 0);
    [ColorUsage(false, true)]
    public Color targetEquatorColor = new Color32(36, 26, 17, 0);

    [Header("FOG")]
    [ColorUsage(false, false)]
    public Color startFogColor = new Color(0.183f, 0.71f, 1);
    [Tooltip("The color the fog will lerp to.")]
    [ColorUsage(false, false)]
    public Color targetFogColor = new Color(1, 0.578f, 0.193f, 1);

    [Space(5)]
    public float startFogDistance = 100;
    [Tooltip("The distance the fog will lerp to.")]
    public float targetFogDistance = 130;

    [Header("SKYBOX")]
    [ColorUsage(false, false)]
    public Color startSkyColor = new Color(0.05f, 0.07f, 0.09f);
    [Tooltip("The color of the skybox material which set in the Ligthing window.")]
    [ColorUsage(false, false)]
    public Color targetSkyColor = new Color(0.94f, 0.81f, 0.75f, 1);

    [Space(5)]
    public float startSkyExposure = 1;
    public float targetSkyExposure = 1;

    private float lerpValue = 0;

    private float currentFogDistance;
    private Color currentFogColor;
    private Vector4 currentEquatorColor;
    private float currentLightIntensity;
    private Color currentLightColor;
    private Vector3 currentLightRotation;
    private Color currentSkyColor;
    private float currentSkyExposure;

    private float tempFogDistance;
    private Color tempFogColor;
    private Color tempEquatorColor;
    private float tempLightIntensity;
    private Color tempLightColor;
    private Vector3 tempLightRotation;
    private Color tempSkyColor;
    private float tempSkyExposure;

    private int skyExpPropertyID = -1;
    private Material skyMaterial;

    void Awake () {

        skyMaterial = RenderSettings.skybox;
        if (skyMaterial != null)
        {
            skyExpPropertyID = Shader.PropertyToID("_Exposure");

            if (skyExpPropertyID != -1) startSkyExposure = skyMaterial.GetFloat(skyExpPropertyID);
            startSkyColor = skyMaterial.color;
        }
	}

#if UNITY_EDITOR
    private void OnDisable()
    {   
        //reset the sky material values after exiting playmode in editor
        if (!Application.isPlaying) return;
        if (lerpValue < 1)
        {
            if (skyExpPropertyID != -1) skyMaterial.SetFloat(skyExpPropertyID, startSkyExposure);
            skyMaterial.color = startSkyColor;
        }
        else
        {
            if (skyExpPropertyID != -1) skyMaterial.SetFloat(skyExpPropertyID, targetSkyExposure);
            skyMaterial.color = targetSkyColor;
        }
    }
#endif

    public void SwapValues()
    {
        //save temp variables
        tempFogDistance = startFogDistance;
        tempFogColor = startFogColor;
        tempEquatorColor = startEquatorColor;

        //assign first variables
        startFogDistance = targetFogDistance;
        startFogColor = targetFogColor;
        startEquatorColor = targetEquatorColor;

        //assign second variables from temp
        targetFogDistance = tempFogDistance;
        targetFogColor = tempFogColor;
        targetEquatorColor = tempEquatorColor;

        if (mainLight != null)
        {
            //save temp variables
            tempLightIntensity = startLightIntensity;
            tempLightColor = startLightColor;
            tempLightRotation = startLightRotation;

            //assign first variables
            startLightIntensity = targetLightIntensity;
            startLightColor = targetLightColor;
            startLightRotation = targetLightRotation;

            //assign second variables from temp 
            targetLightIntensity = tempLightIntensity;
            targetLightColor = tempLightColor;
            targetLightRotation = tempLightRotation;
        }

        if (skyMaterial != null)
        {
            //save temp variables
            tempSkyColor = startSkyColor;

            //assign first variables
            startSkyColor = targetSkyColor;

            //assign second variables from temp
            targetSkyColor = tempSkyColor;

            if (skyExpPropertyID != -1)
            {
                //save temp variables
                tempSkyExposure = startSkyExposure;

                //assign first variables
                startSkyExposure = targetSkyExposure;

                //assign second variables from temp
                targetSkyExposure = tempSkyExposure;
            }
        }
    }

    public void UpdateAll(bool instantChange = false, float lerpValueOverride = -1)
    {
        if (lerpValueOverride != -1) lerpValue = lerpValueOverride;

        float newFogDistance = Mathf.Lerp(startFogDistance, targetFogDistance, lerpValue);
        Color newFogColor = Color.Lerp(startFogColor, targetFogColor, lerpValue);
        Vector4 newequatorColor = Color.Lerp(startEquatorColor, targetEquatorColor, lerpValue);

        if (mainLight != null)
        {
            float newLightIntensity = Mathf.Lerp(startLightIntensity, targetLightIntensity, lerpValue);
            Color newLightColor = Color.Lerp(startLightColor, targetLightColor, lerpValue);
            Vector3 newLightRotation = Vector3.Slerp(startLightRotation, targetLightRotation, lerpValue);

            mainLight.intensity = newLightIntensity;
            mainLight.color = newLightColor;
            mainLight.transform.eulerAngles = newLightRotation;
        }

        if(skyMaterial != null)
        {
            Color newSkyColor = Color.Lerp(startSkyColor, targetSkyColor, lerpValue);
            skyMaterial.color = newSkyColor;
            if(skyExpPropertyID != -1)
            {
                float newSkyExposure = Mathf.Lerp(startSkyExposure, targetSkyExposure, lerpValue);
                skyMaterial.SetFloat(skyExpPropertyID, newSkyExposure);
            }
        }

        RenderSettings.fogColor = newFogColor;
        RenderSettings.fogEndDistance = newFogDistance;
        RenderSettings.ambientEquatorColor = newequatorColor;
    }

    IEnumerator FogFade()
    {
        while (true)
        {
            if (lerpValue < 1) {
                lerpValue += transitionSpeed * Time.deltaTime;

                UpdateAll();
            }
            else
            {
                SwapValues();
                StopCoroutine("FogFade");
            }
            yield return null;
        }
    }

    public void StartFogFade()
    {
        StopCoroutine("FogFade");

        lerpValue = 0;

        StartCoroutine("FogFade");
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(NL_TimeOfDayController))]
public class NL_TimeOfDay_editor : Editor
{
    private NL_TimeOfDayController timeOfDayController;
    private void OnEnable()
    {
        timeOfDayController = (NL_TimeOfDayController)target;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        EditorGUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Start <-> Target", "Swap the start and target values for all the properties")))
        {
            timeOfDayController.SwapValues();
        }

        if (EditorGUI.EndChangeCheck())
        {
            timeOfDayController.UpdateAll(true, 0);
        }
    }
}
#endif
