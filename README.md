# Dotnet-Testing-Agent 工具包

在 VS Code 中安裝與管理 Claude Code、Codex 的 .NET 測試工作流程。工具包會將工作流程所需的 Skills、Agents 與 scripts 部署到專案工作區，並提供安裝、升級、修復、解除安裝及環境診斷。

## 功能介紹

- **四種工作流程模式**：Claude Full、Claude Lite、Codex Full、Codex Lite 可同時安裝在同一工作區，各模式個別管理。
- **完整與精簡流程**：Full 提供 Unit、TUnit、Integration、Aspire 測試工作流程；Lite 提供較精簡的單元測試流程。
- **安裝與版本管理**：支援線上正式版本與內建離線安裝內容，顯示已安裝版本、內建版本及可取得的線上版本。切換查看模式只更新面板內容。
- **環境診斷**：檢查受管理檔案、來源版本與設定，協助確認缺檔、檔案修改及其他需要處理的問題。
- Node.js 與 CLI 的檢查結果會顯示於各模式診斷及複製的完整報告；缺少必要工具時會顯示需要修復。
- **Codex Agent 設定**：可針對單一 Agent 調整模型與推理強度；Full 與 Lite 的設定各自保留。
- **工作區檔案保護**：保留使用者自訂檔案與既有設定，部署失敗時復原原有內容。

## 最新版本：v0.8.4

下載 [v0.8.4 Release](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions/releases/tag/v0.8.4) 中的 `dotnet-testing-agent-0.8.4.vsix`。完整步驟請見 [v0.8.4 安裝與升級指南](docs/v0.8.4.md)，變更紀錄請見 [CHANGELOG](CHANGELOG.md)。

## v0.8.4：既有 Skill 共存與衝突提示

既有 Claude Full 工作區可在核對來源後直接加裝 Lite，保留原 Skill 的內容與未接管狀態。Windows／macOS 安裝及既有工作區處理方式見 [v0.8.4 安裝與升級指南](docs/v0.8.4.md)。

相同內容的既有 Skills 不再因模式來源版本不同而被誤報為 `MIXED_VERSION_CONFLICT`。Claude Full v1.7.3 與 Lite v1.2.0 的既有 `dotnet-test` 若通過來源紀錄及實際雜湊核對，可沿用既有正式原檔，保留其內容與未接管狀態；未知來源與使用者修改仍受保護。

安裝本次更新的 v0.8.4 VSIX 並完整重啟 VS Code 後，保留既有 Full，直接安裝 Claude Lite；不需要先解除安裝 Full 或移走 Skill。安裝通知與環境診斷會說明沿用的來源差異，各模式的原始來源證據仍保留。移除或更新 Extension 不會清除工作區檔案。

四模式內建離線版本沿用 v0.8.3，保留 Code Coverage 與預設提示詞功能。2026-10-16 使用者確認 macOS R2 可保留既有 Full、安裝 Lite，安裝後診斷畫面正常。Windows 11 已透過實際 VS Code 面板完成四模式乾淨安裝、環境診斷及既有 Full 工作區加裝 Lite，既有檔案保持不變。共存後 Full 僅有既有用量統計範圍提醒，其餘三模式正常；驗證範圍不包含 .NET 測試工作流程執行。

## 範例專案與操作指南

