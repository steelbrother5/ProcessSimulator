
// Learn more about F# at http://fsharp.net

module UtilitiesFunctions

open System

///Iota de APL
let Iota( i ) = 0 |> Seq.unfold ( fun x -> if ( x >= i )
                                           then None
                                           else Some ( x , ( x + 1 ) ) )

///
///mode: si modalDeltaDays = 30 & dia es el último del mes, 1 indica usar 30 como último día del mes
///                                                    0 indica usar el último día del mes
///      si modalDeltaDays = 0 , mode es deltaDays
///      si modalDeltaDays <> 0 & <> 30,   
let RelativeDate ( originalDate: DateTime, deltaDays: int, mode: int ) =

    originalDate.AddDays(float(deltaDays))


///
///Recibe una fecha y devuelve la misma fecha pero con la hora completa del día
///actualizada a la hora completa en que se invoca esta función
let DateWithCurrentTimeOfDay ( originalDate : DateTime ) =

    let dateTimeNow = DateTime.Now
    DateTime ( originalDate.Year , 
               originalDate.Month , 
               originalDate.Day , 
               dateTimeNow.Hour , 
               dateTimeNow.Minute, 
               dateTimeNow.Second , 
               dateTimeNow.Millisecond )