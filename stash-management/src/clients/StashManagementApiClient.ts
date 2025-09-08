// StashManagementApiClient.ts
// Client for interacting with StashManagement.API
import type { FabricItem } from '../entities/stashItem';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'https://localhost:7118';

export class StashManagementApiClient {
  static async getAllFabrics(profileId: string): Promise<FabricItem[]> {
    const response = await fetch(`${API_BASE_URL}/fabrics`, {
      headers: { 'profileId': profileId },
    });
    if (!response.ok) throw new Error('Failed to fetch fabrics');
    return response.json();
  }

  static async getFabricById(id: string, profileId: string): Promise<FabricItem> {
    const response = await fetch(`${API_BASE_URL}/fabric/${id}`, {
      headers: { 'profileId': profileId },
    });
    if (!response.ok) throw new Error('Failed to fetch fabric');
    return response.json();
  }

  static async createFabric(item: Omit<FabricItem, 'id' | 'createdAt' | 'updatedAt'>, profileId: string): Promise<FabricItem> {
    const response = await fetch(`${API_BASE_URL}/fabric`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json', 'profileId': profileId },
      body: JSON.stringify(item),
    });
    if (!response.ok) throw new Error('Failed to create fabric');
    return response.json();
  }

  static async updateFabric(id: string, item: Partial<FabricItem>, profileId: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/fabric/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json', 'profileId': profileId },
      body: JSON.stringify(item),
    });
    if (!response.ok) throw new Error('Failed to update fabric');
  }

  static async deleteFabric(id: string, profileId: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/fabric/${id}`, {
      method: 'DELETE',
      headers: { 'profileId': profileId },
    });
    if (!response.ok) throw new Error('Failed to delete fabric');
  }

  static async uploadFabricImage(id: string, file: File, profileId: string): Promise<void> {
    const formData = new FormData();
    formData.append('file', file);
    const response = await fetch(`${API_BASE_URL}/fabric/${id}/image`, {
      method: 'POST',
      headers: { 'profileId': profileId },
      body: formData,
    });
    if (!response.ok) throw new Error('Failed to upload fabric image');
  }
}
