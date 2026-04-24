namespace ModbusServer
{
	public static class VirtualMemory
	{
		//인풋은 읽기만 가능, 홀딩은 읽기, 쓰기 둘다 가능(30000, 40000)
		private static ushort[] _holdingRegisters = new ushort[100];

		private static ushort[] _inputRegisters = new ushort[100];
		private static bool[] _coils = new bool[100];
		private static bool[] _discreteInputs = new bool[100];

		//데이터 변경 이벤트 헨들러
		public static event Action<int, ushort> OnChangeEv;

		//데이터 동시 변경 방지 -> lock 객체
		private static readonly object _lock = new object();

		static VirtualMemory()
		{
			LoadMemory();
		}

		//메모리 값 파일에서 읽어오는 로직
		public static void LoadMemory()
		{
			//가상 메모리 설정
			lock (_lock)
			{
				// 1. 고정값 테스트 (설정값 등)
				_holdingRegisters[0] = 100; // 목표 온도
				_holdingRegisters[1] = 500; // 회전 속도
				_inputRegisters[0] = 150; // 현재 온도
				_inputRegisters[1] = 450; // 현재 회전 속도

				// 2. 일련번호 테스트 (배열 순서 확인용)
				for (int i = 10; i < 20; i++)
				{
					_holdingRegisters[i] = (ushort)(i * 10);
				}
			}
		}

		//인풋레지스터 읽기
		public static ushort[] inputRegisterRead(ushort startAddress, ushort quantity)
		{
			ushort[] result = new ushort[quantity];
			lock (_lock)
			{
				for (int i = 0; i < quantity; i++)
				{
					result[i] = _inputRegisters[startAddress + i];
				}
			}
			return result;
		}

		//홀딩레지스터 읽기
		public static ushort[] holdingRegisterRead(ushort startAddress, ushort quantity)
		{
			ushort[] result = new ushort[quantity];
			lock (_lock)
			{
				for (int i = 0; i < quantity; i++)
				{
					result[i] = _holdingRegisters[startAddress + i];
				}
			}
			return result;
		}

		internal static void singleRegisterWrite(ushort address, ushort value)
		{
			lock (_lock)
			{
				/*	들어오는 코드
					Function code: 0x06 1byte
					Register address(Hi) 2byte
					Register address(Lo) 2byte
					Register value(Hi) 2byte
					Register value(Lo) 2byte

				 */
				if (address < _holdingRegisters.Length)
				{
					//값 변경 로직
					_holdingRegisters[address] = value;
				}
				else
				{
					//주소 범위 벗어남 -> 예외 처리 구성 필요
				}
				//변경 이벤트 발생
				//OnChangeEv?.Invoke(address, value);
			}
			
		}

		internal static void multipleRegisterWrite(ushort address, ushort[] values)
		{
			

			lock (_lock)
			{
				if(address + values.Length > _holdingRegisters.Length)
				{
					
				}
				for (int i = 0; i < values.Length; i++)
				{
					_holdingRegisters[address + i] = values[i];
				}

				//변경 이벤트 발생
				//OnChangeEv?.Invoke(0, 0);
			}
			
		}
	}
}