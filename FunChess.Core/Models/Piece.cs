using FunChess.Core.Enums;
using FunChess.Core.Exceptions;
using FunChess.Core.Factory;
using System;
using System.Collections.Generic;

namespace FunChess.Core.Models
{
    public abstract class Piece
    {
        protected readonly CoreFactory core;
        protected bool[,] permissionMatrix;

        public PieceColor Color { get; set; }
        public Position Position { get; set; }

        protected Piece() { }
        protected Piece(CoreFactory core)
        {
            this.core = core;
        }
        protected Piece(CoreFactory core, PieceColor color)
        {
            this.core = core;
            permissionMatrix = new bool[8, 8];
            Color = color;
            Position = new Position(-1, -1);
        }

        public abstract bool[,] GetPermissionMatrix(Board board);

        public int CountPermitedPositions()
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (permissionMatrix[i, j])
                        count++;
                }
            }
            return count;
        }

        public HashSet<Position> GetAllowedSet()
        {
            HashSet<Position> allowedSet = new HashSet<Position>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (permissionMatrix[i, j])
                        allowedSet.Add(core.CreatePosition(i, j));
                }
            }
            return allowedSet;
        }

        public void ValidateIfPositionIsInsideLimits()
        {
            if (Position.Line < 0 || Position.Line > 7 || Position.Column < 0 || Position.Column > 7)
                throw new PieceOutOfBoardException(GetType(), Color);
        }

        public override bool Equals(object obj)
        {
            return obj is Piece piece &&
                   Color == piece.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color);
        }

        protected void ApprovePosition(bool[,] permissionMatrix, Position position)
        {
            permissionMatrix[position.Line, position.Column] = true;
        }
    }
}
