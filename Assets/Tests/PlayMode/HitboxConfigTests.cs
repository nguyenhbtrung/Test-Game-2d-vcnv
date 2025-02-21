using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HitboxConfigTests
{
    private const float allowedOffset = 0.04f;


    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(SceneTestSettings.SceneIndex);
        yield return null;
    }



    [UnityTest]
    public IEnumerator TestTrapsHitboxComponent()
    {

        string logFileName = "TestTrapsHitboxComponent.txt";
        List<string> issues = new List<string>();
        TestLogger.ClearLog(logFileName);

        GameObject trapsParent = GameObject.Find("bayax");
        Assert.IsNotNull(trapsParent, "Không tìm thấy GameObject 'bayax' trong scene.");

        foreach (Transform child in trapsParent.transform)
        {
            GameObject trap = child.gameObject;

            var trapScript = trap.GetComponent<Trap>();
            var boxCollider = trap.GetComponent<BoxCollider2D>();
            if (trapScript == null)
            {
                issues.Add($"Chướng ngại vật '{trap.name}' không có script Trap.");
            }
            if (boxCollider == null)
            {
                issues.Add($"Chướng ngại vật '{trap.name}' không có BoxCollider2D.");
            }
        }
        if (issues.Count > 0)
        {
            string errorMessage = "TestTrapsHitboxComponent fail:\n" + string.Join("\n", issues);
            TestLogger.Log(errorMessage, logFileName);
            Assert.Fail(errorMessage);
        }
        else
        {
            string successMessage = "TestTrapsHitboxComponent pass.";
            TestLogger.Log(successMessage, logFileName);
            Assert.Pass(successMessage);
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestPlayerHitboxIsValid()
    {

        string logFileName = "TestPlayerHitboxIsValid.txt";
        TestLogger.ClearLog(logFileName);

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Không tìm thấy GameObject 'Player' trong scene.");

        List<string> issues = new List<string>();

        var spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            string issue = $"Player không có SpriteRenderer.";
            issues.Add(issue);
        }

        var sprite = spriteRenderer.sprite;
        if (sprite == null)
        {
            string issue = $"Player không có sprite trong SpriteRenderer.";
            issues.Add(issue);
        }

        Vector2 spriteSize = sprite.bounds.size;

        var boxCollider = player.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            string issue = $"Player không có BoxCollider2D.";
            issues.Add(issue);
        }

        HitBoxConfig playerHitBoxConfig = HitBoxConfigManager.GetHitBoxConfig(HitBoxType.Player);

        Vector2 colliderSize = boxCollider.size;

        // Tính toán tỉ lệ kích thước
        float widthRatio = colliderSize.x / spriteSize.x;
        float heightRatio = colliderSize.y / spriteSize.y;

        if (Mathf.Abs(widthRatio - playerHitBoxConfig.sizeRatio.x) > allowedOffset ||
            Mathf.Abs(heightRatio - playerHitBoxConfig.sizeRatio.y) > allowedOffset)
        {
            string issue = $"Player có kích thước HitBox không hợp lệ.";
            issues.Add(issue);

        }

        Vector2 offset = boxCollider.offset;
        float offsetRatioX = offset.x / spriteSize.x;
        float offsetRatioY = offset.y / spriteSize.y;

        if (Mathf.Abs(offsetRatioX - playerHitBoxConfig.offsetRatio.x) > allowedOffset ||
            Mathf.Abs(offsetRatioY - playerHitBoxConfig.offsetRatio.y) > allowedOffset)
        {
            string issue = $"Player có offset HitBox không hợp lệ.";
            issues.Add(issue);
        }

        if (issues.Count > 0)
        {
            string errorMessage = "Phát hiện lỗi Player hitbox\n" + string.Join("\n", issues);
            TestLogger.Log(errorMessage, logFileName);
            Assert.Fail(errorMessage);
        }
        else
        {
            string successMessage = "Player hitbox hợp lệ.";
            TestLogger.Log(successMessage, logFileName);
            Assert.Pass(successMessage);
        }

        yield return null;

    }

    [UnityTest]
    public IEnumerator TestFinishPointHitboxIsValid()
    {

        string logFileName = "TestFinishPointHitboxIsValid.txt";
        TestLogger.ClearLog(logFileName);

        GameObject player = GameObject.Find("End (Pressed) (64x64)_0");
        Assert.IsNotNull(player, "Không tìm thấy GameObject 'End (Pressed) (64x64)_0' trong scene.");

        List<string> issues = new List<string>();

        var spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            string issue = $"End (Pressed) (64x64)_0 không có SpriteRenderer.";
            issues.Add(issue);
        }

        var sprite = spriteRenderer.sprite;
        if (sprite == null)
        {
            string issue = $"End (Pressed) (64x64)_0 không có sprite trong SpriteRenderer.";
            issues.Add(issue);
        }

        Vector2 spriteSize = sprite.bounds.size;

        var boxCollider = player.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            string issue = $"End (Pressed) (64x64)_0 không có BoxCollider2D.";
            issues.Add(issue);
        }


        HitBoxConfig playerHitBoxConfig = HitBoxConfigManager.GetHitBoxConfig(HitBoxType.FinishPoint);

        Vector2 colliderSize = boxCollider.size;

        float widthRatio = colliderSize.x / spriteSize.x;
        float heightRatio = colliderSize.y / spriteSize.y;

        if (Mathf.Abs(widthRatio - playerHitBoxConfig.sizeRatio.x) > allowedOffset ||
            Mathf.Abs(heightRatio - playerHitBoxConfig.sizeRatio.y) > allowedOffset)
        {
            string issue = $"End (Pressed) (64x64)_0 có kích thước HitBox không hợp lệ.";
            issues.Add(issue);

        }

        Vector2 offset = boxCollider.offset;
        float offsetRatioX = offset.x / spriteSize.x;
        float offsetRatioY = offset.y / spriteSize.y;

        if (Mathf.Abs(offsetRatioX - playerHitBoxConfig.offsetRatio.x) > allowedOffset ||
            Mathf.Abs(offsetRatioY - playerHitBoxConfig.offsetRatio.y) > allowedOffset)
        {
            string issue = $"End (Pressed) (64x64)_0 có offset HitBox không hợp lệ.";
            issues.Add(issue);
        }

        if (issues.Count > 0)
        {
            string errorMessage = "Phát hiện lỗi End (Pressed) (64x64)_0 hitbox\n" + string.Join("\n", issues);
            TestLogger.Log(errorMessage, logFileName);
            Assert.Fail(errorMessage);
        }
        else
        {
            string successMessage = "End (Pressed) (64x64)_0 hitbox hợp lệ.";
            TestLogger.Log(successMessage, logFileName);
            Assert.Pass(successMessage);
        }

        yield return null;

    }

    [UnityTest]
    public IEnumerator TestTrapHitboxsIsValid()
    {

        string logFileName = "TestTrapHitboxsIsValid.txt";
        TestLogger.ClearLog(logFileName);


        GameObject trapsParent = GameObject.Find("bayax");
        Assert.IsNotNull(trapsParent, "Không tìm thấy GameObject 'bayax' trong scene.");

        List<string> trapsWithIssues = new List<string>(); // Danh sách lưu các trap có vấn đề

        foreach (Transform child in trapsParent.transform)
        {
            GameObject trap = child.gameObject;

            var spriteRenderer = trap.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                string issue = $"Trap '{trap.name}' không có SpriteRenderer.";
                trapsWithIssues.Add(issue);
                TestLogger.Log(issue);
                continue;
            }

            var sprite = spriteRenderer.sprite;
            if (sprite == null)
            {
                string issue = $"Trap '{trap.name}' không có sprite trong SpriteRenderer.";
                trapsWithIssues.Add(issue);
                TestLogger.Log(issue);
                continue;
            }

            Vector2 spriteSize = sprite.bounds.size;

            var boxCollider = trap.GetComponent<BoxCollider2D>();
            if (boxCollider == null)
            {
                string issue = $"Trap '{trap.name}' không có BoxCollider2D.";
                trapsWithIssues.Add(issue);
                TestLogger.Log(issue);
                continue;
            }

            HitBoxConfig hitBoxConfig = HitBoxConfigManager.GetHitBoxConfig(GetTrapHitboxType(trap.name));

            Vector2 colliderSize = boxCollider.size;

            float widthRatio = colliderSize.x / spriteSize.x;
            float heightRatio = colliderSize.y / spriteSize.y;
            bool sizeIssue = false;

            if (Mathf.Abs(widthRatio - hitBoxConfig.sizeRatio.x) > allowedOffset ||
                Mathf.Abs(heightRatio - hitBoxConfig.sizeRatio.y) > allowedOffset)
            {
                sizeIssue = true;
            }

            Vector2 offset = boxCollider.offset;
            float offsetRatioX = offset.x / spriteSize.x;
            float offsetRatioY = offset.y / spriteSize.y;
            bool offsetIssue = false;

            if (Mathf.Abs(offsetRatioX - hitBoxConfig.offsetRatio.x) > allowedOffset ||
                Mathf.Abs(offsetRatioY - hitBoxConfig.offsetRatio.y) > allowedOffset)
            {
                offsetIssue = true;
            }

            if (sizeIssue || offsetIssue)
            {
                string issues = $"Trap '{trap.name}' có vấn đề:";
                if (sizeIssue)
                {
                    issues += $" Tỉ lệ kích thước không hợp lệ (widthRatio = {widthRatio:F2}, heightRatio = {heightRatio:F2}).";
                }
                if (offsetIssue)
                {
                    issues += $" Tỉ lệ offset không hợp lệ (offsetRatioX = {offsetRatioX:F2}, offsetRatioY = {offsetRatioY:F2}).";
                }
                trapsWithIssues.Add(issues);
            }
        }

        // Kiểm tra và báo cáo kết quả
        if (trapsWithIssues.Count > 0)
        {
            string errorMessage = "Phát hiện các chướng ngại vật không hợp lệ:\n" + string.Join("\n", trapsWithIssues);
            TestLogger.Log(errorMessage, logFileName);
            Assert.Fail(errorMessage);
        }
        else
        {
            string successMessage = "Tất cả các chướng ngại vật đều hợp lệ.";
            TestLogger.Log(successMessage, logFileName);
            Assert.Pass(successMessage);
        }

        yield return null;
    }

    public HitBoxType GetTrapHitboxType(string trapName)
    {
        if (trapName.StartsWith("On (38x38)_0"))
        {
            return HitBoxType.OnTrap;
        }
        else if (trapName.StartsWith("Idle"))
        {
            return HitBoxType.IdleTrap;
        }
        else if (trapName.StartsWith("Blink (54x52)_0"))
        {
            return HitBoxType.BlinkTrap;
        }
        else
        {
            throw new ArgumentException("Unknown trap name");
        }
    }
}
