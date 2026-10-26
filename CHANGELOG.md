# Changelog

## [Unreleased]

- Codex Lite 來源與內建離線快照同步至 v1.2.3，固定 commit `a39f35ee9b1a6b836d65d927592113772af44654`；保留上游 Skill 名稱、部署結構與 v1.2.2 升級來源。Codex Full 維持 v1.3.1，其他來源版本不變。本項尚未發布。

## [0.8.5] - 2026-10-19

- 補足部署及 Codex Agent 設定失敗後的交易回復警告呈現：輸出逐筆列出警告碼與 discard／備份路徑，通知說明原檔復原後仍可能有暫存資料需要人工檢查。
- 環境診斷補上交易備份與 discard 的人工處理順序；兩份 README 提供相同六步說明，先保留資料並核對交易歸屬及實際復原結果，再處理對應 discard、備份與重新診斷。交易、清理及殘留掃描邏輯不變，沒有新增自動清理功能。
- 納入已合併的 Node.js 20 工具鏈與 CI 維護：test-electron 固定 2.5.2、TypeScript ESLint plugin／parser 固定 8.71.2、官方 vsce 固定 3.9.3-5 與相容 lock，Actions 更新並使用 Ubuntu 24.04 runner。
- 六套 Samples 與指南已透過公開儲存庫的 git／ZIP 提供，納入公開同步及 VSIX 排除政策；包含後補 .NET 9 範例的限定相依修補，範例仍不包入 VSIX。
- 同步安裝指南與一般安裝步驟的 VSIX 名稱。四模式與共用 Skills 的內建離線來源沿用 v0.8.4；Claude Lite 共存修正屬 v0.8.4 既有內容，不重列為本版修正。

## [0.8.4] - 2026-10-16

- 修正沿用既有 Skills 後安裝另一模式時，相同內容因模式來源版本不同而被誤報為 `MIXED_VERSION_CONFLICT` 的問題。相同內容可供多個模式沿用，仍不取得覆寫或刪除權限。
- 補上只安裝 Claude Lite 的既有工作區情境：Claude Full v1.7.3／Lite v1.2.0 的 `dotnet-test/SKILL.md` 經來源紀錄與實際雜湊核對後，可沿用相容的正式原檔，不覆寫、不接管。此例外限於已核對的兩版內容；各模式保留自己的原始來源與實際使用雜湊。
- 安裝通知與環境診斷說明沿用的來源差異。未知來源、無有效使用紀錄或經使用者修改的檔案仍以 `UNMANAGED_CONFLICT` 阻擋，前置檢查失敗不寫入檔案。
- 安裝指南改為保留既有 Full、直接安裝 Lite 的驗證步驟，不再將解除安裝 Full 與移走 Skill 當成必要前提。四模式內建離線來源沿用 v0.8.3。
- 補充 2026-10-16 macOS R2 使用者驗證：既有 Full 保留、Lite 安裝完成，安裝後環境診斷畫面正常；manifest 保留四模式，並記錄原 Skill 未接管且由 Full／Lite 共用。此結果不包含 .NET 測試工作流程執行。
- 補充 Windows 11 實際 VS Code 面板驗證：四模式乾淨安裝，以及保留既有 Full／Codex 模式後直接加裝 Lite 均完成。既有 410 個檔案的內容、修改時間與檔案識別碼未變；共存後四模式診斷沒有錯誤，Full 保留既有用量統計範圍提醒。文件整理為 v0.8.4 發布格式。

## [0.8.3] - 2026-10-15

- 工具包新增獨立、預設收合的 Code Coverage Webview；四模式共用入口，未初始化工作流程的工作區亦可使用。
- 靜態掃描 `.sln`／`.slnx` 與 `.csproj`，依方案成員及專案屬性／套件證據列出測試專案；提供多根目錄選擇、重新掃描及無法確認設定的說明。
- 修正 MTP 版本判定：採用適用的 `VersionOverride`，中央版本需配合直接參照及中央版本管理設定；無法確認時不推定為確定版本。
- 辨識直接 `xunit.v3.mtp-v2` 的測試／MTP 證據，將 MTP 主要版本與精確版本分開；保留明確非測試排除、.NET 10／MTP 2 舊版相容限制，版本證據衝突無法經確認略過。
- 明列工作區內靜態掃描的支援界線：套件 `no` 表示未找到直接參照並維持阻擋，`unknown` 仍需人工確認；不將 SDK、傳遞套件或工作區外設定推定為已確認，也不把一般類別庫全部列為測試專案。阻擋訊息不再將未找到參照表述為實際缺少套件。
- 依 VSTest、MTP 原生或 MTP 舊版相容模式產生 Coverage 指令，並提供對應的 ReportGenerator HTML 指令。兩段指令各自複製，共用隔離結果目錄；處理 PowerShell 路徑引號及舊版 MTP 參數轉送。
- 支援同一工作區內方案目錄的個別 `global.json`：依最近的設定辨識 runner，Coverage 指令暫時切換目錄後恢復，HTML 與結果目錄維持工作區根目錄。
- 掃描與複製不安裝工具、不修改專案、不執行測試、不送出終端機命令。覆蓋率工具與 SDK 前置條件須由使用者確認；已知不相容設定停用指令複製。
- Claude／Codex 既有模式面板新增「預設提示詞」分頁，Full 各四份、Lite 各一份，共十份原文公版；提供展開／收合、完整純文字複製、成功／失敗回饋、手動選取及 `.csproj` 路徑範例，離線與尚未安裝模式時亦可使用。複製不啟動工作流程。
- 四模式內建離線來源沿用 v0.8.2。

## [0.8.2] - 2026-10-11

- Codex Full 的 Agents 區塊順序調整為 Unit、Integration、Aspire、TUnit，讓常用的單元測試 Agents 優先顯示。
- Codex Full／Lite 套用或重設 Agent 模型與推理強度，或清單收到背景狀態更新時，保留目前捲動位置與已展開的 Agent 技術資訊，減少連續調整時重新尋找位置的操作。

