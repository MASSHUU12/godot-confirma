import { expect, test, afterEach } from "bun:test";
import { unlink } from "node:fs/promises";
import { getE2eTestsPath, JSON_FILE_PATH, runGodot } from "../utils";

afterEach(async (): Promise<void> => {
  if (await Bun.file(JSON_FILE_PATH).exists()) {
    await unlink(JSON_FILE_PATH);
  }
});

test("Passed empty output type, returns with error", async (): Promise<void> => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-output",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value '' for '--confirma-output' argument.",
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
    "Invalid value '' for '--confirma-output' argument.",
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
  expect(await Bun.file(JSON_FILE_PATH).exists()).toBeTrue();
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
