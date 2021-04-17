Imports System
Imports System.Formats.Asn1

Module Program
    Dim ring(,) As Integer = {{0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22,18, 29, 7, 28, 12, 35, 3, 26},
                              {0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2}}
    Dim colors() As ConsoleColor = {ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Black}
    Dim ringRank0() As Integer = {0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22,18, 29, 7, 28, 12, 35, 3, 26}
    Dim intr() As String = {
                                "####    ###   #   #  #      #####  #####  #####  #####", 
                                "#   #  #   #  #   #  #      #        #      #    #     ",
                                "#   #  #   #  #   #  #      #        #      #    #     ",
                                "#   #  #   #  #   #  #      #        #      #    #     ",
                                "####   #   #  #   #  #      ####     #      #    ####  ",
                                "##     #   #  #   #  #      #        #      #    #     ",
                                "# #    #   #  #   #  #      #        #      #    #     ",
                                "#  #   #   #  #   #  #      #        #      #    #     ",
                                "#   #   ###    ###   #####  #####    #      #    ##### "}
    Function GetIndex(mass() As Integer, obj As Integer) As Integer
        For i = 0 To UBound(mass)
            If mass(i) = obj Then Return i
        Next
    End Function
    
    Sub sleep(d As Single)
        Dim t As Single = Timer
        Do while Timer - t < d
        Loop
    End Sub
    
    Sub intro()
        Console.SetCursorPosition(Console.WindowWidth \ 2 - Len(intr(0))\2,Console.WindowHeight()\2 - UBound(intr) + 3)
        For i = 0 To UBound(intr)
            Console.WriteLine(intr(i))
            Console.SetCursorPosition(Console.WindowWidth \ 2 - Len(intr(0))\2, Console.GetCursorPosition().Item2)
            sleep(0.0888)
        Next
        'sleep(5)
        Console.SetCursorPosition(0,0)
        For i = 0 To (Console.WindowHeight()\2 - UBound(intr) + 2) \ 2
            Console.Write(StrDup(Console.WindowWidth, "#"))
            sleep(0.0888)
        Next
        For i = 0 To Console.WindowHeight - (Console.WindowHeight()\2 - UBound(intr) + 2) - 3
            Console.Write(StrDup((Console.WindowWidth \ 2 - Len(intr(0)) \ 2) \ 2, "#")) 
            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth \ 2 - Len(intr(0)) \ 2) \ 2, (Console.GetCursorPosition().Item2))
            Console.WriteLine(StrDup((Console.WindowWidth \ 2 - Len(intr(0)) \ 2) \ 2 - 1 , "#"))
            sleep(0.0888)
        Next
        For i = 0 To (Console.WindowHeight()\2 - UBound(intr) + 2) \ 2
            Console.Write(StrDup(Console.WindowWidth, "#"))
            sleep(0.0888)
        Next
        Dim entr As String = "   Нажмите любую кнопку, чтобы начать игру! "
        Console.SetCursorPosition((Console.WindowWidth \ 2) - Len(entr) \ 2,Console.WindowHeight - (Console.WindowHeight()\2 - UBound(intr) + 2) \ 4 - 1)
        Console.Write(StrDup(Len(entr) + 2, " "))
        
        Console.SetCursorPosition((Console.WindowWidth \ 2) - Len(entr) \ 2,Console.WindowHeight - (Console.WindowHeight()\2 - UBound(intr) + 2) \ 4)
        Console.Write(entr & "  ")
        
        Console.SetCursorPosition((Console.WindowWidth \ 2) - Len(entr) \ 2,Console.WindowHeight - (Console.WindowHeight()\2 - UBound(intr) + 2) \ 4 + 1)
        Console.Write(StrDup(Len(entr) + 2, " "))
        
        Console.ReadKey
        Console.Clear
    End Sub
    
    Function spinWheel() As String()
        Dim rnd As New Random
        Dim probability(36) As Double
        Dim whatDropped As New List(Of String)
        
        For k = 0 To rnd.Next(1,5)
            For i = 0 To rnd.Next(1,rnd.Next(5,100))
                rnd.NextDouble()
            Next
        Next
        
        Dim max As Double = 0
        Dim dropped As Integer
        
        For i = 0 To 36
            probability(i) = rnd.NextDouble()
            if probability(i) > max then
                max = probability(i)
                dropped = i
            End If
        Next
        
        whatDropped.Add(ring(0,dropped).ToString())
        
        If ring(0, dropped) mod 2 = 0 And dropped > 0
            whatDropped.Add("EVEN")
        Else
            whatDropped.Add("ODD")
        End If
        
        If ring(0, dropped) mod 3 = 0 And dropped > 0
            whatDropped.Add("3L")
        Else If ring(0, dropped) mod 3 = 1
            whatDropped.Add("2L")
        Else
            whatDropped.Add("1L")
        End If
        
        If ring(1,dropped) = 1
            whatDropped.Add("RED")
        Else If  ring(1,dropped) = 2
            whatDropped.Add("BLACK")
        End If
        
        If ring(0, dropped) > 0 And ring(0, dropped) <= 12
            whatDropped.Add("F12")
        Else If ring(0, dropped) > 12 And ring(0, dropped) <= 24   
            whatDropped.Add("S12")
        Else If ring(0, dropped) > 24
            whatDropped.Add("T12")
        End If
        
        If ring(0,dropped) > 0 And ring(0,dropped) < 19
            whatDropped.Add("TO18")
        ElseIf ring(0,dropped) > 18
            whatDropped.Add("FROM18")
        End If
        
        Return whatDropped.ToArray()
    End Function
    
    Sub display(dropped As Integer)
        Console.BackgroundColor = ConsoleColor.Green
        Console.Clear()
        For i = 0 To 36
            If ring(0,i) <> dropped
                Console.ForegroundColor = colors(ring(1,i))
                Console.Write(ring(0,i) & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,i))
                Console.Write(ring(0, i))
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Dim temp As Integer
        Console.WriteLine()
        Console.WriteLine()
        For i = 3 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> dropped
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Console.WriteLine()
        For i = 2 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> dropped
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Console.WriteLine()
        For i = 1 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> dropped
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
    End Sub
    
    Sub rules()
        Console.Clear
        Console.WriteLine("Правила:")
        Console.WriteLine("Игра представляет собой Европейскую рулетку. Игрок должен сделать ставку на определённую зону, будь то число, сектор, строка, чётность или цвет. Игрок может делать несколько ставок. Ставки вводятся в предоставленную зону через пробел.")
        Console.WriteLine("<число 0 - 36> - ставка на число (1:36).")
        Console.WriteLine("1L/2L/3L - 1/2/3 линия соответственно, снизу вверх (1:3).")
        Console.WriteLine("F12/S12/T12 - ставка на сектора от 1 по 12/от 13 по 24/от 25 по 36 соответственно (1:3).")
        Console.WriteLine("RED/BLACK - ставка на цвет (1:2).")
        Console.WriteLine("TO18/FROM18 - ставка на сектор от 1 по 18/от 19 по 36 (1:2).")
        Console.WriteLine("EVEN/ODD - чётные/нечётные (1:2).")
        Console.WriteLine("Игрок изначально получает 5000 фишек.")
        Console.WriteLine("После того, как игрок укажет, на что ставит, он указывает количество фишек на каждую ставку через пробел.")
        Console.WriteLine("Например:")
        Console.WriteLine("На что ставите: 0 16 2L T12 RED ODD")
        Console.WriteLine("Сколько ставите: 100 50 500 500 1000 1000")
        Console.WriteLine("ВНИМАНИЕ! Количество ставок должно совпадать с количеством фишек, которые ставите, в примере 6 = 6.")
        Console.WriteLine("Или просто нажмите ENTER, чтобы пропустить ставку.")
        Console.WriteLine("Удачи!")
        Console.ReadKey()
    End Sub
    
    Sub game()
        Console.WriteLine("Игра началась!")
        Dim fish As Long = 5000
        Dim isGaming As Boolean = True
        Do while fish > 0
            
            
            
            
            
            Console.WriteLine("У Вас {0} фишек.", fish)
            Console.WriteLine("Продолжить игру? (Y/n)")
            Dim temp As String = Console.ReadLine()
            If temp = "n" or temp = "N" Then
                Exit Sub
            Else IF temp = "" or temp = "y" or temp = "Y"
                isGaming = True
            Else
                Console.WriteLine("Неверный ввод, продолжаем игру.")
            End If
            
            
            Dim generated() As String = spinWheel()
            Console.Clear
            Console.Write("Делайте ставки: ")
            Dim stav As New List(Of String)
            stav = Console.ReadLine().Split.ToList()
            Console.Write("Укажите суммы ставок: ")
            Dim summs As New List(Of Integer)
            Dim summ() As String
            summ = Console.ReadLine().Split
            For i = 0 To UBound(summ)
                summs.Add(Int(summ(i)))
            Next
            If stav.count <> summs.count Or summs.ToArray.Sum() > fish Then
                Console.WriteLine("Ставки не корректны. Пропуск.")
                stav.Clear()
                summs.Clear()
                Continue Do
            End If
            Console.WriteLine("Крутим колесо...")
            sleep(5)
            display(Int(generated(0)))
            Dim saray() As Integer = summs.ToArray()
            fish -= saray.Sum()
            Dim indedx As Integer
            For i = 0 To UBound(generated)
                If stav.Contains(generated(i)) Then
                    indedx = stav.IndexOf(generated(i))
                    If IsNumeric(generated(i))
                        saray(indedx) *= 35
                    Else If generated(i) = "RED" Or generated(i) = "BLACK" Or generated(i) = "ODD" Or generated(i) = "EVEN" Or generated(i) = "FROM18" Or generated(i) = "TO18" Then
                        saray(indedx) *= 1
                    Else If generated(i) = "3L" Or generated(i) = "2L" Or generated(i) = "1L" Or generated(i) = "F12" Or generated(i) = "S12" Or generated(i) = "T12" Then
                        saray(indedx) *= 2
                    Else
                        saray(indedx) = -saray(indedx)
                        fish += saray(indedx)
                    End If
                End If
            Next
            
            
            display(Int(generated(0)))
            Console.WriteLine()
            Console.WriteLine("Выпало: ")
            For i = 0 To UBound(generated)
                Console.Write(generated(i) & " ")
            Next
            Console.WriteLine()
        Loop
        
    End Sub
    
    Sub Main()
        Console.BackgroundColor = ConsoleColor.Green
        Randomize()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Black
        intro()
        'Console.SetWindowSize(108,100)
        Console.WriteLine("Игра Рулетка")
        Console.WriteLine("1) Начать игру")
        Console.WriteLine("2) Ознакомиться с правилами")
        Console.WriteLine("3) Выйти из игры")
        Dim input As Char = Console.ReadKey.KeyChar
        Dim n As Integer = If(Int(input.ToString()) = 1 Or Int(input.ToString()) = 2 Or Int(input.ToString()) = 3, Int(input.ToString()), 0)
        Dim history As New List(Of Integer)
        Dim historyC As New List(Of Integer)
        Select Case n
            Case 0
                Console.WriteLine()
                Console.WriteLine("Неправильный ввод!")
                sleep(5)
                Main
            Case 1
                Console.Clear
                game()
                Main()
            Case 2
                Console.Clear
                rules()
                Main()
            Case 3
                Exit Sub
        End Select
    End Sub
End Module
