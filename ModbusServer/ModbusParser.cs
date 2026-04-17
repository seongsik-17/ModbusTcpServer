using System.Globalization;

namespace ModbusServer
{
	internal class ModbusParser
	{
		//폼 갱신 델리게이트
		private Action<string> _interfaceUpdate;
		//private delegate void funcasf(StringInfo a);
		//funcasf fff;
		//private Func<string fgggg, string, string, string> a;
		ModbusParser(Action<string> interfaceUpdate) 
		{ 
			this._interfaceUpdate = interfaceUpdate;
		}
		private enum ModbusFunctionCode
		{
			ReadCoils = 0x01,
			ReadDiscreteInputs = 0x02,
			ReadHoldingRegisters = 0x03,
			ReadInputRegisters = 0x04,
			WriteSingleCoil = 0x05,
			WriteSingleRegister = 0x06,
			WriteMultipleRegisters = 0x16,
			ReadWriteMultipleRegisters = 0x23,
		}

		//모드버스 프레임 파싱 및 예외 처리 담당
		public ushort[] ProcessRequest(byte[] requestBuff, int requestLen)
		{
			//들어온 버퍼 테스트 출력용
			//_interfaceUpdate($"수신된 요청: {BitConverter.ToString(requestBuff, 0, requestLen)}");

			//파서에서 필요한 데이터 목록: 기능코드, 시작주소, 개수 또는 쓸 값
			//기능코드
			ushort functionCode = requestBuff[7];
			//시작주소
			ushort startAddress = (ushort)((requestBuff[8] << 8) | requestBuff[9]);
			//개수 또는 쓸 값
			ushort quantityOrValue = (ushort)((requestBuff[10] << 8) | requestBuff[11]);

			//ModbusFrame frame = new ModbusFrame();
			ushort[] responseBuff = new ushort[256];

			//기능코드에 따른 처리
			/*
			 필요 기능 정리
			 Read inupt register: 0x04
			 Read holding register: 0x03
			 Write single register: 0x06
			 Write multiple register: 0x16
			 */
			if (functionCode == (byte)ModbusFunctionCode.ReadInputRegisters)
			{
				responseBuff = VirtualMemory.inputRegisterRead((byte)startAddress, (byte)quantityOrValue);
			}
			else if (functionCode == (byte)ModbusFunctionCode.ReadHoldingRegisters)
			{
				responseBuff = VirtualMemory.holdingRegisterRead((byte)startAddress, (byte)quantityOrValue);
			}
			else if (functionCode == (byte)ModbusFunctionCode.WriteSingleRegister)
			{
				VirtualMemory.singleRegisterWrite();
			}
			else if (functionCode == (byte)ModbusFunctionCode.WriteMultipleRegisters)
			{
				VirtualMemory.multipleRegisterWrite();
			}
			return responseBuff;
		}
	}
}