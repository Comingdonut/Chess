using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Views
{
    internal class Rules
    {
        internal string Title { get; private set; }
        internal string[] RuleOptions { get; private set; }
        // Basic Rules
        internal string Objective { get; private set; }
        internal string MovesFirst { get; private set; }
        internal string TurnTaking { get; private set; }
        internal string Movement { get; private set; }
        internal string StaleMate { get; private set; }
        internal string Check { get; private set; }
        internal string CheckMate { get; private set; }
        // Piece Movement
        internal string Pawn { get; private set; }
        internal string Knight { get; private set; }
        internal string Bishop { get; private set; }
        internal string Rook { get; private set; }
        internal string Queen { get; private set; }
        internal string King { get; private set; }
        // Special Conditions
        internal string EnPassant { get; private set; }
        internal string Castling { get; private set; }
        internal string PawnPromotion { get; private set; }
        internal Rules()
        {
            Title = "Rules";
            RuleOptions = new string[] { "Basic Rules", "Piece Movement", "Special Conditions", "Back"};
            // Basic Rules
            Objective = "The objective of the game is essentially TRAP the enemy KING in a position \n" 
                + "where they cannot escape or be saved by other opposing pieces.";
            MovesFirst = "The PLAYER who controls the WHITE pieces moves first.";
            TurnTaking = "When one PLAYER is done with their turn, the next PLAYER goes next. This \n"
                + "continues until the end of the game.";
            Movement = "A PLAYER has to move when it is their turn. Each PLAYER can only move once.";
            StaleMate = "If moving any piece results in CHECK, then the game ends in a STALEMATE.";
            Check = "When the KING is threaten with CAPTURE but cannot escape or eliminate the piece \n"
                + "that threatens it, then the King is in Check. If the KING is in CHECK, then the PLAYER\n"
                + " is required to removed the threat of CAPTURE and cannot end their turn until the threat\n"
                + " is removed.";
            CheckMate = "If the KING is in CHECK and cannot remove CHECK in any legal way,\n"
                + "then the PLAYER who's KING is in CHECK loses.";
            // Piece Movement
            Pawn = "PAWN - Move forward 1 or 2 spaces if they haven't moved yet.\n"
                + "Moves one space if they already moved.\n"
                + "Can only capture enemy piece if it's at the front and diagnolly across of pawn.";
            Knight = "KNIGHT - Can move forward 1 right 2, forward 2 right 1, forward 2 left 1,\n"
                + " forward 1 left 2, backward 1 right 2, backward 2 right 1, backward 2 left 1\n"
                + " and backward 1 left 2";
            Bishop = "BISHOP - Can only move diagnolly forward right, forward left, backward left, and backward\n"
                + "right.";
            Rook = "ROOK - Can only move forward, backward, left, and right.";
            Queen = "QUEEN - Can only move forward, backward, left, right,\n"
                + "diagnolly forward right, forward left, backward left, and backward right.";
            King = "KING - Can move 1 space forward, 1 space backward, 1 space left, 1 space right\n"
                + ", 1 space diagnolly forward right, forward left, backward left, and backward right.";
            // Special Conditions
            EnPassant = "EN PASSANT - When a PAWN moves two spaces forward and an enemy PAWN is directyly\n"
                + "to the left or right. The enemy PAWN can move diagnolly in front of the pawn to CAPTURE it.";
            Castling  = "Castling - If a ROOK can move next to it's KING and the KING and ROOK have not yet\n"
                + "moved, then the ROOK can move next to the KING and the KING can move to the other side of\n"
                + " the ROOK.";
            PawnPromotion  = "Pawn Promotion - If a PAWN reaches the enemy side of the board,\n"
                + "then the PAWN can be promoted to any piece that is not a KING or a PAWN.";
        }
    }
}
