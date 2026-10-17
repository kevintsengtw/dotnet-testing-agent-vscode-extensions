# 套用到自己的專案

在自己的專案分支沿用 [Unit：選擇操作範圍](../unit/practice/README.md#選擇操作範圍)所示的工具包與 AI 對話操作。保留既有來源、測試與設定，透過 Extension 安裝需要的模式，不把範例的 `.agents`、`.claude`、`.codex` 或管理紀錄複製過去。

## 沿用範例操作

1. 在 VS Code 開啟你自己的專案根目錄，讓 AI 擴充套件的對話也使用這個工作區。
2. 在測試工具包選擇適合的 Claude／Codex Full 或 Lite 模式，安裝到這個專案，再執行 **環境診斷**。範例副本已安裝，不代表自己的專案也已安裝。
3. 在 **預設提示詞** 選擇要做的測試類型，按 **複製提示詞**，貼到目前專案的 Claude Code／Codex 對話框。
4. 保留第一行的工作流程名稱，修改任務描述以符合本輪範圍（例如 Codex Aspire 限定端點時移除「涵蓋全部端點」）；按下表替換成自己專案的路徑、目標與情境，再送出。
5. 檢視 AI 實際新增的測試、執行結果與審查意見；不要直接把範例的測試答案或套件設定搬過去。

## 替換提示詞的內容

以下以 .NET 8 方法情境示範欄位替換。選擇單類別或多類別時，依下方範圍說明調整同一份範本：

| 範例中的內容 | 換成你的專案 |
| --- | --- |
| `unit/practice/src/Practice.Core.Net8/Practice.Core.Net8.csproj` | 工作區內實際待測 `.csproj` 的相對路徑 |
| `TemperatureConverter` | 指定目標類別；有同名類別時填來源路徑。多類別請求使用 Full，逐項列出各目標名稱或路徑 |
| `CelsiusToFahrenheit` | 指定方法時填方法名；指定整個類別或多個類別時，移除「範圍」整行 |
| `unit/practice/tests/Practice.Core.Net8.Tests/Practice.Core.Net8.Tests.csproj` | 對應且可建置的測試專案相對路徑 |
| 攝氏轉華氏的情境 | 你的正常、邊界與錯誤行為，以及需要保留的既有規則 |

範圍依需求選擇：指定方法時保留「範圍」並填方法名；指定整個類別時移除此欄；一次處理多個類別時，使用 Full 並在「被測試類別」列出各自的名稱或來源路徑。Unit 的 .NET 8／9／10 是三種情境的教材分配，不需要為了選擇範圍而變更自己專案的 .NET 版本。

所有路徑以 AI 工具與 Extension 共同使用的工作區根目錄為基準。Lite 一次只指定一個類別；不要用「整個服務層」取代具體目標。

## 保留自己的技術選擇

確認目標框架、測試框架、套件版本與 ProjectReference；不要為了使用範例提示詞，把自己的專案直接改成 Net10 或套用範例的所有套件。若使用的是不同框架，先確認選用流程是否適合。

時間相依、檔案系統或外部服務的測試，需說明實際相依與隔離方式。TUnit 的新測試與 xUnit 移轉請參考[TUnit：指定方法、整個類別與 xUnit 移轉](../tunit/practice_tunit/README.md)；[Integration：API 操作、單一 Controller 與多個目標](../integration/practice_integration/README.md) 使用 WebApplicationFactory 與測試容器；[Aspire：指定端點、預約流程與跨服務檢查](../aspire/practice_aspire/README.md) 另指定 AppHost、Web API 與服務名稱。Integration 用「被測試目標」列出 API 操作、Controller 或多個類別；Aspire 的「被測試專案」填 AppHost，「被測試目標」填 API 與範圍；TUnit 移轉另寫明來源檔／來源專案與保留情境。先核對[容器練習前置條件與隔離](containers.md)，不把示範連線或固定資料名稱搬入自己的環境。

## 檢查交付內容

檢視新增與修改檔案、套件及專案參照，確認測試驗證預期行為，沒有為了讓測試通過而不必要地改寫待測程式。保留建置、測試數、失敗與審查結果；成功還原、成功建置或沒有測試的退出碼不能代替測試完成。

如需 Code Coverage，先有實際執行的測試，再核對自己的 runner、覆蓋率套件與報告工具。Extension 只會掃描與產生指令，仍由你在所選工作區根目錄執行同組 Coverage 與 HTML 指令。
