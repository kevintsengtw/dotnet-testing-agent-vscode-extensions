# Integration：API 操作、單一 Controller 與多個目標

這套範例讓你把不同範圍的整合測試需求，交給工具包安裝的工作流程。你會從面板複製提示詞，在 Claude Code 或 Codex 的 VS Code 對話框填入目標，讓 AI 建立、執行及審查 HTTP 與資料庫測試。三種情境都使用 **Claude Full 或 Codex Full**；Lite 不提供 Integration 工作流程。

## Step 1：選擇情境並開啟工作區

依[工作區準備與工具設定](../../guides/getting-started.md)取得 public repo 的 git／ZIP，複製整份 samples 成自己的副本，再用 VS Code 開啟該副本。最上層應直接看得到 unit、integration、aspire、tunit 與 guides；以下提示詞都從此根目錄計算。

| .NET 版本 | 本輪操作範圍 | 方案（相對於 `integration/practice_integration/`） |
| --- | --- | --- |
| .NET 8 | 指定 API 操作 | `Practice.Integration.Net8.slnx` |
| .NET 9 | 指定單一 Controller | `Practice.Integration.slnx` |
| .NET 10 | 指定多個目標 | `Practice.Integration.Net10.slnx` |

選定一列與一個 AI 模式即可。要比較另一情境或 Claude／Codex，另建新副本並保留原成果。版本與範圍是教材的情境分配，不是工作流程的版本限制。

**.NET 9 沿用沒有 `.Net9` 字尾的來源名稱**，請依實際路徑操作，不能只替換 .NET 10 提示詞的版本字樣。

## Step 2：準備環境，安裝 Full 工作流程

先完成[容器練習前置條件與隔離](../../guides/containers.md)：.NET 10 SDK、所選版本的 .NET／ASP.NET Core runtime，以及可連線的本機 Linux Docker。練習只使用本次建立的資源。

1. 在這個 VS Code 視窗準備好 Claude Code 或 Codex 擴充套件，完成登入並開啟此副本的對話。
2. 開啟 **dotnet-testing-agent 工具包**，選 **Claude Full** 或 **Codex Full**，在 **總覽** 安裝。
3. 執行 **環境診斷**，依實際結果補齊缺少的工具或檔案；到 **Skills／Agents** 查看安裝內容。

畫面與模式選擇見[在工具包安裝模式，再到 AI 對話啟動工作流程](../../guides/mode-setup.md)。此時準備的是工作流程，測試要等你送出 AI 需求才會開始。

## Step 3：複製預設提示詞，貼到 AI 對話

在剛安裝模式的 **預設提示詞** 展開 **Integration 整合測試**，按 **複製提示詞**。貼進同一工作區的 Claude Code／Codex 對話輸入框，先不要送出。提示詞貼在 AI 對話；頁末的重跑命令才貼到 PowerShell。

## Step 4：依所選情境修改欄位

保留所選模式的 Skill 名稱與 `/` 或 `$` 寫法。以下提供填好欄位的完整文字，選擇你的版本與 AI 工具後，對照修改剛貼上的範本。

### .NET 8：指定 API 操作

