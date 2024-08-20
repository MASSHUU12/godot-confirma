import { expect, test } from "bun:test";
import { runGodot } from "../utils";

test("Passed empty value, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: '--confirma-category' cannot be empty.",
  );
});

test("Passed empty value with '=', returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=",
  );

  expect(exitCode).toBe(1);
  expect(stderr.toString()).toContain(
    "Invalid value: '--confirma-category' cannot be empty.",
  );
});

test("Passed non-existing category, returns with error", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-category=LoremIpsum",
  );

  expect(exitCode).toBe(0); // TODO: Return 1.
  expect(stderr.toString()).toContain(
    "No test classes found with category 'LoremIpsum'.",
  );
});

test.todo("Passed valid category", () => {
  expect(false).toBeTrue();
});
