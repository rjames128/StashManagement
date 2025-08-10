export interface StashItem {
  id: string;
  name: string;
  description: string;
  sourceLocation: string;
  createdAt: Date;
  updatedAt: Date;
  imageSrc: string;
  profileId: string;
  cut: string;
  amount: number;
}