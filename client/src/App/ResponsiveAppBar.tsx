import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import { QueryStats } from '@mui/icons-material';
import { NavLink, useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from './configureStore';
import SignedInMenu from './SignedInMenu';
import { signOut } from '../Pages/Account/accountSlice';

const barLinks = [
    { title: 'Home', path: '/home' },
    { title: 'Statistics', path: '/statistics' },
    { title: 'About', path: '/about' },
];

function ResponsiveAppBar() {
    let { user } = useAppSelector(state => state.account);
    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const signLinks = [
        { title: 'Login', path: '/login' },
        { title: 'Register', path: '/register' },
    ];


    return (
        <AppBar position="static" sx={{ marginBottom: '30px' }}>
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <QueryStats sx={{ display: { xs: 'none', md: 'flex' }, mr: 1 }} />
                    <Typography
                        variant="h6"
                        noWrap
                        component="a"
                        href="/"
                        sx={{
                            mr: 1,
                            ml: 1,
                            display: { xs: 'none', md: 'flex' },
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            letterSpacing: '.3rem',
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        QUIZ
                    </Typography>
                    <Box width={20}></Box>
                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                        <IconButton
                            size="large"
                            aria-label="account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleOpenNavMenu}
                            color="inherit"
                        >
                            <MenuIcon />
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorElNav}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left',
                            }}
                            open={Boolean(anchorElNav)}
                            onClose={handleCloseNavMenu}
                            sx={{
                                display: { xs: 'block', md: 'none' },
                            }}
                        >
                            {barLinks.map(({ title, path }) => (
                                <MenuItem key={path} component={NavLink} to={path}>
                                    <Typography textAlign="center">{title}</Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>
                    <QueryStats sx={{ display: { xs: 'flex', md: 'none' }, mr: 1 }} />
                    <Typography
                        variant="h5"
                        noWrap
                        component="a"
                        href="/"
                        sx={{
                            mr: 2,
                            display: { xs: 'flex', md: 'none' },
                            flexGrow: 1,
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            letterSpacing: '.3rem',
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        QUIZ
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        {barLinks.map(({ title, path }) => (
                            <Button
                                component={NavLink}
                                key={path}
                                to={path}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                            >
                                {title}
                            </Button>
                        ))}
                    </Box>

                    <Box sx={{ flexGrow: 0 }} display={'flex'}>
                        <Tooltip title="Open settings">
                            <IconButton onClick={handleOpenUserMenu} sx={{ p: 1 }} disabled={user == null}>
                                <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" sx={{ width: 32, height: 32 }} />
                            </IconButton>
                        </Tooltip>
                        <Menu
                            sx={{ mt: '45px' }}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            <MenuItem key="Profile" onClick={() => { navigate('/profile'); handleCloseUserMenu(); }}>
                                <Typography textAlign="center">Profile</Typography>
                            </MenuItem>
                            <MenuItem key="LogOut" onClick={() => { dispatch(signOut()); handleCloseUserMenu(); }}>
                                <Typography textAlign="center">Logout</Typography>
                            </MenuItem>
                        </Menu>

                        {user ? (<SignedInMenu />) : (
                            signLinks.map(({ title, path }) => (
                                <Button
                                    component={NavLink}
                                    to={path}
                                    key={path}
                                    onClick={handleCloseNavMenu}
                                    sx={{ my: 1, color: 'white', display: 'block', fontSize: 12 }}
                                >
                                    {title}
                                </Button>
                            ))
                        )}
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}
export default ResponsiveAppBar;
