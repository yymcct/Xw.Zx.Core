//证照状态
const ElelicenseState = {
    '11': "有效",
    '-10': "暂时失效",
    '-4': "已过期",
	'-5': '已作废'
}
//窗口受理状态
const ckclState = {
    '0':'未受理',
    '1':'已受理',
    '2':'补齐补正告知',
    '3':'办理中',
    '4':'已办结',
    '5':'挂起'
}
const SBLYRela={'0':'工改窗口','1':'综合窗口','2':'内网录入'}
export {
    ElelicenseState,
    ckclState,
    SBLYRela
}
