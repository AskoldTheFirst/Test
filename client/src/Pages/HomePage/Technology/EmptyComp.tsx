import { useEffect, useRef } from "react";

export default function EmptyComp() {
    const canvasRef = useRef<any>();

    useEffect(() => {
        const canvas = canvasRef.current;
        const context = canvas.getContext('2d');

        context.beginPath();
        context.fillStyle = '#d8dde6';
        context.fillRect(5, 5, 90, 80);
        context.fillRect(110, 5, 90, 80);
        context.fillRect(5, 97, 90, 80);
        context.fillRect(110, 97, 90, 80);

    }, []);
    
    return (
        <>
            <canvas ref={canvasRef} width={200} height={180} />
        </>
    );
}