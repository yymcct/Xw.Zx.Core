var root = '/jz/';
var root2 = '/jz/';
// var root2 = '/api/jz/';
var root3='/jz/web/';
var root4='/dghy/'; 
export let apiUrl = {
	//登录
	LOGIN: root + 'PAGE_1_10.bsp', //用户登录
	GET_USER_INFO: root + 'PAGE_1_19.bsp', //获取用户信息
	GET_QRCODE: root + 'page_1_115.bsp',//获取二维码
	GET_QRTYPE: root + 'page_1_114.bsp',
	GET_SERVERIP: root + 'page_1_121.bsp',//获取内外网地址

	//                     主页
	//待办事项
	GET_TO_List: root + 'page_1_82.bsp', //获取首页待办事项
	GET_EMAIL_COUNT: root + 'page_268_36.bsp', //内部邮件接口
	GET_PEND_LIST: root + 'page_268_40.bsp', //联合审查
	GET_PRO_RESERVELIB_DATA: root + 'page_268_38.bsp', //项目策划
	GET_TODO_OFFICE: root + 'page_268_37.bsp', //政务审批待办
	GET_TO_LIST_WINDOW: root3 + 'registerIndexList', //首页窗口受理
	GET_HOME_EFFICIENCY_COUNT: root + 'page_268_46.bsp', //效能监管
	GET_LEADER_EFFICIENCY_COUNT: root + 'page_268_57.bsp', //效能监管-局长
	GET_LEADER_CKEFFICIENCY_COUNT: root3 + 'acceptStatistics', //效能监管-窗口受理-局长
	CHECK_CK_EFFICIENCY_DETAIL: root3 + 'acceptStatisticsDetails', //效能监管-窗口受理-待办、办结、超期详情
	CHECK_SP_EFFICIENCY_DETAIL: root + 'page_268_42.bsp', //行政审批-待办、办结、超期详情
	CHECK_ZW_EFFICIENCY_DETAIL: root + 'page_268_48.bsp', //政务管理-待办、办结、超期详情
	CHECK_CH_EFFICIENCY_DETAIL: root + 'page_268_51.bsp', //项目策划-待办、办结、超期详情
	CHECK_SC_EFFICIENCY_DETAIL: root + 'page_268_54.bsp', //联合审查-待办、办结、超期详情

	//通知公告
	GET_LAWS_LIST: root + 'page_1_129.bsp',//法律法规列表
	ADD_LAWS: root + 'page_1_128.bsp',//新增法律法规
	DEL_LAWS: root + 'page_1_130.bsp',//删除法律法规
	CHECK_LAWS: root + 'page_1_127.bsp',//法律法规详情
	GET_LAWS_CAT: root + 'page_1_133.bsp',//法律法规目录
	ADD_LAWS_CAT: root + 'page_1_131.bsp',//新增法律法规目录
	DEL_LAWS_CAT: root + 'page_1_132.bsp',//删除法律法规目录

	//使用帮助
	GET_HELP_LIST: root + 'page_1_135.bsp',//使用帮助列表
	ADD_HELP_LIST: root + 'page_1_136.bsp',//使用帮助目录添加
	DEL_HELP_LIST: root + 'page_1_137.bsp',//使用帮助目录删除
	UPDATE_HELP_CONT: root + 'page_1_138.bsp',//使用帮助添加或编辑
	GET_HELP_CONT: root + 'page_1_140.bsp',//使用帮助详情

	//图表
	GET_SUPERVISION_COUNT: root + 'page_1_83.bsp', //督察计数统计
	GET_EFFICIENCY_COUNT: root + 'page_1_84.bsp', //业务效能统计
	//通知公告
	GET_HOME_NOTICE: root + 'page_2_5.bsp', //主页通知公告
	//学习教育
	GET_HOME_STUDY: root + 'page_268_7.bsp', //主页通知公告

	
	//快捷方式
	// GET_SHORTCUT_LIST: root + 'page_1_90.bsp', //应用列表
	// GET_SHORTCUT_SCREEN: root + 'page_1_91.bsp', //屏幕列表
	// GET_SHORTCUT_ADDSCREEN: root + 'page_1_86.bsp', //增加屏幕列表
	// GET_SHORTCUT_DELSCREEN: root + 'page_1_88.bsp', //删除屏幕列表
	// GET_SHORTCUT_ADDAPP: root + 'page_1_85.bsp', //增加应用
	// GET_SHORTCUT_DELAPP: root + 'page_1_87.bsp', //删除应用
	// GET_SHORTCUT_SCREENORDER: root + 'page_1_89.bsp', //删除应用
	//主页日志
	GET_HOME_LOG: root + 'page_1_94.bsp', //主页登录日志
	// GET_HOME_ONLINE: root + 'page_1_102.bsp', //在线人数



	//==================系统管理============

	// --------菜单管理----------
	GET_AUTH_MENU_LIST: root + 'PAGE_1_15.bsp', //权限菜单列表
	GET_MENU_LIST: root + 'PAGE_1_52.bsp', //菜单列表
	ADD_LEVEL1_MENU: root + 'PAGE_1_53.bsp', //新增一级菜单
	ADD_LEVEL2_MENU: root + 'PAGE_1_56.bsp', //新增二级菜单
	UPDATE_LEVEL1_MENU: root + 'PAGE_1_54.bsp', //更新一级菜单
	UPDATE_LEVEL2_MENU: root + 'PAGE_1_57.bsp', //更新二级菜单
	DEL_MENU: root + 'PAGE_1_55.bsp', //删除菜单
	GET_APPROVAL_MENU: root + 'page_2_2.bsp',//业务审批菜单
	GET_MANAGE_MENU: root + 'page_339_1.bsp',//政务管理菜单
	// --------组织机构管理----------
	// GET_ORG_LIST: root + 'PAGE_1_65.bsp', //组织机构列表
	GET_ORG_LIST: root + 'PAGE_1_20.bsp', //组织机构列表
	GET_USER_LIST: root + 'PAGE_1_21.bsp', //用户列表

	ADD_USER: root + 'PAGE_1_22.bsp', //用户信息新增
	EDIT_USER: root + 'PAGE_1_23.bsp', //用户信息修改
	DEL_USER: root + 'PAGE_1_24.bsp', //用户信息修改

	GET_DEPART_LIST: root + 'PAGE_1_25.bsp', //部门管理查询列表
	ADD_DEPART: root + 'PAGE_1_26.bsp', //部门管理_新增
	EDIT_DEPART: root + 'PAGE_1_27.bsp', //部门管理_修改
	DEL_DEPART: root + 'PAGE_1_28.bsp', //部门管理_删除

	GET_UNIT_LIST: root + 'PAGE_1_29.bsp', //单位管理列表
	ADD_UNIT: root + 'PAGE_1_30.bsp', //单位添加
	RESET_PASSWORD: root + 'page_1_31.bsp', //重置用户密码


	//版本查询
	GET_QUERY_VERSION: root + 'page_1_59.bsp', //版本查询
	//计数器管理
	GET_COUNT_LIST: root + 'page_1_26.bsp', //获取计数器列表
	GET_COUNT_ADD: root + 'page_1_23.bsp', //新增计数器
	GET_COUNT_MODIFY: root + 'page_1_25.bsp', //修改计数器
	GET_COUNT_SEARCH: root + 'page_1_27.bsp', //计数器查询
	GET_COUNT_DEL: root + 'page_1_24.bsp', //计数器删除
	GET_COUNT_TYPE: root + 'page_1_28.bsp', //获取业务类型


	//附件管理
	GET_ENCLOSURE_LIST: root + 'page_1_21.bsp', //获取附件列表
	GET_ENCLOSURE_DEL: root + 'page_1_20.bsp', //附件删除
	GET_ENCLOSURE_SEARCH: root + 'page_1_60.bsp', //附件搜索
	SAVE_ENCLOSURE: root + 'page_1_22.bsp', //附件保存

	//接口管理
	GET_INTERFACE_LIST: root + 'page_1_17.bsp', //获取接口管理列表
	GET_INTERFACE_SEARCH: root + 'page_1_61.bsp', //接口查询
	GET_INTERFACE_ADD: root + 'page_1_63.bsp', //接口添加
	GET_INTERFACE_DEL: root + 'page_1_64.bsp', //接口删除
	GET_INTERFACE_EDIT: root + 'page_1_62.bsp', //接口修改
	GET_INTERFACE_LISTCHILD: root + 'page_1_69.bsp', //接口修改
	GET_INTERFACE_DETAIL: root + 'page_1_66.bsp', //接口详情

	//日志管理
	GET_LOG_LIST: root + 'page_1_30.bsp', //获取登录日志管理列表
	GET_LOG_SEARCH: root + 'page_1_31.bsp', //登录日志查询
	GET_LOG_DEL: root + 'page_1_32.bsp', //登录日志删除
	GET_LOG_ADD: root + 'page_1_95.bsp', //登录日志添加
	GET_LOG_LEAVE: root + 'page_1_96.bsp', //登录日志注销时间
	GET_OPERATION_LOG_LIST: root + 'page_1_118.bsp', //获取操作日志管理列表
	GET_OPERATION_LOG_DEL: root + 'page_1_119.bsp', //操作日志删除

	//窗口受理
	GET_AuthIssue_list:root+'page_1_149.bsp',//接件事项列表
	GET_AuthIssue_File_list:root+'page_1_156.bsp',//事项列表材料清单

	Get_Acceptance_List:root3 + 'registerList',//数据列表--工改接口
	Approval_Acceptance:root3 + 'accept',//受理通过--工改接口
	TurnBack_Acceptance:root3 + 'patchApply',//补齐补正--工改接口
	Update_Acceptance_State:root3 + 'updateState',//修改状态--工改接口
	Get_fileList:root3 + 'queryAttrByProjid',//获取附件--工改接口
	GET_ISSUE_fileList:root + 'page_1_156.bsp',//根据事项获取材料清单
	SAVE_ACCEPT_INFO:root3 + 'loggingData',//接件保存录入
	GET_ACCEPT_COUNT:root3 + 'statisticalData',//统计分析日、月统计
	CHECK_DISK_PATH:root3 + 'queryAttrFileByUrl',//查询附件路径
	//电子证照
	GET_CATALOG_INFO:'/dz_exchange/api/license/findCatalogInfo',//证照类型选择下拉框
	GET_LICENSEINFO_LIST:'/dz_exchange/api/license/findLicenseInfo',//证照查询列表
	DOWNLOAD_LICENSEFILE:'/dz_exchange/api/license/downLicenseFileByZzid',//证照文件下载
	GET_EXPIRE_LICENSEFILE_LIST:'/dz_exchange/api/license/expireLicenseInfo',//证照作废列表
	CANCEL_LICENSEFILE:'/dz_exchange/api/license/licenseInvalid',//证照作废操作
	GET_AUTHLICENSE_LIST:root+'page_268_64.bsp',//证照类型权限列表
	
	//公示公告
	GET_HOME_ANNOUNCEMENT: '/dz_exchange/api/license/queryLicense', //公示公告
	GET_HOME_ANNOUNCEMENT_DETAIL: '/dz_exchange/api/license/queryLicenseInfoDetails', //公示公告详情

	//材料清单
	GET_FileSheets:root3 + 'page_1_154.bsp',//审批事项材料清单
	SAVE_FileSheets:root3 + 'page_1_153.bsp',//附件保存
	DEL_FileSheets:root3 + 'page_1_155.bsp',//附件删除

	// 数据字典
	GET_BJLX_DICT: root4 + 'page_206_3.bsp', //办件类型
	GET_SXMC_DICT: root4 + 'page_206_4.bsp', //事项名称
	GET_PROJECT_REGIST_TYPE: root4 + 'page_204_14.bsp', //立项类型
	GET_CARD_DICT: root4 + 'page_206_14.bsp', //有效证件
	GET_DECLARE_SOURCE: root4 + 'page_206_15.bsp', //申报来源
	GET_APPROVAL_TYPE: root4 + 'page_206_16.bsp', //审批类型
	GET_PROJECT_NATURE: root4 + 'page_206_17.bsp', //项目性质
	GET_PROJECT_TYPE: root4 + 'page_204_3.bsp', //项目类型
	GET_PROJECT_SOURCE: root4 + 'page_204_4.bsp', //项目投资来源
	GET_CONSTRUCTIVE_NATURE: root4 + 'page_204_6.bsp', //建设性质
	SAVE_PLAN_JOINT_DATA: root4 + 'page_206_2.bsp', //带方案接件保存、发送
	SAVE_NOPLAN_JOINT_DATA: root4 + 'page_206_5.bsp', //不带方案接件保存、发送
	GET_LEGAL_PERSON: root4 + 'page_206_14.bsp', //法人有效证件
	GET_OWER_PERSON: root4 + 'page_206_20.bsp', //个人有效证件

	//数据字典设置
	GET_DICTIONARIES_PARENTS: root + 'page_1_47.bsp', //数据字典父节点的添加
	GET_DICTIONARIES_CHILD: root + 'page_1_42.bsp', //数据字典子节点的添加
	GET_DICTIONARIES_DETAIL: root + 'page_1_35.bsp43', //数据字典详情
	GET_DICTIONARIES_LIST: root + 'page_1_45.bsp', //字典列表
	GET_DICTIONARIES_DEL: root + 'page_1_44.bsp', //字典删除
	GET_DICTIONARIES_EDIT: root + 'page_1_46.bsp', //字典修改

	//行政区代码
	GET_REGION_LIST: root + 'PAGE_268_5.bsp',//列表
	// GET_REGION_LIST:root+'PAGE_268_2.bsp',//列表
	UPDATE_REGION_LIST: root + 'PAGE_268_3.bsp',//增加与修改
	DEL_REGION_LIST: root + 'PAGE_268_4.bsp',//删除

	//数据对象管理
	GET_DATABASEMAGEMENT_LIST: root + 'page_1_35.bsp', //数据对象列表
	GET_DATABASEMAGEMENT_DETAIL: root + 'page_1_37.bsp', //数据对象详情
	GET_DATABASEMAGEMENT_EDIT: root + 'page_1_38.bsp', //数据对象修改
	GET_DATABASEMAGEMENT_DEL: root + 'page_1_40.bsp', //数据对象删除
	GET_DATABASEMAGEMENT_DELLIST: root + 'page_1_39.bsp', //数据对象删除整个列表

	//权限管理
	GET_JURISDICTION_LIST: root + 'page_1_48.bsp', //个人权限列表
	GET_JURISDICTION_PERSONLIST: root + 'page_1_77.bsp', //角色权限，业务管理
	GET_JURISDICTION_ALLLIST: root + 'page_1_49.bsp', //所有角色权限
	GET_JURISDICTION_ADD: root + 'page_1_50.bsp', //业务角色权限添加
	GET_JURISDICTION_DEL: root + 'page_1_51.bsp', //删除业务角色权限


	//即时消息
	GET_INSTANT_SEND: root2 + 'page_1_81.bsp', //发送即时消息
	GET_INSTANT_LIST: root2 + 'page_1_79.bsp', //消息列表
	GET_NEW_INSTANT_LIST: root2 + 'PAGE_1_108.bsp', //刷新用户列表
	GET_INSTANT_PERSON: root2 + 'page_1_80.bsp', //消息人员


	//==================个人事务============

	// --------电子邮件----------
	GET_INBOX_LIST: root + 'PAGE_2_16.bsp', //收件箱
	GET_OUTBOX_LIST: root + 'PAGE_2_15.bsp', //发件箱
	ADD_EMAIL: root + 'PAGE_2_12.bsp', //写邮件
	REPLY_EMAIL: root + 'PAGE_2_19.bsp', //回复邮件
	GET_EMAIL_PERSON_LIST: root + 'PAGE_2_13.bsp', //部门列表--选择人员
	GET_EMAIL_LOAD_PERSON_LIST: root + 'page_2_71.bsp', //部门人员列表
	GET_EMAIL_DEPART_LIST: root + 'page_2_70.bsp', //部门列表

	CHECK_EMAIL_DETAIL: root + 'PAGE_2_17.bsp', //查看邮件详情
	GET_DRAFT_LIST: root + 'PAGE_2_14.bsp', //草稿箱
	SUBMIT_DRAFT_FORM: root + 'PAGE_2_20.bsp', //草稿箱发送
	GET_UNREAD_NUM: root + 'PAGE_2_18.bsp', //获取未读条数
	DEL_EMAIL: root + 'PAGE_2_44.bsp', //电子邮件删除
	ADD_SELF_GROUP: root + 'page_2_58.bsp',//电子邮件自定义添加分组
	ADD_SELF_GROUP_PERSON: root + 'page_2_59.bsp',//电子邮件自定义添加分组成员
	ADD_SELF_GROUP_LIST: root + 'page_2_60.bsp',//电子邮件自定义分组列表
	DEL_SELF_GROUP: root + 'page_2_61.bsp',//电子邮件删除自定义分组
	DEL_SELF_GROUP_PERSON: root + 'page_2_62.bsp',//电子邮件删除自定义分组人员
	  SEARCH_EMAIL_OUT_LIST:root+'Page_2_15.bsp',//查询已发送列表
    SEARCH_EMAIL_INBOX_LIST:root+'Page_2_16.bsp',//查询收件列表
    SEARCH_EMAIL_DRAFT_LIST:root+'Page_2_14.bsp',//查询草稿箱列表





	//通讯簿
	GET_ADDRESS_LIST: root + 'PAGE_2_41.bsp', //通讯簿列表
	ADD_ADDRESS: root + 'PAGE_2_28.bsp', //通讯簿新增
	EDIT_ADDRESS: root + 'PAGE_2_29.bsp', //通讯簿修改
	DEL_ADDRESS: root + 'PAGE_2_30.bsp', //通讯簿删除
	SEL_ADDRESS_DEPART: root + 'PAGE_2_40.bsp', //通讯簿部门选择
	SEL_ADDRESS_PERSON: root + 'PAGE_2_49.bsp', //个人通讯录列表
	SEL_ADDRESS_DEL: root + 'page_2_54.bsp', //公共通讯录删除权限

	//通知公告
	GET_RELEASED_NOTICE: root + 'page_2_5.bsp', //通知公告列表-已发布
	GET_UNRELEASED_NOTICE: root + 'page_2_11.bsp', //通知公告列表-未发布
	ADD_NOTICE: root + 'page_2_6.bsp', //通知公告新建
	CHECK_NOTICE: root + 'page_2_7.bsp', //通知公告详情
	EDIT_NOTICE: root + 'page_2_10.bsp', //通知公告修改
	DEL_NOTICE: root + 'page_2_8.bsp', //通知公告删除
	UPDATE_NOTICE_STATE: root + 'page_2_43.bsp', //通知公告更新发布状态
	GET_NOTICE_LIST: root + 'page_2_5.bsp', //通知公告列表（模糊查询）
	GET_MY_NOTICE_LIST: root + 'page_2_11.bsp', //本人公告列表（模糊查询）

	//消息管理
	GET_MSG_LIST: root2 + 'PAGE_2_39.bsp', //消息管理列表
	GET_MSG_PUSH_LIST: root2 + 'page_2_3.bsp', //推送消息列表
	GET_MSG_ALL_LIST: root2 + 'page_2_55.bsp', //全部消息列表
	GET_MSG_UPDATE_LIST: root2 + 'page_2_4.bsp', //推送消息状态更新

	//个人文件柜
	GET_FILE_LIST: root + 'page_2_37.bsp', //个人文件柜列表
	GET_FILE_CATALOG: root + 'page_2_33.bsp', //个人文件柜目录
	GET_FILE_CATALOG_ADD: root + 'page_2_35.bsp', //个人文件柜目录添加
	GET_FILE_CATALOGFILE_ADD: root + 'page_2_45.bsp', //个人文件柜目录文件添加
	GET_FILE_CATALOGFILE_DEL: root + 'page_2_50.bsp', //个人文件柜目录文件删除
	GET_FILE_CATALOG_DEL: root + 'page_2_51.bsp', //个人文件柜目录删除
	GET_FILE_CATALOG_BTN_AUTH: root + 'page_2_72.bsp', //个人文件柜上传删除权限

	//个人信息
	GET_PERSONAL_INFORMATION: root + 'page_1_32.bsp', //个人信息
	GET_PERSONAL_INFORMATION_EDIT: root + 'page_1_33.bsp', //个人信息修改
	GET_PERSONAL_INFORMATION_PASSWORD: root + 'page_1_34.bsp', //密码修改
	GET_PERSONAL_INFORMATION_DEPARTMENT: root + 'page_1_35.bsp', //个人部门获取

	//惯用语
	GET_IDIOMS_ADD: root + 'page_2_22.bsp', //惯用语添加
	GET_IDIOMS_MY: root + 'page_2_23.bsp', //我的惯用语
	GET_IDIOMS_DEL: root + 'page_2_24.bsp', //惯用语删除
	GET_IDIOMS_EDIT: root + 'page_2_25.bsp', //惯用语修改
	GET_IDIOMS_NUM: root + '/page_2_26.bsp', //惯用语使用次数


	//日程安排
	GET_SCHEDULE_LIST: root + 'page_2_65.bsp',//日程列表
	GET_SCHEDULE_ADD: root + 'page_2_63.bsp',//日程安排增加
	GET_SCHEDULE_EDIT: root + 'page_2_64.bsp',//日程安排修改
	GET_SCHEDULE_DEL: root + 'page_2_66.bsp',//日程安排删除


}
