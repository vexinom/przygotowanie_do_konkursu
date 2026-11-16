import { useQuery } from "@tanstack/react-query";
import { ContestService } from "../services/ContestService";

export function useContest() {
    const query = useQuery({
        queryKey: ["Contests"],
        queryFn: () => ContestService.getContest({}),
        retry: 5
    })

    return {
        ...query
    }
}