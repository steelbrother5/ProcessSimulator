module PortfolioClosingFunctions

open System.Collections.Generic
open UtilitiesFunctions
open PeriodClosingCommonClassesC

///Recibe los inventarios positivos
///Retorna un Array de 1 fila por cada inventario positivo y las siguientes 4 columnas:
///     - inventario positivo acumulado
///     - inventario positivo
///     - secuencia original del inventario positivo
///     - precio por unidad del inventario positivo
let createInitialRemainingInventory ( inMovements : seq<int * decimal * decimal> ) =

    let inAccumulatedMovements = inMovements |> Seq.map (fun ( _ , ffaceValue, _ ) -> ffaceValue)
                                             |> Seq.scan ( fun acc ffaceValue -> acc + ffaceValue ) 0.0m
                                             |> Seq.skip 1
    
    let arrayLength = inAccumulatedMovements |> Seq.length

    let initialRemainingInventory = Array2D.create arrayLength 4 0.0m

    ignore ( inAccumulatedMovements |> Seq.iteri ( fun i x -> initialRemainingInventory.[i , 0] <- x ) )

    ignore ( inMovements |> Seq.iteri ( fun i (fsecuence , ffaceValue, funitPrice) -> initialRemainingInventory.[i , 1] <- ffaceValue
                                                                                      initialRemainingInventory.[i , 2] <- (decimal)fsecuence
                                                                                      initialRemainingInventory.[i , 3] <- funitPrice ) )
    initialRemainingInventory

