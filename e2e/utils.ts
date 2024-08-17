import { $, type ShellOutput } from "bun";

export function getProjectPath(): string {
  return Bun.main.split("/").slice(0, -3).join("/");
}

export async function runGodot(...args: string[]): Promise<ShellOutput> {
  return await $`$GODOT --path ${getProjectPath()} --headless -- ${{ raw: args.join(" ") }}`
    .nothrow()
    .quiet();
}
