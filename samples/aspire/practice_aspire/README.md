# Aspire：指定端點、預約流程與跨服務檢查

這套範例用同一組預約服務，示範如何在提示詞中指定單一端點、狀態流程或完整 API 與資料檢查。操作入口是工具包安裝的 **Claude Full 或 Codex Full** 工作流程。你把 AppHost 與 API 目標交給 AI，觀察它如何建立 fixture、啟動服務、執行測試並審查結果；Lite 不提供 Aspire 工作流程。

## Step 1：選擇情境並開啟工作區

依[工作區準備與工具設定](../../guides/getting-started.md)取得 public repo 的 git／ZIP，複製整份 samples 成自己的副本，再用 VS Code 開啟該副本。最上層應直接看得到 unit、integration、aspire、tunit 與 guides；以下提示詞都從此根目錄計算。

| .NET 版本 | 本輪操作範圍 | 方案（相對於 `aspire/practice_aspire/`） |
| --- | --- | --- |
| .NET 8 | 指定單一端點 | `Practice.Aspire.Net8.slnx` |
| .NET 9 | 指定預約狀態流程 | `Practice.Aspire.slnx` |
| .NET 10 | 完整 API 與跨服務資料檢查 | `Practice.Aspire.Net10.slnx` |

選定一列與一個 AI 模式即可。要比較另一情境或 Claude／Codex，另建新副本並保留原成果。版本與範圍是教材的情境分配，不是工作流程的版本限制。

**.NET 9 沿用沒有 `.Net9` 字尾的來源名稱**，請依實際路徑操作，不能只替換 .NET 10 提示詞的版本字樣。

## Step 2：準備環境，安裝 Full 工作流程

先完成[容器練習前置條件與隔離](../../guides/containers.md)：.NET 10 SDK、所選版本的 .NET／ASP.NET Core runtime，以及可連線的本機 Linux Docker。練習只使用本次建立的資源。

1. 在這個 VS Code 視窗準備好 Claude Code 或 Codex 擴充套件，完成登入並開啟此副本的對話。
2. 開啟 **dotnet-testing-agent 工具包**，選 **Claude Full** 或 **Codex Full**，在 **總覽** 安裝。
3. 執行 **環境診斷**，依實際結果補齊缺少的工具或檔案；到 **Skills／Agents** 查看安裝內容。

畫面與模式選擇見[在工具包安裝模式，再到 AI 對話啟動工作流程](../../guides/mode-setup.md)。此時準備的是工作流程，測試要等你送出 AI 需求才會開始。

## Step 3：複製預設提示詞，貼到 AI 對話

在剛安裝模式的 **預設提示詞** 展開 **Aspire 整合測試**，按 **複製提示詞**。貼進同一工作區的 Claude Code／Codex 對話輸入框，先不要送出。提示詞貼在 AI 對話；頁末的重跑命令才貼到 PowerShell。

## Step 4：依所選情境修改欄位

保留所選模式的 Skill 名稱與 `/` 或 `$` 寫法。以下提供填好欄位的完整文字，選擇你的版本與 AI 工具後，對照修改剛貼上的範本。

**Codex 公版第一行有「涵蓋全部端點」**：.NET 8／9 是限定範圍，請刪去這段描述並保留 Skill 名稱；.NET 10 才保留，避免第一行與這次需求矛盾。

### .NET 8：指定單一端點

