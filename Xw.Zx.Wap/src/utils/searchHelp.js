const HISTORY_KEY = 'HISTORY_KEY';

export default  {
    loadSearchKey() {
        let keys = JSON.parse(localStorage.getItem(HISTORY_KEY));
        if (keys) {
            return keys;
        }
        return [];
    },

    addSearchKey(val) {
        this.removeSearchKey(val);
        let keys = this.loadSearchKey();       
        keys.splice(0, 0, val);
        localStorage.setItem(HISTORY_KEY, JSON.stringify(keys));
        return this.loadSearchKey();
    },

    removeSearchKey(val) {
        let keys = this.loadSearchKey();
        const index = keys.indexOf(val);
        if (index >= 0) {
            keys.splice(index, 1);
            localStorage.setItem(HISTORY_KEY, JSON.stringify(keys));
        }
        return this.loadSearchKey();
    }
}