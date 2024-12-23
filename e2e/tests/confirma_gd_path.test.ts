import { expect, test } from "bun:test";
import { getE2eTestsPath, runGodot } from "../utils";

test("Passed empty path, returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-gd-path",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-gd-path cannot be empty.",
    );
});

test("Passed valid path", async () => {
    const gdVoid = getE2eTestsPath() + "/void/";

    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        `--confirma-gd-path=${gdVoid}`,
    );

    expect(exitCode).toBe(0);
    expect(stderr.toString()).toBeEmpty();
});

test("Passed invalid path, returns with error", async () => {
    const { exitCode, stderr } = await runGodot(
        "--confirma-run",
        "--confirma-gd-path",
    );

    expect(exitCode).toBe(1);
    expect(stderr.toString()).toContain(
        "Value for --confirma-gd-path cannot be empty.",
    );
});
