# Frenzy Reaper

![Unity](https://img.shields.io/badge/Unity-2D%20LTS-black?logo=unity)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)
![Genre](https://img.shields.io/badge/Genre-Roguelite%20%2F%20Survivors--like-orange)
![Status](https://img.shields.io/badge/Status-Phase%200%20Foundation-blue)
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
- [ ] 第 1 阶段：核心战斗原型（移动、自动开火、敌人追踪、基础血量）
- [ ] 第 2 阶段：肉鸽循环（废料拾取、升级 UI、动态刷怪波次）
- [ ] 第 3 阶段：架构重构（对象池 Object Pool、数据驱动 SO 配置）
- [ ] 第 4 阶段：量产与视觉反馈（丰富的废土武器、受击闪白、屏幕震动）
- [ ] 第 5 阶段：局外养成（避难所建设、本地 JSON 存档读取）

## 技术栈与亮点（Tech Stack & Architecture）

- **引擎**：Unity 2D（LTS 线），C#
- **代码质量**：模块化设计与整洁代码实践
- **性能优化**：采用 **Object Pooling** 支撑海量实体生成与回收
- **数据驱动**：使用 **ScriptableObject** 分离数据与逻辑
- **像素呈现**：接入 **2D Pixel Perfect** 保持像素画面清晰稳定

> 当前仓库处于“第 0 阶段：搭建地基”，部分系统仍为开发占位实现。

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
