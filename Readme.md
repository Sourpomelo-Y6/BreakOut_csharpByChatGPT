# BreakOut

ChatGPTにC#でWPFのブロック崩しプログラムを作ってもらい、動作しなかったので手動で修正しました。

![ScreenShot](ScreenShot.png)


## GPT3.5Plusに出してもらったUML図

```
UMLを用いた設計の流れ
```
と聞いてから
```
Mermaid形式でブロック崩しのユースケース図を出してください
```
という風に聞いて行きました。

### ユースケース図
```mermaid
graph TD;
  A[プレイヤー] -->|パドルを動かす| B(パドル)
  B -->|ボールを打ち返す| C[ボール]
  C -->|ブロックを壊す| D[ブロック]
  D -->|ゲームクリア| E[ゲームクリア]
  C -->|壁に当たる| F[壁]
  F -->|ボールの反射| C
  C -->|パドルに当たる| B
  C -->|地面に落ちる| G[ゲームオーバー]
```

### クラス図

```mermaid
classDiagram
  class Ball{
    +speedX: float
    +speedY: float
    +x: float
    +y: float
    +move(): void
    +reverseX(): void
    +reverseY(): void
  }
  class Paddle{
    -width: float
    -height: float
    +x: float
    +y: float
    +moveRight(): void
    +moveLeft(): void
  }
  class Block{
    -width: float
    -height: float
    +x: float
    +y: float
    +isBroken: bool
    +break(): void
  }
  class Game{
    -score: int
    -ball: Ball
    -paddle: Paddle
    -blocks: Block[]
    +start(): void
    +pause(): void
    +resume(): void
    +reset(): void
    +update(): void
  }
  
  Ball --> Paddle
  Ball --> Block
  Game --> Ball
  Game --> Paddle
  Game --> Block

```
### シーケンス図
```mermaid
sequenceDiagram
  participant Player
  participant Paddle
  participant Ball
  participant Block
  participant Game
  
  Player->>Paddle: パドルを動かす
  Paddle->>Ball: ボールを打ち返す
  Ball->>Block: 衝突判定
  Block->>Game: 壊れたかどうかを報告
  Game->>Block: 得点を加算
  Game->>Ball: 次の位置を計算
  Game->>Paddle: 次の位置を計算
  Game->>Player: 次の状況を表示
```

### アクティビティ図
```mermaid
graph TD;
  A[開始] --> B{ゲーム中?}
  B -- Yes --> C[パドルの移動]
  C --> D[ボールの移動]
  D --> E{ブロックを全て破壊?}
  E -- No --> C
  E -- Yes --> F[ゲームクリア]
  F --> G[終了]
  B -- No --> H[ゲームオーバー]
  H --> G
```

### コンポーネント図
```mermaid
graph TD;
  A[ブロック崩し] --> B[ゲーム画面]
  A --> C[ゲーム処理]
  C --> D[ボール処理]
  C --> E[パドル処理]
  C --> F[ブロック処理]
  C --> G[得点処理]
  B --> H[画面表示]
  B --> I[入力処理]
```

## 実装もChatGPTにお願いする

Chatが長くなるといろいろと不都合が発生するためNewChatにて、
```
以下のmarmaid形式のクラス図を基にC#でクラスを実装してください。
(先ほどのmarmaid形式クラス図)
```
と入力した後、
```
C#のWPFで実装してみてください。
```
と
```
先ほどのコードに対してのGameクラスを出力してください。
```
で出力されたものをプロジェクトに手動でまとめました。
（ただし、途中で止まってしまうのでその都度、関数〇〇から後の分を出力してくださいと入力）

でも最初は動作しませんでした。

## 手動で修正

### 修正した問題
・イベントハンドラーが巡回<br/>
・別スレッドからUIを操作しているのにTask.Runを使っていない<br/>
・ブロックに反射しない（デフォルトで貫通玉）<br/>
・ゲームエリアとbackgroundが同じ色で区別できない<br/>

### 残っている問題
・めり込むと反射がおかしくなる<br/>

・MVVMに準拠させる（作業中）<br/>
