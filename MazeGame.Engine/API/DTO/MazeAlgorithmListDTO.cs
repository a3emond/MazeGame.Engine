using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.API.DTO;

public class MazeAlgorithmListDTO
{
    public List<string> AvailableAlgorithms { get; set; } = new();
}

