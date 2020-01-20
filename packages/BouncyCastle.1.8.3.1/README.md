Board = script.Parent
local bf
local inair
local airray
Model = Board.Parent

MaxSpeed = 40

Players = game:GetService("Players")
ContentProvider = game:GetService("ContentProvider")
UserInputService = game:GetService("UserInputService")
RunService = game:GetService("RunService")

ActiveAnimations = {}

Remotes = Board:WaitForChild("Remotes")
ServerControl = Remotes:WaitForChild("Server")
ClientControl = Remotes:WaitForChild("Client")

Anims = Board:WaitForChild("Animations")
R6Anims = Anims:WaitForChild("R6")	
R15Anims = Anims:WaitForChild("R15")

local Character
local plr = game.Players:WaitForChild(script.Parent.Parent.Parent.Name)

Animations = {
	R6 = {
		BoardKick = R6Anims:WaitForChild("BoardKick"),
		CoastingPose = R6Anims:WaitForChild("CoastingPose"),
		LeftTurn = R6Anims:WaitForChild("LeftTurn"),
		RightTurn = R6Anims:WaitForChild("RightTurn"),
		Ollie = R6Anims:WaitForChild("Ollie")
	},
	R15 = {
		BoardKick = R15Anims:WaitForChild("BoardKick"),
		CoastingPose = R15Anims:WaitForChild("CoastingPose"),
		LeftTurn = R15Anims:WaitForChild("LeftTurn"),
		RightTurn = R15Anims:WaitForChild("RightTurn"),
		Ollie = R15Anims:WaitForChild("Ollie")
	}
}

Sounds = {
	BoardDrop = Board:WaitForChild("BoardDrop"),
	BoardOllie = Board:WaitForChild("BoardOllie"),
	BoardStop = Board:WaitForChild("BoardStop"),
	CruiseLoop = Board:WaitForChild("CruiseLoop")
}

LastAction = nil
LastTime = nil
JumpEnabled = true

SkateboardEquipped = true

--ClientControl.OnClientInvoke = (function(player, Mode, Value)
--end)

--texture id change
plr.Chatted:Connect(function(msg)
	if string.match(msg,"/texid ") then
		script.Parent.Remotes.Texture:FireServer("http://www.roblox.com/asset/?id="..string.sub(msg,7))
	end
end)
--tricks

local trickdb = false
local flipdb = false
local grab = false

