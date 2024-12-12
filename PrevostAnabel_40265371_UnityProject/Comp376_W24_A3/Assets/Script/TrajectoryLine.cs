using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lineRenderer;

    //to smeeth the trajectory line
    public int lineSegments = 60;
    public float timeOfFlight = 5.0f;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity)
    {
        float timeStep = timeOfFlight / lineSegments;

        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);

        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRenderedPoints = new Vector3[lineSegments];

        lineRenderedPoints[0] = startPoint;

        for(int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startPoint + progressBeforeGravity - gravityOffset;
            lineRenderedPoints[i] = newPosition;
        }

        return lineRenderedPoints;
    }
}
