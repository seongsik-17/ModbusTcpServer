# C# Modbus TCP Server for MES

C# `System.Net.Sockets`를 기반으로 자체 구현한 경량화된 다중 접속 Modbus TCP 서버입니다. 스마트 팩토리 및 MES 환경에서 설비(PLC, 센서)와의 데이터 연동을 목적으로 설계되었습니다. 외부 라이브러리 의존성 없이 프로토콜 파싱과 메모리 관리를 직접 구현했습니다.

## 주요 기능

* **다중 접속 처리:** `Task.Run`을 활용한 비동기 스레드 기반 멀티 클라이언트 동시 접속 지원.
* **가상 메모리 제어:** `ushort` 기반의 16비트 레지스터 가상 메모리 구현 및 Thread-Safe 데이터 접근 구조 적용.
* **아키텍처 분리:** `Action` 델리게이트를 통해 통신 엔진과 UI(WinForms/WPF 등) 계층의 결합도 최소화.
* **데이터 포맷 및 엔디안 대응:** Float32 처리를 위한 배율(Scale Factor) 적용 및 바이트 오더(Word Swap 등) 변환 로직 지원.

## 프로젝트 구조

* `TcpServer`: 서버 구동 대기 및 클라이언트 연결(Accept) 관리
* `ClientHandler`: 개별 클라이언트의 패킷 수신 및 응답을 전담하는 백그라운드 작업
* `ModbusParser`: 바이트 배열 내 Function Code 및 메모리 주소 파싱
* `VirtualMemory`: 서버 데이터 읽기/쓰기를 담당하는 내부 레지스터 배열

### 데이터 모델
* `MachineConfiguration`: 제어 설정값 (목표 온도, PID 제어 게인 등)
* `MachineStatus`: 실시간 상태값 (현재 온도, 히팅 작동 상태 등)

## 테스트 가이드 (Modbus Poll)

범용 모드버스 클라이언트 프로그램을 통한 테스트 시 아래 설정을 권장합니다.

1. **주소 체계:** `Display` -> `Protocol Addresses (Base 0)` 적용 (서버 배열 인덱스와 직관적 일치)
2. **실수형 표기:** `Format` -> `Float Inverse` 설정 (리틀 엔디안 기반의 Float32 데이터 정상 출력)
