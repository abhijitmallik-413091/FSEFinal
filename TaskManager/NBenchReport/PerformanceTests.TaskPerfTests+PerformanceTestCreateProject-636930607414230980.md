# PerformanceTests.TaskPerfTests+PerformanceTestCreateProject
_10-05-2019 04:52:21_
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
|    Elapsed Time |              ms |        1,162.00 |        1,054.20 |          956.00 |           73.55 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|    Elapsed Time |              ms |          999.94 |          999.91 |          999.86 |            0.04 |

### Raw Data
#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          956.00 |          999.86 |    10,00,137.76 |
|               2 |        1,037.00 |          999.93 |    10,00,068.85 |
|               3 |        1,162.00 |          999.94 |    10,00,062.05 |
|               4 |        1,053.00 |          999.86 |    10,00,139.70 |
|               5 |        1,063.00 |          999.94 |    10,00,061.62 |


## Benchmark Assertions

* [PASS] Expected Elapsed Time to must be less than or equal to 2,000.00 ms; actual value was 1,054.20 ms.

