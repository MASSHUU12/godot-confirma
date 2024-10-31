import { afterEach, beforeAll, expect, test } from "bun:test";
import { deleteFile, getE2eTestsPath, JSON_FILE_PATH, runGodot } from "../utils";
import type { BunFile } from "bun";
import type { TestResult } from "../types/test_result";

beforeAll(async () => {
    deleteFile(JSON_FILE_PATH);
});

afterEach(async () => {
    deleteFile(JSON_FILE_PATH);
});

test("No C# tests ran, no GDScript tests", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-disable-cs",
        "--confirma-category=DummyTests",
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toContain("No test classes found with category 'DummyTests'.");
});

test("No C# tests ran, GDScript tests present", async () => {
    const e2eGdPath = getE2eTestsPath() + "/dummy_tests/";

    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-disable-cs",
        "--confirma-output=json",
        `--confirma-output-path=${JSON_FILE_PATH}`,
        `--confirma-gd-path=${e2eGdPath}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();

    const file: BunFile = Bun.file(JSON_FILE_PATH);
    expect(await file.exists()).toBeTrue();

    const json: TestResult = await file.json();
    expect(file.type).toBe("application/json;charset=utf-8");
    expect(json.TestsPassed).toBeGreaterThan(0);

    for (let log of json.TestLogs) {
        expect(log.Lang).not.toBe(0);
    }
});
