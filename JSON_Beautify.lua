#!/usr/local/bin/lua
--[[
;
; "JSON_Beautify" // JSON_BnU
;
; Tested Lua Version:    5.2.1
; Language:       English
; Author:         Joe DF  |  http://joedf.users.sf.net  |  joedf@users.sourceforge.net
; Date:           May 8th, 2014
;
--]]

function JSON_Uglify(JSON)
	JSON = string.gsub(JSON,"%s$","") --trim spaces

	len = string.len(JSON)
	if (len==0) then
		return ""
	end
	JSON = string.gsub(JSON,"[\n|\r|\t|\f|\b]","")

	_JSON = ""
	in_str = false
	c = 0
	ch = ""
	l_char = "\0"

	for c = 0, len do
		ch = string.sub(JSON,c,c)
		if ( (not in_str) and (ch==" ") ) then
			goto JSON_Uglify_continue
		end
		if( (ch=="\"") and (l_char~="\\") ) then
			in_str = (not in_str)
		end
		l_char = ch
		_JSON = _JSON .. ch
		::JSON_Uglify_continue::
	end
	return _JSON
end

function JSON_Beautify(JSON,gap)
	--fork of http://pastebin.com/xB0fG9py
	JSON = JSON_Uglify(JSON)

	indent = ""

	if ( type(gap) == "number") then
		i=0
		while (i < gap) do
			indent = indent .. " "
			i = i + 1
		end
	else
		if (string.len(gap)==0) then
			gap = "\t"
		end
		indent = gap
	end

	_JSON = ""
	in_str = false
	k = 0
	c = 0
	x = 0
	_s = ""
	ch = "\0"
	l_char = "\0"
	len = string.len(JSON)

	nl = "\n"

	for c = 0, len do
		ch = string.sub(JSON,c,c)
		if (not in_str) then
			if ( (ch=="{") or (ch=="[") ) then

				_s = ""
				k = k + 1
				for x = 1, k do
					_s = _s .. indent
				end

				_JSON = _JSON .. ch .. nl .. _s
				goto JSON_Beautify_continue
			elseif ( (ch=="}") or (ch=="]") ) then

				_s = ""
				k = k - 1
				for x = 1, k do
					_s = _s .. indent
				end

				_JSON = _JSON .. nl .. _s .. ch
				goto JSON_Beautify_continue
			elseif ( (ch==",") ) then

				_s = ""
				for x = 1, k do
					_s = _s .. indent
				end

				_JSON = _JSON .. ch .. nl .. _s
				goto JSON_Beautify_continue
			end
		end
		if( (ch=="\"") and (l_char~="\\") ) then
			in_str = (not in_str)
		end
		l_char = ch
		_JSON = _JSON .. ch
		::JSON_Beautify_continue::
	end
	return _JSON
end


--[[ Example

function readAll(file)
    local f = io.open(file, "rb")
    local content = f:read("*all")
    f:close()
    return content
end

data = readAll("example.json")

print(JSON_Beautify(data,4))

--]]
