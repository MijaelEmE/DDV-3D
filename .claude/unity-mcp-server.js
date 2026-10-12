#!/usr/bin/env node

const fs = require("fs");
const path = require("path");

const projectPath = process.env.UNITY_PROJECT_PATH || ".";
const assetsPath = path.join(projectPath, "Assets", "_Project");

// Herramientas disponibles
const tools = [
  {
    name: "list_scripts",
    description: "Lista todos los scripts C# en Assets/_Project/Scripts",
    inputSchema: {
      type: "object",
      properties: {},
      required: [],
    },
  },
  {
    name: "read_script",
    description: "Lee el contenido de un script C#",
    inputSchema: {
      type: "object",
      properties: {
        filename: {
          type: "string",
          description: "Nombre del archivo (ej: AltarInteractable.cs)",
        },
      },
      required: ["filename"],
    },
  },
  {
    name: "list_materials",
    description: "Lista todos los materiales en Assets/_Project/Materials",
    inputSchema: {
      type: "object",
      properties: {},
      required: [],
    },
  },
  {
    name: "list_prefabs",
    description: "Lista todos los prefabs en Assets/_Project/Prefabs",
    inputSchema: {
      type: "object",
      properties: {},
      required: [],
    },
  },
  {
    name: "project_summary",
    description: "Resume la estructura del proyecto",
    inputSchema: {
      type: "object",
      properties: {},
      required: [],
    },
  },
];

// Procesar herramientas
function processToolCall(toolName, toolInput) {
  switch (toolName) {
    case "list_scripts":
      return listScripts();
    case "read_script":
      return readScript(toolInput.filename);
    case "list_materials":
      return listMaterials();
    case "list_prefabs":
      return listPrefabs();
    case "project_summary":
      return projectSummary();
    default:
      return { error: `Herramienta desconocida: ${toolName}` };
  }
}

function listScripts() {
  const scriptsPath = path.join(assetsPath, "Scripts");
  if (!fs.existsSync(scriptsPath)) {
    return { error: "Carpeta Scripts no encontrada" };
  }

  const files = fs
    .readdirSync(scriptsPath)
    .filter((f) => f.endsWith(".cs"))
    .sort();

  return {
    scripts: files,
    count: files.length,
    path: scriptsPath,
  };
}

function readScript(filename) {
  const filePath = path.join(assetsPath, "Scripts", filename);

  if (!fs.existsSync(filePath)) {
    return { error: `Script no encontrado: ${filename}` };
  }

  const content = fs.readFileSync(filePath, "utf-8");
  return {
    filename,
    content,
    lines: content.split("\n").length,
  };
}

function listMaterials() {
  const materialsPath = path.join(assetsPath, "Materials");
  if (!fs.existsSync(materialsPath)) {
    return { materials: [], count: 0 };
  }

  const files = fs
    .readdirSync(materialsPath, { withFileTypes: true })
    .filter((f) => !f.name.endsWith(".meta"))
    .map((f) => ({
      name: f.name,
      type: f.isDirectory() ? "folder" : "file",
    }))
    .sort();

  return {
    materials: files,
    count: files.length,
    path: materialsPath,
  };
}

function listPrefabs() {
  const prefabsPath = path.join(assetsPath, "Prefabs");
  if (!fs.existsSync(prefabsPath)) {
    return { prefabs: [], count: 0 };
  }

  const files = fs
    .readdirSync(prefabsPath)
    .filter((f) => f.endsWith(".prefab"))
    .sort();

  return {
    prefabs: files,
    count: files.length,
    path: prefabsPath,
  };
}

function projectSummary() {
  return {
    unityVersion: "6000.0.82f1",
    renderPipeline: "Built-in",
    projectStructure: {
      scripts: listScripts(),
      materials: listMaterials(),
      prefabs: listPrefabs(),
    },
    branch: "feature/gameplay",
    baseBranch: "main",
  };
}

// Entrada principal (simulación de protocolo MCP)
async function main() {
  // Para propósitos de demostración, exponemos las herramientas
  console.log("Herramientas disponibles:");
  tools.forEach((tool) => {
    console.log(`- ${tool.name}: ${tool.description}`);
  });

  // Test: mostrar resumen del proyecto
  console.log("\n=== RESUMEN DEL PROYECTO ===");
  console.log(JSON.stringify(projectSummary(), null, 2));
}

main().catch(console.error);