game:GetService("UserInputService").InputBegan:Connect(function(k,gp)
	if gp == false then
		if k.KeyCode == Enum.KeyCode.R then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("kickflip")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.T then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("heelflip")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.Y then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("heelunder")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.U then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("kickunder")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.J then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("laserflip")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.F then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("shuvit")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.G then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("doubleshuvit")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.C then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("impossible")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.(E) then--COPY
			--if inair and not trickdb then--
				script.Parent.Remotes.Trick:FireServer("spinright")--SPIN RIGHT
				--trickdb = true--
				--wait(0)--
				--trickdb = false--
			--end--
-			
		elseif k.KeyCode == Enum.KeyCode.E then
			--if inair and not trickdb then--
				script.Parent.Remotes.Trick:FireServer("spinright")
				--trickdb = true--
				--wait(0)--
				--trickdb = false--
			--end--
			
		elseif k.KeyCode == Enum.KeyCode.Q then
			--if inair and not trickdb then--
				script.Parent.Remotes.Trick:FireServer("spinleft")
				--trickdb = true--
				--wait(0)--
				--trickdb = false--
			--end--
-				
		elseif k.KeyCode == Enum.KeyCode.(Q) then--COPY
			--if inair and not trickdb then--
				script.Parent.Remotes.Trick:FireServer("spinleft")--SPIN LEFT
				--trickdb = true--
				--wait(0)--
				--trickdb = false--
			--end--
		elseif k.KeyCode == Enum.KeyCode.N then--DONT CARE
			--if inair and not trickdb then
			if inair then
				script.Parent.Remotes.Trick:FireServer("backflip")
				--trickdb = true
				--wait(0)
				--trickdb = false
			--end
			end
		elseif k.KeyCode == Enum.KeyCode.M then
			--if inair and not trickdb then
			if inair then
				script.Parent.Remotes.Trick:FireServer("frontflip")
				--trickdb = true
				--wait(0)
				--trickdb = false
			--end
			end
		elseif k.KeyCode == Enum.KeyCode.Z then --STANDUP
			if not flipdb then
				flipdb = true
				Gyro.MaxTorque = Vector3.new(9e9,0,9e9)
				wait(2)--MAYBE SHOULD B 0
				Gyro.MaxTorque = Vector3.new(0,0,0)
				flipdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.K then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("hardflip")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.RightBracket then
			if not trickdb then
				script.Parent.Remotes.Trick:FireServer("leanback")--LEAN BACK
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.LeftBracket then
			if not trickdb then
				script.Parent.Remotes.Trick:FireServer("leanfor")--LEAN FOWARD
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.H then
			if inair and not trickdb and not grab then
				script.Parent.Remotes.Trick:FireServer("360flip")
				trickdb = true
				wait(0)
				trickdb = false
			end
		elseif k.KeyCode == Enum.KeyCode.One then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("bs",true)
				grab = true
			end		
		elseif k.KeyCode == Enum.KeyCode.Two then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("fs",true)
				grab = true
			end	
		elseif k.KeyCode == Enum.KeyCode.Three then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("crossbone",true)
				grab = true
			end
		elseif k.KeyCode == Enum.KeyCode.Four then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("japan",true)
				grab = true
			end
		elseif k.KeyCode == Enum.KeyCode.Five then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("melon",true)
				grab = true
			end
		elseif k.KeyCode == Enum.KeyCode.Six then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("method",true)
				grab = true
			end
		elseif k.KeyCode == Enum.KeyCode.Seven then
			if inair and not grab and not trickdb then
				script.Parent.Remotes.Grab:FireServer("nofoot",true)
				grab = true
			end
		elseif k.KeyCode == Enum.KeyCode.Backspace then
			script.Parent.Remotes.Dismount:FireServer()
		end
	end
end)

game:GetService("UserInputService").InputEnded:Connect(function(k,gp)
	if gp == false then
		if k.KeyCode == Enum.KeyCode.One then
			if grab then
				script.Parent.Remotes.Grab:FireServer("bs",false)
				grab = false
			end		
		elseif k.KeyCode == Enum.KeyCode.Two then
			if grab then
				script.Parent.Remotes.Grab:FireServer("fs",false)
				grab = false
			end	
		elseif k.KeyCode == Enum.KeyCode.Three then
			if grab then
				script.Parent.Remotes.Grab:FireServer("crossbone",false)
				grab = false
			end	
		elseif k.KeyCode == Enum.KeyCode.Four then
			if grab then
				script.Parent.Remotes.Grab:FireServer("japan",false)
				grab = false
			end	
		elseif k.KeyCode == Enum.KeyCode.Five then
			if grab then
				script.Parent.Remotes.Grab:FireServer("melon",false)
				grab = false
			end	
		elseif k.KeyCode == Enum.KeyCode.Six then
			if grab then
				script.Parent.Remotes.Grab:FireServer("method",false)
				grab = false
			end	
		elseif k.KeyCode == Enum.KeyCode.Seven then
			if grab then
				script.Parent.Remotes.Grab:FireServer("nofoot",false)
				grab = false
			end	
		end	
	end
end)



function InvokeServer(Mode, Value)
	local ServerReturn = nil
	pcall(function()
		ServerReturn = ServerControl:InvokeServer(Mode, Value)
	end)
	return ServerReturn
end

function GetAnimation(AnimName)
	if not Humanoid then
		return
	end
	local RigType = Humanoid.RigType
	if RigType == Enum.HumanoidRigType.R15 then
		return Animations["R15"][AnimName]
	else
		return Animations["R6"][AnimName]
	end
end

