//using UnityEngine;
//using System.Collections.Generic;

//public class WormController : MonoBehaviour
//{
//    public Transform head; // Transform của đầu sâu
//    public GameObject bodySegmentPrefab; // Prefab của đoạn thân
//    public float gridSize = 1f; // Kích thước ô lưới (mặc định 1x1)

//    private List<Transform> bodySegments = new List<Transform>(); // Danh sách các đoạn thân
//    private Vector2 direction = Vector2.right; // Hướng di chuyển mặc định
//    private Vector2 lastHeadPosition; // Vị trí trước đó của đầu
//    private bool canMove = true; // Biến kiểm soát xem có thể di chuyển không

//    void Start()
//    {
//        // Kiểm tra xem head và bodySegmentPrefab đã được gán chưa
//        if (head == null || bodySegmentPrefab == null)
//        {
//            Debug.LogError("Head hoặc BodySegmentPrefab chưa được gán trong Inspector!");
//            return;
//        }

//        // Khởi tạo danh sách với đầu sâu
//        bodySegments.Add(head);

//        // Thêm một đoạn thân ban đầu
//        AddBodySegment();
//    }

//    void Update()
//    {
//        // Chỉ xử lý di chuyển bằng phím nếu được phép
//        if (!canMove) return;

//        // Xử lý input từ bàn phím
//        Vector2 newDirection = direction;
//        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
//            newDirection = Vector2.up;
//        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
//            newDirection = Vector2.down;
//        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
//            newDirection = Vector2.left;
//        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
//            newDirection = Vector2.right;

//        // Nếu hướng thay đổi, di chuyển rắn
//        if (newDirection != direction || Input.GetKeyDown(KeyCode.UpArrow) ||
//            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
//            Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            direction = newDirection;
//            Move();
//        }
//    }

//    // Hàm công khai để nút Up gọi
//    public void MoveUp()
//    {
//        if (canMove && direction != Vector2.down)
//        {
//            direction = Vector2.up;
//            Move();
//        }
//    }

//    // Hàm công khai để nút Down gọi
//    public void MoveDown()
//    {
//        if (canMove && direction != Vector2.up)
//        {
//            direction = Vector2.down;
//            Move();
//        }
//    }

//    // Hàm công khai để nút Left gọi
//    public void MoveLeft()
//    {
//        if (canMove && direction != Vector2.right)
//        {
//            direction = Vector2.left;
//            Move();
//        }
//    }

//    // Hàm công khai để nút Right gọi
//    public void MoveRight()
//    {
//        if (canMove && direction != Vector2.left)
//        {
//            direction = Vector2.right;
//            Move();
//        }
//    }

//    void Move()
//    {
//        // Lưu vị trí hiện tại của đầu
//        lastHeadPosition = head.position;

//        // Tính vị trí mới của đầu theo hướng
//        Vector2 newPosition = (Vector2)head.position + direction * gridSize;

//        // Căn chỉnh vị trí theo lưới
//        newPosition.x = Mathf.Round(newPosition.x / gridSize) * gridSize;
//        newPosition.y = Mathf.Round(newPosition.y / gridSize) * gridSize;

//        // Di chuyển đầu
//        head.position = newPosition;

//        // Xoay đầu theo hướng di chuyển
//        if (direction == Vector2.up)
//            head.rotation = Quaternion.Euler(0, 0, 90); // Quay lên
//        else if (direction == Vector2.down)
//            head.rotation = Quaternion.Euler(0, 0, -90); // Quay xuống
//        else if (direction == Vector2.left)
//            head.rotation = Quaternion.Euler(0, 0, 180); // Quay trái
//        else if (direction == Vector2.right)
//            head.rotation = Quaternion.Euler(0, 0, 0); // Quay phải

//        // Cập nhật vị trí các đoạn thân
//        for (int i = bodySegments.Count - 1; i > 0; i--)
//        {
//            bodySegments[i].position = bodySegments[i - 1].position;
//        }
//    }

//    void AddBodySegment()
//    {
//        // Lấy vị trí của đoạn thân cuối cùng
//        Vector2 lastSegmentPos = bodySegments[bodySegments.Count - 1].position;

