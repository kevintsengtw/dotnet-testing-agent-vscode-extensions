# 工作區準備與工具設定

這份指南帶你取得範例、開啟正確的資料夾，以及準備工具包和 AI 擴充套件。完成後，依[在 VS Code 操作測試工作流程：選擇操作情境](../README.md#選擇操作情境)選擇範例，實際操作「安裝工作流程 → 複製提示詞 → 貼上修改 → 送出 → 查看測試結果」。

## 1. 準備這次會用到的工具

| 工具 | 用途與準備方式 |
| --- | --- |
| VS Code 1.85 以上 | 開啟範例專案，操作下列兩種擴充套件 |
| dotnet-testing-agent 工具包 | 從 [v0.8.4 Release](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions/releases/tag/v0.8.4) 取得 VSIX，在 Extensions 面板以 **Install from VSIX...** 安裝。它提供模式安裝、環境診斷、預設提示詞與 Code Coverage 面板 |
| Claude Code 或 Codex 的 VS Code 擴充套件 | 選擇你要用的一個，完成該工具的安裝、登入及必要環境設定；稍後在它的對話框送出提示詞。測試工具包不會代為安裝或登入 AI 工具 |
| Node.js 18 以上、對應 AI 工具所需的 CLI 與 PowerShell | 工作流程及診斷會使用；安裝模式後依「環境診斷」的實際提示補齊 |
| 正式版 .NET 10 SDK | Unit、Integration、Aspire、TUnit 的三版本方案使用此 SDK 政策；執行另需對應 .NET 8／9／10 runtime，容器 API 另需 ASP.NET Core runtime |

先確認你能在 VS Code 開啟所選 AI 擴充套件的對話並使用帳號。本教材以 Windows／PowerShell 說明操作；Unit 情境不需要 Docker、ReportGenerator 或 C# Dev Kit。NuGet 還原與 AI 服務通常需要網路。

Integration／Aspire 的額外需求放在[容器練習前置條件與隔離](containers.md)；兩套 MTP 的 SDK、runtime 與報告工具需求放在[Code Coverage 操作指南](code-coverage.md)，不必為了 Unit 情境全部安裝。

## 2. 取得範例，建立自己的練習副本

在[公開儲存庫](https://github.com/kevintsengtw/dotnet-testing-agent-vscode-extensions)頁面按 **Code → Download ZIP** 並解壓縮，或使用 Git clone。取得後應看得到儲存庫根目錄下的 `samples/`，裡面有 `README.md`、`guides/`、`unit/` 等目錄。範例不在 VSIX 裡。

將整個 `samples/` **複製成一個新資料夾**，例如 `C:\Temp\dotnet-testing-practice`。稍後安裝的工作流程與 AI 產生的測試都寫入這份副本，保留下載的原始範例供下次練習。若這個名稱已存在，請換一個新名稱。

也可以在已下載儲存庫的根目錄，用 PowerShell 完成複製：

```powershell
$sampleSource = (Resolve-Path -LiteralPath '.\samples').Path
$practiceRoot = 'C:\Temp\dotnet-testing-practice'
if (Test-Path -LiteralPath $practiceRoot) { throw '請換一個不存在的練習資料夾名稱。' }
Copy-Item -LiteralPath $sampleSource -Destination $practiceRoot -Recurse
```

## 3. 在 VS Code 開啟這份副本

使用 **File → Open Folder...** 開啟 `C:\Temp\dotnet-testing-practice`。檔案總管的最上層應直接看得到：

```text
dotnet-testing-practice/
  README.md
  guides/
  unit/
  integration/
  aspire/
  tunit/
  coverage-mtp-native/
  coverage-mtp-legacy/
```

**不要開啟外層下載目錄，也不要只開啟 `unit/practice/src`。** 提示詞的 `unit/practice/...` 路徑都是從這個副本根目錄計算；工具包也會把 Skills、Agents 與相關檔案安裝在這裡。

在這個 VS Code 視窗開啟整合終端機與 Claude Code／Codex 對話。終端機的目前目錄、AI 對話的工作區，應與檔案總管開啟的副本一致。若 AI 對話來自其他專案，請在此視窗建立新的對話。

## 4. 選擇要操作的情境

四種流程各提供三個版本、三種情境。依你想操作的範圍閱讀對應指南，再從工具包複製該種提示詞；不是把同一份文字換版本後重做。

| 流程 | .NET 8 | .NET 9 | .NET 10 | 適用模式 |
| --- | --- | --- | --- | --- |
| [Integration：API 操作、單一 Controller 與多個目標](../integration/practice_integration/README.md) | 訂單建立操作 | 活動 Controller | 多個目標與三種資料相依 | Full |
| [Aspire：指定端點、預約流程與跨服務檢查](../aspire/practice_aspire/README.md) | 建立預約端點 | 預約狀態流程 | 完整 API 與 SQL／Redis | Full |
| [TUnit：指定方法、整個類別與 xUnit 移轉](../tunit/practice_tunit/README.md) | ISBN 方法 | BookCatalog 類別 | xUnit 移轉 | Full |

以下列出 Unit 的範圍與 Full／Lite 對應；兩套 MTP 則使用 [Code Coverage 操作指南](code-coverage.md)，不需 AI 模式。

打開 [Unit：選擇操作範圍](../unit/practice/README.md#選擇操作範圍)，選 .NET 8 的方法、.NET 9 的單一類別或 .NET 10 的多類別情境，再依步驟安裝適用模式、複製提示詞並送出。這套範例已提供待測程式與測試專案骨架，你不需要先手動撰寫測試。

| Unit 情境 | 目標 | 本情境的適用模式 | 執行所需 runtime |
| --- | --- | --- | --- |
| .NET 8／指定方法 | `TemperatureConverter.CelsiusToFahrenheit` | Claude Full、Claude Lite、Codex Full、Codex Lite，擇一 | .NET 8 |
| .NET 9／指定單一類別 | `TemperatureConverter` | Claude Full、Claude Lite、Codex Full、Codex Lite，擇一 | .NET 9 |
| .NET 10／指定多個類別 | `EmployeeService`、`EmployeeValidator` | Claude Full 或 Codex Full | .NET 10 |

三種範圍是本教材的情境分配，不綁定工作流程可用的 .NET 版本。四種流程的 .NET 9 專案都沿用沒有 `.Net9` 字尾的來源名稱（Unit 為 `Practice.Core`／`Practice.Core.Tests`）；請使用各指南的實際路徑，不要只替換提示詞中的版本字樣。

想先看安裝畫面與四模式的差異，可閱讀[在工具包安裝模式，再到 AI 對話啟動工作流程](mode-setup.md)。安裝時遇到缺少 CLI、SDK 或設定問題，依面板診斷和[問題排查](troubleshooting.md)處理，再回到原步驟。

## 5. 重新練習與套用自己的專案

操作後保留副本中的提示詞、AI 產生的測試、執行輸出與審查結果。要比較另一模式時，再從原始 `samples/` 複製一份新資料夾，重做相同操作；不必刪除上一份成果。

完成所選操作後，依[套用到自己的專案](apply-to-your-project.md)替換欄位。你會沿用相同的面板與對話操作，只把專案路徑、測試目標及情境換成自己的內容。
