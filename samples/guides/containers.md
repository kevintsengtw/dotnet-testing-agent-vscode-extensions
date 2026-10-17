# 容器練習前置條件與隔離

這份指南適用於 [Integration：API 操作、單一 Controller 與多個目標](../integration/practice_integration/README.md) 與 [Aspire：指定端點、預約流程與跨服務檢查](../aspire/practice_aspire/README.md)。兩套提供待測 API 與空的 xUnit 測試專案，先完成工具檢查，再用 Claude Full 或 Codex Full 產生測試；Lite 沒有這兩種流程。

## 執行前確認

- 依[工作區準備與工具設定](getting-started.md)建立獨立副本，VS Code、AI 工具及 PowerShell 使用同一個副本根目錄。
- 準備正式版 .NET 10 SDK。各套 `global.json` 使用 `10.0.100`、`latestFeature`、禁止預覽版與 VSTest runner；歷史 .NET 8／10 檢查使用 SDK 10.0.402；本次補入 .NET 9 未重跑建置或容器。
- Net10 需要已修補的 .NET／ASP.NET Core 10 執行階段（歷史檢查使用 10.0.13；Pkcs 10.x 的修補門檻為 10.0.13）；Net8／Net9 情境分別另需 .NET／ASP.NET Core 8／9。歷史 .NET 8／10 檢查使用 10.0.13、8.0.32，沒有 .NET 9 執行結果。
- Docker Desktop 已啟動，使用 Linux containers；歷史檢查使用 Windows x64、WSL2 後端。需要可存取 Docker daemon，以及下載 NuGet 套件及容器映像檔的網路。
- 先確認可用記憶體與磁碟空間；Aspire 會同時啟動 SQL Server、Redis 及 Web API。不要同時執行兩個版本或兩個模式的練習來爭用資源。

以下只有讀取，不會啟動、停止或刪除容器：

```powershell
dotnet --list-sdks
dotnet --list-runtimes
docker context show
docker version
docker info --format '{{.OSType}}'
docker ps -a --format 'table {{.ID}}\t{{.Names}}\t{{.Image}}\t{{.Status}}'
```

確認 Server 可連線、OSType 為 `linux`，並確認 Docker context 是你打算用來練習的本機環境。不能因為 Docker CLI 存在就認定 daemon 可用，也不要自行切換到共用或遠端 Docker。

歷史檢查的工具版本為 Docker Desktop 4.95.0、Engine 29.8.2、PowerShell 7.6.7；這是實際驗證環境，不是最低版本或最新版推薦。其他平台、Windows containers、Windows PowerShell 5.1 尚未驗收。

## Integration：由測試 fixture 建立依賴

- 使用 WebApplicationFactory 與 Testcontainers；容器端點由 fixture 取得並注入，不使用自己原有的 PostgreSQL、MongoDB、Redis。
- Net8 訂單建立情境只需 PostgreSQL；Net9 顧客活動 Controller 使用 MongoDB；Net10 多目標才同時涉及 PostgreSQL、MongoDB 與 RedisCacheService。API 控制器目前沒有呼叫快取服務，不能把訂單 HTTP 成功視為 Redis 已驗證。
- 原始 `appsettings.json` 的 localhost 連線字串只是開發示意，不是測試目標。fixture 設定環境為 `Testing`，注入該次容器的 DbContext／MongoDB／Redis 註冊。
- Testcontainers 使用動態主機連接埠、獨立資料庫／識別與預設 Resource Reaper；不要啟用 reuse、綁定既有資料目錄或關閉 Resource Reaper。
- 先前限定檢查使用 `postgres:16-alpine`、`mongo:7.0`、`redis:7.2-alpine`；標籤不是不可變 digest。測試應記錄實際映像身分，不宣稱每次下載都有相同位元組。

