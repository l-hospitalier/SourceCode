unit Thread1;
{$mode delphi}
{$H+}

interface
uses
  Classes;

type

  TMyThread = class(TThread)
  public
    constructor Create(CreateSuspended:Boolean);
  private
    { Private 宣言 }
    procedure Disp_1;
  protected
    procedure Execute; override;
  end;

implementation
  uses Windows, Metex2U, SysUtils;

constructor TMyThread.Create(CreateSuspended: boolean);
begin
  inherited Create(True);
  FreeOnTerminate:=True;
end;

procedure TMyThread.Disp_1;
 var   k: byte;
       St1, St2, St3: String;
       RdData: Array[1..11] of Char;
begin
    ReadFile(hCommFileHandle, RdData, 11, ReadNum, @WriteOverLap);

    for k:=1 to 11 do St1:=St1+Char(RdData[k]);
        Delete(St1, 6, 7);

        if (Form1.RadioButton1.Checked) then
            begin
               delete(St1, 1, 2);
               St2:=St1;
               delete(St2, 1, 2);
               St3:=St1;
               delete(St3, 3, 1);
               St1:=St3 + '.' +St2;
               Form1.Memo1.Append(St1+' db');
             end;
        if (Form1.RadioButton2.Checked) then
             begin
               delete(St1, 1, 2);
               Form1.Memo1.Append(St1+' lux');
             end;
        sleep(450);
end;

procedure TMyThread.Execute;
begin
  { ToDo : スレッドとして実行したいコードをこの下に記述してください }
  repeat
    sleep(45);
    Synchronize(Disp_1)
  until terminated;
end;

end.