- 修正具有舊版移轉紀錄的工作區，在解除安裝 Claude Full 或 Codex Full 後，中央部署 manifest 被誤判為無效，導致 Lite 管理或 Full／Lite 重新安裝受阻的問題。歷史移轉紀錄不要求對應模式仍已安裝；目前檔案歸屬、固定來源及完整性檢查仍維持。
- 已受此問題影響的 manifest 可由修正版直接讀取，不需要手動刪除或修改；保留其他模式使用的檔案及使用者自訂內容。
- 四模式內建離線版本沿用 v0.8.1：Claude Full v1.7.3、Claude Lite v1.2.0、Codex Full v1.3.1、Codex Lite v1.2.2，Shared Skills v2.4.5。

## [0.8.1] - 2026-10-10

- 修正模式環境診斷遺漏共用檢查結果的問題：Node.js 或 CLI 缺失會顯示於 Full／Lite 畫面及複製的完整報告，不再誤顯示環境正常。
- 四模式預設離線安裝內容更新為 Claude Full v1.7.3、Claude Lite v1.2.0、Codex Full v1.3.1 與 Codex Lite v1.2.2；來源鎖定、完整檔案及 SHA-256 一併更新。「版本資訊」列出四個內建版本，與工作區已安裝版本分開顯示。
- 「查看 Release」可在未安裝、查詢中或 GitHub API 額度用完時使用，直接開啟所選工作流程模式的官方最新發行頁。

- Codex Full／Lite 套用或重設 Agent 模型與推理強度後，保留目前的 Agents 頁籤與查看模式；自動環境診斷更新結果時不再跳回總覽。
- 四模式依工作區已安裝、內建與可取得的線上版本提供直接升級；GitHub 查詢受限或連線失敗時，仍可升級至較新的完整內建版本，不必先解除安裝。升級保留使用者檔案與 Agent 設定，失敗復原，拒絕降版或來源衝突。
- 移除重複的「管理現有安裝」主按鈕；每個模式保留一個管理入口，依可用版本提供升級安裝、修復與解除安裝。內建最新版與尚未確認的線上最新版分開顯示。

- Codex Full／Lite 的 Agent 模型選項依 Codex 選單順序排列：GPT-6.1 Sol、GPT-6 Astra、GPT-6 Sol、GPT-6 Luna、GPT-5.6 Sol、GPT-5.6 Terra、GPT-5.6 Luna；預選模型及已套用設定保留。

- 修正 Codex Full／Lite 的 Agents 設定頁在既有 v0.8.0 工作區誤報缺少原始內容：模式的固定來源及全部檔案一致時，不因整體內建資源識別改變而阻止模型設定。真正缺少原始內容時顯示對應原因，不再要求重複執行環境診斷。

- 修正線上版本查詢限制處理：依 GitHub reset／Retry-After 在四模式與 Shared Skills 間共用等待時間，安裝重用有效的固定來源查詢。面板與操作結果區分額度限制、逾時、連線失敗及來源驗證失敗；Extension 本身版本檢查先確認 HTTP 狀態與 Release 格式，避免將錯誤回應當作版本資料。

- 修正更新完成後仍等待新版通知選擇而持續占用部署操作的問題；修正背景版本查詢與手動查詢的競爭，避免已完成的手動查詢被誤判為尚未取得線上版本。

- 修正既有 v0.8.0 四模式工作區的升級前診斷與修復：Doctor 依已安裝共用 Skills 的正式版本核對，避免將正常舊版誤報為混版或使用者修改；工作流程原始內容未變時，不因 Extension 整體 bundled 識別變更而拒絕修復。移除模式保留其他模式使用的共用 Skills 版本，並支援已知舊版 shared manifest 的交易內遷移。

- 同一路徑的受管理 Skill 採用最後安裝、更新或修復模式的原始來源內容；保留各模式來源紀錄及所有使用模式，Doctor 依最後部署版本檢查。不同 Skills 根目錄中的同名檔案各自保留，解除安裝時保留其他模式仍使用的檔案。

- 支援 Codex Full v1.3.1 的精確正式來源：部署 Full 專屬 runtime、四套實際用量 HTML 與 NuGet 預檢；升級保留使用者檔案、Codex 設定及逐 Agent 模型選擇，只退役原先受管理的舊 scripts，不部署上游封存檔殘留的舊估算器。內建離線版本同步更新。

- 支援 Codex Lite v1.2.2 的固定來源：用量儀表板加入 GPT-6.1 Sol Standard credit 費率；工作流程在派遣 Agent 前建立結果交付與停止回報目錄。v1.2.1 的 NuGet 還原預檢、既有 Codex 設定與 Agent 模型選擇均保留，內建離線版本同步更新。

- Codex Full／Lite Agent 模型選單加入 GPT-6 Astra，支援 Low、Medium、High、XHigh、Max 推理強度。

- 支援 Codex Lite v1.2.1 的固定來源：NuGet 預檢依原始還原診斷提供處理建議；一般啟動不需 `NUGET_PACKAGES`，升級保留使用者設定及模型選擇。

- Codex Full／Lite Agent 模型選單加入 GPT-6.1 Sol，支援 Low、Medium、High、XHigh、Max；預選仍為 GPT-6 Sol，既有套用值不變。

- 內建 Shared Skills 更新至 v2.4.5 固定來源：承接前版修正，更新 Web API／Aspire 測試範本與 TUnit、FakeTimeProvider 等說明。Web API 範本移除 `ResetTime()`；重新複製範本時原有呼叫須調整。使用者自有檔案保留，四模式內建版本同步更新。

- 支援 Claude Full v1.7.3 的固定來源：承接四套工作流程及 token 用量去重修正；獨立規則文件增至 4 份，並保留 v1.7.2 已安裝來源的升級辨識。Full／Lite 共存的上游用量範圍限制仍由環境診斷提示。

- 工作流程更新依固定上游來源處理 scripts 的變更，Skills 沿用來源原有名稱與目錄。

- 支援 Codex Lite v1.2.0 的固定來源：新增 NuGet 沙箱還原預檢，並採用來源指定的 GPT-6 Sol／Medium Lite Agent 設定。

- Codex Full／Lite 的 Agent 模型選單加入 GPT-6 Sol 與 GPT-6 Luna；未自訂 Agent 預選 GPT-6 Sol，已套用的設定仍維持原選擇。

