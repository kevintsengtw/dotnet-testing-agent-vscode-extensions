# 在 VS Code 操作測試工作流程

這份 `samples/` 提供 dotnet-testing-agent 工具包與提示詞的操作範例。你會使用已備好的 .NET 專案，在 VS Code 安裝測試工作流程，從工具包複製預設提示詞，貼到 **Claude Code 或 Codex 擴充套件的對話框**，修改測試目標後送出，觀察 AI 如何分析、撰寫、執行及審查測試。

四種測試工作流程都提供 .NET 8／9／10 三種不同情境；Unit 分別指定方法、單一類別與多個類別，其他流程的情境見下表。依你要了解的使用情境選擇，參照[工作區準備與工具設定](guides/getting-started.md)與 [Unit：選擇操作範圍](unit/practice/README.md#選擇操作範圍)操作。

## 你會實際操作什麼

| 步驟 | 在哪裡操作 | 做什麼、應看見什麼 |
| --- | --- | --- |
| 1. 開啟範例 | VS Code 的「開啟資料夾」 | 開啟自己的範例副本，檔案總管直接看得到 `unit/`、`guides/`、`README.md` |
| 2. 安裝工作流程 | 測試工具包的 Claude／Codex 模式面板 | 依情境選擇模式：Unit 方法／單類別可用 Full 或 Lite，多類別使用 Full；安裝後工作區出現該模式的 Skills、Agents 及相關檔案 |
| 3. 確認安裝 | 同一面板的「總覽」 | 執行「環境診斷」，查看需要處理的項目；也可在「Skills」「Agents」查看安裝內容 |
| 4. 取得範本 | 同一模式的「預設提示詞」 | 找到要練習的測試類型，按「複製提示詞」 |
| 5. 填入這次需求 | 同一 VS Code 工作區的 Claude Code／Codex 對話框 | 貼上範本，依範例指南替換專案路徑、測試目標、測試專案及情境 |
| 6. 送出並觀察 | Claude Code／Codex 對話 | 讓 AI 依指定工作流程進行分析、撰寫、執行與審查；查看工具活動與回覆 |
| 7. 查看成果 | VS Code 檔案總管、對話與測試輸出 | 找到新增的測試、實際執行結果與審查意見，再依[套用到自己的專案](guides/apply-to-your-project.md)換成自己的專案 |

**安裝 Extension、安裝工作流程、送出 AI 提示詞是三個不同動作。** 工具包負責讓你安裝與使用工作流程；Claude Code／Codex 接到你送出的提示詞後，才開始執行測試工作。預設提示詞中指定的 Skill 是這次工作流程的入口，請保留對應模式的原名。

## 選擇操作情境

本表列出操作指南、範例專案的題材與適用模式。四種測試工作流程的練習，都是從工具包複製提示詞、修改目標與情境，再送到 AI coding agent 的對話框執行。兩套 Coverage 則依指南複製命令到終端機執行。

| 操作指南 | 範例情境 | 使用方式 |
| --- | --- | --- |
| [Unit：指定方法、單一類別與多類別範圍](unit/practice/README.md) | 溫度轉換、員工服務與員工資料驗證 | .NET 8／9／10；方法與單類別可 Full／Lite，多類別用 Full |
| [Integration：API 操作、單一 Controller 與多個目標](integration/practice_integration/README.md) | 訂單與顧客活動 API、Redis 快取服務 | .NET 8／9／10；Claude／Codex Full；需先準備[容器練習前置條件與隔離](guides/containers.md) |
| [Aspire：指定端點、預約流程與跨服務檢查](aspire/practice_aspire/README.md) | 由 AppHost 編排的預約 API、SQL Server 與 Redis | .NET 8／9／10；Claude／Codex Full；需要容器 |
| [TUnit：指定方法、整個類別與 xUnit 移轉](tunit/practice_tunit/README.md) | 圖書館書目與 ISBN 驗證、既有 xUnit 測試移轉 | .NET 8／9／10；Claude／Codex Full |
| [MTP 原生 Code Coverage：Step by Step](coverage-mtp-native/README.md) | 既有 TUnit 測試的單一專案覆蓋率與 HTML 報告 | 不需 AI 模式；依[Code Coverage 操作指南](guides/code-coverage.md)操作 |
| [MTP 舊版相容 Code Coverage：Step by Step](coverage-mtp-legacy/README.md) | 既有 TUnit 測試的完整方案覆蓋率與合併 HTML 報告 | 不需 AI 模式；測試已備妥 |

Full 提供 Unit、Integration、Aspire、TUnit；Lite 提供一次一個類別的 Unit 練習。選擇你已有的 AI 工具與一個模式即可，不必同時安裝四模式。兩套 Coverage 是工具包附加功能的獨立練習，可直接操作，不必先讓 AI 產生測試。

## 路徑與取得方式

所有 AI 練習提示詞都以 **本 README 所在的範例副本目錄**為工作區根目錄，例如 `unit/practice/src/...`。請開啟整份副本，讓 VS Code、工具包與 AI 對話共用同一個根目錄。

範例隨公開儲存庫提供，可用 Git clone 或 **Code → Download ZIP** 取得；安裝 VSIX 不會下載這份 `samples/`。取得範例與建立副本的方式見[工作區準備與工具設定](guides/getting-started.md)。

遇到問題時，先看[問題排查](guides/troubleshooting.md)，授權見 [LICENSE](LICENSE)。範例使用固定套件版本；各頁的歷史檢查結果依其日期與範圍閱讀。

## 請 AI agent 協助練習時

讓協助你的 AI 先讀本索引與所選練習頁面，確認工作區根目錄、AI 工具、已安裝模式及這次唯一的練習目標。使用者在 VS Code 完成面板操作並送出填好的提示詞後，AI 依指定的已安裝工作流程執行任務。文件裡各模式與版本的提示詞是供選擇的範例，不代表要一次全部執行；如果找不到工作流程或無法執行，回報實際缺少的項目，保留當次成果。
