package othello;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GridLayout;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.border.BevelBorder;
import javax.swing.border.LineBorder;

public class BoardDisplay extends JFrame{

	//白石と黒石の初期位置
	final int WHITE_COL1 = 3;
	final int WHITE_ROW1 = 3;
	final int WHITE_COL2 = 4;
	final int WHITE_ROW2 = 4;

	final int BLACK_COL1 = 3;
	final int BLACK_ROW1 = 4;
	final int BLACK_COL2 = 4;
	final int BLACK_ROW2 = 3;

	//盤の行数
	final int BOARD_COL = 8;
	final int BOARD_ROW = 8;

	//マス目の線の太さ
	final int LINE_THICK = 1;

	//フレームの幅と高さ
	final int FRAME_WIDTH = 500;
	final int FRAME_HEIGHT = 500;

	//碁盤パネルの幅と高さ
	final int BOARD_PANEL_WIDTH = 100;
	final int BOARD_PANEL_HEIGHT = 100;

	//戦況パネルの幅と高さ
	final int BATTLE_SITUATION_PANEL_WIDTH = 100;
	final int BATTLE_SITUATION_PANEL_HEIGHT = 100;

	//石のサイズ
	final int BUTTON_FONT_SIZE = 27;

	//戦況パネルのプレイヤー名
	JLabel label1 = new JLabel("Player1：");
	JLabel label2 = new JLabel("Player2：");

	//戦況パネルの石の数
	JLabel text1 = new JLabel("0");
	JLabel text2 = new JLabel("0");

	//フレームのコンストラクタ&タイトル付け
	JFrame frame = new JFrame("Othello Game");

	//パネル作成
	JPanel board_panel = new JPanel();

	JPanel battle_situation_panel =  new JPanel();

	//8×8の盤(ボタン形式)を作成する
	JButton [][] button = new JButton[BOARD_COL][BOARD_ROW];

	/* オセロ盤を描画したウインドウを表示する(コンストラクタ) */
	public BoardDisplay(){

		//フレームサイズの指定(幅、高さ)
		frame.setSize(FRAME_WIDTH, FRAME_HEIGHT);

		// 表示位置とフレームサイズの指定(x座標、y座標、幅、高さ)
		//frame.setBounds(100, 100, 300, 250);

		//表示位置を必ず画面の中央にする
		frame.setLocationRelativeTo(null);

		//右上の×ボタンがクリックされた時アプリケーションを終了する
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		// 碁盤パネルの幅・高さ
		board_panel.setPreferredSize(new Dimension(BOARD_PANEL_WIDTH, BOARD_PANEL_HEIGHT));

		// 碁盤パネルの背景色
		board_panel.setBackground(Color.GREEN);

		// 碁盤パネルを8×8にする
		board_panel.setLayout(new GridLayout(BOARD_COL, BOARD_ROW));

		// 碁盤パネルに枠線を追加する
		BevelBorder board_border = new BevelBorder(BevelBorder.RAISED);
		board_panel.setBorder(board_border);


		// 戦況パネルの幅・高さ
		battle_situation_panel.setPreferredSize(new Dimension(BATTLE_SITUATION_PANEL_WIDTH, BATTLE_SITUATION_PANEL_HEIGHT));

		// 戦況パネルの背景色
		battle_situation_panel.setBackground(Color.WHITE);

		// 戦況パネルに枠線を追加する
		BevelBorder battle_situation_border = new BevelBorder(BevelBorder.RAISED);
		battle_situation_panel.setBorder(battle_situation_border);

		battle_situation_panel.add(label1);
		battle_situation_panel.add(text1);
		battle_situation_panel.add(label2);
		battle_situation_panel.add(text2);

		Container contentPane = getContentPane();
		//パネルを中央にセット
		contentPane.add(board_panel,BorderLayout.CENTER);

		contentPane.add(battle_situation_panel,BorderLayout.SOUTH);

		StoneSet();

		//戦況パネルをフレームに追加
		frame.add(battle_situation_panel,BorderLayout.SOUTH);

		//碁盤パネルをフレームに追加
		frame.add(board_panel,BorderLayout.CENTER);

		//フレームの表示
		frame.setVisible(true);
	}

	/* 盤を表すパネルにボタンを追加 + 初期石を設置 */
	public void StoneSet() {
		for (int i = 0; i < BOARD_COL; i++) {
			for (int j = 0; j < BOARD_ROW; j++) {
				button[i][j] = new JButton();
				button[i][j].setBorder(new LineBorder(Color.BLACK, LINE_THICK, true));
				button[i][j].setBackground(Color.GREEN);
				button[i][j].setFont(new Font("ＭＳ ゴシック", Font.BOLD, BUTTON_FONT_SIZE));
				board_panel.add(button[i][j]);
				if ((i == WHITE_COL1 && j == WHITE_ROW1) || (i == WHITE_COL2 && j == WHITE_ROW2)) {
					button[i][j].setText("●");
					button[i][j].setForeground(Color.WHITE);
				}
				else if ((i == BLACK_COL1 && j == BLACK_ROW1) || (i == BLACK_COL2 && j == BLACK_ROW2)) {
					button[i][j].setText("●");
					button[i][j].setForeground(Color.BLACK);
				}
			}
		}
	}

}
