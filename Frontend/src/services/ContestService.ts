import { apiDelete, apiGet, apiPost, apiPut } from "./api.ts"
import type { Contest } from "../types/Contest.ts";

export const ContestService = {
    getContest: ({/* possible pagination information */}) => apiGet<[Contest]>(`/Contest`),
    getContestById: (id: string) => apiGet<Contest>(`/Contest/${id}`),
    deleteContest: (id: string) => apiDelete(`/Contest/${id}`),
    editContest: (id: string, body: Contest | {}) => apiPut<Contest>(`/Contest/${id}`, body),
    postContest: (body: Contest | {}) => apiPost<Contest>(`/Contest`, body)
};