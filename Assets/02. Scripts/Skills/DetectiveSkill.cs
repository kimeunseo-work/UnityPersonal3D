using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/Detective")]
public class DetectiveSkill : InstantCastSKill
{
    private Plane[] _frustumPlanes = new Plane[6];
    [SerializeField] private LayerMask detectableLayerMask;

    public override IEnumerator Activate()
    {
        _frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // 검색 범위 제한
        var colliders = Physics.OverlapSphere(
            Camera.main.transform.position,
            50f
        );

        foreach (var col in colliders)
        {
            // 1. 레이어 필터
            if (((1 << col.gameObject.layer) & detectableLayerMask) == 0)
                continue;

            // 2. 형제 구조 (Collider = 자식1, IDetectable = 자식2)
            var parent = col.transform.parent;
            if (parent == null) continue;

            var detectable = parent.GetComponentInChildren<IDetectable>();
            if (detectable == null) continue;

            // 3. 카메라 시야 내 여부
            var bounds = col.bounds;
            if (GeometryUtility.TestPlanesAABB(_frustumPlanes, bounds))
            {
                detectable.Detected();
            }
        }

        yield break;
    }
}