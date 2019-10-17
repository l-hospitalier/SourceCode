unit Metex2U;
{$MODE DELPHI}
{$H+}
interface
uses
  Windows, Classes, SysUtils, FileUtil, Forms, Controls, Graphics, Dialogs,
  StdCtrls, Menus, ExtCtrls, Thread1;

type
  { TForm1 }

  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Label1: TLabel;
    Label2: TLabel;
    Memo1: TMemo;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormShow(Sender: TObject);

  private
    { Private 宣言 }
  public
      { Public 宣言 }
  end;

Var
  Form1: TForm1;

  CommPortName: PChar;
  CommSpeed: Integer;

  ModemStatus : DWord;
  hCommFileHandle : THandle;

  TimeOut: TCommTimeOuts;
  WriteNum, ReadNum: Dword;
  WriteOverLap: TOverLapped;

  i:Integer;
  //j:Integer;
  First: Boolean;

  dcb: Tdcb;
  WtData: Array[1..11] of Char;
  MyThreadInstance:TMyThread;

implementation
{$R *.lfm}

procedure SetDCBBlock(BaudRate, StopBits, Parity, ByteSize, Flags:integer);
begin
  GetCommState(hCommFileHandle, dcb);

  dcb.BaudRate:=BaudRate;
  dcb.StopBits:=StopBits;     // 0: 1 Stop Bit 1: 1.5 Stop Bit 2: 2 StopBits
  dcb.Parity:=Parity;       // 0: Non Parity
  dcb.ByteSize:=ByteSize;     // 8: 8 bit data size
  dcb.Flags:=Flags;    // 1: Flags:=$1011 or $0011;

  SetCommState(hCommFileHandle, dcb);
end;

procedure SendCommBreak(interval: integer);
begin
    SetCommBreak(hCommFileHandle);            // BREAK
    sleep(interval);                          // 待機
    ClearCommBreak(hCommFileHandle);          // BREAKの停止
end;

procedure Init;
begin
    CommPortName:='COM1';
    CommSpeed:=19200;
    First:=TRUE;

    //PortClose();
    //ポートをオープンする
    hCommFileHandle:=CreateFile(
		CommPortName,
		GENERIC_READ or GENERIC_WRITE,
		0,
                { FILE_SHARE_READ or FILE_SHARE_WRITE }
		nil,
		OPEN_EXISTING, //OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL or FILE_FLAG_OVERLAPPED,
		0);

	if hCommFileHandle=INVALID_HANDLE_VALUE then
	begin
		ShowMessage('回線オープンエラー');
		PurgeComm(hCommFileHandle, PURGE_TXABORT or PURGE_RXABORT);
                CloseHandle(hCommFileHandle);
                Exit;
	end;

    //ＰＣのＣＯＭポートを19200bps, 7bit, odd-parity, 1 stopbitに設定

    SetUpComm(hCommFileHandle, 8, 8);
    SetDCBBlock(CommSpeed,0,1,7,3);

    EscapeCommFunction(hCommFileHandle, SETRTS); // 動作時 RTS, DTR セット
    EscapeCommFunction(hCommFileHandle, SETDTR); // 動作時 RTS, DTR セット

    sleep(100);
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
   SendCommBreak(100);  // RS mode
end;

procedure TForm1.Button2Click(Sender: TObject);
// DIO.DLL使用時ここをSmallInt, Wordにすると初期化に失敗する。
             // ===>> Byte, Integer, LongIntならＯＫ, 今回はWin32API使用
begin
    MyThreadInstance.Suspended:=not MyThreadInstance.Suspended;
//   if ThreadInstance.Suspended=True then ThreadInstance.Start;
end;

procedure TForm1.FormShow(Sender: TObject);
begin
    Form1.Label1.Caption:='Port = ' + CommPortName;
    Form1.Label2.Caption:='Speed = '+ IntToStr(CommSpeed);
end;

Initialization
  Init;
  MyThreadInstance:=TMyThread.Create(True);
  MyThreadInstance.FreeOnTerminate:=True;

Finalization
  MyThreadInstance.Terminate;

end.
