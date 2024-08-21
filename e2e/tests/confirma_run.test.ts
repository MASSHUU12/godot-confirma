import { expect, test } from "bun:test";
import { runGodot } from "../utils";

test("Passed empty value, runs all tests", async () => {
  const { exitCode, stderr } = await runGodot("--confirma-run");

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();
});

test("Passed empty value with '=', runs all tests", async () => {
  const { exitCode, stderr } = await runGodot("--confirma-run=");

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();
});

test("Passed invalid class name, returns with error", async () => {
  const { exitCode, stderr } = await runGodot("--confirma-run=LoremIpsum");

  expect(exitCode).toBe(0); // TODO: Return 1.
  expect(stderr.toString()).toContain(
    "No test class found with the name 'LoremIpsum'.",
  );
});

test("Passed valid class name, returns with results", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run=DummySuccessfulTest",
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();
});
