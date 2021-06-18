<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnLine = New System.Windows.Forms.Button()
        Me.pictGraph = New System.Windows.Forms.PictureBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnPoint = New System.Windows.Forms.Button()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.btnAndLine = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoPoly1 = New System.Windows.Forms.RadioButton()
        Me.rdoLine2 = New System.Windows.Forms.RadioButton()
        Me.rdoPoint = New System.Windows.Forms.RadioButton()
        Me.rdoLine1 = New System.Windows.Forms.RadioButton()
        Me.chkAnd = New System.Windows.Forms.CheckBox()
        Me.btnCutLine = New System.Windows.Forms.Button()
        Me.btnGetpoint = New System.Windows.Forms.Button()
        Me.btnCross = New System.Windows.Forms.Button()
        Me.btnArea = New System.Windows.Forms.Button()
        Me.btnHlpPnt = New System.Windows.Forms.Button()
        Me.btnHlpLine = New System.Windows.Forms.Button()
        CType(Me.pictGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLine
        '
        Me.btnLine.Location = New System.Drawing.Point(12, 28)
        Me.btnLine.Name = "btnLine"
        Me.btnLine.Size = New System.Drawing.Size(69, 28)
        Me.btnLine.TabIndex = 0
        Me.btnLine.Text = "moveLine"
        Me.btnLine.UseVisualStyleBackColor = True
        '
        'pictGraph
        '
        Me.pictGraph.BackColor = System.Drawing.Color.White
        Me.pictGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictGraph.Location = New System.Drawing.Point(201, 62)
        Me.pictGraph.Name = "pictGraph"
        Me.pictGraph.Size = New System.Drawing.Size(374, 327)
        Me.pictGraph.TabIndex = 1
        Me.pictGraph.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(517, 28)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(69, 28)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnPoint
        '
        Me.btnPoint.Location = New System.Drawing.Point(12, 62)
        Me.btnPoint.Name = "btnPoint"
        Me.btnPoint.Size = New System.Drawing.Size(69, 28)
        Me.btnPoint.TabIndex = 3
        Me.btnPoint.Text = "chkPoint"
        Me.btnPoint.UseVisualStyleBackColor = True
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(412, 12)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.Size = New System.Drawing.Size(70, 19)
        Me.txtResult.TabIndex = 6
        '
        'btnAndLine
        '
        Me.btnAndLine.Location = New System.Drawing.Point(12, 96)
        Me.btnAndLine.Name = "btnAndLine"
        Me.btnAndLine.Size = New System.Drawing.Size(69, 28)
        Me.btnAndLine.TabIndex = 8
        Me.btnAndLine.Text = "andLine"
        Me.btnAndLine.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPoly1)
        Me.GroupBox1.Controls.Add(Me.rdoLine2)
        Me.GroupBox1.Controls.Add(Me.rdoPoint)
        Me.GroupBox1.Controls.Add(Me.rdoLine1)
        Me.GroupBox1.Location = New System.Drawing.Point(108, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 45)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'rdoPoly1
        '
        Me.rdoPoly1.AutoSize = True
        Me.rdoPoly1.Location = New System.Drawing.Point(189, 18)
        Me.rdoPoly1.Name = "rdoPoly1"
        Me.rdoPoly1.Size = New System.Drawing.Size(63, 16)
        Me.rdoPoly1.TabIndex = 11
        Me.rdoPoly1.TabStop = True
        Me.rdoPoly1.Text = "Polyline"
        Me.rdoPoly1.UseVisualStyleBackColor = True
        '
        'rdoLine2
        '
        Me.rdoLine2.AutoSize = True
        Me.rdoLine2.Location = New System.Drawing.Point(75, 18)
        Me.rdoLine2.Name = "rdoLine2"
        Me.rdoLine2.Size = New System.Drawing.Size(50, 16)
        Me.rdoLine2.TabIndex = 10
        Me.rdoLine2.TabStop = True
        Me.rdoLine2.Text = "Line2"
        Me.rdoLine2.UseVisualStyleBackColor = True
        '
        'rdoPoint
        '
        Me.rdoPoint.AutoSize = True
        Me.rdoPoint.Location = New System.Drawing.Point(134, 18)
        Me.rdoPoint.Name = "rdoPoint"
        Me.rdoPoint.Size = New System.Drawing.Size(49, 16)
        Me.rdoPoint.TabIndex = 9
        Me.rdoPoint.TabStop = True
        Me.rdoPoint.Text = "Point"
        Me.rdoPoint.UseVisualStyleBackColor = True
        '
        'rdoLine1
        '
        Me.rdoLine1.AutoSize = True
        Me.rdoLine1.Location = New System.Drawing.Point(15, 18)
        Me.rdoLine1.Name = "rdoLine1"
        Me.rdoLine1.Size = New System.Drawing.Size(50, 16)
        Me.rdoLine1.TabIndex = 8
        Me.rdoLine1.TabStop = True
        Me.rdoLine1.Text = "Line1"
        Me.rdoLine1.UseVisualStyleBackColor = True
        '
        'chkAnd
        '
        Me.chkAnd.AutoSize = True
        Me.chkAnd.Location = New System.Drawing.Point(98, 103)
        Me.chkAnd.Name = "chkAnd"
        Me.chkAnd.Size = New System.Drawing.Size(44, 16)
        Me.chkAnd.TabIndex = 11
        Me.chkAnd.Text = "And"
        Me.chkAnd.UseVisualStyleBackColor = True
        '
        'btnCutLine
        '
        Me.btnCutLine.Location = New System.Drawing.Point(12, 130)
        Me.btnCutLine.Name = "btnCutLine"
        Me.btnCutLine.Size = New System.Drawing.Size(69, 28)
        Me.btnCutLine.TabIndex = 12
        Me.btnCutLine.Text = "cutLine"
        Me.btnCutLine.UseVisualStyleBackColor = True
        '
        'btnGetpoint
        '
        Me.btnGetpoint.Location = New System.Drawing.Point(12, 164)
        Me.btnGetpoint.Name = "btnGetpoint"
        Me.btnGetpoint.Size = New System.Drawing.Size(69, 28)
        Me.btnGetpoint.TabIndex = 13
        Me.btnGetpoint.Text = "getPoint"
        Me.btnGetpoint.UseVisualStyleBackColor = True
        '
        'btnCross
        '
        Me.btnCross.Location = New System.Drawing.Point(12, 198)
        Me.btnCross.Name = "btnCross"
        Me.btnCross.Size = New System.Drawing.Size(69, 28)
        Me.btnCross.TabIndex = 14
        Me.btnCross.Text = "交点"
        Me.btnCross.UseVisualStyleBackColor = True
        '
        'btnArea
        '
        Me.btnArea.Location = New System.Drawing.Point(12, 232)
        Me.btnArea.Name = "btnArea"
        Me.btnArea.Size = New System.Drawing.Size(69, 28)
        Me.btnArea.TabIndex = 15
        Me.btnArea.Text = "面積"
        Me.btnArea.UseVisualStyleBackColor = True
        '
        'btnHlpPnt
        '
        Me.btnHlpPnt.Location = New System.Drawing.Point(12, 266)
        Me.btnHlpPnt.Name = "btnHlpPnt"
        Me.btnHlpPnt.Size = New System.Drawing.Size(69, 28)
        Me.btnHlpPnt.TabIndex = 16
        Me.btnHlpPnt.Text = "閉－点"
        Me.btnHlpPnt.UseVisualStyleBackColor = True
        '
        'btnHlpLine
        '
        Me.btnHlpLine.Location = New System.Drawing.Point(12, 300)
        Me.btnHlpLine.Name = "btnHlpLine"
        Me.btnHlpLine.Size = New System.Drawing.Size(69, 28)
        Me.btnHlpLine.TabIndex = 17
        Me.btnHlpLine.Text = "閉－線"
        Me.btnHlpLine.UseVisualStyleBackColor = True
        '
        'fmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 414)
        Me.Controls.Add(Me.btnHlpLine)
        Me.Controls.Add(Me.btnHlpPnt)
        Me.Controls.Add(Me.btnArea)
        Me.Controls.Add(Me.btnCross)
        Me.Controls.Add(Me.btnGetpoint)
        Me.Controls.Add(Me.btnCutLine)
        Me.Controls.Add(Me.chkAnd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnAndLine)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.btnPoint)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.pictGraph)
        Me.Controls.Add(Me.btnLine)
        Me.Name = "fmMain"
        Me.Text = "Form1"
        CType(Me.pictGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLine As System.Windows.Forms.Button
    Friend WithEvents pictGraph As System.Windows.Forms.PictureBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnPoint As System.Windows.Forms.Button
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents btnAndLine As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoLine2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPoint As System.Windows.Forms.RadioButton
    Friend WithEvents rdoLine1 As System.Windows.Forms.RadioButton
    Friend WithEvents chkAnd As System.Windows.Forms.CheckBox
    Friend WithEvents btnCutLine As System.Windows.Forms.Button
    Friend WithEvents btnGetpoint As System.Windows.Forms.Button
    Friend WithEvents btnCross As System.Windows.Forms.Button
    Friend WithEvents btnArea As System.Windows.Forms.Button
    Friend WithEvents btnHlpPnt As System.Windows.Forms.Button
    Friend WithEvents rdoPoly1 As System.Windows.Forms.RadioButton
    Friend WithEvents btnHlpLine As System.Windows.Forms.Button

End Class