這一輪只要求測試建立預約端點。被測試專案填 AppHost，目標才填 WebApi 的 Controller 與 POST 範圍；藉此區分「由誰啟動服務」與「這次要測哪個行為」。AppHost 仍會啟動原本宣告的全部資源。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `aspire/practice_aspire/src/Practice.Aspire.Net8.AppHost/Practice.Aspire.Net8.AppHost.csproj`；填 AppHost |
| 被測試目標 | `aspire/practice_aspire/src/Practice.Aspire.Net8.WebApi/Controllers/BookingsController.cs（只測 POST /api/bookings）` |
| 測試專案 | `aspire/practice_aspire/tests/Practice.Aspire.Net8.AppHost.Tests/Practice.Aspire.Net8.AppHost.Tests.csproj` |
| 說明 | 使用下方的行為情境、依賴與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試
被測試專案：aspire/practice_aspire/src/Practice.Aspire.Net8.AppHost/Practice.Aspire.Net8.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.Net8.WebApi/Controllers/BookingsController.cs（只測 POST /api/bookings）
測試專案：aspire/practice_aspire/tests/Practice.Aspire.Net8.AppHost.Tests/Practice.Aspire.Net8.AppHost.Tests.csproj
說明：本輪只測 POST /api/bookings：有效預約回 201、狀態 Pending；無效 Email、入住／退房日期順序及金額回 400，檢查驗證錯誤。透過本次 SQL Server 確認建立結果，本輪不延伸到查詢、確認、入住或取消端點。 本版 Hosting 為 8.2.2。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試
被測試專案：aspire/practice_aspire/src/Practice.Aspire.Net8.AppHost/Practice.Aspire.Net8.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.Net8.WebApi/Controllers/BookingsController.cs（只測 POST /api/bookings）
測試專案：aspire/practice_aspire/tests/Practice.Aspire.Net8.AppHost.Tests/Practice.Aspire.Net8.AppHost.Tests.csproj
說明：本輪只測 POST /api/bookings：有效預約回 201、狀態 Pending；無效 Email、入住／退房日期順序及金額回 400，檢查驗證錯誤。透過本次 SQL Server 確認建立結果，本輪不延伸到查詢、確認、入住或取消端點。 本版 Hosting 為 8.2.2。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
```

### .NET 9：指定預約狀態流程

這一輪的目標是一條狀態流程，不只是幾個各自獨立的端點。你在說明中指定同一筆預約的建立、讀回、確認及重複確認，讓工作流程安排資料與呼叫順序，並核對 SQL 狀態。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `aspire/practice_aspire/src/Practice.Aspire.AppHost/Practice.Aspire.AppHost.csproj`；填 AppHost |
| 被測試目標 | `aspire/practice_aspire/src/Practice.Aspire.WebApi/Controllers/BookingsController.cs（建立 → 讀回 → 確認 → 重複確認）` |
| 測試專案 | `aspire/practice_aspire/tests/Practice.Aspire.AppHost.Tests/Practice.Aspire.AppHost.Tests.csproj` |
| 說明 | 使用下方的行為情境、依賴與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試
被測試專案：aspire/practice_aspire/src/Practice.Aspire.AppHost/Practice.Aspire.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.WebApi/Controllers/BookingsController.cs（建立 → 讀回 → 確認 → 重複確認）
測試專案：aspire/practice_aspire/tests/Practice.Aspire.AppHost.Tests/Practice.Aspire.AppHost.Tests.csproj
說明：本輪測一條預約狀態流程：POST /api/bookings 建立後回 201／Pending，GET /api/bookings/{id} 讀回一致，PATCH /api/bookings/{id}/confirm 回 200／Confirmed，再次 confirm 回 409。每次狀態轉換後檢查本次 SQL Server 的資料，409 不應改壞既有狀態。本輪不擴展成全部端點。 本版 Hosting 為 9.0.0。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試
被測試專案：aspire/practice_aspire/src/Practice.Aspire.AppHost/Practice.Aspire.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.WebApi/Controllers/BookingsController.cs（建立 → 讀回 → 確認 → 重複確認）
測試專案：aspire/practice_aspire/tests/Practice.Aspire.AppHost.Tests/Practice.Aspire.AppHost.Tests.csproj
說明：本輪測一條預約狀態流程：POST /api/bookings 建立後回 201／Pending，GET /api/bookings/{id} 讀回一致，PATCH /api/bookings/{id}/confirm 回 200／Confirmed，再次 confirm 回 409。每次狀態轉換後檢查本次 SQL Server 的資料，409 不應改壞既有狀態。本輪不擴展成全部端點。 本版 Hosting 為 9.0.0。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
```

