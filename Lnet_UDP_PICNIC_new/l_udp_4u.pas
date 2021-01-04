// TriState new PI-NIC用  Lazarus 2.10  UDP A/D, paralell port LCD制御　プログラム
// IP 192.168.1.9  port LCD = 10000, parallel = 10001,
unit L_UDP_4U;
{$mode objfpc}{$H+}{$CodePage UTF8}
interface
uses
  Classes, SysUtils, FileUtil, LResources, Forms, Controls, Graphics, Dialogs,
  ExtCtrls, StdCtrls, lNetComponents, lNet; //, Windows;
type
  { TForm1 }
  TForm1 = class(TForm)
    Button10: TButton;
    Button6: TButton;
    Button7: TButton;
    Button8: TButton;
    CheckBox1: TCheckBox;
    CheckBox10: TCheckBox;
    CheckBox11: TCheckBox;
    CheckBox12: TCheckBox;
    CheckBox13: TCheckBox;
    CheckBox14: TCheckBox;
    CheckBox15: TCheckBox;
    CheckBox16: TCheckBox;
    CheckBox2: TCheckBox;
    CheckBox3: TCheckBox;
    CheckBox4: TCheckBox;
    CheckBox5: TCheckBox;
    CheckBox6: TCheckBox;
    CheckBox7: TCheckBox;
    CheckBox8: TCheckBox;
    CheckBox9: TCheckBox;
    Edit1: TEdit;
    Edit10: TEdit;
    Edit11: TEdit;
    Edit12: TEdit;
    Edit13: TEdit;
    Edit14: TEdit;
    Edit15: TEdit;
    Edit16: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Edit4: TEdit;
    Edit6: TEdit;
    Edit7: TEdit;
    Edit8: TEdit;
    Edit9: TEdit;
    Label1: TLabel;
    Label10: TLabel;
    Label2: TLabel;
    GroupBox1: TGroupBox;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    LUDPComponent1: TLUDPComponent;
    Timer1: TTimer;
    procedure Button10Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure CheckBox10Change(Sender: TObject);
    procedure CheckBox11Change(Sender: TObject);
    procedure CheckBox12Change(Sender: TObject);
    procedure CheckBox13Change(Sender: TObject);
    procedure CheckBox14Change(Sender: TObject);
    procedure CheckBox15Change(Sender: TObject);
    procedure CheckBox16Change(Sender: TObject);
    procedure CheckBox1Change(Sender: TObject);
    procedure CheckBox2Change(Sender: TObject);
    procedure CheckBox3Change(Sender: TObject);
    procedure CheckBox4Change(Sender: TObject);
    procedure CheckBox5Change(Sender: TObject);
    procedure CheckBox6Change(Sender: TObject);
    procedure CheckBox7Change(Sender: TObject);
    procedure CheckBox8Change(Sender: TObject);
    procedure CheckBox9Change(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure LUDPComponent1Receive(aSocket: TLSocket);
    procedure GetStatus();
    procedure RESet(n: byte);
    procedure REClear(n: byte);
    procedure RJSet(n: byte);
    procedure RJClear(n: byte);
    procedure GetADC();
    procedure Timer1Timer(Sender: TObject);
  private
  public
end;

var
  Form1: TForm1;

implementation

{ TForm1 }
const
  Adr ='192.168.1.9';
  LCDport = 10000;
  ParallelPort = 10001;

var
   ADch: Byte;
   RJ, RE, TRIS_RJ, TRIS_RE, AD_H, AD_L:byte;

procedure TForm1.LUDPComponent1Receive(aSocket: TLSocket);
var
  status : array[1..8] of Byte;
  temp: double;
  i : Integer ;
  st:  string;

begin
  if LUDPComponent1.GetMessage(st) > 0 then
    begin
       //データを表示
       for i:=1 to 8 do status[i] := Byte(st[i]);

       RJ:=status[1]; RE:=status[2];
       TRIS_RJ:=status[3]; TRIS_RE:=status[4];
       AD_H:=status[5];  AD_L:=status[6];
       //pad_1:=status[7];  pad_2:=status[8];

       Edit7.Text:='RJ = ' + IntToStr(RJ);
       Edit8.Text:='RE = ' + IntToStr(RE);
       Edit11.Text:='AD_H =' + IntToHex(AD_H, 4);
       Edit12.Text:='AD_L =' + IntToHex(AD_L, 4);

       //temp := ((AD_H and $03) * $FF + AD_L) * 500/1024;     // 500/1024 = 0.48828125   0.48828125
       temp := ((AD_H and $03) * $FF + AD_L) * 500/1024;     // 500/1024 = 0.48828125   0.48828125


       Edit6.Text := FloatToStrF(temp, ffFixed, 3, 2)+' ℃ ';

         if ADch = $81 then Edit1.Text := IntToHex(status[1],4) + '(' + IntToStr(1) + ')'
         else if ADch = $89 then Edit2.Text := IntToHex(status[2],4) + '(' + IntToStr(2) + ')'
         else if ADch = $91 then Edit3.Text := FloatToStrF((status[5] *256 + status[6])*500/1024, ffFixed, 3,2)
         else if ADch = $99 then Edit4.Text := IntToHex(status[4],4) + '(' + IntToStr(3) + ')'
         else if ADch = $A1 then Edit13.Text := IntToHex(status[2],4) + '(' + IntToStr(2) + ')'
         else if ADch = $A9 then Edit14.Text := FloatToStrF((status[5] *256 + status[6])*500/1024, ffFixed, 3,2)
         else if ADch = $B1 then Edit15.Text := IntToHex(status[4],4) + '(' + IntToStr(3) + ')'
         else if ADch = $B9 then Edit16.Text := IntToHex(status[4],4) + '(' + IntToStr(3) + ')'

    end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
   ADch := $081;  //RA0
   GetADC();      //
end;

procedure LCD1_send;   //LCD String 送信  1
var
  C : array[1..255] of Char ;
  i: integer;
begin

   C[1] := chr($01);  //LCDcontrol
   C[2] := chr($80);  //GoTo Line 0
//   C[3] := chr($02);  //Cursoe home
    for i:=3 to length(Form1.Edit9.text)+2 do
      begin
         C[i] := Form1.Edit9.Text[i-2];
      end;

   Form1.LUDPComponent1.Connect(Adr, LCDport);
   Form1.LUDPComponent1.Send(C, length(Form1.Edit9.text)+2);
end;

procedure LCD2_send;   //LCD String 送信  2
var
  C : array[1..256] of Char ;
  i: integer;
begin

   C[1] := chr($01);  //LCDcontrol
   C[2] := chr($C0);  //GoTo Line 1

   for i:=3 to length(Form1.Edit10.text)+2 do
      begin
         C[i] := Form1.Edit10.Text[i-2];
      end;

   Form1.LUDPComponent1.Connect(Adr, LCDport);
   Form1.LUDPComponent1.Send(C, length(Form1.Edit10.text)+2);
end;

procedure TForm1.Button10Click(Sender: TObject);
var
 // C : array[1..255] of Char ;
 // i: integer;
  S1,S2:ANSIstring;
begin
  S1 := ' 192.168.1.9';
  Form1.Edit9.Text := S1;
  LCD1_send;

  S2 := 'Tristate PIC-NIC';
  Form1.Edit10.Text := S2;
  LCD2_send;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
   ADch := $089;  //RA1
   GetADC();      //
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
   ADch := $081;  //RA.0
   GetADC();      //
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
   ADch := $099;  //RF2
   GetADC();      //
end;


procedure TForm1.Button6Click(Sender: TObject);   //LCD String 送信  1
var
  C : array[1..255] of Char ;
  i: integer;
begin
   if length(Edit9.Text)<2 then exit;
   C[1] := chr($01);  //LCDcontrol
   C[2] := chr($80);  //GoTo Line 0
//   C[3] := chr($02);  //Cursoe home
    for i:=3 to 255 do  //length(Edit9.text)+2 do
      begin
         C[i] := Edit9.Text[i-2];
      end;

   LUDPComponent1.Connect(Adr, LCDport);
   LUDPComponent1.Send(C, length(Edit9.text)+2);
end;

procedure TForm1.Button7Click(Sender: TObject);    // String Clear
var
  c : Array[1..3] of char;
begin
   
   C[1] := chr($01);  //LCDcontrol
   C[2] := chr($01);  //Clear

   LUDPComponent1.Connect(Adr, LCDport);
   LUDPComponent1.Send(C, 2);
   Edit9.Text:='';
   Edit10.Text:='';
end;

procedure TForm1.Button8Click(Sender: TObject);   //LCD String 送信  2
var
  C : array[1..256] of Char ;
  i: integer;
begin
      if length(Edit10.Text)<2 then exit;
   C[1] := chr($01);  //LCDcontrol
   C[2] := chr($C0);  //GoTo Line 1

   for i:=3 to 255 do //length(Edit10.text)+2 do
      begin
         C[i] := Edit10.Text[i-2];
      end;

   LUDPComponent1.Connect(Adr, LCDport);
   LUDPComponent1.Send(C, length(Edit10.text)+2);
end;

procedure TForm1.CheckBox10Change(Sender: TObject);
begin
  if CheckBox10.Checked then REset(1) else REclear(1);
end;

procedure TForm1.CheckBox11Change(Sender: TObject);
begin
  if CheckBox11.Checked then REset(2) else REclear(2);
end;

procedure TForm1.CheckBox12Change(Sender: TObject);
begin
  if CheckBox12.Checked then REset(3) else REclear(3);
end;

procedure TForm1.CheckBox13Change(Sender: TObject);
begin
  if CheckBox13.Checked then REset(4) else REclear(4);
end;

procedure TForm1.CheckBox14Change(Sender: TObject);
begin
  if CheckBox14.Checked then REset(5) else REclear(5);
end;

procedure TForm1.CheckBox15Change(Sender: TObject);
begin
  if CheckBox15.Checked then REset(6) else REclear(6);
end;

procedure TForm1.CheckBox16Change(Sender: TObject);
begin
   if CheckBox16.Checked then REset(7) else REclear(7);
end;

procedure TForm1.CheckBox1Change(Sender: TObject);
begin
  if CheckBox1.Checked then RJset(0) else RJclear(0);
end;

procedure TForm1.CheckBox2Change(Sender: TObject);
begin
  if CheckBox2.Checked then RJset(1) else RJclear(1);
end;

procedure TForm1.CheckBox3Change(Sender: TObject);
begin
  if CheckBox3.Checked then RJset(2) else RJclear(2);
end;

procedure TForm1.CheckBox4Change(Sender: TObject);
begin
  if CheckBox4.Checked then RJset(3) else RJclear(3);
end;

procedure TForm1.CheckBox5Change(Sender: TObject);
begin
  if CheckBox5.Checked then RJset(4) else RJclear(4);
end;

procedure TForm1.CheckBox6Change(Sender: TObject);
begin
  if CheckBox6.Checked then RJset(5) else RJclear(5);
end;

procedure TForm1.CheckBox7Change(Sender: TObject);
begin
  if CheckBox7.Checked then RJset(6) else RJclear(6);
end;

procedure TForm1.CheckBox8Change(Sender: TObject);
begin
  if CheckBox8.Checked then RJset(7) else RJclear(7);
end;

procedure TForm1.CheckBox9Change(Sender: TObject);
begin
  if CheckBox9.Checked then REset(0) else REclear(0);
end;

//RExセット
procedure TForm1.REset(n : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($01);   // set
  C[2] := chr($06);   // port = RE
  C[3] := chr(n);    // Bit

  LUDPComponent1.Connect(Adr, ParallelPort);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RB) + ' Set') ;