- 補齊 Claude Full v1.7.2 的安裝、升級、修復與移除驗證；面板將 16 個 Agent 與 2 份規則文件分列，並呈現此來源無 hooks／settings 的契約。Full／Lite 共存時，Doctor 提示 Full v1.7.2 上游用量統計可能納入 Lite 的限制。

- 加入 Claude Lite v1.2.0、Codex Lite v1.2.0 的固定來源契約與來源映射檢查；舊版來源資料僅供既有工作區辨識與修復，新安裝使用本版指定的四模式內建版本。
- 依固定來源宣告處理可省略設定、canonical Skills 需求及 Codex Lite 的 14 個專屬 Skills，並承接 Claude Full v1.7.2 共存來源。
- 修正升級退役、跨模式 consumer、Codex 設定保留、Doctor 來源證據與未知檔案診斷。Claude Lite 索引保留來源內容與 `.agents/SKILLS-INDEX.md` 原路徑，提示 Full 共存的上游檢查限制。

---

## [0.8.0] - 2026-08-28

### Added

- 新增 Claude Lite 與 Codex Lite；Full／Lite 可同時安裝、更新、診斷與移除，並共用 canonical `.agents/skills`，不在模式目錄複製共用 Skills。
- 新增中央 ownership manifest、四模式 source-planned deployment、完整 bundled fallback、online exact-commit staging、原子交易與 rollback。
- Codex Full／Lite 的每個受管理 Agent 可獨立選擇 GPT-5.6 Sol／Terra／Luna 與 Low／Medium／High／XHigh／Max 推理強度；支援唯讀 preview、VS Code 原生確認、per-Agent reset 與 Full／Lite 隔離。
- 啟動與操作完成後檢查 Shared Skills 加四個工作流程來源；更新通知支援查看 Release、稍後與忽略指定版本。

### Changed

- Shared Skills 更新至 `v2.4.2`（`715400f6`）；Codex Full orchestration 更新至 `v1.2.1`（`2f2cde2d`）。Claude Full 維持 `v1.5.0`，Claude Lite／Codex Lite 皆鎖定 `v1.0.0`。
- Claude／Codex 面板以 Full／Lite 按鍵切換檢視；目前模式使用明確的 primary 樣式與「✓ 目前」，安裝狀態另以「已安裝／尚未安裝」顯示。
- Codex Agents 清單與模型設定合併為同一組卡片；Full 依 Aspire、Integration、TUnit、Unit 分組，Scripts 與 Codex Config 收進技術詳細資訊。
- Doctor、版本狀態與面板改由中央 manifest、exact source identity、managed asset 與 per-Agent metadata 統一判定。

### Fixed

- v0.7.0 Codex Full 升級會在完整 preflight 後以正式 exact-source reconciliation 接管原本 37 個 managed assets；使用者修改或來源不符時仍 fail closed。
- Codex Agent source bytes 可在 online staging cleanup 後依 exact base hash 安全重建，合法 override 可跨 repair／source rebase 保留。
- Claude Full／Lite 切換不再誤讀只有 Codex 才存在的 Agent preview 元件。
- 修復 root scripts ownership、unmanaged-identical reuse、shared reference counting、Doctor provenance presentation、部署 repair idempotence 與各交易 fault point的 rollback 契約。

---

## [0.7.0] - 2026-08-10

本正式版彙整 `0.7.0-beta.1` 至 `0.7.0-beta.6` 的跨 Agent 目錄遷移、部署安全、內容可追溯性與實機驗證成果，並改鎖 Claude／Codex orchestration 的正式 Release。

### Breaking changes

- 移除 GitHub Copilot 模式、`.github/skills`／`.github/agents` 初始化、local RAG、MCP provider 與相關命令、設定及面板。
- 共用技術 Skills 與公開 `unit-test-scenarios` 的 canonical path 改為 `.agents/skills`；`.claude/skills` 與 `.codex/skills` 只保留模式專屬 Skills。

### Added

- 新增 shared Skills installer、獨立 manifest、精確來源 lock、SHA-256 integrity manifest 與 bundled／VSIX 內容 gate。
- Claude／Codex 初始化與「更新／修復」改為 bundled-first 原子交易；失敗時一併 rollback shared 與 mode 資產，未知自訂檔案維持不動。
- Doctor 與版本資訊面板新增 shared／mode hash、legacy path、混合版本與 managed-file set 驗證。

### Changed

- Claude orchestration 改鎖正式版 `v1.5.0`（`8ef1a1c6`）。
- Codex orchestration 改鎖正式版 `v1.2.0`（`c2d800c2`），隨附 16 個 Agents、5 個模式 Skills 與 13 個 runtime scripts。
- Shared Skills 維持 `v2.4.1`；`unit-test-scenarios` 維持公開 repository 的 `d0050198` 精確 commit。
- 發佈頻道切換為 `stable`；所有 component version、來源 ref 與 integrity hash 由同一次同步產生。

### Fixed

- 納入 beta 驗證期間確認的 Reviewer 強制執行、Analyzer dispatch telemetry、Unit acceptance、重試用量歸屬、跨平台清理與 Phase 5 收尾契約修正。
- 版本資訊面板不再因 Extension 版本號變更而誤報兩種模式皆需重新部署，改由實際 manifest 與內容完整性判定。

---

## [0.7.0-beta.6] - 2026-08-07

### Fixed

- **Claude 的 Integration 工作流程不再遺漏後置清理**：orchestration 來源重新鎖定至 `3ae94c50`。四種工作流程的流程宣告都要求「Phase 0 → 階段 1～4 → Phase 5」，但 Integration 連續兩次執行完畢即結束，未執行 Phase 5，測試專案因此留下暫存交接檔案。四份 orchestrator 在該處的措辭、章節位置與前後順序經逐項比對皆相同，無可歸咎的文字差異。

  修法為把 Phase 5 納入**強制輸出契約**：於必要輸出表明列其為 Token 表格之後的最後一步、於自我檢查清單新增結束前的攔截點、並要求輸出一行清理狀態。本專案內已有直接對照——標記為「強制輸出」的 Token 用量表格從未被跳過，僅列為必要動作而無輸出要求的 Phase 5 則會被跳過。

