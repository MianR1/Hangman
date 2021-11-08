'***************************************************************************************************************************************************************************************************
' PROGRAMME	    :	Hangman
' OUTLINE		:	This program choses a random word based on diffculty and the user has to guess the word
' PROGRAMMER	:	Mian Rafay
' DATE		    :	December 26, 2019
' **************************************************************************************************************************************************************************************************
Imports System.IO
Public Class frmHangman
    Dim tstword As String
    Dim strout As String = Nothing
    Dim multiple As String
    Dim guessLeft As Integer
    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        Dim btnArray() As Button = {btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ}
        cmbDifficulty.Enabled = False
        btnPlay.Enabled = False
        lblLetters.Text = ""
        btnGuess.Visible = True
        btnChangelvl.Visible = True
        If cmbDifficulty.Text = "Easy" Then
            lblGuess.Text = 8
            guessLeft = 8
        ElseIf cmbDifficulty.Text = "Medium" Then
            lblGuess.Text = 10
            guessLeft = 10
        ElseIf cmbDifficulty.Text = "Hard" Then
            lblGuess.Text = 12
            guessLeft = 12
        End If
        For i As Integer = 0 To btnArray.Length - 1
            btnArray(i).Enabled = True
        Next
        strout = Nothing
        Dim rand As New Random
        Dim randNum As Integer
        Dim strarray() As String
        strarray = File.ReadAllLines(cmbDifficulty.Text & ".txt")
        randNum = rand.Next(0, strarray.Length)
        tstword = StrConv(strarray(randNum), vbUpperCase)
        multiple = tstword
        For Each x As Char In tstword
            strout &= "_ "
        Next
        lblWord.Text = strout
    End Sub
    Private Sub btnQ_Click(sender As Object, e As EventArgs) Handles btnA.Click, btnB.Click, btnC.Click, btnD.Click, btnE.Click, btnF.Click, btnG.Click, btnH.Click, btnI.Click, btnJ.Click, btnK.Click, btnL.Click, btnM.Click, btnN.Click, btnO.Click, btnP.Click, btnQ.Click, btnR.Click, btnS.Click, btnT.Click, btnU.Click, btnV.Click, btnW.Click, btnX.Click, btnY.Click, btnZ.Click
        Dim key As String
        Dim selected As Button = sender
        key = selected.Text
        selected.Enabled = False
        lblLetters.Text &= Space(1) & key
        If tstword.Contains(key) = True Then
            Do Until multiple.Contains(key) = False
                Dim chars() As Char
                Dim index As Integer
                index = multiple.IndexOf(key)
                Dim indexxx As Integer = 0
                For Each x As Char In strout
                    ReDim Preserve chars(indexxx)
                    chars(indexxx) = x
                    indexxx += 1
                Next
                chars(index * 2) = key
                strout = Nothing
                For i As Integer = 0 To chars.Length - 1
                    strout &= chars(i)
                Next

                lblWord.Text = strout
                Dim c As Integer = multiple.IndexOf(key)
                multiple = multiple.Remove(c, 1).Insert(c, "|")
            Loop
        Else
            guessLeft -= 1
        End If
        Dim win As Boolean = False

        For Each x As Char In multiple
            If x <> "|" Then
                win = True
            End If
        Next
        If win = False Then
            picAnimation.Image = My.Resources.fly
            MessageBox.Show("Congrats! You Win", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Application.Restart()
        End If
        lblGuess.Text = guessLeft
        If guessLeft = 0 Then
            Guess()
        End If
    End Sub
    Private Sub btnGuess_Click(sender As Object, e As EventArgs) Handles btnGuess.Click
        Guess()
    End Sub
    Private Sub Guess()
        Dim guessWrd As String = Nothing
        guessWrd = InputBox("Guess The Word", "Guess", "").ToUpper
        If guessWrd = tstword Then
            picAnimation.Image = My.Resources.fly
            MessageBox.Show("Congrats! You Win", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            picAnimation.Image = My.Resources.man_falling_and_dies
            MessageBox.Show("YOU LOSE! The correct word was: " & tstword, "FAILURE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Application.Restart()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub
    Private Sub btnChangelvl_Click(sender As Object, e As EventArgs) Handles btnChangelvl.Click
        Application.Restart()
    End Sub
    Private Sub CmbDifficulty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDifficulty.SelectedIndexChanged
        btnPlay.Enabled = True
    End Sub
End Class