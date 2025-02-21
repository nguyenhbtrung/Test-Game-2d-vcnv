using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class HitboxBehaviorTests : InputTestFixture
{
    private List<Vector2> testPositions = new List<Vector2>
    {
        new Vector2(-1.81f, -0.35f),
        new Vector3(-1.08200002f,-0.186000004f,0),
        new Vector3(-2.00900006f,0.759000003f,0),
        new Vector3(-0.782999992f,0.280999988f,0),
        new Vector3(-0.419999987f,0.566999972f,0),
        new Vector3(-0.404000014f,1.17700005f,0),
        new Vector3(-0.228f,-0.51700002f,0),
        new Vector3(0.120999999f,0.263999999f,0),
        new Vector3(1.02999997f,0.860000014f,0),
        new Vector3(1.38999999f,-0.25f,0),
        new Vector3(1.83000004f,0.409999996f,0),
        new Vector3(0.939999998f,-0.419999987f,0),
        new Vector3(0.699999988f,-0.74000001f,0),
        new Vector3(1.98000002f,-0.75f,0),
        new Vector3(2.88000011f,-0.741999984f,0),
        new Vector3(2.88000011f,0.515999973f,0),
        new Vector3(3.34899998f,0.388999999f,0),
        new Vector3(3.84100008f,-0.421000004f,0),
        new Vector3(4.09399986f,-0.559000015f,0),
        new Vector3(4.80299997f,-0.745000005f,0),
        new Vector3(5.59100008f,-0.248999998f,0),
        new Vector3(6.02299976f,0.221000001f,0),
        new Vector3(6.61399984f,-1.00699997f,0),
        new Vector3(7.03100014f,-0.247999996f,0),
        new Vector3(8.11600018f,-0.114f,0),
        new Vector3(9.11499977f,0.335999995f,0),
        new Vector3(11.3479996f,0.72299999f,0),
        new Vector3(12.8570004f,-0.56099999f,0),
        new Vector3(16.9500008f,-0.270000011f,0),
        new Vector3(17.8959999f,-0.778999984f,0),
        new Vector3(18.2520008f,0.0450000018f,0),
        new Vector3(19.1949997f,-0.437000006f,0),
        new Vector3(17.507f,-0.828999996f,0),
    };

    // Layer mask của nền tảng (đảm bảo rằng các nền tảng của bạn được gán layer phù hợp)
    private LayerMask platformLayer;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(SceneTestSettings.SceneIndex);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestPlayerStandingOnPlatform()
    {
        string logFileName = "TestPlayerStandingOnPlatform.txt";
        TestLogger.ClearLog(logFileName);
        List<string> issues = new List<string>();

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Không tìm thấy Player");

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        platformLayer = LayerMask.GetMask("Ground");


        foreach (Vector2 pos in testPositions)
        {

            player.transform.position = pos;

            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.5f);

            float footOffset = 0.15f; 
            Vector2 footPosition = (Vector2)player.transform.position + Vector2.down * footOffset;

            Collider2D hitCollider = Physics2D.OverlapCircle(footPosition, 0.1f, platformLayer);
            bool isStandingOnPlatform = hitCollider != null 
                && Mathf.Abs(player.transform.position.y - pos.y) < 0.15f 
                && rb.velocity == Vector2.zero;
            if (!isStandingOnPlatform) issues.Add($"Player tại vị trí {pos} không đứng trên nền tảng.");

            yield return null;
        }

        if (issues.Count > 0)
        {
            string errorMessage = "TestPlayerStandingOnPlatform fail :\n" + string.Join("\n", issues);
            TestLogger.Log(errorMessage, logFileName);
            Assert.Fail(errorMessage);
        }
        else
        {
            string successMessage = "TestPlayerStandingOnPlatform Pass.";
            TestLogger.Log(successMessage, logFileName);
            Assert.Pass(successMessage);
        }
    }

    [UnityTest]
    public IEnumerator TestPlayerCollisionWithTrap()
    {
        yield return new WaitForSeconds(0.1f);

        string logFileName = "TestPlayerCollisionWithTrap.txt";
        TestLogger.ClearLog(logFileName);

        var traps = GameObject.FindObjectsOfType<Trap>();
        Assert.IsNotNull(traps, "Không tìm thấy đối tượng nào có script Trap!");
        Assert.IsTrue(traps.Length > 0, "Không tìm thấy Trap nào trong scene!");

        List<Vector3> trapPositions = new List<Vector3>();
        foreach (var trap in traps)
        {
            trapPositions.Add(trap.transform.position);
        }
        

        foreach (var pos in trapPositions)
        {
            SceneManager.LoadScene(SceneTestSettings.SceneIndex);
            yield return null;

            GameController gameController = GameObject.FindObjectOfType<GameController>();
            
            Assert.IsNotNull(gameController, "Không tìm thấy GameController trong scene!");

            GameObject player = GameObject.Find("Player");
            Assert.IsNotNull(player, "Không tìm thấy Player");

            player.transform.position = pos;

            yield return new WaitForFixedUpdate();
            gameController.StopAllCoroutines();
            Time.timeScale = 1f;
            yield return new WaitForSeconds(0.05f);

            FieldInfo isGameOverField = typeof(GameController).GetField("isGameOver", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(isGameOverField, "Không tìm thấy trường isGameOver trong GameController!");

            bool isGameOverValue = (bool)isGameOverField.GetValue(gameController);
            Assert.IsTrue(isGameOverValue, "GameOver chưa được kích hoạt");
            yield return null;
        }
        Assert.Pass("TestPlayerCollisionWithTrap pass.");

    }

    [UnityTest]
    public IEnumerator TestPlayerCollisionFinishPoint()
    {
        yield return new WaitForSeconds(0.1f);

        string logFileName = "TestPlayerCollisionFinishPoint.txt";
        TestLogger.ClearLog(logFileName);

        var finishPoint = GameObject.FindObjectOfType<FinishPoint>();
        Assert.IsNotNull(finishPoint, "Không tìm thấy đối tượng nào có script FinishPoint!");

        GameController gameController = GameObject.FindObjectOfType<GameController>();
        Assert.IsNotNull(gameController, "Không tìm thấy GameController trong scene!");

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Không tìm thấy Player");

        player.transform.position = finishPoint.transform.position;

        yield return new WaitForFixedUpdate();
        gameController.StopAllCoroutines();
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.05f);

        FieldInfo isYouWinField = typeof(GameController).GetField("isYouWin", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(isYouWinField, "Không tìm thấy trường isYouWin trong GameController!");

        bool isYouWinValue = (bool)isYouWinField.GetValue(gameController);
        Assert.IsTrue(isYouWinValue, "isYouWin chưa được kích hoạt");
        Assert.Pass("TestPlayerCollisionFinishPoint pass.");
    }

    [UnityTest]
    public IEnumerator TestPlayerFallOffCornerPlatform()
    {
        Vector3[] testPos =
        {
            new Vector3(-0.73f, 1.163f,0),
            new Vector3(-0.084f, 1.16f,0)

        };

        string logFileName = "TestPlayerFallOffCornerPlatform.txt";
        TestLogger.ClearLog(logFileName);

        SceneManager.LoadScene(6, LoadSceneMode.Additive);
        yield return null;

        UnityEngine.SceneManagement.Scene testScene = SceneManager.GetSceneByBuildIndex(6);
        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Không tìm thấy Player");

        SceneManager.MoveGameObjectToScene(player, testScene);


        foreach (var pos in testPos)
        {
            player.transform.position = pos;
            yield return new WaitForSeconds(0.5f);

            Assert.LessOrEqual(player.transform.position.y, 0.879f, "Player không bị rơi khỏi góc của platform");
        }
        Assert.Pass("TestPlayerFallOffCornerPlatform pass.");

    }

    [UnityTest]
    public IEnumerator TestIsGroundedBeforeAndAfterJump()
    {
        yield return new WaitForSeconds(0.1f);

        string logFileName = "TestIsGroundedBeforeAndAfterJump.txt";
        TestLogger.ClearLog(logFileName);

        // Tìm đối tượng Player trong scene (giả sử tên là "Player")
        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Không tìm thấy đối tượng Player trong scene!");

        // Lấy component chứa logic kiểm tra ground (giả sử là PlayerController)
        var playerController = player.GetComponent<dichuyen>();
        Assert.IsNotNull(playerController, "Không tìm thấy PlayerController trên Player!");

        // Đợi 1 frame để Update() chạy và cập nhật isGrounded
        yield return null;

        // Sử dụng Reflection để truy cập biến private isGrounded
        FieldInfo isGroundedField = typeof(dichuyen)
            .GetField("isGrounded", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(isGroundedField, "Không tìm thấy trường isGrounded trong PlayerController!");

        // Kiểm tra rằng ban đầu người chơi đang đứng trên mặt đất
        bool groundedBefore = (bool)isGroundedField.GetValue(playerController);
        Assert.IsTrue(groundedBefore, "Player không được nhận diện là đang đứng trên mặt đất trước khi nhảy!");

        // Thêm virtual Keyboard từ Input System để giả lập input
        var jumpSpeed = 3.0f;
        var rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        // Đợi thêm vài frame để quá trình nhảy được xử lý (vì nhảy sẽ thay đổi velocity và làm cho isGrounded = false)
        yield return new WaitForSeconds(0.1f);

        // Kiểm tra rằng sau khi nhảy, isGrounded = false
        bool groundedAfter = (bool)isGroundedField.GetValue(playerController);
        Assert.IsFalse(groundedAfter, "Player vẫn được nhận diện là đang đứng trên mặt đất sau khi nhảy!");
        Assert.Pass("TestIsGroundedBeforeAndAfterJump pass.");
    }

    //[UnityTest]
    //public IEnumerator TestHighSpeedCollisionWithTrap_AndJumpInput()
    //{
    //    // Đợi một chút để đảm bảo scene được khởi tạo đầy đủ
    //    yield return new WaitForSeconds(0.1f);

    //    // Tìm Trap (giả sử có ít nhất 1 Trap trong scene)
    //    var traps = GameObject.FindObjectsOfType<Trap>();
    //    Assert.IsNotNull(traps, "Không tìm thấy đối tượng nào có script Trap!");
    //    Assert.IsTrue(traps.Length > 0, "Không tìm thấy Trap nào trong scene!");

    //    // Lưu lại vị trí của các Trap
    //    List<Vector3> trapPositions = new List<Vector3>();
    //    foreach (var trap in traps)
    //    {
    //        trapPositions.Add(trap.transform.position);
    //    }


    //    foreach (var pos in trapPositions)
    //    {
    //        // Reload lại scene để reset trạng thái game cho mỗi test riêng biệt
    //        SceneManager.LoadScene(7, LoadSceneMode.Additive);
    //        yield return null;

    //        UnityEngine.SceneManagement.Scene testScene = SceneManager.GetSceneByBuildIndex(7);
    //        GameObject player = GameObject.Find("Player");
    //        Assert.IsNotNull(player, "Không tìm thấy Player");

    //        // Tìm GameController trong scene (GameController chứa logic GameOver)
    //        GameController gameController = GameObject.FindObjectOfType<GameController>();
    //        Assert.IsNotNull(gameController, "Không tìm thấy GameController trong scene!");

    //        // Đặt người chơi dưới Trap: giảm giá trị Y của vị trí trap một khoảng (ví dụ 1 đơn vị)
    //        Vector3 newPlayerPos = new Vector3(pos.x, pos.y - 1f, pos.z);
    //        player.transform.position = newPlayerPos;

    //        // Thiết lập một virtual Keyboard để giả lập input nhấn phím Space
    //        var keyboard = InputSystem.AddDevice<Keyboard>();

    //        // Giả lập input: nhấn space liên tục (từng lần nhấn, không giữ liên tục)
    //        int tapCount = 5;
    //        for (int i = 0; i < tapCount; i++)
    //        {
    //            // Giả lập nhấn phím Space
    //            Press(keyboard.spaceKey);
    //            // Chờ một chút (0.05 giây) cho sự kiện được gửi đi
    //            yield return new WaitForSeconds(0.05f);
    //            // Giả lập thả phím Space
    //            Release(keyboard.spaceKey);
    //            yield return new WaitForSeconds(0.05f);
    //        }

    //        // Đợi thêm một chút để hệ thống vật lý xử lý va chạm và gọi GameOver()
    //        yield return new WaitForSeconds(0.2f);

    //        // Đợi vài frame để vật lý cập nhật và xử lý va chạm
    //        yield return new WaitForFixedUpdate();
    //        gameController.StopAllCoroutines();
    //        Time.timeScale = 1f;
    //        yield return new WaitForSeconds(0.05f);

    //        FieldInfo isGameOverField = typeof(GameController).GetField("isGameOver", BindingFlags.NonPublic | BindingFlags.Instance);
    //        Assert.IsNotNull(isGameOverField, "Không tìm thấy trường isGameOver trong GameController!");

    //        bool isGameOverValue = (bool)isGameOverField.GetValue(gameController);
    //        Assert.IsTrue(isGameOverValue, "GameOver chưa được kích hoạt");
    //        yield return null;
    //    }





    //    //// Ngay sau đó, dừng coroutine RestartGame() để tránh scene bị reload khi kiểm tra
    //    //gameController.StopAllCoroutines();

    //    //// Sử dụng Reflection để truy cập biến private isGameOver trong GameController
    //    //FieldInfo isGameOverField = typeof(GameController)
    //    //    .GetField("isGameOver", BindingFlags.NonPublic | BindingFlags.Instance);
    //    //Assert.IsNotNull(isGameOverField, "Không tìm thấy trường isGameOver trong GameController!");

    //    //bool isGameOverValue = (bool)isGameOverField.GetValue(gameController);
    //    //Assert.IsTrue(isGameOverValue, "isGameOver chưa được kích hoạt sau khi va chạm với Trap khi nhảy.");

    //    //yield return null;
    //}
}