- **Claude 的清理指令改用跨平台方式並加上驗證**：先前以 `rm -rf` 執行，Windows 路徑尾端的反斜線會跳脫結尾引號，指令在解析階段即失敗、不會被送進 shell，既有的 `cmd /c rd` 備援也因此攔不到。改用 `node` 的檔案系統 API，並在回報完成前先確認目標目錄確實消失；未通過驗證時回報清理未完成並附殘留清單，不重試。此變更同時涵蓋前置與後置清理。

### Changed

- 清理狀態行一律依實際驗證輸出產生，不得推定寫入。流程未執行卻回報完成，較諸缺失更難察覺。

---

## [0.7.0-beta.5] - 2026-08-07

以 beta.4 對四種測試工作流程 × 兩個模式共八種組合進行實機驗證，全部功能通過。本版納入驗證過程中發現的上游修正。

### Fixed

- **Claude 的 TUnit 與 Aspire 不再可能跳過品質審查**：orchestration 來源重新鎖定至 `42265641`。這兩份 orchestrator 的階段 4 章節帶有【可略過】標籤與「首次全數通過即跳過 Reviewer」的條件，與同一份文件前面「Reviewer 一律執行」的硬性禁令直接矛盾。實測顯示矛盾造成的是**行為不確定**——措辭逐字相同的兩份文件，TUnit 跳過了、Aspire 沒跳過。測試通過不等於測試寫得好，Reviewer 檢查的命名、斷言方式與場景覆蓋都不會使 `dotnet test` 失敗。本版移除該標籤與跳過條件，四份 orchestrator 措辭一致。Unit 與 Integration 原本就無此問題。
- **Codex 的 Reviewer 裁決欄位補上防禦性規則**：orchestration 來源重新鎖定至 `b8b36b97`。`gateDecision` 的判定規則原本只在「使用者有提供測試情境」的段落中給出，本版補上未提供情境時的判定依據，並明訂不得僅因缺少情境或測試全數通過就填入通過。**此為預防性補強，不對應任何已知問題**——見下方更正說明。
- **Codex 的 token 用量估算不再偏高**：output 估算將同一份交接檔案同時計入「寫出檔案」與「artifact」兩個欄位，形成重複計算。修正後每份寫出檔案只計一次，估算結果的 `schemaVersion` 升至 2，因此新舊數值不可直接比較。
- **Codex 重試階段的用量歸屬修正**：階段逾時後自動重派時，產出歸屬被記在逾時失敗的那次，實際完成的重派則為零。

### Changed

- **Codex 的暫存目錄清理行為一致化**：TUnit、Integration、Aspire 與 Unit 統一在流程結束後清除 `executor-result` 暫存目錄，其餘交接檔案與執行紀錄保留供查驗。清理改用跨平台可靠的方式執行，先前在 Windows 非 bash 環境可能失敗。

### 更正說明（2026-08-07）

本版初次發佈時，曾將 Codex 的 `gateDecision` 描述為「四種工作流程實測有兩種未寫入」的缺陷修正。後續查證推翻此描述：

- 四種工作流程**各有獨立的 Reviewer agent 定義**，`gateDecision` 僅存在於單元測試工作流程的契約中。TUnit 與 Integration 未產出該欄位屬正確行為，非缺漏。
- 實測確認 Analyzer 在使用者未提供測試情境時仍會自行建立情境目錄，因此「缺乏判定依據」的情況不會發生。

`b8b36b97` 對該欄位的修改因此屬預防性補強，無害但非必要。同版的暫存目錄清理一致化與 token 估算修正則為真實且已驗證的改善，Claude 的品質審查修正亦經實機重測確認生效。

---

## [0.7.0-beta.4] - 2026-08-06

### Fixed

- **Codex Unit 工作流程的驗收裁決**：orchestration 來源重新鎖定至 `68c68252`，納入上游對 Reviewer `gateDecision` 契約的修正。先前 Reviewer 的 agent 定義從未宣告該欄位，但 Unit 的驗證器要求它必須有值，契約因此拆在兩處且不一致——Reviewer 完成審查卻未留下正式裁決時，acceptance gate 會判定不通過。本版更新 Reviewer agent 定義、Unit orchestrator 說明與 Unit 驗證器，使該欄位成為明確要求。Claude 模式與 Codex 的 TUnit／Integration／Aspire 不受此問題影響。

---

## [0.7.0-beta.3] - 2026-08-06

### Fixed

- **面板不再誤報部署落差**：原本比對 `manifest.extensionVersion` 與目前 extension 版本，只要版本號 bump，兩個模式都會顯示落差並要求重新初始化，不論該模式內容是否真的變更。改為使用既有的三個 `validateManifest()` 判定——與環境診斷同一份驗證，兩者不再可能給出矛盾的答案。
- **共用 Skills 納入面板視野**：`SharedSkillsInstaller` 先前未被版本資訊面板引用，`.agents/skills` 的部署狀態完全不顯示。
- **prerelease 使用者可收到更新通知**：`/releases/latest` 端點排除 prerelease，導致安裝 beta 的使用者查到的永遠是 v0.6.0，面板顯示「最新版本 v0.6.0 ✓ 已是最新」並附上舊版說明。beta 通道改查 `/releases` 清單並以 semver 挑選，因此同時收得到新 beta 與新正式版通知。正式版使用者行為不變，不會被 prerelease 打擾。

### Changed

- **版本資訊面板改用內容版本呈現**：「部署落差」區塊改為「測試工作流程內容」，恆常顯示四列（共用技術 Skills、unit-test-scenarios、Claude 工作流程、Codex 工作流程），每列顯示 extension 帶的來源版本與工作區實際狀態。使用者可直接看出目前套用的是哪個版本的工作流程內容，不需從 extension 版本號反推。
- 發佈頻道改由 extension 自身版本號推導（含 prerelease 標籤即 beta），不新增設定項。`verify-bundled` 斷言 `bundled-lock.json` 的 `channel` 與版本號推導結果一致，避免兩者各自漂移。

---

## [0.7.0-beta.2] - 2026-08-06

