# Unit：指定方法、單一類別與多類別範圍

這套範例使用三個 .NET 版本呈現三種單元測試需求。操作方式相同：在工具包安裝模式，複製 **Unit 單元測試** 範本，貼到 Claude Code／Codex 擴充套件的對話框，按所選情境修改欄位後送出，觀察工作流程如何處理不同範圍。

## 選擇操作範圍

| .NET 版本 | 範圍 | 本範例的目標 | 提示詞如何指定 | 可用模式 |
| --- | --- | --- | --- | --- |
| [.NET 8](#net-8指定方法) | 指定一個方法 | `TemperatureConverter.CelsiusToFahrenheit` | 填一個類別，保留「範圍」並填方法名 | Claude／Codex Full 或 Lite |
| [.NET 9](#net-9指定單一類別) | 指定整個類別 | `TemperatureConverter` 的公開行為 | 填一個類別，移除「範圍」整行 | Claude／Codex Full 或 Lite |
| [.NET 10](#net-10指定多個類別) | 在同一請求指定多個類別 | `EmployeeService` 與 `EmployeeValidator` | 在「被測試類別」列出兩個來源檔，移除「範圍」整行 | Claude Full／Codex Full |

這是範例情境的分配方式，不表示指定方法只能用 .NET 8、整個類別只能用 .NET 9。套用自己的專案時，依需求選擇範圍，保留該專案的目標框架。Lite 一次只處理一個類別，多類別請求使用 Full。

## 工作區與專案路徑

依[工作區準備與工具設定](../../guides/getting-started.md)複製整份 `samples/`，在 VS Code 開啟副本根目錄，例如 `C:\Temp\dotnet-testing-practice`，檔案總管最上層直接有 `unit/`、`guides/`。工具包、AI 對話與終端機使用同一根目錄。

下表及完整提示詞中的路徑都以這個工作區根目錄計算：

| 版本 | 被測試專案 | 測試專案 | 方案（位於 `unit/practice/`） |
| --- | --- | --- | --- |
| .NET 8 | `unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj` | `unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj` | `Practice.Samples.Net8.slnx` |
| .NET 9 | `unit/practice/src/Practice.Core/Practice.Core.csproj` | `unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj` | `Practice.Samples.slnx` |
| .NET 10 | `unit/practice/src/Practice.Core.Net10/Practice.Core.Net10.csproj` | `unit/practice/tests/Practice.Core.Net10.Tests/Practice.Core.Net10.Tests.csproj` | `Practice.Samples.Net10.slnx` |

.NET 9 沿用來源的 `Practice.Core`／`Practice.Core.Tests` 名稱與 `Practice.Core` 命名空間，檔名沒有 `.Net9` 字尾；其 `TargetFramework` 是 `net9.0`。其他兩版分別使用 `Practice.Core.Net8`、`Practice.Core.Net10` 命名空間。三個方案彼此獨立，測試骨架起點沒有生成的測試答案。

## 從工具包範本送出需求

1. 在工具包開啟 **Claude 模式／Codex 模式**，選擇對應的 **Full／Lite**。於 **總覽** 安裝模式並執行 **環境診斷**；已安裝者核對狀態。安裝操作見[在工具包安裝模式，再到 AI 對話啟動工作流程](../../guides/mode-setup.md)。
2. 在 **Skills** 與 **Agents** 查看這次提供的技術指引、流程入口與角色，再切到 **預設提示詞 → Unit 單元測試**，按 **複製提示詞**。
3. 切到同一工作區的 Claude Code／Codex 擴充套件對話框，貼上完整範本；先不要送出。
4. 選擇下方一個情境，保留所選模式的第一行，填入專案、類別、測試專案與說明；依方法／類別範圍決定是否保留「範圍」欄。對照該模式的完整提示詞，確認沒有 `<...>` 佔位文字。
5. 送出後，觀察 AI 使用已安裝工作流程的分析、撰寫、執行與審查分工。Full 與 Lite 分工不同，不需要手動為每個角色另開對話。
6. 查看指定測試目錄的新增檔案、實際執行結果與審查意見。若只收到計畫或程式碼片段，留在原對話要求依指定工作流程繼續；執行失敗則保留輸出並接續處理。

提示詞的 `/dotnet-...` 或 `$dotnet-...` 放在 AI 對話框，不是終端機命令。每個情境只選與你安裝模式相同的一份提示詞；比較其他模式時另建副本。

## .NET 8：指定方法

需求：只處理 `TemperatureConverter.CelsiusToFahrenheit`，讓其他方法維持本輪範圍之外。這適合單一方法新增或調整後，提出明確的測試需求。

| 要改的欄位 | 內容 |
| --- | --- |
| 被測試專案／測試專案 | 使用上表的 .NET 8 路徑 |
| 被測試類別 | `TemperatureConverter` |
| 範圍 | `CelsiusToFahrenheit`，保留這一行 |
| 說明 | 指定此方法的轉換值與邊界；.NET 8 此方法沒有低於絕對零度的例外防護，不套用 .NET 10 的規則 |

### Claude Full

```text
使用 /dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj
被測試類別：TemperatureConverter
範圍：CelsiusToFahrenheit
測試專案：unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj
說明：只針對 CelsiusToFahrenheit 產生單元測試，不擴大到其他方法。驗證攝氏 0 → 華氏 32、100 → 212、-273.15 → 約 -459.67，以及 -273.16 仍回傳約 -459.688 且不擲出例外；依 .NET 8 既有實作判定，不套用其他版本的輸入防護。以絕對誤差 1e-10 比較浮點數，不修改待測程式或其他版本專案，完成測試執行與審查。
```

### Claude Lite

```text
使用 /dotnet-testing-lite-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj
被測試類別：TemperatureConverter
範圍：CelsiusToFahrenheit
測試專案：unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj
說明：只針對 CelsiusToFahrenheit 產生單元測試，不擴大到其他方法。驗證攝氏 0 → 華氏 32、100 → 212、-273.15 → 約 -459.67，以及 -273.16 仍回傳約 -459.688 且不擲出例外；依 .NET 8 既有實作判定，不套用其他版本的輸入防護。以絕對誤差 1e-10 比較浮點數，不修改待測程式或其他版本專案，完成測試執行與審查。
```

### Codex Full

```text
使用 $dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj
被測試類別：TemperatureConverter
範圍：CelsiusToFahrenheit
測試專案：unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj
說明：只針對 CelsiusToFahrenheit 產生單元測試，不擴大到其他方法。驗證攝氏 0 → 華氏 32、100 → 212、-273.15 → 約 -459.67，以及 -273.16 仍回傳約 -459.688 且不擲出例外；依 .NET 8 既有實作判定，不套用其他版本的輸入防護。以絕對誤差 1e-10 比較浮點數，不修改待測程式或其他版本專案，完成測試執行與審查。
```

### Codex Lite

```text
使用 $dotnet-testing-lite-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj
被測試類別：TemperatureConverter
範圍：CelsiusToFahrenheit
測試專案：unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj
說明：只針對 CelsiusToFahrenheit 產生單元測試，不擴大到其他方法。驗證攝氏 0 → 華氏 32、100 → 212、-273.15 → 約 -459.67，以及 -273.16 仍回傳約 -459.688 且不擲出例外；依 .NET 8 既有實作判定，不套用其他版本的輸入防護。以絕對誤差 1e-10 比較浮點數，不修改待測程式或其他版本專案，完成測試執行與審查。
```

觀察重點：新增測試應位於 `unit/practice/tests/Practice.Core.Net8.Tests/`，測試目標為 `Practice.Core.Net8.TemperatureConverter.CelsiusToFahrenheit`。確認結果回應指定方法的情境，沒有把整個類別的其他方法也當成本輪任務。

## .NET 9：指定單一類別

需求：將整個 `TemperatureConverter` 交給工作流程分析，涵蓋攝氏／華氏／克氏轉換、冰點與沸點判斷及溫度等級。這適合對一個類別建立測試，而不是逐個方法送出請求。

| 要改的欄位 | 內容 |
| --- | --- |
| 被測試專案／測試專案 | 使用上表的 .NET 9 路徑，名稱沒有 `.Net9` 字尾 |
| 被測試類別 | `TemperatureConverter` |
| 範圍 | 移除此欄整行，讓請求以整個類別為範圍 |
| 說明 | 請工作流程分析並測試該類別全部公開方法，涵蓋各自的正常、邊界與例外行為 |

### Claude Full

```text
使用 /dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core/Practice.Core.csproj
被測試類別：TemperatureConverter
測試專案：unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj
說明：以整個 TemperatureConverter 類別為範圍，分析並撰寫全部公開方法的單元測試：CelsiusToFahrenheit、FahrenheitToCelsius、CelsiusToKelvin、KelvinToCelsius、IsBelowFreezing、IsAboveBoiling、GetTemperatureLevel。依 .NET 9 原碼涵蓋轉換值、冰點／沸點與溫度等級邊界，以及兩個克氏轉換方法的 ArgumentException；不要替沒有輸入防護的方法新增例外契約。保留待測程式與現有版本，完成測試執行與審查。
```

### Claude Lite

```text
使用 /dotnet-testing-lite-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core/Practice.Core.csproj
被測試類別：TemperatureConverter
測試專案：unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj
說明：以整個 TemperatureConverter 類別為範圍，分析並撰寫全部公開方法的單元測試：CelsiusToFahrenheit、FahrenheitToCelsius、CelsiusToKelvin、KelvinToCelsius、IsBelowFreezing、IsAboveBoiling、GetTemperatureLevel。依 .NET 9 原碼涵蓋轉換值、冰點／沸點與溫度等級邊界，以及兩個克氏轉換方法的 ArgumentException；不要替沒有輸入防護的方法新增例外契約。保留待測程式與現有版本，完成測試執行與審查。
```

### Codex Full

```text
使用 $dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core/Practice.Core.csproj
被測試類別：TemperatureConverter
測試專案：unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj
說明：以整個 TemperatureConverter 類別為範圍，分析並撰寫全部公開方法的單元測試：CelsiusToFahrenheit、FahrenheitToCelsius、CelsiusToKelvin、KelvinToCelsius、IsBelowFreezing、IsAboveBoiling、GetTemperatureLevel。依 .NET 9 原碼涵蓋轉換值、冰點／沸點與溫度等級邊界，以及兩個克氏轉換方法的 ArgumentException；不要替沒有輸入防護的方法新增例外契約。保留待測程式與現有版本，完成測試執行與審查。
```

### Codex Lite

```text
使用 $dotnet-testing-lite-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core/Practice.Core.csproj
被測試類別：TemperatureConverter
測試專案：unit/practice/tests/Practice.Core.Tests/Practice.Core.Tests.csproj
說明：以整個 TemperatureConverter 類別為範圍，分析並撰寫全部公開方法的單元測試：CelsiusToFahrenheit、FahrenheitToCelsius、CelsiusToKelvin、KelvinToCelsius、IsBelowFreezing、IsAboveBoiling、GetTemperatureLevel。依 .NET 9 原碼涵蓋轉換值、冰點／沸點與溫度等級邊界，以及兩個克氏轉換方法的 ArgumentException；不要替沒有輸入防護的方法新增例外契約。保留待測程式與現有版本，完成測試執行與審查。
```

觀察重點：測試寫入 `unit/practice/tests/Practice.Core.Tests/`，使用 `Practice.Core.TemperatureConverter`。分析與審查應回應整個類別的公開方法；只產生 CelsiusToFahrenheit 測試，尚未涵蓋這次指定的類別範圍。

## .NET 10：指定多個類別

需求：在同一個請求指定 `EmployeeService` 與 `EmployeeValidator`。兩者使用相同員工模型，但驗證規則與時間來源不同；工作流程應分別分析、撰寫、執行與審查各目標，再彙整結果。這適合一次處理一組相關類別。

本情境選 **Claude Full／Codex Full**。Lite 一次只處理一個類別；若使用 Lite，需分成各自的單類別請求，並非本節的一次多類別操作。

| 要改的欄位 | 內容 |
| --- | --- |
| 被測試專案／測試專案 | 使用上表的 .NET 10 路徑，兩個目標使用同一測試專案 |
| 被測試類別 | 列出 `Services/EmployeeService.cs` 與 `Validators/EmployeeValidator.cs` 的完整工作區相對路徑，見下方提示詞 |
| 範圍 | 移除此欄整行；本輪對兩個指定類別分別處理公開行為 |
| 說明 | 指明兩個目標均要完成測試與審查，不把服務與驗證器的規則混為一套 |

### Claude Full

```text
使用 /dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net10/Practice.Core.Net10.csproj
被測試類別：
- unit/practice/src/Practice.Core.Net10/Services/EmployeeService.cs
- unit/practice/src/Practice.Core.Net10/Validators/EmployeeValidator.cs
測試專案：unit/practice/tests/Practice.Core.Net10.Tests/Practice.Core.Net10.Tests.csproj
說明：本次同時指定 EmployeeService 與 EmployeeValidator 兩個類別，請對每個目標分別分析公開行為、撰寫單元測試、執行並審查，再彙整各自結果。涵蓋 EmployeeService 的員工驗證、獎金、摘要、部門薪資分析與報告，以及 EmployeeValidator 的欄位規則與日期邊界。兩者規則依各自原碼判定：EmployeeService.ValidateEmployee 接受薪資 0，EmployeeValidator 則要求大於 0；前者使用系統日期，後者可注入 TimeProvider，不將兩者強行統一。測試只寫入指定測試專案，不修改待測程式或其他版本專案。
```

### Codex Full

```text
使用 $dotnet-testing-orchestrator-unit 撰寫單元測試
被測試專案：unit/practice/src/Practice.Core.Net10/Practice.Core.Net10.csproj
被測試類別：
- unit/practice/src/Practice.Core.Net10/Services/EmployeeService.cs
- unit/practice/src/Practice.Core.Net10/Validators/EmployeeValidator.cs
測試專案：unit/practice/tests/Practice.Core.Net10.Tests/Practice.Core.Net10.Tests.csproj
說明：本次同時指定 EmployeeService 與 EmployeeValidator 兩個類別，請對每個目標分別分析公開行為、撰寫單元測試、執行並審查，再彙整各自結果。涵蓋 EmployeeService 的員工驗證、獎金、摘要、部門薪資分析與報告，以及 EmployeeValidator 的欄位規則與日期邊界。兩者規則依各自原碼判定：EmployeeService.ValidateEmployee 接受薪資 0，EmployeeValidator 則要求大於 0；前者使用系統日期，後者可注入 TimeProvider，不將兩者強行統一。測試只寫入指定測試專案，不修改待測程式或其他版本專案。
```

觀察重點：在 `unit/practice/tests/Practice.Core.Net10.Tests/` 找到兩個目標各自的測試，回覆應能辨識各目標的分析、執行與審查結果。相同模型不代表相同驗證契約；薪資 0 與日期邊界是可直接對照的差異。測試檔名與案例數由實際分析產生，不規定固定數量。

## 查看與重跑成果

從 AI 對話確認實際寫入的檔案、執行命令、成功／失敗數與審查意見，再到所選版本的 `tests/` 查看測試。成功建置或零測試的退出碼，不代表已執行指定範圍的測試。

如需自行重跑，在工作區根目錄的 PowerShell 選用對應方案：

```powershell
Push-Location -LiteralPath '.\unit\practice'
try {
    # 依所選情境使用其中一個方案：
    # .NET 8 方法：Practice.Samples.Net8.slnx
    # .NET 9 類別：Practice.Samples.slnx
    # .NET 10 多類別：Practice.Samples.Net10.slnx
    $solution = 'Practice.Samples.Net8.slnx'
    dotnet test $solution --verbosity normal
    if ($LASTEXITCODE -ne 0) { throw '請保留測試失敗輸出，接續原工作流程處理。' }
} finally {
    Pop-Location
}
```

保留各輪提示詞、生成的測試、失敗與修正、執行及審查結果。要用於自己的專案時，沿用相同操作，依需求指定方法、單一類別或多類別，見[套用到自己的專案](../../guides/apply-to-your-project.md)。

## 版本與套件

三個方案共用 `unit/practice/global.json` 的 SDK 選取政策：SDK 10.0.100 起、`rollForward=latestFeature`、不使用預覽版，test runner 為 VSTest。執行各版本測試另需相應的 .NET 8／9／10 runtime；專案目標框架與用來讀取 `.slnx` 的 SDK 版本分開判讀。

| 套件 | Net8 | Net9 | Net10 |
| --- | --- | --- | --- |
| FluentValidation（待測專案） | 11.11.0 | 11.11.0 | 11.11.0 |
| System.IO.Abstractions（待測專案） | 21.1.3 | 21.1.3 | 21.1.3 |
| xunit／runner／Test SDK | 2.9.2／2.8.2／17.12.0 | 相同 | 相同 |
| coverlet.collector | 6.0.2 | 6.0.2 | 6.0.2 |
| AwesomeAssertions／NSubstitute | 9.1.0／5.3.0 | 相同 | 相同 |
| AutoFixture／Xunit2／AutoNSubstitute | 4.18.1 | 4.18.1 | 4.18.1 |
| Bogus | 35.6.1 | 35.6.1 | 35.6.1 |
| Microsoft.Extensions.TimeProvider.Testing | 8.10.0 | 9.0.0 | 10.1.0 |
| System.IO.Abstractions.TestingHelpers | 21.1.3 | 21.1.3 | 21.1.3 |
| System.Net.Http（限定修補參照） | 4.3.4 | 未新增 | 未新增 |
| System.Text.RegularExpressions（限定修補參照） | 4.3.1 | 未新增 | 未新增 |

.NET 9 的待測程式、專案、測試骨架及方案直接沿用來源內容，沒有升級套件或加入已生成測試。.NET 8、10 的待測程式與套件也未因本次情境重整而改動。

2026-10-17 先前以 SDK 10.0.402、Windows x64，從公開 nuget.org 檢查的是 .NET 8／10 共四個 Unit 專案，當時直接及傳遞相依未回報已知弱點；另在獨立副本檢查 AutoFixture、AutoData、Frozen／NSubstitute，兩版各 10 項通過，runtime 為 8.0.32／10.0.13。當時也分別核對兩版溫度轉換方法的既有行為，各 10 項通過，並保留錯套例外型別的失敗。這些是原輪限定結果，不能套用成新增 .NET 9 或本次三種範圍的重新執行結果。

本次補入 .NET 9 與重整三種操作範圍，只核對來源、專案參照及文件，沒有重新執行建置、套件 audit、測試或 AI 流程。固定套件是教材起點，不應整份覆寫自己專案的技術選擇。
