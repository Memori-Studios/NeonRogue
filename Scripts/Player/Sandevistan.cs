using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sandevistan : MonoBehaviour
{
    [Header("Graphics Transition")]
    public float transitionTime;
    public static Sandevistan instance;
    public Volume sandyVolume, mainVolume;
    public Camera mainCamera;

    [Header("Sandevistan Active")]

    [SerializeField] private float activeTime, meshRefreshRate = 0.1f, meshDestroyDelay = 3f;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderer;
    [SerializeField] private List<GameObject> spawnedMeshes;
    public Transform positionToSpawnAt;

    [Header ("Shader Related")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f, shaderVarRefreshRate = 0.05f;

    [Header("Color change")]
    [SerializeField] private Color[] mycolors;
    [SerializeField] private float lerpTime;
    int colorIndex = 0, len;
    float t = 0f;
    private Color sandyColor;
    public bool sandevistanActive;
    public bool OnCoolDown;
    public List<GameObject> sandevistanObjects;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        len = mycolors.Length;
        CreateCopiesPool();
    }

    // Update is called once per frame
    void Update()
    {
        sandyColor = Color.Lerp(sandyColor, mycolors[colorIndex], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1, lerpTime * Time.deltaTime);

        if(t > 0.9f)
        {
            t = 0;
            colorIndex++;
            colorIndex = (colorIndex == mycolors.Length) ? 0 : colorIndex;
        }
    }
    public IEnumerator ActivateSandy()
    {
        OnCoolDown = true;
        sandevistanActive = true;
        float timeActive = activeTime;

        PlayerManager.instance.uIManager.RechargeCyberUpgradeUI(CyberwareType.Sandevistan);
        StartCoroutine(ControlMainVolume(1, 0f, transitionTime));
        StartCoroutine(ControlSandyVolume(0, 1f, transitionTime));
        StartCoroutine(ControlCameraFOV(60f, 50f, transitionTime));

        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if(skinnedMeshRenderer==null)
                skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i = 0; i < skinnedMeshRenderer.Length; i++)
            {
                GameObject gObj = GetCopyFromPool();
                gObj.SetActive(true);
                gObj.transform.SetPositionAndRotation(positionToSpawnAt.position, positionToSpawnAt.rotation);

                MeshRenderer mr = gObj.GetComponent<MeshRenderer>();
                MeshFilter mf = gObj.GetComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderer[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;
                mr.material.color = sandyColor;
                spawnedMeshes.Add(gObj);

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));
            }
            yield return new WaitForSeconds(meshRefreshRate);
        }

        foreach(GameObject playerCopy in spawnedMeshes)
            playerCopy.SetActive(false);

        spawnedMeshes.Clear();

        sandevistanActive = false;
        StartCoroutine(ControlMainVolume(0, 1f, transitionTime));
        StartCoroutine(ControlSandyVolume(1, 0f, transitionTime));
        StartCoroutine(ControlCameraFOV(50, 60f, transitionTime));
    }
    private void CreateCopiesPool()
    {
        for(int i = 0; i < 50; i++)
        {
            GameObject playerCopy = new GameObject();
            playerCopy.name = "PlayerCopy";
            MeshRenderer mr = playerCopy.AddComponent<MeshRenderer>();
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            MeshFilter mf = playerCopy.AddComponent<MeshFilter>();
            playerCopy.SetActive(false);
            sandevistanObjects.Add(playerCopy);
        }
    }
    public GameObject GetCopyFromPool()
    {
        foreach(GameObject playerCopy in sandevistanObjects)
        {
            if (!playerCopy.activeInHierarchy)
                return playerCopy;
        }
        return null;
    }
    
    private IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float rerfreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);
        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(rerfreshRate);
        }
    }
    private void SetColor(MeshRenderer mr, Color color)
    {
        mr.material.color = color;
    }
    private Color CurrentColor()
    {
        return Color.Lerp(mycolors[0], mycolors[1], t);
    }
    private IEnumerator ControlSandyVolume(float startValue, float endValue, float lerpDuration)
    {
        float currentLerpTime = 0f, lerpValue = 0f;
  
        while (sandyVolume.weight != endValue)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime < lerpDuration)
            {
                lerpValue = Mathf.Lerp(startValue, endValue, currentLerpTime / lerpDuration);
            }
            else
            {
                lerpValue = endValue;
            }

            sandyVolume.weight = lerpValue;
            yield return null;
        }
    }
    private IEnumerator ControlMainVolume(float startValue, float endValue, float lerpDuration)
    {
        float currentLerpTime = 0f, lerpValue = 0f;
  
        while (mainVolume.weight != endValue)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime < lerpDuration)
            {
                lerpValue = Mathf.Lerp(startValue, endValue, currentLerpTime / lerpDuration);
            }
            else
            {
                lerpValue = endValue;
            }

            mainVolume.weight = lerpValue;
            yield return null;
        }
    }
    private IEnumerator ControlCameraFOV(float startValue, float endValue, float lerpDuration)
    {
        float currentLerpTime = 0f, lerpValue = 0f;
  
        while (mainCamera.fieldOfView != endValue)
        {
            currentLerpTime += Time.deltaTime;
            
            if (currentLerpTime < lerpDuration)
            {
                lerpValue = Mathf.Lerp(startValue, endValue, currentLerpTime / lerpDuration);
            }
            else
            {
                lerpValue = endValue;
            }

            mainCamera.fieldOfView = lerpValue;
            yield return null;
        }
    }
}