///
///Recibe: - outMovementSequenceNumber: numero de secuencia del movimiento de inventario de salida que hay que compensar
///        - outMovement: valor del inventario que hay que compensar
///        - remainingInventory: es un array de 1 fila por cada inventario positivo que aun este disponible para compensar
///                              y las siguientes 3 columnas:
///                                 - inventario positivo acumulado
///                                 - inventario positivo
///                                 - secuencia original del inventario positivo
///        - inventoryCompesations: lista de compensaciones realizadas en iteraciones anteriores (llamados anteriores a este 
///                                 método. Esta información no se usa para nada, pero las compensaciones que resulten de la ejecución
///                                 de este método se adicionan a la lista 
///        - outMovementUnitPrice: precio unitario del movimiento de salida (de la venta)  
let CompensateOutMovement( outMovementSequenceNumber : int , outMovement : decimal , remainingInventory : decimal[,] ,
                           inventoryCompensations : List<OutRecordInventoryCompensation> , outMovementUnitPrice : decimal ,
                           snonCompensatedList  : List<OutRecordInventoryNonCompensatedItem> ) =

    //valores de la columna 1 de remainingInventory
    let remainingInventoryColumn1 = Iota( remainingInventory.GetLength(0) )
                                    |> Seq.map ( fun x -> remainingInventory.[x, 0] )
                                    |> Seq.toArray 

    //firstPartialyConsumedInventoryIndex es el número de la fila en remainigInventory 
    ///que se consume de últimas para cumplir la operación de salida (puede alcanzar o no
    ///alcanzar). 
    ///Es decir, puede ser:
    ///     - el índice del primer inventario acumulado que es mayor o igual al inventario
    ///        negativo que se quiere compensar. 
    ///     - el inidice del útltimo inventario disponible, cuando todo el inventario
    ///       disponible no es suficiente para cubrir las operaciones de salida.
    ///Los inventarios con índice menor serán consumidos
    ///en su totalidad por el inventario negativo. El inventario con índice
    ///firstPartialyConsumedInventoryIndex será comsumido total o parcialmente por el saldo
    ///no compensado por los inventarios de índice menor que se consumieron en su 
    ///totalidad 
    let firstPartialyConsumedInventoryIndex = 
        let firstPartialyConsumedInventoryIndexOption = 
            remainingInventoryColumn1 |> Seq.tryFindIndex ( fun x -> x >= outMovement ) 
        match firstPartialyConsumedInventoryIndexOption.IsSome with
        | true -> firstPartialyConsumedInventoryIndexOption.Value
        | false -> remainingInventory.GetLength(0) - 1 


    ///firstNonDepletedInventoryIndex es el número de la primera fila en remainingInventory 
    ///que tiene saldo. Hay filas que pueden no tener saldo porque ha sido utilizado en llamados
    ///anteriores a este método
    let firstNonDepletedInventoryIndex = remainingInventoryColumn1 
                                         |> Seq.tryFindIndex ( fun x -> x <> 0.0m )
    
    ///saca error si no hay inventario alguno para compensar un inventario negativo
    if firstNonDepletedInventoryIndex.IsNone then

        failwith "(CompensateOutMovement) Inventario disponible = 0.0m y falta por compensar. Error de lógica."
    
    ///numberOfDepletedInventories es el número de inventarios que se han agotado en su totalidad
    let numberOfDepletedInventories = firstNonDepletedInventoryIndex.Value
    
    ///completeInventoriesValue es el valor de lo inventarios que van a ser consumidos
    ///     en su totalidad.
    let completeOutInventoriesValue = Iota(firstPartialyConsumedInventoryIndex)
                                      |> Seq.map ( fun x -> remainingInventory.[x,1] )
                                      |> Seq.sum
    
    ///este ciclo solo se ejcuta si se van a consumir inventarios completos.
    ///Si el primer inventario que se consume parcialmente es el primer inventario que aun
    ///tiene valor, quiere decir que no hay invnetarios anteriores para consumir
    ///completamente.
    if firstPartialyConsumedInventoryIndex <> firstNonDepletedInventoryIndex.Value then
        
        ///Esta iteración tiene dos ciclos: - el ciclo exterior es para cada inventario que va a ser consumido en su totalidad
        ///                                 - ciclo interior es para actualizar la columna 0 de remainingInventory, que contiene
        ///                                   el valor acumulado del inventario disponible, de tal forma que se le reste el que 
        ///                                   se está consumiendo en su totalidad
        ignore(
                 printfn "iter level 0 <<<<<<<<<<<<<<0000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                 printfn "iter level 0 iota( firstPartialyConsumedInventoryIndex.Value ) |> Seq.skip numberOfDepletedInventories %A" 
                          (Iota( firstPartialyConsumedInventoryIndex ) |> Seq.skip numberOfDepletedInventories )

                 ///Ciclo exterior.
                 ///Iota retorna los números de los inventarios que va a ser consumidos en su totalidad.
                 ///El skip es para no gastar tiempo procesando los inventarios que ya han sido consumidos en iteraciones anteriores

                 Iota( firstPartialyConsumedInventoryIndex ) 
                 |> Seq.skip numberOfDepletedInventories 
                 |> Seq.iter ( fun inventoryNumberToDepleteCompletely -> 
                                        ///Ciclo interior
                                        ///Las filas que hay que ajustar en remainingInventory son las posteriores al inventario
                                        ///que se está consumiendo en su totalidad. Para eso es el skip. 
                                        Iota( remainingInventory.GetLength(0) )
                                        |> Seq.skip ( inventoryNumberToDepleteCompletely + 1 ) 
                                        |> Seq.iter ( fun accumulatedInventoryNumberToAdjust -> 
                                                                 
                                                                  ///iota( remainingInventory.GetLength(0) - fila - 1 ) contiene un número por cada
                                                                  ///posición de inventario acumulado posterior a la que se está consumiendo en su totalidad.
                                                                  ///La posición de cada uno de esos inventarios es filaAbajo + fila + 1.
                                                                  ///A cada uno de esos inventarios acumulados se le resta el valor del inventario que va a
                                                                  ///ser consumido en su totalidad. 

                                                                  ///el ajuste a la columna 0 (columnas de inventarios disponibles acumulados)
                                                                  ///es restarle el valor del inventario que se está consumiendo en su totalidad
                                                                  remainingInventory.[accumulatedInventoryNumberToAdjust, 0] <- 
                                                                        remainingInventory.[accumulatedInventoryNumberToAdjust, 0] 
                                                                      - remainingInventory.[inventoryNumberToDepleteCompletely , 1]) 
                                        ///esta es la parte final del ciclo exterior.
                                        ///Se ponen en 0 el inventario consumido totalmente y el inventario acumulado
                                        ///   de la fila del inventario consumido totalmente.
                                        ///Nótese que todos los inventarios anteriores al inventario que se está 
                                        ///consumiendo totalmente,ya están en 0.
                                        ///Se agrega un RecordInventoryCompensation a inventoryCompensations, con la compensación
                                        ///hecha con el inventario consumido totalmente, 
                                        remainingInventory.[inventoryNumberToDepleteCompletely , 0 ] <- 0.0m
                                        inventoryCompensations.Add(new OutRecordInventoryCompensation ( outMovementSequenceNumber ,
                                                                                                        outMovementUnitPrice ,
                                                                                                        (int)remainingInventory.[inventoryNumberToDepleteCompletely , 2] ,
                                                                                                        remainingInventory.[inventoryNumberToDepleteCompletely , 3] ,
                                                                                                        remainingInventory.[inventoryNumberToDepleteCompletely , 1] ,
                                                                                                        true ) ) 
                                        remainingInventory.[inventoryNumberToDepleteCompletely , 1 ] <- 0.0m
          )
        )

    ///outMovement - completeOutInventoriesValue es valor que debe ser compensado con un
    ///inventario que no se consume totalmente.
    ///Este valor nunca es 0 porque aunque coincidencialmente el valor del inventario
    ///a compensar se pueda lograr coninventarios completos, el último de estos no se procesa
    ///en el ciclo anterior. 
    ///Lo primero es determinar cuánto efectivamente se va a compensar (lastAmountToCompensate), 
    ///porque puede suceder que el valor a compensar sea mayor que el inventario disponible.
    ///Si no se puede compensar todo, hay que adicionar un registro a snonCompensatedList
    ///con los datos de la transacción y el valor no compensado).
    let lastAmountToCompensate = 
        match remainingInventory.[firstPartialyConsumedInventoryIndex,0] < ( outMovement - completeOutInventoriesValue ) with
        | true -> let uncompensatedValue = 
                      outMovement - completeOutInventoriesValue - remainingInventory.[firstPartialyConsumedInventoryIndex,0]
                  snonCompensatedList.Add( new OutRecordInventoryNonCompensatedItem ( outMovementSequenceNumber ,
                                                                                      -1.0m ,
                                                                                      uncompensatedValue ,
                                                                                      outMovementUnitPrice ,
                                                                                      false ) )
                  remainingInventory.[firstPartialyConsumedInventoryIndex,0]
        | false -> outMovement - completeOutInventoriesValue

    ///En estos pasos se ajusta remainingInventory por el valor no compensado por los
    ///inventarios totalmente consumidos compensando, y se adiciona a inventoryCompensations.
    remainingInventory.[firstPartialyConsumedInventoryIndex,0] <- 
        remainingInventory.[firstPartialyConsumedInventoryIndex,0] - lastAmountToCompensate
    remainingInventory.[firstPartialyConsumedInventoryIndex,1] <- 
        remainingInventory.[firstPartialyConsumedInventoryIndex,1] - lastAmountToCompensate
    inventoryCompensations.Add(new OutRecordInventoryCompensation ( outMovementSequenceNumber ,
                                                                    outMovementUnitPrice ,
                                                                    (int)remainingInventory.[firstPartialyConsumedInventoryIndex , 2] ,
                                                                    remainingInventory.[firstPartialyConsumedInventoryIndex , 3] ,                        
                                                                    lastAmountToCompensate ,
                                                                    ( remainingInventory.[firstPartialyConsumedInventoryIndex,1] = 0.0m ) ) )

    ///iota( remainingInventory.GetLength(0) - firstPartialyConsumedInventoryIndex.Value - 1 ) contiene un 
    ///valor por cada fila en remainingInventory que hay que ajustar cuando se deduzca el valor del inventario
    ///negativo que faltó compensar con inventarios completos.
    ///Los índices de las filas son: fila + 1 + firstPartialyConsumedInventoryIndex.Value 
    ///El valor que se deduce de cada fila es outMovement - completeOutInventoriesValue, o sea lo que falta por
    ///compensar luego de que se consumieron todos los inventarios que se consumían completamente 
    ignore( Iota( remainingInventory.GetLength(0) - firstPartialyConsumedInventoryIndex - 1 )
            |> Seq.iter ( fun fila -> remainingInventory.[fila + 1 + firstPartialyConsumedInventoryIndex , 0 ] <- 
                                            remainingInventory.[fila + 1 + firstPartialyConsumedInventoryIndex , 0 ] - lastAmountToCompensate )
    )

    ///isInInventoryAvailable 
    let isInInventoryAvailable = 0.0m < remainingInventory.[remainingInventory.GetLength(0)-1,0]
    
    remainingInventory , inventoryCompensations , snonCompensatedList , isInInventoryAvailable