### Fixed

- **Codex 四種工作流程的 Analyzer 階段 telemetry**：orchestration 來源重新鎖定至 `33a421ef`，納入上游新增的「Analyzer dispatch transaction」硬閘門。`dispatchIssuedAt` 寫入失敗不得啟動 Analyzer、`dispatchAcceptedAt` 寫入失敗不得進入 Writer，且進入 Writer 前強制檢查五個 telemetry 欄位皆非 null。修正 v0.7.0-beta.1 驗證期間發現的「Analyzer 階段耗時缺失、timing evidence incomplete」問題。Claude 側不受影響。

### Changed

- `verify-bundled` 移除對 Codex Unit orchestrator SKILL.md 的內容斷言（allowlist 措辭、章節順序、canonical 命令字串）。驗證工作流程契約的正確性屬 orchestration repository 的職責，非 extension；該斷言僅存在於八個 orchestrator 中的一個，且會在上游合理演進契約時誤報為同步失敗。出貨層級的把關改由 `known-upstream-issues.json` 機制負責。

---

## [0.7.0-beta.1] - 2026-08-05

此版本取代未發佈的 `0.7.0-alpha`（該版本從未建立 tag 或 Release）。

### Breaking changes

- 移除 GitHub Copilot 模式、`.github/skills`／`.github/agents` 初始化、local RAG、MCP provider 與相關命令、設定及面板。
- 共用 Skills canonical path 改為 `.agents/skills`；`.claude/skills` 與 `.codex/skills` 僅保留模式專屬 Skills。

### Added

- 以精確 commit 鎖定 Claude、Codex lab branch 快照，並加入來源 lock、檔案 allowlist 與 SHA-256 integrity manifest。
- 從公開 `kevintsengtw/unit-test-scenarios` repository 獨立同步 `unit-test-scenarios`。
- 新增 shared Skills installer 與獨立 manifest；初始化 Claude／Codex 時與模式資產一併交易式部署。
- Doctor 新增 shared/mode hash、legacy shared path、缺少 runtime 資產及混合版本診斷。
- Doctor 與 Codex 面板支援顯示 exact-commit 綁定的 release-blocking 上游契約問題；manifest asset-set、snapshot version 與 managed-file set 也納入驗證。

### Changed

- Claude／Codex 初始化改為部署 30 個共用 Skills 至 `.agents/skills`，並保留未知自訂 Skills。
- Alpha 面板顯示固定快照，不查詢或以舊版 public orchestration Release 覆寫 lab 內容。
- 已初始化模式的入口顯示為「更新／修復」，重複執行會以同一原子交易修復 managed content 並保留自訂 Skills。
- Codex runtime scripts 依鎖定 lab 快照收斂為 `run-state.mjs`、token estimator 與 unified read-scope validator。
- Codex lab snapshot 重新鎖定至 Unit 啟動契約修正版，統一為 `Phase 0 → Phase 0.5 run-state init → Analyzer`，並清除已解決的 release-blocking issue。
- **orchestration 同步來源改為公開 repository**：`claudeOrchestration` 與 `codexOrchestration` 由私有 lab repository 改指 `dotnet-testing-agent-orchestration-claude` 與 `dotnet-testing-agent-orchestration-codex` 的 `beta/agent-modes` 分支 exact commit。四個同步來源自此全部公開，任何人 clone 本 repository 皆可重建並驗證 `bundled/`，不再需要私有存取權。
- `bundled/alpha-source-lock.json` 更名為 `bundled/source-lock.json`、`scripts/sync-bundled-alpha.mjs` 更名為 `scripts/sync-bundled.mjs`，發佈頻道改由 lock 檔的 `channel` 欄位表達；`npm run sync-bundled-alpha` 別名移除。
- 各元件 `version` 前綴改由頻道常數決定（`beta-<commit12>`），與 `channel` 欄位不再可能不同步。

### Fixed

- `sync-bundled` 的 git fetch 逾時由 300 秒放寬至 900 秒。`dotnet-testing-agent-skills` 約 103 MB，`--depth 1` fetch 實測需 380 秒以上，原值會在網路較慢時被 SIGTERM 中止。
- `sync-bundled` 清理暫存目錄失敗不再覆蓋真正的錯誤。Windows 上 git pack 檔為唯讀會使 `rmSync` 拋 EPERM，先前會取代 try 區塊的實際例外而導致誤診。

### Added（發佈流程）

- `release.yml` 對含 prerelease 標籤的版本號（含 `-`）自動帶入 `--prerelease`，正式版三段數字則不受影響。

### Validation status

- bundled integrity、TypeScript、ESLint、unit tests 與 Extension Host tests 已納入候選版 gate。
- 正式發布前仍需完成乾淨／升級 workspace 與四種測試工作流程 smoke test。

---

## [0.6.0] - 2026-07-21

### Added

- **三模式統一工作流程版本檢查**：啟動、模式面板顯示及初始化完成後，依 workspace manifest 比較共用 Skills 與 Copilot／Claude／Codex orchestration 的最新正式 GitHub Release
  - 成功取得新版時提供「立即更新」、「查看 Release」與「略過此版本」
  - 加入 8 秒 timeout、15 分鐘成功快取、短暫失敗快取及同一請求 in-flight 去重
  - 檢查與部署共用同一份固定 UpdatePlan，避免 latest 在更新途中改變
- **交易式更新與 rollback**：工作流程更新前備份受影響內容，下載或部署任一步驟失敗時還原 workspace 與 manifest
- **Claude／Codex managed-file manifest**：記錄 Extension 實際部署的檔案，後續更新會移除已由新版 Release 淘汰的受管理檔案，同時保留不在清單內的使用者自訂內容
- **可重現的 bundled lock**：新增 `bundled/bundled-lock.json`、正式 Release tag 同步工具及 bundled／VSIX 檔案清單驗證
- **CI 品質 gate**：PR 與 Release 執行 bundled 驗證、型別檢查、零 warning lint、單元測試、VSIX 內容驗證與候選套件建置
- 新增接手維護規範 `AGENTS.md` 與完整發布 CHECKLIST 文件

### Changed

