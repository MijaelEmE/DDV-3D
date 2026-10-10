# Fase 3: Audio, UI y Efectos Visuales

## 🤖 Auto-configuración

En Unity, ve a:
**`Doctocliq > Setup Phase 3 - Audio UI VFX`**

Esto crea automáticamente:
- ✅ AudioManager (gestor de sonidos)
- ✅ Canvas con HUD (Items, Vidas, Objetivo)
- ✅ Pantalla de Victoria
- ✅ Pantalla de Derrota
- ✅ Botón de Reintentar

---

## 📊 Lo que se implementó

### AudioManager
- Música de fondo en loop
- Reproductor de efectos de sonido
- Control de volumen para música y SFX

### HUD (GameUI)
Muestra en tiempo real:
- **Items recolectados:** 0/3
- **Vidas restantes:** ❤️ ❤️ ❤️
- **Objetivo actual:** "Encuentra 3 objetos"
- **Barra de salud:** Verde → Amarillo → Rojo

### Pantalla de Victoria
- Título: "VICTORIA! Escapaste del templo"
- Botón para reintentar
- Fondo oscuro con transparencia

### Pantalla de Derrota
- Título: "DERROTA! El guardián te capturó"
- Botón para reintentar
- Fondo oscuro

---

## 🎵 Audio Setup (MANUAL)

Para que el audio funcione, necesitas audio clips:

1. En el folder `Assets/` busca o descarga:
   - Música de fondo (ej: "background_music.mp3")
   - Sonidos de items
   - Sonidos de pasos
   - Sonidos de alerta del guardian

2. Asignalos en los respectivos scripts:
   - **AudioManager:** Background Music
   - **ItemPickup:** Pickup Sound
   - **GuardianAI:** Alert Sound, Attack Sound

### Sonidos Gratuitos:
- Freesound.org
- Zapsplat.com
- OpenGameArt.org

---

## 🧪 Testing

1. **Presiona Play**
2. Verifica que aparezca el HUD:
   - Items: 0/3
   - Vidas: 3
   - Objetivo: "Encuentra 3 objetos"
3. Recolecta un item:
   - Items debería cambiar a 1/3
4. Pierdes una vida:
   - Vidas debería cambiar a 2
   - Barra de salud se reduce
5. Completa objetivo:
   - Items llega a 3/3
   - Portal se abre
6. Toca portal:
   - Pantalla de Victoria aparece
   - Reinicio automático en 5 segundos

---

## 📋 Checklist

- [ ] AudioManager en la escena
- [ ] Canvas visible con HUD
- [ ] HUD muestra Items (0/3)
- [ ] HUD muestra Vidas (3)
- [ ] Items se actualizan al recolectar
- [ ] Vidas se actualizan al recibir daño
- [ ] Pantalla de Victoria funciona
- [ ] Pantalla de Derrota funciona
- [ ] Botón Reintentar funciona

---

## 🎨 Personalización de UI

Para cambiar colores, tamaños o posiciones:

1. Selecciona el Canvas en Hierarchy
2. Modifica los elementos en el Inspector:
   - Colores de paneles
   - Tamaños de texto
   - Posiciones

---

## 📊 Scripts Creados

| Script | Función |
|--------|---------|
| `AudioManager.cs` | Gestiona música y sonidos |
| `GameUI.cs` | HUD principal en tiempo real |
| `GameOverUI.cs` | Pantallas de victoria/derrota |
| `Phase3Setup.cs` | Setup automático |

---

## ⚙️ Próximos Pasos

1. **Ahora:** Ejecutar setup de Fase 3
2. **Después:** Agregar audio clips
3. **Fase 4:** Optimización y pulido
4. **Fase 5:** Testing y build

---

**¿Ejecutaste el setup? Reporta qué ves.** 🎮
