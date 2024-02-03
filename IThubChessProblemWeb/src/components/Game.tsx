import { useMutation } from 'react-query';
import { useEffect, useState } from 'react';
import axios from 'axios';

import Board from './Board';
import { Info } from './Info';
import s from './style.module.scss';
import { formatPosition } from '@utils/formatPosition';
import { ResponseType } from '@interfaces/ResponseType';

function Game() {

    const [king, setKing] = useState('')
    const [rook, setRook] = useState('')
    const [bishop, setBishop] = useState('')

    const {mutate, data, isLoading} = useMutation({
        mutationFn: () => axios.post<ResponseType>("http://109.107.181.29:7777/Chess",
        {
            king: formatPosition(king),
            rook: formatPosition(rook),
            bishop: formatPosition(bishop)
        },
        {headers: {
            "Content-Type": "application/json",
            Accept: "text/plain"
        }})
        .then(res => res.data),
        mutationKey: ["chess"]
    })

    useEffect(() => {
        if(king && rook && bishop){
            mutate()
        }
    }, [king, rook, bishop])

    const resetGame = () => {
        setKing('')
        setRook('')
        setBishop('')
    }

    const setPiece = () => {
        if(!king) return setKing
        else if(!rook) return setRook
        else if(!bishop) return setBishop
    }

    const currentPiece = () => {
        if(!king) return "king"
        else if(!rook) return "rook"
        else if(!bishop) return "bishop"
    }
 
    return (
        <div className={s.container}>
            <Board king={king} rook={rook} bishop={bishop} setPiece={setPiece()} />
            <Info isLoading={isLoading} response={data} currentPiece={currentPiece()} resetGame={resetGame} />
        </div>
    );
}

export default Game;
