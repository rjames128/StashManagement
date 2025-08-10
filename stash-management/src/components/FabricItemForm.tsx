import { useState } from 'react';
import type { ChangeEvent, FormEvent } from 'react';
import { TextField, Button, Card, CardContent, Typography, Select, MenuItem, InputLabel, FormControl } from '@mui/material';
import type { FabricItem } from '../entities/stashItem';

interface FabricItemFormProps {
  onCreate: (item: FabricItem) => void;
}

export function FabricItemForm({ onCreate }: FabricItemFormProps) {
  const [form, setForm] = useState({
    name: '',
    description: '',
    sourceLocation: '',
    imageSrc: '',
    cut: '',
    amount: 0,
  });
  const [imagePreview, setImagePreview] = useState<string | null>(null);

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: name === 'amount' ? Number(value) : value }));
  };

  const handleSelectChange = (e: any) => {
    setForm(prev => ({ ...prev, cut: e.target.value }));
  };

  const handleImageChange = (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      const url = URL.createObjectURL(file);
      setForm(prev => ({ ...prev, imageSrc: url }));
      setImagePreview(url);
    }
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    const newItem: FabricItem = {
      id: crypto.randomUUID(),
      profileId: '',
      createdAt: new Date(),
      updatedAt: new Date(),
      ...form,
    };
    onCreate(newItem);
  setForm({ name: '', description: '', sourceLocation: '', imageSrc: '', cut: '', amount: 0 });
  setImagePreview(null);
  };

  return (
    <Card sx={{ maxWidth: 400, margin: '1rem auto' }}>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          Add a New Stash Item
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField label="Name" name="name" value={form.name} onChange={handleChange} fullWidth margin="normal" required />
          <TextField label="Description" name="description" value={form.description} onChange={handleChange} fullWidth margin="normal" />
          <TextField label="Source Location" name="sourceLocation" value={form.sourceLocation} onChange={handleChange} fullWidth margin="normal" />
          <Button variant="outlined" component="label" fullWidth sx={{ my: 2 }}>
            Upload Image
            <input type="file" accept="image/*" hidden onChange={handleImageChange} />
          </Button>
          {imagePreview && (
            <img src={imagePreview} alt="Preview" style={{ width: '100%', maxHeight: 200, objectFit: 'cover', marginBottom: 16 }} />
          )}
          <FormControl fullWidth margin="normal">
            <InputLabel id="cut-label">Cut</InputLabel>
            <Select
              labelId="cut-label"
              id="cut"
              name="cut"
              value={form.cut}
              label="Cut"
              onChange={handleSelectChange}
              required
            >
              <MenuItem value="None">None</MenuItem>
              <MenuItem value="Scraps">Scraps</MenuItem>
              <MenuItem value="Fat Quarters">Fat Quarters</MenuItem>
              <MenuItem value="Yards">Yards</MenuItem>
            </Select>
          </FormControl>
          <TextField label="Amount" name="amount" type="number" value={form.amount} onChange={handleChange} fullWidth margin="normal" />
          <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
            Create
          </Button>
        </form>
      </CardContent>
    </Card>
  );
}
