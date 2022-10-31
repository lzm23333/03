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
    /// ��һ֡��ͼƬ���������������
    /// </summary>
    Vector3 frontSpinnerVector;

    /// <summary>
    /// �ٶ�
    /// </summary>
    float speed;
    float s;

    bool b;

    void Start()
    {
    }

    void Update()
    {
        //1.���������ָ������ת
        //2.������������������������ٶȴ�С�����ж��Ƿ�Ҫ�����ȼ�����ת
        //2.����ת�� ��Ȧ/����

        //ͼƬ������̶�
        //����ʱ������������겻�̶�
        //��ÿ֮֡���ͼƬ��������������ļн���Ϊ��ת�ĽǶȣ��ٶȣ�


        //����ʱ�ı仯(��ʱ������껬�����棬���ڲ���)
        if (Input.GetMouseButton(0))
        {
            if (frontSpinnerVector != Vector3.zero)
            {
                //�������������ȷ����ת����
                if (Vector3.Cross(frontSpinnerVector, Input.mousePosition - spinnerImage.position).z > 0)
                {
                    f = 1f;
                }
                else
                {
                    f = -1f;
                }

                //����Vector3.Angle��������֡������֮��ĽǶ�
                spinnerImage.rotation *= Quaternion.AngleAxis( f * Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position)), Vector3.forward);
                
                
                if (Vector3.Angle(frontSpinnerVector, Input.mousePosition - spinnerImage.position) != 0f)
                {
                    s = Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position));
                    speed = Vector3.Angle(frontSpinnerVector, (Input.mousePosition - spinnerImage.position)) * (1 / Time.deltaTime) * 60 / 360;
                    speedText.GetComponent<Text>().text = string.Format(Mathf.FloorToInt(speed) + "Ȧ/ÿ����");
                }
                else
                {
                    speedText.GetComponent<Text>().text = string.Format(0 + "Ȧ/ÿ����");
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

                speedText.GetComponent<Text>().text = string.Format(Mathf.FloorToInt(speed) + "Ȧ/ÿ����");
            }
            else
            {
                b = false;
                speedText.GetComponent<Text>().text = string.Format(0 + "Ȧ/ÿ����");
            }
        }

        


    }

    
     

}
