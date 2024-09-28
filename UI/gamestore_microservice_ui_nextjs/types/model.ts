// public string CardHolderName { get; set; }
// public string CardNumber { get; set; }
// public string ExpireMonth { get; set; }
// public string ExpireYear { get; set; }
// public string Cvc { get; set; }


export type PaymentForm = {
    cardHolderName:string,
    cardNumber:string,
    expireMonth:string,
    expireYear:string,
    cvc:string
}