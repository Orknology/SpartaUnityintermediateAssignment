## **구현 방식**

unity 게임 개발 숙련 KDT 실무형 Unity 게임개발자 양성과정 3회차 강의 영상의 인벤토리 구현과정을 기반으로 제작

### **주요 코드**

#### **Inventory**

아이템의 장착과 해제, 인벤토리 팝업에 아이템 정보 전달, 인벤토리 UI등을 관리한다.


1. 기존 코드 방식에서 장비로만 구분 되던 것을 무기와 방어구로 구분하여 장착되도룍 변형
기존 강의 코드에서 조건문들을 추가하여 아이템 데이터의 타입이 장비인지 소모품인지 -> 장비에서 무기인지 방어구인지 구분하는 과정을 추가

인덱스가 int curEquipIndex로만 구분 되던 것을

    private int curEquipWeaponIndex = -1;
    private int curEquipArmorIndex = -1;
  
두 가지로 변경.


이렇게 바꾸는 중 트러블 슈팅으로
인덱스 넘버를 비교해서 장비를 장착 및 해제하는 코드가 무기와 방어구 2가지로 바뀌면서

    private int curEquipWeaponIndex;
    private int curEquipArmorIndex;
  
로만 처음 작성하였던 것이 첫 장착 이후 순서 상 0번에 존재하게 되는 아이템 인덱스와 겹치게 되면서
장착 및 해제에 문제가 생겨 이 문제를 찾는 것이 오래 걸렸었다.
이 부분을 위와 같이 -1을 추가 해주고 조건문에도

    if (curEquipWeaponIndex >= 0 && selectedItem.item.type == ItemType.Weapon && uiSlots[curEquipWeaponIndex].equipped)
    {
        UnEquip(curEquipWeaponIndex);
    }
    else if (curEquipArmorIndex >= 0 && selectedItem.item.type == ItemType.Armor && uiSlots[curEquipArmorIndex].equipped)
    {
        UnEquip(curEquipArmorIndex);
    }
    
이런 방식으로 현재 인덱스가 0보다 크거나 같은지 확인하는 조건을 추가하여 해결하였다.
이 부분에서 시간 소모가 컸기에 상점 구현은 넘어가게 되었다.

---
#### **EquipManager**

아이템 장착과 아이템의 스탯이 플레이어 스탯에 전달되는 메소드들을 담당
강의 코드에서 스탯 관련 메소드들이 추가 되었다.

      public void EquipStatAdd(ItemData item)
    {
        //무기
        if (curEquipWeapon == null && item.valueType == ValueType.Attack)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatAdd("attack", item.stats[i].statsValue);
            }
        }

        //방어구
        if (curEquipArmor == null && item.valueType == ValueType.Defense)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatAdd("defense", item.stats[i].statsValue);
            }
        }
    }

    public void EquipStatSubtract(ItemData item)
    {
        //무기
        if (item.valueType == ValueType.Attack)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatSubtract("attack", item.stats[i].statsValue);
            }
        }

        //방어구
        if (item.valueType == ValueType.Defense)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatSubtract("defense", item.stats[i].statsValue);
            }
        }
    }
이런 식으로 아이템 스탯 값을 받고 있다.
이와 연계되는 코드로 PlayerStats가 존재.

---
#### **PlayerStats**

플레이어의 스탯과 관련된 내용을 클래스로 가지고 있다.
구현하며 테스트 체크를 위해 [HideInInspector]가 꺼져있는 상태

curValue와 maxValue, startValue로 직접적으로 값을 가지고 있거나
Add와 Subtract메소드로 해당 값들의 변화를 담당한다.

아이템의 스탯을 받는 메소드로 

    public void StatAdd(string statName ,float amount)
    {
        if(statName ==  "health")
        {
            health.Add(amount);
        }
        else if (statName == "attack")
        {
            attack.Add(amount);
        }
        else if (statName == "defense")
        {
            defense.Add(amount);
        }
    }

    public void StatSubtract(string statName ,float amount)
    {
        if (statName == "health")
        {
            health.Subtract(amount);
        }
        else if (statName == "attack")
        {
            attack.Subtract(amount);
        }
        else if (statName == "defense")
        {
            defense.Subtract(amount);
        }
    }
를 추가하였다.

---

#### **StatUI**

스탯이 스테이터스 UI에 적용 되도록 정보를 전달하는 역할
해당 텍스트들을 배정해주면

    public void Update()
    {
        healthText.text = playerStats.health.curValue.ToString();
        attackText.text = playerStats.attack.curValue.ToString();
        defeseText.text = playerStats.defense.curValue.ToString();
    }
와 같은 방법으로 값을 갱신

---
### **기타 코드**

#### **MoveableUI**

UnityEngine.EventSystems에 대해 알게되며 공부하는 과정 중에 IDragHandler를 이용해 UI를 움직이는 기능을 구현해본 코드

    [SerializeField] private RectTransform targetUI;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        targetUI.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
클릭하여 드래그할 헤더를 canvas로 받고 움직일 대상인 UI를 받아서 작동한다.
그러나 이렇게 UI창을 움직이고 나면 이상한 위치에 놓게 될 경우, 나중에 그 UI를 SetActive하게 되면 그 자리에서 다시 나오는데
이로 인한 문제를 해결하기 위해 창을 오가는 버튼 코드를 사용하고 있다.

#### **UIButton**

창을 킬 때 해당 창의 포지션 벡터값을 받아 저장한 뒤
창을 끌 때 해당 벡터값을 돌려주어 위치를 초기화 시키는 코드

#### **MoneyUI**

골드 창에 통화 자릿수 단위를 표현해주는 코드

    private void Awake()
    {
        moneyCurValue = moneyStartValue;
    }
    private void Update()
    {
        moneyText.text = GetThousandCommaText(moneyCurValue).ToString();
    }
    public string GetThousandCommaText(int data) 
    { 
        return string.Format("{0:#,###}", data); 
    }

---

## **구현 된 기능**

과제 2 기준

1. 메인 화면 구성
   -아이디
   -레벨
   -골드
   -Status 버튼 -> Status 보기
   -Inventory 버튼 -> Inventory 보기

2. Status 보기
   -버튼 사라지기
   -캐릭터 정보 표현
   -뒤로가기 버튼
   +캐릭터 장착 장비 스탯 반영

3. Inventory 보기
    - 버튼 사라지기
    - 인벤토리 표시
    - 아이템 클릭하면 장착
    - 장착중인 아이템 표시
    - 뒤로가기 버튼

4. 선택요구사항
     4-1. 아이템 장착 팝업 업그레이드
           - PopupUI를 만들어 Iventory에서 선택한 장비의 정보를 받음



