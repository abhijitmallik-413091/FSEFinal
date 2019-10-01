# PerformanceTests.TaskPerfTests+PerformanceTestCreateProject
_10-05-2019 04:47:05_
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
|    Elapsed Time |              ms |        1,079.00 |        1,052.00 |        1,027.00 |           23.23 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|    Elapsed Time |              ms |        1,000.71 |        1,000.30 |          999.45 |            0.50 |

### Raw Data
#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,079.00 |          999.45 |    10,00,549.49 |
|               2 |        1,027.00 |        1,000.38 |     9,99,619.28 |
|               3 |        1,074.00 |        1,000.58 |     9,99,415.92 |
|               4 |        1,044.00 |        1,000.35 |     9,99,646.26 |
|               5 |        1,036.00 |        1,000.71 |     9,99,289.09 |


## Benchmark Assertions

* [PASS] Expected Elapsed Time to must be less than or equal to 2,000.00 ms; actual value was 1,052.00 ms.

