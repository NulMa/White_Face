프로젝트명 - 백면(White Face)
메트로베니아 장르의 플랫포머 게임을 목적으로 만들었음.
특징으로는 적을 격파하기 위해서 패링이 강제되어 있다는 점과
패링이 2가지로 나뉘어 있다는 점이 있음.

졸업 작품 제출 용으로 약 5개월 간의 설계와 5개월 간의 구현, 총 10개월 소요

기본적으로 Unity(2021.2.7f1) 엔진을 기반으로 한 C#으로 작성, 
SE의 경우 저작권이 없는 프리 소스를 찾아가며 삽입,
BGM의 경우 음원 제작자에게 라이센스를 구매하여 사용
그래픽 소스의 경우 Aseprite를 이용해 직접 제작한 도트를 사용




  구현 기능

        기본이동
        - 좌우이동
        - 특정행동(패링, 공격)을 하면 이동을 강제로 멈춤

        키를 누르는 시간에 비례한 점프력
        - 키를 오래 누를수록 점프력이 증가(상한존재)

        패링A, B
        - 머리 위 구슬의 색상에 따라 다른 키 입력
        - 패링 할 수 없는 패턴도 존재

        UI
        - 플레이어 체력
        - 의약품 최대 수와 현재 소지량
        - 소지 동전 수
        - 보스 맵 입장 시 보스 체력 표기
        - 메뉴에서 조작법 설명
        - NPC 대화
        - 상점기능 구현

        함정구현
        - 떨어지면 체력이 닳음
        - 근처 안전지역에서 리스폰
        - 효과음재생
        배경
        - 배경 이미지가 플레이어를 따라옴
        - 근경일수록 천천히, 원경일수록 빨리 움직여서 원근감 부여

        상호작용 및 공격
        - 적을 그로기 상태(기절)에 빠지게 한 후 공격키를 이용해 체력을 깎을 수 있음
        - 또는 물리적으로 상호작용 가능한 물체(부서지는 벽, 나무함정)의 기능실행

        상호작용
        - 부서지는 벽 
          - 상호작용 시 효과음과 함께 가짜 벽 페이드아웃
          - 유용한 아이템이 숨겨져 있거나 보스공략에 도움(후술)을 줌
        - 잠긴 문
          - 잠겨있는 문의 경우 상호작용 시 막힌 소리 출력, 통행 불가
          - 보스를 격파해 키를 습득 후 상호작용 시 문이 열리고 통행 가능


        - 휴식지점
          - 모닥불의 모습을 한 오브젝트로 존재
          - 상호작용 시 불이 붙고 저장한다는 뜻의 '記' 출력
          - 활성화 시 장작이 타는 소리 재생
          - 위 소리는 멀리 떨어지면 작아지다가 들리지 않음
          - 쓰러지면, 최근 상호작용 한 휴식지점에서 부활
          - 체력이나 소모품이 최대가 아닐 때 상호작용 시 재충전기능

        - NPC
          - 상호작용 키를 통해 이야기를 들을 수 있음
          - 종종 도움을 줄 수 있는 이야기를
          ( ex : 보스전에 도움을 주는 함정의 정보)
          - 획득 한 동전을 이용해 거래를 할 수 있음
          - 거래를 통해 도움을 주는 아이템 구매 가능
          - 아이템 구매 UI의 경우 이름, 가격, 효과 등을 표기
          - 구매 시 효과음 재생 및 보유 동전이 가격만큼 차감,
            구매한 아이템은 회색처리 해서 구매버튼 비활성화
          - 가진 돈이 부족해 구매할 수 없을 경우 경고음 재생

        - 아이템획득
          - 상자에 상호 작용 시 획득한 아이템의 정보와 효과 출력
          - 최대 체력 증가나 소모품 소지 상한의 증가 등의 효과제공
          - 상자에서 획득했거나 상인에게 구매 한 아이템은
            ESC키를 눌러 메뉴창에서 확인 가능
          - 소모품(의약품)
          - 관련 오브젝트(약초, 의약품최대치증가아이템)

        일반몬스터
        - 하나의 공격 모션
        - 매 번 랜덤한 구슬 색상
        - 처치 시 소량의 동전 드롭

        보스
        - 3가지의 패턴(패링가능2, 불가능1)
        - 패턴 안의 공격모션에 각기 다른 구슬 부여
          (1개의 패턴은 3개의 공격 모션을 가지고 있음)
        - 맵 입장 시 장애물 생성, 배경음 변경// 보스를 잡거나 쓰러지기 전 까지 탈출X
        - 위에서 서술한 부서지는 벽을 통해 상호작용 가능한 함정을 발동 하면 보스의
          체력을 미리 깎아놓을 수 있음
        - 보스 격파 시 배경음 원위치, 동전을 드롭해 보상 제공, 상자에서 다음 맵을 갈
          수 있는 키 제공



Unity에 관한 기반 지식이 현저히 부족한 상황에서 시작한 프로젝트다 보니 기초적인 문제의 해결에도 상당한 시간을 소비했음.
플레이어의 콜라이더를 박스형으로 할당해서 평지 이동에도 걸리는 문제가 발생하는 것을 며칠이 지난 후에 해결하기도 함

설계를 충분히 했다고 생각했지만, 첫 개발이다 보니 결국 스파게티 코드가 되어버림
기능에 대한 완벽한 이해보다는 '이렇게 해보면 어떨까?'라는 마음가짐으로 작성한 기능이 더 많았음,
알고있는 기본 지식을 활용한다는 점에서, 사고의 유연함을 늘릴 수는 있었지만 결과적으로 깔끔한 코드와는 거리가
멀어지는 원인을 제공함.
