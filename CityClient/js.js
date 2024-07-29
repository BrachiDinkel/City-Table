let cities = [];
let currentPage = 1;
const itemsInPage = 5;
let isAscending = true;
let editCityName = null;
const apiUrl = 'https://localhost:7042/api/city';

document.addEventListener('DOMContentLoaded', () => {
    fetchCities();
    document.querySelector('#addCityButton').addEventListener('click', () => {
        const cityName = document.querySelector('#cityNameInput').value;
        if (cityName) {
            addCity(cityName);
        }
    });

    document.querySelector('#searchInput').addEventListener('input', displayCities);
});

 fetchCities =()=> {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            cities = data;
            displayCities();
        })
        .catch(error => console.error('Error:', error));
}

 displayCities =()  =>{
    const cityContainer = document.getElementById('cityContainer');
    cityContainer.innerHTML = '';

    const searchInput = document.querySelector('#searchInput').value;
    const filteredCities = cities.filter(city => searchCity(city.name, searchInput));

    const paginagCities = filteredCities.slice((currentPage - 1) * itemsInPage, currentPage * itemsInPage);

    paginagCities.forEach(city => {
        const cityCard = document.createElement('div');
        cityCard.className = 'city-card';

        cityCard.innerHTML = `
            <h2>${editCityName === city.name ? `<input type="text" value="${city.name}" id="editInput">` : city.name}</h2>
            <div>
                <button onclick="editCity('${city.name}')"><i class="fas fa-edit"></i></button>
                <button class="delete" onclick="deleteCity('${city.name}')"><i class="fas fa-trash-alt"></i></button>
            </div>
        `;

        cityContainer.appendChild(cityCard);
    });
}

async function addCity(cityName) {
    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name: cityName })
        });
        if (!response.ok) {
            throw new Error(`An error occurred: ${response.statusText}`);
        }
        fetchCities();
    } catch (error) {
        console.error('Error adding city:', error);
    }
}

async function updateCity(oldName, newName) {
    try {
        const response = await fetch(`${apiUrl}/${oldName}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name: newName })
        });
        if (!response.ok) {
            throw new Error(`An error occurred: ${response.statusText}`);
        }
        fetchCities();  
    } catch (error) {
        console.error('Error updating city:', error);
    }
}

async function deleteCity(cityName) {
    try {
        const response = await fetch(`${apiUrl}/${cityName}`, {
            method: 'DELETE'
        });
        if (!response.ok) {
            throw new Error(`An error occurred: ${response.statusText}`);
        }
        fetchCities(); 
    } catch (error) {
        console.error('Error deleting city:', error);
    }
}

 editCity = (cityName) => {
    if (editCityName) {
        const newCityName = document.getElementById('editInput').value;
        updateCity(editCityName, newCityName);
        editCityName = null;
    } else {
        editCityName = cityName;
    }
    displayCities();
}

 sortCities = () => {
    isAscending = !isAscending;
    cities.sort((a, b) => isAscending ? a.name.localeCompare(b.name) : b.name.localeCompare(a.name));
    displayCities();
}

 previousPage = () => {
    if (currentPage > 1) {
        currentPage--;
        displayCities();
    }
}

 nextPage = () => {
    if (currentPage * itemsInPage < cities.length) {
        currentPage++;
        displayCities();
    }
}

searchCity = (cityName, searchInput) =>{
    const englishToHebrew = {
        a: 'ש', b: 'נ', c: 'ב', d: 'ג', e: 'ק', f: 'כ', g: 'ע', h: 'י', i: 'ן', j: 'ח', k: 'ל', l: 'ך',
        m: 'צ', n: 'מ', o: 'ם', p: 'פ', q: '/', r: 'ר', s: 'ד', t: 'א', u: 'ו', v: 'ה', w: "'", x: 'ס',
        y: 'ט', z: 'ז', ',': 'ת', '.': 'ץ'
    };

    const hebrewInput = searchInput.split('').map(char => englishToHebrew[char] || char).join('');

    return cityName.includes(searchInput) || cityName.includes(hebrewInput);
}
