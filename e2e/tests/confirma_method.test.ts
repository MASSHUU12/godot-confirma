import { expect, test, beforeEach, afterEach } from "bun:test";
import { deleteFile, JSON_FILE_PATH, runGodot } from "../utils";

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
