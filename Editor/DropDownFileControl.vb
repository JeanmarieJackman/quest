﻿<ControlType("dropdownfile")> _
Public Class DropDownFileControl
    Inherits DropDownControl

    Private m_fileLister As Func(Of IEnumerable(Of String))
    Private m_source As String

    Protected Overrides Sub InitialiseControl(controlData As IEditorControl)

        ' TO DO: we may want to allow a "freetext" attribute (as for base DropDownControl)
        'lstDropdown.DropDownStyle = ComboBoxStyle.DropDown

        lstDropdown.Items.Clear()

        If controlData IsNot Nothing Then
            Dim source As String = controlData.GetString("source")
            If source = "libraries" Then
                m_fileLister = AddressOf GetAvailableLibraries
            Else
                m_source = source
                m_fileLister = AddressOf GetFilesInGamePath
            End If

            lstDropdown.Items.AddRange(m_fileLister.Invoke().ToArray())
        End If

        ' TO DO: For files which are in the game path, we want to invoke m_fileLister each time Populate is called

    End Sub

    Private Function GetAvailableLibraries() As IEnumerable(Of String)
        Return Controller.GetAvailableLibraries()
    End Function

    Private Function GetFilesInGamePath() As IEnumerable(Of String)
        ' TO DO: Implement - extensions specified as a list in <source> attribute
        Return New List(Of String)
    End Function
End Class
