import { useMemo } from 'react';
import s from './style.module.scss';
import King from '@assets/images/King.png';
import Rook from '@assets/images/Rook.png';
import Bishop from '@assets/images/Bishop.png';
import { ResponseType } from '@interfaces/ResponseType';

interface InfoProps {
    currentPiece: 'king' | 'rook' | 'bishop' | undefined;
    resetGame: () => void;
    response: ResponseType | undefined;
    isLoading: boolean
}

export const Info = ({ currentPiece, resetGame, response, isLoading }: InfoProps) => {
    const img = useMemo(() => {
        if (currentPiece === 'king') return King;
        else if (currentPiece === 'rook') return Rook;
        else if (currentPiece === 'bishop') return Bishop;
    }, [currentPiece]);

    const color = useMemo(() => {
        if (currentPiece === 'king') return 'белого';
        else if (currentPiece === 'rook') return 'чёрную';
        else if (currentPiece === 'bishop') return 'чёрного';
    }, [currentPiece]);

    const name = useMemo(() => {
        if (currentPiece === 'king') return 'короля';
        else if (currentPiece === 'rook') return 'ладью';
        else if (currentPiece === 'bishop') return 'слона';
    }, [currentPiece]);

    if(isLoading){
        return(
            <div className={s.loading}>Загрузка...</div>
        )
    }
    return (
        <div className={currentPiece ? s.info : s.info_res}>
            <h1 className={s.header}>
                Best
                <br />
                Chess
                <br />
                Problem
                <br />
            </h1>
            {!currentPiece ? (
                <>
                    <p className={s.result}>
                        Результат:
                        <br />
                        <span className={response?.threat ? s.result_lose : s.result_win}>{response?.text}</span>
                    </p>
                    <button onClick={resetGame} className={s.play_again_btn}>
                        Сыграть ещё раз
                    </button>
                </>
            ) : (
                <>
                    <p className={s.question}>
                        Куда поставить <span>{color}</span> {name}?
                    </p>
                    <div>
                        <img src={img} />
                    </div>
                </>
            )}
        </div>
    );
};
