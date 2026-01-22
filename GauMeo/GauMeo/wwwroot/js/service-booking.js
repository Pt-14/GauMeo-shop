/**
 * Service Booking JavaScript - Enhanced Version
 * Handles form interactions, price calculations, and pet-service matching
 */

document.addEventListener('DOMContentLoaded', function() {
    console.log('=== SERVICE BOOKING SCRIPT LOADED ===');
    initializeBookingForm();
});

function initializeBookingForm() {
    console.log('=== INITIALIZING BOOKING FORM ===');
    
    // Set minimum date to tomorrow
    const bookingDateInput = document.getElementById('BookingDate');
    if (bookingDateInput) {
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        bookingDateInput.min = tomorrow.toISOString().split('T')[0];
        
        // Set default date to tomorrow if not set
        if (!bookingDateInput.value) {
            bookingDateInput.value = tomorrow.toISOString().split('T')[0];
        }
        console.log('Date input set to:', bookingDateInput.value);
    }

    // Enable submit button - Force enable regardless
    const submitBtn = document.getElementById('submit-btn');
    if (submitBtn) {
        submitBtn.disabled = false;
        submitBtn.style.opacity = '1';
        submitBtn.style.cursor = 'pointer';
        console.log('Submit button enabled successfully!');
    } else {
        console.error('Submit button not found!');
    }

    // Initialize price calculation
    setupPriceCalculation();
    
    // Initialize pet-service matching
    setupPetServiceMatching();
    
    // Calculate initial price
    updatePriceCalculation();
    
    console.log('=== BOOKING FORM INITIALIZED ===');
}

function setupPriceCalculation() {
    console.log('Setting up price calculation...');
    
    // Listen for variant selection changes
    const variantRadios = document.querySelectorAll('input[name="ServiceVariantId"]');
    console.log('Found variant radios:', variantRadios.length);
    variantRadios.forEach(radio => {
        radio.addEventListener('change', updatePriceCalculation);
    });
    
    // Listen for addon selection changes
    const addonCheckboxes = document.querySelectorAll('input[name="SelectedAddonIds"]');
    console.log('Found addon checkboxes:', addonCheckboxes.length);
    addonCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', updatePriceCalculation);
    });
}

function setupPetServiceMatching() {
    console.log('Setting up pet-service matching...');
    
    const petTypeSelect = document.getElementById('PetType');
    const petSizeSelect = document.querySelector('select[name="PetSize"]');
    
    if (petTypeSelect) {
        petTypeSelect.addEventListener('change', function() {
            console.log('Pet type changed to:', this.value);
            filterServiceVariants();
            updatePetServiceWarning();
        });
    }
    
    if (petSizeSelect) {
        petSizeSelect.addEventListener('change', function() {
            console.log('Pet size changed to:', this.value);
            filterServiceVariants();
            updatePetServiceWarning();
        });
    }
    
    // Listen for variant selection to check compatibility
    const variantRadios = document.querySelectorAll('input[name="ServiceVariantId"]');
    variantRadios.forEach(radio => {
        radio.addEventListener('change', updatePetServiceWarning);
    });
}

function filterServiceVariants() {
    const petType = document.getElementById('PetType')?.value;
    const petSize = document.querySelector('select[name="PetSize"]')?.value;
    const variantCards = document.querySelectorAll('.variant-card');
    
    console.log(`Filtering variants for pet type: ${petType}, size: ${petSize}`);
    
    variantCards.forEach(card => {
        const radio = card.querySelector('input[name="ServiceVariantId"]');
        const variantPetType = radio?.dataset.petType;
        const variantPetSize = radio?.dataset.petSize;
        
        let isCompatible = true;
        
        // Check pet type compatibility
        if (petType && variantPetType && variantPetType !== 'both' && variantPetType !== petType) {
            isCompatible = false;
        }
        
        // Check pet size compatibility
        if (petSize && variantPetSize && variantPetSize !== 'all' && variantPetSize !== petSize) {
            isCompatible = false;
        }
        
        // Update card appearance
        if (isCompatible) {
            card.style.opacity = '1';
            card.style.pointerEvents = 'auto';
            card.classList.remove('incompatible');
        } else {
            card.style.opacity = '0.5';
            card.style.pointerEvents = 'none';
            card.classList.add('incompatible');
            // Uncheck if currently selected
            if (radio?.checked) {
                radio.checked = false;
                updatePriceCalculation();
            }
        }
    });
}

