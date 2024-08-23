import { expect, test } from "bun:test";
import { runGodot } from "../utils";

test.todo("No C# tests ran", async () => {
  const { exitCode, stderr } = await runGodot(
    "--confirma-run",
    "--confirma-disable-cs",
    "--confirma-category=DummyTests",
  );

  expect(exitCode).toBe(1);
  // Or something like this
  expect(stderr.toString()).toContain("No tests found to ran.\n");
});
