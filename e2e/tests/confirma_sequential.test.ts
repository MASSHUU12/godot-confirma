import { expect, test, afterAll } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../utils";
import type { TestResult } from "../types/test_result";
import type { BunFile } from "bun";
import type { TestLog } from "../types/test_log";

afterAll(async () => {
  deleteFile(JSON_FILE_PATH);
});

test("The tests are run sequentially in the same order.", async () => {
  deleteFile(JSON_FILE_PATH);

  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-sequential",
    "--confirma-category=DummySequentialTests",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TotalTests).toBe(6);
  expect(json.TestsPassed).toBe(6);
  expect(json.TotalClasses).toBe(2);
  expect(json.TestLogs.length).toBe(10);

  const expectedLogs: TestLog[] = [
    {
      Lang: 0,
      Message: "DummySequential2Test",
      Name: null,
      State: 2,
      Type: 5,
    },
    {
      Lang: -1,
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperTest",
      State: 0,
      Type: 4,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperExtraTest",
      State: 0,
      Type: 4,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperExtraProTest",
      State: 0,
      Type: 4,
    },
    {
      Lang: 0,
      Message: "DummySequentialTest",
      Name: null,
      State: 2,
      Type: 5,
    },
    {
      Lang: -1,
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperTest",
      State: 0,
      Type: 4,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperExtraTest",
      State: 0,
      Type: 4,
    },
    {
      Lang: 0,
      Message: null,
      Name: "SuperExtraProTest",
      State: 0,
      Type: 4,
    },
  ];

  for (let i = 0; i < json.TestLogs.length; ++i) {
    expect(json.TestLogs[i]).toEqual(expectedLogs[i]);
  }
});
