# TUnit：指定方法、整個類別與 xUnit 移轉

這套範例讓你使用工具包的 TUnit 工作流程，分別提出方法測試、類別測試及既有 xUnit 測試移轉需求。三種情境都使用 **Claude Full 或 Codex Full**；Lite 不提供 TUnit 工作流程。待測程式與空的 TUnit 測試專案已備妥，你只需透過工具包取得提示詞，修改需求後交給對應 AI 對話執行。

## Step 1：選擇情境並開啟工作區

依[工作區準備與工具設定](../../guides/getting-started.md)取得 public repo 的 git／ZIP，複製整份 samples 成自己的副本，再用 VS Code 開啟該副本。最上層應直接看得到 unit、integration、aspire、tunit 與 guides；以下提示詞都從此根目錄計算。

| .NET 版本 | 本輪操作範圍 | 方案（相對於 `tunit/practice_tunit/`） |
| --- | --- | --- |
| .NET 8 | 指定單一方法 | `Practice.TUnit.Net8.slnx` |
| .NET 9 | 指定整個類別 | `Practice.TUnit.slnx` |
| .NET 10 | 移轉既有 xUnit 測試 | `Practice.TUnit.Net10.slnx` |

選定一列與一個 AI 模式即可。要比較另一情境或 Claude／Codex，另建新副本並保留原成果。版本與範圍是教材的情境分配，不是工作流程的版本限制。

**.NET 9 沿用沒有 `.Net9` 字尾的來源名稱**，請依實際路徑操作，不能只替換 .NET 10 提示詞的版本字樣。

## Step 2：準備環境，安裝 Full 工作流程

準備正式版 .NET 10 SDK 與所選版本的 .NET runtime；本套不需要 Docker 或 ReportGenerator。

1. 在這個 VS Code 視窗準備好 Claude Code 或 Codex 擴充套件，完成登入並開啟此副本的對話。
2. 開啟 **dotnet-testing-agent 工具包**，選 **Claude Full** 或 **Codex Full**，在 **總覽** 安裝。
3. 執行 **環境診斷**，依實際結果補齊缺少的工具或檔案；到 **Skills／Agents** 查看安裝內容。

畫面與模式選擇見[在工具包安裝模式，再到 AI 對話啟動工作流程](../../guides/mode-setup.md)。此時準備的是工作流程，測試要等你送出 AI 需求才會開始。

## Step 3：複製預設提示詞，貼到 AI 對話

在剛安裝模式的 **預設提示詞** 展開 **TUnit 單元測試**，按 **複製提示詞**。貼進同一工作區的 Claude Code／Codex 對話輸入框，先不要送出。提示詞貼在 AI 對話；頁末的重跑命令才貼到 PowerShell。

## Step 4：依所選情境修改欄位

保留所選模式的 Skill 名稱與 `/` 或 `$` 寫法。以下提供填好欄位的完整文字，選擇你的版本與 AI 工具後，對照修改剛貼上的範本。

### .NET 8：指定單一方法

