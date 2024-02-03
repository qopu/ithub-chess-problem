import { useEffect, useState } from "react";
import s from "./style.module.scss"
import King from "@assets/images/smallKing.png"
import Rook from "@assets/images/smallRook.png"
import Bishop from "@assets/images/smallBishop.png"

interface BoardProps {
    king: string
    rook: string
    bishop: string
    setPiece: React.Dispatch<React.SetStateAction<string>> | undefined
}
 
export default function Board({setPiece, king, rook, bishop}: BoardProps) {
    const letters = ["a", "b", "c", "d", "e", "f", "g", "h"]
    const n = 8;
    const m = 8;

    const [chessBoard, setChessBoard] = useState<Array<Array<number>>>([]);
 
    useEffect(() => {
        const result = [];

        for (let i = 0; i < n; i++) {
            const row = Array.from({ length: m }) as number[];
            result.push(row);
        }
 
        setChessBoard(result);
    }, []);

    const placePiece = (x: number, y: number) => {
        const position = (x.toString() + y.toString())
        if(king === position || rook === position || bishop === position) return
        if(setPiece){
            setPiece(x.toString() + y.toString())
        }
    }

    return (
        <div className={s.board}>
            {chessBoard.length > 0 &&
                chessBoard.map((row, rIndex) => {
                    return (
                        <div className={s.row} key={rIndex}>
                            {row.map((_, cIndex) => {
                                return (
                                    <div
                                        onClick={() => placePiece(rIndex, cIndex)}
                                        className={`${s.box} ${
                                            (rIndex + cIndex) % 2 !== 0
                                                ? s.black : s.white
                                            }`}
                                        key={cIndex}
                                    >
                                        {cIndex === 7 && <span className={s.number}>{8-rIndex}</span>}
                                        {rIndex === 7 && <span className={s.letter}>{letters[cIndex]}</span>}
                                        {(king === (rIndex.toString()+cIndex.toString())) && <img alt="king" src={King} />}
                                        {(rook === (rIndex.toString()+cIndex.toString())) && <img alt="rook" src={Rook} />}
                                        {(bishop === (rIndex.toString()+cIndex.toString())) && <img alt="bishop" src={Bishop} />}
                                    </div>
                                );
                            })}
                        </div>
                    );
                })}
        </div>
    );
}