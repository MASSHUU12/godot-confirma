import { expect, test } from "bun:test";
import { deleteFile, runGodot } from "../utils";

test("Verbose mode returns detailed information", async () => {
  const { exitCode, stdout, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-verbose",
    "--confirma-category=DummyTests",
  );

  expect(exitCode).toBe(0);
  expect(stderr.toString()).toBeEmpty();
  expect(stdout.toString()).toContain(
    "\u001B[38;2;155;73;147m[C#]\u001B[0m " +
      "\u001B[38;2;33;138;114mDummySuccessfulTest\u001B[0m...\n " +
      "\u001B[38;2;155;73;147m|\u001B[0m SuperTest... " +
      "\u001B[38;2;142;239;151mpassed\u001B[0m.\n " +
      "\u001B[38;2;155;73;147m|\u001B[0m SuperPlusTest... " +
      "\u001B[38;2;142;239;151mpassed",
  );
  expect(stdout.toString()).toContain(
    "\u001B[0m.\n\nConfirma ran 2 tests in 1 test classes. ",
  );
  expect(stdout.toString()).toContain(
    "\n\u001B[38;2;142;239;151m2 passed" +
      "\u001B[0m, \u001B[38;2;255;120;107m0 failed\u001B[0m, " +
      "\u001B[38;2;255;221;101m0 ignored\u001B[0m, " +
      "\u001B[38;2;255;221;101m0 orphans, " +
      "\u001B[0m\u001B[38;2;255;221;101m0 warnings\u001B[0m.\n",
  );
});