end;

//RExクリア
procedure TForm1.REClear(n : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($02) ;   // clear
  C[2] := chr($06) ;   // port = RB
  C[3] := chr(n) ;    // Bit
  LUDPComponent1.Connect(Adr, ParallelPort);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RB) + ' Clear') ;
end;

//RJxセット
procedure TForm1.RJset(n : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($01);   // set
  C[2] := chr($05);   // port = RJ
  C[3] := chr(n);    // Bit

  LUDPComponent1.Connect(Adr, ParallelPort);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RJ) + ' Set') ;
end;

//RExクリア
procedure TForm1.RJClear(n : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($02) ;   // clear
  C[2] := chr($05) ;   // port = RB
  C[3] := chr(n) ;    // Bit
  LUDPComponent1.Connect(Adr, ParallelPort);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RJ) + ' Clear') ;
end;

procedure TForm1.FormActivate(Sender: TObject);
begin
    GetStatus();
end;

procedure TForm1.GetStatus();
 var
    C: array[1..3] of Char;
 begin
    C[1] := Chr($0);
    LUDPComponent1.Connect(Adr, ParallelPort);
    LUDPComponent1.Send(C[1], 1);
 end;

//ADCデータ出力コマンド発行
procedure TForm1.GetADC();
 var
  C : array[1..3]of  Char ;
 begin
  C[1] := chr($04) ;  //GetADC
  C[2] := chr(ADch) ; //ch
  C[3] := chr($01) ;  //wait time $01×5μsec  = 5μsec

  LUDPComponent1.Connect(Adr, ParallelPort);
  LUDPComponent1.Send(C, 3);
 end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
   ADch := $91;  //RA0
   GetADC();
end;

initialization
  {$I L_UDP_4u.lrs}

end.