function SetAnimation(Mode, Value)
	if Mode == "Play" and Value and SkateboardEquipped and CheckIfAlive() then
		for i, v in pairs(ActiveAnimations) do
			if v.Animation == Value.Animation then
				v.AnimationTrack:Stop()
				table.remove(ActiveAnimations, i)
			end
		end
		local AnimationTrack = Humanoid:LoadAnimation(Value.Animation)
		table.insert(ActiveAnimations, {Animation = Value.Animation, AnimationTrack = AnimationTrack})
		AnimationTrack:Play(Value.FadeTime, Value.Weight, Value.Speed)
	elseif Mode == "Stop" and Value then
		for i, v in pairs(ActiveAnimations) do
			if v.Animation == Value.Animation then
				v.AnimationTrack:Stop(Value.FadeTime)
				table.remove(ActiveAnimations, i)
			end
		end
	end
end

function DidAction(Action)
	LastAction = Action
	LastTime = time()
end

function AxisChanged(Axis)
	
	Board.Steer = Controller.Steer
	Board.Throttle = Controller.Throttle
	
	if Board.Steer == -1 then --Left turn
		SetAnimation("Stop", {Animation = GetAnimation("BoardKick")})
		SetAnimation("Stop", {Animation = GetAnimation("Ollie")})
		SetAnimation("Stop", {Animation = GetAnimation("RightTurn"), FadeTime = 0.5})
		SetAnimation("Play", {Animation = GetAnimation("LeftTurn"), FadeTime = 0.5})
		DidAction("Left")
	elseif Board.Steer == 1 then --Right turn
		SetAnimation("Stop", {Animation = GetAnimation("BoardKick")})
		SetAnimation("Stop", {Animation = GetAnimation("LeftTurn"), FadeTime = 0.5})
		SetAnimation("Stop", {Animation = GetAnimation("Ollie")})
		SetAnimation("Play", {Animation = GetAnimation("RightTurn"), FadeTime = 0.5})
		DidAction("Right")
	elseif Board.Steer == 0 then --Forward/Backward
		SetAnimation("Stop", {Animation = GetAnimation("BoardKick")})
		SetAnimation("Stop", {Animation = GetAnimation("LeftTurn"), FadeTime = 0.5})
		SetAnimation("Stop", {Animation = GetAnimation("Ollie")})
		SetAnimation("Stop", {Animation = GetAnimation("RightTurn"), FadeTime = 0.5})
		if (LastAction == "Go" and time() - LastTime < 0.1) then
		end
		DidAction("Go")
		if not Sounds.CruiseLoop.IsPlaying then
			script.Parent.Remotes.Sound:FireServer("cruise1")
		end
	end
end

function EnterAirState(Boolean)
	if Boolean then
		inair = true
		if not flipdb then
			Gyro.maxTorque = Vector3.new(90, 0, 90) --ICHANGED FROM 90,0,90
		end
		JumpEnabled = false
		script.Parent.Remotes.Sound:FireServer("ollie1")
		Board.StickyWheels = false
	else
		inair = false
		if not flipdb then
			Gyro.maxTorque = Vector3.new(0, 0, 0)
		end
		JumpEnabled = true
		script.Parent.Remotes.Sound:FireServer("ollie2")
		Board.StickyWheels = true
	end
end

local kickdb = false

function EnterPushingState()
	if not kickdb then
		SetAnimation("Play", {Animation = GetAnimation("BoardKick")})
		kickdb = true
		wait(0)
		kickdb = false
	end
end

function EnterCoastingState()
	if not Sounds.CruiseLoop.IsPlaying then
		script.Parent.Remotes.Sound:FireServer("cruise1")
	end
end

function EnterStoppingState()
	script.Parent.Remotes.Sound:FireServer("cruise2")
	BreakModifyAndPlay()
end

