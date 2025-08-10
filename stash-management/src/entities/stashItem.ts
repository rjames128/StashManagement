export interface StashItem {
  id: string;
  name: string;
  description: string;
  sourceLocation: string;
  createdAt: Date;
  updatedAt: Date;
  imageSrc: string;
  profileId: string;
}

export interface FabricItem extends StashItem {
  cut: string;
  amount: number;
}