## 개요

개인 프로젝트로 제작한 2D 턴제 게임 'Project : Remnant' 입니다.<br />

Project : Remnant는 게임 'Limbus Company'에서 모티브를 얻어 제작한 프로젝트로,<br />
과거 팀 프로젝트로 진행한 3D 턴제 게임 'Project : Yuria'의 턴제 전투 시스템 개선 및 업그레이드,<br />
'Limbus Company'의 전투 시스템 구현, ECA 패턴 기반 스킬 시스템, 자동 세이브 구현을 목표로 제작하였습니다.
<br />

## 요약
- 게임 장르 : 2D 턴제 게임
- 제작 기간 : 25.12.01 ~ 26.00.00
- 개발 목표 : 게임 'Limbus Company'의 턴제 전투 시스템 구현, ECA 패턴 기반 스킬 시스템, 자동 세이브
- 기술 문서 : "
- 동작 영상 : "
<br />

## 개발 환경
- Unity 2022.3.16f1
- Visual Studio 2022
- ComfyUI
- Window 11

<br />

## 주요 구현
## **1. 인격(캐릭터) & 몬스터**

### **A. 구현 정리**

#### ▣ 플레이어 인격
- 플레이어 인격 2종 구현
- 스킬 14종 구현 (ch01 = 9개, cch02 = 5개)

#### ▣ 몬스터
- 몬스터 4종 구현

인격(캐릭터) 구현 과정에서 중요하게 고려한 부분은<br />
정적 데이터와 런타임 데이터, 인격 동작 로직의 명확한 분리입니다.<br />

---

### **B. 사례**

#### **▣ B-1.**

- **문제점**<br />

- **고민**<br />

- **해결책**<br />

---

## **2. 인격(캐릭터) 편성**

### **A. 구현 정리**

#### ▣
-

---

### **B. 사례**

#### **▣ B-1.**

- **문제점**<br />

- **고민**<br />

- **해결책**<br />

---

## **3. 스킬 시스템**

### **A. 구현 정리**

#### ▣ 스킬 시스템
- ECA 패턴 기반 스킬 데이터 구현
- ECA 패턴 스킬 데이터를 읽어 자동으로 설명 스크립트 작성
- 동기화(돌파)에 따른 스킬 강화

<br />

스킬 시스템 구현에서 중요하게 고려한 부분은 데이터 & 동작 분리,<br />
동기화(돌파)에 따른 스킬 데이터 변경 대응, 쉽게 작성 가능한 스킬 데이터 구현입니다.<br />
<br />

---

### **B. 사례**

#### **▣ B-1.특수한 스킬 설계를 고려한 데이터 구조 설계**

- **문제점**<br />
  기존 프로젝트에서 제작한 스킬과 다른 형식으로 동작하는 스킬 데이터 구현

- **고민**<br />
  Project : Remnant의 스킬은 게임 'Limbus Company'의 전투 로직을 구현하는 것이 목표입니다.<br />
  이때 모티브가 되는 게임의 스킬 구조는 하나의 스킬에 여러 개의 코인(공걱 횟수)가 존재하며,<br />
  각 코인마다 부가 효과가 붙어 있는 방식으로 구현되어 있습니다.<br />
  
  부가 효과는 앞에 사용시, 적중시, 앞면 적중시 등 특정 조건을 만족했을 때 효과가 발동하며,<br />
  버프 & 디버프 부여, 해당 스킬의 데미지 증가, 치명타 피해량 증가, 코인 재사용 등,<br />
  스킬은 다수의 코인 + 조건 + 효과로 구성되어 있어 기존에 사용하던 제작 방식을 적용할 수 없었습니다.<br />
  
- **해결책**<br />
  다수의 조건과 효과로 구성된 스킬을 제작하기 위해<br />
  ECA 패턴 기반으로 스킬의 조건과 값, 효과를 분리하기로 결정하였습니다.<br />
  
  <img width="1302" height="517" alt="Image" src="https://github.com/user-attachments/assets/5e4bf2d8-2650-4b81-922c-c2f213d7d7cc" /><br />
  
  스킬을 코인 단위로 나눈 CoinInfoSO,<br />
  코인 내에 들어갈 조건과 효과를 담당하는 EffectNode,<br />
  효과 발동을 위한 조건(>, =, <, None)을 설정하는 ConditionNode,<br />
  수치를 결정하는 EffectValue와 효과를 지정하는 ActionNode로 구성하였습니다.<br />
  
```
public class CoinInfoSO : ScriptableObject
{
    [Header("---Coin Setting---")]
    [SerializeField] private CoinType coinType;
    // 해당 코인이 전체 데미지 중 몇 %의 데미지를 주는지
    [SerializeField] private int damagePercent;
    // 사용시, 적중시 같은 효과 발동 조건 데이터가 담긴 so
    [SerializeField] private List<EffectNode> effectNodes;

    public CoinType Coin => coinType;
    public int DamagePercent => damagePercent;
    public List<EffectNode> EffectNodes => effectNodes;
}
```
```
[System.Serializable]
public class EffectNode
{
    [Header("---Trigger---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private TargetType targetType;

    [Header("---Condition---")]
    [SerializeField] private ConditionNode condition;

    [Header("---Action---")]
    [SerializeField] private ActionNode action;

    #region Getter
    public TriggerType Trigger => triggerType;
    public TargetType Target => targetType;
    public ConditionNode Condition => condition;
    public ActionNode Action => action;
    #endregion

    #region Enum
    public enum TargetType { Self, Target, Both, AllEnemies, AllAllies, Everyone }
    public enum CompareType{ None, LessEqual, Equal, GreaterEqual }
    #endregion

    public struct ConditionNode
    {
        [Header("---Condition---")]
        public TargetType checkTarget;
        public CompareType compareType;
        public int conditionValue;
        public List<EffectValue> values;
    }
}
```
```
public struct ActionNode
{
    [Header("---Action---")]
    public ActionType actionType;
    public EffectRange effectRange;
    public EffectValue valueNode;

    [Header("---Original Action---")]
    public int originalActionId;
    [TextArea] public string actionDescription;
}
```
  스킬 데이터를 종류별로 분리한 결과 복수의 조건과 효과를 요구하는 스킬 구현이 가능해졌으며,<br />
  스킬 내 규칙과 동작을 개별 노드로 분리되어 있어 설명 스크립트 작성 자동화,<br />
  동기화(돌파)에 따른 스킬 강화 기능을 구현할 수 있었습니다.<br />
  
---

## **4. 버프 & 디버프 시스템**

### **A. 구현 정리**

#### ▣ 공용 & 키워드 이펙트
- 

---

### **B. 사례**

#### **▣ B-1.**

- **문제점**<br />

- **고민**<br />

- **해결책**<br />

---

## **5. 전투 시스템**

### **A. 구현 정리**

#### ▣
- 

---

### **B. 사례**

#### **▣ B-1.**

- **문제점**<br />

- **고민**<br />

- **해결책**<br />

---

## **6. 세이브 & 로드**

### **A. 구현 정리**

#### ▣
- 

---

### **B. 사례**

#### **▣ B-1.**

- **문제점**<br />

- **고민**<br />

- **해결책**<br />