- **初始化改為 bundled-first**：三模式可在無網路時以 VSIX 內建正式版本完成部署，再於背景檢查最新版
- **離線背景檢查完全靜默**：啟動或切換面板遇到離線、DNS／proxy／certificate、timeout、HTTP 或無效回應時只寫 debug log，不顯示 Error／Warning toast；只有使用者主動初始化或確認更新時才提示
- **安全部署**：保留使用者自訂 skills、agents 與 scripts；Claude hooks 採結構化 merge，Codex config 保留既有設定；無效設定檔不再被覆寫
- v0.5.1 升級時會清理由舊封裝留下且完全沒有檔案的 `skill-creator-advanced` 目錄；若目錄內有任何使用者檔案則保留
- 使用者確認更新但下載／部署失敗後，可在同一個 VS Code session 重新檢查與重試；備份或 rollback 本身失敗時會顯示明確警示
- Claude／Codex 更新成功後會以靜默模式執行 Doctor 並刷新畫面；Copilot 模式維持既有行為
- `isInitialized` 改為驗證 manifest、必要路徑、數量與 mode-specific 元件，不再只依 Extension 版本判斷
- Release notes 面板無法連線時顯示中性狀態，切換頁籤後可重新嘗試
- 版本比較支援 `v` 前綴、prerelease 與 build metadata，無效版本不再產生誤報
- 移除未實際生效的 Claude／Codex Skills 版本設定，工作流程更新固定追蹤最新正式 Release

### Bundled content

- Shared Skills：v2.4.1
- Copilot orchestration：v2.0.1
- Claude orchestration：v1.4.0
- Codex orchestration：v1.1.0（含 `unit-test-scenarios` 與 10 個 validator scripts）

---

## [0.5.1] - 2026-07-08

### Added

- **版本資訊面板新增「剛更新」提示橫幅**：偵測到 extension 版本與上次啟動不同（`globalState` 比對）時，於「版本資訊」面板頂端顯示橫幅，提醒「若側邊欄未看到新面板（如 Codex 模式），請**完整結束 VS Code 再重新開啟**，不能只用 Reload Window」。橫幅可手動關閉，完整重啟後版本一致即自動消失
  - 背景：以 VSIX 就地覆蓋更新且該版**新增了 Sidebar 面板**時，VS Code 會沿用舊版面板容器貢獻點快取，只 Reload Window 新面板不會出現；此提示放在永遠可見、不受該快取影響的「版本資訊」面板，比右下角 toast 更不易被忽略
  - 新增 globalState 鍵 `dta.lastSeenVersion`；`AboutWebviewProvider` 接收 `justUpdated` / `updatedFrom`

### Changed

- **更新指引補充「安裝新版後需完整重啟 VS Code」**：README / public-readme-draft / v0.5.0 安裝說明明確提醒——新增 Sidebar 面板的版本以 VSIX 就地覆蓋後，只 Reload Window 新面板不會出現，需完整結束 VS Code 再重開（非必要不需解除安裝）

---

## [0.5.0] - 2026-07-07

### Added

- **新增 Codex 模式**：在同一個 Extension 中新增第三種 AI coding agent 模式，將 `dotnet-testing-agent-orchestration-codex`（v1.0.1）的 1+4 測試 orchestration 部署到 workspace `.codex/`，架構鏡像 Claude 模式
  - 指令 `dta.codex.init`（初始化 Codex Agent 環境）、`dta.codex.doctor`（Codex 環境診斷）
  - Sidebar 新增「Codex 模式」WebviewView（`dta.codex.webview`，3 分頁：總覽 / Skills / Agents）
  - `CodexInstaller`：線上優先（orchestration-codex Release + 共用 skills）+ bundled fallback + init 後背景更新檢查
  - 部署內容：`.codex/skills/`、`.codex/agents/`（16 個 `.toml`）、`.codex/scripts/estimate-token-usage.mjs`、`.codex/config.toml`
  - **config.toml 採「補差異」合併**（`src/utils/toml-merge.ts`，零依賴）：workspace 不存在時整檔複製、已存在時只補上缺少的 key 並保留使用者既有值
  - doctor 6 項檢查：Node.js、`.codex/skills`、`.codex/agents`、`.codex/config.toml`、token 估算引擎、`codex --version`；前述檔案類項目支援一鍵自動修復
  - 版本資訊面板（`dta.about`）的部署落差偵測與「重新初始化」按鈕新增 Codex 模式
  - 新增 `npm run sync-bundled-codex`（`scripts/sync-bundled-codex.mjs`）同步 `bundled/codex/`
  - 設定 `dta.codex.skillsVersion`、型別 `CodexManifest`、manifest 為 `manifest-codex.json`
  - 與 Claude 差異：agents 為 `.toml`、無 hooks、平台設定為 `config.toml`、CLI 診斷為 `codex --version`

### Changed

- **`bundled/codex/` 同步至 orchestration-codex v1.0.3**：修正「Codex VS Code 擴充下工作流程 telemetry（各階段耗時 / Estimated Token Usage）間歇 unavailable」的問題
  - **真正根因（v1.0.3 修正）**：`estimate-token-usage.mjs` 逐 assignment 對 artifact 路徑 `JSON.parse`，但 Writer canonical artifact 偶被 stamp 成生成的 `.cs`（非 `writer-result.json`），`JSON.parse` 拋未捕捉例外 → 估算器整份崩潰、`token-usage-estimate.json` 不產生 → telemetry 間歇 unavailable。v1.0.3 把 `readJsonIfFile` 包 try/catch（parse 失敗回 `null`，`.cs` raw token 仍計入、僅該 assignment confidence 降級），並移除 Claude 移植遺留的死碼 `.codex/hooks.json`（與 run-state/token 無因果）
  - 上游 v1.0.3 驗證：unit/tunit/integration/aspire × Win/mac × CLI/Ext 共 16 格遙測全通過
  - 承接自 v1.0.2 的 `.codex/scripts/run-state.mjs`（確定性 run-state 寫入器）：Codex 模式 `doctor` scripts 檢查同時驗證 `run-state.mjs` 與 `estimate-token-usage.mjs`；Sidebar 面板一併列出 `run-state.mjs`；`deployScripts` 部署整個 `.codex/scripts/`（兩支腳本自動納入，無需改部署邏輯）
