import {Box, Typography} from '@mui/material';
import { StashMenu } from './stashMenu';

export function StashHeader() {
    return (
        <div id='header'>
            <Box sx={{ p: 2, bgcolor: 'primary.main', color: 'white' }}>
                <Typography variant="h1">Stash Management</Typography>
            </Box>
            <StashMenu />
        </div>
    );
}
