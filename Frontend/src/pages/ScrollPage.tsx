import { useContest } from "../hooks/useContest"

export default function ScrollPage() {
    const { isError, error, data, isLoading } = useContest();

    if (isLoading) return (<h1>Loading in progress.</h1>)
    if (isError) return (<h1>Unexpected error ocurred. Error: {error.message}</h1>)
    if (!data) return (<h1>Nothing was found.</h1>)

    return (
        <>
            <h1> Welcom to ScrollPage! </h1>
            {data?.toString()}
        </>
    )
}