# Fase 2: IA del Guardian

## 🤖 Auto-configuración

### Paso 1: Ejecutar el Setup Script

En Unity, ve a:
**`Doctocliq > Setup Phase 2 - AI Guardian`**

Esto crea automáticamente:
- ✅ Guardian (enemigo rojo)
- ✅ PlayerHealth (sistema de vidas)
- ✅ PatrolPath (waypoints de patrulla)

### Paso 2: Configurar Waypoints (IMPORTANTE)

Después de ejecutar el script:

1. En **Hierarchy**, busca **`PatrolPath`**
2. Selecciona y mira el **Inspector**
3. Expande **"Waypoints"** (debe decir Size: 4)
4. Configura estas posiciones:
   - **[0]:** x=-10, y=0, z=0
   - **[1]:** x=-10, y=0, z=5
   - **[2]:** x=10, y=0, z=5
   - **[3]:** x=10, y=0, z=0

5. Selecciona **Guardian** y asigna el **PatrolPath** en el Inspector:
   - Find: **Patrol Path** field
   - Drag `PatrolPath` desde Hierarchy

---

## 🎮 Cómo Funciona

### Estados del Guardian

**1. PATRULLA (Patrol)**
- Camina entre los waypoints
- Busca al jugador
- Si lo ve → cambia a CHASE

**2. PERSECUCIÓN (Chase)**
- Corre hacia el jugador
- Si está en rango de ataque → ataca
- Si lo pierde de vista por 2 segundos → vuelve a PATRULLA

### Visión del Guardian

- **Rango de detección:** 15 metros
- **Usa Raycast:** Solo ve si hay línea directa
- **Patrulla Speed:** 2 m/s
- **Chase Speed:** 5 m/s

---

## ❤️ Sistema de Vidas del Jugador

- **Vidas iniciales:** 3
- **Daño por golpe:** 1 vida
- **Knockback:** El guardián te empuja cuando te golpea
- **Game Over:** Al llegar a 0 vidas

---

## 🧪 Testing

1. **Presiona Play**
2. Camina alrededor del Guardian (debería patrullar)
3. Acércate → debe perseguirte
4. Aléjate/escóndete → debe perder interés y volver a patrulla
5. Si te toca → pierdes 1 vida, recibes knockback
6. Al perder 3 vidas → Game Over

---

## 📋 Checklist Post-Setup

- [ ] Guardian aparece como cubo rojo
- [ ] PlayerHealth está en el jugador
- [ ] Guardian patrulla entre waypoints
- [ ] Guardian me persigue cuando lo ves
- [ ] Me hace daño al tocarme
- [ ] Pierdo vidas correctamente
- [ ] Game Over funciona

---

## 🐛 Problemas Comunes

**Guardian no patrulla:**
- Verifica que `PatrolPath` esté asignado en el Guardian

**Guardian no me ve:**
- Asegúrate de que hay línea de visión (Raycast)
- Verifica que el Player tiene tag "Player"

**No pierdo vidas al tocar:**
- Player necesita `PlayerHealth` (ejecuta el setup de nuevo)
- Player necesita Rigidbody

**Guardian muy fácil/difícil:**
- Ajusta `Detection Range` (15 por defecto)
- Ajusta `Chase Speed` (5 por defecto)
- Ajusta `Attack Range` (2 por defecto)

---

## 📊 Scripts Creados

| Script | Función |
|--------|---------|
| `AIStateMachine.cs` | Sistema base de estados (Máquina de estados) |
| `PatrolPath.cs` | Definir waypoints de patrulla |
| `GuardianAI.cs` | IA completa (patrulla + persecución + ataque) |
| `PlayerHealth.cs` | Sistema de vidas del jugador |

---

## ⚙️ Configuración Avanzada

Selecciona el **Guardian** en la Hierarchy e Inspector para ajustar:

```csharp
Patrol Speed: 2          // Velocidad de patrulla
Chase Speed: 5           // Velocidad de persecución
Detection Range: 15      // Cuán lejos ve
Attack Range: 2          // Rango de ataque
Attack Cooldown: 1       // Segundos entre ataques
```

---

## ✅ Próximos Pasos

Una vez que Fase 2 funcione:
- Fase 3: Audio, UI, Efectos Visuales
- Fase 4: Optimización y Pulido
- Fase 5: Testing y Build

---

**¿Ejecutaste el script? ¡Reporta cómo va!** 🚀
