# Plan de Mejoras - PA3 Videojuegos

## 🎯 Objetivos
- [ ] Arreglar bugs de texturas
- [ ] Agregar features de jugabilidad
- [ ] Mejorar interacción del portal
- [ ] Optimizar efectos visuales

## 🐛 Bugs Identificados

### Bug: Texturas Faltantes/Incorrectas
- **Descripción:** Algunos materiales muestran magenta o no cargan correctamente
- **Causa Probable:** Assets externos no importados o referencias rotas
- **Solución:** Verificar Package Manager (ModularFirstPersonController, Polytope Studio, AllSkyFree)
- **Archivos Relacionados:** `Materials/`, `Scenes/Main.unity`
- **Status:** ⏳ Por revisar

### Bug: Portal Interacción Incompleta
- **Descripción:** El sistema de portal existe pero puede no funcionar completamente
- **Scripts Relacionados:** `AltarInteractable.cs`, `AltarActivationEffects.cs`
- **Status:** ⏳ Por revisar en editor

## ✨ Features Pendientes

### Feature 1: Sistema de Interacción Mejorado
- [ ] Agregar feedback visual al interactuar
- [ ] Sonidos de interacción
- [ ] Animaciones de objetos
- **Complejidad:** Media
- **Estimado:** 2-3 horas

### Feature 2: Mechénicas de Gameplay
- [ ] Sistema de recolección de items (si aplica)
- [ ] Sistema de puntos/score
- [ ] Progresión de niveles
- **Complejidad:** Alta
- **Estimado:** 4-6 horas

### Feature 3: Efectos Visuales Mejorados
- [ ] Más efectos de partículas
- [ ] Animaciones de transición
- [ ] Efectos de iluminación dinámica
- **Complejidad:** Media
- **Estimado:** 2-3 horas

### Feature 4: Sistema de UI
- [ ] HUD mejorado
- [ ] Menú de pausa
- [ ] Pantalla de game over / victoria
- **Complejidad:** Media-Alta
- **Estimado:** 3-4 horas

## 📋 Checklist de Trabajo

### Fase 1: Diagnóstico (Este commit)
- [x] Revisar estado actual del proyecto
- [x] Identificar bugs principales
- [x] Documentar estructura

### Fase 2: Arreglos Críticos
- [ ] Resolver texturas/materiales
- [ ] Verificar funcionamiento de portal
- [ ] Testear en editor

### Fase 3: Nuevas Features
- [ ] Implementar features seleccionadas
- [ ] Testear jugabilidad
- [ ] Optimizar rendimiento

### Fase 4: Pulido
- [ ] Revisar diseño visual
- [ ] Ajustar sonidos y efectos
- [ ] PR y merge a main

## 🔧 Próximos Pasos

1. **Abre el editor Unity** con la escena `Assets/_Project/Scenes/Main.unity`
2. **Verifica los assets externos** en Package Manager
3. **Identifica visualmente** qué texturas están rotas
4. **Reporta** qué features específicas quieres agregar

## 📂 Archivos Clave
- Main Script 1: `Assets/_Project/Scripts/AltarInteractable.cs`
- Main Script 2: `Assets/_Project/Scripts/AltarActivationEffects.cs`
- Escena: `Assets/_Project/Scenes/Main.unity`
- Materiales: `Assets/_Project/Materials/`
- Prefabs: `Assets/_Project/Prefabs/`
