# Plan de Trabajo PA3 - Videojuego 3D Unity

## 📋 Resumen Ejecutivo

**Proyecto:** Videojuego 3D completo en Unity 6000.0.82f1  
**Duración estimada:** 4-6 semanas (80-120 horas de trabajo)  
**Equipo:** 29 personas Doctocliq (adaptado para este proyecto educativo)  
**Alcance:** 1 nivel corto, 3-7 minutos de gameplay, mecánicas core bien pulidas  
**Género propuesto:** Exploración + Puzzle (compatible con assets actuales)

---

## 🎮 Concepto del Juego (PROPUESTA)

**Nombre:** "Portal Guardian" o "The Altar Key"  
**Género:** Exploración 3D + Puzzle simple  
**Objetivo Principal:** Encontrar 3 objetos antiguos esparcidos en el entorno para activar un portal y escapar  
**Mecánica Core:** Recolección de objetos + Activación de mecanismos (altares)  
**Duración:** 5 minutos de gameplay

### Por qué este concepto:
✅ Usa los altares que ya existen  
✅ Compatible con assets Polytope Studio (entorno natural)  
✅ Permite IA simple (guardian que patrulla)  
✅ Fácil de debuggear y pulir  
✅ Escalable (se pueden agregar más altares/objetos)

---

## 🗂️ Estructura del Juego

```
Scenes/
├── Main.unity          ← Nivel principal (3-7 min gameplay)
├── Menu.unity          ← Menú principal (OPCIONAL: Fase 4)
└── GameOver.unity      ← Pantalla de victoria/derrota (Fase 3)

Scripts/
├── Player/
│   ├── PlayerController.cs         (YA EXISTE - mejoras)
│   ├── PlayerInventory.cs          (NUEVO - recolecta objetos)
│   └── PlayerInteraction.cs        (NUEVO - interactúa con altares)
├── Gameplay/
│   ├── AltarInteractable.cs        (YA EXISTE - refactor)
│   ├── AltarActivationEffects.cs   (YA EXISTE - mejorar)
│   ├── ItemPickup.cs               (NUEVO - sistema de items)
│   └── ObjectiveManager.cs         (NUEVO - controla objetivo)
├── AI/
│   ├── GuardianAI.cs               (NUEVO - enemigo patrullador)
│   ├── AIStateMachine.cs           (NUEVO - patrulla/persecución)
│   └── PatrolPath.cs               (NUEVO - puntos de patrulla)
├── UI/
│   ├── GameUI.cs                   (NUEVO - HUD principal)
│   ├── ObjectiveUI.cs              (NUEVO - mostrar progreso)
│   └── GameOverUI.cs               (NUEVO - pantalla final)
├── Audio/
│   ├── AudioManager.cs             (NUEVO - gestión de sonidos)
│   └── MusicController.cs          (NUEVO - gestión música)
└── Utils/
    ├── GameManager.cs              (NUEVO - orquestador del juego)
    └── ObjectPool.cs               (NUEVO - optimización)

Prefabs/
├── Items/
│   ├── AncientKey.prefab           (NUEVO)
│   ├── MysticOrb.prefab            (NUEVO)
│   └── SacredAmulet.prefab         (NUEVO)
├── Enemies/
│   └── Guardian.prefab             (NUEVO - IA)
└── VFX/
    ├── PortalActivation.prefab     (NUEVO - partículas)
    └── ItemPickup.prefab           (NUEVO - efecto al recolectar)

Materials/
├── Items/
│   ├── M_AncientKey.mat            (NUEVO)
│   ├── M_MysticOrb.mat             (NUEVO)
│   └── M_SacredAmulet.mat          (NUEVO)
├── Enemy/
│   └── M_Guardian.mat              (NUEVO)
└── VFX/
    └── M_PortalEnergy.mat          (NUEVO)
```

---

## 📅 Fases de Desarrollo

### ⏱️ Cronograma Estimado
- **Fase 1:** 1 semana (Core gameplay)
- **Fase 2:** 1.5 semanas (IA y enemigos)
- **Fase 3:** 1 semana (UI y audio)
- **Fase 4:** 1 semana (Pulido y optimización)
- **Fase 5:** 0.5 semanas (Testing y build)

---

## 🔴 FASE 1: Core Gameplay (Semana 1)

### 1.1 Sistema de Recolección de Objetos
**Objetivo:** Poder recolectar items que aparecen en el mundo  
**Archivos a crear:**
- `Scripts/Gameplay/ItemPickup.cs` — Script para items interaccionables
- `Prefabs/Items/` — 3 prefabs de items diferentes