///adiciona a snonCompensatedList los movimientos de salida que no se compensan porque ya quedan
///movimientos positivos para compensarlos
let AddUncompensatedOutMovement( outMovementSequenceNumber : int , outMovement : decimal , remainingInventory : decimal[,] ,
                                 inventoryCompensations : List<OutRecordInventoryCompensation> , outMovementUnitPrice : decimal ,
                                 snonCompensatedList  : List<OutRecordInventoryNonCompensatedItem> ) =

    ignore ( snonCompensatedList.Add( new OutRecordInventoryNonCompensatedItem ( outMovementSequenceNumber ,
                                                                                   -1.0m ,
                                                                                   outMovement ,
                                                                                   outMovementUnitPrice ,
                                                                                   true ) ) )
    remainingInventory , inventoryCompensations , snonCompensatedList , false

///devuelve una lista con los movimientos de entrada que no compensan movimientos
///de salida porque ya no hay mpás movimietos de salida para compensar.
///Cada posición de la lista contiene:
///     - número de secuencia del movimiento
///     - valor remanente (que no se usó para compensar movimientos de salida
///     - indicador de transacción completa (que no se utilizó parcialmente)
let GenerateUncompensatedInMovement( cIL_RecordInventory : System.Collections.Generic.List<RecordInventory> , 
                                     remainingInventory : decimal[,] ) =

    let uncompensatedInMovements = 
        cIL_RecordInventory
        |> Seq.filter ( fun frecordInventory -> frecordInventory.Sign = 1.0m ) 
        |> Seq.mapi ( fun i frecordInventory -> frecordInventory.Secuence ,
                                                frecordInventory.FaceValue , 
                                                frecordInventory.UnitPrice , 
                                                remainingInventory.[i,1] )
        |> Seq.filter ( fun ( finMovementSequence , finMovementFaceValue , finMovementUnitPrice , finMovementNonCompensationValue ) 
                              -> finMovementNonCompensationValue > 0.0m ) 
        |> Seq.map ( fun ( finMovementSequence , finMovementFaceValue , finMovementUnitPrice , finMovementNonCompensationValue )
                          -> finMovementSequence , 
                             finMovementUnitPrice ,
                             finMovementNonCompensationValue ,
                             finMovementNonCompensationValue = finMovementFaceValue )

    uncompensatedInMovements
