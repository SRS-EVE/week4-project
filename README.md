# 4주차 개인과제

# 🎮 숙련주차 과제

스페이스 바 : 점프
w, a, s, d : 이동

게임 소개 : 점프대, 플레이어 아이템, 장애물 구현 

- 속도 증가 아이템, 체력/스태미너 회복 아이템을 활용하여 게임을 진행할 수 있다.
- 1번아이템 스태미너 상승 2번아이템 체력 상승 3번아이템 스피드업 각각 아이템을 획득 후 1, 2, 3번으로 사용합니다.
- 장애물에 닿으면 체력이 깍이거나 날아간다
- 점프대에 오르면 강제로 높이 점프가 된다

# 🛠️ 개발환경

- `Unity 2022.3.17f`
- `C#`
- `GitHub`

# 📂 프로젝트 구조

```
week4-project/
├── Scripts/                     
│   ├── Item/                    # 아이템 관련 스크립트
│   │   ├── AcquisitionItem.cs
│   │   ├── ItemObject.cs
│
│   ├── Obstacle/                # 장애물 관련 스크립트
│   │   ├── CampFire.cs
│   │   ├── DamageEffect.cs
│   │   ├── JumpEffect.cs
│   │   ├── Obstacle.cs
│   │   └── ObstacleEffect.cs
│
│   ├── Player/                  # 플레이어 관련 스크립트
│   │   ├── CharacterManager.cs
│   │   ├── Interaction.cs
│   │   ├── ObstacleTrigger.cs
│   │   ├── Player.cs
│   │   ├── PlayerCondition.cs
│   │   └── PlayerController.cs
│
│   ├── ScriptableObject/       # ScriptableObject 데이터 정의
│   │   ├── ItemData/
│   │   │   ├── Heal.asset
│   │   │   ├── Speed.asset
│   │   │   └── Stamina.asset
│   │   └── ObsData/
│   │       ├── Fire.asset
│   │       ├── Jump.asset
│   │       ├── Random.asset
│   │       ├── Thorn.asset
│   │       ├── ItemData.cs
│   │       └── ObstacleData.cs
│
│   ├── UI/                      # UI 관련 스크립트
│   │   ├── Condition.cs
│   │   ├── DamageIndicator.cs
│   │   ├── ItemUseInventory.cs
│   │   └── UICondition.cs
│
│   └── BuffManager.cs          # 버프 관리 싱글턴 클래스
```
# 🎯 주요 기능

- 캐릭터 이동 및 점프 구현
- 속도/체력/아이템 획득/아이템 사용 처리
- 장애물 충돌 시 반응처리
- 게임 UI (체력, 스태미너 등) 실시간 표시

# 📌 게임 예시 화면

## 🎮 게임 플레이 영상
