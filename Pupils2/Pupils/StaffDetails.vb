Imports System.IO

Public Class StaffDetails

    ' Structure to hold staff data
    Private Structure Staff

        Public StaffID As String ' Used to uniquely identify a member of staff
        Public FirstName As String
        Public Surname As String
        Public EmailAddress As String
        Public PhoneNumber As String

    End Structure

    Private Sub StaffDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' If the textfile doesn't already exist
        If Dir$("staffdetails.txt") = "" Then

            ' Create the textfile
            Dim sw As New StreamWriter("staffdetails.txt", True)

            ' Write a '0' to it
            sw.WriteLine("0")

            ' Must be closed after being used
            sw.Close()

            ' Output that a new textfile has been created
            MsgBox("A new file has been created", vbExclamation, "Warning!")

        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim StaffData As New Staff

        Dim StaffsData() As String = File.ReadAllLines("staffdetails.txt")

        ' Procedure to generate the Staff ID
        GenerateID(StaffsData)

        ' If validation checks have been passed
        If Validation() = True Then

            Dim sw As New System.IO.StreamWriter("staffdetails.txt", True)

            ' Assign the data in the textboxes to the structure
            StaffData.StaffID = LSet(txtStaffID.Text, 4)
            StaffData.FirstName = LSet(txtFirstName.Text, 20)
            StaffData.Surname = LSet(txtSurname.Text, 20)
            StaffData.EmailAddress = LSet(txtEmailAddress.Text, 30)
            StaffData.PhoneNumber = LSet(txtPhoneNumber.Text, 11)

            ' Write the data of the member of staff to the textfile
            sw.WriteLine(StaffData.StaffID & StaffData.FirstName & StaffData.Surname & StaffData.EmailAddress & StaffData.PhoneNumber)

            ' Must be closed after being used
            sw.Close()

            ' Output that the file has been saved
            MsgBox("File Saved!")

            ' Clears the textboxes so new data can be entered
            ClearTextboxes()

        End If

    End Sub

    Private Sub cmdRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRead.Click

        ' Used to store the number of members of staff found
        Dim StaffCount As Integer
        StaffCount = 0

        ' Used to store the details of the member of staff if only one is found
        Dim OneStaff As Integer

        Dim StaffsData() As String = File.ReadAllLines("staffdetails.txt")

        ' If a staff ID is not being searched for
        If txtStaffID.Text = "" Then

            For i = 0 To UBound(StaffsData)

                If ((Trim(Mid(StaffsData(i), 5, 20)) = txtFirstName.Text) Or txtFirstName.Text = "") And ((Trim(Mid(StaffsData(i), 25, 20)) = txtSurname.Text) Or txtSurname.Text = "") And ((Trim(Mid(StaffsData(i), 45, 30)) = txtEmailAddress.Text) Or txtEmailAddress.Text = "") And ((Trim(Mid(StaffsData(i), 75, 11)) = txtPhoneNumber.Text) Or txtPhoneNumber.Text = "") Then

                    ' A member of staff has been found
                    StaffCount = StaffCount + 1

                    ' Store which part of the textfile there data is saved at
                    OneStaff = i

                End If

            Next

            If StaffCount = 1 Then

                ' Output that only one member of staff has been found
                MsgBox("One member of staff has been found.")

                ' Output their data to the textboxes
                txtFirstName.Text = Trim(Mid(StaffsData(OneStaff), 5, 20))
                txtSurname.Text = Trim(Mid(StaffsData(OneStaff), 25, 20))
                txtEmailAddress.Text = Trim(Mid(StaffsData(OneStaff), 45, 30))
                txtPhoneNumber.Text = Trim(Mid(StaffsData(OneStaff), 75, 11))

            ElseIf StaffCount > 1 Then

                ' Output how many members of staff have been found
                MsgBox(StaffCount & " members of staff has been found.")

            Else

                ' Output that no members of staff were found
                MsgBox("No members of staff has been found.")

            End If

        Else

            ' Clear the textboxes so that just the Staff ID is in a textbox
            txtFirstName.Text = ""
            txtSurname.Text = ""
            txtEmailAddress.Text = ""
            txtPhoneNumber.Text = ""

            ' Loops from start of the textfile until the last set of data
            For i = 0 To UBound(StaffsData)

                ' If the Staff ID in the textfile is the same as the Staff ID in the textbox
                If Trim(Mid(StaffsData(i), 1, 4)) = txtStaffID.Text Then

                    StaffCount = 1

                    ' Output that a member of staff with the ID searched for has been found
                    MsgBox("A member of staff with this Staff ID has been found.")

                    ' Output their data to the textboxes
                    txtFirstName.Text = Trim(Mid(StaffsData(i), 5, 20))
                    txtSurname.Text = Trim(Mid(StaffsData(i), 25, 20))
                    txtEmailAddress.Text = Trim(Mid(StaffsData(i), 45, 30))
                    txtPhoneNumber.Text = Trim(Mid(StaffsData(i), 75, 11))

                    Exit Sub

                End If

            Next

            If StaffCount = 0 Then

                ' Output that the Staff ID entered is not linked with a member of staff
                MsgBox("A member of staff with this Staff ID has not been found.")

            End If

        End If

    End Sub

    Public Function Validation() As Boolean

        If txtFirstName.Text = "" Or txtSurname.Text = "" Or txtEmailAddress.Text = "" Or txtPhoneNumber.Text = "" Then

            ' Presence check
            MsgBox("You must enter Name, Email Address and Phone Number.")

            Return False

        ElseIf (txtFirstName.Text).Length > 20 Or (txtSurname.Text).Length > 20 Then

            ' Length check
            MsgBox("Name must be 20 characters or less in length.")

            Return False

        ElseIf (txtEmailAddress.Text).Contains("@") = False Then

            ' Format check
            MsgBox("Email Address must contain the '@' symbol.")

            Return False

        ElseIf IsNumeric(txtPhoneNumber.Text) = False Then

            ' Data type check
            MsgBox("Phone number must be numeric.")

            Return False

        Else

            Return True

        End If

    End Function

    Public Sub GenerateID(ByVal StaffsData)

        ' Used to store the highest Staff ID there currently is
        Dim HighestStaffID As Integer
        HighestStaffID = 0

        ' Loops from start of the textfile until the last set of data
        For i = 0 To UBound(StaffsData)

            ' If the current highest ID in the textfile is equal to the highest ID
            If Val(Trim(Mid(StaffsData(i), 1, 4))) = HighestStaffID Then

                ' Add one to the Staff ID
                HighestStaffID = HighestStaffID + 1

                ' Set the new ID to the Staff ID textbox
                txtStaffID.Text = HighestStaffID

            End If

        Next

    End Sub

    ' Used to clear the textboxes
    Public Sub ClearTextboxes()

        txtStaffID.Text = ""
        txtFirstName.Text = ""
        txtSurname.Text = ""
        txtEmailAddress.Text = ""
        txtPhoneNumber.Text = ""

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        ClearTextboxes()

    End Sub
End Class