///
///Recibe: inventories ( seq<RecordInventory), que son los inventarios positivos y negativos que hay que 
///        compensar. Es decir, asignar inventarios positivos a inventarios negativos bajo FIFO, y
///        reducir los inventarios positivos en la medida en que se asignan a inventarios negativos
///Devuelve dos listas: - OutRecordInventoryCompensations, con las compensaciones
///                     - OutRecordNonCompensatedInventories, con lo no compensado
let CompensateAndSegregateInventory ( cIL_RecordInventory : System.Collections.Generic.List<RecordInventory> ) =

    ///inventarios negativos
    let outMovements = cIL_RecordInventory |> Seq.filter ( fun x -> x.Sign = -1.0m )
                                           |> Seq.map ( fun x -> x.Secuence, x.FaceValue, x.UnitPrice )
                                           |> Seq.toArray
    
    ///inventarios positivos
    let inMovements = cIL_RecordInventory |> Seq.filter ( fun x -> x.Sign = 1.0m )
                                          |> Seq.map ( fun x -> x.Secuence, x.FaceValue, x.UnitPrice )
                                          |> Seq.toArray

    let isInInventoryAvailable = 0.0m < ( inMovements |> Seq.fold ( fun acc ( _ , ffaceValue , _ ) -> ffaceValue ) 0.0m )

    ///Se procesan uno a uno los inventarios negativos por medio de un Seq.Fold 
    ///que tiene la siguiente estructura:
    /// - Las variables que fluyen por las acumulaciones del fold son:
    ///             - Array de inventarios positivos remanentes (ver createInitialRemaingInventory)
    ///             - Lista de compensaciones (ver RecordInventoryCompensation)
    let compensatedMovements  = 
        outMovements 
        |> Seq.fold ( fun ( sremainingInventory , scompensationList , snonCompensatedList , sisInInventoryAvailable ) (fsecuence, foutMovement, outMovementUnitPrice) -> 
                          ( match sisInInventoryAvailable with
                            | true -> CompensateOutMovement( fsecuence , 
                                                             foutMovement , 
                                                             sremainingInventory , 
                                                             scompensationList , 
                                                             outMovementUnitPrice , 
                                                             snonCompensatedList )
                            | false -> AddUncompensatedOutMovement( fsecuence , 
                                                                    foutMovement , 
                                                                    sremainingInventory , 
                                                                    scompensationList , 
                                                                    outMovementUnitPrice , 
                                                                    snonCompensatedList )
                                                            ) )                                   
                          ( createInitialRemainingInventory inMovements , 
                            new List<OutRecordInventoryCompensation>() , 
                            new List<OutRecordInventoryNonCompensatedItem>() ,
                            isInInventoryAvailable ) 
    
    let (remainingInventory , inventoryCompensationList , nonCompensatedinventoryList , _) = compensatedMovements
    
    if remainingInventory.GetLength(0) <> 0 then
        
        if remainingInventory.[remainingInventory.GetLength(0)-1,0] <> 0.0m then
            ignore ( GenerateUncompensatedInMovement( cIL_RecordInventory , remainingInventory )
                     |> Seq.iter ( fun ( finMovementSequence , finMovementPricePerUnit , finMovementNonCompensationValue , fcompleteTransaction )
                                        -> nonCompensatedinventoryList.Add ( new OutRecordInventoryNonCompensatedItem ( finMovementSequence ,
                                                                                                                        1.0m ,
                                                                                                                        finMovementNonCompensationValue ,
                                                                                                                        finMovementPricePerUnit ,
                                                                                                                        fcompleteTransaction ) ) ) )
    ( inventoryCompensationList |> Seq.toArray ), ( nonCompensatedinventoryList |> Seq.toArray )


