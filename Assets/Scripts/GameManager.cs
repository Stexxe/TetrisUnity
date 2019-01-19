using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour {
    private Shape shape;
    private ShapeView shapeView;
    private ShapeController shapeController;

    private Board board;
    private BoardView boardView;

    public float SpeedIncPercent;
    public int ScoreSpeedInc;

    public GameObject Menu;
    public Text Score;
    private String scoreTitle;

    private bool gameRunning;
    private bool gameOver = false;

    private int score = 0;

    private GameSettings settings;

    public AudioSource cleanAudio;
    public AudioSource loseAudio;

    void Start () {
        settings = GetComponent<GameSettings>();

        scoreTitle = Score.text;
        UpdateScore();

        gameRunning = true;

        boardView = GetComponent<BoardView>();
        board = new Board(boardView.Rows, boardView.Columns);

        shapeView = GetComponent<ShapeView>();

        shape = SpawnShape(settings.ShapeStartSpeed);
    }
	
	void Update () {
        if (!gameOver) {

            if (gameRunning) {
                shapeController.ReceiveInput();

                StartCoroutine(shapeController.Fall(stopped => {
                    if (stopped) {
                        board.PutBlocks(shape.GetCurrentBlocks());
                        boardView.PutBlocks(shape.GetCurrentBlocks());

                        int cleanedCount;
                        var cleanedSome = board.CleanFullRows(out cleanedCount);

                        if (cleanedSome) {
                            cleanAudio.Play();
                            score += cleanedCount;
                            UpdateScore();
                            shape.Speed = RecalcSpeed();
                            boardView.EraseBlocks();
                            boardView.PutBlocks(board.GetBlocks());
                        }

                        // Game over
                        if (board.IsOverflow()) {
                            loseAudio.Play();
                            Over();
                        }

                        shape = SpawnShape(shape.Speed);
                    }
                }));
            }
            

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (gameRunning) {
                    Pause();
                } else {
                    Resume();
                }
            }
        }
    }

    private void Pause() {
        gameRunning = false;
        Menu.SetActive(true);
    }

    private void Resume() {
        gameRunning = true;
        Menu.SetActive(false);
    }

    private void Over() {
        gameOver = true;
        Menu.SetActive(true);
    }


    private void UpdateScore() {
        Score.text = String.Format("{0} {1}", scoreTitle, score);
    }

    private float RecalcSpeed() {
        var maxPercent = 100.0f;
        return shape.Speed * (score / ScoreSpeedInc / maxPercent * SpeedIncPercent + 1);
    }

    private Shape SpawnShape(float shapeSpeed) {
        var shp = ShapeFactory.CreateRandom();
        shp.Pos = new Cell(0, 5);
        shp.Speed = shapeSpeed;
        shp.MovementRestrict = board.IsEmptyCell;
        shapeView.PlaceBlocks(shp.GetCurrentBlocks());

        shapeController = new ShapeController(shp, shapeView);

        return shp;
    }
}
