using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public Transform target; // Object cần Undo (ví dụ: nhân vật)

    private Stack<Vector3> positionHistory = new Stack<Vector3>();

    void Update()
    {
        // Nhấn phím mũi tên để di chuyển và lưu vị trí
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SavePosition();
            target.position += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SavePosition();
            target.position += Vector3.left;
        }

        // Nhấn Ctrl+Z (giả lập bằng Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
    }

    void SavePosition()
    {
        positionHistory.Push(target.position);
    }

    void Undo()
    {
        if (positionHistory.Count > 0)
        {
            Vector3 lastPosition = positionHistory.Pop();
            target.position = lastPosition;
        }
    }
    public void OnUndoButtonClick()
    {
        Undo();
    }

}
