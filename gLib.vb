Imports System.Math

Public Class gLib
    Public Const ZOK As Integer = 0
    Public Const ZERR As Integer = 1
    Public Const ZERO As Double = 0.0001
    Public Const ZRIGHT As Integer = 0
    Public Const ZLEFT As Integer = 1
    Private ALW As Double
    Private Const OUTMAX As Integer = 51

    Public Sub New(Optional ByVal a As Double = 0.5)
        ALW = a
    End Sub

    '2点間の距離を求める
    Public Function getDist(ByVal p1 As gPoint, ByVal p2 As gPoint, ByRef dist As Double) As Integer
        Dim ret As Double = ZOK
        Dim dx As Double = Abs(p1.x - p2.x)
        Dim dy As Double = Abs(p1.y - p2.y)

        If (dx <= ALW) Then
            dist = dy
        ElseIf (dy <= ALW) Then
            dist = dx
        Else
            dist = Sqrt(dx * dx + dy * dy)
        End If
        If (dist <= ALW) Then
            ret = ZERR
        End If
        getDist = ret
    End Function

    '線分の平行移動
    Public Function moveLine(ByVal olin As gLine, ByVal dist As Double, ByRef nlin As gLine) As Integer
        moveLine = moveLine(olin.sp, olin.ep, dist, nlin.sp, nlin.ep)
    End Function

    Public Function moveLine(ByVal op1 As gPoint, ByVal op2 As gPoint, ByVal dist As Double, ByRef np1 As gPoint, ByRef np2 As gPoint) As Integer
        Dim lng As Double

        Dim ret As Integer = getDist(op1, op2, lng)
        If (ret = ZOK) Then
            If ((Abs(op1.x - op2.x) > ALW) And (Abs(op1.y - op2.y) > ALW)) Then
                Dim dx As Double = (op2.x - op1.x) / lng
                Dim dy As Double = (op2.y - op1.y) / lng
                np1.x = op1.x + dist * dy
                np1.y = op1.y - dist * dx
                np2.x = op2.x + dist * dy
                np2.y = op2.y - dist * dx
            Else
                If (Abs(op1.x - op2.x) > ALW) Then
                    If (op1.x < op2.x) Then
                        np1.x = op1.x
                        np1.y = op1.y - dist
                        np2.x = op2.x
                        np2.y = np1.y
                    Else
                        np1.x = op1.x
                        np1.y = op1.y + dist
                        np2.x = op2.x
                        np2.y = np1.y
                    End If
                Else
                    If (op1.y < op2.y) Then
                        np1.x = op1.x + dist
                        np1.y = op1.y
                        np2.x = np1.x
                        np2.y = op2.y
                    Else
                        np1.x = op1.x - dist
                        np1.y = op1.y
                        np2.x = np1.x
                        np2.y = op2.y
                    End If
                End If
            End If
        End If
        moveLine = ret
    End Function

    '点と線の位置関係の判定，直交点と距離を求める
    ' op    : 直交点
    ' dist  : 距離
    ' irc   : 直交点と線の位置関係	１：端点上,２：線分上,０：載っていない
    Public Function chkPointToLine(ByVal cp As gPoint, ByVal st_p As gPoint, ByRef ed_p As gPoint, _
                             ByRef out_p As gPoint, ByRef dist As Double, ByRef irc As Double) As Integer
        Dim xxc, yyc As Double
        Dim xds = 0.0, yds = 0.0, xs, ys, cost, sint, rab As Double
        Dim sp As New gPoint(st_p)
        Dim ep As New gPoint(ed_p)

        irc = 0
        chkPointToLine = ZERR
        If getDist(sp, ep, rab) <> ZOK Then
            Exit Function
        End If

        If (Abs(ep.x - sp.x) <= ALW) Then
            dist = Abs(cp.x - sp.x)
            xds = sp.x
            yds = cp.y
            If (ep.y <= sp.y) Then
                ys = ep.y
                ep.y = sp.y
                sp.y = ys
            End If
            If (((sp.y + ALW) < cp.y) And (cp.y < (ep.y - ALW))) Then
                irc = 2
            End If
            If ((Abs(cp.y - sp.y) <= ALW) Or (Abs(cp.y - ep.y) <= ALW)) Then
                irc = 1
            End If
        ElseIf (Abs(ep.y - sp.y) <= ALW) Then
            dist = Abs(cp.y - sp.y)
            xds = cp.x
            yds = sp.y
            If (ep.x <= sp.x) Then
                xs = ep.x
                ep.x = sp.x
                sp.x = xs
            End If
            If (((sp.x + ALW) < cp.x) And (cp.x < (ep.x - ALW))) Then
                irc = 2
            End If
            If ((Abs(cp.x - sp.x) <= ALW) Or (Abs(cp.x - ep.x) <= ALW)) Then
                irc = 1
            End If
        Else
            cost = (ep.x - sp.x) / rab
            sint = (ep.y - sp.y) / rab
            xs = cp.x - sp.x
            ys = cp.y - sp.y
            xxc = xs * cost + ys * sint
            yyc = -xs * sint + ys * cost
            dist = Abs(yyc)
            If ((ALW < xxc) And (xxc < (rab - ALW))) Then
                irc = 2
            End If
            If ((Abs(xxc) <= ALW) Or (Abs(xxc - rab) <= ALW)) Then
                irc = 1
            End If
            xds = xxc * cost + sp.x
            yds = xxc * sint + sp.y
        End If

        out_p.x = xds
        out_p.y = yds
        chkPointToLine = ZOK
    End Function

    '点が線分のどちら側か調べる
    ' irc   : １：左, ２：右
    Public Function chkPointToLine(ByVal cp As gPoint, ByVal sp As gPoint, ByRef ep As gPoint, ByRef irc As Double) As Integer
        Dim dist, wy, cost, sint As Double

        irc = 0
        chkPointToLine = ZERR
        If getDist(sp, ep, dist) <> ZOK Then
            Exit Function
        End If

        If (Abs(sp.x - ep.x) > ALW) Then
            If (Abs(sp.y - ep.y) > ALW) Then
                cost = (ep.x - sp.x) / dist
                sint = (ep.y - sp.y) / dist
                wy = -(cp.x - sp.x) * sint + (cp.y - sp.y) * cost
                If (wy > ALW) Then
                    irc = 1
                ElseIf (wy < -ALW) Then
                    irc = 2
                End If
            Else
                If (sp.x < ep.x) Then
                    If (cp.y > (sp.y + ALW)) Then
                        irc = 1
                    ElseIf (cp.y < (sp.y - ALW)) Then
                        irc = 2
                    End If
                Else
                    If (cp.y < (sp.y - ALW)) Then
                        irc = 1
                    ElseIf (cp.y > (sp.y + ALW)) Then
                        irc = 2
                    End If
                End If
            End If
        Else
            If (sp.y < ep.y) Then
                If (cp.x < (sp.x - ALW)) Then
                    irc = 1
                ElseIf (cp.x > (sp.x + ALW)) Then
                    irc = 1
                End If
            Else
                If (cp.x > (sp.x - ALW)) Then
                    irc = 1
                ElseIf (cp.x < (sp.x + ALW)) Then
                    irc = 2
                End If
            End If
        End If
        chkPointToLine = ZOK
    End Function

    '線分を線分で切り取る
    Public Function cutLine(ByVal line1 As gLine, ByVal line2 As gLine, ByRef nout As Integer, ByRef oline() As gLine) As Integer
        Dim pout(4, 4) As gPoint
        cutLine = cutLine(line1.sp, line1.ep, line2.sp, line2.ep, nout, pout)
        For i As Integer = 0 To nout - 1
            oline(i) = New gLine(pout(i, 0), pout(i, 1))
        Next
    End Function
    Public Function cutLine(ByVal sp1 As gPoint, ByVal ep1 As gPoint, ByVal sp2 As gPoint, ByVal ep2 As gPoint, _
                            ByRef nout As Integer, ByRef pout(,) As gPoint) As Integer
        Dim id As Integer
        Dim so As New gPoint
        Dim eo As New gPoint

        nout = 0
        id = 0
        If andLine(0, sp1, ep1, sp2, ep2, so, eo) <> ZOK Then
            pout(nout, 0) = New gPoint(sp1)
            pout(nout, 1) = New gPoint(ep1)
            nout = nout + 1
            cutLine = ZERR
            Exit Function
        End If
        If Not (Abs(sp1.x - so.x) <= ALW And Abs(sp1.y - so.y) <= ALW) Then
            pout(nout, 0) = New gPoint(sp1)
            pout(nout, 1) = New gPoint(so)
            nout = nout + 1
        End If
        If Not (Abs(ep1.x - eo.x) <= ALW And Abs(ep1.y - eo.y) <= ALW) Then
            pout(nout, 0) = New gPoint(eo)
            pout(nout, 1) = New gPoint(ep1)
            nout = nout + 1
        End If
        cutLine = ZOK
    End Function

    '線分のＡＮＤ，ＯＲを求める
    ' mode  ： ０：ＡＮＤ，１：ＯＲ
    Public Function andLine(ByVal mode As Integer, ByVal line1 As gLine, ByVal line2 As gLine, ByRef oline As gLine) As Integer
        andLine = andLine(mode, line1.sp, line1.ep, line2.sp, line2.ep, oline.sp, oline.ep)
    End Function

    Public Function andLine(ByVal mode As Integer, ByVal sp1 As gPoint, ByVal ep1 As gPoint, ByVal sp2 As gPoint, ByVal ep2 As gPoint, _
                             ByRef sout As gPoint, ByRef eout As gPoint) As Integer
        Dim dist As Double
        Dim irc1, irc2, irc3, irc4, ret As Integer
        Dim st_p, ed_p As gPoint
        Dim op As New gPoint

        andLine = ZERR
        If ((Abs(sp1.x - ep1.x) <= ALW) And (Abs(sp1.y - ep1.y) <= ALW)) Then
            Exit Function
        End If
        If ((Abs(sp2.x - ep2.x) <= ALW) And (Abs(sp2.y - ep2.y) <= ALW)) Then
            Exit Function
        End If
        andLine = ZOK
        If (Abs(sp1.x - ep1.x) > ALW And (sp1.x - ep1.x) * (sp2.x - ep2.x) > 0) Or _
           (Abs(sp1.y - ep1.y) > ALW And (sp1.y - ep1.y) * (sp2.y - ep2.y) > 0) Then
            st_p = New gPoint(sp2)
            ed_p = New gPoint(ep2)
        Else
            st_p = New gPoint(ep2)
            ed_p = New gPoint(sp2)
        End If
        chkPointToLine(sp1, st_p, ed_p, op, dist, irc1)
        If (Abs(dist) > ALW) Then
            ret = 1
            sout.Copy(sp1)
            eout.Copy(ep1)
            Exit Function
        End If
        chkPointToLine(ep1, st_p, ed_p, op, dist, irc2)
        If (Abs(dist) > ALW) Then
            ret = 1
            sout.Copy(sp1)
            eout.Copy(ep1)
            Exit Function
        End If
        chkPointToLine(st_p, sp1, ep1, op, dist, irc3)
        chkPointToLine(ed_p, sp1, ep1, op, dist, irc4)
        If (irc1 = 0 And irc2 = 0 And irc3 = 0 And irc4 = 0) Then
            ret = 2
            Exit Function
        End If

        sout.Copy(sp1)
        eout.Copy(ep1)
        If mode <> 0 Then
            If (irc1 <> 0) Then
                sout.Copy(st_p)
            End If
            If (irc2 <> 0) Then
                eout.Copy(ed_p)
            End If
        Else
            If (irc1 = 0) Then
                sout.Copy(st_p)
            End If
            If (irc2 = 0) Then
                eout.Copy(ed_p)
            End If
        End If
        If (irc1 = 0 And irc2 = 1 And irc3 = 1 And irc4 = 0) Then
            ret = 3
        ElseIf (irc1 = 1 And irc2 = 0 And irc3 = 0 And irc4 = 1) Then
            ret = 3
        End If
    End Function

    '点が線上にあるか判定する
    ' irc   : ０：線上にない，１：端点と一致，２：線上で端点と一致しない
    Public Function chkPointOnLine(ByVal cp As gPoint, ByVal sp As gPoint, ByRef ep As gPoint, ByRef irc As Integer) As Integer
        Dim dist, vmin, vmax, cost, sint, wx, wy As Double

        irc = 0
        chkPointOnLine = ZERR
        If getDist(sp, ep, dist) <> ZOK Then
            Exit Function
        End If
        If (Abs(ep.x - sp.x) < ALW) Then
            If (Abs(cp.x - sp.x) < ALW) Then
                vmax = Max(sp.y, ep.y)
                vmin = Min(sp.y, ep.y)
                If ((vmin + ALW) < cp.y And cp.y < (vmax - ALW)) Then
                    irc = 2
                End If
                If ((Abs(cp.y - vmin) <= ALW) Or (Abs(cp.y - vmax) <= ALW)) Then
                    irc = 1
                End If
            End If
        ElseIf (Abs(ep.y - sp.y) < ALW) Then
            If (Abs(cp.y - sp.y) < ALW) Then
                vmax = Max(sp.x, ep.x)
                vmin = Min(sp.x, ep.x)
                If ((vmin + ALW) < cp.x And cp.x < (vmax - ALW)) Then
                    irc = 2
                End If
                If ((Abs(cp.x - vmin) <= ALW) Or (Abs(cp.x - vmax) <= ALW)) Then
                    irc = 1
                End If
            End If
        Else
            cost = (ep.x - sp.x) / dist
            sint = (ep.y - sp.y) / dist
            wx = (cp.x - sp.x) * cost + (cp.y - sp.y) * sint
            wy = (sp.x - cp.x) * sint + (cp.y - sp.y) * cost
            If (Abs(wy) < ALW) Then
                If ((ALW < wx) And (wx < (dist - ALW))) Then
                    irc = 2
                End If
                If ((Abs(wx) <= ALW) Or (Abs(wx - dist) <= ALW)) Then
                    irc = 1
                End If
            End If
        End If
        chkPointOnLine = ZOK
    End Function

    '線の始点からｒｌ，垂直方向にｒｖ行った２点を求める
    Public Function getPoint(ByVal sp As gPoint, ByVal ep As gPoint, ByVal rh As Double, ByVal rv As Double, _
                                    ByVal p_left As gPoint, ByVal p_right As gPoint, ByVal p_on As gPoint) As Integer
        Dim dist As Double

        getPoint = ZERR
        If getDist(sp, ep, dist) <> ZOK Then
            Exit Function
        End If
        If (Abs(ep.x - sp.x) < ALW) Then
            If (ep.y < sp.y) Then
                p_left.x = sp.x + rv
                p_left.y = sp.y - rh
                p_right.x = sp.x - rv
                p_right.y = p_left.y
                p_on.x = sp.x
                p_on.y = p_left.y
            Else
                p_left.x = sp.x - rv
                p_left.y = sp.y + rh
                p_right.x = sp.x + rv
                p_right.y = p_left.y
                p_on.x = sp.x
                p_on.y = p_left.y
            End If
        ElseIf (Abs(ep.y - sp.y) < ALW) Then
            If (ep.x < sp.x) Then
                p_left.x = sp.x - rh
                p_left.y = sp.y - rv
                p_right.x = p_left.x
                p_right.y = sp.y + rv
                p_on.x = p_left.x
                p_on.y = sp.y
            Else
                p_left.x = sp.x + rh
                p_left.y = sp.y + rv
                p_right.x = p_left.x
                p_right.y = sp.y - rv
                p_on.x = p_left.x
                p_on.y = sp.y
            End If
        Else
            Dim ex As Double = (ep.x - sp.x) / dist
            Dim ey As Double = (ep.y - sp.y) / dist
            p_on.x = sp.x + (rh * ex)
            p_on.y = sp.y + (rh * ey)
            p_left.x = p_on.x - (rv * ey)
            p_left.y = p_on.y + (rv * ex)
            p_right.x = p_on.x + (rv * ey)
            p_right.y = p_on.y - (rv * ex)
        End If
    End Function

    '線分の交点を得る
    ' mode  : ０：線分上のみ，１：延長線もみる
    ' irc   : ０：あり，１：なし
    Public Function getPoint(ByVal mode As Integer, ByVal line1 As gLine, ByVal line2 As gLine, ByRef op As gPoint, ByRef irc As Integer) As Integer
        getPoint = getPoint(mode, line1.sp, line1.ep, line2.sp, line2.ep, op, irc)
    End Function

    Public Function getPoint(ByVal mode As Integer, ByVal sp1 As gPoint, ByVal ep1 As gPoint, ByVal sp2 As gPoint, ByVal ep2 As gPoint, _
                              ByRef op As gPoint, ByRef irc As Integer) As Integer
        getPoint = ZOK
        op.x = sp2.x
        op.y = sp2.y
        If mode = 0 Then
            If (Max(sp1.x, ep1.x) + ALW < Min(sp2.x, ep2.x)) Or (Max(sp2.x, ep2.x) + ALW < Min(sp1.x, ep1.x)) Or _
               (Max(sp1.y, ep1.y) + ALW < Min(sp2.y, ep2.y)) Or (Max(sp2.y, ep2.y) + ALW < Min(sp1.y, ep1.y)) Then
                irc = 1
                Exit Function
            End If
        End If
        Dim is_v1 As Boolean = (Abs(sp1.x - ep1.x) <= ALW)
        Dim is_l1 As Boolean = (Abs(sp1.y - ep1.y) <= ALW)
        If is_l1 Or is_v1 Then
            Dim is_v2 As Boolean = (Abs(sp2.x - ep2.x) <= ALW)
            Dim is_l2 As Boolean = (Abs(sp2.y - ep2.y) <= ALW)
            If is_l2 Or is_v2 Then
                If (is_l1 And is_l2) Or (is_v1 And is_v2) Then
                    irc = 1
                Else
                    If is_l1 Then
                        op.y = sp1.y
                    End If
                    If is_v1 Then
                        op.x = sp1.x
                    End If
                    irc = 0
                End If
                Exit Function
            End If
        End If

        Dim dx1 As Double = ep1.x - sp1.x
        Dim dx2 As Double = ep2.x - sp2.x
        Dim dy1 As Double = ep1.y - sp1.y
        Dim dy2 As Double = ep2.y - sp2.y
        Dim a1, b1, c1, a2, b2, c2 As Double
        getParm(sp1, ep1, a1, b1, c1)
        getParm(sp2, ep2, a2, b2, c2)
        If (Abs(a1 * b2 - a2 * b1) < ZERO) Then
            irc = 1
            Exit Function
        End If
        Dim wbnb As Double = dx1 * dy2 - dx2 * dy1
        If (Abs(wbnb) < ZERO) Then
            irc = 1
            Exit Function
        End If
        Dim wxy1 As Double = ep1.x * sp1.y - sp1.x * ep1.y
        Dim wxy2 As Double = ep2.x * sp2.y - sp2.x * ep2.y
        op.x = (dx2 * wxy1 - dx1 * wxy2) / wbnb
        op.y = (dy2 * wxy1 - dy1 * wxy2) / wbnb
        irc = 0
        If mode = 0 Then
            Dim dp As New gPoint
            Dim dist As Double
            Dim ret, ion As Integer
            ret = chkPointToLine(op, sp1, ep1, dp, dist, ion)
            If (ret <> ZOK) Or (ion = 0) Or (dist > ALW) Then
                irc = 1
            End If
            ret = chkPointToLine(op, sp2, ep2, dp, dist, ion)
            If (ret <> ZOK) Or (ion = 0) Or (dist > ALW) Then
                irc = 1
            End If
        End If

    End Function

    Public Function getParm(ByVal sp As gPoint, ByVal ep As gPoint, ByRef a As Double, ByRef b As Double, ByRef c As Double) As Integer
        Dim dist As Double

        getParm = ZERR
        If getDist(sp, ep, dist) <> ZOK Then
            Exit Function
        End If
        a = (sp.y - ep.y) / dist
        b = (ep.x - sp.x) / dist
        c = (ep.x * sp.y - ep.y * sp.x) / dist
        If c < 0.0 Then
            a *= (-1.0)
            b *= (-1.0)
            c *= (-1.0)
        End If
    End Function

    '線分群の方向を得る
    ' muki	[O] 線の向き 上：１、右：２、下：３、左：４、斜め：５，同一点：０
    Public Function chkLineDir(ByVal sp As gPoint, ByVal ep As gPoint, ByRef muki As Integer) As Integer
        ' Ｙ軸に平行
        If (Abs(sp.x - ep.x) <= ALW) Then
            If (Abs(sp.y - ep.y) <= ALW) Then
                muki = 0        ' 同一点
            ElseIf (sp.y > ep.y) Then
                muki = 3        ' 下
            Else
                muki = 1        ' 上
            End If
            ' Ｘ軸に平行
        ElseIf (Abs(sp.y - ep.y) <= ALW) Then
            If (sp.x > ep.x) Then
                muki = 4        ' 左
            Else
                muki = 2        ' 右
            End If
        Else
            muki = 5            ' 斜め
        End If
        chkLineDir = ZOK
    End Function

    '中間点の状態を得る
    ' p1,p2,p3  [I] 始点, 中間点, 終点
    ' muki	    [O] １：出隅，２：入隅３：孤立，４：不通，５：重複
    Public Function chkPoint(ByVal p1 As gPoint, ByVal p2 As gPoint, ByVal p3 As gPoint, ByRef irc As Integer) As Integer
        Dim muk1, muk2 As Integer
        Dim ret, istat, ier As Integer

        ier = ZOK
        chkLineDir(p2, p1, muk1)
        If muk1 = 0 Then
            istat = 5
        Else
            ret = chkPointToLine(p1, p3, p2, istat)
            If ret <> ZOK Then
                chkLineDir(p2, p3, muk2)
                If (muk2 = 0) Then
                    ier = ZERR
                End If
                If ret = 1 And (muk1 & muk2) = 0 Then
                    istat = 4
                Else
                    istat = 3
                End If
            End If
        End If
        irc = istat
        chkPoint = ier
    End Function

    '面積を求める
    Public Function getArea(ByVal cnt As Integer, ByVal src_p() As gPoint) As Double
        Dim area As Double
        getArea(cnt, src_p, area)
        getArea = area
    End Function

    Public Function getArea(ByVal cnt As Integer, ByVal src_p() As gPoint, ByRef area As Double) As Integer
        area = 0
        For i As Integer = 0 To cnt - 2
            area += (src_p(i + 1).y + src_p(i).y) * (src_p(i + 1).x - src_p(i).x) / 2
        Next
        If (area > 0) Then
            getArea = ZRIGHT
        Else
            getArea = ZLEFT
        End If
        area = Abs(area)
    End Function

    '閉ループと点の関係を調べる
    ' stat    [O] １：外，２：内，３：辺上,４：端点上
    Public Function chkPoint(ByVal cnt As Integer, ByVal hlp() As gPoint, ByVal pnt As gPoint, ByRef stat As Integer) As Integer
        chkPoint = ZOK
        Dim r, xyr, rr, angle, sangl, aangl As Double
        Dim abx, aby As Double
        Dim i, iixy, ierr, ion As Integer
        Dim wpnt(hlp.GetLength(0) - 1) As gPoint

        Dim no As Integer = 0
        Dim ixy As Integer = 0
        cpyPoint(cnt, hlp, wpnt)
        chgRotate(0, cnt, wpnt)
        Dim rm As Double = 20000.0
        Dim xmax As Double = wpnt(0).x
        Dim ymax As Double = wpnt(0).y
        Dim xmin As Double = xmax
        Dim ymin As Double = ymax
        stat = 1
        For i = 1 To cnt - 1
            xmax = Max(xmax, wpnt(i).x)
            ymax = Max(ymax, wpnt(i).y)
            xmin = Min(xmin, wpnt(i).x)
            ymin = Min(ymin, wpnt(i).y)
        Next
        If (pnt.x > xmax + ALW) Or (pnt.y > ymax + ALW) Or (pnt.x < xmin - ALW) Or (pnt.y < ymin - ALW) Then
            Exit Function
        End If

        Dim sflg As Integer = 0
        For i = 0 To cnt - 2
            abx = Abs(wpnt(i).x - wpnt(i + 1).x)
            aby = Abs(wpnt(i).y - wpnt(i + 1).y)
            If abx > ALW And aby > ALW Then
                sflg = 1
                Exit For
            End If
        Next

        If sflg = 0 Then
            For i = 0 To cnt - 2
                abx = Abs(wpnt(i).x - wpnt(i + 1).x)
                aby = Abs(wpnt(i).y - wpnt(i + 1).y)
                If abx < ALW And aby < ALW Then
                    Continue For
                End If
                If abx < ALW Then
                    ymax = Max(wpnt(i).y, wpnt(i + 1).y)
                    ymin = Min(wpnt(i).y, wpnt(i + 1).y)
                    If (pnt.y > ymax + ALW Or pnt.y < ymin - ALW) Then
                        Continue For
                    End If
                    If Abs(pnt.x - wpnt(i).x) <= ALW Then
                        If (Abs(wpnt(i).y - pnt.y) <= ALW Or Abs(wpnt(i + 1).y - pnt.y) <= ALW) Then
                            stat = 4
                        Else
                            stat = 3
                        End If
                        Exit Function
                    End If
                    r = wpnt(i).x - pnt.x
                    iixy = 1
                Else
                    xmax = Max(wpnt(i).x, wpnt(i + 1).x)
                    xmin = Min(wpnt(i).x, wpnt(i + 1).x)
                    If (pnt.x > xmax + ALW Or pnt.x < xmin - ALW) Then
                        Continue For
                    End If
                    If (Abs(pnt.y - wpnt(i).y) <= ALW) Then
                        If (Abs(wpnt(i).x - pnt.x) <= ALW Or Abs(wpnt(i + 1).x - pnt.x) <= ALW) Then
                            stat = 4
                        Else
                            stat = 3
                        End If
                        Exit Function
                    End If
                    r = pnt.y - wpnt(i).y
                    iixy = 0
                End If
                If Abs(r) < Abs(rm) Then
                    rm = r
                    no = i
                    ixy = iixy
                End If
            Next
            If (rm < 20000.0) Then
                xyr = wpnt(no).x - wpnt(no + 1).x
                If (ixy = 1) Then
                    xyr = wpnt(no).y - wpnt(no + 1).y
                End If
                rr = rm * xyr
                stat = 1
                If (rr > 0) Then
                    stat = 2
                End If
            End If
        Else
            angle = 0.0
            For i = 0 To cnt - 2
                If (Abs(wpnt(i).x - wpnt(i + 1).x) < ALW And Abs(wpnt(i).y - wpnt(i + 1).y) < ALW) Then
                    Continue For
                End If
                stat = getAngle(wpnt(i), pnt, wpnt(i + 1), sangl, aangl)
                If (stat <> 0) Then
                    stat = 4
                    Exit Function
                End If
                Dim ass As Double = Abs(sangl)
                If (Abs(180.0 - ass) < 0.01) Then
                    stat = 3
                    Exit Function
                End If
                If (ass < 170.0) Then
                    angle = angle + sangl
                    Continue For
                End If
                Dim op As New gPoint
                ierr = chkPointToLine(pnt, wpnt(i), wpnt(i + 1), op, r, ion)
                If (ion <> 0 And r < ALW) Then
                    stat = 3
                    Exit Function
                End If
            Next
            If (Abs(angle) > 180.0) Then
                stat = 2
            Else
                stat = 1
            End If
        End If
    End Function

    '閉ループと線分の関係を調べる
    ' iout    [O] 出力線分数
    ' oline   [O] 出力線分
    ' stat    [O] １：外，２：内，３：辺上
    Public Function chkLine(ByVal cnt As Integer, ByVal hlp() As gPoint, ByVal line As gLine, _
                            ByRef iout As Integer, ByRef oline() As gLine, ByRef stat() As Integer) As Integer
        Dim i, n, mode, ier As Integer
        Dim ic, ixy, ir, irc As Integer
        Dim crs(OUTMAX) As gPoint
        Dim wk As Double
        Dim cx, cy As Double

        ic = 0
        For n = 0 To cnt - 2
            crs(ic) = New gPoint
            getPoint(0, hlp(n), hlp(n + 1), line.sp, line.ep, crs(ic), ir)
            If ir = 0 Then
                If ic >= OUTMAX - 2 Then
                    Exit For
                End If
                ic += 1
            End If
        Next
        crs(ic) = New gPoint
        crs(ic).Copy(line.sp)
        crs(ic + 1) = New gPoint
        crs(ic + 1).Copy(line.ep)
        ic += 2
        If ic <> 2 Then
            ixy = 1
            If (Abs(line.sp.x - line.ep.x) < Abs(line.sp.y - line.ep.y)) Then
                ixy = 2
                For i = 0 To ic - 1
                    wk = crs(i).x
                    crs(i).x = crs(i).y
                    crs(i).y = wk
                Next
            End If
            mode = 0
            If (crs(ic - 2).x > crs(ic - 1).x) Then
                mode = 1
            End If
            sortPoint(mode, ic, crs)
            If ixy = 2 Then
                For i = 0 To ic - 1
                    wk = crs(i).x
                    crs(i).x = crs(i).y
                    crs(i).y = wk
                Next
            End If
        End If

        iout = 0
        ier = ZOK
        For n = 0 To ic - 2
            If Not (Abs(crs(n).x - crs(n + 1).x) <= ALW And Abs(crs(n).y - crs(n + 1).y) <= ALW) Then
                If (iout + 1 > OUTMAX) Then
                    ier = ZERR
                    Exit For
                End If
                If oline(iout) Is Nothing Then
                    oline(iout) = New gLine
                End If
                oline(iout).sp.Copy(crs(n))
                oline(iout).ep.Copy(crs(n + 1))
                iout += 1
            End If
        Next

        For n = 0 To iout - 1
            cx = (oline(n).sp.x + oline(n).ep.x) / 2.0
            cy = (oline(n).sp.y + oline(n).ep.y) / 2.0
            Dim dp As New gPoint(cx, cy)
            chkPoint(cnt, hlp, dp, irc)
            stat(n) = irc
        Next
        chkLine = ier
    End Function

    '相対角度、絶対角度を求める
    Public Function getAngle(ByVal p1 As gPoint, ByVal p2 As gPoint, ByVal p3 As gPoint, ByRef sang As Double, ByRef aang As Double) As Integer
        Dim d1 As Double = 1.0
        Dim x32 As Double = p3.x - p2.x
        Dim y32 As Double = p3.y - p2.y
        Dim x13 As Double = p1.x - p3.x
        Dim y13 As Double = p1.y - p3.y
        Dim x21 As Double = p2.x - p1.x
        Dim y21 As Double = p2.y - p1.y

        getAngle = 1
        sang = 0.0
        aang = 0.0
        Dim a As Double = x32 * x32 + y32 * y32
        If (Abs(x32) < ALW And Abs(y32) < ALW) Then
            Exit Function
        End If
        Dim b As Double = x13 * x13 + y13 * y13
        If (Abs(x13) < ALW And Abs(y13) < ALW) Then
            Exit Function
        End If
        Dim c As Double = x21 * x21 + y21 * y21
        If (Abs(x21) < ALW And Abs(y21) < ALW) Then
            Exit Function
        End If

        getAngle = 0
        aang = (x13 * p2.y - y13 * p2.x + (p3.x * p1.y - p1.x * p3.y))
        aang = aang / Sqrt(b)
        If Abs(aang) >= ALW Then
            Dim d As Double = ((a + c - b) / (2.0 * Sqrt(a * c)))
            If (d < -d1) Then d = -d1
            If (d > d1) Then d = d1
            sang = Acos(d)
            sang = (sang * 180.0 / PI)
            aang = (y21 * p3.x - x21 * p3.y + (p2.x * p1.y - p1.x * p2.y))
            aang = aang / Sqrt(c)
        End If
        If (Abs(aang) < ALW) Then
            aang = (x21 * x32 + y21 * y32)
            If (aang < 0.0) Then
                sang = 0.0
                aang = 0.0
            Else
                sang = 180.0
                aang = sang
            End If
        ElseIf (aang < 0.0) Then
            sang = -(sang)
            aang = 360.0 + sang
        Else
            aang = sang
        End If
    End Function

    '閉ループの回転方向を変える
    ' mode    [I] 回転方向（０：右，その他：左）
    Public Function chgRotate(ByVal mode As Integer, ByVal cnt As Integer, ByRef pnt() As gPoint) As Integer
        chgRotate = ZOK
        If mode = getRotate(cnt, pnt) Then
            Exit Function
        End If
        For i As Integer = 0 To CInt(cnt / 2) - 1
            chgPoint(pnt(i), pnt(cnt - i - 1))
        Next
    End Function

    '閉ループの回転方向を求める
    Public Function getRotate(ByVal cnt As Integer, ByVal src_p() As gPoint) As Integer
        Dim area As Double
        getRotate = getArea(cnt, src_p, area)
    End Function

    '点列コピー
    Public Function cpyPoint(ByVal cnt As Integer, ByVal src_p() As gPoint, ByRef dst_p() As gPoint) As Integer
        For i As Integer = 0 To cnt - 1
            If dst_p(i) Is Nothing Then
                dst_p(i) = New gPoint
            End If
            dst_p(i).Copy(src_p(i))
        Next
    End Function

    Public Function cpyPoint(ByVal cnt As Integer, ByVal src_p() As gPoint, ByRef dst_p(,) As Double) As Integer
        For i As Integer = 0 To cnt - 1
            dst_p(i, 0) = src_p(i).x
            dst_p(i, 1) = src_p(i).y
        Next
    End Function

    '２点を入れ替える
    Public Function chgPoint(ByRef p1 As gPoint, ByRef p2 As gPoint) As Integer
        Dim dp As New gPoint(p1)
        p1.Copy(p2)
        p2.Copy(dp)
    End Function

    '点群を昇順または降順にソートする
    ' mode    [I] ０：昇順，その他：降順
    Public Function sortPoint(ByVal mode As Integer, ByVal cnt As Integer, ByRef pnt() As gPoint) As Integer
        Dim dx, dy As Double
        Dim f_chg As Integer

        sortPoint = ZOK
        If cnt <= 1 Then
            Exit Function
        End If

        For i As Integer = 0 To cnt - 2
            For j As Integer = i + 1 To cnt - 1
                dx = pnt(i).x - pnt(j).x
                dy = pnt(i).y - pnt(j).y
                f_chg = 0
                If mode <> 0 Then
                    If Abs(dx) < ALW Then
                        If dy < -ALW Then
                            f_chg = 1
                        End If
                    Else
                        If dx < 0 Then
                            f_chg = 1
                        End If
                    End If
                Else
                    If Abs(dx) < ALW Then
                        If dy > ALW Then
                            f_chg = 1
                        End If
                    Else
                        If dx > 0 Then
                            f_chg = 1
                        End If
                    End If
                End If
                If f_chg = 1 Then
                    chgPoint(pnt(i), pnt(j))
                End If
            Next
        Next
    End Function

    Public Sub test(ByVal p As gPoint)
        p.x = 100.0
        p.y = 100.0
    End Sub
End Class

' 点データクラス
Public Class gPoint
    Public x As Double
    Public y As Double
    Public Sub New()
    End Sub
    Public Sub New(ByVal xx As Double, ByVal yy As Double)
        x = xx
        y = yy
    End Sub
    Public Sub New(ByVal p As gPoint)
        x = p.x
        y = p.y
    End Sub
    Public Sub Copy(ByVal p As gPoint)
        x = p.x
        y = p.y
    End Sub
    Public Shared Operator +(ByVal a As gPoint, ByVal b As gPoint) As gPoint
        Return New gPoint(a.x + b.x, a.y + b.y)
    End Operator
End Class

' 直線データクラス
Public Class gLine
    Public sp As gPoint
    Public ep As gPoint
    Public Sub New()
        sp = New gPoint
        ep = New gPoint
    End Sub
    Public Sub New(ByVal p1 As gPoint, ByVal p2 As gPoint)
        sp = New gPoint(p1)
        ep = New gPoint(p2)
    End Sub
    Public Sub New(ByVal sx As Double, ByVal sy As Double, ByVal ex As Double, ByVal ey As Double)
        sp = New gPoint(sx, sy)
        ep = New gPoint(ex, ey)
    End Sub
End Class
