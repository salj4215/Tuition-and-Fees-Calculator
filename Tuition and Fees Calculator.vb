'HFC Calculator application
'Program that calculates tuition and total fees of students attending HFC
'Developed by Salleh Jahaf
'Version 1.0
'Updated on 10/08/2015
Option Strict On

Public Class frmTFCalculator

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblTotalFee_Click(sender As Object, e As EventArgs) Handles lblTotalFee.Click

    End Sub
    'Exit Button
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Close the application when user clicks exit
        Me.Close()

    End Sub
    'Calculate button
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        'Declare constants
        Const intADMISSION_APPLICATION_FEE As Integer = 30
        Const intREGISTRATION_FEE As Integer = 46
        Const intINFRASTRUCTURE_FEE As Integer = 45
        Const intDEARBORN_TUITION_RATE As Integer = 92
        Const intOUT_OF_DISTRICT_TUITION_RATE As Integer = 158
        Const intOUT_OF_STATE_INTERNATIONAL_RATE As Integer = 225
        Const intSERVICE_FEE_RATE As Integer = 15
        Const intTECH_INVESTMENT_FEE_RATE As Integer = 2

        'Declare variables
        Dim intCreditHours As Integer
        Dim intCourseFees As Integer
        Dim intLabFees As Integer
        Dim intTotalFees As Integer
        Dim intTuitionTotal As Integer
        Dim intFinalTotal As Integer
        Dim intTuitionRate As Integer

        'Convert input into numbers, then assign to variables
        Integer.TryParse(txtCourseFees.Text, intCourseFees)
        Integer.TryParse(txtCreditHours.Text, intCreditHours)
        Integer.TryParse(txtLabFees.Text, intLabFees)

        'Code message box to show if no radio buttons are selected by user
        If radDearbornResident.Checked = False And radOutOfDistrict.Checked = False And radInternational.Checked = False Then
            MessageBox.Show("Please choose your status", "Henry Ford College", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        'Code message box to show if user name is not entered
        If txtStudentName.Text = String.Empty Then
            MessageBox.Show("Please enter your name", "Henry Ford College", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtStudentName.Focus()
            Exit Sub
        End If
        'Code message box if invalid number of credit hours is entered
        If intCreditHours < 2 Or intCreditHours > 18 Then
            MessageBox.Show("Please enter correct number of credit hours. Only 2 to 18 credit hours are allowed", "Henry Ford College", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCreditHours.Focus()
            txtCreditHours.SelectAll()
        End If
        'Assign tuition rate based on radio button selection
        If radDearbornResident.Checked Then
            intTuitionRate = intDEARBORN_TUITION_RATE
        ElseIf radOutOfDistrict.Checked Then
            intTuitionRate = intOUT_OF_DISTRICT_TUITION_RATE
        ElseIf radInternational.Checked Then
            intTuitionRate = intOUT_OF_STATE_INTERNATIONAL_RATE
        End If
        'Calculate total fee and other fees
        intTotalFees = intCreditHours * intSERVICE_FEE_RATE + intCreditHours * intTECH_INVESTMENT_FEE_RATE + intCourseFees + intLabFees + intREGISTRATION_FEE + intINFRASTRUCTURE_FEE
        'Add application fee to total fee if new student checkbox is checked
        If chkNewStudent.Checked = True Then
            intTotalFees = intTotalFees + intADMISSION_APPLICATION_FEE
        End If
        intTuitionTotal = intCreditHours * intTuitionRate
        intFinalTotal = intTotalFees + intTuitionTotal
        'Send totals to label boxes
        lblTotalFee.Text = intTotalFees.ToString("N2")
        lblTuition.Text = intTuitionTotal.ToString("N2")
        lblTotalDue.Text = intFinalTotal.ToString("C2")


    End Sub

   


    'Code so if any characters other than numbers and backspace are not inputed into text boxes by user
    Private Sub Cancel_Keys(sender As Object, e As KeyPressEventArgs) Handles txtCourseFees.KeyPress,
        txtCreditHours.KeyPress, txtLabFees.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
    'Clear all outputs when text within text boxes are changed by user
    Private Sub Clear_Labels(sender As Object, e As EventArgs) Handles txtCourseFees.TextChanged, txtCreditHours.TextChanged,
        txtLabFees.TextChanged, txtStudentName.TextChanged, radDearbornResident.Click, radOutOfDistrict.Click, radInternational.Click,
        chkNewStudent.CheckedChanged

        lblTuition.Text = String.Empty
        lblTotalFee.Text = String.Empty
        lblTotalDue.Text = String.Empty

    End Sub
    'Clear button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        radDearbornResident.Checked = False
        radOutOfDistrict.Checked = False
        radInternational.Checked = False
        chkNewStudent.Checked = False
        txtStudentName.Text = String.Empty
        txtCreditHours.Text = String.Empty
        txtCourseFees.Text = String.Empty
        txtLabFees.Text = String.Empty
        lblTuition.Text = String.Empty
        lblTotalFee.Text = String.Empty
        lblTotalDue.Text = String.Empty
    End Sub
    'Print button
    Dim memoryImage As Bitmap
    Private Sub CaptureScreen()
        'this code will create a graphics image in memory and copy this form’s image to it

        Dim myGraphics As Graphics = Me.CreateGraphics()
        Dim s As Size = Me.Size
        memoryImage = New Bitmap(s.Width, s.Height, myGraphics)
        Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
        btnCalculate.visible = False
        btnClear.visible = False
        btnPrintForm.visible = False
        btnExit.visible = False
        memoryGraphics.CopyFromScreen(Me.Location.X, Me.Location.Y, 0, 0, s)
        btnCalculate.visible = True
        btnClear.visible = True
        btnPrintForm.visible = True
        btnExit.visible = True
    End Sub
    Private Sub prtPrintFormImage_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles prtPrintFormImage.PrintPage
        e.Graphics.DrawImage(memoryImage, 0, 0)
    End Sub
    Private Sub btnPrintForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintForm.Click
        CaptureScreen()
        prtPrintFormImage.Print()
    End Sub
End Class