這一輪指定 BookCatalog 的 ISBN 驗證方法。保留「範圍」欄並填 IsValidIsbn13，讓 AI 針對檢查碼、長度與字元情境建立 TUnit 測試，不延伸到類別其他方法。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `tunit/practice_tunit/src/Practice.TUnit.Net8.Core/Practice.TUnit.Net8.Core.csproj` |
| 被測試類別 | `BookCatalog` |
| 範圍 | `IsValidIsbn13` |
| 測試專案 | `tunit/practice_tunit/tests/Practice.TUnit.Net8.Core.Tests/Practice.TUnit.Net8.Core.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-tunit 撰寫 TUnit 單元測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net8.Core/Practice.TUnit.Net8.Core.csproj
被測試類別：BookCatalog
範圍：IsValidIsbn13
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net8.Core.Tests/Practice.TUnit.Net8.Core.Tests.csproj
說明：只為 BookCatalog.IsValidIsbn13 產生新測試，涵蓋有／無連字號的有效 ISBN、錯誤檢查碼、長度不足、非數字及空白。不要讀取或移轉 migration_source，本輪不測 BookCatalog 其他方法。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-tunit 撰寫 TUnit 測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net8.Core/Practice.TUnit.Net8.Core.csproj
被測試類別：BookCatalog
範圍：IsValidIsbn13
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net8.Core.Tests/Practice.TUnit.Net8.Core.Tests.csproj
說明：只為 BookCatalog.IsValidIsbn13 產生新測試，涵蓋有／無連字號的有效 ISBN、錯誤檢查碼、長度不足、非數字及空白。不要讀取或移轉 migration_source，本輪不測 BookCatalog 其他方法。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

### .NET 9：指定整個類別

這一輪要處理整個 BookCatalog，請移除「範圍」整行。說明中列出六個公開方法與重要原有規則，觀察工作流程如何分類正常、邊界與例外情境，而不是只擴充 ISBN 測試。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `tunit/practice_tunit/src/Practice.TUnit.Core/Practice.TUnit.Core.csproj` |
| 被測試類別 | `BookCatalog` |
| 範圍 | 移除此欄，處理整個類別 |
| 測試專案 | `tunit/practice_tunit/tests/Practice.TUnit.Core.Tests/Practice.TUnit.Core.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-tunit 撰寫 TUnit 單元測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Core/Practice.TUnit.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Core.Tests/Practice.TUnit.Core.Tests.csproj
說明：為整個 BookCatalog 產生新測試，涵蓋 IsValidIsbn13、CalculateDiscountPrice、CalculateOverdueFine、ClassifyByPageCount、GenerateIndexCode、IsClassic 的正常、邊界與例外情境。先讀原碼：未逾期先回 0，不能把負價格例外套到該分支；IsClassic 使用 DateTime.Today，資料依執行日計算。不要讀取 migration_source，不修改待測行為。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-tunit 撰寫 TUnit 測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Core/Practice.TUnit.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Core.Tests/Practice.TUnit.Core.Tests.csproj
說明：為整個 BookCatalog 產生新測試，涵蓋 IsValidIsbn13、CalculateDiscountPrice、CalculateOverdueFine、ClassifyByPageCount、GenerateIndexCode、IsClassic 的正常、邊界與例外情境。先讀原碼：未逾期先回 0，不能把負價格例外套到該分支；IsClassic 使用 DateTime.Today，資料依執行日計算。不要讀取 migration_source，不修改待測行為。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

### .NET 10：移轉既有 xUnit 測試

這一輪已有 xUnit 測試，要保留原有情境並移轉成 TUnit。使用同一張 TUnit 提示詞卡，但在說明中填入來源測試與來源專案，讓 AI 讀取既有資料列、斷言與生命週期，將結果寫入獨立目標。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj` |
| 被測試類別 | `BookCatalog` |
| 範圍 | 移除此欄，處理整個類別的既有測試移轉 |
| 測試專案 | `tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

