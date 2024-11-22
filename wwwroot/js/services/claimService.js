const claimService = {
    async getAllClaims() {
        const response = await fetch('/api/claims');
        if (!response.ok) throw new Error('Failed to fetch claims');
        return await response.json();
    },

    async approveClaim(id) {
        const response = await fetch(`/api/claims/approve/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) throw new Error('Failed to approve claim');
        return await response.json();
    },

    async rejectClaim(id) {
        const response = await fetch(`/api/claims/reject/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) throw new Error('Failed to reject claim');
        return await response.json();
    }
};