範例用來讓你實際操作工具包：在 VS Code 安裝所選模式的 Skills／Agents 與相關檔案，從「預設提示詞」複製範本，貼到 Claude Code／Codex 擴充套件的對話框，修改測試目標與情境後送出。AI coding agent 會依工作流程產生、執行與審查測試，你可在對話查看進度與結果。工作區與面板操作見[工作區準備與工具設定](samples/guides/getting-started.md)；Unit 依 [Unit：選擇操作範圍](samples/unit/practice/README.md#選擇操作範圍)選擇情境。

請透過[公開儲存庫](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions)的 **Code → Download ZIP** 或 Git clone 取得整份 samples/；範例與指南不包含在 VSIX 中。其他練習見[在 VS Code 操作測試工作流程](samples/README.md)。

| 練習 | 操作入口 | 適用條件 |
| --- | --- | --- |
| Unit | [Unit：指定方法、單一類別與多類別範圍](samples/unit/practice/README.md) | Claude／Codex Full 或 Lite；多類別使用 Full |
| Integration | [Integration：API 操作、單一 Controller 與多個目標](samples/integration/practice_integration/README.md) | Full、Linux containers |
| Aspire | [Aspire：指定端點、預約流程與跨服務檢查](samples/aspire/practice_aspire/README.md) | Full、Linux containers |
| TUnit | [TUnit：指定方法、整個類別與 xUnit 移轉](samples/tunit/practice_tunit/README.md) | Full |
| MTP 原生 Coverage | [MTP 原生 Code Coverage：Step by Step](samples/coverage-mtp-native/README.md) | 不需 AI 模式；另需覆蓋率工具 |
| MTP 舊版相容 Coverage | [MTP 舊版相容 Code Coverage：Step by Step](samples/coverage-mtp-legacy/README.md) | 不需 AI 模式；另需覆蓋率工具 |

Unit、Integration、Aspire、TUnit 都各有 .NET 8／9／10 三種情境；Unit 示範方法、單一類別與多類別，其他流程依上表指南分別操作。依情境選擇 AI 工具與模式，再依[套用到自己的專案](samples/guides/apply-to-your-project.md)替換提示詞欄位。容器及 Coverage 的 SDK、runtime 與工具需求另列於對應指南。

## v0.8.3：Code Coverage 與預設提示詞

新增獨立、預設收合的「Code Coverage」區塊，不需先初始化工作流程模式。掃描 `.sln`／`.slnx` 的實際測試專案成員，依 VSTest 或 MTP 原生／舊版相容模式產生 Coverage 與 HTML Report 指令，提供分別複製與成功回饋。

請在 PowerShell 的所選工作區根目錄先執行 Coverage，再執行同組 HTML Report。VSTest 需要 `coverlet.collector`，MTP 預設需要 `Microsoft.Testing.Extensions.CodeCoverage`；HTML 報告需要已安裝的 ReportGenerator 全域工具。指令使用同組結果目錄，重新掃描或變更選擇後須重新執行 Coverage。Extension 只讀取設定與複製文字，不執行測試或修改專案。

同一工作區可包含使用不同 runner 的方案。面板讀取方案目錄往上至工作區根目錄最近的 `global.json`；必要時 Coverage 指令暫時切換到該設定目錄，執行後恢復原目錄。HTML 指令及結果目錄仍以所選工作區根目錄為準。

Coverage 掃描會採用適用的 `VersionOverride`，中央 MTP 版本需配合直接參照。直接 `xunit.v3.mtp-v2` 可作為測試／MTP 證據，但不把 xUnit 套件版本當成 MTP 精確版本；明確 `IsTestProject=false` 仍排除，MTP 版本證據衝突無法經確認略過。

本版僅依工作區內可直接解析的專案、props、targets 與 Import 辨識，不解析 SDK 內部、傳遞套件或工作區外上層設定。套件證據 `no` 表示未在可讀設定找到直接參照，會停用指令，並不表示已確認實際環境缺少套件；`unknown` 須由使用者確認實際環境，已知衝突仍不可略過。無不確定設定且沒有已辨識測試證據的 `Microsoft.NET.Sdk` 專案不列入清單；若測試設定僅由未解析來源提供，可能無法列出或產生指令。`MSTest.Sdk` 可辨識為測試專案，套件仍可能未確認。

Claude／Codex 模式面板新增「預設提示詞」分頁。Full 各提供 Unit、Integration、Aspire、TUnit 四份公版，Lite 各提供 Unit 一份；可展開、收合、手動選取與複製完整純文字。保留 Claude `/`、Codex `$` 及待填欄位，提供 `.csproj` 工作區相對路徑範例；查看與複製可離線使用，不啟動工作流程。

## 延續 v0.8.2 的功能

Codex Full 的 Agents 區塊依序為 Unit、Integration、Aspire、TUnit；Full／Lite 套用或重設後保留目前捲動位置與已展開的 Agent 技術資訊，方便連續調整模型與推理強度。

v0.8.2 修正舊版移轉工作區解除安裝 Full 模式後，中央部署 manifest 被誤判為無效、無法重新安裝的問題。歷史移轉紀錄不再被當成目前已安裝模式；其他模式的檔案與使用者自訂內容仍受保護。已受影響的 manifest 可直接讀取，不需要手動刪除或修改。

## 內建離線版本

| 內容 | 內建版本 |
| --- | --- |
| Claude Full | `v1.7.3` |
| Claude Lite | `v1.2.0` |
| Codex Full | `v1.3.1` |
| Codex Lite | `v1.2.2` |
| Shared Skills | `v2.4.5` |
| unit-test-scenarios | `d0050198` 精確 commit |

「版本資訊」列出四模式內建版本，工作區已安裝版本另行顯示。

## 延續 v0.8.1 的功能

- **直接升級**：四模式依已安裝、內建及可取得的線上版本提供「升級安裝」，不必先解除安裝。GitHub 查詢受限或連線失敗時，仍可升級至較新的完整內建版本；保留使用者檔案與 Agent 設定，拒絕降版。
- **統一管理入口**：每個模式保留一個管理入口，依版本提供升級安裝、修復或解除安裝。已是內建最新版但無法查詢線上版本時，會明確顯示線上最新版本尚未確認。
- **隨時查看 Release**：所選模式的「查看 Release」可開啟官方最新發行頁，不受安裝狀態或 GitHub API 查詢限制影響。
- **改善 Agents 操作**：套用或重設模型與推理強度後，保留 Agents 頁籤與查看模式。模型選單依序為 GPT-6.1 Sol、GPT-6 Astra、GPT-6 Sol、GPT-6 Luna、GPT-5.6 Sol、GPT-5.6 Terra、GPT-5.6 Luna。
- **改善 v0.8.0 工作區相容性**：正式舊版共用 Skills 不會被誤判為使用者修改；既有模式原始內容未變時，仍可設定 Agent 模型，不必先更新工作流程。
- **同名受管理 Skills 共存**：同一路徑只保留一份，安裝、更新或修復時採用最後部署模式的原始來源內容。不同 Skills 根目錄各自保留；解除安裝時保留其他模式仍使用的檔案。
- **改善版本查詢與通知**：分別顯示 GitHub 查詢限制、逾時、連線失敗及來源驗證失敗；受到限制時顯示可重試時間並共用等待時間。安裝重用有效的固定版本查詢結果，手動查詢不再被未完成的背景查詢誤判。更新通知不再占用部署操作。

### 工作流程來源更新

Claude Full `v1.7.3` 使用 16 個 Agent、4 份獨立規則文件、5 個模式專屬 Skills 與 30 個共用 Skills；6 個 scripts 位於 `.claude/scripts/dotnet-testing-claude-full/`。此來源不再提供 timer hooks 或 `settings.json`。面板會將 Agent 與規則文件分開顯示；升級會保留其他模式仍需的 hooks 與使用者設定，並退役該版本已不使用的受管理項目。

Claude Full `v1.7.3` 已修正同一則訊息重複計入 token 的問題；用量帳本格式升為第 3 版，舊報告的數值不宜直接比較。上游用量統計仍以 `dotnet-testing-` 前綴篩選 Agent，可能包含同一工作階段的 Lite 記錄。Full／Lite 共存時，環境診斷會提示此限制；Full 報表不可直接視為 Full 專屬用量。Skills 的名稱、路徑與內容依固定上游來源部署，不另行改名。

Codex Full `v1.3.1` 的正式來源使用 16 個 Agent、5 個模式專屬 Skills 與 30 個共用 Skills。59 個 runtime scripts 與 5 份 manifest 位於 `.codex/scripts/dotnet-testing-codex-full/`；四套流程各自提供實際用量 HTML 與選用 Standard credit 換算，並在派遣前進行 NuGet 預檢。來源 Agent 維持 `gpt-5.6-sol`／`medium`，既有 Extension 模型選擇與使用者設定會保留。升級只移除原先記錄為 Extension 管理的舊 scripts；不部署封存檔中殘留的舊估算器。

v0.8.1 也支援 Claude Lite `v1.2.0` 與 Codex Lite `v1.2.2` 的固定 commit 來源。Claude Lite 使用 18 個 canonical 共用 Skills，索引維持上游的 `.agents/SKILLS-INDEX.md`，不再加入 timer hooks 或 settings。Codex Lite 使用 14 個模式專屬 Skills 與 `.codex/scripts/dotnet-testing-codex-lite/`，不再要求 canonical 共用 Skills。Codex Lite v1.2.2 的用量儀表板加入 GPT-6.1 Sol Standard credit 費率，工作流程會在派遣 Agent 前建立結果交付與停止回報目錄。v1.2.1 已有的 NuGet 預檢仍依還原診斷提供建議，一般啟動不需要設定 `NUGET_PACKAGES`；還原受阻便不建立 run。兩個 Lite Agent 的來源模型仍為 `gpt-6-sol`／`medium`。

離線安裝沿用內建來源的原始檔案配置，不額外搬移或改名 Skills；使用者自訂檔案保留。

升級依管理紀錄逐檔移除未修改的舊資產，保留使用者檔案；待退役資產如有修改會停止更新。Codex 設定只補缺項並保留既有值。Claude Lite 索引的上游 `--check` 在 Full 共存時可能因額外 Skills 失敗，環境診斷會提示此限制。內建 Shared Skills 已更新至 v2.4.5，四模式內建離線版本同步更新為 Claude Full v1.7.3、Claude Lite v1.2.0、Codex Full v1.3.1 與 Codex Lite v1.2.2。

Shared Skills v2.4.5 承接前版修正，更新 Web API／Aspire 測試範本與 TUnit、FakeTimeProvider 等說明。Web API 範本移除 `ResetTime()`；重新複製範本時，原有呼叫須改為向前推進時間或依目前時間計算。已修改的舊受管理檔案會阻擋升級，使用者自有檔案保留。

## Full／Lite 模式

以下截圖沿用 v0.8.0，供辨識查看模式與安裝狀態；v0.8.3 的版本資訊及操作按鈕以本文說明為準。

![Codex Full 與 Lite 可同時安裝，目前查看模式與安裝狀態分開顯示](images/v0.8.0-codex-modes-overview.png)

藍色實心按鍵與「✓ 目前」代表目前查看的模式；`● 已安裝`／`○ 尚未安裝` 則是獨立的安裝狀態。切換查看不會修改工作區，也不會改變另一模式的安裝內容。

## Codex Agent 模型設定

Codex Full／Lite 的每個受管理 Agent 都可獨立選擇模型，選單依序為 GPT-6.1 Sol、GPT-6 Astra、GPT-6 Sol、GPT-6 Luna、GPT-5.6 Sol、GPT-5.6 Terra、GPT-5.6 Luna，並搭配 Low、Medium、High、XHigh、Max 推理強度。未自訂 Agent 的選單預選 GPT-6 Sol；按「套用」後才會寫入，既有選擇仍保留。套用前會先唯讀檢查，確認無衝突後才寫入；Full 與 Lite 的選擇互相隔離。

既有 v0.8.0 模式的原始內容未變時，升級 Extension 後仍可設定模型，不必先更新工作流程。原始內容的取得問題會顯示具體原因；執行環境診斷不會改寫 Agent 設定。

![Codex Lite 的 Agent 資產狀態與模型設定集中在同一卡片](images/v0.8.0-codex-lite-agent-settings.png)

此截圖為 v0.8.0；v0.8.3 的模型順序與「套用」「重設」操作依下列說明，已不使用舊版的預覽確認流程。

Codex Full 的 16 個 Agents 依 Unit、Integration、Aspire、TUnit 排列；Codex Lite 顯示兩個 Unit Agents。套用、重設及背景更新後，保留目前捲動位置與已展開的 Agent 技術資訊。

每張 Agent 卡片會顯示資產狀態、來源預設、目前 model／effort、兩個下拉選單及單一 Agent 的套用／重設。受管理路徑和 exact source 收合在「Agent 技術資訊」中。

套用流程：

1. 選擇模型與推理強度。
2. 點選「套用」；Extension 會先唯讀檢查，確認無衝突後才寫入。
3. 套用成功後 Reload Window，並建立新的 Codex task。

安裝與更新會固定 stable Release tag 和 exact commit，再以同一來源計畫完成驗證與原子交易。網路不可用時，乾淨工作區仍可使用完整 bundled snapshot。未知的使用者檔案、Claude 既有設定與 Codex 既有設定值都會保留；衝突或無效設定會停止操作，不會被覆寫。

## 使用方式

1. 安裝提供的 `dotnet-testing-agent-0.8.3.vsix`，完整重啟 VS Code。
2. 開啟目標 workspace，從工具包側邊欄展開 Claude 或 Codex。
3. 選擇 Full 或 Lite 並安裝需要的模式；四個模式可同時存在。
4. 執行環境診斷。
5. 若修改 Codex Agent 模型設定，套用後 Reload Window，並建立新的 Codex task。

從 v0.7.0 升級時，請先完整重啟 VS Code，再依面板提示對實際使用的 Claude Full、Codex Full 執行更新或重新初始化，完成後執行環境診斷。Extension 會以保守的 v0.7 exact-source reconciliation 核對既有 managed assets；發現使用者修改、缺檔或來源不符時會停止操作，不會採用未驗證的直接覆寫。Lite 不會自動安裝，需要時再個別安裝。

從 v0.8.0 升級後，可先診斷既有四模式，再逐一更新、修復或解除安裝。正式舊版共用 Skills 不會被誤判為使用者修改；bundled 模式原始內容未變時，修復不因整體 bundled 識別變更而被拒絕，共用 Skills 會隨修復交易採用新版內建版本。移除模式會保留其他模式仍使用的共用檔案。

啟動及完成操作後會檢查 Shared Skills 與四個工作流程的最新 stable Release，提供「查看 Release」、「稍後」及「忽略此版本」。Shared Skills 新版只會通知，不會靜默更新工作區。
