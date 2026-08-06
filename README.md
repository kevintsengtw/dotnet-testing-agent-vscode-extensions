# dotnet Testing Agent v0.7.0-beta.3

一鍵初始化 Claude Code 與 Codex 的 .NET 測試工作流程。

此 alpha 版本完成新的跨 Agent 共用目錄：

- 共用技術 Skills 與公開的 `unit-test-scenarios` 安裝到 `.agents/skills/`。
- Claude 專屬 Skills、Agents、hooks、scripts 留在 `.claude/`。
- Codex 專屬 Skills、Agents、scripts 與 config 留在 `.codex/`。
- GitHub Copilot 模式與 RAG/MCP 功能已移除。

初始化會保留未知的使用者檔案，以原子交易部署並驗證整套內容；失敗時回復原狀。完成後同一入口會切換為「更新／修復」。Doctor 可辨識缺檔、內容遭修改、manifest/snapshot 不一致、舊共用 Skill 殘留與混合版本安裝。

本版本用於發佈前的長時間工作流程驗證。Claude 與 Codex 資產固定來自公開 orchestration repository `beta/agent-modes` 分支的精確 commit，不會在背景改抓舊的正式 Release。`unit-test-scenarios` 則獨立取自 [公開 repository](https://github.com/kevintsengtw/unit-test-scenarios) 的鎖定 commit。

## 使用方式

1. 安裝 `dotnet-testing-agent-0.7.0-beta.3.vsix`。
2. 開啟目標 workspace。
3. 從工具包側邊欄初始化 Claude 或 Codex。
4. 執行環境診斷。
5. 重啟 Coding Agent，驗證 Unit、TUnit、Integration、Aspire 工作流程。

> Alpha 版本尚未進入發布階段，應先用於開發與 consumer workspace 驗證。

Codex Unit Orchestrator 已重新鎖定上游修正版，Analyzer 前的 canonical 順序統一為 `Phase 0 → Phase 0.5 run-state init → Analyzer`。
