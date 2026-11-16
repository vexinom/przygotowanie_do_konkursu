interface ExampleType {
    id: string,
    title: string,
    description: string,
    isDeleted: boolean,
    imageUrl: URL,
    creationDate: Date,
    creatorUserId: string,
    categories: string[]
}

export type { ExampleType }