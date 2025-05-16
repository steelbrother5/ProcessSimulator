module PeriodClosingCommonClassesC

open System

type RecordInventory ( secuence : int ,
                       //oid : Guid ,
                           sign : decimal ,
                           faceValue : decimal ,
                           unitPrice : decimal ) =

    member this.Secuence = secuence
    //member this.Oid = oid
    member this.Sign = sign 
    member this.FaceValue = faceValue
    member this.UnitPrice = unitPrice

type RecordCurrencyTrade ( secuence : int ,
                           fraction : int,
                           //tradeType : string ,
                           sign : decimal ,
                           faceValue : decimal ,
                           tradeRate : decimal ,
                           isInPortfolio : bool ,
                           matchGroup : int
                            ) =

    let mutable m_fraction = fraction 
    let mutable m_faceValue = faceValue 
    let mutable m_matchGroup = matchGroup 
                   
    member this.Secuence = secuence
    member this.Fraction with get () = m_fraction
                         and set newFraction = m_fraction <- newFraction 
    //member this.TradeType = tradeType 
    member this.Sign = sign 
    member this.FaceValue with get () = m_faceValue
                          and set newFaceValue = m_faceValue <- newFaceValue
    member this.TradeRate = tradeRate
    member this.IsInPortfolio = isInPortfolio 
    member this.MatchGroup with get () = m_matchGroup
                           and set newMatchGroup = m_matchGroup <- newMatchGroup

type OutRecordControlPanel ( totalBuyUnits : decimal ,
                             totalBuyValueInLocalCurrency : decimal ,
                             averageBuyRate : decimal ,
                             totalSellUnits : decimal ,
                             totalSellValueInLocalCurrency : decimal ,
                             averageSellRate : decimal ,
                             totalLockedUnits : decimal , 
                             totalLockedBuyValueInLocalCurrency : decimal , 
                             totalLockedSellValueInLocalCurrency : decimal ,
                             averageLockedBuyRate : decimal ,
                             averageLockedSellRate : decimal ,
                             lockedProfit : decimal ,
                             position : decimal ) =

    member this.TotalBuyUnits = totalBuyUnits
    member this.TotalBuyValueInLocalCurrency = totalBuyValueInLocalCurrency 
    member this.AverageBuyRate = averageBuyRate 
    member this.TotalSellUnits = totalSellUnits
    member this.TotalSellValueInLocalCurrency = totalSellValueInLocalCurrency 
    member this.AverageSellRate = averageSellRate 
    member this.TotalLockedUnits = totalLockedUnits 
    member this.TotalLockedBuyValueInLocalCurrency = totalLockedBuyValueInLocalCurrency 
    member this.TotalLockedSellValueInLocalCurrency = totalLockedSellValueInLocalCurrency
    member this.AverageLockedBuyRate = averageLockedBuyRate 
    member this.AverageLockedSellRate = averageLockedSellRate 
    member this.LockedProfit = lockedProfit 
    member this.Position = position 



type OutRecordInventoryCompensation ( outMovementNumber : int ,
                                      outMovementUnitPrice : decimal ,
                                      inMovementNumber : int ,
                                      inMovementUnitPrice : decimal ,
                                      faceValue : decimal ,
                                      depletesBalance : bool) =

    member this.OutMovementNumber = outMovementNumber 
    member this.OutMovementUnitPrice = outMovementUnitPrice 
    member this.InMovementNumber = inMovementNumber 
    member this.InMovementUnitPrice = inMovementUnitPrice 
    member this.FaceValue = faceValue    
    member this.DepletesBalance = depletesBalance    

type OutRecordInventoryItemCompensation ( compensatedNumber : int ,
                                          compensationNumber : int ,
                                          compensationUnitPrice : decimal,
                                          compensatedValue : decimal ,
                                          compensatedSign : decimal) =

    member this.CompensatedNumber = compensatedNumber 
    member this.CompensationNumber = compensationNumber 
    member this.CompensationUnitPrice = compensationUnitPrice 
    member this.CompensatedValue = compensatedValue    
    member this.CompensatedSign = compensatedSign    

type OutRecordInventoryCompensatedItem ( sequenceNumber : int ,
                                         sign : decimal ,
                                         inventoryFaceValue : decimal ,
                                         nonCompensatedValue : decimal ,
                                         unitPrice : decimal ,
                                         compensatedValueInLocalCurrency : decimal ,
                                         compensationValueInLocalCurrency : decimal ,
                                         averageCompensationPrice : decimal,
                                         lockedProfit : decimal ,
                                         compensations : array<OutRecordInventoryItemCompensation> ,
                                         isCompleteTransaction : bool ) =
                                      
    member this.SequenceNumber = sequenceNumber 
    member this.Sign = sign    
    member this.InventoryFaceValue = inventoryFaceValue    
    member this.NonCompensatedValue = nonCompensatedValue    
    member this.UnitPrice = unitPrice    
    member this.CompensatedValueInLocalCurrency = compensatedValueInLocalCurrency    
    member this.CompensationValueInLocalCurrency = compensationValueInLocalCurrency    
    member this.AverageCompensationPrice = averageCompensationPrice    
    member this.LockedProfit = lockedProfit    
    member this.Compensations = compensations
    member this.isCompleteTransaction = isCompleteTransaction    

type OutRecordInventoryNonCompensatedItem ( nonCompensatedItemNumber : int ,
                                            nonCompensatedSign : decimal ,
                                            nonCompensatedFaceValue : decimal ,
                                            nonCompensatedUnitPrice : decimal ,
                                            isCompleteTransaction : bool ) =
                                      
    member this.NonCompensatedItemNumber = nonCompensatedItemNumber 
    member this.NonCompensatedSign = nonCompensatedSign    
    member this.NonCompensatedFaceValue = nonCompensatedFaceValue
    member this.NonCompensatedUnitPrice = nonCompensatedUnitPrice    
    member this.IsCompleteTransaction = isCompleteTransaction    