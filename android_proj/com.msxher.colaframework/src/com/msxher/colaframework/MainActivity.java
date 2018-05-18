package com.msxher.colaframework;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.app.Application;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.Menu;
import android.view.MenuItem;
//import android.widget.Toast;

public class MainActivity extends UnityPlayerActivity {

	private static String receiveObj = "GameLauncher";
	private static int BUFFER_SIZE = 1024;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	/**
	 * Android������ؼ�ʱ�Ķ���ȷ�Ͽ�
	 */
	public void ShowConfirmDialog(){
		 Builder builder = new AlertDialog.Builder(this);
		 builder.setTitle("Cola��ʾ");
		 builder.setMessage("ȷ���˳�Cola��Ϸ��");
		 
		 //��������İ�ť�¼�
		 builder.setPositiveButton("ȷ��", new DialogInterface.OnClickListener() {
			
			@Override
			public void onClick(DialogInterface dialog, int which) {
//				Toast.makeText(getApplicationContext(),"�����ȷ��",Toast.LENGTH_SHORT).show();
				UnityPlayer.UnitySendMessage(receiveObj, "ApplicationQuit","0");
			}
		});
		 
		 builder.setNegativeButton("ȡ��", new DialogInterface.OnClickListener() {
			
			@Override
			public void onClick(DialogInterface dialog, int which) {
//				Toast.makeText(getApplicationContext(),"�����ȡ��",Toast.LENGTH_SHORT).show();				
			}
		});
		 builder.setCancelable(true);
		 AlertDialog dialog = builder.create();
		 dialog.show();
	}
	
	/**
	 * �����ļ�
	 * 
	 * @param src Դ�ļ�
	 * @param dest Ŀ���ļ�
	 */
	public void copy(Context context, String zipPath, String targetPath) {
		if (TextUtils.isEmpty(zipPath) || TextUtils.isEmpty(targetPath)) {
			return;
		}
		File dest = new File(targetPath);
		dest.getParentFile().mkdirs();
		InputStream in = null;
		OutputStream out = null;
		try {
			in = new BufferedInputStream(context.getAssets().open(zipPath));
			out = new BufferedOutputStream(new FileOutputStream(dest));
			byte[] buffer = new byte[BUFFER_SIZE];
			int length = 0;
			while ((length = in.read(buffer)) != -1) {
				out.write(buffer, 0, length);
			}
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			try {
				out.close();
				in.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}

	/**
	 * ����assets�ļ����ļ���ָ��·��
	 * 
	 * @param assetDir  Դ�ļ�/�ļ���
	 * @param dir  Ŀ���ļ���
	 */
	public boolean copyAssets(String assetDir, String targetDir) {
		if (TextUtils.isEmpty(assetDir) || TextUtils.isEmpty(targetDir)) {
			return true;
		}
		String separator = File.separator;
		boolean ret = false;
		try {
			Context context = this.getApplicationContext();
			// ��ȡassetsĿ¼assetDir��һ�������ļ��Լ��ļ���
			String[] fileNames = context.getResources().getAssets().list(assetDir);
			// ������ļ���(Ŀ¼),������ݹ����
			if (fileNames.length > 0) {
				File targetFile = new File(targetDir);
				if (!targetFile.exists() && !targetFile.mkdirs()) {
					return false;
				}
				for (String fileName : fileNames) {
					copyAssets(assetDir + separator + fileName, targetDir + separator + fileName);
				}
			} else { // �ļ�,��ִ�п���
				copy(context, assetDir, targetDir);
			}
			ret = true;
		} catch (Exception e) {
			e.printStackTrace();
			ret = false;
		}
		return ret;
	}
   
}
