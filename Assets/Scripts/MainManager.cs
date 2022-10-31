using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    [SerializeField]
    Transform speedText, spinnerImage;

    float f;
    /// <summary>
    /// 上一帧的图片坐标与鼠标间的向量
    /// </summary>
    Vector3 frontSpinnerVector;

    /// <summary>
    /// 速度
    /// </summary>
    float speed;
    float s;

    bool b;

    void Start()
    {
    }

    void Update()
    {
        //1.物体跟随手指滑动旋转
        //2.滑动结束后，物体根据其最后的速度大小进行判断是否要进行匀减速旋转
        //2.计算转速 ：圈/分钟

        //图片的坐标固定
        //滑动时，鼠标点击的坐标不固定
        //用每帧之间的图片和鼠标点击的向量的夹角作为旋转的角度（速度）


        //滑动时的变化(暂时先用鼠标滑动代替，便于测试)
        if (Input.GetMouseButton(0))
        {
            if (frontSpinnerVector != Vector3.zero)
            {
                //利用向量叉乘来确定旋转方向
                if (Vector3.Cross(frontSpinnerVector, Input.mousePosition - spinnerImage.position).z > 0)
                {
                    f = 1f;
                }
                else
                {
                    f = -1f;
                }

                //利用Vector3.Angle来计算两帧的向量之间的角度
                spinnerImage.rotation *= Quaternion.AngleAxis( f * Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position)), Vector3.forward);
                
                
                if (Vector3.Angle(frontSpinnerVector, Input.mousePosition - spinnerImage.position) != 0f)
                {
                    s = Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position));
                    speed = Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position)) * (1 / Time.deltaTime) * 60 / 360;
                    speedText.GetComponent<Text>().text = string.Format(Mathf.FloorToInt(speed) + "圈/每分钟");
                }
                else
                {
                    speedText.GetComponent<Text>().text = string.Format(0 + "圈/每分钟");
                }
            }
            
            frontSpinnerVector = Input.mousePosition - spinnerImage.position;
        }
        if (Input.GetMouseButtonUp(0))
        {
            frontSpinnerVector  =  Vector3.zero;
            b = true;
        }




        if (b)
        {
            if (speed > 0)
            {
                speed = s * (1 / Time.deltaTime) * 60 / 360;

                s-= 0.05f;

                spinnerImage.rotation *= Quaternion.AngleAxis(f*s, Vector3.forward);

                speedText.GetComponent<Text>().text = string.Format(Mathf.FloorToInt(speed) + "圈/每分钟");
            }
            else
            {
                b = false;
                speedText.GetComponent<Text>().text = string.Format(0 + "圈/每分钟");
            }
        }

        


    }

    
     

}
