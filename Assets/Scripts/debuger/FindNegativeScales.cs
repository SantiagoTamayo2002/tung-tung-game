using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNegativeScales : MonoBehaviour
{
    [ContextMenu("Fix Negative Scales")]
    void FixScales()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Transform t = obj.transform;
            Vector3 scale = t.localScale;
            Vector3 newScale = scale;

            // Corrige cada eje si es negativo
            if (scale.x < 0) newScale.x = -scale.x;
            if (scale.y < 0) newScale.y = -scale.y;
            if (scale.z < 0) newScale.z = -scale.z;

            if (newScale != scale)
            {
                t.localScale = newScale;

                // Para mantener la orientación, se rota 180° en el eje correspondiente
                Vector3 rotation = t.localEulerAngles;

                if (scale.x < 0) rotation.y += 180f;
                if (scale.y < 0) rotation.z += 180f;
                if (scale.z < 0) rotation.x += 180f;

                t.localEulerAngles = rotation;

                Debug.Log($"Escala corregida en: {obj.name} (Nueva escala: {newScale})", obj);
            }
        }

        Debug.Log("✔ Todas las escalas negativas fueron corregidas.");
    }
}
