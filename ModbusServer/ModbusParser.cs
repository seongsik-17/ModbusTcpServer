using System.Globalization;

namespace ModbusServer
{
	internal class ModbusParser
	{
		//폼 갱신 델리게이트
		private Action<string> _interfaceUpdate;

		public ModbusParser(Action<string> interfaceUpdate)
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
			WriteMultipleRegisters = 0x10,
			ReadWriteMultipleRegisters = 0x23,
		}

		//모드버스 프레임 파싱 및 예외 처리 담당
		public byte[] ProcessRequest(byte[] requestBuff, int requestLen)
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

			int byteCount = quantityOrValue * 2;

			//ModbusFrame frame = new ModbusFrame();
			ushort[] responseBuff = new ushort[256];
			//응답 버퍼 조립
			byte[] returnBuff = new byte[9 + byteCount];
			//기능코드에 따른 처리
			/*
			 필요 기능 정리
			 Read inupt register: 0x04
			 Read holding register: 0x03
			 Write single register: 0x06
			 Write multiple register: 0x10
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
				VirtualMemory.singleRegisterWrite(startAddress, quantityOrValue);
				byte[] writeResponse = requestBuff;
				return writeResponse;
			}
			else if (functionCode == (byte)ModbusFunctionCode.WriteMultipleRegisters)
			{
				ushort[] values = new ushort[quantityOrValue];
				int indexForMultipleReg = 13; // 데이터는 13번째 바이트부터 시작
				for (int i = 0; i < quantityOrValue; i++)
				{
					values[i] = (ushort)((requestBuff[indexForMultipleReg] << 8) | requestBuff[indexForMultipleReg + 1]);
					indexForMultipleReg += 2;
				}
				VirtualMemory.multipleRegisterWrite(startAddress, values);
				byte[] writeResponse = requestBuff;
				return writeResponse;
			}
			else
			{
				//지원하지 않는 기능코드
			}

			//그냥 돌려주는게 아니라 응답 프레임을 만들어 줘야지
			Array.Copy(requestBuff, 0, returnBuff, 0, 8);
			returnBuff[8] = (byte)byteCount;
			int dataIndex = 9;
			for (int i = 0; i < quantityOrValue; i++)
			{
				returnBuff[dataIndex] = (byte)(responseBuff[i] >> 8);       // High Byte
				returnBuff[dataIndex + 1] = (byte)(responseBuff[i] & 0xFF); // Low Byte
				dataIndex += 2;
			}
			int length = 3 + byteCount;

			returnBuff[4] = (byte)(length >> 8);
			returnBuff[5] = (byte)(length & 0xFF);

			return returnBuff;
		}
	}
}