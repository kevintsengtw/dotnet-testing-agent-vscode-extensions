# Code Coverage 操作指南

工具包會掃描方案與測試專案，產生可複製的 Coverage／HTML 命令；你在 PowerShell 執行後，就能用瀏覽器檢視覆蓋率。兩套 MTP 教材已附固定測試，無須先安裝 AI 模式或讓 AI 生成測試。

## 先選一條完整操作路徑

| 教材 | 操作情境 | 分步指南 |
| --- | --- | --- |
| MTP 原生 | 單一 .NET 10 測試專案 → 一份 XML → 一個模組的 HTML | [MTP 原生 Code Coverage：Step by Step](../coverage-mtp-native/README.md) |
| MTP 舊版相容 | .NET 8＋10 整個方案 → 兩份 XML → 合併 HTML | [MTP 舊版相容 Code Coverage：Step by Step](../coverage-mtp-legacy/README.md) |

選一套依序操作即可。兩份指南都有面板填值、執行位置、成功時應看見的內容與失敗時回查位置；本頁補充共用工具、產物對應與閱讀報告的方法。

## 前置環境

### Step 1：核對 SDK、runtime 與報告工具

在 VS Code 安裝 dotnet-testing-agent，取得 samples 的 git／ZIP 並建立整份副本。開啟直接包含兩套 coverage 目錄的副本根目錄，整合終端機也使用同一目錄。取得與安裝方式見[工作區準備與工具設定](getting-started.md)。

兩套 global.json 固定 SDK 10.0.401、latestPatch，需同功能帶的正式修補版，不跨其他功能帶；單一 Net10 需 .NET 10 runtime，Net8 或整個方案另需 .NET 8 runtime。

在 PowerShell 查看可用工具：

```powershell
dotnet --list-sdks
dotnet --list-runtimes
dotnet tool list --global
Get-Command reportgenerator
```

確認 SDK 清單有符合 global.json 的版本、runtime 符合本輪目標，且 Get-Command 找得到 reportgenerator。若報告工具尚未安裝，可自行執行這份教材歷史檢查所用的固定版本；已有工具者先核對，不必重裝：

```powershell
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.5.11
```

安裝後找不到命令時，重新開啟 PowerShell 並檢查 PATH。Extension 不會代為安裝工具、修改 global.json 或執行測試。

## 在 Code Coverage 面板操作

### Step 2：掃描並選定範圍

開啟工具包的 **Code Coverage**，核對 **工作區** 與終端機一致，按 **重新掃描**。依所選範例指南設定 **方案／測試專案／測試平台／MTP 指令模式**。若選整個方案，測試專案選 **不指定專案（使用所選方案）**。

兩套的測試平台都選 **Microsoft.Testing.Platform（MTP）**；原生搭配 **原生模式（.NET 10 以上）**，相容搭配 **舊版相容模式**。即使相容範例的 global.json runner 為 VSTest，面板平台仍是 MTP。

### Step 3：閱讀前置條件並確認

展開 **工具與設定前置條件**，核對 SDK／runner／測試專案／覆蓋率套件。確認實際環境後勾選前置條件確認框，取得可用命令。已知衝突不會因勾選而消失；有錯誤時先看訊息與[問題排查](troubleshooting.md)。

### Step 4：執行本組 Coverage

在 **Coverage command** 按 **複製**，看到 **已複製** 後，把完整文字貼到副本根目錄的 PowerShell 執行。命令內有暫時切換設定目錄與返回的處理，相容模式另有參數傳遞設定，請整段保留。

讀取測試輸出並檢查 `$LASTEXITCODE`、實際測試數與該組 raw 目錄的 Cobertura XML。失敗時保留輸出，先釐清問題；零測試、舊 XML 或其他歷史 HTML 都不能代表這次收集成功。

### Step 5：執行同組 HTML 並開啟結果

保留原選擇，先不要重新掃描。在 **HTML Report command** 按 **複製**，於同一根目錄 PowerShell 執行，確認退出碼 0。從這段命令的 **-targetdir** 找到本次目錄，用瀏覽器開啟其中的 index.html，再依下一節閱讀。

若找不到 XML，回頭核對 Step 4 的結果與兩組路徑。不要把 reports 改為所有歷史結果；那會讓不同次執行混在一起。

## 指令與產物如何對應

```text
coverage/<範圍識別>/<本次識別>/raw/    Cobertura XML
coverage/<範圍識別>/<本次識別>/html/   index.html 與來源明細
```

Coverage 與 HTML 共用同一組識別。Coverage 會暫時切到方案設定目錄，使 SDK 讀到正確 runner；HTML 從工作區根目錄讀取該組 raw。識別值由面板產生，不需手動取名。

| 選擇 | 固定測試數 | XML 份數 | 報告模組 |
| --- | --- | --- | --- |
| 單一 Net10 | 17 | 1 | Net10 |
| 單一 Net8 | 17 | 1 | Net8 |
| 整個方案 | 34 | 2 | Net8、Net10 |

## 解讀 HTML

### Step 6：核對報告範圍與本次結果

先看模組清單是否符合本輪所選專案或方案，再進入 BookCatalog 的來源明細。查看行覆蓋與分支資訊，對照固定測試的 ISBN、折扣、罰款、頁數分類與索引碼案例，找出仍未執行的路徑。

這些測試刻意不涵蓋所有方法與例外。可在自己的副本補測，保留原意圖，重新掃描取得新一組命令，重做 Coverage／HTML，再比較結果；不必刪掉前一份報告。增加覆蓋行數後，仍須閱讀斷言與測試結果，不能由百分比推論程式沒有錯誤。

2026-10-16 P3 歷史紀錄中，各 BookCatalog 為 48／62 行（約 77.42%）；這是當時固定測試的**類別行覆蓋率**，不是整個方案數值，也不是本次文件整理重新執行的結果。

## 模式比較與重新執行

| 設定 | 原生範例 | 舊版相容範例 |
| --- | --- | --- |
| global.json runner | Microsoft.Testing.Platform | VSTest |
| TestingPlatformDotnetTestSupport | false | true |
| MTP／MSBuild | 1.8.4 | 1.8.4 |
| CodeCoverage | 18.0.6 | 18.0.6 |

兩套不能只靠切換面板選單互換設定。比較結果時在兩套都選相同專案範圍；預設指南各示範單一專案與完整方案，總覆蓋率不宜直接比較。一般 TUnit 空骨架的相依配置也不同，不能當作這兩套範例。

重新掃描或改選範圍後，重新確認條件並使用新一組 Coverage／HTML；不要混用前後兩組或重複使用同一組累積結果。

## 歷史環境與本次界線

P3 於 2026-10-16 使用 Windows、SDK 10.0.402、ReportGenerator 5.5.11、PowerShell 7.6.7，檢查兩套完整 .slnx、單一 Net10／Net8 與完整 .sln；Windows PowerShell 5.1 只另查兩個完整 .slnx。這些歷史結果未延伸到其他平台；本次僅重寫文件、核對設定與連結，沒有重跑命令或修改測試。
