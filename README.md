## サンプル演習
---
## 目的
- 【〇】Blazorの基本的な使い方を理解する
- 
---
## 環境
- 【〇】言語：C#
- 【〇】画面: Microsoft.NET.Sdk.BlazorWebAssembly (Client),
             WebServer (ASP.NET Core WEB API)、電卓共通（クラスライブラリ)
- 【〇】IDE：VisualStudio2022 （バージョンは問いません）
---
## 動作確認方法
- サーバーとクライアントを同時にデバッグ実行。
- 想定として、実績取集のWEBアプリを想定して作成。タッチパネルで操作。
---
## 未実装
- 数量入力のテキストボックスの桁数制限
- 各項目のコンボボックス以外の文字、数値その他入力制限
- チェック実行後のチェック対象項目へのフォーカス移動
- エンターキーで次項目へ進む
- テンキーが電卓に対応して動く（イコールまたは音声などで数量へ反映）
- 保存後、ボタンを `Enable=false` にできていない。（要修正）
- 製品の生産完了時（プラス）に、必要部品および構成品の在庫払い出し
- 製品の生産完了時（マイナス）に、必要部品および構成品の在庫戻し
- 音声入力、音声イベント
---