### .NET 10：完整 API 與跨服務資料檢查

這一輪把範圍擴為全部預約端點，另加入直接 SQL 核對與獨立 Redis 行為。提示詞分開描述三種結果，讓 AI 能安排 HTTP、資料持久化與快取測試；原碼沒有將預約 API 接到 Redis，因此不把它們描述成同一條業務流程。

| 範本欄位 | 本情境填法 |
| --- | --- |
| 被測試專案 | `aspire/practice_aspire/src/Practice.Aspire.Net10.AppHost/Practice.Aspire.Net10.AppHost.csproj`；填 AppHost |
| 被測試目標 | `aspire/practice_aspire/src/Practice.Aspire.Net10.WebApi/Controllers/BookingsController.cs（全部端點），以及本次 SQL／Redis 資源` |
| 測試專案 | `aspire/practice_aspire/tests/Practice.Aspire.Net10.AppHost.Tests/Practice.Aspire.Net10.AppHost.Tests.csproj` |
| 說明 | 使用下方的行為情境、依賴與執行要求 |

#### Claude Full

```text
使用 /dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試
被測試專案：aspire/practice_aspire/src/Practice.Aspire.Net10.AppHost/Practice.Aspire.Net10.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.Net10.WebApi/Controllers/BookingsController.cs（全部端點），以及本次 SQL／Redis 資源
測試專案：aspire/practice_aspire/tests/Practice.Aspire.Net10.AppHost.Tests/Practice.Aspire.Net10.AppHost.Tests.csproj
說明：涵蓋 api/bookings 全部端點：建立、查詢、依狀態查詢、更新、確認、入住、取消及刪除，依原碼檢查成功回應、驗證錯誤、找不到與狀態衝突。另確認 HTTP 寫入後直接 SQL 讀回一致，並用本次唯一 key 獨立驗證 Redis 寫入／讀回／移除。API 沒有呼叫 Redis，不能把快取檢查說成預約端點的一部分。分開報告 HTTP、SQL、Redis 結果。 本版 Hosting 為 13.1.2。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
```

#### Codex Full