- **`bundled/claude/` 同步至 orchestration-claude v1.4.0**

---

## [0.4.0] - 2026-06-10

### Added

- **Sidebar 新增「版本資訊」面板**：在 `dta-explorer` 容器最上方新增與模式無關的 WebviewView（`dta.about`），解決「面板上看不到目前安裝版本、也無從得知有無新版」的問題（本 extension 不走 Marketplace、無自動更新提示）
  - **目前安裝版本**：來自 `packageJSON.version`，永遠可見、不依賴 init
  - **最新 release**：`VersionChecker.fetchLatestRelease()`（沿用 `resolveReleaseApiUrl`，未設 Gitea 時打公開 repo Releases API），顯示版本 / 發佈日期 / release note + 狀態徽章（已是最新 / 有新版）+「在 GitHub 查看 / 前往下載」連結
  - **部署落差提示**：已初始化但 `manifest.extensionVersion` 與目前安裝版本不一致的模式，顯示「部署於舊版」與「重新初始化」按鈕（點擊後跑對應 init，落差即時消除）
  - 啟動時的更新通知 toast 維持原樣，與本面板互補
  - `isNewerVersion` 抽至 vscode-free 的 `src/utils/semver.ts`，供 provider 與單元測試共用

---

## [0.3.0] - 2026-06-10

### Added

- **Claude 模式新增 `.claude/scripts/` 部署類別（token-usage 引擎）**：支援 `dotnet-testing-agent-orchestration-claude` v1.1.0+ 的「測試工作流程 Token 用量記錄」功能。Claude 模式 `init` 後，orchestrator 跑完 1+4 流程會自動呈現 Token 用量表
  - `ClaudeInstaller.deployScripts()`：部署 `bundled/claude/scripts/` → workspace `.claude/scripts/`；對 bundled 缺源容錯（回 `{ installed: false }` 不中斷），online `deployOrchFromRelease` 一併加入 `.claude/scripts` 對應
  - `dta.claude.init` 新增「Scripts 部署」步驟並寫入 manifest（`ClaudeManifest.scripts`）
  - `dta.claude.doctor` 新增「`.claude/scripts` 目錄（token-usage 引擎）」診斷項，檢查 `token_usage.js` 是否存在，支援自動重新部署修復
  - Claude Sidebar Webview 的 Agents 分頁新增「Token 用量引擎」存在性列
  - `scripts/sync-bundled-claude.mjs` 同步上游 `.claude/scripts/` 至 `bundled/claude/scripts/`
  - 新增常數 `WS_CLAUDE_SCRIPTS_PATH`、型別 `ClaudeManifest.scripts`

### Changed

- **`bundled/claude/` 同步至 orchestration-claude v1.1.1**：含 token-usage 引擎的 self-location 修正（改以 runtime 權威 `CLAUDE_CODE_SESSION_ID` 跨 `~/.claude/projects/*/` 定位 session transcript，修正 Windows 路徑正規化 `_`→`-` 導致 IDE 環境找不到 session 而略過 Token 表的問題）

---

## [0.2.1] - 2026-06-04

### Fixed

- **修正 Extension 更新通知預設失效**：版本檢查（`VersionChecker.check()`）原本要求 `dta.giteaUrl` 與 `dta.giteaRepo` 皆設定才執行，而兩者預設皆為空，導致使用者預設永遠收不到 Extension 更新通知。現改為未設定 Gitea 時，預設改打 GitHub 公開 repo（`dotnet-testing-agent-vscode-extensions`）的 Releases API，免設定即可收到更新通知。URL 決策邏輯抽為純函式 `resolveReleaseApiUrl()` 並補上單元測試（涵蓋未設定、僅設定其一、皆設定等情境）

### Added

- README 新增「更新方式」章節，說明 `.vsix` 發佈不會自動更新、自動偵測新版機制與手動更新步驟（內部 `README.md` 與發佈用 `public-readme-draft.md` 同步）

### Removed

- 自 `bundled/shared/skills/` 移除 `skill-creator-advanced`（與 .NET 測試無關的 meta skill）。`SKILLS_EXCLUDE` 過濾規則保留，作為日後 `sync-bundled` 從上游再次帶入時的防線

---

## [0.2.0] - 2026-06-01

### Added

- **Claude 模式**：在單一 Activity Bar 容器（`dta-explorer`）中新增「Claude 模式」面板，與既有的「Copilot 模式」面板並存於同一個 extension，互不干擾
  - `dta.claude.init`：4 步驟初始化（前置環境檢查 → 部署 Skills → 部署 Agents + Hooks → 寫入 Manifest）
  - `dta.claude.doctor`：5 項環境診斷（Node.js、`.claude/skills/`、`.claude/agents/`、`.claude/hooks/`、Claude Code CLI）；支援自動重新部署修復
  - Claude Sidebar WebviewView（3 分頁：總覽 / Skills / Agents）與 TreeView 狀態面板（顯示 Skills / Agents / Hooks 數量）
  - Claude 模式部署至 `.claude/` 路徑（與 Copilot 的 `.github/` 完全獨立），**不需要** mcp-local-rag 或 RAG 索引
  - 獨立 Manifest（`.dotnet-testing-agent/manifest-claude.json`），不影響 Copilot manifest
- **`bundled/` 目錄重構**：29 個共用 Skills 統一存放，消除各模式重複複製的問題；架構支援未來加入 Codex 等新模式
  - `bundled/shared/skills/` — 所有模式共用的 Skills（由 `sync-bundled` 管理）
  - `bundled/copilot/` — Copilot 模式專用（`orchestration/`、`version.json`）
  - `bundled/claude/` — Claude 模式專用（`skills/` orchestrator skills、`agents/`、`hooks/`）
- **`scripts/sync-bundled-claude.mjs`**（`npm run sync-bundled-claude`）：從 `dotnet-testing-agent-orchestration-claude` repo 同步 Claude 模式專用內容（orchestrator skills、agents、hooks）
- **`src/utils/file-deploy.ts`**：提取 `copyDirRecursive()` 與 `countFiles()` 為共用工具，供各模式安裝器使用
- **`dta.claude.skillsVersion`** 設定：指定 Claude 模式 Skills 版本 tag（預設 `"latest"`）

