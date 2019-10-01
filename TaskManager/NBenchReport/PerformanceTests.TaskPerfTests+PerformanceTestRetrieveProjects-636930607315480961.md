# PerformanceTests.TaskPerfTests+PerformanceTestRetrieveProjects
_10-05-2019 04:52:11_
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
|    Elapsed Time |              ms |        2,316.00 |        1,283.20 |          973.00 |          582.77 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|    Elapsed Time |              ms |        1,000.00 |          999.84 |          999.63 |            0.15 |

### Raw Data
#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          988.00 |          999.82 |    10,00,181.98 |
|               2 |          973.00 |          999.63 |    10,00,372.87 |
|               3 |          977.00 |          999.79 |    10,00,206.14 |
|               4 |        1,162.00 |          999.97 |    10,00,032.36 |
|               5 |        2,316.00 |        1,000.00 |     9,99,997.67 |


## Benchmark Assertions

* [PASS] Expected Elapsed Time to must be less than or equal to 2,000.00 ms; actual value was 1,283.20 ms.

