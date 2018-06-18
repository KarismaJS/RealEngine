# SnowBrothers

사용법
 게임 실행시 Angry.exe로 실행.
 게임 해상도는 1024 X 768로 설정.
 
 
스크립트 정보
 Boad.cs : 게임 내 나무판자 관련 스크립트
 Box.cs : 게임 내 나무상자 관련 스크립트
 ChangeScene.cs : 스테이지 선택창 관련 스크립트
 Enemy.cs : 적 오브젝트 관련 스크립트
 Pause.cs : 게임 내 일시정지 관련 스크립트
 Shooting.cs : 오브젝트 발사 관련 스크립트
 
 
 개발자 정보
   - 박지상 : qkrwltkd77@gmail.com
   - 김성준 : tkdwnsthwhs@naver.com
   - 강준희 : khs0348@naver.com
   - 김종완 : kimjw9912@gmail.com


새로운 스테이지 만들 때 주의사항
  
  1. 프리펩을 이용하여 새로운 맵을 만든 뒤 Bullets프리펩 내의 chiken들의 인스펙터에서 shooting(script)를 찾는다.
  2. shooting(script)에 Siling Line Right필드에 SilingShot프리펩 내의 RightSiling을 넣어준다
  3. 마찬가지로 LeftSiling을 Siling Line Left필드에 넣어준다.
  4. 첫번째 chiken(그냥 chiken)에 Next Ball필드에 chiken(1)을 넣어준다.
  5. 마찬가지로 chiken(2)를 chiken(1) Next Ball필드에 넣어준다.
  6. 발사 횟수를 늘리고 싶다면 chiken을 복사하여 chiken(4)를 만들어 준 뒤 chiken(3)의 Next Ball필드에 chiken(4)를 넣어준다.
  7. Hook프리펩을 chiken의 Spring Joint 2D의 Connected Rigid Body필드에 넣어준다.
  8. 마찬가지로 나머지 chiken들의 connected Rigid Body필드에 Hook프리펩을 넣어준다.
  
  
Caution of create new stage

  1. After create new map to use prefabs, find shootin(script) in inspecter of chiken in Bullets prefabs.
  2. Input RightSiling in SilingShot prefabs to Siling Line Right field in shooting(script).
  3. Same as 2, input LeftSiling to Siling Line Left field.
  4. Input chiken(1) to Next Ball field in first chiken(just chiken).
  5. Same as 4, input chiken(2) to Next Ball field in chiken(1).
  6. If you want to increase fire count, copy chiken(will made chiken(4)) and input chiken(3) to Next Ball field in chiken(4)
  7. Input Hook prefab to Connected Rigid Body in Spring Joint 2D in chiken.
  8. Same as 7, input Hook prefab to Connected Rigid Body in other chikens.
  
