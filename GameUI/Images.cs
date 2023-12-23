/**
  @file Board.cs
  @brief Класс доски - игрового поля с фигурами
  @author Листопад В.
\par Использует классы:
- @ref Piece
- @ref Player
- @ref PieceType
\par Содержит класс:
  @ref Board
*/

using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameLogic;

namespace GameUI
{
    /// Суть класса - загрузить содержимое из папки ассетов и упростить к ним доступ
    public static class Images
    {
        /// Словарь с путями к ассетам белых фигур
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn, LoadImage("Assets/PawnW.png") },
            { PieceType.Rook, LoadImage("Assets/RookW.png") },
            { PieceType.Bishop, LoadImage("Assets/BishopW.png") },
            { PieceType.Knight, LoadImage("Assets/KnightW.png") },
            { PieceType.King, LoadImage("Assets/KingW.png") },
            { PieceType.Queen, LoadImage("Assets/QueenW.png") },
        };
        /// Словарь с путями чёрных фигур
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn, LoadImage("Assets/PawnB.png") },
            { PieceType.Rook, LoadImage("Assets/RookB.png") },
            { PieceType.Bishop, LoadImage("Assets/BishopB.png") },
            { PieceType.Knight, LoadImage("Assets/KnightB.png") },
            { PieceType.King, LoadImage("Assets/KingB.png") },
            { PieceType.Queen, LoadImage("Assets/QueenB.png") },
        };

        /** Метод загрузки изображения, использует библиотекии windows.media для работы с изображениями
            Он принимает строку в качестве пути к изображению, при чём этот путь относителен
            Сами изображения будут сохранены в двух словарях
        */
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        /** Метод для получения изображения. Он принимает цвет и тип, а дальше в свиче мы выбираем нужный спрайт нужного типа и цвета
            Если цвет белый, то мы возвращаем белую фигуру нужного типа, аналогично для чёрного цвета
        */
        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => blackSources[type],
                _ => null
            };
        }

        /** В случае, если мы передали фигуру и она каким-то образом ничего не содержит, то мы возвращаем null, то есть ничто
            В противном же случае распаковываем эту фигуру и вызываем основной метод, посылая в него цвет и тип
        */
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }

            return GetImage(piece.Color, piece.Type);
        }
    }
}