這一輪把需求限縮到「建立訂單」一個 API 操作。你會在同一個 Controller 路徑旁標明 POST /api/orders，並在說明中交代成功、無效輸入及 PostgreSQL 寫入結果，讓工作流程不會自行延伸到全部端點。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `integration/practice_integration/src/Practice.Integration.WebApi.Net8/Practice.Integration.WebApi.Net8.csproj` |
| 被測試目標 | `integration/practice_integration/src/Practice.Integration.WebApi.Net8/Controllers/OrdersController.cs（只測 POST /api/orders）` |
| 測試專案 | `integration/practice_integration/tests/Practice.Integration.WebApi.Net8.Tests/Practice.Integration.WebApi.Net8.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi.Net8/Practice.Integration.WebApi.Net8.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi.Net8/Controllers/OrdersController.cs（只測 POST /api/orders）
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Net8.Tests/Practice.Integration.WebApi.Net8.Tests.csproj
說明：限定 OrdersController.Create 對應的 POST /api/orders。有效訂單應回 201 且狀態 Pending，無效請求回 400 並檢查驗證錯誤；以本次 PostgreSQL 確認有效請求確實寫入、無效請求沒有新增資料。本輪不擴展成其他訂單端點或 MongoDB／Redis 測試。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi.Net8/Practice.Integration.WebApi.Net8.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi.Net8/Controllers/OrdersController.cs（只測 POST /api/orders）
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Net8.Tests/Practice.Integration.WebApi.Net8.Tests.csproj
說明：限定 OrdersController.Create 對應的 POST /api/orders。有效訂單應回 201 且狀態 Pending，無效請求回 400 並檢查驗證錯誤；以本次 PostgreSQL 確認有效請求確實寫入、無效請求沒有新增資料。本輪不擴展成其他訂單端點或 MongoDB／Redis 測試。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

### .NET 9：指定單一 Controller

這一輪將範圍改成整個 CustomerActivitiesController。填入類別路徑並說明要涵蓋五個操作，觀察工作流程如何共用 MongoDB fixture，同時隔離不同顧客與活動類型的資料。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `integration/practice_integration/src/Practice.Integration.WebApi/Practice.Integration.WebApi.csproj` |
| 被測試目標 | `integration/practice_integration/src/Practice.Integration.WebApi/Controllers/CustomerActivitiesController.cs（整個類別）` |
| 測試專案 | `integration/practice_integration/tests/Practice.Integration.WebApi.Tests/Practice.Integration.WebApi.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi/Practice.Integration.WebApi.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi/Controllers/CustomerActivitiesController.cs（整個類別）
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Tests/Practice.Integration.WebApi.Tests.csproj
說明：測試整個 CustomerActivitiesController（api/CustomerActivities），涵蓋建立、依 ID 讀取、依顧客查詢、依類型查詢及刪除。以本次 MongoDB 驗證文件寫入與查詢隔離，涵蓋有效建立 201、無效請求 400、讀取不存在 ID 404、刪除 204／不存在 404。不存在的 ID 使用格式有效的 ObjectId 字串；時間欄位依原碼的 TimeProvider 驗證。本輪不測 OrdersController 或 RedisCacheService。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi/Practice.Integration.WebApi.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi/Controllers/CustomerActivitiesController.cs（整個類別）
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Tests/Practice.Integration.WebApi.Tests.csproj
說明：測試整個 CustomerActivitiesController（api/CustomerActivities），涵蓋建立、依 ID 讀取、依顧客查詢、依類型查詢及刪除。以本次 MongoDB 驗證文件寫入與查詢隔離，涵蓋有效建立 201、無效請求 400、讀取不存在 ID 404、刪除 204／不存在 404。不存在的 ID 使用格式有效的 ObjectId 字串；時間欄位依原碼的 TimeProvider 驗證。本輪不測 OrdersController 或 RedisCacheService。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

### .NET 10：指定多個目標

