public class SocialNetwork
{
    public Dictionary<string, List<string>> network { get; set; }
    public SocialNetwork()
    {
        network = new Dictionary<string, List<string>>();
    }
    public void AddUser(string name)
    {
        if (!network.ContainsKey(name))
        {
            network[name] = new List<string>();
            System.Console.WriteLine($"{name} has been added");
        }
        else
        {
            System.Console.WriteLine($"{name} already exists");
        }
    }

    public Dictionary<string, List<string>> GetNetwork()
    {
        return network;
    }

    public void RemoveUser(string name)
    {
        if (network.ContainsKey(name))
        {
            network.Remove(name);
            foreach (var friendList in network.Values)
            {
                if (friendList.Contains(name))
                    friendList.Remove(name);
            }
            System.Console.WriteLine($"{name} has been removed from the network.");
        }
        else
        {
            System.Console.WriteLine($"{name} does not exist.");
        }
    }
    public void AddFriend(string user1, string user2)
    {
        if (!network.ContainsKey(user1) || !network.ContainsKey(user2))
        {
            System.Console.WriteLine($"One or both users do not exist.");
            return;

        }
        if (network[user1].Contains(user2) && network[user2].Contains(user1))
        {
            System.Console.WriteLine($"{user1} and {user2} are already friends.");
            return;
        }

        network[user1].Add(user2);
        network[user2].Add(user1);
        System.Console.WriteLine($"{user1} and {user2} are now friends.");
    }
    public void RemoveFriend(string user1, string user2)
    {
        if (!network.ContainsKey(user1) || !network.ContainsKey(user2))
        {
            System.Console.WriteLine($"One or both users do not exist.");
            return;
        }
        if (!network[user1].Contains(user2) && !network[user2].Contains(user1))
        {
            System.Console.WriteLine($"{user1} and {user2} are not friends.");
            return;
        }

        network[user1].Remove(user2);
        network[user2].Remove(user1);
        System.Console.WriteLine($"{user1} and {user2} are no longer friends.");
    }

    public void DisplayFriends(string user)
    {
        if (!network.ContainsKey(user))
        {
            System.Console.WriteLine($"{user} does not exist.");
            return;
        }
        if (network[user].Count == 0)
        {
            System.Console.WriteLine($"{user} has no friends.");
            return;
        }

        List<string> friendList = new List<string>();
        foreach (var friend in network[user])
        {
            friendList.Add(friend);
        }

        System.Console.WriteLine($"{user}'s friends: {string.Join(", ", friendList)}");
    }
    public void FindMutualFriends(string user1, string user2)
    {
        if (!network.ContainsKey(user1) || !network.ContainsKey(user2))
        {
            System.Console.WriteLine($"One or both users do not exist.");
            return;
        }
        List<string> mutualFriends = new List<string>();
        foreach (var friend1 in network[user1])
        {
            foreach (var friend2 in network[user2])
            {
                if (friend1 == friend2)
                {
                    mutualFriends.Add(friend1);
                }
            }
        }
        if (mutualFriends.Count == 0)
        {
            System.Console.WriteLine($"{user1} and {user2} have no mutual friends.");
            return;
        }

        System.Console.WriteLine($"Mutual friends of {user1} and {user2}: {string.Join(", ", mutualFriends)}");

    }

    public void SuggestFriends(string user)
    {
        if (!network.ContainsKey(user))
        {
            System.Console.WriteLine($"{user} does not exist.");
            return;
        }
        List<string> suggestions = new List<string>();
        foreach (var friend in network[user])
        {
            foreach (var friendOfFriend in network[friend])
            {
                if (!network[user].Contains(friendOfFriend) && friendOfFriend != user)
                {
                    if (!suggestions.Contains(friendOfFriend))
                        suggestions.Add(friendOfFriend);
                }
            }
        }
        if (suggestions.Count == 0)
        {
            System.Console.WriteLine($"No friend suggestions for {user}.");
            return;
        }
        suggestions = suggestions.Select(f => f + $" ({network[user].Intersect(network[f]).Count()} mutual friends)").ToList();
        System.Console.WriteLine($"Friend suggestions for {user}: {string.Join(", ", suggestions)}");
    }
}
