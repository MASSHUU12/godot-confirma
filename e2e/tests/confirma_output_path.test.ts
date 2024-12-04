import { expect, test } from "bun:test";
import { JSON_FILE_PATH, runGodot } from "../utils";

test("Passed empty value, returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output-path",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-output-path cannot be empty.",
    );
});

test("Passed empty value with '=', returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output-path=",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-output-path cannot be empty.",
    );
});

test("Passed valid path, stderr empty", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        `--confirma-output-path=${JSON_FILE_PATH}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();
});

test("Passed invalid path, returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output-path=invalid",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain("Invalid output path: invalid.");
});

test("Passed valid, non-existing path, returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output-path=./lorem/ipsum.json",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Invalid output path: ./lorem/ipsum.json.",
    );
});
