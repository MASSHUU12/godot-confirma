import { expect, test } from "bun:test";
import { runGodot } from "../utils";

test("Verbose mode returns detailed information", async () => {
  const { exitCode, stdout, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-verbose",
    "--confirma-category=DummyTests",
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();
  expect(stdout.toString()).toContain(
    "\u001B[38;2;34;167;235mDummySuccessfulTest\u001B[0m...\n" +
      "| SuperTest... \u001B[38;2;142;239;151mpassed\u001B[0m.\n" +
      "| SuperPlusTest... \u001B[38;2;142;239;151mpassed\u001B[0m.\n",
  );
});
