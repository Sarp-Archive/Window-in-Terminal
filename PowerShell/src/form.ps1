$lastb = 0
$wintitle = "Application"

function Draw-Window([string]$title = "ConsoleWindow"){

    Clear-Host

    $conW = $Host.UI.RawUI.BufferSize.Width
    $conH = $Host.UI.RawUI.BufferSize.Height

    $esc = [char]27
    $pos_title = "$esc[0;3H"
    
    $pos_tr = "$esc[0;${conW}H"

    # TOP

    Write-Host -NoNewline "╔"
    for ($i = 0; $i -lt $conW - 2; $i++)
    {
        Write-Host -NoNewline "═"
    }
    Write-Host "${pos_tr}╗"


    # SIDE
    for ($i = 0; $i -lt $conH - 2; $i++)
    {
        Write-Host -NoNewline "║"

        $curY = $Host.UI.RawUI.CursorPosition.Y
        $r1 = $conW
        $r2 = $curY + 1
        $pos_r = "${esc}[${r2};${r1}H"
        Write-Host "${pos_r}║"
    }


    # BOTTOM
    Write-Host -NoNewline "╚"
    for ($i = 0; $i -lt $conW - 2; $i++)
    {
        Write-Host -NoNewline "═"
    }
    Write-Host -NoNewline "╝"


    # WINDOW CONTROLS
    $tl_x = $conW - 18
    $esc = [char]27

    $pos_cbx = "$esc[3;1H"
    Write-Host -NoNewline "${pos_cbx}╠"
    for ($i = 0; $i -lt $conW - 20; $i++)
    {
        Write-Host -NoNewline "═"
    }

    $pos_cont = "$esc[1;${tl_x}H"
    Write-Host -NoNewline "${pos_cont}╦═════╦═════╦═════╗"

    $pos_cont = "$esc[2;${tl_x}H"
    Write-Host -NoNewline "${pos_cont}║  ⎯  ║  □  ║  ⨯  ║"

    $pos_cont = "$esc[3;${tl_x}H"
    Write-Host -NoNewline "${pos_cont}╩═════╩═════╩═════╣"
    
    $pos_title = "$esc[2;2H"
    Write-Host -NoNewline "${pos_title} ${title}"
}

function Draw-Loading(){
    $esc = [char]27
    $pos_load = "$esc[3;3H"

    [int]$delay = 100
    $loadlist = "⠁⠂⠄⡀⢀⠠⠐⠈"

    foreach ($c in $loadlist.ToCharArray()) {
        Write-Host -NoNewline "${pos_load}${c} Loading, please wait."
        Start-Sleep -Milliseconds $delay
    }
}

function Start-ResizeHandler(){
    $curb = $Host.UI.RawUI.BufferSize
    if($lastb -ne $curb) {
        $lastb = $curb
        Draw-Window("App")
    }
}

function Draw-MinWindow([string]$title = "ConsoleWindow"){
    Clear-Host

    $conH = $Host.UI.RawUI.BufferSize.Height

    $esc = [char]27
    $pos_cont = "$esc[${tl_y};${tl_x}H"

    $tl_x = 1
    $tl_y = $conH - 2

    Write-Host -NoNewline "${pos_cont}╔══"
    foreach ($c in $title.ToCharArray()) {
        Write-Host -NoNewline "═"
    }
    Write-Host "══╦═════╦═════╦═════╗"

    Write-Host "║  ${title}  ║  ⎯  ║  □  ║  ⨯  ║"

    Write-Host -NoNewline "╚══"
    foreach ($c in $title.ToCharArray()) {
        Write-Host -NoNewline "═"
    }
    Write-Host -NoNewline "══╩═════╩═════╩═════╝"
}

[console]::CursorVisible = $false
[Console]::Title = $wintitle

while (1 -eq 1){

    $curb = $Host.UI.RawUI.BufferSize
    if($lastb -ne $curb) {
        $lastb = $curb
        Draw-Window($wintitle)
    }

    [String]$cboard = Get-Clipboard
    if ($cboard -eq '⨯') {
        Set-Clipboard -Value " "
        Clear-Host
        exit
    }
    elseif ($cboard -eq '□') {
        Set-Clipboard -Value " "
        Draw-Window($wintitle)
    }
    elseif ($cboard -eq '⎯') {
        Set-Clipboard -Value " "
        Draw-MinWindow($wintitle)
    }
}

#╦═════╦═════╦═════╗
#║  ⎯  ║  □  ║  X  ║
#╩═════╩═════╩═════╣
