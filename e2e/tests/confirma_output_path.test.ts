import { expect, test } from "bun:test";
import { $ } from "bun";
import { getProjectPath } from "../utils";

// TODO: Find a better solution
const PATH = await getProjectPath();

test("Passed empty value, returns with error", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.stderr.toString()).toContain("Invalid output path: .");
});

test("Passed valid path, stderr empty", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=${PATH}/result.json`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(0);
  expect(out.stderr.toString()).toBeEmpty();
});

test("Passed invalid path, returns with error", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=invalid`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.stderr.toString()).toContain("Invalid output path: invalid.");
});

test("Passed valid, non-existing path, returns with error", async () => {
  const out =
    await $`$GODOT --path ${PATH} --headless -- --confirma-run --confirma-output-path=./lorem/ipsum.json`
      .nothrow()
      .quiet();

  expect(out.exitCode).toBe(1);
  expect(out.stderr.toString()).toContain(
    "Invalid output path: ./lorem/ipsum.json.",
  );
});
