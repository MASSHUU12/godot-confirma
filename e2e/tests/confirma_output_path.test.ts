import { expect, test } from "bun:test";
import { $ } from "bun";
import { getProjectPath } from "../utils";

// TODO: Find a better solution
const PATH = await getProjectPath();

test("Passed empty value", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.text()).toContain("Invalid output path: .");
});

test("Passed valid path", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=${PATH}/result.json`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(0);
});

test("Passed invalid path", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=invalid`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.text()).toContain("Invalid output path: invalid.");
});

test("Passed valid, non-existing path", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=./lorem/ipsum.json`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.text()).toContain("Invalid output path: ./lorem/ipsum.json.");
});
