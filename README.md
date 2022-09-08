
![이미지](https://cdn.discordapp.com/attachments/892285347352936470/930510454109904956/unknown.png)  

# 게임 소개  

이름 : Boost On

장르 : 3d 바이크 레이싱 게임

사용 tools : Unity 2019.4.18f1, Visual Studio 2019

개발 기간 : 2021/06 ~ 2021/09

인원 : 싱글 플레이

플랫폼 : PC

승리조건 : 정해진 코스를 ai보다 빠르게 달려서 1등으로 완주하기  
  
  
# 구현 방법  

### 카메라 처리  

#### 사용 패키지 : Unity Post-Processing

=> Lens Distortion(컴포넌트) : 화면에 왜곡을 주는 카메라 효과   
일정이상 속도가 높아지면 화면에 왜곡효과를 주어 플레이어가 느끼는 속도감을 높임.  

=> Motion Blur(컴포넌트) : 화면의 사각부분을 번져보이게 하는 카메라 효과  
일정이상 속도가 높아지면 화면가장자리에 블러처리를 하여 플레이어가 느끼는 속도감을 높임.  

카메라에 애니메이션을 넣어 게임 시작 전에 플레이어가 코스를 확인할 수 있게 하는 연출을 넣음.  

게임 내 설정기능에서 모션블러와 화면 왜곡의 정도를 조절 할 수 있음.

## 코스 제작  

#### 사용 에셋 : ArtisticMechanics(무료)

무료 에셋으로 제공된 터레인과 오브젝트들을 다듬어서 주변 지형을 제작함    
유니티 프로파일링을 돌려보니 렌더링에서 메모리 점유율이 너무 높은걸 발견    
=> 터레인의 맵 텍스쳐 픽셀을 1/4로 줄이고, 터레인을 포함한 맵에 Occlusion Culling을 bake하여 들어가는 렌더링 값을 감소시킴
(맵 대부분의 영역에서 카메라가 위치할 수 있기 때문에 Occlusion Area를 따로 정의하지 않음)

## 코스 완주

 맵에 체크포인트를 3개 부여(transform)  
체크포인트 3개를 모두 지나면 1바퀴 주행으로 간주함. 그렇게 3바퀴를 돌면 완주.  
(통로상 반드시 지나가야 하는 곳에 체크포인트를 둬서 플레이어 및 AI가 체크포인트를 통과하도록 유도함)  

## 순위체크 및 랭킹  

#### 실시간 순위 체크  

플레이어와 AI들의 주행정보를 담는 클래스를 선언  
리스트를 통해 각각의 데이터를 비교하여 순위를 구별함.  

#### 결과화면

게임이 끝나면 순위데이터를 가지고 있던 오브젝트에서  
UI매니저로 데이터를 보냄. 이후 순차적으로 UI창에 정보를 표시.  

## AI  

사용 컴포넌트 : Unity Navigation

Navigation을 bake하여 AI들이 지정된 범위 안에서 움직일 수 있도록 제한을 둠.  
AI가 지정된 경로를 따라가기만 하면 재미가 없으니  
체크포인트를 두 타입으로 만들어서 AI가 랜덤으로 체크포인트를 선택하여 주행하게 함.  

## 플레이어 주행

유튜브를 통해 바이크의 실제 동작을 보고 최대한 비슷하게 구현
애니메이션은 개발자의 실력 부족으로 구현하지 못함.

#### 가속도 구현  
플레이어 속도가 빨라질 수록 최대속도까지 도달하는 시간이 줄어듬

#### 코너링 구현
속도와 바이크가 회전하는 시간에 따라 바이크가 기울어짐
회전하는 시간에 따라서 회전 폭이 점차 커지도록 구현함
(만약 플레이어가 회전을 제어하지 못하고 바이크가 넘어질 경우 체크포인트에서 다시시작)

## 설정

Unity Prayerㅖrefs를 활용하여 기존 설정 데이터를 저장  
사운드 볼륨 조절, 화면 효과 조절이 가능.  
한번 조절해두면 계속 기록됨.

#### 시연 영상 : 
  =>https://www.youtube.com/watch?v=VN5hqyQYV9I&ab_channel=%EC%A0%95%EC%96%B4%EB%A6%AC%ED%8C%8C%EC%9D%B4
  
# 화면 구상  

## 화면 구조도  
<img width="777" alt="image" src="https://user-images.githubusercontent.com/68889645/179690121-84705d85-8054-4f91-a515-a5e986d6068f.png">  

## 메뉴 화면  
![image](https://user-images.githubusercontent.com/68889645/160330042-c0ca5562-3032-40e0-b25d-9f41c6bfe394.png)

## 인게임 화면  
![이미지](https://cdn.discordapp.com/attachments/892285347352936470/930491981010960504/unknown.png)  

## 결과화면  
![이미지](https://cdn.discordapp.com/attachments/892285347352936470/930512943060222033/unknown.png) 

 


