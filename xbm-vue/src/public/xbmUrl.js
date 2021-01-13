export default {
    // 行政审批侧边栏
    menulist: [
        {
            icon: "el-icon-user-solid",
            name: "个人中心",
            path: "1",
            childrenMenu: [
                {
                    icon: "el-icon-waller",
                    name: "主页",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[50]&token="
                },
                {
                    icon: "el-icon-waller",
                    name: "代办任务",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[52]&token="
                },
                
                {
                    icon: "el-icon-waller",
                    name: "办结任务",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[51]&token="
                },
                {
                    icon: "el-icon-waller",
                    name: "个人信息",
                    path: "/approval/personalInformation"
                }
            ]
        },
        {
            icon: "el-icon-s-order",
            name: "行政审批",
            path: "2",
            childrenMenu: [
                {
                    icon: "el-icon-waller",
                    name: "矿权审批",
                    path: "21",
                    showAll: false,
                    children: [
                        {
                            name: "划定矿区范围",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[4]&token="
                        },
                        {
                            name: "采矿权扩大矿区范围变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[35]&token="
                        },

                        {
                            name: "采矿权缩小矿区范围变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[40]&token="
                        },
                        {
                            name: "采矿权延续登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[8]&token="
                        },

                        {
                            name: "采矿权注销登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[30]&token="
                        },
                        {
                            name: "采矿权新立登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[7]&token="
                        },
                        {
                            name: "采矿权开采方式变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[50]&token="
                        },
                        {
                            name: "采矿权开采主矿种变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[45]&token="
                        },

                        {
                            name: "采矿权人名称变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[55]&token="
                        },
                        {
                            name: "采矿权转让变更登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[205].[60]&token="
                        },
                        {
                            name: "采矿许可证补发",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[210].[3]&token="
                        },
                        {
                            name: "采矿权抵押备案",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[214].[3]&token="
                        },
                        {
                            name: "矿产资源储量评审备案",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[215].[3]&token="
                        },
                        {
                            name: "占用矿产资源储量登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[216].[2]&token="
                        },
                        {
                            name: "残留矿产资源储量登记",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[217].[2]&token="
                        },
                        {
                            name: "矿山地质环境保护与土地复垦方案",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[232].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "测绘管理",
                    path: "22",
                    showAll: false,
                    children: [
                        {
                            name: "测绘作业证核发",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[223].[2]&token="
                        },
                        {
                            name: "地图审核审批",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[224].[2]&token="
                        },
                        {
                            name:
                                "法人或者其他组织需要利用属于国家秘密的基础测绘成果审批",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[225].[2]&token="
                        },
                        {
                            name: "测绘任务备案",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[226].[2]&token="
                        },
                        {
                            name: "乙、丙、丁级测绘资质认定初审",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[204].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "建设工程规划许可",
                    path: "23",
                    showAll: false,
                    children: [
                        {
                            name: "建筑类建设工程规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[206].[3]&token="
                        },
                        {
                            name: "市政类建设工程规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[207].[2]&token="
                        },
                        {
                            name: "交通类建设工程规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[208].[3]&token="
                        },
                        {
                            name: "临时建设工程规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[209].[2]&token="
                        },
                        {
                            name: "建设工程规划许可证延期",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[211].[2]&token="
                        },
                        {
                            name: "建设工程规划许可变更",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[212].[2]&token="
                        },
                        {
                            name: "建设工程规划许可证注销",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[213].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "建设用地使用权与规划许可",
                    path: "24",
                    showAll: false,
                    children: [
                        {
                            name: "划拨国有建设用地使用权审核和用地规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[239].[6]&token="
                        },
                        {
                            name: "划拨或出让国有建设用地改变用途审核",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[239].[12]&token="
                        },
                        {
                            name: "划拨国有建设用地使用权出租审核",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[240].[3]&token="
                        },
                        {
                            name: "划拨国有建设用地使用权转让审核",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[239].[19]&token="
                        },
                        {
                            name:
                                "划拨国有建设用地使用权转为协议出让国有建设用地使用权审核",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[237].[6]&token="
                        },
                        {
                            name: "租赁国有建设用地使用权审核",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[239].[25]&token="
                        },
                        {
                            name: "国有建设用地使用权续期",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[241].[3]&token="
                        },
                        {
                            name: "国有建设用地使用权收回",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[242].[3]&token="
                        },
                        {
                            name: "临时建设用地规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[209].[2]&token="
                        },
                        {
                            name: "协助执行过户建设用地规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[236].[9]&token="
                        },
                        {
                            name: "建设用地规划许可证延期",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[235].[13]&token="
                        },
                        {
                            name: "建设用地规划许可变更",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[235].[27]&token="
                        },
                        {
                            name: "建设用地规划许可证注销",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[235].[20]&token="
                        },
                        {
                            name: "出让地转让建设用地规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[236].[3]&token="
                        },
                        {
                            name: "协议出让国有建设用地使用权审核及用地规划许可",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[238].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "预审选址",
                    path: "25",
                    showAll: false,
                    children: [
                        {
                            name: "建设项目用地预审及选址意见书核发",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[234].[2]&token="
                        },
                        {
                            name: "建设项目用地预审及选址意见书延期",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[233].[4]&token="
                        },
                        {
                            name: "建设项目用地预审及选址意见书变更",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[233].[11]&token="
                        },
                        {
                            name: "建设项目用地预审及选址意见书注销",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[233].[18]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "乡村建设规划许可",
                    path: "26",
                    children: [
                        {
                            name: "村民住宅乡村建设规划许可证核发",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[227].[2]&token="
                        },
                        {
                            name: "企业、公共设施和公益事业乡村建设规划许可证核发",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[228].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "建设工程验线",
                    path: "27",
                    children: [
                        {
                            name: "建设工程验线",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[229].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "建设工程规划核实",
                    path: "28",
                    children: [
                        {
                            name: "建设工程规划核实",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[230].[2]&token="
                        }
                    ]
                },
                {
                    icon: "el-icon-waller",
                    name: "行政奖励",
                    path: "29",
                    showAll: false,
                    children: [
                        {
                            name: "地质灾害防治工作中做出突出贡献奖励",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[218].[2]&token="
                        },
                        {
                            name: "对勘查、开发、保护矿产资源和进行科学技术研究的奖励",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[219].[2]&token="
                        },
                        {
                            name: "古生物化石保护工作中做出突出成绩奖励",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[220].[2]&token="
                        },
                        {
                            name: "对（全省）节约集约示范县（市）创建的奖励",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[221].[2]&token="
                        },
                        {
                            name: "土地调查工作中做出突出贡献奖励",
                            path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[222].[2]&token="
                        }
                    ]
                }
            ]
        },
        {
            icon: "el-icon-setting",
            name: "系统设置",
            path: "3",
            childrenMenu: [
                {
                    icon: "el-icon-waller",
                    name: "组织机构管理",
                    path: "/approval/Organization"
                },
                {
                    icon: "el-icon-waller",
                    name: "日志管理",
                    path: "/approval/LogMagementguan"
                }, {
                    icon: "el-icon-waller",
                    name: "数据字典设置",
                    path: "/approval/Dictionaries"
                }, {
                    icon: "el-icon-waller",
                    name: "内部邮件",
                    path: "/approval/email"
                }, {
                    icon: "el-icon-waller",
                    name: "附件管理",
                    path: "/approval/Enclosure"
                }, {
                    icon: "el-icon-waller",
                    name: "日常用语",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[269].[19]&token="
                }, {
                    icon: "el-icon-waller",
                    name: "文档管理",
                    path: "/approval/filingCabinet"
                }, {
                    icon: "el-icon-waller",
                    name: "日程管理",
                    path: "/approval/schedule"
                }, {
                    icon: "el-icon-waller",
                    name: "通讯录管理",
                    path: "/approval/address"
                }, {
                    icon: "el-icon-waller",
                    name: "通知公告",
                    path: "/approval/notic"
                }, {
                    icon: "el-icon-waller",
                    name: "公告管理",
                    path: "/approval/notice"
                },{
                    icon: "el-icon-waller",
                    name: "政策法规",
                    path: "/approval/Laws"
                }, {
                    icon: "el-icon-waller",
                    name: "系统提醒",
                    path: "/approval/msgManage"
                }, {
                    icon: "el-icon-waller",
                    name: "便笺记事管理",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[269].[17]&token="
                },{
                    icon: "el-icon-waller",
                    name: "一张图权限管理",
                    path: "/approval/Jurisdiction"
                },{
                    icon: "el-icon-waller",
                    name: "行政区代码",
                    path: "/approval/region"
                }
            ]
        }
    ],
    // 政务管理
    manageList: [
        {
            // 日常办公
            data: [
                {
                    name: "行政发文",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[243].[5]&token="
                },
                {
                    name: "窗口发文",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[243].[12]&token="
                },
                {
                    name: "月工作台账",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[244].[2]&token="
                },
                {
                    name: "周工作台账",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[245].[4]&token="
                },
                {
                    name: "地信周工作台账",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[245].[11]&token="
                },
                {
                    name: "收文管理",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[246].[4]&token="
                },
                {
                    name: "科室业务信息发布",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[247].[5]&token="
                },
                {
                    name: "政务信息上报",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[248].[3]&token="
                },
                {
                    name: "会议纪要",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[249].[3]&token="
                },
                

                {
                    name: "局长办公会交办",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[277].[6]&token="
                },
                {
                    name: "重点项目",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[285].[6]&token="
                }
            ]
        },
        {
            // 业务审批
            data: [
                {
                    name: "土地登记",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[252].[4]&token="
                },
                {
                    name: "权籍调查",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[266].[4]&token="
                },
                {
                    name: "权籍调查窗口",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[266].[3]&token="
                },
                {
                    name: "权籍无图",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[267].[2]&token="
                },
                {
                    name: "规划预审查",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[279].[2]&token="
                }
            ]
        },
        {
            // 综合监管
            data: [{
                name: "重要事项请示",
                path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[250].[3]&token="
            },]
        },
        {
            // 检查督办
            data: [{
                name: "领导督办件",
                path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[251].[3]&token="
            },]
        },
        {
            // 档案管理
            data: [
                {
                    name: "中心档案库",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[273].[13]&token="
                },
                {
                    name: "局长办公会交办",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[277].[6]&token="
                },
                {
                    name: "重点项目",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[285].[6]&token="
                },
                {
                    name: "土地登记卡列表",
                    path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[261].[3]&token="
                },
            ]
        },
        {
            // 查询统计
            data: [{
                name: "重要事项请示",
                path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[250].[3]&token="
            },]
        },
        {
            // 其他事项
            data: [{
                name: "重要事项请示",
                path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[250].[3]&token="
            },]
        },
        {
            // 系统管理
            data: [
                { name: "便笺记事管理", path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[269].[17]&token=" },
                { name: "日常用语管理", path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[269].[19]&token=" },
                { name: "名片信息管理", path: "/jz/XBM_Service.bsp?EXEC&Source=FORM[269].[12]&token=" },
            ]
        }
    ]
}