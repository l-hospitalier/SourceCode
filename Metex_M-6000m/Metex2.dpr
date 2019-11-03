program Metex2;


uses
  Interfaces,

  Forms,
  Metex2U in 'Metex2U.pas' {Form1};

{.$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
