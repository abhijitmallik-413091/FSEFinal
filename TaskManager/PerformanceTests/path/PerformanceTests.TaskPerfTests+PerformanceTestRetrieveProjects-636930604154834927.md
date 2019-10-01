# PerformanceTests.TaskPerfTests+PerformanceTestRetrieveProjects
_10-05-2019 04:46:55_
### System Info
```ini
NBench=NBench, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=3
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Test
SkipWarmups=True
NumberOfIterations=5, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|    Elapsed Time |              ms |          998.00 |          990.60 |          981.00 |            6.15 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|    Elapsed Time |              ms |        1,000.92 |        1,000.00 |          999.18 |            0.67 |

### Raw Data
#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          992.00 |          999.18 |    10,00,824.29 |
|               2 |          990.00 |        1,000.10 |     9,99,900.71 |
|               3 |          992.00 |        1,000.27 |     9,99,730.54 |
|               4 |          981.00 |          999.55 |    10,00,451.68 |
|               5 |          998.00 |        1,000.92 |     9,99,078.96 |


## Benchmark Assertions

* [PASS] Expected Elapsed Time to must be less than or equal to 2,000.00 ms; actual value was 990.60 ms.