game:GetService("RunService").RenderStepped:Connect(function()
	local hit=workspace:FindPartOnRay(Ray.new(script.Parent.Position,script.Parent.CFrame.upVector*-math.abs(0.8)),script.Parent.Parent)
	
	if not airray and OllieThrust then
		OllieThrust.Force = Vector3.new(script.Parent.Steer*900,OllieThrust.Force.Y,0)
	elseif airray and OllieThrust then		
		OllieThrust.Force = Vector3.new(script.Parent.Steer*850,OllieThrust.Force.Y,0)
	end
	
	if not hit then
		airray = true
		if bf then
			--bf.Force = Vector3.new(0,0,0)
		end
		--Gyro.maxTorque = Vector3.new(90, 0, 90)
	else
		if not flipdb then
			--Gyro.maxTorque = Vector3.new(0, 0, 0)
		end
		if bf then
			--[[if bf.Force.Y <= 100 then
				bf.Force = Vector3.new(0,30,0)*Board.Velocity.Magnitude
			end]]
			--bf.Force = Vector3.new(0,200,0)
		end
		if hit:FindFirstChild("CylinderMesh") == nil and hit.Shape ~= Enum.PartType.Cylinder and hit.Size.Y > 1 or hit.Size.Z > 1 and not hit:IsA("WedgePart") then
			airray = false
		end
		--Gyro.maxTorque = Vector3.new(0, 0, 0)
	end
end)

function MoveStateChanged(NewState, OldState)
	if not JumpEnabled then
		--if not airray then
			EnterAirState(false)
		--end
	end
	if NewState == Enum.MoveState.AirFree then
		if airray then
		--if #script.Parent.Parent["Left Rear"]:GetTouchingParts() < 1 or #script.Parent.Parent["Right Rear"]:GetTouchingParts() < 1  then 
			EnterAirState(true)
		end
	elseif NewState == Enum.MoveState.Pushing then
		EnterPushingState()
	elseif NewState == Enum.MoveState.Coasting then
		EnterCoastingState()
	elseif NewState == Enum.MoveState.Stopping then
		EnterStoppingState()
	end
end

function MakeOllieThrust()
	if not CheckIfAlive() then
		return
	end
	OllieThrust = Instance.new("BodyThrust")
	OllieThrust.Name = "OllieThrust"
	OllieThrust.force = Vector3.new(0, 0, 0)
	OllieThrust.location = Vector3.new(0, 0, -30) --?
	OllieThrust.Parent = Board
end

function MakeGyro()
	if not CheckIfAlive() then
		return
	end
	Gyro = Instance.new("BodyGyro")
	Gyro.Name = "SkateboardGyro"
	Gyro.maxTorque = Vector3.new(0, 0, 0) --?
	Gyro.P = 200
	Gyro.D = 200
	Gyro.Parent = Torso
end

function ButtonChanged(Button)
	local ButtonState = Controller:GetButton(Button)
	if not ButtonState then
		return
	end
	if Button == Enum.Button.Dismount then
		Board.ControllingHumanoid.Jump = ButtonState
	elseif Button == Enum.Button.Jump then
		if JumpEnabled --[[and math.abs(Board.Velocity.Y) < 5]] then
			JumpEnabled = false
			SetAnimation("Stop", {Animation = GetAnimation("BoardKick")})
			SetAnimation("Stop", {Animation = GetAnimation("LeftTurn"), FadeTime = 0.5})
			SetAnimation("Stop", {Animation = GetAnimation("RightTurn"), FadeTime = 0.5})
			SetAnimation("Play", {Animation = GetAnimation("Ollie"), FadeTime = 0, Weight = 1, Speed = 4})
			Board.StickyWheels = false
			Board:ApplySpecificImpulse(Vector3.new(0, 45, 0))		--?
			OllieThrust.force = Vector3.new(0, 45, 0)	--?
			DidAction("Jump")
			delay(0, function()
				if OllieThrust then
					OllieThrust.force = Vector3.new(0, 0, 0)		--?
				end
			end)
		else
			Board.StickyWheels = false
		end
	end
end

function CruiseModify()
	local Velocity = Board.Velocity.magnitude
	local LowPitch = 1
	local TopPitch = 1
	local LowV = 1
	local TopV = 1
	if Velocity < 1 then
		Sounds.CruiseLoop.Volume = 1
		return
	end
	Sounds.CruiseLoop.Volume = math.min((((TopV - LowV) * (Velocity / MaxSpeed)) + LowV), 1)
	Sounds.CruiseLoop.Pitch = (((TopPitch - LowPitch) * (Velocity / MaxSpeed)) + LowPitch)