///Este método es invocado desde c# para la compensación del cierre
///Recibe: inventories ( seq<RecordInventory), que son los inventarios positivos y negativos que hay que 
///        compensar. Es decir, asignar inventarios positivos a inventarios negativos bajo FIFO, y
///        reducir los inventarios positivos en la medida en que se asignan a inventarios negativos
///Devuelve una lista de OutRecordInventoryCompensations, con las compensaciones
let CompensateInventory ( cIL_RecordInventory : System.Collections.Generic.List<RecordInventory> ) =

    let ( inventoryCompensation , _ ) = CompensateAndSegregateInventory( cIL_RecordInventory )

    inventoryCompensation

///crea instancia de OutRecordInventoryCopensatedItem
let CreateOutRecordInventoryCompensatedItem ( sequenceNumber : int ,
                                              sign : decimal ,
                                              inventoryFaceValue : decimal ,
                                              unitPrice : decimal ,
                                              compensations : seq<OutRecordInventoryItemCompensation * decimal> ) =

    let nonCompensatedValue = inventoryFaceValue - abs ( compensations 
                                                         |> Seq.map (fun ( fInventoryCompensation , _ ) -> fInventoryCompensation ) 
                                                         |> Seq.fold (fun acc fInventoryCompensation 
                                                                                     -> acc + fInventoryCompensation.CompensatedValue ) 0.0m )

    let compensationValueInLocalCurrency = abs ( compensations 
                                                 |> Seq.map (fun ( fInventoryCompensation , _ ) -> fInventoryCompensation.CompensationUnitPrice * 
                                                                                                   fInventoryCompensation.CompensatedValue  )
                                                 |> Seq.sum )
                                           
    let compensatedValue = inventoryFaceValue - nonCompensatedValue

    let compensatedValueInLocalCurrency =  compensatedValue * unitPrice

    let averageCompensationPrice = compensationValueInLocalCurrency / compensatedValue
    
    let lockedProfit = sign * ( compensationValueInLocalCurrency - compensatedValueInLocalCurrency )                           
                                           

    new OutRecordInventoryCompensatedItem ( sequenceNumber , 
                                            sign ,
                                            inventoryFaceValue ,
                                            nonCompensatedValue , 
                                            unitPrice ,
                                            compensatedValueInLocalCurrency ,
                                            compensationValueInLocalCurrency ,
                                            averageCompensationPrice ,
                                            lockedProfit ,
                                            ( compensations |> Seq.map (fun ( fInventoryCompensation , _ ) -> fInventoryCompensation ) 
                                                            |> Seq.toArray ) ,
                                            true )

