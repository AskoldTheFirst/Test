import { Button, Menu, MenuItem } from "@mui/material";
import React from "react";
import { useAppDispatch, useAppSelector } from "./configureStore";
import { signOut } from "../Pages/Account/accountSlice";
import { useNavigate } from "react-router-dom";

export default function SignedInMenu() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
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
                <MenuItem onClick={() => {
                    navigate('/profile');
                    handleClose();
                }}>
                    Profile
                </MenuItem>

                <MenuItem onClick={() => {
                    dispatch(signOut());
                    handleClose();
                    navigate('/login');
                }}>
                    Logout
                </MenuItem>

            </Menu >
        </>
    );
}