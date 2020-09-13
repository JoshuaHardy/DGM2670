/*
I guess this is LUA code not C#.
Though I was really surprised how much I could read this code that was by and large written by another user. 
And I adjusted it within TableTop Simulator To Make a Unity game object behave differently.

 
rouletteWheelGUID = 'e6ecd4'
rouletteWheel = nil

function onload ()
rouletteWheel = getObjectFromGUID(rouletteWheelGUID)
end

spinning = false
startTime = 0
spinTime = 3000

function onCollisionEnter (collision_info)
if spinning == false then
startTime = os.clock()
startLuaCoroutine(self, 'SpinCoroutine')
end
end

function SpinCoroutine ()
spinning = true
timeDiff = os.clock() - startTime

prevClock = os.clock()

while timeDiff < spinTime do

timeDelta = os.clock() - prevClock --make spinning frame independent by getting delta
prevClock = os.clock()

spinSpeed = (spinTime - timeDiff) * timeDelta * 60

currentRot = rouletteWheel.getRotation()
currentRot['x'] = 0
currentRot['y'] = currentRot['y'] + spinSpeed
currentRot['z'] = 0
rouletteWheel.setRotation(currentRot)

coroutine.yield(0) --wait one frame

timeDiff = os.clock() - startTime
end

spinning = false
--[[ Always return 1 at the end of a coroutine. --]]
return 1
end

*/