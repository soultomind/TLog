# TLog
System.Diagnostics.Trace 래퍼 클래스   

해당 프로젝트는 간단한 패턴레이아웃과 로그레벨을 제공합니다.

## 패턴레이아웃
```
TLogger.DefaultPatternLayout = "%includefilter %level %date [%C:%M] %message%newline"
```
위와 같이 DefaultPatternLayout 속성을 통하여 출력될 패턴 레이아웃 형태를 설정 할 수 있습니다. 타입에 해당 하는 문자열에 앞에 '<strong>%</strong>' 붙여서 설정합니다.

#### ClassPatternLayout
- 로그 출력시 클래스(해당 로그 출력 호출스택 위치의 클래스)명을 제공합니다. 
  + 패턴레이아웃은 "<strong>C</strong>" 문자열이며 IsFullName 속성을 FQDN 형태로 출력할지 클래스명만 출력할지 설정 할 수 있습니다.
  ```
  /// FQDN 형태로 클래스명 출력
  TLogger.IsFullName = true;

  /// 클래스명만 출력
  TLogger.IsFullName = false;
  ```
_______________________
#### DatePatternLayout
- 로그 출력시 현재 날짜를 제공합니다.
  + 패턴레이아웃은 "<strong>date</strong>" 문자열이며 DateTimeFormat 속성을 통하여 패턴을 설정 할 수 있습니다.
  ```
  TLogger.DebugViewIncludeFilter = "yyyy-MM-dd hh:mm:ss:ffff";
  ```
_______________________
#### IncludeFilterPatternLayout
- 로그 출력시 DebugView Filter/Include 항목에서 사용될 수 있는 필터 이름을 제공합니다.   
  해당 값을 설정함으로써 DebugView 에 많은 로그 출력이 발생할때 해당 항목값을 설정해서 특정 애플리케이션에대한 로그만 추출하여 볼 수 있습니다.
  + 패턴레이아웃은 "<strong>includefilter</strong>" 문자열이며 DebugViewIncludeFilter 속성을 통하여 값을 설정 할 수 있습니다.
  ```
  TLogger.DebugViewIncludeFilter = "TLog";
  ```
_______________________
#### LevelPatternLayout
- 로그 출력시 로그레벨 값을 제공합니다. Trace 메서드를 통해 로그출력을 하였을 경우 TRACE 로그레벨 형태를 제공합니다.
  + 패턴레이아웃은 "<strong>level</strong>" 문자열입니다.
_______________________
#### MethodPatternLayout
- 로그 출력 메서드가 호출된 메서드명을 제공합니다.
  + 패턴레이아웃은 "<strong>M</strong>" 문자열입니다.
_______________________
#### MessagePatternLayout
- 출력할 메시지를 제공합니다.
  + 패턴레이아웃은 "<strong>message</strong>" 문자열입니다.
_______________________
#### NewLinePatternLayout
- 개행을 제공합니다.
  + 패턴레이아웃은 "<strong>newline</strong>" 문자열입니다.
_______________________
#### ThreadPatternLayout
- 스레드 아이디값을 제공합니다.
  + 패턴레이아웃은 "<strong>thread</strong>" 문자열이며 스레드 아이디값은 Thread.CurrentThread.ManagedThreadId 값을 사용
  ```
  Thread.CurrentThread.ManagedThreadId
  ```
_______________________

## Test Code
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TLog
{
    class Program
    {
        static void Main(string[] args)
        {
            TLogger.DefaultPatternLayout = "%includefilter %level %date [%C:%M] %message%newline";
            TLogger.DebugViewIncludeFilter = "TLog";
            TLogger.Configure(Level.Warn);

            TLogger.Trace("TRACE");
            TLogger.Debug("DEBUG");
            TLogger.Info("INFO");
            TLogger.Warn("WARN");
            TLogger.Error("ERROR");
            TLogger.Fatal("FATAL");

            System.Console.ReadKey();
        }
    }
}

```

## Output
```
TLog WARN 2022-06-21 09:25:49:2975 [TLog.Program:Main] WARN
TLog ERROR 2022-06-21 09:25:49:3039 [TLog.Program:Main] ERROR
TLog FATAL 2022-06-21 09:25:49:3069 [TLog.Program:Main] FATAL
```