# Frenzy Reaper

![Unity](https://img.shields.io/badge/Unity-2D%20LTS-black?logo=unity)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)
![Genre](https://img.shields.io/badge/Genre-Roguelite%20%2F%20Survivors--like-orange)
![Status](https://img.shields.io/badge/Status-Phase%202%20Roguelite%20Loop-orange)
![License](https://img.shields.io/badge/Code%20License-GPL--3.0-success)

[English](./README.md)

## 项目简介（About the Project）

《Frenzy Reaper》是一款 2D 像素风废土题材肉鸽割草游戏。  
你将在辐射污染区面对海量异变生物，回收机械废料，并通过义体改造与基因突变逐步强化角色。  
每一局都围绕“生存、成长、撤离”展开：活下来，带回资源，建设下一次远征的基础。

## 核心特色（Key Features）

- **自动废土武器系统**：自动攻击，玩家专注走位与生存决策。
- **同屏海量异变体**：高密度怪群围剿，强调割草节奏与压迫感。
- **义体 / 基因突变成长**：局内随机升级，支持多流派构筑。
- **废料驱动循环**：回收废料用于局内强化与局外成长。
- **避难所建设（规划中）**：将战斗收益沉淀为长期养成能力。

## 开发路线图（Roadmap）

- [x] 第 0 阶段：工程初始化与白盒环境搭建
- [x] 第 1 阶段：核心战斗原型（移动、自动开火、敌人追踪、基础血量）
- [ ] **第 2 阶段：肉鸽循环（废料拾取、升级 UI、动态刷怪波次）** ← *进行中*
- [ ] 第 3 阶段：架构重构（对象池 Object Pool、数据驱动 SO 配置）
- [ ] 第 4 阶段：量产与视觉反馈（丰富的废土武器、受击闪白、屏幕震动）
- [ ] 第 5 阶段：局外养成（避难所建设、本地 JSON 存档读取）

## 技术栈与亮点（Tech Stack & Architecture）

- **引擎**：Unity 2D（LTS 线），C#
- **代码质量**：模块化设计与整洁代码实践
- **性能优化**：采用 **Object Pooling** 支撑海量实体生成与回收
- **数据驱动**：使用 **ScriptableObject** 分离数据与逻辑
- **像素呈现**：接入 **2D Pixel Perfect** 保持像素画面清晰稳定

> 当前仓库处于「**第 2 阶段：肉鸽循环**」，正在积极开发中。

## 现代架构模式（Core Architecture & Modern Patterns）

本项目面向 **Unity 6**，强制执行现代、零 GC 分配、低耦合的架构约定。
Unity 5 时代的过时模式**一律拒绝**——代码 Review 将直接驳回。

- **视图与逻辑分离（UI Toolkit + C# MVP）**
  UI 使用 **UI Toolkit（UXML/USS）** 构建。数据模型（`Core` 层）仅暴露纯 C# 数值与事件；
  由独立的 **Presenter/ViewModel**（`[CreateProperty]`）通过 Runtime Data Binding 将数据映射为视图状态。
  `Core` 类型**严禁**引用 `UnityEngine.UIElements`。替代传统 uGUI 手动状态同步。
- **零耦合通信（ScriptableObject 事件通道）**
  跨模块信号（受击、死亡、生成、拾取）通过 **SO Event Channel** 广播。
  预制体之间**拒绝**任何 Inspector 直连的网状对象引用。
- **统一服务管理（静态 Service Locator）**
  运行时服务（玩家 Transform、子弹池、刷怪导演）通过极简静态 **Service Locator**
  （`ServiceLocator.Get<T>()`）解析，实现依赖倒置。**严禁使用单例（`GameManager.Instance`）。**
- **代码直驱动画（`Animator.Play(hash)`）**
  动画状态切换全部由代码驱动：使用缓存的 `StringToHash`，通过 `Animator.Play(stateHash)` /
  `CrossFade(hash)` 切换。**Blend Tree 仅保留用于多方向（八方向）移动混合。**
  严禁 Animator 连线迁移、严禁参数条件驱动、严禁用 `SetTrigger`/`SetBool` 触发一次性动画。

## 性能与安全红线（DO NOT CROSS）

面对同屏海量怪群，以下为硬性红线：

- ❌ **热路径严禁每帧/每次受击的堆分配。** 禁止 `new WaitForSeconds`、禁止 LINQ、禁止装箱
  → 缓存 wait 或用 `Time.time` 计时；复用 `MaterialPropertyBlock`。
- ❌ **严禁在 `Update`/热循环中 `GetComponent`。** → 在 `Awake` 中缓存全部组件引用。
- ❌ **海量实体（子弹、敌人、特效）严禁 `Instantiate`/`Destroy`。** →
  使用官方 **`UnityEngine.Pool`**（`ObjectPool<T>`），严禁手写自定义对象池类。
- ❌ **严禁产生分配的物理查询。** → 使用带预分配缓冲的
  `Physics2D.OverlapCircle(ContactFilter2D, Collider2D[])`；比较 `sqrMagnitude`，禁用 `Vector2.Distance`。
- ❌ **运行时严禁字符串查参的 Animator 调用。** → 预计算 `Animator.StringToHash`。
- ❌ **运行时严禁全局查找**（`FindObjectOfType`、`GameObject.Find`）。 → 通过 Service Locator 解析。

## 代码命名约定（Naming Conventions & Technical First）

**技术命名绝对优先于美术/世界观术语。** 当可读性与世界观冲突时，以代码可读性与技术栈迁移直觉为准。

- **代码标识符使用通用技术术语**：`Exp`、`Enemy`、`LevelUp`、`Health`、`Bullet`、`Pickup`
  ——确保任何工程师（或 AI Agent）无需查阅设计文档即可检索与推理。严禁把世界观编码进类型/字段/方法名。
- **世界观/美术术语**（如「异变体 Mutant」「废料 Scrap」「义体 Cybernetic」）**仅**存在于
  Inspector 标签（`[Header]`/`[Tooltip]`）、UXML 展示文本与代码注释中，作为表现层解释——绝不进入 API 表面。
- **一个类只有一个名字。** 跨命名空间严禁重名类型
  （例如应为 `PlayerAnimator` 与 `EnemyAnimator`，而非两个 `AnimationHandler`）。
- **命名空间对应分层**：`Core.*` = 引擎/逻辑（无视图依赖），
  `UI.*` = 视图/Presenter，`Data.*` = ScriptableObject 定义。

## 更新日志 (Changelog)

### 第 1 阶段 — 核心战斗原型 ✅
- 玩家 8 方向移动，基于 Unity Input System 事件驱动。
- 自动瞄准：`Attack.cs` 每帧通过 `Physics2D.OverlapCircle` 锁定最近存活敌人。
- 子弹对象池（`BulletPool.cs` + `Bullet.cs`），触发器碰撞判定命中。
- 敌人寻路追踪 AI，基于 **A\* Pathfinding Project**（AIPath + RVO），含 `decisionInterval` 节流优化。
- 组件化血量系统（`Health.cs`），暴露 `OnDamaged` / `OnDeath` C# 事件；UI 通过 Unity UI Toolkit Runtime Data Binding 实时刷新。
- 死亡界面与场景重载，由 `DeathController.cs` 管理。

## 如何运行测试（How to Play / Build）

1. 克隆仓库：
   ```bash
   git clone https://github.com/q1w264/Frenzy-Reaper.git
   ```
2. 使用 Unity Hub 打开项目（建议使用项目对应的 Unity LTS 版本）。
3. 打开场景 `Assets/Scenes/Gaming.unity`。
4. 点击 Play 进入测试。

> 注意：仓库中不包含 `Assets/Art Resources` 及相关 Art 文件（为合规已移除）。  
> 你需要自行提供合法授权的替代美术资源后再进行完整体验测试。

> 注意：本项目还使用了付费闭源模块（`A* Pathfinding Project`），公开仓库不包含该模块。  
> 如需使用其功能，请在你自己的有效授权下本地安装。

## 许可证协议（License）

本仓库采用分离许可模式：

- **代码（Scripts / Config / Project Settings）**：`GPL-3.0`（见根目录 `LICENSE`）
- **美术与音效资产（Sprites / Audio）**：版权归其权利人所有，未经授权不得商用或再分发

第三方声明与 Unity 相关组件信息请参考：`ThirdParty/THIRD_PARTY_LICENSES.md`
