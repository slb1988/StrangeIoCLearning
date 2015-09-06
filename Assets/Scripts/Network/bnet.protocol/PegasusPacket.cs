using System;
using System.IO;
public class PegasusPacket : PacketFormat
{
	private const int TYPE_BYTES = 4;
	private const int SIZE_BYTES = 4;
	public int Size;
	public int Type;
	public int Context;
	public object Body;
	private bool sizeRead;
	private bool typeRead;
	public PegasusPacket()
	{
	}
	public PegasusPacket(int type, int context, object body)
	{
		this.Type = type;
		this.Context = context;
		this.Size = -1;
		this.Body = body;
	}
	public PegasusPacket(int type, int context, int size, object body)
	{
		this.Type = type;
		this.Context = context;
		this.Size = size;
		this.Body = body;
	}
	public object GetBody()
	{
		return this.Body;
	}
	public override bool IsLoaded()
	{
		return this.Body != null;
	}
	public override int Decode(byte[] bytes, int offset, int available)
	{
		string arg = string.Empty;
		int num = 0;
		while (num < 8 && num < available)
		{
			arg = arg + bytes[offset + num] + " ";
			num++;
		}
		int num2 = 0;
		if (!this.typeRead)
		{
			if (available < 4)
			{
				return num2;
			}
			this.Type = BitConverter.ToInt32(bytes, offset);
			this.typeRead = true;
			available -= 4;
			num2 += 4;
			offset += 4;
		}
		if (!this.sizeRead)
		{
			if (available < 4)
			{
				return num2;
			}
			this.Size = BitConverter.ToInt32(bytes, offset);
			this.sizeRead = true;
			available -= 4;
			num2 += 4;
			offset += 4;
		}
		if (this.Body == null)
		{
			if (available < this.Size)
			{
				return num2;
			}
			byte[] array = new byte[this.Size];
			Array.Copy(bytes, offset, array, 0, this.Size);
			this.Body = array;
			num2 += this.Size;
		}
		return num2;
	}
	public override byte[] Encode()
	{
		if (this.Body is IProtoBuf)
		{
			IProtoBuf protoBuf = (IProtoBuf)this.Body;
			this.Size = (int)protoBuf.GetSerializedSize();
			byte[] array = new byte[this.Size + 4 + 4];
			Array.Copy(BitConverter.GetBytes(this.Type), 0, array, 0, 4);
			Array.Copy(BitConverter.GetBytes(this.Size), 0, array, 4, 4);
			protoBuf.Serialize(new MemoryStream(array, 8, this.Size));
			return array;
		}
		return null;
	}
}
