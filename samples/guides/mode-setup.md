# 在工具包安裝模式，再到 AI 對話啟動工作流程

這份指南說明工具包與 AI 對話各做哪一段工作。先依[工作區準備與工具設定](getting-started.md)在 VS Code 開啟自己的範例副本；以下以 v0.8.4 面板名稱說明。

## 1. 依你使用的 AI 工具選擇一個模式

| 你要在哪裡送出提示詞 | 工具包要安裝的模式 | 可練習的內容 |
| --- | --- | --- |
| Claude Code 的 VS Code 對話框 | Claude Full | Unit 指定方法、單類別或多類別；另有 Integration、Aspire、TUnit |
| Claude Code 的 VS Code 對話框 | Claude Lite | Unit 指定方法或單一類別；一次只處理一個類別 |
| Codex 的 VS Code 對話框 | Codex Full | Unit 指定方法、單類別或多類別；另有 Integration、Aspire、TUnit |
| Codex 的 VS Code 對話框 | Codex Lite | Unit 指定方法或單一類別；一次只處理一個類別 |

如果你已經在用 Claude Code，就選一個 Claude 模式；已在用 Codex，就選一個 Codex 模式。Full 的 Unit 流程分工分析、撰寫、執行與審查；Lite 將工作集中於較精簡的產生與驗證流程，Lite 每次只指定一個類別。你不需要為了練習同時安裝四模式，也不需要手動為每個角色建立對話。

對照本套 Unit 範例：.NET 8 的方法與 .NET 9 的單類別情境可用 Full／Lite；.NET 10 的 EmployeeService／EmployeeValidator 多類別情境使用 Full。這是本例的情境分配，並不表示 Lite 不能用於其他 .NET 版本的單一類別。

## 2. 在測試工具包安裝工作流程

1. 在 VS Code 的 Activity Bar 點開 **dotnet-testing-agent 工具包**。
2. 開啟 **Claude 模式**或 **Codex 模式**面板。在面板上方「查看模式」選 **Full** 或 **Lite**。
3. 進入 **總覽**。模式尚未安裝時，按所選模式的安裝按鈕，例如 **安裝 Claude Full** 或 **安裝 Codex Lite**；依安裝流程完成操作。
4. 查看「安裝狀態」「已安裝版本」「來源」。單純切換 Full／Lite 只是在查看，按安裝並完成後才會有該模式的工作流程。
5. 在同一模式按 **環境診斷**，閱讀結果中需要處理的項目；例如 CLI 缺少或設定問題，依提示處理後再診斷。

安裝後可切到 **Skills** 與 **Agents** 分頁，查看這次提供的能力與角色。檔案總管可查看共用 Skills 所在的 `.agents/skills/`，以及所選工具的 `.claude/` 或 `.codex/` 等相關檔案；實際部署項目依模式與來源而定，Lite 不必具備與 Full 相同的所有檔案。這就是工具包替你準備的測試工作流程，無須手動從範例外複製 Skills 或 Agent 檔案。

| 你看到的內容 | 在這次練習中做什麼 |
| --- | --- |
| Skills | 提供測試技術指引與工作流程入口；提示詞第一行指定要使用的工作流程 Skill |
| Agents | 定義工作分工。以 Full 的 Unit 為例，分別處理分析、撰寫、執行與審查；由工作流程調度 |
| 相關 scripts／設定 | 支援工作流程的執行與所選 AI 工具設定，由工具包依來源部署 |
| Claude Code／Codex 擴充套件 | 接收你送出的提示詞，讓 AI 使用上述內容與工具讀寫專案、執行測試並回報 |

你不用先讀完每個檔案才開始。先確認模式已裝好，再從一輪練習觀察這些分工如何串起來。

安裝 Extension 是取得工具包；以上操作才是把工作流程安裝到目前的練習工作區。若診斷回報未知檔案衝突，保留原檔並查看[問題排查](troubleshooting.md)，不要直接覆寫。

## 3. 從同一模式複製預設提示詞