**Checklist:**
- [ ] Crear 3 modelos/materiales de items (cubes temporales está ok)
- [ ] Script ItemPickup detecta al jugador (sphere trigger)
- [ ] Al tocar, agrega a inventario y desaparece
- [ ] Efecto visual pequeño (fade out o escala)
- [ ] Sonido al recolectar (placeholder)

**Dependencias:** Nada (independiente)

---

### 1.2 Sistema de Inventario
**Objetivo:** El jugador sabe qué items tiene recolectados  
**Archivos a crear:**
- `Scripts/Player/PlayerInventory.cs` — Gestiona items

**Checklist:**
- [ ] Almacena items recolectados en lista
- [ ] Consulta cantidad de items
- [ ] Notifica cuando se completan todos

**Dependencias:** 1.1 ItemPickup

---

### 1.3 Objetivo del Juego
**Objetivo:** Sistema que controla el estado del juego (inicio → gameplay → victoria)  
**Archivos a crear:**
- `Scripts/Gameplay/ObjectiveManager.cs` — Controla objetivo
- `Scripts/Utils/GameManager.cs` — Orquestador del juego

**Checklist:**
- [ ] ObjectiveManager espera 3 items recolectados
- [ ] Al completar, abre el portal
- [ ] Mensaje en pantalla: "¡Portal Abierto!"
- [ ] Detecta cuando jugador toca el portal → Victoria

**Dependencias:** 1.2 Inventario

---

### 1.4 Mejoras a Altares Existentes
**Objetivo:** Refactorizar y mejorar los scripts existentes  
**Archivos a modificar:**
- `Scripts/Gameplay/AltarInteractable.cs`
- `Scripts/Gameplay/AltarActivationEffects.cs`

**Checklist:**
- [ ] Refactorizar código (separar responsabilidades)
- [ ] Agregar feedback visual mejor (luz pulsante)
- [ ] Agregar sonido de activación
- [ ] Compatibilidad con ObjectiveManager

**Dependencias:** Nada

---

### 📊 Fase 1: Estado Esperado
- ✅ Puedes caminar por el nivel
- ✅ Recolectas 3 items diferentes
- ✅ Portal se activa al completar objetivo
- ✅ Puedes "ganar" al tocar el portal
- ❌ No hay enemigos aún
- ❌ No hay UI completa
- ❌ No hay sonido ambiental

---

## 🔵 FASE 2: Inteligencia Artificial (1.5 semanas)

### 2.1 Sistema de Estados (State Machine)
**Objetivo:** Base para comportamientos complejos de IA  
**Archivos a crear:**
- `Scripts/AI/AIStateMachine.cs` — Máquina de estados genérica

**Checklist:**
- [ ] Sistema flexible de estados
- [ ] Transiciones entre estados
- [ ] Actualización de comportamiento por frame

**Dependencias:** Nada

---

### 2.2 IA - Estado Patrulla
**Objetivo:** Guardian camina en patrón predefinido  
**Archivos a crear:**
- `Scripts/AI/PatrolPath.cs` — Define puntos de patrulla
- `Scripts/AI/GuardianAI.cs` — IA del guardian
- `Prefabs/Enemies/Guardian.prefab`

**Checklist:**
- [ ] Crear 4-5 waypoints en la escena
- [ ] Guardian sigue los waypoints
- [ ] Rotación suave hacia el siguiente punto
- [ ] Velocidad configurable
- [ ] Modelo/material del guardian

**Dependencias:** 2.1

---

### 2.3 IA - Estado Persecución
**Objetivo:** Guardian persigue al jugador cuando lo ve  
**Archivos a crear:**
- Script de persecución dentro de GuardianAI.cs

**Checklist:**
- [ ] Guardian detecta jugador (Raycast o distancia)
- [ ] Cambia a estado "Persiguiendo"
- [ ] Corre hacia el jugador
- [ ] Si pierde de vista, vuelve a patrulla
- [ ] Sonido de alerta cuando detecta

**Dependencias:** 2.2

---

### 2.4 Sistema de Combate Simple
**Objetivo:** El guardian puede "golpear" al jugador  
**Archivos a crear:**
- `Scripts/Player/PlayerHealth.cs` — Vida del jugador

**Checklist:**
- [ ] Jugador tiene 3 vidas
- [ ] Guardian hace daño al tocar
- [ ] Knockback al jugador
- [ ] Sonido de golpe
- [ ] Derrota al llegar a 0 vidas

**Dependencias:** 2.3

---