[官方 Testcontainers 建議](https://dotnet.testcontainers.org/api/best_practices/)也說明動態連接埠與 Resource Reaper 的用途。

## Aspire：由 AppHost 編排

- 測試使用 `DistributedApplicationTestingBuilder` 啟動範例 AppHost，以 `app.CreateHttpClient("bookingapi", "http")` 呼叫 API；不另開一套 Testcontainers，也不改用 WebApplicationFactory。
- AppHost 的資源名稱為 `sql`、`BookingsDb`、`cache`、`bookingapi`。連線字串由 AppHost 提供；不要直接執行 WebApi，否則可能缺少 `BookingsDb` 設定或讀到 localhost 快取示意值。
- 三版練習 AppHost 已移除原素材的固定具名資料磁碟區，不掛載既有 Docker 資料，也沒有指定持續存在的容器生命週期。不要為練習重新加入舊 volume 名稱。
- fixture 需等待 SQL／Redis／HTTP 就緒，再建立該次 SQL 資料庫結構；AppHost 啟動不等於 EF Core 資料表已建立。資源等待應有逾時與可診斷輸出。
- Net8／Net9／Net10 的 Hosting 分別為 8.2.2／9.0.0／13.1.2，不應混用 API。Net10 Redis TLS 的測試處理由 fixture 按目前 Full 流程契約處理，不改主機 TLS 設定或待測 AppHost；Net8 不套用 Net10 專用呼叫。
- AppHost 的 UserSecretsId 只是範例識別，不附帶 secrets。fixture 使用本次的測試參數；不要複製原工作區的 secrets、個人設定或固定真實密碼。

[官方 Aspire Testing 說明](https://aspire.dev/testing/overview/)介紹測試 builder 與 AppHost 的角色；官方現行文件的版本可能不同，本範例以專案固定版本及實際檢查為準。

## 結束與失敗處理

fixture 應在成功、失敗及初始化失敗時釋放自己建立的 HttpClient、工廠／AppHost 與容器。資料重設只能針對本次獨立測試資料庫；Redis 使用本次唯一 key，不對既有 Redis 執行清空操作。

不要使用 Docker 全域清理、刪除所有容器／volume，或停止不屬於本次的服務。若中斷後留下資源，先保留錯誤輸出，從容器 ID、標籤及本次日誌確認歸屬；不能僅憑名稱相近便刪除。重新練習可建立新副本，歷史成果與 Docker 資料先保留。

骨架建置成功或零測試退出碼，不代表容器與 AI 工作流程完成。生成測試後應確認非零測試數、實際使用的依賴、HTTP／資料狀態，以及審查結論。

2026-10-17 Integration 兩版 MongoDB.Driver 升級後，四個正式專案的直接／傳遞相依檢查未回報已知弱點，詳見 [Integration：固定版本與必要測試套件](../integration/practice_integration/README.md#固定版本與必要測試套件)。後續新副本已把三個 Testcontainers 模組一致升至 4.14.0，原生解析 SSH.NET 2026.0.0；兩版含工具的完整 audit 無回報、每版五項容器行為通過，詳見 [Integration：測試工具的 SSH.NET 相依修補](../integration/practice_integration/README.md#測試工具的-sshnet-相依修補)。Aspire 則另已完成兩版六個正式專案的限定修補，完整 audit 未回報弱點，AppHost／SQL／Redis／HTTP 檢查通過；版本、Pkcs shared framework 與未驗證的 Kubernetes 認證範圍見 [Aspire：固定版本與版本差異](../aspire/practice_aspire/README.md#固定版本與版本差異)。上述結果只屬先前 .NET 8／10 的歷史檢查，不涵蓋本次新增 .NET 9；本次沒有重跑 audit 或容器。上述結果限本機 Docker，未驗證 SSH／SCP／SFTP 或遠端認證；舊副本與舊警告仍保留，不宣稱已回寫修復歷史環境。檢查使用的工具版本不能直接當成正式環境的套件版本建議。
