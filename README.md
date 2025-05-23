# Star-Ring-Demo - 科幻战舰RPG Demo 🚀⚔️

<img alt="Banner" src="Media/Start.gif" width="600"/>

*载入界面*

<img alt="Banner" src="Media/Loading.gif" width="600"/>

*游戏开始界面*

## 🔥 项目概述

《Star Ring》是基于Unity开发的科幻战舰策略RPG原型，玩家通过模块化装配系统打造独特星舰，在程序生成的宇宙中指挥舰队作战，并通过多线剧情决定星际文明的命运。

## ✨ 核心特色

- 🛸 **自由战舰设计** - 200+模块化部件自由组合，支持驱逐舰/巡洋舰/战列舰/泰坦级等12种舰型架构
- ⚡ **科技研发** - 三维科技树系统，通过逆向工程解锁曲速核心/暗物质炮等尖端装备
- 🌐 **沙盒宇宙** - 使用Prim算法随机生成的航道在超过1000+星系中自由探索
- 👥 **灵能释放** - 使用独特的灵能技能树操纵熵，时间，空间和心灵成为灵能大师
- ⚔️ **舰船战斗** - 战术指挥+护盾频率+灵能对抗系统，支持编队协同作战指令

## 🛠️ 系统架构

![Banner](Media/MainStarMap.png)
*主要星图界面*

### 1. 星图探索系统 🌟

在程序生成的1000+星系中展开探险，每个星域都包含独特的天文现象与文明遗迹：

```csharp
// 简化的星图生成算法（基于Prim最小生成树）
public void GenerateStarmap(int nodeCount) {
    List<StarNode> nodes = new();
    for(int i=0; i<nodeCount; i++){
        nodes.Add(new StarNode(Random.insideUnitSphere * 100f));
    }
    // 构建最小连接网络
    PrimAlgorithm.ConnectNodes(nodes); 
}
```

探索机制流程图：

```mermaid
graph TB
    A[跃迁至新星系] --> B{探测类型}
    B -->|主动扫描| C[消耗能量获取完整情报]
    B -->|隐蔽模式| D[避免被发现]
    C --> E[发现特殊事件]
    D --> F[可能遭遇伏击]
    E --> G[进行交互或者战斗]
    F --> G
    G --> H[占领星系]
```

### 2. 科研系统 ⚡

<img alt="Banner" src="Media/ResearchPanel.gif" width="300"/>

*多层次科技树 | 动态增益联动 | 跨领域突破*

超过100+科技等待研发，提供武器部件和舰队加成。科技树分为**物质学**、**能量学**、**信息学**
三大分支：

**物质学**解锁离子炮阵列、纳米装甲等尖端装备，提升舰队火力与防御

**能量学**增强跃迁引擎效率，实现反应堆小型化，降低舰船能耗并激活护盾过载能力

**信息学**提供帝国领土上限，并解锁高端信息战装备

支持科技组合效应


<img alt="Banner" src="Media/ResearchPanel2.png" width="300"/>

### 3. 库存系统 🏡

![Banner](Media/Inventory.png)

*动态市场模拟 | 存储物资 | 跨星系物流*

收集物资，进行贸易，建立商业帝国

### 4. 装配系统(未完成) 🚀

<img alt="Banner" src="Media/Equipment.gif" width="300"/>

*模块化设计 | 装配点组装 | 实战测试*

自由组装舰船，配置武器

### 5. 战斗系统(未完成) ⚔️

<img alt="Banner" src="Media/fight.gif" width="300"/>

*多层战场维度 | 实时战术指挥 | 环境互动*

### 6. Odin配置表 🛠️

<img alt="Banner" src="Media/ObjectConfig.png"/>
可视化数据生态，通过Odin插件构建的可视化配置体系支持：

恒星生成规则：自定义恒星类型材质生成

武器编辑器：编辑武器效果与属性

舰船编辑器：配置NPC舰队的AI逻辑

支持嵌套式数据模板：创建"磁轨炮"基础类后，可通过继承机制快速派生"速射型"、
"穿甲型"等变种。所有修改实时同步至游戏实例。

<img alt="Banner" src="Media/WeaponConfig.png"/>

## 📜 开源协议

![Banner]()

## 📬 联系我们

None