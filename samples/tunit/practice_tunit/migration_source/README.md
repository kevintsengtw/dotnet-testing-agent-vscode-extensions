# xUnit → TUnit 移轉練習

這個練習先確認既有 xUnit 測試可執行，再透過 Claude Full 或 Codex Full 將相同情境移轉到 TUnit。原始測試與目標保持分離，不預先附上 TUnit 解答。

本頁是 TUnit 三情境中的 .NET 10 移轉路徑；.NET 8 指定方法、.NET 9 指定整個類別，見[TUnit：指定方法、整個類別與 xUnit 移轉](../README.md)。這裡保留 xUnit 來源，將結果寫到另一個 TUnit 測試專案。

## Step 1：準備副本，確認來源與目標

下列路徑以整份範例副本的工作區根目錄為基準：

| 角色 | 路徑 |
| --- | --- |
| 待測程式 | `tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Services/BookCatalog.cs` |
| xUnit 來源測試 | `tunit/practice_tunit/migration_source/BookCatalogXunitTests.cs` |
| xUnit 來源專案 | `tunit/practice_tunit/migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj` |
| TUnit 目標專案 | `tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj` |

來源專案只明確編譯 `BookCatalogXunitTests.cs`，不加入 TUnit 方案。三個版本的 TUnit 目標各在自己的 `tests/` 子目錄，不會自動編譯外部的 `migration_source/`。不要把 xUnit 檔案複製進目標後，讓兩種框架一起編譯。

準備一份乾淨副本，TUnit 目標應尚無前次生成的測試。若剛做完新測試練習，重新複製範例，不要將舊答案當成移轉起點。安裝 Full 與工作區開啟方式見[在工具包安裝模式，再到 AI 對話啟動工作流程](../../../guides/mode-setup.md)。

## Step 2：執行來源，保留移轉前結果

在工作區根目錄開啟 PowerShell：

```powershell
& {
    Push-Location -LiteralPath '.\tunit\practice_tunit' -ErrorAction Stop
    try {
        dotnet test 'migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj' --verbosity normal
        if ($LASTEXITCODE -ne 0) { throw '來源測試失敗，先保留輸出並釐清原因。' }
    } finally {
        Pop-Location
    }
}
```

此專案使用 xUnit 2.9.2、runner 2.8.2、Microsoft.NET.Test.Sdk 17.12.0、AwesomeAssertions 9.1.0。這些套件只屬於 xUnit 來源，不要加入 TUnit 目標。

P2（2026-10-16）的歷史來源檢查通過 36 項展開測試；本次未重跑。來源涵蓋 ISBN、折扣、逾期罰款、頁數分類、索引碼與經典書判斷。`IsClassic` 仍使用系統日期，本次沒有順便重構時間來源或補入所有日期邊界；請保留原有測試意圖，必要改善另行檢視。

## Step 3：安裝 Full，複製 TUnit 範本

在工具包選 Claude Full 或 Codex Full，於「總覽」安裝並執行「環境診斷」，到 Skills／Agents 查看安裝內容。確認 AI 擴充套件已登入且對話位於同一副本。工具包沒有另一張「移轉」提示詞卡。這次仍從 **Claude Full／Codex Full → 預設提示詞 → TUnit 單元測試** 按 **複製提示詞**，貼到同一份副本的 AI 擴充套件對話框，再把任務改成移轉：

| 欄位 | 這次要改成什麼 |
| --- | --- |
| 被測試專案 | `tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj` |
| 被測試類別 | `BookCatalog` |
| 範圍 | 移除此欄，因為這次處理整個類別的既有測試情境 |
| 測試專案 | `tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj` |
| 說明 | 明確說明「xUnit → TUnit 移轉」，填入上表的 xUnit 來源測試與來源專案路徑，要求保留原有情境並執行及審查；完整文字見下方對應工具的提示詞 |

