# dotnet Testing Agent — VS Code Extension

> 一鍵設定 AI 輔助 .NET 測試開發環境。支援 **Claude Code** 與 **GitHub Copilot** 兩種模式。

[![Release](https://img.shields.io/github/v/release/kevintsengtw/dotnet-testing-agent-vscode-extensions?label=最新版本)](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions/releases)
![VS Code](https://img.shields.io/badge/VS%20Code-1.85.0+-blueviolet)
![授權](https://img.shields.io/badge/授權-MIT-green)

> 📦 本頁為 **發布頁**，提供 `.vsix` 安裝檔與各版本安裝文件下載；不含原始碼。

![單一 Activity Bar 中的 Claude 模式與 Copilot 模式兩個面板](images/two-modes.png)

---

## 簡介

**dotnet Testing Agent** 是一個專為 .NET 開發人員設計的 VS Code Extension，讓你**一鍵**完成 AI 輔助測試開發環境設定。

在沒有這個 Extension 之前，要讓 AI Coding Agent 具備 .NET 測試的領域知識，得手動下載 Skills 定義檔、部署 Orchestration、（Copilot 模式還要）安裝 `mcp-local-rag` 並建立 RAG 索引。dotnet Testing Agent 把這些全部自動化——**安裝後執行一個指令即可開始**撰寫 AI 輔助的 .NET 測試。

支援兩種 AI 工具，以 **Claude** 為主、**GitHub Copilot** 並行：

- **Claude 模式**：搭配 VS Code 的 Claude 擴充套件（亦支援 Claude Code CLI），部署至 `.claude/`，**免 RAG**
- **Copilot 模式**：搭配 GitHub Copilot Chat（Agent 模式），部署至 `.github/`，含 RAG 精準搜尋

測試框架約束：**AwesomeAssertions**（非 FluentAssertions）、**NSubstitute**（非 Moq）。

自 **v0.2.0** 起，兩種模式整合為**單一 extension**，安裝一份 `.vsix` 即可同時使用，於 Activity Bar 的「dotnet-testing-agent 工具包」面板中切換。

---

## 兩種模式

| | Claude 模式 | Copilot 模式 |
| --- | --- | --- |
| 搭配的 AI 工具 | Claude Code（VS Code 擴充套件 / CLI） | GitHub Copilot Chat（Agent 模式） |
| 部署路徑 | `.claude/`（skills、agents、hooks） | `.github/`（skills、agents） |
| RAG 索引 | ❌ 不需要 | ✅ 需要（mcp-local-rag + LanceDB） |
| 初始化指令 | `初始化 Claude 模式` | `初始化 .NET Testing Agent` |
| 安裝步驟 | 4 步驟 | 6 步驟 |
| Orchestration 來源 | [dotnet-testing-agent-orchestration-claude](https://github.com/kevintsengtw/dotnet-testing-agent-orchestration-claude) | [dotnet-testing-agent-orchestration](https://github.com/kevintsengtw/dotnet-testing-agent-orchestration) |

兩種模式部署路徑與 manifest 完全獨立，互不干擾，可只用其中一種或兩種並用。兩者皆使用共用的 [dotnet-testing-agent-skills](https://github.com/kevintsengtw/dotnet-testing-agent-skills)；差異在於 Orchestration 定義（Agents / Hooks 等）各自來自上表對應的 repo。

---

## 功能特色

### Claude 模式

- **單一面板整合**：在「dotnet-testing-agent 工具包」中提供獨立的「Claude 模式」面板，與 Copilot 模式並存、互不干擾
- **一鍵部署**：自動部署 Skills（`.claude/skills/`）、16 個 Agents（`.claude/agents/`）、Hooks（`.claude/hooks/`）
- **搭配 Claude 擴充套件**：優先支援 VS Code 的 Claude 擴充套件，亦支援 Claude Code CLI
- **無需 RAG**：不需要安裝 `mcp-local-rag`，也不需要建立本地索引庫
- **環境診斷**：5 項環境檢查（Node.js、Skills/Agents/Hooks 目錄、Claude Code CLI），失敗項目可自動重新部署修復

### Copilot 模式

- **一鍵初始化**：自動執行 6 個步驟，完成從 Skills 部署到 MCP Server 啟動的全部工作；可選擇線上（HuggingFace）或離線（本地 zip）安裝 embedding model
- **內建 Skills & Agents**：隨 Extension 打包 30 個 .NET 測試 Skills 與 20 個 Orchestration Agent 定義，不需網路下載
- **Online 優先部署**：init 時優先從 GitHub Release 抓取最新版本；網路不可用時自動退回內建版本
- **RAG 精準搜尋**：透過 `mcp-local-rag` 建立本地索引庫，讓 Copilot Chat 能精準引用 Skills 內容
- **RAG 索引驗證**：4 步驟自動驗證索引完整性（DB 存在性、文件數量、索引範圍、Smoke test）
- **環境診斷**：7 項環境檢查，發現問題自動提供修復選項
- **版本更新通知**：init 完成後自動偵測 Skills / Orchestration 是否有新版本

---

## 前置需求

| 項目 | 最低版本 |
| --- | --- |
| VS Code | 1.85.0 |
| Node.js | 18.0.0 |
| 作業系統 | Windows 10/11、macOS（皆完整支援） |

依使用模式擇一：

| 模式 | 額外需求 |
| --- | --- |
| Claude 模式 | Claude Code（優先支援 VS Code 的 Claude 擴充套件，亦支援 Claude Code CLI） |
| Copilot 模式 | GitHub Copilot Chat（最新版、已登入） |

---

## 安裝方式

1. 前往 [Releases](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions/releases) 下載最新版 `dotnet-testing-agent-<版本>.vsix`
2. 在 VS Code 中安裝：

   **方法一：Extensions 面板**
   - 開啟 Extensions 面板（Windows：`Ctrl+Shift+X`；macOS：`Cmd+Shift+X`）→ 右上角 `...` → **Install from VSIX...** → 選擇下載的 `.vsix`

   **方法二：命令列**
   ```bash
   code --install-extension dotnet-testing-agent-<版本>.vsix
   ```

---

## 更新方式

本 Extension 以 `.vsix` 形式發佈於 GitHub Releases，**不走 VS Code Marketplace，因此 VS Code 不會自動更新**，需手動更新。

啟動約 10 秒後會自動檢查是否有新版本，若有會跳出通知，點「前往下載」即開啟 Releases 頁面（可用設定 `dta.checkUpdatesOnStartup` 關閉）。

手動更新（例如 v0.2.0 → v0.3.0）：

1. 前往 [Releases](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions/releases) 下載新版 `dotnet-testing-agent-<版本>.vsix`
2. 以與安裝相同的方式安裝即可，**不需要先解除安裝舊版**，VS Code 會就地覆蓋：
   - **面板**：Extensions（Windows：`Ctrl+Shift+X`；macOS：`Cmd+Shift+X`）→ `...` → **Install from VSIX...** → 選擇新檔
   - **命令列**：`code --install-extension dotnet-testing-agent-<版本>.vsix`
3. 依提示 **Reload Window**（重新載入視窗）讓新版生效。
4. 若新版更新了內建 Skills / Agents 或部署邏輯，建議重新執行一次初始化指令以同步最新內容（重跑安全，會以 QuickPick 確認是否覆蓋）。

---

## 快速開始

安裝後，Activity Bar 會出現「dotnet-testing-agent 工具包」圖示，側邊欄含 **Claude 模式** 與 **Copilot 模式** 兩個面板。依需求初始化對應模式。

> 快捷鍵對照：命令面板 — Windows `Ctrl+Shift+P`、macOS `Cmd+Shift+P`；Copilot Chat — Windows `Ctrl+Alt+I`、macOS `Cmd+Ctrl+I`。

### Claude 模式

1. 開啟命令面板（Windows：`Ctrl+Shift+P`；macOS：`Cmd+Shift+P`）→ 執行 `dotnet-testing 測試工作流程: 初始化 Claude 模式`
2. 等待 4 個步驟完成（部署 Skills / Agents / Hooks 至 `.claude/`）

完成後可在側邊欄查看部署狀態與環境診斷：

![Claude 模式總覽，環境診斷 5/5 全部通過](images/claude-overview.png)

<details>
<summary>Claude 模式部署的 Agents 與 Hooks 清單</summary>

![Claude 模式 Agents 分頁，列出 16 個 agents 與 hooks](images/claude-agents.png)

</details>

### Copilot 模式

1. 開啟命令面板（Windows：`Ctrl+Shift+P`；macOS：`Cmd+Shift+P`）→ 執行 `dotnet-testing 測試工作流程: 初始化 .NET Testing Agent`
2. 選擇安裝模式（線上 / 離線）

   ![選擇安裝模式：線上由 mcp-local-rag 自動下載，離線使用預先打包的 model zip](images/install-mode.png)

3. 等待 6 個步驟完成
4. 開啟 Copilot Chat（Windows：`Ctrl+Alt+I`；macOS：`Cmd+Ctrl+I`）→ 切換 **Agent** 模式 → 點選工具圖示（🔨）→ 確認 `dotnet-testing-skills` 已啟用

完成後可在側邊欄執行環境診斷確認：

![Copilot 模式環境診斷 7/7 全部通過](images/copilot-doctor.png)

<details>
<summary>驗證 RAG 索引狀態</summary>

![Copilot 模式 RAG 索引分頁，驗證索引 4/4 通過](images/copilot-rag.png)

</details>

> **MCP Server 說明**：`dotnet-testing-skills` 不會出現在 Extensions 面板的「MCP 伺服器」區段——透過 Extension 程式碼註冊的 MCP Server 只在 Copilot Chat 工具清單中顯示。重新開啟 VS Code 後，只要工作區已初始化，Extension 會自動重新啟動 MCP Server，不需再次初始化。

各版本的詳細安裝指南見 [`docs/`](docs/) 目錄。

---

## 指令說明

### Claude 模式

| 指令 | 說明 |
| --- | --- |
| 初始化 Claude 模式 | 部署 Skills、16 個 Agents、Hooks，寫入 `manifest-claude.json` |
| Claude 模式環境診斷 | 執行 5 項環境檢查（Node.js、Skills/Agents/Hooks 目錄、Claude Code CLI） |

### Copilot 模式

| 指令 | 說明 |
| --- | --- |
| 初始化 .NET Testing Agent | 部署 Skills 與 Agents、安裝 mcp-local-rag、建立 RAG 索引、啟動 MCP Server |
| 環境診斷 | 執行 7 項環境檢查，失敗項目提供自動修復選項 |
| 檢視 Testing Agent 狀態 | 在 Sidebar 顯示各元件的安裝狀態 |
| 重建 RAG 索引 | 重新 ingest Skills 內容至 LanceDB 索引庫 |
| 索引狀態檢查 | 執行 4 步驟驗證確認 RAG 索引完整性 |

---

## Sidebar 面板

點選 Activity Bar 的「dotnet-testing-agent 工具包」圖示，側邊欄含兩個可折疊面板：

### Claude 模式面板（3 分頁）

- **總覽**：顯示初始化狀態與安裝時間；提供初始化與環境診斷快速操作
- **Skills**：`.claude/skills/` 部署詳情
- **Agents**：`.claude/agents/` 部署詳情（目標 16 個）

### Copilot 模式面板（5 分頁）

- **總覽**：顯示 Skills、Orchestration、RAG 索引的安裝版本與狀態；提供初始化、重建索引、環境診斷快速操作
- **Skills**：Skills 部署詳情與 GitHub Release 說明
- **Orchestration**：Orchestration Agent 部署詳情與 GitHub Release 說明
- **RAG 索引**：索引驗證結果（DB 存在性、documentCount、索引範圍、Smoke test）
- **環境診斷**：7 項診斷結果詳細列表

---

## 初始化後的目錄結構

### Claude 模式

```plaintext
<workspace>/
├── .claude/
│   ├── skills/      ← Skills 定義（共用 + orchestrator 專用）
│   ├── agents/      ← 16 個 Claude Agent 定義
│   └── hooks/       ← shell scripts + install-hooks.js（寫入 .claude/settings.json）
└── .dotnet-testing-agent/
    └── manifest-claude.json     ← Claude 模式安裝記錄（不提交 git）
```

### Copilot 模式

```plaintext
<workspace>/
├── .github/
│   ├── skills/      ← 30 個 Skills 定義（含 dotnet-test）
│   └── agents/      ← 20 個 Orchestration Agent 定義
├── .mcp/
│   ├── dotnet-testing-skills/   ← LanceDB RAG 索引庫（本地，不提交 git）
│   └── cache/                   ← 離線模式 embedding model 快取（不提交 git）
└── .dotnet-testing-agent/
    └── manifest.json            ← 安裝記錄（不提交 git）
```

> `.mcp/`、`.claude/` 相關產物與 `.dotnet-testing-agent/` 會在初始化後自動加入 `.gitignore`。

---

## 內建 Skills

<details>
<summary>總覽 Skills（2 個）</summary>

| Skill 名稱 | 說明 |
| --- | --- |
| `dotnet-testing` | 基礎測試技能總覽，包含 19 個子技能的入口 |
| `dotnet-testing-advanced` | 進階測試技能總覽，包含 8 個子技能的入口 |

</details>

<details>
<summary>基礎 & 框架類（10 個）</summary>

| Skill 名稱 | 說明 |
| --- | --- |
| `dotnet-test` | .NET 測試執行指南（含 blame mode、平行執行、theory 過濾） |
| `dotnet-testing-unit-test-fundamentals` | 單元測試基礎概念與實踐 |
| `dotnet-testing-xunit-project-setup` | xUnit 測試專案建立與設定 |
| `dotnet-testing-test-naming-conventions` | 測試命名慣例指南 |
| `dotnet-testing-test-output-logging` | 測試輸出與日誌記錄 |
| `dotnet-testing-code-coverage-analysis` | 程式碼覆蓋率分析 |
| `dotnet-testing-private-internal-testing` | 測試私有與內部成員 |
| `dotnet-testing-advanced-tunit-fundamentals` | TUnit 新世代測試框架入門 |
| `dotnet-testing-advanced-tunit-advanced` | TUnit 進階使用技巧 |
| `dotnet-testing-advanced-xunit-upgrade-guide` | xUnit v2 升級至 v3 指南 |

</details>

<details>
<summary>工具 & 模式類（13 個）</summary>

| Skill 名稱 | 說明 |
| --- | --- |
| `dotnet-testing-nsubstitute-mocking` | NSubstitute Mock 框架使用指南 |
| `dotnet-testing-awesome-assertions-guide` | AwesomeAssertions 斷言庫使用指南 |
| `dotnet-testing-autofixture-basics` | AutoFixture 基礎使用 |
| `dotnet-testing-autofixture-customization` | AutoFixture 客製化設定 |
| `dotnet-testing-autofixture-bogus-integration` | AutoFixture 整合 Bogus |
| `dotnet-testing-autofixture-nsubstitute-integration` | AutoFixture 整合 NSubstitute |
| `dotnet-testing-autodata-xunit-integration` | AutoData 與 xUnit 整合 |
| `dotnet-testing-bogus-fake-data` | Bogus 假資料產生 |
| `dotnet-testing-test-data-builder-pattern` | Test Data Builder 設計模式 |
| `dotnet-testing-complex-object-comparison` | 複雜物件比較技巧 |
| `dotnet-testing-datetime-testing-timeprovider` | 日期時間測試與 TimeProvider |
| `dotnet-testing-filesystem-testing-abstractions` | 檔案系統測試抽象化 |
| `dotnet-testing-fluentvalidation-testing` | FluentValidation 驗證測試 |

</details>

<details>
<summary>整合測試類（5 個）</summary>

| Skill 名稱 | 說明 |
| --- | --- |
| `dotnet-testing-advanced-aspnet-integration-testing` | ASP.NET Core 整合測試 |
| `dotnet-testing-advanced-webapi-integration-testing` | Web API 整合測試 |
| `dotnet-testing-advanced-aspire-testing` | .NET Aspire 測試 |
| `dotnet-testing-advanced-testcontainers-database` | Testcontainers 資料庫測試 |
| `dotnet-testing-advanced-testcontainers-nosql` | Testcontainers NoSQL 測試 |

</details>

---

## 內建 Orchestration Agents

供 AI Coding Agent 呼叫的測試工作流程定義，共 4 套、每套 5 個角色（orchestrator / analyzer / writer / reviewer / executor）。Copilot 模式部署至 `.github/agents/`，Claude 模式部署至 `.claude/agents/`。

<details>
<summary>基礎測試套件</summary>

- `dotnet-testing-orchestrator`
- `dotnet-testing-analyzer`
- `dotnet-testing-writer`
- `dotnet-testing-reviewer`
- `dotnet-testing-executor`

</details>

<details>
<summary>進階 Aspire 測試套件</summary>

- `dotnet-testing-advanced-aspire-orchestrator`
- `dotnet-testing-advanced-aspire-analyzer`
- `dotnet-testing-advanced-aspire-writer`
- `dotnet-testing-advanced-aspire-reviewer`
- `dotnet-testing-advanced-aspire-executor`

</details>

<details>
<summary>進階整合測試套件</summary>

- `dotnet-testing-advanced-integration-orchestrator`
- `dotnet-testing-advanced-integration-analyzer`
- `dotnet-testing-advanced-integration-writer`
- `dotnet-testing-advanced-integration-reviewer`
- `dotnet-testing-advanced-integration-executor`

</details>

<details>
<summary>進階 TUnit 測試套件</summary>

- `dotnet-testing-advanced-tunit-orchestrator`
- `dotnet-testing-advanced-tunit-analyzer`
- `dotnet-testing-advanced-tunit-writer`
- `dotnet-testing-advanced-tunit-reviewer`
- `dotnet-testing-advanced-tunit-executor`

</details>

---

## Extension 設定

在 VS Code 設定（`Ctrl+,`）中搜尋 `dta` 即可找到所有設定項目：

| 設定鍵 | 類型 | 預設值 | 說明 |
| --- | --- | --- | --- |
| `dta.giteaUrl` | string | `""` | Release 伺服器 URL（留空使用 GitHub；可設為 Gitea 伺服器位址） |
| `dta.giteaRepo` | string | `""` | Release Repository 路徑，格式為 `owner/repo` |
| `dta.checkUpdatesOnStartup` | boolean | `true` | 啟動時自動檢查是否有新版 Extension |
| `dta.npmRegistry` | string | `""` | 自訂 npm registry（企業內部鏡像用） |
| `dta.ragDbPath` | string | `""` | 自訂 LanceDB 索引庫路徑（留空使用工作區預設路徑） |
| `dta.shellExecutable` | string | `""` | 執行 npm 指令的 shell 路徑 |
| `dta.skillsVersion` | string | `"latest"` | Skills 版本 tag（例如 `v2.4.1`）；`"latest"` 自動抓取最新 release |
| `dta.orchVersion` | string | `"latest"` | Orchestration 版本 tag（例如 `v1.0.0`）；`"latest"` 自動抓取最新 |
| `dta.mcpInstallMode` | string | `"online"` | Embedding model 安裝模式：`online`（HuggingFace 自動下載）或 `offline`（使用本地預先打包的 zip） |
| `dta.claude.skillsVersion` | string | `"latest"` | Claude 模式 Skills 版本 tag；`"latest"` 自動抓取最新 release |

---

## 相關專案

| Repo | 說明 |
| --- | --- |
| [dotnet-testing-agent-skills](https://github.com/kevintsengtw/dotnet-testing-agent-skills) | .NET 測試 Agent Skill 定義檔（兩種模式共用） |
| [dotnet-testing-agent-orchestration-claude](https://github.com/kevintsengtw/dotnet-testing-agent-orchestration-claude) | **Claude 模式**的 Orchestration 定義（skills / agents / hooks） |
| [dotnet-testing-agent-orchestration](https://github.com/kevintsengtw/dotnet-testing-agent-orchestration) | **Copilot 模式**的 Agent Orchestration 流程定義 |

---

## 授權

[MIT](LICENSE)
