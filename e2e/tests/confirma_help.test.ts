import { expect, test } from "bun:test";
import { runGodot } from "../utils";

test("Passed nothing, returns help page", async() => {
    const { exitCode, stderr, stdout} = await runGodot(
        "--confirma-help",
      );

    expect(exitCode).toBe(0);
    expect(stdout.toString()).toContain("Arguments");
    expect(stderr.toString()).toBeEmpty();
});
test("Passed nonexisting page, returns with error", async() => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-help=lorem",
      );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain("Page: `lorem`, not found or failed to load");
});
test("Passed existing page, returns help page", async() => {
    const { exitCode, stderr, stdout } = await runGodot(
        "--confirma-help=default", //todo change to non-default page
      );

    expect(exitCode).toBe(0);
    expect(stdout.toString()).toContain("Arguments");
    expect(stderr.toString()).toBeEmpty();
});
