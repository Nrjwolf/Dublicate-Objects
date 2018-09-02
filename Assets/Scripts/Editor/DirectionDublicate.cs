using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DirectionDublicate : MonoBehaviour
{
    [MenuItem("Edit/DirectionDublicate/Dublicate Right %RIGHT")]
    private static void DublicateRight() { MoveGameObject(CreateClone(), 1, 0); }

    [MenuItem("Edit/DirectionDublicate/Dublicate Left %LEFT")]
    private static void DublicateLeft() { MoveGameObject(CreateClone(), -1, 0); }

    [MenuItem("Edit/DirectionDublicate/Dublicate Left %UP")]
    private static void DublicateUp() { MoveGameObject(CreateClone(), 0, 1); }

    [MenuItem("Edit/DirectionDublicate/Dublicate Left %DOWN")]
    private static void DublicateDown() { MoveGameObject(CreateClone(), 0, -1); }

    private static GameObject CreateClone()
    {
        var go = Selection.activeGameObject;
        if (go == null || go.GetComponent<SpriteRenderer>() == null) return null;

        GameObject clone = null;
        GameObject prefab = PrefabUtility.GetPrefabParent(go) as GameObject;
        if (prefab)
        {
            clone = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
                PrefabUtility.SetPropertyModifications(clone, PrefabUtility.GetPropertyModifications(go));
            clone.transform.position = go.transform.position;
            clone.transform.rotation = go.transform.rotation;
            clone.transform.SetParent(go.transform.parent);
        }
        else
        {
            clone = Instantiate(go, go.transform.position, go.transform.rotation, go.transform.parent);
        }

        clone.name = go.name;
        Selection.activeGameObject = clone;
        return clone;
    }

    private static void MoveGameObject(GameObject go, int x, int y)
    {
        if (go == null || go.GetComponent<SpriteRenderer>() == null) return;
        var spriteRenderer = go.GetComponent<SpriteRenderer>();

        go.transform.position += new Vector3(spriteRenderer.size.x * x, spriteRenderer.size.y * y);
    }
}