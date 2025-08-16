import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import { Stack } from '@mui/material'
import './App.css'
import { StashHeader } from './components/stashHeader'
import { FabricItemCard } from './components/FabricItemCard'
import { FabricItemForm } from './components/FabricItemForm'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Stack direction="column" spacing={2} sx={{ padding: 2 }}>
        <StashHeader />
        <FabricItemCard item={{
        id: '1',
        name: 'Cotton Fabric',
        description: 'A soft and breathable cotton fabric.',
        sourceLocation: 'Warehouse A',
        createdAt: new Date(),
        updatedAt: new Date(),
        imageSrc: 'https://placehold.co/400',
        profileId: 'user-123',
        cut: '1 yard',
        amount: 5
          }} />
        <FabricItemForm onCreate={(item) => console.log('New item created:', item)} />
      </Stack>
    </>
  )
}

export default App