### 📊 Fase 2: Estado Esperado
- ✅ Aparece un enemigo en la escena
- ✅ Patrulla automáticamente
- ✅ Te persigue si te ve
- ✅ Puede hacerte daño
- ✅ Puedes morir (Game Over)
- ❌ No hay múltiples enemigos
- ❌ No hay UI de vida aún

---

## 🟢 FASE 3: Audio, UI y Pulido (1 semana)

### 3.1 Sistema de Audio
**Objetivo:** Música, sonidos ambientales y efectos  
**Archivos a crear:**
- `Scripts/Audio/AudioManager.cs`
- `Scripts/Audio/MusicController.cs`
- Audio clips (buscar en Freesound o usar assets incluidos)

**Checklist:**
- [ ] Música de fondo (loop)
- [ ] Sonido de pasos (footsteps)
- [ ] Sonido de recolectar item
- [ ] Sonido de activar altar
- [ ] Sonido de persecución/alerta guardian
- [ ] Sonido de golpe/daño
- [ ] Sonido de victoria
- [ ] Volumen controlable

**Dependencias:** Todo (audios en todas partes)

---

### 3.2 HUD Principal
**Objetivo:** Mostrar info relevante en pantalla  
**Archivos a crear:**
- `Scripts/UI/GameUI.cs`
- `Scripts/UI/ObjectiveUI.cs`

**Checklist:**
- [ ] Mostrar items recolectados (3/3)
- [ ] Mostrar vidas del jugador (❤️ ❤️ ❤️)
- [ ] Mostrar objetivo actual ("Encuentra 3 items")
- [ ] Mostrar controles (teclas disponibles)
- [ ] Indicador de portal abierto
- [ ] Estilos: fuente clara, colores con contraste

**Dependencias:** Fase 1 y 2

---

### 3.3 Pantalla de Game Over / Victoria
**Objetivo:** Escenas de cierre del juego  
**Archivos a crear:**
- `Scripts/UI/GameOverUI.cs`
- `Scenes/GameOver.unity` (o en Main.unity con canvas extra)

**Checklist:**
- [ ] Pantalla de Victoria (¡Escapaste!)
- [ ] Pantalla de Derrota (Te atraparon)
- [ ] Botón "Reintentar" (recarga escena)
- [ ] Botón "Menú" (ir a inicio)
- [ ] Sonido de victoria/derrota
- [ ] Efectos visuales simples

**Dependencias:** 3.1, 3.2

---

### 3.4 Efectos Visuales (Partículas)
**Objetivo:** Al menos 2 sistemas de partículas  
**Archivos a crear:**
- `Prefabs/VFX/PortalActivation.prefab`
- `Prefabs/VFX/ItemPickup.prefab`

**Checklist:**
- [ ] Efecto de portal activándose (brillo, partículas)
- [ ] Efecto al recolectar item (estrellas, chispas)
- [ ] Efecto de daño (sangre o rojo)
- [ ] Fade in/out suave

**Dependencias:** Nada (visual puro)

---

### 📊 Fase 3: Estado Esperado
- ✅ Música y sonidos ambientales
- ✅ HUD mostrando progreso
- ✅ Efectos visuales de recolecta y portal
- ✅ Pantalla de victoria funcional
- ✅ Pantalla de derrota funcional
- ✅ Juego "jugable" de punta a punta

---

## 🟡 FASE 4: Optimización y Pulido (1 semana)

### 4.1 Optimizaciones
**Objetivo:** Mejorar rendimiento  
**Archivos a crear/modificar:**
- `Scripts/Utils/ObjectPool.cs` — Pool de objetos

**Checklist:**
- [ ] Object Pool para items (reutiliza prefabs)
- [ ] Object Pool para efectos de partículas
- [ ] Occlusion Culling en la escena (si hay separaciones)
- [ ] Limitar draw calls
- [ ] Optimizar sombras (baked si es posible)

**Dependencias:** Nada

---

### 4.2 Refactorización y Documentación
**Objetivo:** Código limpio y documentado  
**Checklist:**
- [ ] Comentarios en scripts principales
- [ ] Nombres de variables claros
- [ ] Separación de responsabilidades
- [ ] Sin código muerto
- [ ] Estructura de carpetas clara

**Dependencias:** Nada

---

### 4.3 Balanceo de Gameplay
**Objetivo:** Ajustar dificultad y sensación  
**Checklist:**
- [ ] Guardian no es demasiado fácil/difícil
- [ ] Items son fáciles de encontrar
- [ ] Música y sonidos tienen volumen adecuado
- [ ] Tiempo de gameplay: ~5 minutos
- [ ] Controles responden bien