### Changed

- **整合 Copilot 與 Claude 介面至單一 Activity Bar**：兩種模式收斂為同一個 extension、同一個 `.vsix`，不再各自獨立發佈
- **發佈流程改為統一 tag 系列**：release workflow 觸發條件由 `copilot/v*` 改為 `v*`，產出的 `.vsix` 改名為 `dotnet-testing-agent-<版本>.vsix`（移除 `-copilot` 後綴）；舊的 `copilot/v0.1.2` tag 保留不再延伸

---

## [0.1.2] - 2026-05-12

### Added

- **離線安裝模式（`dta.mcpInstallMode`）**：init Step 4 前新增安裝模式選擇（QuickPick），預選目前設定值
  - `online`（預設）：由 mcp-local-rag 自動從 HuggingFace 下載 embedding model
  - `offline`：從 orchestration release 下載預先打包的 `Xenova-all-MiniLM-L6-v2.zip`，或由使用者手動指定本機 zip 檔；解壓至 workspace `.mcp/cache/` 目錄，不需連線 HuggingFace
- **ModelSetup service**（`src/services/model-setup.ts`）：負責離線 embedding model 下載、解壓（Windows 用 PowerShell `Expand-Archive`、macOS/Linux 用 `unzip`）與完整性驗證
- **`.gitignore` 新增 `.mcp/cache`**：離線 model 快取目錄自動排除於版控之外

### Changed

- **Skills 部署失敗改為致命錯誤**：v2.0.0+ Orchestration 相依 Skills 定義檔，Skills 部署失敗時 init 立即中止並顯示錯誤訊息（原本為繼續執行）
- **MCP Provider `CACHE_DIR` 改為 workspace-local**：從 `~/.dotnet-testing-agent/cache` 改為 `<workspace>/.mcp/cache`，支援多 workspace 各自獨立的 model 快取
- **MCP Provider 改為動態讀取 workspace path**：`provideMcpServerDefinitions` 不再使用 activate 時捕捉的閉包變數，每次呼叫即時讀取，確保多視窗與工作區切換正確
- **bundled orchestration 升級至 v2.0.0**

### Fixed

- **macOS / Linux 跨平台相容性**：MCP server 啟動時以 `which npx` 解析 `npx` 絕對路徑，修正 VS Code GUI 在非 Windows 平台因 PATH 未繼承而無法啟動 MCP server 的問題

## [0.1.1] - 2026-04-24

- `.gitignore` 自動更新：建立 RAG 索引（init / rebuildIndex）完成後，自動將 `.mcp/dotnet-testing-skills` 與 `.dotnet-testing-agent/` 加入工作區 `.gitignore`，無需手動操作（原本為詢問式提示，改為靜默寫入）

## [0.1.0] - 2026-04-24

### Added

#### 核心指令

- `dta.init`：6 步驟一鍵初始化（前置環境檢查 → Skills 部署 → Orchestration 部署 → mcp-local-rag 安裝 → RAG 索引建立 → 寫入 Manifest）
- `dta.doctor`：7 項環境診斷（Node.js、npm、mcp-local-rag、Skills 目錄、Agents 目錄、LanceDB 索引、Copilot Chat）；失敗項目支援一鍵自動修復
- `dta.status`：顯示目前工作區安裝狀態
- `dta.rebuildIndex`：清除並重建 LanceDB RAG 索引
- `dta.checkIndex`：4 步驟 RAG 索引驗證（DB 存在性、documentCount、索引範圍、Smoke test）
- `dta.openExternalUrl`：透過瀏覽器開啟外部連結

#### UI 與導覽

- Sidebar Webview 面板（5 分頁：總覽 / Skills / Orchestration / RAG 索引 / 環境診斷）
- Sidebar TreeView「狀態資訊」：顯示各元件安裝版本與狀態
- Status Bar 快速狀態指示：已初始化（✓）/ 未初始化（⚠，點擊觸發 init）/ 執行中（旋轉動畫）
- Walkthrough 首次安裝引導（3 步驟：環境診斷 → 初始化 → 開啟 Copilot Chat）

#### 部署機制

- Online 優先 + Bundled Fallback：init 優先從 GitHub Release 下載最新 Skills / Orchestration；網路不可用時自動退回 Extension 內建版本
- Skills 部署過濾：排除 `skill-creator-advanced/` 與 `README.md`，確保 RAG 索引不含雜訊文件
- 背景版本更新檢查：init 完成後自動偵測線上是否有新版 Skills / Orchestration，提示一鍵更新

#### MCP 整合

- MCP Provider 模式：透過 `vscode.lm.registerMcpServerDefinitionProvider()` 管理 MCP Server 生命週期，不寫入 `.vscode/mcp.json`
- init / 更新完成後自動觸發 `mcpChangeEmitter.fire()`，Copilot Chat 無需重新載入視窗即可使用 MCP server

#### 其他

- Manifest 記錄（`.dotnet-testing-agent/manifest.json`）：記錄各元件版本、部署路徑、RAG 索引狀態
- `.gitignore` 自動更新：建立 RAG 索引後自動將 `.mcp/dotnet-testing-skills`、`.dotnet-testing-agent/` 加入 `.gitignore`
- Extension 版本更新通知：啟動後延遲 10 秒檢查 Gitea / GitHub Release API
- 設定項目：`dta.giteaUrl`、`dta.giteaRepo`、`dta.checkUpdatesOnStartup`、`dta.npmRegistry`、`dta.ragDbPath`、`dta.shellExecutable`、`dta.skillsVersion`、`dta.orchVersion`

### Technical Notes

- 最低 VS Code 版本：1.85.0（MCP Provider API 需 ~1.96+，舊版本 Extension 仍正常啟動）
- Windows 優先：`child_process` 使用 `cmd.exe`，`BASE_DIR` 以 `fs.realpathSync()` 解析
- RAG 索引存放位置：`<workspace>/.mcp/dotnet-testing-skills`（預設）或 `dta.ragDbPath` 指定路徑
