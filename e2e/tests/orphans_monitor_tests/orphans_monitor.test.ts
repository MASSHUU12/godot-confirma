import { expect, test, afterEach, beforeAll } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../../utils";
import type { BunFile } from "bun";
import type { TestResult } from "../../types/test_result";

beforeAll(async () => {
  deleteFile(JSON_FILE_PATH);
});

afterEach(async () => {
  deleteFile(JSON_FILE_PATH);
});

test("Orphans are reported correctly.", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=OrphansMonitorTest",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TestsPassed).toBe(1);
  expect(json.TotalOrphans).toBe(5);
  expect(json.TestLogs.length).toBe(3);
});

test("Orphans are not monitored when disabled.", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=OrphansMonitorTest",
    "--confirma-disable-orphans-monitor",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TestsPassed).toBe(1);
  expect(json.TotalOrphans).toBe(0);
  expect(json.TestLogs.length).toBe(3);
});
