## Архитектура проекта Pacman Sailor

Ниже — практичное описание архитектуры на уровне папок, модулей и потоков данных, основанное на текущей структуре `Assets/PacmanSailor`.

### Цели архитектуры
- **Простая навигация по коду**: логика разнесена по слоям и доменным модулям.
- **Расширяемость**: добавление персонажей, уровней, экранов UI и сервисов без изменения существующих классов.
- **Тестируемость и изоляция**: минимизация скрытых зависимостей, конфигурация через ScriptableObject.
- **Реактивность и асинхронность**: UniRx/UniTask для событий и корутиноподобной логики.

---

### Слои и папки

- `Assets/PacmanSailor/Scripts`
  - `Bootstrap.cs`: точка входа сцены; собирает и связывает подсистемы (см. `Core/Instantiator`).
  - `Core/`
    - `Instantiator.cs`: простой DI/фабрика для создания и выдачи зависимостей.
    - `IPaused.cs`: контракт паузы, реализуемый системами, чувствительными к паузе.
  - `Config/`
    - `LevelsConfig.cs`, `CharactersConfig.cs`, `UIConfig.cs`: ScriptableObject-конфиги; хранят параметры уровня, персонажей и UI.
  - `Enum/`
    - Типы и состояния: `GameStates`, `CharacterType`, `PatrollerStates` и др.
  - `Data/`
    - DTO/модели данных: `CharacterData` и т.п., отделяют данные от поведения.
  - `GameCycle/`
    - Управление жизненным циклом: `GameStarter`, `GamePauser`, `GameEnder`, `GameCycleController`, `AbstractGameCycle`.
    - Отвечает за переходы состояний игры, запуск/остановку подсистем, показ окон победы/поражения.
  - `Level/`
    - Генерация и инициализация уровня: `LevelConstructor`, `PelletsGenerator`.
  - `Items/`
    - Игровые предметы: `Pellet` и `Items/Management/PelletsManager` (управление коллекцией).
  - `Character/`
    - `Characters/`: доменные сущности: `Pacman`, `Ambusher`, `Patroller`, `Stalker`, `AbstractCharacter`, `ICharacter`.
    - `Control/`: управление поведением и входом: `PlayerInput`, `ICharacterControl`, `AmbusherControl`, `PatrollerControl`, `AbstractEnemyControl`.
      - `States/`: состояния ИИ: `PatrollingControlState`, `AmbushingControlState`, `StalkingControlState`, `IEnemyControlState`.
      - `Modules/`: вспомогательные модули, напр. `PlayerTrigger` (детекция событий).
      - `Parts/`: маркеры и точки интереса (напр. `AmbushPoint`).
    - `Management/`: спавн/пуллинг: `CharactersPool`, `CharacterSpawner`, `CharacterSpawnPoint`.
    - `Movement/`: сервисы перемещения: `MovementService`.
  - `UI/`
    - Паттерн MVVM: `View/`, `ViewModel/`, `Model/`, общие компоненты в `Components/`.
    - Примеры: `HUDView` + `HUDViewModel` + `HUDModel`, окна `WinWindow*`, `LoseWindow*`.
    - `Management/UIInstaller.cs`: сборка/инициализация UI-слоя.
  - `InputSystem.cs` и `Character/Control/PlayerInput.cs`: интеграция с Input System и маппинг управления игрока.

- `Assets/PacmanSailor/Prefabs`
  - `Levels/`: заготовки уровней, `LevelConstructor` prefab.
  - `Characters/`: `Pacman`, призраки (`GhostAmbusher`, `GhostPatroller`, `GhostStalker`), служебные — `Management/`.
  - `Items/`: `Pellet`.
  - `Marks/`: точки спавна, патруля, засад.
  - `UI/`: `Views/` (экраны/окна), `Components/`, `Management/`.

- `Assets/PacmanSailor/Config`
  - ScriptableObject-активы: `Levels Config.asset`, `Characters Config.asset`, `UI Config.asset`.

- Прочее: `Materials/`, `Textures/`, `NavMeshes/`, `PhysicsMaterials/` — визуальные и физические ресурсы.

---

### Потоки данных и взаимодействия

1) Загрузка сцены
- `Bootstrap` создаёт `Instantiator`, регистрирует сервисы, читает ScriptableObject-конфиги.
- Инициализируются `GameCycleController`, `UIInstaller`, менеджеры уровня и предметов.

