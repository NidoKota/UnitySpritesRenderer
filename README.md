# UnitySpritesRenderer
![image](https://user-images.githubusercontent.com/36328961/188186553-e6ef5d16-7a76-48e8-9bd4-ec540cfe0dc4.png)
メッシュに複数のスプライトを自由に描画できるツール<br>
1つのメッシュで作られた軽量な顔モデルでも、自由自在に目や口を動かすことができます<br>

# 動画
https://user-images.githubusercontent.com/36328961/188193514-9abb0d6f-43d6-4256-9e73-9b2139d67683.mp4

# できること
<img width="500" alt="" src="https://user-images.githubusercontent.com/36328961/188253289-2d0de889-1774-4d88-824a-0589594cea87.png"><br>
- スプライトを追加して移動、回転、拡大/縮小、反転ができます<br>
- シェーダー内でスプライト同士のアルファブレンディングを行っているので、透明度を自由に変更できます<br>
- スプライトを登録せずに、TileとOffsetで調整する事もできます<br>

もちろんアニメーションも可能です<br>

# ツール
<img width="500" alt="" src="https://user-images.githubusercontent.com/36328961/188251650-87a07fba-1e38-4d9b-829c-b752153621b9.png"><br>
スプライトを変形させられるツールを備えています<br>
Moveツール、Rotateツール、Rectツールと同じような操作感でスプライトを変形できます<br>

# 新しいスプライトの追加手順
<img width="400" alt="" src="https://user-images.githubusercontent.com/36328961/188252597-6965bbd7-e847-4a3f-bdf7-59dfd0c3f39a.png"><br>
1.テクスチャに画像を追加します<br><br>
<img width="400" alt="" src="https://user-images.githubusercontent.com/36328961/188253247-85daa27b-f48d-4266-8fc9-eed315c5efd6.png"><br>
2.SpritePropWrapperを付与したゲームオブジェクトを作成します<br><br>
<img width="400" alt="" src="https://user-images.githubusercontent.com/36328961/188253123-7cb3b0ae-82bf-4b53-bd07-141eb6e0f28a.png"><br>
3.SpritePropWrapperをSetSpritesのListに登録します<br>
画像をスプライトの項目に登録します<br><br>

# ライセンス
<a href="LICENSE" target="_blank" rel="noopener noreferrer">MIT License</a><br>
