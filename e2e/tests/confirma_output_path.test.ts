import { expect, test } from "bun:test";
import { getProjectPath, runGodot } from "../utils";

test("Passed empty value, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-output-path",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain("Invalid output path: .");
});

test("Passed valid path, stderr empty", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    `--confirma-output-path=${getProjectPath()}/result.json`,
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