先依 Step 2 確認來源結果，再修改提示詞。你會觀察 AI 如何讀取原測試、對應 TUnit 寫法、執行及審查；最後比較來源與目標情境，不是只看到新增 TUnit 檔案就結束。模式尚未準備者，先依[在工具包安裝模式，再到 AI 對話啟動工作流程](../../../guides/mode-setup.md)安裝 Full 與診斷。

## Step 4：填入移轉需求，再送出對話

選擇你使用的工具，核對下面的來源、目標及說明後送出。觀察 AI 讀取來源、對應 TUnit 語法、執行與審查；若執行失敗，保留輸出及後續修正，不刪掉原案例。

### Claude Full

從面板複製 TUnit 公版後，改成整個 `BookCatalog` 的移轉範圍，省略單一方法欄位。在說明中明確指定可讀取的來源，避免工作流程只重新生成新測試而沒有處理既有情境：

```text
使用 /dotnet-testing-orchestrator-tunit 撰寫 TUnit 單元測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj
說明：這次是 xUnit → TUnit 移轉。明確允許讀取 tunit/practice_tunit/migration_source/BookCatalogXunitTests.cs 與 tunit/practice_tunit/migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj，將其中全部既有測試情境移轉到指定 TUnit 目標。保留資料列、預期值、例外條件、初始化／清理意圖及必要診斷輸出；依 TUnit 0.6.123 處理 Fact、Theory、InlineData、MemberData、ITestOutputHelper 與 IDisposable。不要覆寫 xUnit 來源或待測程式，不將 xUnit 套件加入 TUnit 目標。以 dotnet run 執行並回報情境對照、實際結果與尚未完成的項目。
```

### Codex Full

另建乾淨副本後使用：

```text
使用 $dotnet-testing-orchestrator-tunit 撰寫 TUnit 測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj
說明：這次是 xUnit → TUnit 移轉。明確允許讀取 tunit/practice_tunit/migration_source/BookCatalogXunitTests.cs 與 tunit/practice_tunit/migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj，將其中全部既有測試情境移轉到指定 TUnit 目標。保留資料列、預期值、例外條件、初始化／清理意圖及必要診斷輸出；依 TUnit 0.6.123 處理 Fact、Theory、InlineData、MemberData、ITestOutputHelper 與 IDisposable。不要覆寫 xUnit 來源或待測程式，不將 xUnit 套件加入 TUnit 目標。以 dotnet run 執行並回報情境對照、實際結果與尚未完成的項目。
```

## Step 5：比對情境，閱讀執行與審查結果

1. 比對來源及目標的情境與資料列，確認沒有遺漏；即使測試數相同，仍需檢查實際行為與預期值。
2. 檢查 TUnit 0.6.123 支援的屬性、資料來源、非同步斷言與生命週期處理。不要只把屬性名稱換掉，就宣稱完成移轉。
3. 確認新測試寫入 `tests/Practice.TUnit.Net10.Core.Tests/`，來源檔及待測程式保持不變，目標沒有混入 xUnit／Microsoft.NET.Test.Sdk。
4. 依[TUnit：執行與檢視結果](../README.md#執行與檢視結果)使用 `dotnet run`，核對非零測試數、實際成功／失敗／略過數與審查結果。
5. 重新執行 xUnit 來源，保留前後結果；若出現差異或無法移轉的部分，記下具體情境，不直接刪掉來源測試。

本練習只提供 Net10 的 xUnit 來源專案；Net8／Net9 是新測試情境，不要把本移轉提示詞直接換版本後執行。

## Step 6：重跑或套用自己的移轉來源

需要重跑時，TUnit 目標依[TUnit：執行與檢視結果](../README.md#執行與檢視結果)使用 dotnet run，xUnit 來源使用 Step 2 命令。將這個操作套到自己的專案時，替換待測專案、來源測試／來源專案與目標專案，保留「移轉既有情境」的說明。要比較其他模式，另建副本並保留目前成果。