///Este método es invocado desde C# para la compensación intraday
///Recibe: inventories ( seq<RecordInventory), que son los inventarios positivos y negativos que hay que 
///        compensar. Es decir, asignar inventarios positivos a inventarios negativos bajo FIFO, y
///        reducir los inventarios positivos en la medida en que se asignan a inventarios negativos
///Devuelve una lista de RecordInventoryCompensatedItem, donde cada elemento contiene:
///     - número del item que se compensa
///     - signo de ítem que se compensa (1 compra, -1 venta)
///     - array de compensaciones del ítem
let ItemCompensations ( cIL_RecordInventory : System.Collections.Generic.List<RecordInventory> ) =

    let (inventoryCompensations , uncompensatedInventories ) = CompensateAndSegregateInventory ( cIL_RecordInventory )
    
    let itemCompensations =
        inventoryCompensations 
        |> Seq.map (fun finventoryCompensation -> 
                        ( Seq.init 1 (fun x -> new OutRecordInventoryItemCompensation ( finventoryCompensation.OutMovementNumber , 
                                                                                        finventoryCompensation.InMovementNumber ,
                                                                                        finventoryCompensation.InMovementUnitPrice , 
                                                                                        finventoryCompensation.FaceValue,
                                                                                        -1.0m) ,
                                               finventoryCompensation.OutMovementUnitPrice )
                          |> Seq.append (Seq.init 1 (fun x -> new OutRecordInventoryItemCompensation ( finventoryCompensation.InMovementNumber , 
                                                                                                       finventoryCompensation.OutMovementNumber ,
                                                                                                       finventoryCompensation.OutMovementUnitPrice ,
                                                                                                       ( -1.0m * finventoryCompensation.FaceValue ) ,
                                                                                                       1.0m ) ,
                                                              finventoryCompensation.InMovementUnitPrice  ) ) ) )
        |> Seq.concat
        |> Seq.groupBy (fun ( finventoryItemCompensation , fcompensatedUnitPrice ) 
                             -> finventoryItemCompensation.CompensatedNumber , 
                                finventoryItemCompensation.CompensatedSign , 
                                fcompensatedUnitPrice )
        |> Seq.map ( fun (   ( fcompensatedItemNumber , fcompensatedItemSign , fcompensatedUnitPrice ) 
                           , finventoryItemCompensationSeq ) ->
                           CreateOutRecordInventoryCompensatedItem ( fcompensatedItemNumber , 
                                                                     fcompensatedItemSign ,
                                                                     ( cIL_RecordInventory |> Seq.item fcompensatedItemNumber ).FaceValue ,
                                                                     fcompensatedUnitPrice ,
                                                                     finventoryItemCompensationSeq ) )
        
    let aggregatedItemCompensations =

        ( match ( uncompensatedInventories |> Seq.isEmpty ) with
          | true -> itemCompensations
          | false -> uncompensatedInventories
                     |> Seq.map ( fun fnonCompensatedItem 
                                      -> new OutRecordInventoryCompensatedItem ( fnonCompensatedItem.NonCompensatedItemNumber , 
                                                                                 fnonCompensatedItem.NonCompensatedSign ,
                                                                                 fnonCompensatedItem.NonCompensatedFaceValue ,
                                                                                 fnonCompensatedItem.NonCompensatedFaceValue , 
                                                                                 ( cIL_RecordInventory 
                                                                                   |> Seq.item fnonCompensatedItem.NonCompensatedItemNumber ).UnitPrice ,
                                                                                 0.0m ,
                                                                                 0.0m ,
                                                                                 0.0m ,
                                                                                 0.0m ,
                                                                                 Seq.empty |> Seq.toArray ,
                                                                                 fnonCompensatedItem.IsCompleteTransaction ) )
                     |> Seq.append itemCompensations )
        |> Seq.sortBy (fun finventoryCompensatedItem -> finventoryCompensatedItem.SequenceNumber )
        |> Seq.toArray
        
    aggregatedItemCompensations
    