end

function BreakModifyAndPlay()
	local v = Board.Velocity.magnitude
	local Slowest = 1
	local LowV = 1
	local TopV = 1
	if (v < Slowest) then
		return
	end
	Sounds.BoardStop.Volume = math.min((((TopV - LowV) * (v / (MaxSpeed - Slowest))) + LowV), 1)
	script.Parent.Remotes.Sound:FireServer("stop1")
end

function CheckIfAlive()
	return (((Player and Player.Parent and Humanoid and Humanoid.Parent and Humanoid.Health > 0) and true) or false)
end


function Equipped(humanoid, controller)
	trickdb = false
	grab = false
	Controller = controller
	Humanoid = humanoid
	Character = Humanoid.Parent
	Player = Players:GetPlayerFromCharacter(Character)
	PlayerGui = Player:FindFirstChild("PlayerGui")
	Torso = Character:FindFirstChild("HumanoidRootPart")
	
	script.Parent.Parent.Parent = Character
	if not CheckIfAlive() then
		return
	end
	for i, v in pairs({KeyDownConnection, KeyUpConnection}) do
		if v then
			v:disconnect()
		end
	end
	for i, v in pairs({Animations.R6, Animations.R15, Sounds}) do
		for ii, vv in pairs(v) do
			if vv:IsA("Animation") then
				ContentProvider:Preload(vv.AnimationId)
			elseif vv:IsA("Sound") then
				ContentProvider:Preload(vv.SoundId)
			end
		end
	end
	KeyDownConnection = UserInputService.InputBegan:connect(function(InputObject)
		if InputObject.UserInputState == Enum.UserInputState.Begin then
			InvokeServer("KeyPress", {Down = true, Key = InputObject.KeyCode})
		end
	end)
	KeyUpConnection = UserInputService.InputEnded:connect(function(InputObject)
		if InputObject.UserInputState == Enum.UserInputState.End then
			InvokeServer("KeyPress", {Down = false, Key = InputObject.KeyCode})
		end
	end)
	for i, v in  pairs(Board:GetChildren()) do
		if v:IsA("BodyThrust") or v:IsA("BodyGyro") then
			v:Destroy()
		end
	end
	SkateboardEquipped = true
	bf = Instance.new("BodyForce",Board)
	bf.Force = Vector3.new(0,400,0)--?
	MakeOllieThrust()--?
	MakeGyro()--?
	Controller.AxisChanged:connect(AxisChanged)
	Board.MoveStateChanged:connect(MoveStateChanged)
	Controller:BindButton(Enum.Button.Jump, "")
	Controller.ButtonChanged:connect(ButtonChanged)
	SetAnimation("Play", {Animation = GetAnimation("CoastingPose")})
	InvokeServer("Equipped", Character)
	
end

function Unequipped()
	Controller = nil
	Board.Velocity = Vector3.new(0, 0, 0)
	Board.RotVelocity = Vector3.new(0, 0, 0)
	bf.Force = Vector3.new(0,0,0)
	bf:Destroy()
	bf = nil
	for i, v in pairs({Gyro, OllieThrust}) do
		if v and v.Parent then
			v:Destroy()
		end
	end
	for i, v in pairs({KeyDownConnection, KeyUpConnection}) do
		if v then
			v:disconnect()
		end
	end
	for i, v in pairs({Animations.R6, Animations.R15}) do
		for ii, vv in pairs(v) do
			SetAnimation("Stop", {Animation = vv})
		end
	end
	for i, v in pairs(Sounds) do
		v:Stop()
	end
	ActiveAnimations = {}
	SkateboardEquipped = false
	InvokeServer("Unequipped")
end

Board.Equipped:connect(Equipped)
Board.Unequipped:connect(Unequipped)

--[[RunService.Heartbeat:connect(function()
	CruiseModify()
end)]]