## サンプル演習
---
## 目的
- Blazorの基本的な使い方を理解する
- ※もともとある企業の入社試験課題だったもの
- 
- 　課題内容：WPFで電卓を作る（これだけ）
- 
- 　課題変更：WPFは得意ですと伝えたところ、何故か
- 　　　　　　WEBで構築せよと、Blazorを指定される。
- 　　　　　　詳細な仕様指定はWPFと違って指定がなかったので
- 　　　　　　一応十数年のエンジニアなので、電卓だし・・・
- 　　　　　　すぐ作成できてしまいました・・・
- 
- 　　　　　　※WPFならばログ４ｊを使ってログ出力をする指定や
- 　　　　　　　実装のデザインパターンの指定、MVVMモデルでとか
- 　　　　　　　アプリ関係の事を指定してきたが
- 　　　　　　　指定されたのがWEBでしたので・・・
-
- 　現状状況：面白そうな企業だったので、何かアピールできればと
- 　　　　　　関連する生産管理系のシステムをつくろうと
- 　　　　　　現場実績入力アプリを構築しようとしたが（Webで…）
- 　　　　　　別会社に内定が決まったので、ご縁が無くなってしまいました。
- 　　　　　　生産管理系のシステムや、Blazorに、また
- 　　　　　　関わる事があればまた触るかもしれない。
---
## 環境
- 言語：C#
- 開発：
- Microsoft.NET.Sdk.BlazorWebAssembly (Client),
- WebServer (ASP.NET Core WEBAPIの構築),
- 電卓共通モジュール（クラスライブラリで作成)
- IDE：VisualStudio2022, Sqlite3
---
## 動作確認方法
- サーバーとクライアントを同時にデバッグ実行。
- 実績取集WEBアプリを想定、モバイル対応。
- タッチパネルで操作を想定。
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
