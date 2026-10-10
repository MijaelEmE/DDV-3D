# Contexto del Proyecto PA3 - Videojuegos Unity

## Estado Actual

### Scripts Existentes
- **AltarInteractable.cs** — Sistema de interacción con altares (trigger-based, ejecuta eventos Unity)
- **AltarActivationEffects.cs** — Efectos al activar altares (luz, puerta, panel de mensaje)

### Estructura de Assets
- `Materials/` — Materiales y texturas
- `Prefabs/` — Prefabs del proyecto
- `Scenes/Main.unity` — Escena principal
- `Scripts/` — Scripts C#
- `VFX/` — Efectos visuales

### Dependencias Externas (Package Manager)
- ModularFirstPersonController — Controlador FPS
- Polytope Studio — Entorno Low Poly
- AllSkyFree — Skyboxes

### Últimos Commits
- a6a57bf: first person can walk, and portal feature activated
- 8668c2d: Merge feature/entorno
- 2fd88ce: feat: crear entorno y decoracion inicial

## Problemas Identificados & Mejoras

### Bugs Conocidos
- [ ] Texturas con problemas
- [ ] Interacción con portal no refinada

### Features Pendientes
- [ ] Mecanánicas de jugabilidad mejoradas
- [ ] Sistema de eventos más robusto
- [ ] Efectos visuales adicionales

## Rama de Trabajo
- **Branch actual:** feature/gameplay
- **Convención:** feature/<area>
- **Base:** main

## Configuración Unity
- **Versión:** 6000.0.82f1
- **Render Pipeline:** Built-in
- **Serialización:** Force Text (YAML)