1. 保持剛安裝的模式，例如 **Claude Full**；不要在這一步切到另一個模式。
2. 切到 **預設提示詞** 分頁。
3. 找到 **Unit 單元測試**，可展開查看範本，按 **複製提示詞**。
4. 切到 **同一 VS Code 視窗內**的 Claude Code 或 Codex 擴充套件，開啟這份練習工作區的對話。
5. 在對話輸入框貼上剛複製的完整文字，**先修改，還不要送出**。

你貼上的內容應包含「使用……撰寫單元測試」以及被測試專案、類別、範圍、測試專案、說明等欄位。第一行指定工作流程的 Skill，其他欄位是這次的任務資料。

| 模式 | Unit 範本第一行使用的名稱 |
| --- | --- |
| Claude Full | `/dotnet-testing-orchestrator-unit` |
| Claude Lite | `/dotnet-testing-lite-orchestrator-unit` |
| Codex Full | `$dotnet-testing-orchestrator-unit` |
| Codex Lite | `$dotnet-testing-lite-orchestrator-unit` |

保留你從面板複製到的完整第一行及對應名稱。`/`、`$` 與 Skill 名稱是提示詞的一部分，貼在 AI 對話框裡，不是在 PowerShell 執行的命令。

## 4. 修改欄位，再送出

到 [Unit：選擇操作範圍](../unit/practice/README.md#選擇操作範圍)，依所選情境的欄位表，逐項替換 `<...>` 佔位文字。路徑以整份範例副本為根目錄；若仍留下未填寫的佔位文字，AI 就無法明確知道你要測哪個專案。

三個 Unit 情境分別示範方法、單一類別與多類別範圍。指定方法時填「範圍」；指定整個類別時移除此欄；多類別時在「被測試類別」列出各目標並使用 Full。Lite 的方法或單類別情境仍只處理一個類別。

確認 AI 對話能讀取這份工作區，並能依你的工具設定修改檔案、執行建置及測試。若在安裝工作流程之前就開啟對話，請在安裝完成後重新開啟工作階段，讓它讀取剛安裝的內容。

檢查路徑與情境後按送出。這時才由 AI coding agent 依指定 Skill 啟動工作流程，使用相關 Skills、Agents 與工具完成任務；工具包的「複製提示詞」不會替你送出或執行。

## 5. 查看工作流程進度與結果

觀察 AI 是否按指定流程處理，而不是只回覆一段測試範例。Full 的 Unit 可從回覆與工具活動觀察分析、撰寫、執行、審查的分工；Lite 的分工較精簡，不要求出現相同四角色。實際角色名稱、報告與呈現依已安裝來源及 AI 工具而定。

再到指定的 `tests/` 目錄查看測試檔，回到對話查看實際執行命令、成功／失敗數及審查意見。若 AI 只提出計畫而未執行，可在原對話要求繼續依指定工作流程完成；有錯誤時，貼回完整輸出，保留失敗與後續修正。

其他 Full 範本也各有 .NET 8／9／10 三種情境：[TUnit：指定方法、整個類別與 xUnit 移轉](../tunit/practice_tunit/README.md)、[Integration：API 操作、單一 Controller 與多個目標](../integration/practice_integration/README.md)、[Aspire：指定端點、預約流程與跨服務檢查](../aspire/practice_aspire/README.md)。操作仍是「面板複製 → AI 對話貼上 → 依該頁填欄位 → 送出 → 查看成果」；Integration／Aspire 先準備[容器練習前置條件與隔離](containers.md)。三套都使用 Full，Lite 沒有這些流程。TUnit 的方法／類別／移轉情境與 Unit 的 Full／Lite 對應不可混為一談。

## 選用：共存與模型設定

沿用可用的 AI 模型設定即可。完成後如要比較其他模式，先建立另一份乾淨副本，再安裝另一模式；四模式也可以共存於同一工作區，比較不同模式時可自行選擇是否共存。

Codex 使用者如有需要，可在 **Agents** 分頁查看模型設定。Claude Full 的用量統計可能包含 Lite Agent，依診斷提醒閱讀；用量資訊與這次測試是否成功分開看待。
