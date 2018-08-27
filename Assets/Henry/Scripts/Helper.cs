using System.Collections;
using UnityEngine;

/// <summary>
/// Helper Functions
/// </summary>
public static class Helper
{
    public delegate void Callback();

    /// <summary>
	/// Get the direction to aim the initial point to final point.
	/// </summary>
	/// <param name="initialPoint">Initial point.</param>
	/// <param name="finalPoint">Final point.</param>
	/// <returns>An unitary vector pointing to the final point.</returns>
	public static Vector2 Aim(Vector2 ini,Vector2 fin)
	{
        Vector3 aim, dir;
        
        dir = fin - ini;

        if (dir.magnitude == 0)
            aim = fin;
        else
            aim = dir;

        //aim.Normalize();

		return aim/dir.magnitude;
	}

    public static IEnumerator MoveRoutine(GameObject obj, Vector3 position, float time, Callback callback)
    {
        float elapsedTime;
        Vector3 initialPos;

        elapsedTime = 0;
        initialPos = obj.transform.position;
        while (elapsedTime < time)
        {
            obj.transform.position = Vector3.Lerp(initialPos, position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        obj.transform.position = position;

        if (callback != null)
            callback();
    }

}