function updatePetServiceWarning() {
    const petType = document.getElementById('PetType')?.value;
    const petSize = document.querySelector('select[name="PetSize"]')?.value;
    const selectedVariant = document.querySelector('input[name="ServiceVariantId"]:checked');
    
    // Remove existing warnings
    const existingWarnings = document.querySelectorAll('.pet-service-warning');
    existingWarnings.forEach(warning => warning.remove());
    
    if (selectedVariant && (petType || petSize)) {
        const variantPetType = selectedVariant.dataset.petType;
        const variantPetSize = selectedVariant.dataset.petSize;
        const variantName = selectedVariant.closest('.variant-card').querySelector('.variant-name')?.textContent;
        
        let warningMessage = '';
        
        // Check pet type mismatch
        if (petType && variantPetType && variantPetType !== 'both' && variantPetType !== petType) {
            const petTypeName = petType === 'dog' ? 'chó' : 'mèo';
            const variantPetTypeName = variantPetType === 'dog' ? 'chó' : 'mèo';
            warningMessage = `⚠️ Dịch vụ "${variantName}" chỉ dành cho ${variantPetTypeName}, nhưng bạn đã chọn ${petTypeName}.`;
        }
        
        // Check pet size mismatch
        if (petSize && variantPetSize && variantPetSize !== 'all' && variantPetSize !== petSize) {
            const sizeNames = {
                'small': 'nhỏ (< 5kg)',
                'medium': 'trung bình (5-15kg)',
                'large': 'lớn (> 15kg)'
            };
            const variantSizeName = sizeNames[variantPetSize] || variantPetSize;
            const petSizeName = sizeNames[petSize] || petSize;
            
            if (warningMessage) warningMessage += '<br>';
            warningMessage += `⚠️ Dịch vụ "${variantName}" chỉ dành cho thú cưng kích thước ${variantSizeName}, nhưng bạn đã chọn ${petSizeName}.`;
        }
        
        // Display warning if any
        if (warningMessage) {
            const warningDiv = document.createElement('div');
            warningDiv.className = 'pet-service-warning alert alert-warning';
            warningDiv.innerHTML = warningMessage;
            warningDiv.style.cssText = 'margin-top: 10px; padding: 10px; background: #fff3cd; border: 1px solid #ffeaa7; border-radius: 5px; color: #856404;';
            
            selectedVariant.closest('.variant-card').appendChild(warningDiv);
        }
    }
}

function updatePriceCalculation() {
    console.log('=== UPDATING PRICE CALCULATION ===');
    
    let basePrice = 0;
    let addonsPrice = 0;
    
    // Get selected variant price
    const selectedVariant = document.querySelector('input[name="ServiceVariantId"]:checked');
    if (selectedVariant) {
        basePrice = parseFloat(selectedVariant.dataset.price) || 0;
        console.log('Selected variant price:', basePrice);
    } else {
        console.log('No variant selected');
    }
    
    // Get selected addons price
    const selectedAddons = document.querySelectorAll('input[name="SelectedAddonIds"]:checked');
    selectedAddons.forEach(addon => {
        addonsPrice += parseFloat(addon.dataset.price) || 0;
    });
    console.log('Selected addons price:', addonsPrice);
    
    // Update display
    const basePriceElement = document.getElementById('base-price');
    const addonsPriceElement = document.getElementById('addons-price');
    const totalPriceElement = document.getElementById('total-price');
    
    if (basePriceElement) {
        basePriceElement.textContent = basePrice.toLocaleString('vi-VN') + ' VNĐ';
        basePriceElement.parentElement.style.display = basePrice > 0 ? 'flex' : 'none';
    }
    
    if (addonsPriceElement) {
        addonsPriceElement.textContent = addonsPrice.toLocaleString('vi-VN') + ' VNĐ';
        addonsPriceElement.parentElement.style.display = addonsPrice > 0 ? 'flex' : 'none';
    }
    
    if (totalPriceElement) {
        const totalPrice = basePrice + addonsPrice;
        totalPriceElement.textContent = totalPrice.toLocaleString('vi-VN') + ' VNĐ';
        console.log('Total price updated:', totalPrice);
    }
    
    console.log('=== PRICE CALCULATION COMPLETE ===');
}
