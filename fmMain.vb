Public Class fmMain
    Private drawMode As Integer = 0
    Private lMode As Integer = 0
    Private dtPnt As New gPoint
    Private dtLine1 As New gLine
    Private dtLine2 As New gLine
    Private hCnt As Integer = 0
    Private dtHlp(16) As gPoint
    Dim gUnit As Integer = 10
    Dim gHnd As Graphics
    Dim gl As New gLib

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLine.Click
        Dim p1 As New gPoint(10.0, 10.0)
        Dim p2 As New gPoint(20.0, 20.0)
        Dim o1 As New gPoint
        Dim o2 As New gPoint

        If gl.moveLine(dtLine1.sp, dtLine1.ep, -20.0, o1, o2) = gLib.ZOK Then
            gHnd.DrawLine(Pens.Red, CInt(o1.x), CInt(o1.y), CInt(o2.x), CInt(o2.y))
            pictGraph.Refresh()
        End If
    End Sub

    Private Sub btnPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoint.Click
        Dim c As New gPoint(dtPnt.x, cnvY(dtPnt.y))
        Dim p1 As New gPoint(dtLine1.sp.x, cnvY(dtLine1.sp.y))
        Dim p2 As New gPoint(dtLine1.ep.x, cnvY(dtLine1.ep.y))
        Dim irc As Integer

        'gl.chkPointToLine(dtPnt, dtLine1.sp, dtLine1.ep, o1, r, irc)
        'txtResult.Text = CStr(System.Math.Round(r)) + ", " + CStr(irc)
        gl.chkPointToLine(c, p1, p2, irc)
        gl.chkPointOnLine(c, p1, p2, irc)
        txtResult.Text = "irc= " + CStr(irc)
    End Sub

    Private Sub btnAndLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAndLine.Click
        Dim mode As Integer

        If chkAnd.Checked Then
            mode = 1
        Else
            mode = 0
        End If
        Dim ln As New gLine
        If gl.andLine(mode, dtLine1, dtLine2, ln) = gLib.ZOK Then
            gHnd.DrawLine(Pens.Red, CInt(ln.sp.x), CInt(ln.sp.y), CInt(ln.ep.x), CInt(ln.ep.y))
            pictGraph.Refresh()
        End If
    End Sub

    Private Sub btnCutLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutLine.Click
        Dim nout As Integer
        Dim ln(4) As gLine
        If gl.cutLine(dtLine1, dtLine2, nout, ln) = gLib.ZOK Then
            For i As Integer = 0 To nout - 1
                gHnd.DrawLine(Pens.Red, CInt(ln(i).sp.x), CInt(ln(i).sp.y), CInt(ln(i).ep.x), CInt(ln(i).ep.y))
            Next
            pictGraph.Refresh()
        End If
    End Sub

    Private Sub btnGetpoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetpoint.Click
        Dim o_l As New gPoint
        Dim o_r As New gPoint
        Dim o_c As New gPoint
        Dim p1 As New gPoint(dtLine1.sp.x, cnvY(dtLine1.sp.y))
        Dim p2 As New gPoint(dtLine1.ep.x, cnvY(dtLine1.ep.y))

        gl.getPoint(p1, p2, 100.0, 50.0, o_l, o_r, o_c)
        drawpoint(o_l, Color.Red)
        drawpoint(o_r, Color.Blue)
        drawpoint(o_c, Color.Red)
    End Sub

    Private Sub ‚‚‚”‚ŽCross_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCross.Click
        Dim o_c As New gPoint
        Dim irc As Integer
        gl.getPoint(0, dtLine1, dtLine2, o_c, irc)
        drawpoint(o_c, Color.Red)
        txtResult.Text = "irc= " + CStr(irc)
    End Sub

    Private Sub btnArea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArea.Click
        Dim p(5) As gPoint
        Dim r As Double
        p(0) = New gPoint(dtLine1.sp)
        p(1) = New gPoint(dtLine1.ep)
        p(2) = New gPoint(dtLine2.sp)
        p(3) = New gPoint(dtLine2.ep)
        p(4) = New gPoint(dtLine1.sp)
        gl.getArea(5, p, r)
        txtResult.Text = "area= " + CStr(r)
        gl.chgRotate(gLib.ZRIGHT, 5, p)
        gl.chgRotate(gLib.ZLEFT, 4, p)
    End Sub

    Private Sub btnHlpPnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHlpPnt.Click
        Dim p(5) As gPoint
        Dim stat As Integer
        p(0) = New gPoint(dtLine1.sp)
        p(1) = New gPoint(dtLine1.ep)
        p(2) = New gPoint(dtLine2.sp)
        p(3) = New gPoint(dtLine2.ep)
        p(4) = New gPoint(dtLine1.sp)
        gl.chkPoint(5, p, dtPnt, stat)
        txtResult.Text = "stat= " + CStr(stat)
    End Sub

    Private Sub btnHlpLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHlpLine.Click
        Dim iout As Integer
        Dim ln(16) As gLine
        Dim stat(16) As Integer
        Dim dpn As System.Drawing.Pen
        gl.chkLine(hCnt, dtHlp, dtLine1, iout, ln, stat)
        Dim txt As String = CStr(iout)
        For i As Integer = 0 To iout - 1
            txt += (", " + CStr(stat(i)))
            txtResult.Text = txt
            If i Mod 2 = 0 Then
                dpn = Pens.Red
            Else
                dpn = Pens.Blue
            End If
            gHnd.DrawLine(dpn, CInt(ln(i).sp.x), CInt(ln(i).sp.y), CInt(ln(i).ep.x), CInt(ln(i).ep.y))
        Next
    End Sub

    Private Sub fmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim gl As New gLib
        'Dim o1 As New gPoint
        'gl.test(o1)
        Dim w(8) As gPoint

        Dim a As Integer = w.GetLength(0)
        pictGraph.Image = New Bitmap(pictGraph.Size.Width, pictGraph.Size.Height)
        gHnd = Graphics.FromImage(pictGraph.Image)
        drawGrid()
        rdoLine1.Checked = True
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pictGraph.MouseDown
        Dim gx As Double = rnd(e.X)
        Dim gy As Double = rnd(e.Y)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            lMode = 0
            Exit Sub
        End If
        If drawMode = 1 Then
            If lMode = 0 Then
                lMode = 1
                If rdoLine1.Checked Then
                    dtLine1.sp.x = gx
                    dtLine1.sp.y = gy
                Else
                    dtLine2.sp.x = gx
                    dtLine2.sp.y = gy
                End If
            Else
                lMode = 0
                If rdoLine1.Checked Then
                    dtLine1.ep.x = gx
                    dtLine1.ep.y = gy
                    gHnd.DrawLine(Pens.Black, CInt(dtLine1.sp.x), CInt(dtLine1.sp.y), CInt(dtLine1.ep.x), CInt(dtLine1.ep.y))
                Else
                    dtLine2.ep.x = gx
                    dtLine2.ep.y = gy
                    gHnd.DrawLine(Pens.Black, CInt(dtLine2.sp.x), CInt(dtLine2.sp.y), CInt(dtLine2.ep.x), CInt(dtLine2.ep.y))
                End If
                pictGraph.Refresh()
            End If
        ElseIf drawMode = 2 Then
            Dim brsh As SolidBrush = New SolidBrush(Color.Black)
            dtPnt.x = rnd(e.X)
            dtPnt.y = rnd(e.Y)
            gHnd.FillEllipse(brsh, CInt(dtPnt.x), CInt(dtPnt.y), 4, 4)
            pictGraph.Refresh()
            brsh.Dispose()
        Else
            If hCnt >= 16 Then
                Exit Sub
            End If
            If lMode = 0 Then
                hCnt = 0
                lMode = 1
            End If
            If dtHlp(hCnt) Is Nothing Then
                dtHlp(hCnt) = New gPoint
            End If
            dtHlp(hCnt).x = gx
            dtHlp(hCnt).y = gy
            If hCnt > 0 Then
                gHnd.DrawLine(Pens.Black, CInt(dtHlp(hCnt - 1).x), CInt(dtHlp(hCnt - 1).y), CInt(dtHlp(hCnt).x), CInt(dtHlp(hCnt).y))
            End If
            hCnt += 1
            pictGraph.Refresh()
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pictGraph.MouseMove
        If lMode = 0 Then
            Exit Sub
        End If
        Dim g As Graphics = pictGraph.CreateGraphics()
        pictGraph.Refresh()
        If rdoLine1.Checked Then
            g.DrawLine(Pens.Gray, CInt(dtLine1.sp.x), CInt(dtLine1.sp.y), rnd(e.X), rnd(e.Y))
        ElseIf rdoLine2.Checked Then
            g.DrawLine(Pens.Gray, CInt(dtLine2.sp.x), CInt(dtLine2.sp.y), rnd(e.X), rnd(e.Y))
        Else
            If hCnt > 0 Then
                g.DrawLine(Pens.Gray, CInt(dtHlp(hCnt - 1).x), CInt(dtHlp(hCnt - 1).y), rnd(e.X), rnd(e.Y))
            End If
        End If
        g.Dispose()
        'If curX > 0 Then
        '    g.DrawLine(myPen, curX, 0, curX, PictureBox1.Height)
        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        gHnd.Clear(pictGraph.BackColor)
        drawGrid()
    End Sub

    Private Sub drawpoint(ByVal p As gPoint, ByVal col As Color, Optional ByVal size As Integer = 4)
        Dim brsh As SolidBrush = New SolidBrush(col)
        gHnd.FillEllipse(brsh, CInt(p.x), CInt(cnvY(p.y)), size, size)
        pictGraph.Refresh()
        brsh.Dispose()
    End Sub

    Private Sub drawGrid()
        For i As Integer = gUnit To pictGraph.Size.Width Step gUnit
            For k As Integer = gUnit To pictGraph.Size.Height Step gUnit
                gHnd.DrawEllipse(Pens.Gray, i, k, 1, 1)
            Next
        Next
        pictGraph.Refresh()
    End Sub

    Private Function rnd(ByVal v As Integer) As Integer
        rnd = CInt(v / gUnit) * gUnit
    End Function

    Private Function cnvY(ByVal y As Integer) As Integer
        'cnvY = pictGraph.Height - y
        cnvY = y
    End Function

    Private Sub rdoAnd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub chkAnd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnd.CheckedChanged
        If chkAnd.Checked Then
            chkAnd.Text = "Or"
        Else
            chkAnd.Text = "And"
        End If
    End Sub

    Private Sub rdoLine1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoLine1.CheckedChanged
        drawMode = 1
        lMode = 0
    End Sub

    Private Sub rdoLine2_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoLine2.CheckedChanged
        drawMode = 1
        lMode = 0
    End Sub

    Private Sub rdoPoint_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoPoint.CheckedChanged
        drawMode = 2
    End Sub

    Private Sub rdoPoly1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoPoly1.CheckedChanged
        If rdoPoly1.Checked Then
            drawMode = 3
            lMode = 1
            hCnt = 0
        End If
    End Sub
End Class