這一輪在同一份需求列出兩個 Controller 與 RedisCacheService。工作流程需要分別安排 PostgreSQL、MongoDB 與 Redis 測試；訂單／活動走 HTTP，快取則直接測服務，因為現有 API 沒有呼叫它。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `integration/practice_integration/src/Practice.Integration.WebApi.Net10/Practice.Integration.WebApi.Net10.csproj` |
| 被測試目標 | `integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/OrdersController.cs`、`integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/CustomerActivitiesController.cs`、`integration/practice_integration/src/Practice.Integration.WebApi.Net10/Services/RedisCacheService.cs` |
| 測試專案 | `integration/practice_integration/tests/Practice.Integration.WebApi.Net10.Tests/Practice.Integration.WebApi.Net10.Tests.csproj` |
| 說明 | 使用下方的行為情境、相依服務與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi.Net10/Practice.Integration.WebApi.Net10.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/OrdersController.cs、integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/CustomerActivitiesController.cs、integration/practice_integration/src/Practice.Integration.WebApi.Net10/Services/RedisCacheService.cs
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Net10.Tests/Practice.Integration.WebApi.Net10.Tests.csproj
說明：本輪同時測 OrdersController、CustomerActivitiesController 與 RedisCacheService。訂單涵蓋建立／讀回／確認及重複確認的狀態衝突；活動涵蓋 MongoDB 建立／讀回／查詢／刪除；RedisCacheService 另以唯一 key 驗證 SetAsync、GetAsync、ExistsAsync、RemoveAsync。API 控制器目前沒有呼叫快取服務，Redis 必須獨立測試，不能由 HTTP 成功推論已測過 Redis。三種資料相依分別記錄實際結果。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-integration 撰寫整合測試
被測試專案：integration/practice_integration/src/Practice.Integration.WebApi.Net10/Practice.Integration.WebApi.Net10.csproj
被測試目標：integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/OrdersController.cs、integration/practice_integration/src/Practice.Integration.WebApi.Net10/Controllers/CustomerActivitiesController.cs、integration/practice_integration/src/Practice.Integration.WebApi.Net10/Services/RedisCacheService.cs
測試專案：integration/practice_integration/tests/Practice.Integration.WebApi.Net10.Tests/Practice.Integration.WebApi.Net10.Tests.csproj
說明：本輪同時測 OrdersController、CustomerActivitiesController 與 RedisCacheService。訂單涵蓋建立／讀回／確認及重複確認的狀態衝突；活動涵蓋 MongoDB 建立／讀回／查詢／刪除；RedisCacheService 另以唯一 key 驗證 SetAsync、GetAsync、ExistsAsync、RemoveAsync。API 控制器目前沒有呼叫快取服務，Redis 必須獨立測試，不能由 HTTP 成功推論已測過 Redis。三種資料相依分別記錄實際結果。 使用 WebApplicationFactory 與本次 Testcontainers，環境設為 Testing，由 fixture 注入所選情境使用的 DbContext／MongoDB／Redis；只建立必要的相依服務。使用動態連接埠，不連既有 localhost 服務，不使用 InMemory 取代真實資料庫。補齊與目標框架相容的必要測試套件，以 xUnit dotnet test 執行。保留待測程式與既有套件版本；成功或失敗都只釋放本次建立的資源，保留輸出及審查結果。
```

## Step 5：送出需求並觀察工作流程

核對來源、版本、範圍與測試專案後，在 AI 對話送出。從工具活動與回覆觀察分析、撰寫、執行及審查的實際工作。若 AI 工具要求檔案或終端機權限，核對操作位於這份副本，再依你的工具設定處理。

檢查分析是否符合所選情境，測試是否寫入指定 tests 專案，以及執行失敗是否有修正與審查。遇到工具、套件或容器問題，保留完整錯誤並依具體原因處理，不把任務擴成其他版本的全面測試。

## Step 6：閱讀生成測試與審查結果

在 VS Code 檔案總管開啟本情境的測試目錄，對照 Step 4 的行為清單閱讀斷言，再查看對話中的實際命令、非零測試數、成功／失敗／略過及 Reviewer 意見。案例數與檔名可不同，但應能說明指定行為如何被驗證；只有新增檔案或骨架建置成功，還不能說明需求已完成。

確認 fixture 使用本次資料與資源，成功、失敗及初始化失敗時都釋放自己的資源。不要停止或刪除其他容器來處理本次失敗。

## 執行與檢視結果

### Step 7：需要自己重跑時，選擇對應指令

工作流程應已執行測試；下面供你再次執行生成成果。把 `$testProject` 改為所選版本那一列，再於範例副本根目錄的 PowerShell 執行。尚未生成測試的空骨架不作為成果。

| 版本 | `$testProject`（相對於 `integration/practice_integration/`） |
| --- | --- |
| .NET 8 | `tests/Practice.Integration.WebApi.Net8.Tests/Practice.Integration.WebApi.Net8.Tests.csproj` |
| .NET 9 | `tests/Practice.Integration.WebApi.Tests/Practice.Integration.WebApi.Tests.csproj` |
| .NET 10 | `tests/Practice.Integration.WebApi.Net10.Tests/Practice.Integration.WebApi.Net10.Tests.csproj` |

```powershell
& {
    $testProject = 'tests/Practice.Integration.WebApi.Net8.Tests/Practice.Integration.WebApi.Net8.Tests.csproj' # 改為上表所選版本
    Push-Location -LiteralPath '.\integration\practice_integration' -ErrorAction Stop
    try {
        dotnet test $testProject
        if ($LASTEXITCODE -ne 0) { throw '測試失敗，請保留完整輸出。' }
    } finally {
        Pop-Location
    }
}
```

### Step 8：套用到自己的專案

依[套用到自己的專案](../../guides/apply-to-your-project.md)替換專案、目標與情境，沿用相同的面板與對話操作。三種需求都能用同一流程表達，不需要把自己專案的目標框架改成教材版本。

## 固定版本與必要測試套件

| 套件 | .NET 8 | .NET 9 | .NET 10 |
| --- | --- | --- | --- |
| Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.11 | 9.0.4 | 10.0.0 |
| MongoDB.Driver | 3.12.0 | 3.1.0 | 3.12.0 |
| FluentValidation.DependencyInjectionExtensions | 11.11.0 | 11.11.0 | 11.11.0 |
| StackExchange.Redis／Swashbuckle | 2.8.41／7.2.0 | 相同 | 相同 |
| Test SDK／xunit／runner／coverlet | 17.12.0／2.9.2／2.8.2／6.0.2 | 相同 | 相同 |

骨架沒有預先加入 MVC Testing 或 Testcontainers；由工作流程按本輪情境在測試端補齊。MVC Testing 須配合目標框架，不將 .NET 10 的套件整套搬到 .NET 8／9。新增 .NET 9 沿用來源套件，並沒有同步套用其他版本的升級；本次未還原、建置或查詢其弱點。

### 測試工具的 SSH.NET 相依修補

2026-10-17 先前的 .NET 8／10 隔離檢查使用 MVC Testing 8.0.11／10.0.3，以及同版 Testcontainers.PostgreSql／MongoDb／Redis 4.14.0；Testcontainers 原生解析 SSH.NET 2026.0.0。工具只加入當時的暫存測試專案，正式骨架仍無測試答案。

同日歷史紀錄包含兩版 MongoDB.Driver 3.12.0 的公開還原、完整相依與 audit，以及每版五項 PostgreSQL／MongoDB／Redis 和 BSON 壓縮相容檢查；當輪 audit 未回報已知弱點。範圍限 Windows、本機 Docker 與當時版本，沒有驗證 SSH Docker endpoint、SCP／SFTP 或所有端點。這些不是本次重跑結果，也不適用於本次新增的 .NET 9。

容器設定與失敗處理見[容器練習前置條件與隔離](../../guides/containers.md)，工具與路徑問題見[問題排查](../../guides/troubleshooting.md)。
