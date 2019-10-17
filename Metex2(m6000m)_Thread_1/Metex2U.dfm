object Form1: TForm1
  Left = 688
  Top = 155
  Caption = 'Metex2'
  ClientHeight = 742
  ClientWidth = 220
  Color = clBtnFace
  Font.Charset = SHIFTJIS_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'ＭＳ Ｐゴシック'
  Font.Style = []
  OldCreateOrder = False
  Visible = True
  OnPaint = FormPaint
  PixelsPerInch = 96
  TextHeight = 12
  object Label2: TLabel
    Left = 16
    Top = 672
    Width = 201
    Height = 33
    AutoSize = False
    Font.Charset = SHIFTJIS_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'ＭＳ Ｐゴシック'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Button1: TButton
    Left = 16
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Data Read'
    TabOrder = 0
    OnClick = Button1Click
  end
  object ListBox1: TListBox
    Left = 16
    Top = 40
    Width = 185
    Height = 593
    Font.Charset = SHIFTJIS_CHARSET
    Font.Color = clWindowText
    Font.Height = -21
    Font.Name = 'ＭＳ ゴシック'
    Font.Style = [fsBold]
    ItemHeight = 21
    Items.Strings = (
      '     Data')
    ParentFont = False
    TabOrder = 1
  end
end