///Calcula el total de unidades negociadas, el valor total en moneda local y el valor promedio por unidad.
///El signo indica (1) si el cálculo se hace sobre inventarios positivos (incluyen compras), o (-1) si
///se hace sobre inventarios negativos (incluyen ventas).
let CalculateTransactionsTotalsAndAverages  ( itemCompensations : array<OutRecordInventoryCompensatedItem> ,
                                              sign : decimal ) = 
    let ( totalUnits , totalLocalCurrencyValue ) =        
        itemCompensations
        |> Seq.filter ( fun fitemCompensation -> fitemCompensation.Sign = sign && fitemCompensation.isCompleteTransaction )
        |> Seq.fold ( fun ( acctotalBuyUnits , acctotalBuyValue ) fitemCompensation 
                            -> acctotalBuyUnits + fitemCompensation.InventoryFaceValue , 
                               acctotalBuyValue + ( fitemCompensation.InventoryFaceValue * fitemCompensation.UnitPrice ) )
                    ( 0.0m , 0.0m )   
    
    let averageValue = 
        match totalUnits = 0.0m with
        | true -> 0.0m
        | false -> totalLocalCurrencyValue / totalUnits
    
    totalUnits , totalLocalCurrencyValue , averageValue 

///Devuelve un OutRecordControlPanel a partir de una lista de RecordCurrencyTrade que no puede estar
///vacía.
///El parámetro isInPortfolio no se usa para nada por ahora (se dejó porque por algún motivo
///se usa en el llamado desde f#).
let ControlPanelValues ( cIL_RecordCurrencyTrade : System.Collections.Generic.List<RecordCurrencyTrade> , isInPortfolio : bool ) =

    let cIL_RecordInventory = new List<RecordInventory>()

    
    ignore ( cIL_RecordCurrencyTrade
             |> Seq.iter ( fun fcurrencyTrade -> cIL_RecordInventory.Add(new RecordInventory(fcurrencyTrade.Secuence,
                                                                                             (decimal)fcurrencyTrade.Sign,
                                                                                             fcurrencyTrade.FaceValue ,
                                                                                             fcurrencyTrade.TradeRate) ) ) )
    
    let itemCompensations = ItemCompensations cIL_RecordInventory
    
    let ( totalBuyUnits , totalBuyValueInLocalCurrency , averageBuyRate) = CalculateTransactionsTotalsAndAverages ( itemCompensations , 1.0m )
    let ( totalSellUnits , totalSellValueInLocalCurrency , averageSellRate) = CalculateTransactionsTotalsAndAverages ( itemCompensations , -1.0m )

    let ( totalLockedUnits , totalLockedBuyValueInLocalCurrency , totalLockedSellValueInLocalCurrency ) =
        itemCompensations
        |> Seq.filter ( fun fitemCompensation -> fitemCompensation.Sign = 1.0m )
        |> Seq.map ( fun fitemCompensation -> fitemCompensation , fitemCompensation.InventoryFaceValue - fitemCompensation.NonCompensatedValue )
        |> Seq.filter ( fun ( fitemCompensation , fcompensatedUnits ) -> fcompensatedUnits <> 0.0m )
        |> Seq.fold ( fun  ( acctotalLockedUnits , acctotalLockedBuyValueInLocalCurrency , acctotalLockedSellValueInLocalCurrency )
                           ( fitemCompensation , fcompensatedUnits ) 
                             -> acctotalLockedUnits + fcompensatedUnits , 
                                acctotalLockedBuyValueInLocalCurrency + fitemCompensation.CompensatedValueInLocalCurrency ,
                                acctotalLockedSellValueInLocalCurrency + fitemCompensation.CompensationValueInLocalCurrency )
                           ( 0.0m , 0.0m , 0.0m )   
     
    let ( averageLockedBuyRate , averageLockedSellRate ) = 
        match totalLockedUnits = 0.0m with
        | true -> 0.0m , 0.0m
        | false -> totalLockedBuyValueInLocalCurrency / totalLockedUnits ,
                   totalLockedSellValueInLocalCurrency / totalLockedUnits
    
    let lockedProfit = totalLockedSellValueInLocalCurrency - totalLockedBuyValueInLocalCurrency

    let position = totalBuyUnits + totalSellUnits


    let controlPanelObject = new OutRecordControlPanel( totalBuyUnits , 
                                                        totalBuyValueInLocalCurrency , 
                                                        averageBuyRate , 
                                                        totalSellUnits , 
                                                        totalSellValueInLocalCurrency , 
                                                        averageSellRate , 
                                                        totalLockedUnits ,
                                                        totalLockedBuyValueInLocalCurrency ,
                                                        totalLockedSellValueInLocalCurrency ,
                                                        averageLockedBuyRate ,
                                                        averageLockedSellRate ,
                                                        lockedProfit ,
                                                        position )

    controlPanelObject 

///Se llama desde f#
///Devuelve un OutRecordControlPanel a partir de una lista de RecordCurrencyTrade.
///El parámetro isInPortfolio no se usa para nada por ahora (se dejó porque por algún motivo
///se usa en el llamado desde f#).
let CalculateControlPanelPosition ( cIL_RecordCurrencyTrade : System.Collections.Generic.List<RecordCurrencyTrade> , isInPortfolio : bool ) =

    let controlPanelObject = match cIL_RecordCurrencyTrade.Count > 0 with
                             | true -> ControlPanelValues( cIL_RecordCurrencyTrade , isInPortfolio )
                             | false -> new OutRecordControlPanel( 0.0m , 0.0m ,  0.0m , 0.0m , 0.0m ,  0.0m ,  0.0m , 0.0m ,  0.0m ,  0.0m ,  0.0m ,  0.0m , 0.0m)

    controlPanelObject 