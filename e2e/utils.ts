import { $ } from "bun";

export async function getProjectPath(): Promise<string> {
  return (await $`pwd`.text()).split("/").slice(0, -1).join("/");
}
