---
--- UI助手类
---
local UIUtils = Class("UIUtils")

local COMMON_COLORS = {
    Red = Color(225, 0, 0),
    Green = Color(0, 225, 0),
    Blue = Color(0, 0, 225),
    White = Color(240, 244, 244),
    Black = Color(17, 37, 69),
    Gray = Color(91, 93, 112),
    Yellow = Color(227, 147, 7),
}

-- 获取I18N文字
function UIUtils.GetText(id)
    local cfg = ConfigMgr:Instance():GetItem("Language",id)
    if cfg then
        return cfg.text or ""
    end
    return ""
end

function UIUtils.GetColor(name)
    return COMMON_COLORS[name] or COMMON_COLORS.White
end

return UIUtils


