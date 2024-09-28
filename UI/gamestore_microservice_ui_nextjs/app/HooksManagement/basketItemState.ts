import {create} from 'zustand'


export const useStore = create((set) => (
    {
        basketCount:0,
        updateBasketCount:(newCount:any) => set({basketCount:newCount}),
        increateBasketItem:()=>set((state:any) => ({basketCount:state.basketCount+1}))
    }
))  