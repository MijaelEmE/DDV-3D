# AGENTS.md

Proyecto Unity 6 `6000.0.82f1` (3D, Built-in Render Pipeline) — PA3 de Desarrollo de Videojuegos. Setup del equipo: ver `README.md`.

## Reglas criticas

- Usar exactamente Unity `6000.0.82f1` (fuente de verdad: `ProjectSettings/ProjectVersion.txt`). Otra version reescribe escenas/prefabs y produce diffs masivos.
- No commitear assets de terceros: `Assets/AllSkyFree`, `Assets/ModularFirstPersonController` y `Assets/Polytope Studio` estan ignorados a proposito (repositorio publico + licencias). Cada integrante los importa desde `Window > Package Manager > My Assets`.
- Un clon fresco sin esos assets muestra missing references y materiales magenta: es lo esperado, no un bug. No "arreglarlo" forzando esos archivos al repo.
- Todo el trabajo propio va en `Assets/_Project/`. Nunca editar las carpetas de terceros.

## Verificacion

- No hay test framework ni build por CLI (los `.csproj`/`.sln` los genera Unity y estan ignorados). La unica verificacion real es abrir `Assets/_Project/Scenes/Main.unity` en el Editor y entrar en Play Mode.

## Estructura

- `Assets/_Project/` — escena principal (`Scenes/Main.unity`), materiales, prefabs. Unico contenido autorado por el equipo (aun sin scripts propios).
- Assets externos: `ModularFirstPersonController` (controlador en primera persona), `Polytope Studio` (Low Poly Environment - Nature Free), `AllSkyFree` (skyboxes).
- `Library/`, `Temp/`, `Logs/`, `UserSettings/` y `mono_crash.*` son generados; nunca editar ni commitear.

## Git

- Ramas `feature/<area>` (p.ej. `feature/entorno`, `feature/gameplay`, `feature/presentacion`) con PR hacia `main` (estable).
- Commits convencionales en espanol: `feat: crear entorno y decoracion inicial`.
- Serializacion Force Text; `.gitattributes` fuerza LF en YAML y marca binarios. Commitear cada `.meta` junto a su asset.
- `Main.unity` y los prefabs son YAML con GUIDs: los merges son fragiles; coordinar antes de tocar la misma escena desde dos ramas.

## Busquedas

- Excluir `Library/` y `Temp/` al buscar codigo (enormes y regenerados); el trabajo del equipo esta bajo `Assets/_Project/`.
