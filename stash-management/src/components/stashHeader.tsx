import {Box, Typography} from '@mui/material';

export function StashHeader() {
    return (
        <Box sx={{ p: 2, bgcolor: 'primary.main', color: 'white' }}>
            <Typography variant="h6">Stash Management</Typography>
        </Box>
    );
}
