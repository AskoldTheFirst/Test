import { useSelector } from "react-redux";
import { RootState } from "../../App/configureStore";
import { useEffect } from "react";

export default function TestPage() {
    const { state } = useSelector((state: RootState) => state.globalState);

    useEffect(() => {
        //if (state.)
    }, []);

    return (
        <>
        </>
    );
}