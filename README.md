# dotnet Testing Agent v0.7.0

一鍵初始化 Claude Code 與 Codex 的 .NET 測試工作流程。

v0.7.0 將跨 Agent 共用技術 Skills 與 `unit-test-scenarios` 統一部署至 `.agents/skills/`；Claude 與 Codex 的模式專屬 Skills、Agents、scripts 及設定則分別留在 `.claude/` 與 `.codex/`。GitHub Copilot 模式、local RAG 與 MCP provider 已移除。

初始化採 bundled-first，可離線完成。Extension 會保留未知的使用者檔案，以原子交易部署並驗證整套內容，失敗時回復原狀。完成後同一入口會切換為「更新／修復」。Doctor 可辨識缺檔、內容遭修改、manifest／snapshot 不一致、舊共用 Skill 殘留與混合版本安裝。

內建工作流程固定來自公開正式版本：

- Claude orchestration `v1.5.0`
- Codex orchestration `v1.2.0`
- Shared Skills `v2.4.1`
- `unit-test-scenarios` 公開 repository 的鎖定 commit

## 使用方式

1. 安裝 `dotnet-testing-agent-0.7.0.vsix`。
2. 開啟目標 workspace。
3. 從工具包側邊欄初始化 Claude 或 Codex。
4. 執行環境診斷。
5. 完整重啟對應 Coding Agent，再執行 Unit、TUnit、Integration 或 Aspire 工作流程。

若從 v0.6.0 或任一 v0.7.0 beta 升級，請對實際使用的每個模式各執行一次「更新／修復」，讓 workspace 套用新的 `.agents/skills` canonical layout 與正式工作流程快照。
