import type { TestLog } from "./test_log";

export interface TestResult {
  TotalTests: number;
  TestsPassed: number;
  TestsFailed: number;
  TestsIgnored: number;
  TotalOrphans: number;
  TotalClasses: number;
  TotalTime: number;
  Warnings: number;
  TestLogs: TestLog[];
}
