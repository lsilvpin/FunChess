using FunChess.Core.Enums;
using FunChess.Core.Exceptions;
using FunChess.Core.Factory;
using FunChess.Core.Models.Pieces;
using FunChess.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunChess.Core.Models
{
    public class Board
    {
        private readonly CoreFactory core;

        public Piece[,] Grid { get; set; }
        public EnPassant EnPassant { get; set; }


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

        public void PrintInConsole()
        {
            for (int i = 7; i >= -1; i--)
            {
                for (int j = -1; j <= 7; j++)
                {
                    WriteInConsoleAccordingToPosition(i, j);
                }

                BreakTwoLinesInConsole();
            }
        }

        public override string ToString()
        {
            StringBuilder boardString = new StringBuilder();

            for (int i = 7; i >= -1; i--)
            {
                for (int j = -1; j <= 7; j++)
                {
                    WriteInBoardStringAccordingToPosition(i, j, boardString);
                }

                boardString.Append(Environment.NewLine);
                boardString.Append(Environment.NewLine);
            }

            return boardString.ToString();
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


        private void BreakTwoLinesInConsole()
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        private void WriteInConsoleAccordingToPosition(int line, int column)
        {
            if (column == -1)
            {
                if (line == -1)
                {
                    Console.Write("      ");
                }
                else
                {
                    Console.Write($"  {line + 1}  ");
                }
            }
            else
            {
                Position position = core.CreatePosition(line, column);

                if (line == -1)
                {
                    Console.Write($" {position.ToString()[0]}    ");
                }
                else
                {
                    Piece piece = LookAt(position);
                    WriteSignInConsoleAccordingToPiece(piece);
                }
            }
        }

        private void WriteSignInConsoleAccordingToPiece(Piece piece)
        {
            if (piece == null)
            {
                WriteBlankSign(WritingMode.InConsole);
            }
            else
            {
                WritePieceSign(WritingMode.InConsole, piece);
            }
        }

        private void WriteInBoardStringAccordingToPosition(int line, int column, StringBuilder boardString)
        {
            if (column == -1)
            {
                if (line == -1)
                {
                    boardString.Append("      ");
                }
                else
                {
                    boardString.Append($"  {line + 1}  ");
                }
            }
            else
            {
                Position position = core.CreatePosition(line, column);

                if (line == -1)
                {
                    boardString.Append($" {position.ToString()[0]}    ");
                }
                else
                {
                    Piece piece = LookAt(position);
                    WriteInBoardStringAccordinToPiece(piece, boardString);
                }
            }
        }

        private void WriteInBoardStringAccordinToPiece(Piece piece, StringBuilder boardString)
        {
            if (piece == null)
            {
                WriteBlankSign(WritingMode.AsString, boardString);
            }
            else
            {
                WritePieceSign(WritingMode.AsString, piece, boardString);
            }
        }

        private void WritePieceSign(WritingMode writingMode, Piece piece, StringBuilder stringBuilder = null)
        {
            string pieceSign = $"  {piece}  ";

            if (writingMode == WritingMode.InConsole)
            {
                WritePieceSignInConsoleRespectingPieceColor(pieceSign, piece.Color);
            }
            else
            {
                stringBuilder.Append(pieceSign);
            }
        }

        private void WritePieceSignInConsoleRespectingPieceColor(string pieceSign, PieceColor pieceColor)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = (pieceColor == PieceColor.White) ? ConsoleColor.Yellow : ConsoleColor.Blue;
            Console.Write(pieceSign);
            Console.ForegroundColor = originalColor;
        }

        private void WriteBlankSign(WritingMode writingMode, StringBuilder stringBuilder = null)
        {
            const string blankSign = "  --  ";

            if (writingMode == WritingMode.InConsole)
            {
                Console.Write(blankSign);
            }
            else
            {
                stringBuilder.Append(blankSign);
            }
        }
    }
}
