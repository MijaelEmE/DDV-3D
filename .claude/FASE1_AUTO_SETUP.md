# FASE 1: Setup Automático

## 🤖 Auto-configuración con Editor Script

He creado un **Editor Script** que configura TODO automáticamente. No necesitas hacer nada manual.

### Cómo ejecutar:

1. **Abre Unity** (el proyecto debe estar abierto en el editor)
2. **Ve a:** `Doctocliq > Setup Phase 1 - Core Gameplay`
3. **Click** y espera ~5 segundos
4. ✅ **¡Hecho!** La escena se configuró automáticamente

### Qué hace el script:

✅ Crea `PlayerInventory` (gestor de items)  
✅ Crea `GameManager` (gestor de juego)  
✅ Crea `ObjectiveManager` (gestor de objetivo)  
✅ Crea 3 items de ejemplo (AncientKey, MysticOrb, SacredAmulet)  
✅ Crea un Portal placeholder si no existe  
✅ Configura altares existentes  
✅ Todo conectado y listo para jugar  

---

## 📋 Checklist Post-Setup

Después de ejecutar el script, verifica:

- [ ] En Hierarchy ves: PlayerInventory, GameManager, ObjectiveManager, Portal
- [ ] 3 cubos de colores en la escena (AncientKey, MysticOrb, SacredAmulet)
- [ ] Portal es azul/cian y está flotando
- [ ] Console dice "✅ Setup Fase 1 completado"

---

## 🧪 Prueba Rápida

1. **Presiona Play**
2. Camina hasta cada item (cubo de color)
3. Deben desaparecer con fade out
4. Console debe decir "¡Portal Abierto!"
5. Camina al portal (cubo azul)
6. Deberías ver "¡VICTORIA!"

---

## 🐛 Si algo falla

**Error: "Cannot load file"**
- Asegúrate de que Unity está abierto
- El proyecto debe estar abierto (no puede ser preview)

**No aparecen los objetos**
- Verifica que los scripts estén compilados (sin errores en Console)
- Presiona Play, luego Stop, y abre la escena nuevamente
- Intenta el script de nuevo

**Falta el Portal**
- El script lo crea automáticamente como placeholder (cubo azul)
- Puedes reemplazarlo con tu propia geometría después

---

## 📂 Archivo del Script

El script está en:
`Assets/_Project/Scripts/Editor/Phase1Setup.cs`

Es un **Editor Script** (solo funciona en el editor, no en build)

---

## ✅ Estado Actual

Después de ejecutar el setup, tienes:

**Funcionalidades Activas:**
- ✅ Recolectar items
- ✅ Portal se abre al completar
- ✅ Ganar al tocar portal
- ✅ Efectos de fade out
- ✅ Sistema de inventario

**Próximo Paso:**
- Fase 2: IA del Guardian (enemigo que patrulla y persigue)

---

## 💡 Notas Técnicas

- El script usa `EditorSceneManager` para manipular la escena
- Crea GameObjects y componentes directamente
- Usa `SerializedObject` para configurar propiedades
- Es seguro ejecutar múltiples veces (detecta duplicados)

---

**¿Ejecutaste el script? Reporta qué ves en Console.** ✅
