const CATEGORYITEM_KEY = 'CATEGORYITEM_KEY';

export default {
    loadCategoryIteam() {
        let item = localStorage.getItem(CATEGORYITEM_KEY);
        return item;
    },

    addCategoryIteam(val) {
        let item = this.loadCategoryIteam();

        if (item != val) {
            localStorage.setItem(CATEGORYITEM_KEY, val);
        }
        return this.loadCategoryIteam();
    },
}