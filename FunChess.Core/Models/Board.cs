using FunChess.Core.Exceptions;
using FunChess.Core.Factory;
using FunChess.Core.Models.Pieces;
using FunChess.Core.Tools;
using System;
using System.Collections.Generic;

namespace FunChess.Core.Models
{
    public class Board
    {
        private readonly CoreFactory core;

        public Piece[,] Grid { get; set; }

        public Board() { }
        public Board(CoreFactory core)
        {
            this.core = core;
            Grid = new Piece[8, 8];
        }

        public void PutAt(Piece pieceAtHand, Position target)
        {
            Piece pieceAtTarget = Grid[target.Line, target.Column];
            if (pieceAtTarget != null)
            {
                throw new ThereIsNoRoomForYourPieceAtThisPositionException(pieceAtHand, pieceAtTarget, target);
            }

            Grid[target.Line, target.Column] = pieceAtHand;
            pieceAtHand.Position = target;
        }

        public Piece TakeAt(Position position)
        {
            Piece piece = Grid[position.Line, position.Column];
            Grid[position.Line, position.Column] = null;
            return piece;
        }

        public Piece LookAt(Position position)
        {
            Cloner cloner = core.CreateCloner();
            Piece piece = Grid[position.Line, position.Column];

            if (piece == null)
                return null;

            return piece.GetType().Name switch
            {
                nameof(King) => cloner.Clone<King>(piece),
                nameof(Queen) => cloner.Clone<Queen>(piece),
                nameof(Bishop) => cloner.Clone<Bishop>(piece),
                nameof(Knight) => cloner.Clone<Knight>(piece),
                nameof(Rook) => cloner.Clone<Rook>(piece),
                _ => cloner.Clone<Pawn>(piece)
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Board board &&
                   EqualityComparer<Piece[,]>.Default.Equals(Grid, board.Grid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Grid);
        }
    }
}
