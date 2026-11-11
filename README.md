## 개요

개인 프로젝트로 제작한 2D 턴제 게임 'Project : Remnant' 입니다.<br />

Project : Remnant는 게임 'Limbus Company'에서 모티브를 얻어 제작한 프로젝트로<br />
과거 팀 프로젝트로 진행한 3D 턴제 게임 'Project : Yuria'의 턴제 전투 시스템 개선 및 업그레이드,<br />
'Limbus Company'의 전투 시스템 구현, ECA 패턴 기반 스킬 시스템, 자동 세이브 구현을 목표로 제작하였습니다.
<br />

## 요약
- 게임 장르 : 2D 턴제 게임
- 제작 기간 : 25.12.01 ~ ??.??.??
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

#### **▣ B-1.특수한 스킬 구조를 고려한 데이터 구조 설계**

- **문제점**<br />
  기존 프로젝트에서 제작한 스킬과 다른 형식으로 동작하는 스킬 데이터 구현<br />

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
  
  스킬을 코인 단위로 나눈 CoinInfoSO, 코인 내에 들어갈 조건과 효과를 담당하는 EffectNode,<br />
  효과 발동을 위한 조건(>, =, <, None)을 설정하는 ConditionNode,<br />
  수치를 결정하는 EffectValue와 효과를 지정하는 ActionNode로 구성하였습니다.<br />
  
  ``` csharp
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
  ``` csharp
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
  ``` csharp
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
  스킬 데이터를 종류별로 분리한 결과 복수의 조건과 효과를 요구하는 스킬 구현이 가능해졌으며<br />
  스킬 내 규칙과 동작을 개별 노드로 분리되어 있어 설명 스크립트 작성 자동화,<br />
  동기화(돌파)에 따른 스킬 강화 기능을 구현할 수 있었습니다.<br />

---

#### **▣ B-2.데미지 계산**

- **문제점**<br />
  버프 & 디버프 및 스킬 효과를 포함한 데미지 계산 로직 구현<br />
  스킬 효과(스킬 데미지 n% 증가, 스킬 치명타 피해량 n%증가 등) 적용 방법<br />
  
- **고민**<br />
  기존 개발에서는 데미지 계산을 공격자에서 스킬 데미지를 계산한 뒤,<br />
  피격자에게 넘겨 다시 계산하는 방식으로 구현하였습니다.<br />

  하지만 이번 프로젝트는 데미지 계산에 스킬 계수, 공격력, 버프 & 디버프와 더불어<br />
  스킬 고유 효과가 있어 계산 로직을 나눠 구현하기 부적절하였습니다.<br />

  또한 계산 위치도 스킬의 고유 효과가 존재해 공격자가 계산할지,<br />
  SkillBase를 상속받은 동작 스크립트에서 계산할지, 피격자가 계산할지 선택이 필요하였습니다.<br />
  
- **해결책**<br />
  계산 로직이 나눠져 있으면 지나치게 복잡해질 것이라 판단하여<br />
  데미지 계산을 전담할 전용 클래스 DamageCalculator를 구현하였습니다.<br />
  
  또한 데미지 계산 단위를 스킬 전체 데미지에서 해당 스킬의 코인 단위로 변경하여,<br />
  특정 코인의 데미지 증가, 치명타 피해량 증가와 같은 기능에도 대응할 수 있도록 설계하였습니다.<br />

  <img width="932" height="562" alt="Project_Remnant_공격 데미지적용_플로우차트 drawio" src="https://github.com/user-attachments/assets/9c44bf48-03c2-4bee-b1f4-2219bb6c4760" /><br />
  
  ``` csharp
  [System.Serializable]
  public struct CalInfo
  {
      [Header("---공격자 데이터---")]
      public CharacterBase attacker;
      public SkillSO skill;
      public float coinValue; // 해당 코인이 주는 데미지 비율(전체 데미지 중 해당 코인이 n%의 데미지를 준다)
      public bool isFront; // 코인 앞 뒷면 여부 (앞면 : true, 뒷면 : false)
      public bool isCritical;
      public bool isBreak; // 코인 파괴 여부 (파괴 시 데미지 0.25 / 일반 1.0)
      private List<ActionNode> actions; // 데미지 계산 시 적용할 스킬 효과 List

      [Header("---피격자 데이터---")]
      public CharacterBase defender;
  }
  ```
  DamageCalculator는 CalculateDamage(CalInfo info)로 계산에 필요한 정보를 받아 데미지를 계산합니다.<br />
  스킬 효과의 경우 ApplySkillEffects(CalInfo info)로 SkillEffect로 변환 후 적용하는 방법으로<br />
  데미지, 치명타 피해량 증가 등 스킬 효과를 포함해서 데미지를 계산할 수 있도록 구현하였습니다.<br />

  ``` csharp
  [System.Serializable]
  public struct SkillEffect
  {
      // 추가할 옵션이 있다면 이곳에 추가 후 데미지 계산 로직에 적용
      public float increaseDamage; // 데미지 증가
      public float critMultiplier; // 치명타 배율
  }
  ```
  ``` csharp
  public static int CalculateDamage(CalInfo info)
  {
      // 데미지 계산 로직은 다음과 같음
      // 코인 앞면 : 1 뒷면 : 0.5
      // (공격력 * 스킬 계수 * 치명타 배율 * 코인 배율 * 코인 파괴 여부) * 최종 데미지 증감 * 레벨 차이 증감
      // 레벨 차이 증감 = (공격력 - 방어력) / 3 = 두 수치의 차이 10당 데미지 10% 증감 (최대 ±30%)

      // 공격자 데이터 세팅
      StatData attacker = info.attacker.GetStat();
      float skillValue = info.skill.syncDatas[info.attacker.Sync].motionValue;
    
      // 피격자 데이터 세팅
      StatData defender = info.defender.GetStat();

      // 스킬 효과 세팅
      SkillEffect skillEffects = ApplySkillEffects(info.actions);


      // --- 이하 데미지 계산 로직 ---

      // 기본 데미지
      float damage = attacker.aPoint * skillValue * info.coinValue;

      // 코인 파괴 여부 적용
      damage *= (info.isBreak ? 0.25f : 1.0f);

      // 치명타 적용
      damage *= (info.isCritical ? attacker.critMultiplier + skillEffects.critMultiplier : 1.0f);

      // 데미지 증감 적용
      damage *= (1.0f + ((attacker.increaseDamage + skillEffects.increaseDamage) / 100f) - (defender.reductionDamage / 100f));

      // 코인 앞 & 뒷면 적용
      damage *= (info.isFront ? 1.0f : 0.5f);

      // 레벨 차이 적용
      float step = Mathf.Clamp(1.0f + (attacker.aPoint - defender.dPoint) / 100f, 0.7f, 1.3f);
      damage *= step;

      // 반환 -> 데미지 음수 방지 및 최소 데미지 보정
      return Mathf.Max(1, (int)damage);
    }
  ```

  이때 데미지 공식에서 step(공격 & 방어 레벨 차이에 따른 데미지 증감)을 넣은 이유는<br />
  게임 플레이 과정에서 레벨 차이로 인한 전투 난이도 차이를 구현하기 위해서입니다.<br />

  과거 프로젝트에서는 초~중반 난이도는 어렵지만,<br />
  일정 이상 성장할 경우 전투 난이도가 너무 쉬워지는 문제가 종종 발생하였습니다.<br />
  
  이번 프로젝트에서는 난이도 문제를 해소하기 위해 스테이지 클리어를 위한 평균 턴 수를 결정하였습니다.<br />
  직접 플레이하며 스테이지 클리어를 위한 턴은 일반 전투 8 ~ 12턴, 보스 전투 12 ~ 19턴이 적당하다 판단하였고,<br />
  인격 레벨업 & 동기화 상승으로 유저 스펙이 올라갔을 때 유저가 성장을 체감할 수 있으면서<br />
  전투 기믹이 무시당하지 않을 수 있는 수준을 20 ~ 30% 수준으로 판단하였습니다.<br />

  공격 & 방어 포인트는 인격의 레벨에 따라 상승하므로,<br />
  공격자 & 피격자의 차이를 체크하는 방식으로 난이도 조절을 진행하기로 결정하였습니다.<br />
  
  인격 레벨 대신 공격 & 방어 포인트을 선택한 이유는 레벨 차이가 절대적인 방식보다는<br />
  버프 & 디버프를 통해 차이가 나도 극복 가능한 전략적인 요소가 있는 것이 좋다고 판단하였기 때문입니다.<br />
  
---

#### **▣ B-3.스킬 설명 빌더**

- **문제점**<br />
  캐릭터 동기화(돌파)에 따른 스킬 강화 기능으로 인한 스킬 설명 작정량 증가<br />
  
- **고민**<br />
  과거 개인 프로젝트로 진행한 'Project : Combo'에서는<br />
  레벨업에 따른 스킬 설명을 List<string>에 직접 작성해서 SO에 저장하였습니다.<br />

  이같은 방식은 제작은 간단하나 모든 데이터를 직접 작성해야하며,<br />
  스킬의 레벨이 늘어날수록 작성 과정에서 오타나 입력 누락이 발생할 확률이 높아집니다.<br />

  특히 이번 프로젝트의 스킬 시스템은 하나의 스킬이 다수의 코인을 가지고 있으며,<br />
  다수의 코인 내에는 다수의 발동 효과가 존재하기 때문에 직접 작성할 경우,<br />
  데이터 관리 및 작업 능률이 심각하게 떨어질 것이라 판단하였습니다.<br />
  
- **해결책**<br />
  스킬 설명 작성 로직을 구현하기 위해 먼저 스킬 설명 구조의 규칙을 잡았습니다.<br />
  모든 스킬 설명은 [트리거] + [조건] + [동작] 형태로 구현됩니다.<br />
  
  이는 위에서 구현한 ECA패턴의 node와 info를 읽어 작성이 가능하였습니다.<br />
  단, 고유 동작(오리지널)의 경우 조건을 읽을 수 없으므로<br />
  별개의 string 값에 설명을 넣고 해당 값을 [동작] 위치에 넣도록 구현하였습니다.<br />

  이 방법으로 별도의 설명 작성이 필요 없이 SkillSO와 캐릭터의 동기화 수치만 알면<br />
  바로 스킬 설명을 작성하는 SkillDescriptionBuilder를 구현할 수 있었습니다.<br />
  
  ``` csharp
      public static string MakeDescription(SkillSO skillSO, int sync)
      {
          // 사용 시 발동 효과 텍스트화
          StringBuilder sd = new StringBuilder();
          for (int i = 0; i < skillSO.syncDatas[sync].skillEffects.Count; i++)
          {
              // 조건 텍스트화
              string triggerText = GetTriggerText(skillSO.syncDatas[sync].skillEffects[i].Trigger);
              string conditionText = GetConditionText(skillSO.syncDatas[sync].skillEffects[i]);
              string actionText = GetActionText(skillSO.syncDatas[sync].skillEffects[i].Action);

              // 데이터 추가
              sd.Append($"[{triggerText}] {conditionText} {actionText}\n");
          }

          // 코인 효과 텍스트화
          List<CoinInfoSO> coinInfo = skillSO.syncDatas[sync].coins;
          for (int i = 0; i < coinInfo.Count; i++)
          {
              // 코인 표시 텍스트
              sd.Append($"{i + 1} 코인\n");

              // 조건 & 동작 데이터
              for (int j = 0; j < coinInfo[i].EffectNodes.Count; j++)
              {
                  // 트리거 - ([사용 시], [피격 시], [합 승리 시] 등등)
                  string triggerText = GetTriggerText(coinInfo[i].EffectNodes[j].Trigger);

                  // 조건 - (n1이 n2 이상이면, n1 + n2 의 합이 n3 이상이면 등등)
                  string conditionText = GetConditionText(coinInfo[i].EffectNodes[j]);

                  // 효과 - (누구에게 + 체력 50 회복, 실드 25% 획득, 주는 데미지 50% 증가 등등)
                  string actionText = GetActionText(coinInfo[i].EffectNodes[j].Action);
  
                  // 텍스트 조립 ([트리거] [조건] [동작])
                  sd.Append($"[{triggerText}] {conditionText} {actionText}\n");
              }
          }

          return sd.ToString();
      }
  ```
  
---

## **4. 버프 & 디버프 시스템**

### **A. 구현 정리**

#### ▣ 공용 & 키워드 이펙트
- 공용 버프 6종(위력 증가, 데미지 증가, 공격 & 방어 포인트 증가, 코인 위력 증가, 최종 위력 증가)
- 키워드 버프 & 디버프 7종(화상, 출혈, 진동, 파열, 침잠, 호흡, 충전)
- 파생 키워드 n종(...)
- 캐릭터 고유 버프 & 디버프

버프 & 디버프 시스템 구현에서 중요하게 고려한 부분은<br />
게임 'Limbus Company'의 공용 이펙트 & 키워드 구현 및 키워드를 기반으로 제작되는<br />
파생 키워드(기존 키워드의 효과에 +a로 동작하는 버프 & 디버프) 구현입니다.<br />

---

### **B. 사례**

#### **▣ B-1.공용 & 키워드 이펙트 구현**

- **문제점**<br />
  2종류(공용, 키워드)로 나뉘는 버프 & 디버프 구현<br />
  동작 규칙이 서로 다른 버프 & 디버프 관리<br />
  
- **고민**<br />
  'Project : Remnant'의 모티브인 'Limbus Company'에는 버프 & 디버프가 2종류로 나뉩니다.<br />
  하나는 데미지 증감, 공격 포인트 증감과 같이 일반적인 버프 & 디버프(이하 공용),<br />
  다른 하나는 조건 만족 시 동작하거나, 다른 기능의 동작 조건이 되는 버프 & 디버프(이하 키워드)입니다.<br />
  
  공용 버프는 위력(버프 중첩 횟수)과 지속 턴이, 키워드는 위력과 횟수(사용 가능 횟수)로 관리됩니다.<br />
  버프 & 디버프라는 카테고리는 동일하나, 차감 조건이나 동작 효과가 모두 다르기 때문에,<br />
  어떤 방식으로 구현하고 관리할지 결정하는 과정에서 고민이 발생하였습니다.<br />
  
- **해결책**<br />
  EffectBaseSO abstract Class 단일 상속 구조로는 규칙이 다른 공용, 키워드 구현이 어렵다고 판단했습니다.<br />
  공용 버프 & 디버프는 EffectBaseSO를 상속받아 구현하되, 키워드는 EffectBaseSO 아래 계층을 추가하여<br />
  KeywordSO를 상속받아 Keyword_이름 형태로 구현하였습니다.<br />

  기본적인 버프 & 디버프의 데이터는 EffectBaseSO에서 관리하며,<br />
  KeywordSO는 자신이 파생 키워드인지, 아닌지에 대한 데이터를 가지도록 구현하였습니다.<br />
  
  ``` csharp
  public abstract class EffectBaseSO : ScriptableObject
  {
      [Header("---Setting---")]
      [SerializeField] private TriggerType triggerType; // -> flag 사용
      [SerializeField] private EffectCategory effectCategory;
      [SerializeField] private KeywordType keywordType;
      [SerializeField] private int maxPower;
      [SerializeField] private int maxCount;

      public enum EffectCategory { Public, Keyword, Original }
      public EffectCategory Category => effectCategory;
      public KeywordType Keyword => keywordType;

      [Header("---UI---")]
      [SerializeField] private Sprite icon;
      [SerializeField] private string effectName;
      [SerializeField, TextArea] private string effectDescription;

      public TriggerType Trigger => triggerType;
      public int MaxPower => maxPower;
      public int MaxCount => maxCount;
      public Sprite Icon => icon;
      public string EffectName => effectName;
      public string EffectDescription => effectDescription;
  
      // 세부 기능 구현부
      public abstract void Use(CharacterBase target);
  }
  ```  
  ``` csharp
  public class KeywordSO : EffectBaseSO
  {
      [Header("---Keyword Base Setting---")]
      [SerializeField] protected bool isderived;
      public bool IsDerived => isderived;
  
      public override void Use(CharacterBase target)
      {
          // 세부 구현은 override 하여 세부 클래스에서 구현함!
      }
  }
  ```

---

#### **▣ B-2.파생 키워드 구현**

- **문제점**<br />
  기존 키워드의 보유 값을 덮어쓰며 동작하는 파생 키워드 구현<br />
  키워드 & 파생 키워드 간 관계 및 구현 방법 결정<br />
  
- **고민**<br />
  파생 키워드는 해당 키워드의 기본 기능에 추가적인 효과가 더해진 버프 & 디버프입니다.<br />
  파생 키워드 추가 시, 파생 키워드의 원본 키워드나 같은 원본 키워드에서 파생된 파생 키워드가 있을 경우<br />
  해당 키워드의 위력과 횟수를 덮어쓰는 방식으로 동작합니다.<br />

  파생 키워드와 원본 키워드의 구현 및 관계 정리와 파생 키워드 추가 시<br />
  자신과 동일한 원본 키워드를 찾아내는 로직 구현 방법을 결정해야 했습니다.<br />

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
