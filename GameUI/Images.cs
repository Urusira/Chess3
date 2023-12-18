using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameLogic;

namespace GameUI
{
    // Суть класса - загрузить содержимое из папки ассетов и упростить к ним доступ
    public static class Images
    {
        // Словарь с путями к ассетам белых фигур
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
        };
        // Словарь с путями чёрных фигур
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
        };

        /*
         * Метод загрузки изображения, использует библиотекии windows.media для работы с изображениями
         * Он принимает строку в качестве пути к изображению, при чём этот путь относителен
         * Сами изображения будут сохранены в двух словарях
         */
        private static ImageSource LoadImage(string filePath)
        {
        }

        /*
         * Метод для получения изображения. Он принимает цвет и тип, а дальше в свиче мы выбираем нужный спрайт нужного типа и цвета
         * Если цвет белый, то мы возвращаем белую фигуру нужного типа, аналогично для чёрного цвета
         */
        public static ImageSource GetImage(Player color, PieceType type)
        {
        }

        /*
         * В случае, если мы передали фигуру и она каким-то образом ничего не содержит, то мы возвращаем null, то есть ничто
         * В противном же случае распаковываем эту фигуру и вызываем основной метод, посылая в него цвет и тип
         */
        public static ImageSource GetImage(Piece piece)
        {
        }
    }
}
