import { expect, test, afterEach } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../utils";
import type { BunFile } from "bun";

afterEach(async (): Promise<void> => {
    await deleteFile(JSON_FILE_PATH);
});

test("Passed empty output type, returns with error", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-output cannot be empty.",
    );
    expect(await Bun.file(JSON_FILE_PATH).exists()).toBeFalse();
});

test("Passed empty output type with '=', returns with error", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output=",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-output cannot be empty.",
    );
    expect(await Bun.file(JSON_FILE_PATH).exists()).toBeFalse();
});

test("Passed 'log' output type, no JSON created", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output=log",
        `--confirma-output-path=${JSON_FILE_PATH}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();
    expect(await Bun.file(JSON_FILE_PATH).exists()).toBeFalse();
});

test("Passed 'json' output type, JSON created", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output=json",
        `--confirma-output-path=${JSON_FILE_PATH}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();

    const file: BunFile = Bun.file(JSON_FILE_PATH);
    expect(await file.exists()).toBeTrue();

    await file.json();
    expect(file.type).toBe("application/json;charset=utf-8");
});

test("Passed 'log,json' output type, JSON created", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output=log,json",
        `--confirma-output-path=${JSON_FILE_PATH}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();
    expect(await Bun.file(JSON_FILE_PATH).exists()).toBeTrue();
});

test("Passed invalid output type, returns with error", async (): Promise<void> => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-output=xml",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Invalid value 'xml' for '--confirma-output' argument.",
    );
    expect(await Bun.file(JSON_FILE_PATH).exists()).toBeFalse();
});
