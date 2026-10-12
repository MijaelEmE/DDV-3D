# Fase 1: Setup en Unity

## Scripts Creados ✅

- ✅ `PlayerInventory.cs` — Sistema de inventario (max 3 items)
- ✅ `ItemPickup.cs` — Script para recolectar items
- ✅ `ObjectiveManager.cs` — Controla objetivo (abre portal al recolectar 3)
- ✅ `GameManager.cs` — Orquestador del juego (estados: Playing, Paused, Victory, Defeat)
- ✅ `PortalExit.cs` — Detecta victoria cuando tocas el portal
- ✅ `AltarInteractable.cs` — MEJORADO (sonidos, delay)
- ✅ `AltarActivationEffects.cs` — MEJORADO (pulsing light, partículas)

---

## 🛠️ Instrucciones de Setup en Unity

### Paso 1: Agregar PlayerInventory a la Escena

1. En `Assets/_Project/Scenes/Main.unity`
2. Crea un nuevo **Empty GameObject** → Renómbralo "PlayerInventory"
3. Agrega el componente: **Add Component > PlayerInventory**
4. Configura:
   - **Max Items:** 3

### Paso 2: Agregar GameManager a la Escena

1. Crea otro **Empty GameObject** → "GameManager"
2. Agrega: **Add Component > GameManager**
3. Configura:
   - **Victory Panel:** (vacío por ahora, lo crearemos después)
   - **Defeat Panel:** (vacío por ahora)
   - Victory Sound: Busca un audio clip en Assets (o déjalo vacío)
   - Defeat Sound: (opcional)

### Paso 3: Agregar ObjectiveManager

1. Crea otro **Empty GameObject** → "ObjectiveManager"
2. Agrega: **Add Component > ObjectiveManager**
3. Configura:
   - **Portal Object:** (La estructura del portal en tu escena, o crea un Cube como placeholder)
   - **Portal Light:** (Una luz que está en el portal)
   - **Portal Activation Sound:** (Opcional)
   - **Objective Text:** "Encuentra 3 objetos antiguos"

### Paso 4: Crear Items para Recolectar

1. En la carpeta `Assets/_Project/Prefabs/Items/` (crea si no existe)
2. Crea 3 **Empty GameObjects** en la escena:
   - "AncientKey"
   - "MysticOrb"
   - "SacredAmulet"

3. Para cada uno:
   - Agrega un **Cube** como hijo (o modelo)
   - Escala pequeño (ej: 0.5, 0.5, 0.5)
   - Colorea diferente en el material
   - Agrega un **Sphere Collider** → Is Trigger: ✓
   - Agrega componente: **Add Component > ItemPickup**
   - Configura **Item Name:** (ej: "Ancient Key")

4. **Posiciona los 3 items** en diferentes lugares del mapa
5. Convertir cada uno a **Prefab**: Drag a `Assets/_Project/Prefabs/Items/`

### Paso 5: Mejorar el Portal Existente

1. Busca el portal en la escena
2. Agrega componente: **Add Component > PortalExit**
3. Configura:
   - **Player Tag:** "Player" ✓
4. Asegúrate de que tiene un **Collider como Trigger**

### Paso 6: Mejorar los Altares Existentes

1. Para cada altar:
   - Revisa el componente **AltarInteractable**
   - Agregar **AudioClip** para activation sound
   - Agregar **ParticleSystem** (o crea uno simple)
   - Conectar todo en **AltarActivationEffects**

---

## ✅ Checklist de Setup

- [ ] PlayerInventory en escena
- [ ] GameManager en escena
- [ ] ObjectiveManager en escena
- [ ] 3 items creados y posicionados
- [ ] Items tienen ItemPickup.cs
- [ ] Portal tiene PortalExit.cs
- [ ] Altares tienen mejoras
- [ ] Player tagged como "Player"

---

## 🧪 Testing

1. **Presiona Play** en el editor
2. Camina hasta cada item
3. Al tocar → debe desaparecer con fade out
4. Verifica en Console: "¡Portal Abierto!"
5. Camina al portal
6. Debe mostrar pantalla de Victoria

### Esperado:
- ✅ Items desaparecen al tocarlos
- ✅ Portal se abre después de recolectar 3
- ✅ Puedes ganar tocando el portal
- ✅ No hay errores en Console

### Si algo falla:
- Verifica Console por errores
- Asegúrate de que el Player tenga tag "Player"
- Revisa que los colliders sean Trigger
- Verifica que los scripts estén asignados

---

## 📝 Próximos Pasos

1. Después de configurar, toma una captura
2. Reporta si hay errores
3. Una vez funcione, pasamos a **Fase 2: IA del Guardian**

---

## 💡 Notas

- `PlayerInventory` y `GameManager` son **Singletons** (solo existe uno)
- Los items usan **fade out**, se pueden mejorar con partículas después
- No hay UI visual aún, se agregará en Fase 3
- Los altares son decorativos por ahora (se conectan más en Fase 2)
