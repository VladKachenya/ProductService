import { HttpTransportType, HubConnectionBuilder } from '@aspnet/signalr';

// export async function getAuthToken(server, nick)
// {
//     const response = await fetch(`${server}/api/auth/token?nick=${nick}`);
//     return await response.text();
// }

export async function connectToServer(server)
{
    return new HubConnectionBuilder()
            .withUrl(`${server}/product_changes`, {
                transport: HttpTransportType.LongPolling
            })
            .build();
}