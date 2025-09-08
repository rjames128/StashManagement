import { Stack } from '@mui/material'
import { FabricItemCard } from './FabricItemCard'
import type { FabricItem } from '../entities/stashItem';

type FabricItemCardProps = {
    items: Array<FabricItem>;
}
export function FabricStashCollection( { items }: FabricItemCardProps ) {
    return (
        <Stack direction="row" spacing={2} sx={{ padding: 2, flexWrap: 'wrap', justifyContent: 'center' }}>
            {items.map((item) => (
                <FabricItemCard key={item.id} item={item} />
            ))}
        </Stack>
    );
}