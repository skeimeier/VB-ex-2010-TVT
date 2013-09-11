Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        SerialPort.Write(TextBox2.Text)

    End Sub

   
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim availablePorts As Array
        availablePorts = System.IO.Ports.SerialPort.GetPortNames()

        For i = 0 To UBound(availablePorts)
            cmbComPorts.Items.Add(availablePorts(i))
        Next
        cmbComPorts.SelectionStart = 0


    End Sub

    Private Sub cmbComPorts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComPorts.Click

    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If ToolStripButton1.Text.Contains("Close") Then
            ToolStripButton1.Text = "Open"
            SerialPort.Close()
        Else
            SerialPort.PortName = cmbComPorts.SelectedText
            SerialPort.Open()
            If SerialPort.IsOpen Then
                ToolStripButton1.Text = "Close"
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If SerialPort.IsOpen Then
            ToolStripLabel2.Text = "O"
            Button1.Enabled = True
            Button2.Enabled = TextBox1.Text.Length > 0
            Button3.Enabled = True

        Else
            ToolStripLabel2.Text = "X"
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text.Length > 0 Then
            SerialPort.Write(Chr(27) + "[" + TextBox1.Lines(0))
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim datum As System.DateTime
        Dim strDatum, dd, mm, yy As String
        Dim strZeit, hh, mi, sec As String
        datum = System.DateTime.Now
        dd = datum.Day.ToString.PadLeft(2, "0")
        mm = datum.Month.ToString.PadLeft(2, "0")
        yy = (datum.Year - 2000).ToString
        strDatum = dd + ";" + mm + ";" + yy
        SerialPort.Write(Chr(27) + "[" + strDatum + "?t")

        System.Threading.Thread.Sleep(300)

        hh = datum.Hour.ToString.PadLeft(2, "0")
        mi = datum.Minute.ToString.PadLeft(2, "0")
        sec = datum.Second.ToString.PadLeft(2, "0")
        strZeit = hh + ";" + mi + ";" + sec
        SerialPort.Write(Chr(27) + "[" + strZeit + "?s")


    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Tag = 0
        Timer2.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        SerialPort.Write(Chr(27) + "[?75l")

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        SerialPort.Write(Chr(27) + "[?75h")

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim strESC, strLabel As String

        strESC = ComboBox1.SelectedItem
        strLabel = strESC.Substring(strESC.IndexOf("-") + 1)
        Label1.Text = strLabel
        strESC = strESC.Substring(4, strESC.IndexOf("-") - 5)
        TextBox1.Text = strESC
    End Sub
End Class
