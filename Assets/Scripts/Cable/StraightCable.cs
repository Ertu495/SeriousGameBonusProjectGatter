using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StraightCable : MonoBehaviour
{
    private Transform start;
    private Transform end;
    public BasicSlot sourceSlot;

    [Range(2, 20)] public int resolution = 8; 
    [Range(0.05f, 0.5f)] public float width = 0.15f;
    public bool useBezier = true;
    [Range(0f, 1f)] public float curveHeight = 0.3f; 
    
    [Header("Animation")]
    public float flowSpeed = 2f; 
    public float pulseSpeed = 3f;
    [Range(0f, 0.5f)] public float pulseAmount = 0.05f;
    public bool glowOnSignal = true;

    private LineRenderer lr;
    private int lastValue = -999;
    private float anim = 0f;
    private bool animate = false;
    public bool isSolved = false;
    private float flowOffset = 0f;
    private MaterialPropertyBlock mpb;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        mpb = new MaterialPropertyBlock();
        
        lr.useWorldSpace = true;
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.View; 
        
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 1f);
        curve.AddKey(1f, 1f);
        lr.widthCurve = curve;
        lr.widthMultiplier = width;
    }

    public void StartWinAnimation()
    {
        animate = true;
        anim = 0f;
    }

    void AnimateWinCable()
    {
        anim += Time.deltaTime;
        float t = Mathf.Clamp01(anim);
        
        Color startColor = new Color(0.2f, 0.5f, 0.2f);
        Color endColor = Color.green;
        Color pulse = Color.Lerp(Color.white, Color.green, Mathf.PingPong(anim * 3f, 1f));
        
        Color current = Color.Lerp(startColor, endColor, t) * pulse;
        
        lr.startColor = current;
        lr.endColor = current;
        
        float victoryPulse = width + Mathf.Sin(anim * 10f) * 0.02f;
        lr.widthMultiplier = victoryPulse;
    }

    public void SetSolved()
    {
        isSolved = true;
    }

    public void Connect(Transform s, Transform e, BasicSlot slot)
    {
        start = s;
        end = e;
        sourceSlot = slot;
    }

    void Update()
    {
        if (start == null || end == null) return;

        if (useBezier)
        {
            DrawBezierCable();
        }
        else
        {
            DrawStraightCable();
        }

        AnimateFlow();

        if (isSolved)
        {
            AnimateWinCable();
            return;
        }

        int value = sourceSlot != null ? sourceSlot.output : -1;
        Color c = GetColor(value);
        
        if (glowOnSignal && value > 0)
        {
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * 0.2f;
            c *= pulse;
        }
        
        lr.startColor = c;
        lr.endColor = c;

        float widthPulse = width + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        lr.widthMultiplier = widthPulse;

        if (value != lastValue)
        {
            lastValue = value;
            RefreshSignal();
        }
    }

    void DrawStraightCable()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, start.position);
        lr.SetPosition(1, end.position);
    }

    void DrawBezierCable()
    {
        lr.positionCount = resolution;
        
        Vector3 p0 = start.position;
        Vector3 p2 = end.position;
        
        Vector3 mid = Vector3.Lerp(p0, p2, 0.5f);
        Vector3 direction = (p2 - p0).normalized;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward);
        Vector3 p1 = mid + perpendicular * curveHeight;

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            Vector3 point = QuadraticBezier(p0, p1, p2, t);
            
            point = PixelSnap(point, 0.0625f); 
            lr.SetPosition(i, point);
        }
    }

    Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        float u = 1f - t;
        return u * u * a + 2f * u * t * b + t * t * c;
    }

    Vector3 PixelSnap(Vector3 point, float pixelSize)
    {
        return new Vector3(
            Mathf.Round(point.x / pixelSize) * pixelSize,
            Mathf.Round(point.y / pixelSize) * pixelSize,
            Mathf.Round(point.z / pixelSize) * pixelSize
        );
    }

    void AnimateFlow()
    {
        flowOffset -= Time.deltaTime * flowSpeed;
        lr.material.mainTextureOffset = new Vector2(flowOffset, 0);
        
        if (isSolved)
        {
            lr.material.mainTextureOffset = new Vector2(flowOffset * 3f, 0);
        }
    }

    public void RefreshSignal()
    {
        int value = sourceSlot != null ? sourceSlot.output : -1;
        BasicSlot slot = end.GetComponentInParent<BasicSlot>();
        if (slot != null)
        {
            slot.ReceiveValue(this, value);
        }
    }

    Color GetColor(int v)
    {
        return v switch
        {
            -1 => new Color(0.8f, 0.2f, 0.2f),
            0 => new Color(0.3f, 0.3f, 0.3f),
            1 => new Color(1f, 0.8f, 0.2f),
            2 => new Color(0.2f, 0.9f, 0.3f), 
            _ => Color.magenta
        };
    }

    void OnDrawGizmos()
    {
        if (start != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(start.position, Vector3.one * 0.1f);
        }
        if (end != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(end.position, Vector3.one * 0.1f);
        }
    }
}