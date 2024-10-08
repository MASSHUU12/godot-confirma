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

test("Missing lifecycle methods are reported correctly.", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=MissingLifecycleMethodsTest",
    "--confirma-output=json",
    `--confirma-output-path=${JSON_FILE_PATH}`,
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();

  const file: BunFile = Bun.file(JSON_FILE_PATH);
  expect(await file.exists()).toBeTrue();

  const json: TestResult = await file.json();
  expect(file.type).toBe("application/json;charset=utf-8");
  expect(json.TotalTests).toBe(5);
  expect(json.TestsPassed).toBe(1);
  expect(json.TestsFailed).toBe(4);
  expect(json.TotalClasses).toBe(4);
  expect(json.TestLogs.length).toBe(14);

  const expectedLogs: TestLog[] = [
    {
      Message: "MissingAfterAllTest",
      Name: null,
      State: 2,
      Type: 5,
      Lang: 0,
    },
    {
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
      Lang: -1,
    },
    {
      Message: null,
      Name: "TestCase",
      State: 0,
      Type: 4,
      Lang: 0,
    },
    {
      Message: "- Lifecycle method AfterAll not found.\n",
      Name: null,
      State: 2,
      Type: 1,
      Lang: -1,
    },
    {
      Message: "MissingTearDownTest",
      Name: null,
      State: 2,
      Type: 5,
      Lang: 0,
    },
    {
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
      Lang: -1,
    },
    {
      Message: null,
      Name: "TestCase",
      State: 0,
      Type: 4,
      Lang: 0,
    },
    {
      Message: "- Lifecycle method TearDown not found.\n",
      Name: null,
      State: 2,
      Type: 1,
      Lang: -1,
    },
    {
      Message: "MissingSetUpTest",
      Name: null,
      State: 2,
      Type: 5,
      Lang: 0,
    },
    {
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
      Lang: -1,
    },
    {
      Message: "- Lifecycle method SetUp not found.\n",
      Name: null,
      State: 2,
      Type: 1,
      Lang: -1,
    },
    {
      Message: "MissingBeforeAllTest",
      Name: null,
      State: 2,
      Type: 5,
      Lang: 0,
    },
    {
      Message: null,
      Name: null,
      State: 2,
      Type: 3,
      Lang: -1,
    },
    {
      Message: "- Lifecycle method BeforeAll not found.\n",
      Name: null,
      State: 2,
      Type: 1,
      Lang: -1,
    },
  ];

  for (let i = 0; i < json.TestLogs.length; ++i) {
    expect(json.TestLogs[i]).toEqual(expectedLogs[i]);
  }
});

test("Missing lifecycle methods with custom names are reported correctly.", async () => {});

test("Exceptions in lifecycle methods prevent tests from running.", async () => {});
