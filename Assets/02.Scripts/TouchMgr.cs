﻿using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    #region  싱글톤
    //게임매니저의 인스턴스를 담는 전역변수(static 변수이지만 이해하기 쉽게 전역변수라고 하겠다).
    //이 게임 내에서 게임매니저 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것이다.
    //보안을 위해 private으로.
    private static TouchMgr instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static TouchMgr Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion
    private Camera ARCamera;
    public GameObject placeObject;

    public bool isTouch = false;

    void Start()
    {
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (isTouch == false)
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            TrackableHit hit;

            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (touch.phase == TouchPhase.Began
                && Frame.Raycast(touch.position.x
                                , touch.position.y
                                , raycastFilter
                                , out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                GameObject obj = Instantiate(placeObject
                                            , hit.Pose.position
                                            , Quaternion.identity
                                            , anchor.transform);

                var rot = Quaternion.LookRotation(ARCamera.transform.position
                                                    - hit.Pose.position);

                obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x
                                                            , rot.eulerAngles.y
                                                            , ARCamera.transform.position.z);

                isTouch = true;
            }
        }
    }
}