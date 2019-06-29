unit L_UDP_3U;
{$mode objfpc}{$H+}{$CodePage UTF8}
interface
uses
  Classes, SysUtils, FileUtil, LResources, Forms, Controls, Graphics, Dialogs,
  ExtCtrls, StdCtrls, lNetComponents, lNet, Windows;
type
  { TForm1 }
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    CheckBox1: TCheckBox;
    CheckBox2: TCheckBox;
    CheckBox3: TCheckBox;
    CheckBox4: TCheckBox;
    CheckBox5: TCheckBox;
    CheckBox6: TCheckBox;
    CheckBox7: TCheckBox;
    Edit6: TEdit;
    Edit7: TEdit;
    Edit8: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    rb7: TCheckBox;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Edit4: TEdit;
    Edit5: TEdit;
    GroupBox1: TGroupBox;
    LUDPComponent1: TLUDPComponent;
    Timer1: TTimer;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure CheckBox1Change(Sender: TObject);
    procedure CheckBox2Change(Sender: TObject);
    procedure CheckBox3Change(Sender: TObject);
    procedure CheckBox4Change(Sender: TObject);
    procedure CheckBox5Change(Sender: TObject);
    procedure CheckBox6Change(Sender: TObject);
    procedure CheckBox7Change(Sender: TObject);
    procedure rb7Change(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure LUDPComponent1Receive(aSocket: TLSocket);
    procedure GetStatus();
    procedure SetPB(PBDat:char);
    procedure RBSet(RB: byte);
    procedure RBClear(RB: byte);
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
  Adr ='192.168.1.2';
  port =  10001;

var
   ADch: Byte;

procedure TForm1.LUDPComponent1Receive(aSocket: TLSocket);
var
  status : array[1..8] of Byte;
  temp: double;
  i, j : Integer ;
  st:  string;
  portA_data, portA_direct, PortB_data, portB_direct: byte;
begin
  if LUDPComponent1.GetMessage(st) > 0 then
    begin
       //ADCのデータを表示
       for i:=1 to 8 do status[i] := Byte(st[i+1]);
       portA_data:=status[1]; portA_direct:=status[2];
       portB_data:=status[3]; portB_direct:=status[4];

       Edit7.Text:='port_A = ' + IntToStr(portA_data);
       Edit8.Text:='port_B = ' + IntToStr(portb_data);

       temp := (status[4] * 16.0 + status[5]) / 2.0;
       Edit6.Text := FloatToStrF(temp, ffFixed, 3, 1)+' ℃ ';

         if ADch = $81 then Edit5.Text := IntToHex(status[1],4) + '(' + IntToStr(1) + ')'
         else if ADch = $89 then Edit4.Text := IntToHex(status[2],4) + '(' + IntToStr(2) + ')'
         else if ADch = $91 then Edit3.Text := IntToHex(status[3],4) + '(' + IntToStr(3) + ')'
         else if ADch = $99 then Edit2.Text := IntToHex(status[4],4) + '(' + IntToStr(4) + ')'
         else if ADch = $A1 then Edit1.Text := FloatToStrF(status[5] / 1024 * 500, ffFixed, 3,1);

       //PB1のデータを表示
       if (ord(Status[2]) and $02) = 0 then CheckBox2.Checked := False
         else CheckBox2.Checked := True;

       //PB0のデータを表示
       if (ord(Status[2]) and $01) = 0 then CheckBox1.Checked := False
         else CheckBox1.Checked := True;

      //Application.MessageBox( 'データ数が一致しません' , '受信したデータに誤りがあります' , MB_OK) ;

      //ステータスを受信したらADchをクリア
      ADch := 0;
      end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
   ADch := $0A1;  //RA5
   GetADC();      //
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
   ADch := $099;  //RA4
   GetADC();      //
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
   ADch := $091;  //RA3
   GetADC();      //
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
   ADch := $089;  //RA2
   GetADC();      //
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
   ADch := $081;  //R1
   GetADC();      //
end;

procedure TForm1.CheckBox1Change(Sender: TObject);
begin
  if CheckBox1.Checked then RBset(0) else RBclear(0);
end;

procedure TForm1.CheckBox2Change(Sender: TObject);
begin
  if CheckBox2.Checked then RBset(1) else RBclear(1);
end;

procedure TForm1.CheckBox3Change(Sender: TObject);
begin
  //if CheckBox3.Checked then RBset(2) else RBclear(2);
end;

procedure TForm1.CheckBox4Change(Sender: TObject);
begin
  if CheckBox4.Checked then RBset(3) else RBclear(3);
end;

procedure TForm1.CheckBox5Change(Sender: TObject);
begin
  if CheckBox5.Checked then RBset(4) else RBclear(4);
end;

procedure TForm1.CheckBox6Change(Sender: TObject);
begin
  if CheckBox6.Checked then RBset(5) else RBclear(5);
end;

procedure TForm1.CheckBox7Change(Sender: TObject);
begin
  if CheckBox7.Checked then RBset(6) else RBclear(6);
end;

procedure TForm1.rb7Change(Sender: TObject);
begin
  if rb7.Checked then RBset(7) else RBclear(7);
end;

//RBxセット
procedure TForm1.RBset(RB : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($01);   // set
  C[2] := chr($06);   // port = RB
  C[3] := chr(RB);    // RBx

  LUDPComponent1.Connect(Adr, port);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RB) + ' Set') ;
end;

//RBxクリア
procedure TForm1.RBClear(RB : Byte);
var
  C : Array[1..3] of char ;
begin
  C[1] := chr($02) ;   // clear
  C[2] := chr($06) ;   // port = RB
  C[3] := chr(RB) ;    // RBx
  LUDPComponent1.Connect(Adr, port);
  LUDPComponent1.Send(C, 3);
  //Memo1.Lines.Add('--------') ;
  //Memo1.Lines.Add('RB' + IntToStr(RB) + ' Clear') ;
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
    LUDPComponent1.Connect(Adr, port);
    LUDPComponent1.Send(C[1], 1);
 end;

//PICNICのステータスを元にCheckBoxPB7～2を設定
procedure TForm1.SetPB(PBDat: Char);
 begin
    if (ord(PBDat) and $80) = 0 then CheckBox7.Checked := False
     else CheckBox7.Checked := True;

    if (ord(PBDat) and $40) = 0 then CheckBox6.Checked := False
     else CheckBox6.Checked := True;

    if (ord(PBDat) and $20) = 0 then CheckBox5.Checked := False
     else CheckBox5.Checked := True;

    if (ord(PBDat) and $10) = 0 then CheckBox4.Checked := False
     else CheckBox4.Checked := True;

    if (ord(PBDat) and $08) = 0 then CheckBox3.Checked := False
     else CheckBox3.Checked := True;

    if (ord(PBDat) and $04) = 0 then CheckBox2.Checked := False
     else CheckBox2.Checked := True;

    if (ord(PBDat) and $02) = 0 then CheckBox1.Checked := False
     else CheckBox1.Checked := True;
 end;

//ADCデータ出力コマンド発行
procedure TForm1.GetADC();
 var
  C : array[1..3]of  Char ;
 begin
  C[1] := chr($04) ;  //GetADC
  C[2] := chr(ADch) ; //ch
  C[3] := chr($01) ;  //wait time $01×5μsec  = 5μsec

  LUDPComponent1.Connect(Adr, port);
  LUDPComponent1.Send(C, 3);
 end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
   ADch := $A1;  //RA0
   GetADC();
end;

initialization
  {$I L_UDP_3u.lrs}

end.