先閱讀[xUnit → TUnit 移轉練習](migration_source/README.md)，確認 xUnit 來源與 TUnit 目標。工具包沒有另一張移轉卡，這次在 TUnit 範本中明確指定移轉。

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-tunit 撰寫 TUnit 單元測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj
說明：這次是 xUnit → TUnit 移轉。先執行 tunit/practice_tunit/migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj，讀取其中 BookCatalogXunitTests.cs，將全部既有情境移轉到指定目標。保留資料列、預期值、例外、初始化／清理及必要診斷輸出；依 TUnit 0.6.123 對應 Fact、Theory、InlineData、MemberData、ITestOutputHelper 與 IDisposable。不要覆寫來源或待測程式，不將 xUnit／Microsoft.NET.Test.Sdk 加入 TUnit 目標。完成後再執行來源，提供來源／目標情境對照及未完成項目。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-tunit 撰寫 TUnit 測試
被測試專案：tunit/practice_tunit/src/Practice.TUnit.Net10.Core/Practice.TUnit.Net10.Core.csproj
被測試類別：BookCatalog
測試專案：tunit/practice_tunit/tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj
說明：這次是 xUnit → TUnit 移轉。先執行 tunit/practice_tunit/migration_source/Practice.TUnit.Net10.Xunit.Tests.csproj，讀取其中 BookCatalogXunitTests.cs，將全部既有情境移轉到指定目標。保留資料列、預期值、例外、初始化／清理及必要診斷輸出；依 TUnit 0.6.123 對應 Fact、Theory、InlineData、MemberData、ITestOutputHelper 與 IDisposable。不要覆寫來源或待測程式，不將 xUnit／Microsoft.NET.Test.Sdk 加入 TUnit 目標。完成後再執行來源，提供來源／目標情境對照及未完成項目。 使用現有 TUnit 0.6.123 與相容 API，在指定測試專案產生測試，不升級套件或改待測程式。TUnit 目標以 dotnet run --project 執行，回報非零測試數、失敗／略過與審查結果。原有產物與失敗輸出保留。
```

## Step 5：送出需求並觀察工作流程

核對來源、版本、範圍與測試專案後，在 AI 對話送出。從工具活動與回覆觀察分析、撰寫、執行及審查的實際工作。若 AI 工具要求檔案或終端機權限，核對操作位於這份副本，再依你的工具設定處理。

檢查分析是否符合所選情境，測試是否寫入指定 tests 專案，以及執行失敗是否有修正與審查。遇到工具、套件或容器問題，保留完整錯誤並依具體原因處理，不把任務擴成其他版本的全面測試。

## Step 6：閱讀生成測試與審查結果

在 VS Code 檔案總管開啟本情境的測試目錄，對照 Step 4 的行為清單閱讀斷言，再查看對話中的實際命令、非零測試數、成功／失敗／略過及 Reviewer 意見。案例數與檔名可不同，但應能說明指定行為如何被驗證；只有新增檔案或骨架建置成功，還不能說明需求已完成。

移轉還要逐項對照來源資料列、預期值與生命週期。來源保持不變，TUnit 目標用 dotnet run，xUnit 來源用 dotnet test。

## 執行與檢視結果

### Step 7：需要自己重跑時，選擇對應指令

工作流程應已執行測試；下面供你再次執行生成成果。把 `$testProject` 改為所選版本那一列，再於範例副本根目錄的 PowerShell 執行。尚未生成測試的空骨架不作為成果。

| 版本 | `$testProject`（相對於 `tunit/practice_tunit/`） |
| --- | --- |
| .NET 8 | `tests/Practice.TUnit.Net8.Core.Tests/Practice.TUnit.Net8.Core.Tests.csproj` |
| .NET 9 | `tests/Practice.TUnit.Core.Tests/Practice.TUnit.Core.Tests.csproj` |
| .NET 10 | `tests/Practice.TUnit.Net10.Core.Tests/Practice.TUnit.Net10.Core.Tests.csproj` |

```powershell
& {
    $testProject = 'tests/Practice.TUnit.Net8.Core.Tests/Practice.TUnit.Net8.Core.Tests.csproj' # 改為上表所選版本
    Push-Location -LiteralPath '.\tunit\practice_tunit' -ErrorAction Stop
    try {
        dotnet run --project $testProject
        if ($LASTEXITCODE -ne 0) { throw '測試失敗，請保留完整輸出。' }
    } finally {
        Pop-Location
    }
}
```

### Step 8：套用到自己的專案

依[套用到自己的專案](../../guides/apply-to-your-project.md)替換專案、目標與情境，沿用相同的面板與對話操作。三種需求都能用同一流程表達，不需要把自己專案的目標框架改成教材版本。

## 固定版本與 MTP 範例的區別

| 套件 | .NET 8 | .NET 9 | .NET 10 |
| --- | --- | --- | --- |
| TUnit | 0.6.123 | 0.6.123 | 0.6.123 |
| AwesomeAssertions／Bogus／NSubstitute | 9.1.0／35.6.1／5.3.0 | 相同 | 相同 |
| Microsoft.Extensions.TimeProvider.Testing | 8.10.0 | 9.0.0 | 10.0.0 |
| System.IO.Abstractions／TestingHelpers | 21.1.3 | 相同 | 相同 |

各版本保留 OutputType=Exe 與來源套件；TUnit 目標不加入 Microsoft.NET.Test.Sdk，使用 dotnet run。xUnit 移轉來源專案才使用 dotnet test。.NET 9 沿用原來無版本字尾的名稱與 Practice.TUnit.Core 命名空間。

P2（2026-10-16）的 .NET 8／10 歷史檢查包含還原、建置、少量 TUnit 測試與 Net10 xUnit 來源 36 項展開測試；這是當輪結果，不是本次重跑，也不表示完成你的 AI 生成或移轉。新增 .NET 9 本次只核對來源、專案參照與文件。

TUnit 本身使用 MTP，但這份空骨架不是覆蓋率教材。若要操作 Code Coverage 面板，使用已有固定測試及專用設定的 [MTP 原生 Code Coverage：Step by Step](../../coverage-mtp-native/README.md)或[MTP 舊版相容 Code Coverage：Step by Step](../../coverage-mtp-legacy/README.md)範例。
