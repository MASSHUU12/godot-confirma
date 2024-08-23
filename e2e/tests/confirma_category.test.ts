import { expect, test } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../utils";
import type { BunFile } from "bun";
import type { TestResult } from "../types/test_result";

test("Passed empty value, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: '--confirma-category' cannot be empty.",
  );
});

test("Passed empty value with '=', returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: '--confirma-category' cannot be empty.",
  );
});

test("Passed non-existing category, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=LoremIpsum",
  );

  expect(exitCode).toBe(0); // TODO: Return 1.
  expect(stderr.toString()).toContain(
    "No test classes found with category 'LoremIpsum'.",
  );
});

test("Passed valid category, runs only DummyTests", async () => {
  deleteFile(JSON_FILE_PATH);

  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=DummyTests",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TotalTests).toBe(2);
  expect(json.TestsPassed).toBe(2);

  deleteFile(JSON_FILE_PATH);
});