```text
使用 $dotnet-testing-orchestrator-aspire 撰寫 Aspire 整合測試，涵蓋全部端點
被測試專案：aspire/practice_aspire/src/Practice.Aspire.Net10.AppHost/Practice.Aspire.Net10.AppHost.csproj
被測試目標：aspire/practice_aspire/src/Practice.Aspire.Net10.WebApi/Controllers/BookingsController.cs（全部端點），以及本次 SQL／Redis 資源
測試專案：aspire/practice_aspire/tests/Practice.Aspire.Net10.AppHost.Tests/Practice.Aspire.Net10.AppHost.Tests.csproj
說明：涵蓋 api/bookings 全部端點：建立、查詢、依狀態查詢、更新、確認、入住、取消及刪除，依原碼檢查成功回應、驗證錯誤、找不到與狀態衝突。另確認 HTTP 寫入後直接 SQL 讀回一致，並用本次唯一 key 獨立驗證 Redis 寫入／讀回／移除。API 沒有呼叫 Redis，不能把快取檢查說成預約端點的一部分。分開報告 HTTP、SQL、Redis 結果。 本版 Hosting 為 13.1.2。 AppHost 是編排入口，WebApi 是 API 目標。使用與本版本 Hosting 相容的 Aspire.Hosting.Testing／DistributedApplicationTestingBuilder，透過 app.CreateHttpClient("bookingapi", "http") 呼叫；不另建 Testcontainers 或 WebApplicationFactory。fixture 等待 SQL／Redis／HTTP 就緒，建立本次 SQL 資料表，必要時在測試端參照 WebApi。日期依執行當日產生未來值；驗證器用 DateTime.Today，不能假定 TimeProvider 控制它。保留待測碼與 AppHost，以 xUnit dotnet test 執行；成功或失敗都釋放本次 AppHost，不掛載或清理既有資料，回報實際結果及審查。
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

| 版本 | `$testProject`（相對於 `aspire/practice_aspire/`） |
| --- | --- |
| .NET 8 | `tests/Practice.Aspire.Net8.AppHost.Tests/Practice.Aspire.Net8.AppHost.Tests.csproj` |
| .NET 9 | `tests/Practice.Aspire.AppHost.Tests/Practice.Aspire.AppHost.Tests.csproj` |
| .NET 10 | `tests/Practice.Aspire.Net10.AppHost.Tests/Practice.Aspire.Net10.AppHost.Tests.csproj` |

```powershell
& {
    $testProject = 'tests/Practice.Aspire.Net8.AppHost.Tests/Practice.Aspire.Net8.AppHost.Tests.csproj' # 改為上表所選版本
    Push-Location -LiteralPath '.\aspire\practice_aspire' -ErrorAction Stop
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

<a id="固定版本與-net8-補充"></a>

## 固定版本與版本差異

| 項目 | .NET 8 | .NET 9 | .NET 10 |
| --- | --- | --- | --- |
| Aspire.AppHost.Sdk | 9.0.0 | 9.0.0 | 13.1.2 |
| Hosting AppHost／Redis／SqlServer | 8.2.2 | 9.0.0 | 13.1.2（AppHost 由 SDK 提供） |
| EF Core SqlServer | 8.0.11 | 9.0.4 | 10.0.3 |
| FluentValidation／Redis／Swashbuckle | 11.11.0／2.8.41／7.2.0 | 相同 | 相同 |
| Test SDK／xunit／runner／coverlet | 17.12.0／2.9.2／2.8.2／6.0.2 | 相同 | 相同 |
| 限定修補的直接參照 | AppHost：KubernetesClient 17.0.14 | AppHost：KubernetesClient 17.0.14 | AppHost：MessagePack 2.5.307、Pkcs 9.0.21；WebApi：Pkcs 9.0.21 |

三版測試骨架尚未加入 Aspire.Hosting.Testing，由工作流程按 Hosting 版本補齊；不要把 13.x 的 fixture API 整套貼到 8.2.2 或 9.0.0。.NET 8 的 AppHost SDK／Hosting 不同版是原有設定。原始新增 .NET 9 時保留來源套件，未執行還原、建置、audit 或容器測試；2026-10-18 僅在該版 AppHost 明確參考 KubernetesClient 17.0.14，維持 AppHost SDK／Hosting 9.0.0，傳遞解析 Fractions 7.3.0、YamlDotNet 16.3.0。該版 AppHost、WebApi 與測試骨架在隔離副本完成公開還原、完整相依與弱點檢查，未回報已知弱點，Release 建置為 0 個警告、0 個錯誤。本輪未啟動 AppHost、容器或執行測試／AI 流程，沒有建立執行期相容性的驗證結果，待測程式不變。

三版正式 AppHost 均移除來源的兩個固定具名 SQL／Redis 資料磁碟區，避免不同副本共用舊資料；服務名稱與待測 API 不變，沒有加入持續容器生命週期。本次新增 .NET 9 只做同樣的兩處移除，來源及快照保留。

2026-10-17 先前 .NET 8／10 的隔離檢查涵蓋公開還原、完整相依、audit、真正 AppHost 啟動及每版四項 HTTP／SQL／Redis 行為；Net10 另有 MessagePack／Pkcs 往返檢查。當輪 audit 未回報已知弱點，不是本次重跑，也不適用於新增 .NET 9。沒有驗證遠端 Kubernetes、憑證信任鏈、OIDC／exec 認證或惡意輸入。

歷史 Net10 檢查使用 .NET／ASP.NET Core 10.0.13；shared framework 可能提供實際載入的 Pkcs，不能只看 NuGet 版本推論執行時已修補。版本與容器準備見[容器練習前置條件與隔離](../../guides/containers.md)。
