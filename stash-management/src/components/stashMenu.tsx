import {useState} from 'react';
import type { MouseEvent } from 'react';
import { Menu, MenuItem, IconButton } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import AddIcon from '@mui/icons-material/Add';

export function StashMenu() {
	const [profileAnchorEl, setProfileAnchorEl] = useState<null | HTMLElement>(null);
	const [itemAnchorEl, setItemAnchorEl] = useState<null | HTMLElement>(null);
	const openProfile = Boolean(profileAnchorEl);
	const openItem = Boolean(itemAnchorEl);

	const handleProfileClick = (event: MouseEvent<HTMLElement>) => {
		setProfileAnchorEl(event.currentTarget);
	};

	const handleItemClick = (event: MouseEvent<HTMLElement>) => {
		setItemAnchorEl(event.currentTarget);
	};

	const handleClose = () => {
		setProfileAnchorEl(null);
		setItemAnchorEl(null);
	};

	return (
		<div>
			<IconButton
				aria-label="menu"
				aria-controls={openProfile ? 'stash-menu' : undefined}
				aria-haspopup="true"
				onClick={handleProfileClick}
			>
				<MenuIcon />
			</IconButton>
            <IconButton 
                aria-label="items"
                aria-controls={openItem ? 'items-menu' : undefined}
                aria-haspopup="true"
                onClick={handleItemClick}
            >
                <AddIcon />
            </IconButton>
			<Menu
				id="stash-menu"
				anchorEl={profileAnchorEl}
				open={openProfile}
				onClose={handleClose}
				MenuListProps={{
					'aria-labelledby': 'stash-menu-button',
				}}
			>
				<MenuItem onClick={handleClose}>Profile</MenuItem>
				<MenuItem onClick={handleClose}>My account</MenuItem>
				<MenuItem onClick={handleClose}>Logout</MenuItem>
			</Menu>
            <Menu
				id="items-menu"
				anchorEl={itemAnchorEl}
				open={openItem}
				onClose={handleClose}
				MenuListProps={{
					'aria-labelledby': 'items-menu-button',
				}}
			>
				<MenuItem onClick={handleClose}>New Stash Item</MenuItem>
				
			</Menu>
		</div>
	);
};