2) Жизненный цикл игры
- `GameCycleController` управляет состояниями (`GameStates`) и рассылает события в зависимые подсистемы.
- `GameStarter` — старт матча, спавн персонажей через `CharacterSpawner` и генерация пеллетов.
- `GamePauser` — централизованная пауза (через `IPaused`).
- `GameEnder` — условия завершения и показ окон (`Win/Lose`).

3) Персонажи и ИИ
- Игрок: `PlayerInput` получает направление движения из `InputSystem` и передаёт в `MovementService`/`Pacman`.
- Враги: контроллеры (`AmbusherControl`, `PatrollerControl`, и т.д.) переключают `States/*` в зависимости от контекста уровня (маркеры, видимость, таймеры, события).
- Пулы/спавн: через `CharactersPool` и `CharacterSpawner` по точкам `CharacterSpawnPoint`.

4) Уровень и предметы
- `LevelConstructor` строит уровень/грид/навигацию (использует `NavMeshes`/разметку).
- `PelletsGenerator` создаёт пеллеты; `PelletsManager` отслеживает сбор, оповещает `GameCycle`/`UI`.

5) UI (MVVM)
- `ViewModel` — посредник между моделями/сервисами и `View`, подписывается на события (UniRx), содержит презентационную логику.
- `Model` — источник данных (прим.: `HUDModel`, `WinWindowModel`).
- `View` — чистый слой представления (MonoBehaviour), биндится к VM, не содержит бизнес-логики.

6) Конфигурация и параметры
- Все настройки вынесены в ScriptableObject: уровни, персонажи, UI. Это позволяет менять баланс и контент без изменения кода.

---

### Технологические решения
- **UniRx**: реактивные стримы для ввода, событий игры, подписок UI.
- **UniTask**: асинхронные операции без избыточных корутин.
- **Input System**: действия описаны в `Assets/InputSystem_Actions.inputactions`, интеграция через `InputSystem.cs` и `PlayerInput`.

---

### Расширение и точки интеграции
- **Новый враг**: наследовать `AbstractCharacter` и создать контроллер от `AbstractEnemyControl` + набор `States/*` при необходимости. Добавить prefab в `Prefabs/Characters` и записи в `CharactersConfig`.
- **Новый уровень**: собрать сцену уровня или prefab из `Prefabs/Levels`, обновить `Levels Config.asset`, настроить `Marks/*` и навмеш.
- **Новый UI-экран**: создать `View` + `ViewModel` + `Model`, прописать в `UIInstaller` и `UI Config.asset`.
- **Новый предмет**: реализовать MonoBehaviour для предмета, менеджер в `Items/Management`, добавить генерацию/спавн, события для `GameCycle` и бинды в UI.
- **Сервис/подсистема**: зарегистрировать в `Instantiator`, внедрять по месту использования (через фабрику/ссылки из Bootstrap/Installer).

---

### Соглашения
- Папка = модуль домена; внутри — подуровни (Management/Control/States/Model и т.д.).
- MonoBehaviour в `View` и сценовых объектах — только представление/интеграция; доменная логика — в моделях/сервисах/контроллерах.
- Состояния ИИ — отдельные классы, переключение — через контроллеры.
- Все параметры, которые может менять геймдизайнер, — в ScriptableObject.

---

### Диаграмма на словах
- Вход (Input System) → `PlayerInput` → `Pacman` → `MovementService`.
- Враги: `EnemyControl` → `State`(ы) → перемещение/реакция на триггеры `Modules/*`.
- Уровень: `LevelConstructor` → спавн `PelletsGenerator`/маркеры → `PelletsManager`.
- Игровой цикл: `GameCycleController` ⇄ `GameStarter`/`GamePauser`/`GameEnder`.
- UI: `Model` → `ViewModel` ⇄ (события UniRx) → `View`.
- Конфиги: ScriptableObject → читаются `Bootstrap`/`Instantiator`/инсталляторами.

---

### Что важно знать новым разработчикам
- Начинайте с `Bootstrap.cs` и `GameCycleController.cs`, чтобы понять запуск и состояние игры.
- Для UI смотрите `UIInstaller.cs`, затем пары `*ViewModel`/`*View`/`*Model`.
- Для персонажей откройте `Character/Characters` и соответствующий контроллер в `Character/Control`.
- Баланс/параметры ищите в `Assets/PacmanSailor/Config` (активы) и `Scripts/Config` (типы).



