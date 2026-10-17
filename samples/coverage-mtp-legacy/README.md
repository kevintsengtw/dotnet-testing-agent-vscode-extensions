# MTP 舊版相容 Code Coverage：Step by Step

這套練習以 **整個方案（.NET 8＋10）**收集覆蓋率：從工具包複製相容模式指令，在 PowerShell 執行兩個測試專案，將兩份結果合併成 HTML，再核對報告是否包含兩個程式庫。

測試已備妥，不需要 Claude／Codex 模式、AI 生成測試或 Docker。工具包負責產生及複製命令；實際執行由你在終端機完成。

## Step 1：取得範例與準備工具

依[工作區準備與工具設定](../guides/getting-started.md)取得 public repo 的 git／ZIP，複製整份 samples，再以 VS Code 開啟副本。檔案總管最上層應直接看得到 `coverage-mtp-native/`、`coverage-mtp-legacy/` 與 `guides/`，整合終端機也留在同一根目錄。

安裝 dotnet-testing-agent Extension，並依[Code Coverage：前置環境](../guides/code-coverage.md#前置環境)準備正式版 .NET SDK 10.0.401 或同功能帶較新修補版，以及可直接呼叫的 ReportGenerator。本輪選整個方案，需同時有 .NET 8 與 .NET 10 runtime。

在 VS Code 的 PowerShell 先查看 `dotnet --list-sdks`、`dotnet --list-runtimes` 與 `Get-Command reportgenerator`。缺少工具時先依共用指南補齊，確認可用後才進入下一步。不要搬動範例的 global.json。

## Step 2：開啟 Code Coverage 並掃描

在 Activity Bar 開啟 **dotnet-testing-agent 工具包**，展開 **Code Coverage**。確認面板的 **工作區** 與 VS Code 開啟的副本相同；多根工作區若選錯，使用 **切換工作區** 選到本次副本。按 **重新掃描**，等待方案與測試專案清單出現。

若清單沒有下面的方案，先回到檔案總管確認開啟的是整份副本，而不是某個 src 或 tests 子目錄，再重新掃描。

## Step 3：選定本輪收集範圍

依下表逐欄選擇，尤其注意兩套範例名稱相似，應核對完整路徑。

| 面板欄位 | 本輪選擇 |
| --- | --- |
| 方案 | `coverage-mtp-legacy/Practice.TUnit.Coverage.Legacy.slnx` |
| 測試專案 | `不指定專案（使用所選方案）` |
| 測試平台 | **Microsoft.Testing.Platform（MTP）** |
| MTP 指令模式 | **舊版相容模式** |

雖然本例 global.json 的 runner 是 VSTest，測試本身仍使用 MTP，因此面板的「測試平台」選 MTP。舊版相容是 dotnet test 的整合方式，不是只測舊版 .NET。

## Step 4：確認前置條件，取得這組命令

展開 **工具與設定前置條件**，閱讀面板列出的 SDK、runner、目標專案與覆蓋率套件，與頁末固定設定對照。確認本機工具與選擇都正確後，勾選：

> 我已確認實際 SDK／runner、所有目標測試專案及覆蓋率套件符合所選模式，並已安裝 ReportGenerator。

此時才能使用這組命令。若出現已知 runner 或套件衝突，勾選不會略過問題；先看具體訊息與[問題排查](../guides/troubleshooting.md)，不要為了啟用按鈕改寫設定。

## Step 5：複製並執行 Coverage command

在 **Coverage command** 按 **複製**，看到 **已複製** 後，將整段貼到 VS Code 的 PowerShell。核對終端機仍在副本根目錄，再執行。這段文字是終端機命令，無須送到 AI 對話。

保留面板產生的切換目錄、引號、參數與結束處理。命令會暫時切到本例的 global.json 所在目錄，再返回工作區；不要只擷取中間的 dotnet test 一行。相容模式另處理 -- 後的參數及 PowerShell 原生參數傳遞，不能自行改成 --collect。

等待執行結束，先閱讀測試輸出，再在同一終端機執行：

```powershell
$LASTEXITCODE
```

本輪固定測試應有 **34 項**、退出碼 0，並在該組命令指定的 raw 目錄產生 **2 份 Cobertura XML**。檢查命令實際列出的路徑與檔案，不用其他歷史報告代替。若失敗或零測試，保留輸出並釐清原因，先不要進入 HTML 步驟。

## Step 6：用同一組 HTML Report command 產生報告

回到面板，**先不要重新掃描或改選項**。在 **HTML Report command** 按 **複製**，把完整命令貼到剛才的根目錄 PowerShell 執行。它會讀取 Step 5 那一組 XML；等待完成後再查看 `$LASTEXITCODE`，應為 0。

若 ReportGenerator 找不到結果，回頭核對 Coverage 是否成功、XML 是否存在，以及兩段命令是否同組。不要改成掃描所有歷史 coverage 目錄，否則會把別次結果混進來。

## Step 7：找到並開啟本次 index.html

從剛執行的 HTML 命令讀取 **-targetdir** 值，在 VS Code 檔案總管循該路徑找到 index.html，再透過系統檔案總管用瀏覽器開啟。每次識別值由面板產生，以下只是目錄形狀，不是要複製執行的命令：

```text
coverage/<範圍識別>/<本次識別>/raw/    本次 Cobertura XML
coverage/<範圍識別>/<本次識別>/html/   本次 index.html 與來源明細
```

報告應包含 Net8 與 Net10 兩個待測程式庫。分別點入各自 BookCatalog 的來源明細，核對兩個版本都能看到行與分支資訊；不要只看到一個模組就當成整個方案收集完成。

HTML 存在只表示報告已產生，仍要對照本次測試結果與範圍。覆蓋率也不等於斷言正確性；解讀方式見[Code Coverage：解讀 HTML](../guides/code-coverage.md#解讀-html)。

## Step 8：改變範圍或比較另一種模式

要改選單一專案、整個方案或重新練習，先保留本次輸出與報告，再重新掃描、選擇範圍及確認條件，取得新一組命令。依序執行新的 Coverage、HTML，再開啟新 targetdir，不混用前後兩組。

| 範圍 | 固定測試數 | XML 份數 | HTML 模組 |
| --- | --- | --- | --- |
| 單一 Net10 專案 | 17 | 1 | Net10 |
| 單一 Net8 專案 | 17 | 1 | Net8 |
| 整個方案 | 34 | 2 | Net8、Net10 |

另一套範例見[MTP 原生 Code Coverage：Step by Step](../coverage-mtp-native/README.md)。兩份指南分別示範單一專案與完整方案，若要比較模式的結果，先在兩套都選相同範圍，不能直接比較不同範圍的總覆蓋率。另有同成員的 .sln 可對照方案格式；不必更動 runner 或升級套件來切換練習。

## 固定設定與參數

`global.json`：SDK 10.0.401、latestPatch、禁止預覽版，runner **VSTest**。兩個測試專案均為 OutputType=Exe、IsTestProject=true，TestingPlatformDotnetTestSupport=true。

| 直接套件 | 版本 |
| --- | --- |
| TUnit | 0.6.123 |
| Microsoft.Testing.Platform／MSBuild | 1.8.4 |
| Microsoft.Testing.Extensions.CodeCoverage | 18.0.6 |

其他來源相依為 AwesomeAssertions 9.1.0、Bogus 35.6.1、NSubstitute 5.3.0、System.IO.Abstractions／TestingHelpers 21.1.3；TimeProvider.Testing 的 Net10 為 10.0.0、Net8 為 8.10.0。

2026-10-16 P3 歷史檢查以 Windows、SDK 10.0.402、ReportGenerator 5.5.11 與 PowerShell 7.6.7 執行完整 .slnx、單一 Net10／Net8 與完整 .sln；PowerShell 5.1 只另查完整 .slnx。本次重寫文件沒有重新執行測試或產生報告，不將歷史結果當作你的本次輸出。
