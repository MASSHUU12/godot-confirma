import type { LogType } from "./log_type";
import type { TestCaseState } from "./test_case_state";

export interface TestLog {
  Message: string;
  Name: string;
  State: TestCaseState;
  Type: LogType;
}
