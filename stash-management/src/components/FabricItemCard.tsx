import { Card, CardContent, CardMedia, Typography } from '@mui/material';
import type { FabricItem } from '../entities/stashItem';

type FabricItemCardProps = {
  item: FabricItem;
};

export function FabricItemCard({ item }: FabricItemCardProps) {
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardMedia
        component="img"
        height="140"
        image={item.imageSrc}
        alt={item.name}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {item.name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {item.description}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Amount: {item.amount}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Cut: {item.cut}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Purchased at: {item.sourceLocation}
        </Typography>
        <Typography variant="caption" color="text.secondary">
          Updated: {item.updatedAt.toLocaleDateString()}
        </Typography>
      </CardContent>
    </Card>
  );
}
