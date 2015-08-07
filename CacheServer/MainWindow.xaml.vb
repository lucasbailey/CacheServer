Imports TCPServerApp

Class MainWindow
    Dim theServer As TCPServerApp.TCPServerApp = Nothing
    Dim context As Object
    Delegate Sub StartTheServer()

    Private Sub Button_Start_Server_Click(sender As Object, e As RoutedEventArgs)
        If (Not IsNothing(theServer)) Then
            MsgBox("The Server is already running.")
        Else
            StartServer(Me)
        End If

    End Sub
    Private Sub Button_Stop_Server_Click(sender As Object, e As RoutedEventArgs)
        theServer.StopServer()

        theServer = Nothing
        context = Nothing
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        StartServer(Me)
    End Sub

    Private Sub StartServer(ctx As Object)
        context = ctx
        Dim tempArgs As String() = System.Environment.GetCommandLineArgs
        Dim theArgs As New Dictionary(Of String, List(Of String))

        For Each arg In tempArgs
            Dim theSplit = arg.Split("=")
            Dim key = theSplit(0)

            Dim value = ""

            If theSplit.Length > 1 Then
                value = theSplit(1)
            End If

            theArgs.Add(key.ToLower, value.Split(",").ToList)
        Next

        If (Not theArgs.ContainsKey("ipaddress")) Then
            theArgs.Add("ipaddress", New List(Of String) From {TCPServerApp.TCPServerApp.DEFAULT_IP_ADDRESS})
        Else
            theArgs.Item("ipaddress").Add(TCPServerApp.TCPServerApp.DEFAULT_IP_ADDRESS)
        End If

        If (Not theArgs.ContainsKey("port")) Then
            theArgs.Add("port", New List(Of String) From {TCPServerApp.TCPServerApp.DEFAULT_PORT.ToString})
        Else
            theArgs.Item("port").Add(TCPServerApp.TCPServerApp.DEFAULT_PORT.ToString)
        End If

        Dim thePorts As List(Of Long) = theArgs.Item("port").ConvertAll(Function(str) Long.Parse(str))

        theServer = New TCPServerApp.TCPServerApp(theArgs.Item("ipaddress"), thePorts)

        Dim StarterDelegate As StartTheServer = Async Sub()
                                                    Try 'the following will fail when the user clicks stop button
                                                        Await theServer.MainAsync()
                                                    Catch ex As Exception

                                                    End Try
                                                End Sub

        ctx.Dispatcher.BeginInvoke(StarterDelegate, {})

    End Sub
End Class
