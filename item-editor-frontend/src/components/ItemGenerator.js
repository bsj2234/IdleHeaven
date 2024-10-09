import axios from 'axios';
import React, { useEffect, useState } from 'react';

class ComponentContainer {
    constructor() {
        this.components = [];
    }

    findComponent(predicate) {
        return this.components.find(predicate);
    }

    findComponentByType(type) {
        return this.components.find(component => component instanceof type);
    }

    findComponentByName(name) {
        return this.components.find(component => component.name === name);
    }

    addComponent(component) {
        this.components.push(component);
    }

    getAllComponents() {
        return this.components;
    }
}

class ItemDataComponent {
    constructor(name, ownerName) {
        this.name = name;
        this.ownerName = ownerName;
    }
}

class EquipmentDataComponent extends ItemDataComponent {
    constructor(name, ownerName, randomEffectSetName, fixedEffectSetName) {
        super(name, ownerName);
        this.randomEffectSetName = randomEffectSetName;
        this.fixedEffectSetName = fixedEffectSetName;
    }
}


function ItemGenerator() {

    const Rarity = Object.freeze({
        COMMON: 'Common',
        UNCOMMON: 'Uncommon',
        RARE: 'Rare',
        EPIC: 'Epic',
        LEGENDARY: 'Legendary'
    });

    const [items, setItems] = useState([]);
    const [newItem, setNewItem] = useState({
        id: '',
        name: '',
        description: '',
        rarity: Rarity.COMMON,
        components: new ComponentContainer(),
        weight: 1.0
    });

    useEffect(() => {
        fetchItems();
    }, []);

    const fetchItems = async () => {
        try {
            const response = await axios.get('http://localhost:8080/api/items', {
                withCredentials: true
            });
            setItems(response.data);
        } catch (error) {
            console.error('Error fetching items:', error);
        }
    };

    const handleInputChange = (e) => {
        setNewItem({ ...newItem, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log('Sending data:', newItem); // Add this line
        try {
            const response = await axios.post('http://localhost:8080/api/items', newItem);
            console.log('Response:', response.data); // Add this line
            setNewItem({ name: '', description: '', rarity: 'Common', type: 'Equipment' });
            fetchItems();
        } catch (error) {
            console.error('Error creating item:', error.response?.data || error.message);
        }
    };

    return (
        <div>
            <h1>Item Generator</h1>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    value={newItem.name}
                    onChange={handleInputChange}
                    placeholder="Item Name"
                    required
                />
                <input
                    type="text"
                    name="description"
                    value={newItem.description}
                    onChange={handleInputChange}
                    placeholder="Item Description"
                    required
                />
                <select name="rarity" value={newItem.rarity} onChange={handleInputChange}>
                    <option value="Common">Common</option>
                    <option value="Uncommon">Uncommon</option>
                    <option value="Rare">Rare</option>
                    <option value="Epic">Epic</option>
                    <option value="Legendary">Legendary</option>
                </select>
                <select name="type" value={newItem.type} onChange={handleInputChange}>
                    <option value="Equipment">Equipment</option>
                    <option value="Consumable">Consumable</option>
                    <option value="Material">Material</option>
                </select>
                <button type="submit">Add Item</button>
            </form>
            <ul>
                {items.map((item) => (
                    <li key={item.id}>
                        {item.name} - {item.rarity} {item.type}: {item.description}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default ItemGenerator;