**Dependencias:** Todas las fases anteriores

---

### 4.4 Menú Principal (OPCIONAL)
**Archivos a crear:**
- `Scripts/UI/MenuUI.cs`
- `Scenes/Menu.unity`

**Checklist:**
- [ ] Botón "Jugar"
- [ ] Botón "Salir"
- [ ] Título del juego
- [ ] Créditos/Instrucciones

**Dependencias:** 3.1 (audio)

---

### 📊 Fase 4: Estado Esperado
- ✅ Juego optimizado y rápido
- ✅ Código limpio y documentado
- ✅ Gameplay balanceado
- ✅ Buena experiencia visual
- ✅ Audio en su lugar

---

## 🔴 FASE 5: Testing y Build (0.5 semanas)

### 5.1 Testing Funcional
**Checklist:**
- [ ] Prueba: Recolectar todos los items
- [ ] Prueba: Activar portal
- [ ] Prueba: Escapar por portal (Victoria)
- [ ] Prueba: Morir por guardian (Derrota)
- [ ] Prueba: Guardian patrulla correctamente
- [ ] Prueba: HUD actualiza correctamente
- [ ] Prueba: Todos los sonidos suenan
- [ ] Prueba: Sin crashes

---

### 5.2 Build del Proyecto
**Checklist:**
- [ ] `File > Build Settings`
- [ ] Agregar Main.unity a escenas
- [ ] Configurar plataforma (Windows/Mac/WebGL)
- [ ] Build y ejecutar
- [ ] Verificar que funciona fuera del editor
- [ ] Sin errores de consola

---

### 5.3 Documentación Final
**Checklist:**
- [ ] README.md con instrucciones de juego
- [ ] Controles documentados
- [ ] Objetivo claro
- [ ] Créditos de assets usados

---

## 📊 Estado Final Esperado
- ✅ Juego completo y jugable
- ✅ Build funcional
- ✅ Todos los requerimientos de la PA3
- ✅ 5 minutos de gameplay
- ✅ IA con 2 comportamientos
- ✅ Mecánicas pulidas
- ✅ Audio y visuales
- ✅ Documentación

---

## 📈 Requerimientos de la PA3 vs Deliverables

| Requerimiento | Implementado | Archivo |
|---------------|-------------|---------|
| Inicio, gameplay, objetivo, victoria/derrota | ✅ | GameManager, ObjectiveManager |
| Entorno 3D explorable | ✅ | Main.unity + Polytope Studio |
| Mecánica principal (recolección) | ✅ | ItemPickup.cs |
| UI con información | ✅ | GameUI.cs, ObjectiveUI.cs |
| Música y sonidos | ✅ | AudioManager.cs |
| Efecto visual/partículas | ✅ | PortalActivation, ItemPickup VFX |
| IA con 2 comportamientos | ✅ | GuardianAI (patrulla + persecución) |
| Código organizado | ✅ | Estructura de carpetas clara |
| Técnica optimización | ✅ | ObjectPool.cs, Occlusion Culling |
| Build funcional | ✅ | Fase 5 |

---

## 🎯 Prioridades

### Crítico (Sin esto no hay juego):
1. ✅ Core gameplay (recolecta items)
2. ✅ Objetivo funcional (portal se abre)
3. ✅ IA básica (al menos patrulla)
4. ✅ UI mínima (saber dónde estoy)
5. ✅ Build que funcione

### Importante (Mejora experiencia):
6. Audio ambiente
7. Efectos visuales
8. Balanceo de dificultad
9. Feedback visual/auditivo

### Nice to have:
10. Menú principal
11. Múltiples niveles
12. Enemigos adicionales

---

## 🔧 Stack Tecnológico

- **Motor:** Unity 6000.0.82f1
- **Lenguaje:** C#
- **Plataforma:** Windows/Mac/WebGL
- **Assets:** Polytope Studio, ModularFirstPersonController, AllSkyFree
- **Audio:** Freesound.org (libre) o assets de Unity
- **VFX:** Built-in Particle System de Unity

---

## 📌 Próximos Pasos

1. **Ahora:** Revisar este plan contigo
2. **Semana 1:** Empezar Fase 1 (items + objetivo)
3. **Semana 2-3:** Fases 2-3 (IA, audio, UI)
4. **Semana 4:** Fase 4 (optimización y pulido)
5. **Semana 5:** Fase 5 (testing y build)

**¿Estás de acuerdo con este plan? ¿Quieres cambios en:**
- Mecánicas del juego?
- Cantidad de features?
- Estimación de tiempo?
- Fase de inicio?
