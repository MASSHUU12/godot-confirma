import { expect, test, beforeEach, afterEach } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../utils";
import type { TestResult } from "../types/test_result";
import type { BunFile } from "bun";

beforeEach(async () => {
  deleteFile(JSON_FILE_PATH);
});

afterEach(async () => {
  deleteFile(JSON_FILE_PATH);
});

test("Empty '--confirma-run' and '--confirma-method', returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-method",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: argument '--confirma-run' cannot be empty" +
      " when using argument '--confirma-method'.\n",
  );
});

test("Empty '--confirma-method', returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run=DummySuccessfulTest",
    "--confirma-method",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: '--confirma-method' cannot be empty.\n",
  );
});

test("Invalid method name, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run=DummySuccessfulTest",
    "--confirma-method=ExtraSuperTest",
  );

  expect(exitCode).toBe(0); // TODO: Return 1.
  expect(stderr.toString()).toContain(
    "No test methods found with the name ExtraSuperTest.",
  );
});

test("Valid method and class name, runs successfully", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run=DummySuccessfulTest",
    "--confirma-method=SuperTest",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TotalTests).toBe(1);
  expect(json.TestsPassed).toBe(1);
});
