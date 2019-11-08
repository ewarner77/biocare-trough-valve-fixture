Imports System.Text
Public Class Form1
    Declare Sub Sleep Lib "kernel32" Alias "Sleep" _
   (ByVal dwMilliseconds As Long)
    Public Property Encoding As System.Text.Encoding

    Private Sub Wait(ByVal DurationMS As Long)
        Dim EndTime As Long
        Dim counting As Long
        EndTime = Environment.TickCount + DurationMS
        Do While EndTime > Environment.TickCount
            counting = (EndTime - Environment.TickCount) / 1000
            Label1.Text = Format(counting, "00")
            Application.DoEvents()
            Threading.Thread.Sleep(100)
        Loop
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Open_Serial()
    End Sub

    Private Sub Open_Serial()
        'Dim utf8 As System.Text.Encoding = Encoding.UTF8
        SerialPort1.PortName = "COM4"
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.Handshake = IO.Ports.Handshake.None
        'SerialPort1.Encoding = Encoding.UTF8
        SerialPort1.Open()
    End Sub

    Private Function Send_String(ByVal sendstr As String) As String
        Dim holdstr As String
        'Dim utf8 As New System.Text.UTF8Encoding()
        holdstr = ""
        If SerialPort1.IsOpen Then
            Dim stringback As String
            SerialPort1.Write(sendstr & Chr(10) & Chr(13))
            Wait(50)
            stringback = SerialPort1.ReadExisting()
            Send_String = stringback
            For i = 1 To Len(stringback)
                holdstr = holdstr & Asc(Mid(stringback, i, 1)) & ":"
            Next
        Else
            Send_String = ""
        End If
    End Function
    Private Sub valve_position(vno As Integer, pos As Integer)
        Dim posno As Integer
        Dim outstr As String
        'If pos = 1 Then posno = 0
        'If pos = 2 Then posno = 72
        'If pos = 3 Then posno = 144
        'If pos = 4 Then posno = 216
        'If pos = 5 Then posno = 288
        posno = (pos - 1) * 72
        outstr = "/" & Format(vno, "0") & "h29" & Format(posno, "000") & "R"
        Label2.Text = Send_String(outstr)
    End Sub
    Private Function query_valve(vno As Integer)
        Dim outstr As String
        outstr = "/" & Format(vno, "0") & "?25000R"

    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label2.Text = Send_String("/2ZR")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Label2.Text = Send_String("/1ZR")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        valve_position(2, 1)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        valve_position(2, 2)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        valve_position(2, 3)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        valve_position(2, 4)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        valve_position(2, 5)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        valve_position(1, 1)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        valve_position(1, 2)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        valve_position(1, 3)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        valve_position(1, 4)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        valve_position(1, 5)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        valve_position(2, 1)
        Wait(50)
        valve_position(1, 1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        valve_position(2, 2)
        Wait(50)
        valve_position(1, 2)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label2.Text = Send_String("/1?25000R")
    End Sub
End Class
