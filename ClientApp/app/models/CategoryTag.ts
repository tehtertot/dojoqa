export class CategoryTag {
    categoryId: number;
    categoryName: string;
    tags: Array<SimpleTag>;
}

class SimpleTag {
    tagId: number;
    tagName: string;
}