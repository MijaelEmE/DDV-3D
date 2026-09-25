# DDV-3D - Entorno 3D interactivo

Proyecto grupal del Producto Academico 3 de Desarrollo de Videojuegos.

## Concepto propuesto

Un bosque medieval explorable en primera persona. El jugador recorre un sendero con desniveles y ruinas, activa un altar mediante un Trigger y observa un efecto de particulas. Esta idea aprovecha los tres assets seleccionados y cubre todos los requisitos del enunciado sin aumentar innecesariamente el alcance.

## Entorno comun

- Unity: `6000.0.82f1` (Unity 6.0 LTS).
- Plantilla/renderizado: 3D Built-in Render Pipeline.
- Git: rama estable `main` y una rama de trabajo por integrante.
- Repositorio: https://github.com/MijaelEmE/DDV-3D

Todos deben usar exactamente la version indicada en `ProjectSettings/ProjectVersion.txt`. Unity guarda escenas, prefabs y configuraciones con formatos sensibles a la version; abrir el proyecto con otra version puede generar cambios masivos o conflictos.

## Assets externos

1. Modular First Person Controller
2. Low Poly Environment - Nature Free - LOWPOLY MEDIEVAL FANTASY SERIES
3. AllSky Free - 10 Sky / Skybox Set

Importar los assets desde `Window > Package Manager > My Assets`. El repositorio es publico: no subir paquetes `.unitypackage` ni copias sin integrar que permitan redistribuir el asset por separado. Si el equipo necesita versionar todos los archivos fuente de terceros, conviertan primero el repositorio en privado y revisen la licencia de cada asset.

## Primer arranque

1. Clonar el repositorio.
2. En Unity Hub, elegir `Add > Add project from disk` y seleccionar esta carpeta.
3. Abrir con Unity `6000.0.82f1`.
4. Esperar a que termine la importacion inicial antes de tocar la escena.
5. Importar los tres assets desde `Window > Package Manager > My Assets`.