//        // Tạo đoạn thân mới
//        GameObject newSegment = Instantiate(bodySegmentPrefab, lastSegmentPos, Quaternion.identity);
//        newSegment.transform.parent = transform; // Đặt parent là Worm
//        bodySegments.Add(newSegment.transform); // Thêm vào danh sách
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Apple") || other.CompareTag("Banana"))
//        {
//            // Ăn táo hoặc chuối, thêm đoạn thân
//            AddBodySegment();
//            Destroy(other.gameObject); // Xóa táo hoặc chuối
//        }
//        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
//        {
//            // Game over nếu đụng tường hoặc chính mình
//            Debug.Log("Game Over!");
//            canMove = false; // Ngăn di chuyển tiếp
//            // TODO: Thêm logic reset game
//        }
//        else if (other.CompareTag("Exit"))
//        {
//            // Qua màn
//            Debug.Log("Level Complete!");
//            canMove = false; // Ngăn di chuyển tiếp
//            // TODO: Thêm logic chuyển màn
//        }
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class WormController : MonoBehaviour
{
    public Transform head; // Transform của đầu sâu
    public GameObject bodySegmentPrefab; // Prefab của đoạn thân
    public float gridSize = 1f; // Kích thước ô lưới (mặc định 1x1)
    public int maxUndoSteps = 10; // Giới hạn số bước hoàn tác

    private List<Transform> bodySegments = new List<Transform>(); // Danh sách các đoạn thân
    private Vector2 direction = Vector2.right; // Hướng di chuyển mặc định
    private Vector2 lastHeadPosition; // Vị trí trước đó của đầu
    private bool canMove = true; // Biến kiểm soát xem có thể di chuyển không

    // Lớp lưu trạng thái của rắn
    [System.Serializable]
    private class WormState
    {
        public Vector2 headPosition;
        public Quaternion headRotation;
        public List<Vector2> bodyPositions;
        public Vector2 direction;

        public WormState(Vector2 headPos, Quaternion headRot, List<Transform> segments, Vector2 dir)
        {
            headPosition = headPos;
            headRotation = headRot;
            bodyPositions = new List<Vector2>();
            foreach (Transform segment in segments)
            {
                if (segment != null)
                    bodyPositions.Add(segment.position);
            }
            direction = dir;
        }
    }

    private List<WormState> stateHistory = new List<WormState>(); // Lịch sử trạng thái

    void Start()
    {
        // Kiểm tra xem head và bodySegmentPrefab đã được gán chưa
        if (head == null || bodySegmentPrefab == null)
        {
            Debug.LogError("Head hoặc BodySegmentPrefab chưa được gán trong Inspector!");
            return;
        }

        // Khởi tạo danh sách với đầu sâu
        bodySegments.Add(head);

        // Thêm một đoạn thân ban đầu
        AddBodySegment();

        // Lưu trạng thái ban đầu
        SaveState();
    }

    void Update()
    {
        // Chỉ xử lý di chuyển bằng phím nếu được phép
        if (!canMove) return;

        // Xử lý input từ bàn phím
        Vector2 newDirection = direction;
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            newDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            newDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            newDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            newDirection = Vector2.right;

        // Nếu hướng thay đổi, di chuyển rắn
        if (newDirection != direction || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = newDirection;
            Move();
        }

        // Xử lý Ctrl + Z để hoàn tác
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
    }

    // Hàm công khai để nút Up gọi
    public void MoveUp()
    {
        if (canMove && direction != Vector2.down)
        {
            direction = Vector2.up;
            Move();
        }
    }

    // Hàm công khai để nút Down gọi
    public void MoveDown()
    {
        if (canMove && direction != Vector2.up)
        {
            direction = Vector2.down;
            Move();
        }
    }

    // Hàm công khai để nút Left gọi
    public void MoveLeft()
    {
        if (canMove && direction != Vector2.right)
        {
            direction = Vector2.left;
            Move();
        }
    }

    // Hàm công khai để nút Right gọi
    public void MoveRight()
    {
        if (canMove && direction != Vector2.left)
        {
            direction = Vector2.right;
            Move();
        }
    }

    // Hàm công khai để nút Undo gọi
    public void Undo()
    {
        if (!canMove || stateHistory.Count <= 1) return; // Không thể hoàn tác nếu không đủ trạng thái

        // Xóa trạng thái hiện tại
        stateHistory.RemoveAt(stateHistory.Count - 1);

        // Lấy trạng thái trước đó
        WormState previousState = stateHistory[stateHistory.Count - 1];

        // Khôi phục trạng thái
        head.position = previousState.headPosition;
        head.rotation = previousState.headRotation;
        direction = previousState.direction;

        // Cập nhật số lượng đoạn thân
        while (bodySegments.Count - 1 > previousState.bodyPositions.Count)
        {
            // Xóa đoạn thân thừa
            Transform lastSegment = bodySegments[bodySegments.Count - 1];
            bodySegments.RemoveAt(bodySegments.Count - 1);
            Destroy(lastSegment.gameObject);
        }

        // Khôi phục vị trí các đoạn thân
        for (int i = 1; i < bodySegments.Count; i++)
        {
            bodySegments[i].position = previousState.bodyPositions[i - 1];
        }

        Debug.Log("Undo completed!");
    }

    void Move()
    {
        // Lưu trạng thái trước khi di chuyển
        SaveState();

        // Lưu vị trí hiện tại của đầu
        lastHeadPosition = head.position;

        // Tính vị trí mới của đầu theo hướng
        Vector2 newPosition = (Vector2)head.position + direction * gridSize;

        // Căn chỉnh vị trí theo lưới
        newPosition.x = Mathf.Round(newPosition.x / gridSize) * gridSize;
        newPosition.y = Mathf.Round(newPosition.y / gridSize) * gridSize;

        // Di chuyển đầu
        head.position = newPosition;

        // Xoay đầu theo hướng di chuyển
        if (direction == Vector2.up)
            head.rotation = Quaternion.Euler(0, 0, 90); // Quay lên
        else if (direction == Vector2.down)
            head.rotation = Quaternion.Euler(0, 0, -90); // Quay xuống
        else if (direction == Vector2.left)
            head.rotation = Quaternion.Euler(0, 0, 180); // Quay trái
        else if (direction == Vector2.right)
            head.rotation = Quaternion.Euler(0, 0, 0); // Quay phải

        // Cập nhật vị trí các đoạn thân
        for (int i = bodySegments.Count - 1; i > 0; i--)
        {
            bodySegments[i].position = bodySegments[i - 1].position;
        }
    }

    void SaveState()
    {
        // Lưu trạng thái hiện tại
        WormState currentState = new WormState(head.position, head.rotation, bodySegments, direction);
        stateHistory.Add(currentState);

        // Giới hạn số lượng trạng thái
        if (stateHistory.Count > maxUndoSteps)
        {
            stateHistory.RemoveAt(0);
        }
    }

    void AddBodySegment()
    {
        // Lấy vị trí của đoạn thân cuối cùng
        Vector2 lastSegmentPos = bodySegments[bodySegments.Count - 1].position;

        // Tạo đoạn thân mới
        GameObject newSegment = Instantiate(bodySegmentPrefab, lastSegmentPos, Quaternion.identity);
        newSegment.transform.parent = transform; // Đặt parent là Worm
        bodySegments.Add(newSegment.transform); // Thêm vào danh sách

        // Lưu trạng thái sau khi thêm đoạn thân
        SaveState();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple") || other.CompareTag("Banana"))
        {
            // Ăn táo hoặc chuối, thêm đoạn thân
            AddBodySegment();
            Destroy(other.gameObject); // Xóa táo hoặc chuối
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            // Game over nếu đụng tường hoặc chính mình
            Debug.Log("Game Over!");
            canMove = false; // Ngăn di chuyển tiếp
            // TODO: Thêm logic reset game
        }
        else if (other.CompareTag("Exit"))
        {
            // Qua màn
            Debug.Log("Level Complete!");
            canMove = false; // Ngăn di chuyển tiếp
            // TODO: Thêm logic chuyển màn
        }
    }
}