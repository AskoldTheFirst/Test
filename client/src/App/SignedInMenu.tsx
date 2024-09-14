import { Button, Menu, MenuItem } from "@mui/material";
import React from "react";
import { useAppDispatch, useAppSelector } from "./configureStore";
import { signOut } from "../Pages/Account/accountSlice";
//import { useAppDispatch, useAppSelector } from "../store/configureStore";
//import { signOut } from "../../features/account/accountSlice";

export default function SignedInMenu() {
    const dispatch = useAppDispatch();
    let { user } = useAppSelector(state => state.account);
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <Button
                color='inherit'
                onClick={handleClick}
                sx={{ typography: 'h6', fontSize: 12 }}
            >
                {user?.login + ' - ' + user?.email}
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
            >
                <MenuItem onClick={handleClose}>Profile</MenuItem>
                <MenuItem onClick={() => {
                    dispatch(signOut());
                }}>
                    Logout
                </MenuItem>
            </Menu >
        </>
    );
}