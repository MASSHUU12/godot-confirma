import { $, type ShellOutput } from "bun";

export const JSON_FILE_NAME = "results.json";
export const TESTS_PATH = getE2eTestsPath() + "/";
export const JSON_FILE_PATH = TESTS_PATH + JSON_FILE_NAME;

export function getProjectPath(): string {
  return Bun.main.split("/").slice(0, -3).join("/");
}

export function getE2eTestsPath(): string {
  return Bun.main.split("/").slice(0, -1).join("/");
}

export async function runGodot(...args: string[]): Promise<ShellOutput> {
  return await $`$GODOT --path ${getProjectPath()} --headless -- ${{ raw: args.join(" ") }}`
    .nothrow()
    .quiet();
}
