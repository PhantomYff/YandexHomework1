using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelGenerator : MonoBehaviour
    {
        public LinkedList<LevelPiece> Level => _pieces;
        public int PiecesGenerated => _piecesGenerated;

        [field: SerializeField] public int MaxPiecesCount { get; private set; }
        [field: SerializeField] public int StartPiecesCount { get; private set; }

        [SerializeField] private LevelPiece _firstPiecePrefab;
        [SerializeField] private LevelPiece[] _prefabs;

        private WeightedRandom<LevelPiece> _random;
        private int _piecesGenerated;

        private readonly LinkedListShell<LevelPiece> _pieces = new();

        protected void Awake()
        {
            _random = new WeightedRandom<LevelPiece>(_prefabs);

            GeneratePiece(_firstPiecePrefab);
            for (int i = 1; i < StartPiecesCount; i++)
            {
                GenerateRandomPiece();
            }
        }

        private void GenerateRandomPiece()
        {
            GeneratePiece(_random.GetRandomValue(_piecesGenerated));
        }

        private void GeneratePiece(LevelPiece prefab)
        {
            var newPiece = Instantiate(prefab, parent: transform);
            newPiece.EndReached += OnPieceEndReached;
            newPiece.Init(_piecesGenerated);
            SetPosition(newPiece);

            _pieces.AddLast(newPiece);
            if (_pieces.Count > MaxPiecesCount)
            {
                var firstPiece = _pieces.First.Value;
                Destroy(firstPiece.gameObject);

                _pieces.RemoveFirst();
            }
            _piecesGenerated++;
        }

        private void OnPieceEndReached(LevelPiece piece)
        {
            piece.EndReached -= OnPieceEndReached;
            GenerateRandomPiece();
        }

        private void SetPosition(LevelPiece piece)
        {
            float xPosition;
            if (_pieces.Count > 0)
            {
                var lastPiece = _pieces.Last.Value;
                xPosition = lastPiece.transform.position.x + lastPiece.EndPosition - piece.StartPosition;
            }
            else
            {
                xPosition = -piece.StartPosition;
            }
            piece.transform.position = new Vector3(xPosition, 0f, 0f);
        }
    }
}
