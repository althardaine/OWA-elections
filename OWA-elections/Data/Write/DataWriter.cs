using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OWA_elections.Data.Write
{
    public static class DataWriter
    {
       
        public static void WriteData(List<Candidate> candidates, HashSet<Voter> voters, string outputFile)
        {
            using (var file = new StreamWriter(outputFile))
            {
                file.WriteLine(candidates.Count);
                foreach (var candidate in candidates)
                {
                    file.WriteLine(candidate.Id + "," + candidate.Name);
                }
                file.WriteLine(voters.Count);
                foreach (var sortedRankList in voters.Select(voter => (from entry in voter.RankList orderby entry.Value ascending select entry)))
                {
                    foreach (var entry in sortedRankList)
                    {
                        if ((int) entry.Value != candidates.Count - 1)
                        {
                            file.Write(entry.Key.Id + ","); 
                        }
                        else
                        {
                            file.Write(entry.Key.Id);                            
                        }
                    }
                    file.WriteLine();
                }
            } 
        }

    }
}
