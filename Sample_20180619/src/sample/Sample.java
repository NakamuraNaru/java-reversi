package sample;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;

import javax.swing.JFrame;
import javax.swing.JPanel;

class Sample extends JFrame{
	public static void main(String args[]){
		Sample frame = new Sample("タイトル");
		frame.setVisible(true);
	}

	Sample(String title){
		setTitle(title);
		setBounds(100, 100, 300, 250);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		JPanel p1 = new JPanel();
		p1.setBackground(Color.BLUE);

		JPanel p2 = new JPanel();
		p2.setBackground(Color.ORANGE);

		Container contentPane = getContentPane();
		contentPane.add(p1, BorderLayout.NORTH);
		contentPane.add(p2, BorderLayout.SOUTH);
	}
}