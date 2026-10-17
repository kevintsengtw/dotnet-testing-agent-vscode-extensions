# 問題排查

先保留錯誤訊息與目前檔案，再依問題逐項檢查。不要直接刪除 Skill、管理紀錄或自己的設定來略過衝突。

| 現象 | 檢查方式 |
| --- | --- |
| 已安裝 VSIX，專案裡卻沒有工作流程 | 在工具包的 Claude／Codex 面板選 Full／Lite，於「總覽」按所選模式的安裝按鈕。安裝工具包與安裝專案工作流程是不同步驟 |
| 按「複製提示詞」後沒有產生測試 | 複製只把範本放入剪貼簿；切到同一工作區的 Claude Code／Codex 對話框貼上，填完欄位後送出 |
| 不知道提示詞的路徑從哪裡算 | 路徑從 VS Code 開啟的整份範例副本根目錄計算。例如檔案總管最上層有 `unit/`，Unit 路徑就以 `unit/practice/...` 開頭 |
| AI 只有回覆計畫或程式碼，沒有完成檔案與執行 | 確認這個 AI 工作階段可讀寫目前工作區並執行命令；在原對話要求依剛才指定的測試工作流程繼續，查看實際工具活動與執行結果 |
| 把 `/dotnet-...` 或 `$dotnet-...` 貼到 PowerShell 出錯 | 這是 AI 提示詞的工作流程入口，應保留在 Claude Code／Codex 對話框的完整提示詞中送出，不是終端機命令 |
| 提示詞中的 `.csproj` 找不到 | 工作區根目錄應直接包含 `unit/`、`guides/`；確認不是開到儲存庫根目錄或 `unit/practice/src/` |
| 找不到符合 `global.json` 的 SDK | 在 `unit/practice/` 執行 `dotnet --version`；需要正式版 .NET 10 SDK，同主次版本可依 `latestFeature` 選取較新功能帶 |
| Net8／Net9 測試無法啟動 | 查看 `dotnet --list-runtimes` 是否有目標版本的 Microsoft.NETCore.App；Unit 的 Net9 是 Practice.Core／Practice.Core.Tests，SDK 與目標 runtime 分開確認 |
| Unit 溫度轉換的例外預期與原碼不同 | 使用所選情境的提示詞，不能只替換 .NET 版本字樣。[Unit：.NET 8：指定方法](../unit/practice/README.md#net-8指定方法)的 CelsiusToFahrenheit 在低於絕對零度時仍計算數值；[Unit：.NET 9：指定單一類別](../unit/practice/README.md#net-9指定單一類別)的兩個克氏轉換方法依原碼擲出 ArgumentException。不要把其他版本的例外防護套過來 |
| Lite 拒絕同時處理 EmployeeService 與 EmployeeValidator | [Unit：.NET 10：指定多個類別](../unit/practice/README.md#net-10指定多個類別)應選 Claude Full 或 Codex Full；Lite 一次只處理一個類別，使用 Lite 時需拆成各自的請求 |
| Integration／Aspire／TUnit .NET 9 路徑找不到 | 三套 .NET 9 都保留無 `.Net9` 字尾的原名；使用對應指南的實際 .csproj 與方案路徑，不從 Net10 字串直接替換 |
| Codex Aspire 限定情境卻要求全部端點 | .NET 8／9 依指南移除第一行末的「涵蓋全部端點」，保留 Skill 名稱；.NET 10 才使用全部端點情境 |
| Unit .NET 9 路徑找不到 | 使用 `unit/practice/src/Practice.Core/Practice.Core.csproj` 與 `unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj`；來源名稱沒有 `.Net9` 字尾，方案為 `Practice.Samples.slnx` |
| 套件還原失敗 | 保存 NuGet 錯誤碼、套件名稱與來源；檢查網路或組織套件來源，不任意升級所有套件或加入私有來源 |
| 還沒有任何測試 | 起點刻意只有測試專案骨架；先送出 Unit 提示詞。零測試不能記成工作流程通過 |
| TUnit 被當作 xUnit 執行 | TUnit 目標以 `dotnet run --project` 執行；只有獨立的 xUnit 移轉來源使用 `dotnet test`，不要把來源檔或套件貼入目標 |
| AI 工具找不到 Skill | 確認工具、VS Code 與終端機在同一副本，所選模式已安裝並診斷；重新開啟 AI 工作階段後再檢查 |
| `UNMANAGED_CONFLICT` | 從「輸出 → dotnet-testing-agent」查看路徑，保留及備份原檔；不要改名 Skill 或手動接管未知檔案 |
| 面板看到另一模式但無法使用 | 切換查看模式不會安裝；核對所選模式的安裝狀態與 AI 工具 |
| Coverage 無法產生指令 | 核對方案成員、runner 與工作區可讀設定中的覆蓋率套件直接參照；「未找到直接參照」不等於完整建置環境一定缺少套件 |
| Docker CLI 有版本但測試無法啟動 | 確認 Server 可連線、context 正確且 OSType 為 linux；保留錯誤，不清理既有 Docker |
| Integration 缺少 OrderDbContext／MongoDB／Redis | Testing 環境略過原始外部服務註冊；fixture 須注入本次容器的服務，不連 localhost 示意值 |
| Aspire 的 SQL 資料表不存在 | fixture 需等 SQL 就緒並建立本次資料庫結構；AppHost 啟動不會代為執行 EF Core 初始化 |
| Aspire 找不到服務或卡住 | 核對 AppHost／WebApi 角色與 bookingapi 名稱，分別診斷 SQL、Redis、HTTP；Net8／Net9／Net10 使用各自 Hosting 相容的 fixture API |
| NU1902／NU1903 警告 | 保存套件、版本及 advisory；測試通過不會解除弱點，不停用 audit 或任意升級全部相依 |
| HTML 找不到 XML | 先執行同一組 Coverage 並確認成功，再執行 HTML；重新掃描或改選後不能沿用舊組報告指令 |

[Integration：API 操作、單一 Controller 與多個目標](../integration/practice_integration/README.md)／[Aspire：指定端點、預約流程與跨服務檢查](../aspire/practice_aspire/README.md) 需先完成[容器練習前置條件與隔離](containers.md)；MTP 兩種模式的 runner、SDK 同功能帶修補版與 ReportGenerator 要求見[Code Coverage 操作指南](code-coverage.md)。不要把 Unit 的 `VSTest` 設定直接複製成所有範例的共用設定。

如需重新練習，依[工作區準備與工具設定](getting-started.md)建立新副本，保留這次的失敗輸出供比較。
