using Newtonsoft.Json;

namespace Bot.Utils.Constants;

public class Emojis
{
    [JsonProperty("static")] public Static Static = new();

    [JsonProperty("animated")] public Animated Animated = new();
}

public class Animated
{
    [JsonProperty("boost")] public string Boost = string.Empty;
}

public class Static
{
    [JsonProperty("add")] public string Add = string.Empty;

    [JsonProperty("hammer")] public string Hammer = string.Empty;

    [JsonProperty("minus")] public string Minus = string.Empty;

    [JsonProperty("action_check")] public string ActionCheck = string.Empty;

    [JsonProperty("action_help")] public string ActionHelp = string.Empty;

    [JsonProperty("action_info")] public string ActionInfo = string.Empty;

    [JsonProperty("action_warning")] public string ActionWarning = string.Empty;

    [JsonProperty("action_x")] public string ActionX = string.Empty;

    [JsonProperty("check")] public string Check = string.Empty;

    [JsonProperty("close")] public string Close = string.Empty;

    [JsonProperty("arrow_down")] public string ArrowDown = string.Empty;

    [JsonProperty("arrow_left")] public string ArrowLeft = string.Empty;

    [JsonProperty("arrow_refresh")] public string ArrowRefresh = string.Empty;

    [JsonProperty("arrow_right")] public string ArrowRight = string.Empty;

    [JsonProperty("arrow_up")] public string ArrowUp = string.Empty;

    [JsonProperty("back")] public string Back = string.Empty;

    [JsonProperty("next")] public string Next = string.Empty;

    [JsonProperty("instagram")] public string Instagram = string.Empty;

    [JsonProperty("linkedin")] public string Linkedin = string.Empty;

    [JsonProperty("youtube")] public string Youtube = string.Empty;

    [JsonProperty("bell_dot")] public string BellDot = string.Empty;

    [JsonProperty("bell_minus")] public string BellMinus = string.Empty;

    [JsonProperty("bell_off")] public string BellOff = string.Empty;

    [JsonProperty("bell_plus")] public string BellPlus = string.Empty;

    [JsonProperty("bell")] public string Bell = string.Empty;

    [JsonProperty("book_check")] public string BookCheck = string.Empty;

    [JsonProperty("book_minus")] public string BookMinus = string.Empty;

    [JsonProperty("book_plus")] public string BookPlus = string.Empty;

    [JsonProperty("book_x")] public string BookX = string.Empty;

    [JsonProperty("book")] public string Book = string.Empty;

    [JsonProperty("calendar_cog")] public string CalendarCog = string.Empty;

    [JsonProperty("calendar_days")] public string CalendarDays = string.Empty;

    [JsonProperty("calendar_minus")] public string CalendarMinus = string.Empty;

    [JsonProperty("calendar_plus")] public string CalendarPlus = string.Empty;

    [JsonProperty("camera_off")] public string CameraOff = string.Empty;

    [JsonProperty("camera")] public string Camera = string.Empty;

    [JsonProperty("clipboard_check")] public string ClipboardCheck = string.Empty;

    [JsonProperty("clipboard_minus")] public string ClipboardMinus = string.Empty;

    [JsonProperty("clipboard_plus")] public string ClipboardPlus = string.Empty;

    [JsonProperty("clipboard_x")] public string ClipboardX = string.Empty;

    [JsonProperty("clipboard")] public string Clipboard = string.Empty;

    [JsonProperty("clock_check")] public string ClockCheck = string.Empty;

    [JsonProperty("clock_minus")] public string ClockMinus = string.Empty;

    [JsonProperty("clock_off")] public string ClockOff = string.Empty;

    [JsonProperty("clock_plus")] public string ClockPlus = string.Empty;

    [JsonProperty("clock")] public string Clock = string.Empty;

    [JsonProperty("cloud_cog")] public string CloudCog = string.Empty;

    [JsonProperty("cloud_download")] public string CloudDownload = string.Empty;

    [JsonProperty("cloud_upload")] public string CloudUpload = string.Empty;

    [JsonProperty("cloud")] public string Cloud = string.Empty;

    [JsonProperty("code_braces")] public string CodeBraces = string.Empty;

    [JsonProperty("code_brackets")] public string CodeBrackets = string.Empty;

    [JsonProperty("code_bug")] public string CodeBug = string.Empty;

    [JsonProperty("code_file_binary")] public string CodeFileBinary = string.Empty;

    [JsonProperty("code_parentheses")] public string CodeParentheses = string.Empty;

    [JsonProperty("code_server_cog")] public string CodeServerCog = string.Empty;

    [JsonProperty("code_server_off")] public string CodeServerOff = string.Empty;

    [JsonProperty("code_server")] public string CodeServer = string.Empty;

    [JsonProperty("code_terminal")] public string CodeTerminal = string.Empty;

    [JsonProperty("code_window")] public string CodeWindow = string.Empty;

    [JsonProperty("code_wrench")] public string CodeWrench = string.Empty;

    [JsonProperty("database_backup")] public string DatabaseBackup = string.Empty;

    [JsonProperty("database")] public string Database = string.Empty;

    [JsonProperty("icons_bookmark")] public string IconsBookmark = string.Empty;

    [JsonProperty("icons_busy")] public string IconsBusy = string.Empty;

    [JsonProperty("icons_camera")] public string IconsCamera = string.Empty;

    [JsonProperty("icons_clouddown")] public string IconsClouddown = string.Empty;

    [JsonProperty("icons_code")] public string IconsCode = string.Empty;

    [JsonProperty("icons_control")] public string IconsControl = string.Empty;

    [JsonProperty("icons_downarrow")] public string IconsDownarrow = string.Empty;

    [JsonProperty("icons_education")] public string IconsEducation = string.Empty;

    [JsonProperty("icons_flag")] public string IconsFlag = string.Empty;

    [JsonProperty("icons_folder")] public string IconsFolder = string.Empty;

    [JsonProperty("icons_fword")] public string IconsFword = string.Empty;

    [JsonProperty("icons_games")] public string IconsGames = string.Empty;

    [JsonProperty("icons_gif")] public string IconsGif = string.Empty;

    [JsonProperty("icons_gift")] public string IconsGift = string.Empty;

    [JsonProperty("icons_heart")] public string IconsHeart = string.Empty;

    [JsonProperty("icons_hi")] public string IconsHi = string.Empty;

    [JsonProperty("icons_id")] public string IconsId = string.Empty;

    [JsonProperty("icons_idle")] public string IconsIdle = string.Empty;

    [JsonProperty("icons_image")] public string IconsImage = string.Empty;

    [JsonProperty("icons_leftarrow")] public string IconsLeftarrow = string.Empty;

    [JsonProperty("icons_list")] public string IconsList = string.Empty;

    [JsonProperty("icons_loadingerror")] public string IconsLoadingerror = string.Empty;

    [JsonProperty("icons_message")] public string IconsMessage = string.Empty;

    [JsonProperty("icons_music")] public string IconsMusic = string.Empty;

    [JsonProperty("icons_notify")] public string IconsNotify = string.Empty;

    [JsonProperty("icons_off")] public string IconsOff = string.Empty;

    [JsonProperty("icons_offline")] public string IconsOffline = string.Empty;

    [JsonProperty("icons_on")] public string IconsOn = string.Empty;

    [JsonProperty("icons_online")] public string IconsOnline = string.Empty;

    [JsonProperty("icons_outage")] public string IconsOutage = string.Empty;

    [JsonProperty("icons_premium")] public string IconsPremium = string.Empty;

    [JsonProperty("icons_question")] public string IconsQuestion = string.Empty;

    [JsonProperty("icons_quotes")] public string IconsQuotes = string.Empty;

    [JsonProperty("icons_richpresence")] public string IconsRichpresence = string.Empty;

    [JsonProperty("icons_rules")] public string IconsRules = string.Empty;

    [JsonProperty("icons_slashcmd")] public string IconsSlashcmd = string.Empty;

    [JsonProperty("icons_spark")] public string IconsSpark = string.Empty;

    [JsonProperty("icons_speaker")] public string IconsSpeaker = string.Empty;

    [JsonProperty("icons_speakerlock")] public string IconsSpeakerlock = string.Empty;

    [JsonProperty("icons_speakerlow")] public string IconsSpeakerlow = string.Empty;

    [JsonProperty("icons_speakermute")] public string IconsSpeakermute = string.Empty;

    [JsonProperty("icons_stickers")] public string IconsStickers = string.Empty;

    [JsonProperty("icons_stream")] public string IconsStream = string.Empty;

    [JsonProperty("icons_ticket")] public string IconsTicket = string.Empty;

    [JsonProperty("icons_tilde")] public string IconsTilde = string.Empty;

    [JsonProperty("icons_todolist")] public string IconsTodolist = string.Empty;

    [JsonProperty("icons_uparrow")] public string IconsUparrow = string.Empty;

    [JsonProperty("icons_update")] public string IconsUpdate = string.Empty;

    [JsonProperty("icons_view")] public string IconsView = string.Empty;

    [JsonProperty("icons_vip")] public string IconsVip = string.Empty;

    [JsonProperty("device_laptop")] public string DeviceLaptop = string.Empty;

    [JsonProperty("device_pc")] public string DevicePc = string.Empty;

    [JsonProperty("device_smartphone")] public string DeviceSmartphone = string.Empty;

    [JsonProperty("device_tablet")] public string DeviceTablet = string.Empty;

    [JsonProperty("icons_1")] public string Icons1 = string.Empty;

    [JsonProperty("icons_addreactions")] public string IconsAddreactions = string.Empty;

    [JsonProperty("icons_aka")] public string IconsAka = string.Empty;

    [JsonProperty("icons_behance")] public string IconsBehance = string.Empty;

    [JsonProperty("icons_beta")] public string IconsBeta = string.Empty;

    [JsonProperty("icons_bots")] public string IconsBots = string.Empty;

    [JsonProperty("icons_clean")] public string IconsClean = string.Empty;

    [JsonProperty("icons_defaultperms")] public string IconsDefaultperms = string.Empty;

    [JsonProperty("icons_discordbotdev")] public string IconsDiscordbotdev = string.Empty;

    [JsonProperty("icons_discordmod")] public string IconsDiscordmod = string.Empty;

    [JsonProperty("icons_discordnitro")] public string IconsDiscordnitro = string.Empty;

    [JsonProperty("icons_discordpartner")] public string IconsDiscordpartner = string.Empty;

    [JsonProperty("icons_discordstaff")] public string IconsDiscordstaff = string.Empty;

    [JsonProperty("icons_dislike")] public string IconsDislike = string.Empty;

    [JsonProperty("icons_earlysupporter")] public string IconsEarlysupporter = string.Empty;

    [JsonProperty("icons_fb")] public string IconsFb = string.Empty;

    [JsonProperty("icons_figma")] public string IconsFigma = string.Empty;

    [JsonProperty("icons_files")] public string IconsFiles = string.Empty;

    [JsonProperty("icons_friends")] public string IconsFriends = string.Empty;

    [JsonProperty("icons_github")] public string IconsGithub = string.Empty;

    [JsonProperty("icons_hoursglass")] public string IconsHoursglass = string.Empty;

    [JsonProperty("icons_hsbalance")] public string IconsHsbalance = string.Empty;

    [JsonProperty("icons_hsbravery")] public string IconsHsbravery = string.Empty;

    [JsonProperty("icons_hsbrilliance")] public string IconsHsbrilliance = string.Empty;

    [JsonProperty("icons_instagram")] public string IconsInstagram = string.Empty;

    [JsonProperty("icons_kicking")] public string IconsKicking = string.Empty;

    [JsonProperty("icons_kofi")] public string IconsKofi = string.Empty;

    [JsonProperty("icons_like")] public string IconsLike = string.Empty;

    [JsonProperty("icons_locked")] public string IconsLocked = string.Empty;

    [JsonProperty("icons_loop")] public string IconsLoop = string.Empty;

    [JsonProperty("icons_menu")] public string IconsMenu = string.Empty;

    [JsonProperty("icons_msvisualcode")] public string IconsMsvisualcode = string.Empty;

    [JsonProperty("icons_musicstop")] public string IconsMusicstop = string.Empty;

    [JsonProperty("icons_new")] public string IconsNew = string.Empty;

    [JsonProperty("icons_partner")] public string IconsPartner = string.Empty;

    [JsonProperty("icons_patreon")] public string IconsPatreon = string.Empty;

    [JsonProperty("icons_pause")] public string IconsPause = string.Empty;

    [JsonProperty("icons_pings")] public string IconsPings = string.Empty;

    [JsonProperty("icons_play")] public string IconsPlay = string.Empty;

    [JsonProperty("icons_queue")] public string IconsQueue = string.Empty;

    [JsonProperty("icons_reddit")] public string IconsReddit = string.Empty;

    [JsonProperty("icons_serverpartner")] public string IconsServerpartner = string.Empty;

    [JsonProperty("icons_serververified")] public string IconsSerververified = string.Empty;

    [JsonProperty("icons_snapchat")] public string IconsSnapchat = string.Empty;

    [JsonProperty("icons_supportteam")] public string IconsSupportteam = string.Empty;

    [JsonProperty("icons_twitter")] public string IconsTwitter = string.Empty;

    [JsonProperty("icons_unlock")] public string IconsUnlock = string.Empty;

    [JsonProperty("icons_youtube")] public string IconsYoutube = string.Empty;

    [JsonProperty("icons_banmembers")] public string IconsBanmembers = string.Empty;

    [JsonProperty("icons_channelfollowed")]
    public string IconsChannelfollowed = string.Empty;

    [JsonProperty("icons_createcategory")] public string IconsCreatecategory = string.Empty;

    [JsonProperty("icons_createchannel")] public string IconsCreatechannel = string.Empty;

    [JsonProperty("icons_createchannels")] public string IconsCreatechannels = string.Empty;

    [JsonProperty("icons_createemoji")] public string IconsCreateemoji = string.Empty;

    [JsonProperty("icons_createintegration")]
    public string IconsCreateintegration = string.Empty;

    [JsonProperty("icons_createrole")] public string IconsCreaterole = string.Empty;

    [JsonProperty("icons_createsticker")] public string IconsCreatesticker = string.Empty;

    [JsonProperty("icons_createthread")] public string IconsCreatethread = string.Empty;

    [JsonProperty("icons_createwebhook")] public string IconsCreatewebhook = string.Empty;

    [JsonProperty("icons_deletechannel")] public string IconsDeletechannel = string.Empty;

    [JsonProperty("icons_deleteemoji")] public string IconsDeleteemoji = string.Empty;

    [JsonProperty("icons_deleteevent")] public string IconsDeleteevent = string.Empty;

    [JsonProperty("icons_deleteintegration")]
    public string IconsDeleteintegration = string.Empty;

    [JsonProperty("icons_deleterole")] public string IconsDeleterole = string.Empty;

    [JsonProperty("icons_deletesticker")] public string IconsDeletesticker = string.Empty;

    [JsonProperty("icons_deletethread")] public string IconsDeletethread = string.Empty;

    [JsonProperty("icons_deletewebhook")] public string IconsDeletewebhook = string.Empty;

    [JsonProperty("icons_disable")] public string IconsDisable = string.Empty;

    [JsonProperty("icons_discord")] public string IconsDiscord = string.Empty;

    [JsonProperty("icons_enable")] public string IconsEnable = string.Empty;

    [JsonProperty("icons_endstage")] public string IconsEndstage = string.Empty;

    [JsonProperty("icons_envelope")] public string IconsEnvelope = string.Empty;

    [JsonProperty("icons_generalinfo")] public string IconsGeneralinfo = string.Empty;

    [JsonProperty("icons_growth")] public string IconsGrowth = string.Empty;

    [JsonProperty("icons_linkadd")] public string IconsLinkadd = string.Empty;

    [JsonProperty("icons_linkrevoke")] public string IconsLinkrevoke = string.Empty;

    [JsonProperty("icons_linkupdate")] public string IconsLinkupdate = string.Empty;

    [JsonProperty("icons_notificationsettings")]
    public string IconsNotificationsettings = string.Empty;

    [JsonProperty("icons_oauth2")] public string IconsOauth2 = string.Empty;

    [JsonProperty("icons_roles")] public string IconsRoles = string.Empty;

    [JsonProperty("icons_scheduleevent")] public string IconsScheduleevent = string.Empty;

    [JsonProperty("icons_serverinsight")] public string IconsServerinsight = string.Empty;

    [JsonProperty("icons_startstage")] public string IconsStartstage = string.Empty;

    [JsonProperty("icons_swardx")] public string IconsSwardx = string.Empty;

    [JsonProperty("icons_threadchannel")] public string IconsThreadchannel = string.Empty;

    [JsonProperty("icons_unbanmember")] public string IconsUnbanmember = string.Empty;

    [JsonProperty("icons_updatechannel")] public string IconsUpdatechannel = string.Empty;

    [JsonProperty("icons_updateemoji")] public string IconsUpdateemoji = string.Empty;

    [JsonProperty("icons_updateevent")] public string IconsUpdateevent = string.Empty;

    [JsonProperty("icons_updateintegration")]
    public string IconsUpdateintegration = string.Empty;

    [JsonProperty("icons_updatemember")] public string IconsUpdatemember = string.Empty;

    [JsonProperty("icons_updaterole")] public string IconsUpdaterole = string.Empty;

    [JsonProperty("icons_updateserver")] public string IconsUpdateserver = string.Empty;

    [JsonProperty("icons_updatestage")] public string IconsUpdatestage = string.Empty;

    [JsonProperty("icons_updatesticker")] public string IconsUpdatesticker = string.Empty;

    [JsonProperty("icons_updatethread")] public string IconsUpdatethread = string.Empty;

    [JsonProperty("icons_updatewebhook")] public string IconsUpdatewebhook = string.Empty;

    [JsonProperty("icons_0")] public string Icons0 = string.Empty;

    [JsonProperty("icons_10")] public string Icons10 = string.Empty;

    [JsonProperty("icons_2")] public string Icons2 = string.Empty;

    [JsonProperty("icons_3")] public string Icons3 = string.Empty;

    [JsonProperty("icons_4")] public string Icons4 = string.Empty;

    [JsonProperty("icons_5")] public string Icons5 = string.Empty;

    [JsonProperty("icons_6")] public string Icons6 = string.Empty;

    [JsonProperty("icons_7")] public string Icons7 = string.Empty;

    [JsonProperty("icons_8")] public string Icons8 = string.Empty;

    [JsonProperty("icons_9")] public string Icons9 = string.Empty;

    [JsonProperty("icons_a")] public string IconsA = string.Empty;

    [JsonProperty("icons_amogus")] public string IconsAmogus = string.Empty;

    [JsonProperty("icons_b")] public string IconsB = string.Empty;

    [JsonProperty("icons_bday")] public string IconsBday = string.Empty;

    [JsonProperty("icons_book")] public string IconsBook = string.Empty;

    [JsonProperty("icons_c")] public string IconsC = string.Empty;

    [JsonProperty("icons_d")] public string IconsD = string.Empty;

    [JsonProperty("icons_e")] public string IconsE = string.Empty;

    [JsonProperty("icons_f")] public string IconsF = string.Empty;

    [JsonProperty("icons_fingerprint")] public string IconsFingerprint = string.Empty;

    [JsonProperty("icons_g")] public string IconsG = string.Empty;

    [JsonProperty("icons_guardian")] public string IconsGuardian = string.Empty;

    [JsonProperty("icons_h")] public string IconsH = string.Empty;

    [JsonProperty("icons_he_him")] public string IconsHeHim = string.Empty;

    [JsonProperty("icons_i")] public string IconsI = string.Empty;

    [JsonProperty("icons_j")] public string IconsJ = string.Empty;

    [JsonProperty("icons_k")] public string IconsK = string.Empty;

    [JsonProperty("icons_l")] public string IconsL = string.Empty;

    [JsonProperty("icons_library")] public string IconsLibrary = string.Empty;

    [JsonProperty("icons_m")] public string IconsM = string.Empty;

    [JsonProperty("icons_n")] public string IconsN = string.Empty;

    [JsonProperty("icons_o")] public string IconsO = string.Empty;

    [JsonProperty("icons_p")] public string IconsP = string.Empty;

    [JsonProperty("icons_q")] public string IconsQ = string.Empty;

    [JsonProperty("icons_r")] public string IconsR = string.Empty;

    [JsonProperty("icons_s")] public string IconsS = string.Empty;

    [JsonProperty("icons_she_her")] public string IconsSheHer = string.Empty;

    [JsonProperty("icons_statsdown")] public string IconsStatsdown = string.Empty;

    [JsonProperty("icons_t")] public string IconsT = string.Empty;

    [JsonProperty("icons_tada")] public string IconsTada = string.Empty;

    [JsonProperty("icons_they_them")] public string IconsTheyThem = string.Empty;

    [JsonProperty("icons_translate")] public string IconsTranslate = string.Empty;

    [JsonProperty("icons_u")] public string IconsU = string.Empty;

    [JsonProperty("icons_v")] public string IconsV = string.Empty;

    [JsonProperty("icons_vpn")] public string IconsVpn = string.Empty;

    [JsonProperty("icons_w")] public string IconsW = string.Empty;

    [JsonProperty("icons_x")] public string IconsX = string.Empty;

    [JsonProperty("icons_y")] public string IconsY = string.Empty;

    [JsonProperty("icons_z")] public string IconsZ = string.Empty;

    [JsonProperty("eg_addemoji")] public string EgAddemoji = string.Empty;

    [JsonProperty("eg_addfile")] public string EgAddfile = string.Empty;

    [JsonProperty("eg_addons")] public string EgAddons = string.Empty;

    [JsonProperty("eg_announcement")] public string EgAnnouncement = string.Empty;

    [JsonProperty("eg_art")] public string EgArt = string.Empty;

    [JsonProperty("eg_ask")] public string EgAsk = string.Empty;

    [JsonProperty("eg_ban")] public string EgBan = string.Empty;

    [JsonProperty("eg_book")] public string EgBook = string.Empty;

    [JsonProperty("eg_bot")] public string EgBot = string.Empty;

    [JsonProperty("eg_calender")] public string EgCalender = string.Empty;

    [JsonProperty("eg_cautions")] public string EgCautions = string.Empty;

    [JsonProperty("eg_channels")] public string EgChannels = string.Empty;

    [JsonProperty("eg_cloud")] public string EgCloud = string.Empty;

    [JsonProperty("eg_clouddownload")] public string EgClouddownload = string.Empty;

    [JsonProperty("eg_cross")] public string EgCross = string.Empty;

    [JsonProperty("eg_developers")] public string EgDevelopers = string.Empty;

    [JsonProperty("eg_discovery")] public string EgDiscovery = string.Empty;

    [JsonProperty("eg_downarrow")] public string EgDownarrow = string.Empty;

    [JsonProperty("eg_emojis")] public string EgEmojis = string.Empty;

    [JsonProperty("eg_excl")] public string EgExcl = string.Empty;

    [JsonProperty("eg_female")] public string EgFemale = string.Empty;

    [JsonProperty("eg_files")] public string EgFiles = string.Empty;

    [JsonProperty("eg_fire")] public string EgFire = string.Empty;

    [JsonProperty("eg_gift")] public string EgGift = string.Empty;

    [JsonProperty("eg_globe")] public string EgGlobe = string.Empty;

    [JsonProperty("eg_hammer")] public string EgHammer = string.Empty;

    [JsonProperty("eg_heart")] public string EgHeart = string.Empty;

    [JsonProperty("eg_home")] public string EgHome = string.Empty;

    [JsonProperty("eg_hourclock")] public string EgHourclock = string.Empty;

    [JsonProperty("eg_inbox")] public string EgInbox = string.Empty;

    [JsonProperty("eg_link")] public string EgLink = string.Empty;

    [JsonProperty("eg_lock")] public string EgLock = string.Empty;

    [JsonProperty("eg_mail")] public string EgMail = string.Empty;

    [JsonProperty("eg_male")] public string EgMale = string.Empty;

    [JsonProperty("eg_member")] public string EgMember = string.Empty;

    [JsonProperty("eg_message")] public string EgMessage = string.Empty;

    [JsonProperty("eg_modadmin")] public string EgModadmin = string.Empty;

    [JsonProperty("eg_monitor")] public string EgMonitor = string.Empty;

    [JsonProperty("eg_music")] public string EgMusic = string.Empty;

    [JsonProperty("eg_netual")] public string EgNetual = string.Empty;

    [JsonProperty("eg_notification")] public string EgNotification = string.Empty;

    [JsonProperty("eg_openpage")] public string EgOpenpage = string.Empty;

    [JsonProperty("eg_pins")] public string EgPins = string.Empty;

    [JsonProperty("eg_premium")] public string EgPremium = string.Empty;

    [JsonProperty("eg_question")] public string EgQuestion = string.Empty;

    [JsonProperty("eg_refresh")] public string EgRefresh = string.Empty;

    [JsonProperty("eg_right")] public string EgRight = string.Empty;

    [JsonProperty("eg_setting")] public string EgSetting = string.Empty;

    [JsonProperty("eg_shield")] public string EgShield = string.Empty;

    [JsonProperty("eg_star")] public string EgStar = string.Empty;

    [JsonProperty("eg_stop")] public string EgStop = string.Empty;

    [JsonProperty("eg_study")] public string EgStudy = string.Empty;

    [JsonProperty("eg_support")] public string EgSupport = string.Empty;

    [JsonProperty("eg_thumbdown")] public string EgThumbdown = string.Empty;

    [JsonProperty("eg_thumbup")] public string EgThumbup = string.Empty;

    [JsonProperty("eg_ticket")] public string EgTicket = string.Empty;

    [JsonProperty("eg_tools")] public string EgTools = string.Empty;

    [JsonProperty("eg_trans")] public string EgTrans = string.Empty;

    [JsonProperty("eg_unlock")] public string EgUnlock = string.Empty;

    [JsonProperty("eg_uparrow")] public string EgUparrow = string.Empty;

    [JsonProperty("eg_upleft")] public string EgUpleft = string.Empty;

    [JsonProperty("eg_upload")] public string EgUpload = string.Empty;

    [JsonProperty("eg_upright")] public string EgUpright = string.Empty;

    [JsonProperty("eg_video")] public string EgVideo = string.Empty;

    [JsonProperty("eg_wave")] public string EgWave = string.Empty;

    [JsonProperty("eg_wrench")] public string EgWrench = string.Empty;

    [JsonProperty("eg_wrong")] public string EgWrong = string.Empty;

    [JsonProperty("iconslogo")] public string Iconslogo = string.Empty;

    [JsonProperty("icons_activedevbadge")] public string IconsActivedevbadge = string.Empty;

    [JsonProperty("icons_activities")] public string IconsActivities = string.Empty;

    [JsonProperty("icons_adventcalendar")] public string IconsAdventcalendar = string.Empty;

    [JsonProperty("icons_announce")] public string IconsAnnounce = string.Empty;

    [JsonProperty("icons_archive")] public string IconsArchive = string.Empty;

    [JsonProperty("icons_audiodisable")] public string IconsAudiodisable = string.Empty;

    [JsonProperty("icons_audioenable")] public string IconsAudioenable = string.Empty;

    [JsonProperty("icons_award")] public string IconsAward = string.Empty;

    [JsonProperty("icons_awardcup")] public string IconsAwardcup = string.Empty;

    [JsonProperty("icons_backforward")] public string IconsBackforward = string.Empty;

    [JsonProperty("icons_badping")] public string IconsBadping = string.Empty;

    [JsonProperty("icons_ban")] public string IconsBan = string.Empty;

    [JsonProperty("icons_bank")] public string IconsBank = string.Empty;

    [JsonProperty("icons_beta1")] public string IconsBeta1 = string.Empty;

    [JsonProperty("icons_beta1a")] public string IconsBeta1a = string.Empty;

    [JsonProperty("icons_beta2")] public string IconsBeta2 = string.Empty;

    [JsonProperty("icons_beta2a")] public string IconsBeta2a = string.Empty;

    [JsonProperty("icons_birdman")] public string IconsBirdman = string.Empty;

    [JsonProperty("icons_box")] public string IconsBox = string.Empty;

    [JsonProperty("icons_bright")] public string IconsBright = string.Empty;

    [JsonProperty("icons_bugs")] public string IconsBugs = string.Empty;

    [JsonProperty("icons_bulb")] public string IconsBulb = string.Empty;

    [JsonProperty("icons_calendar1")] public string IconsCalendar1 = string.Empty;

    [JsonProperty("icons_callconnect")] public string IconsCallconnect = string.Empty;

    [JsonProperty("icons_calldecline")] public string IconsCalldecline = string.Empty;

    [JsonProperty("icons_calldisconnect")] public string IconsCalldisconnect = string.Empty;

    [JsonProperty("icons_channel")] public string IconsChannel = string.Empty;

    [JsonProperty("icons_clock")] public string IconsClock = string.Empty;

    [JsonProperty("icons_coin")] public string IconsCoin = string.Empty;

    [JsonProperty("icons_colorboostnitro")]
    public string IconsColorboostnitro = string.Empty;

    [JsonProperty("icons_colornitro")] public string IconsColornitro = string.Empty;

    [JsonProperty("icons_colorserverpartner")]
    public string IconsColorserverpartner = string.Empty;

    [JsonProperty("icons_colorserververified")]
    public string IconsColorserververified = string.Empty;

    [JsonProperty("icons_colorstaff")] public string IconsColorstaff = string.Empty;

    [JsonProperty("icons_connect")] public string IconsConnect = string.Empty;

    [JsonProperty("icons_correct")] public string IconsCorrect = string.Empty;

    [JsonProperty("icons_creditcard")] public string IconsCreditcard = string.Empty;

    [JsonProperty("icons_customstaff")] public string IconsCustomstaff = string.Empty;

    [JsonProperty("icons_dac")] public string IconsDac = string.Empty;

    [JsonProperty("icons_dblurple")] public string IconsDblurple = string.Empty;

    [JsonProperty("icons_delete")] public string IconsDelete = string.Empty;

    [JsonProperty("icons_dfuchsia")] public string IconsDfuchsia = string.Empty;

    [JsonProperty("icons_dgreen")] public string IconsDgreen = string.Empty;

    [JsonProperty("icons_discover")] public string IconsDiscover = string.Empty;

    [JsonProperty("icons_djoin")] public string IconsDjoin = string.Empty;

    [JsonProperty("icons_dleave")] public string IconsDleave = string.Empty;

    [JsonProperty("icons_dollar")] public string IconsDollar = string.Empty;

    [JsonProperty("icons_download")] public string IconsDownload = string.Empty;

    [JsonProperty("icons_downvote")] public string IconsDownvote = string.Empty;

    [JsonProperty("icons_dred")] public string IconsDred = string.Empty;

    [JsonProperty("icons_dwhite")] public string IconsDwhite = string.Empty;

    [JsonProperty("icons_dyellow")] public string IconsDyellow = string.Empty;

    [JsonProperty("icons_edit")] public string IconsEdit = string.Empty;

    [JsonProperty("icons_emojiguardian")] public string IconsEmojiguardian = string.Empty;

    [JsonProperty("icons_eventcolour")] public string IconsEventcolour = string.Empty;

    [JsonProperty("icons_exclamation")] public string IconsExclamation = string.Empty;

    [JsonProperty("icons_file")] public string IconsFile = string.Empty;

    [JsonProperty("icons_fire")] public string IconsFire = string.Empty;

    [JsonProperty("icons_forum")] public string IconsForum = string.Empty;

    [JsonProperty("icons_forumlocked")] public string IconsForumlocked = string.Empty;

    [JsonProperty("icons_forumnfsw")] public string IconsForumnfsw = string.Empty;

    [JsonProperty("icons_frontforward")] public string IconsFrontforward = string.Empty;

    [JsonProperty("icons_gitbranch")] public string IconsGitbranch = string.Empty;

    [JsonProperty("icons_gitcommit")] public string IconsGitcommit = string.Empty;

    [JsonProperty("icons_gitmerge")] public string IconsGitmerge = string.Empty;

    [JsonProperty("icons_gitpullrequest")] public string IconsGitpullrequest = string.Empty;

    [JsonProperty("icons_globe")] public string IconsGlobe = string.Empty;

    [JsonProperty("icons_goodping")] public string IconsGoodping = string.Empty;

    [JsonProperty("icons_hammer")] public string IconsHammer = string.Empty;

    [JsonProperty("icons_headphone")] public string IconsHeadphone = string.Empty;

    [JsonProperty("icons_headphonedeafen")]
    public string IconsHeadphonedeafen = string.Empty;

    [JsonProperty("icons_hyphen")] public string IconsHyphen = string.Empty;

    [JsonProperty("icons_idelping")] public string IconsIdelping = string.Empty;

    [JsonProperty("icons_illustrator")] public string IconsIllustrator = string.Empty;

    [JsonProperty("icons_info")] public string IconsInfo = string.Empty;

    [JsonProperty("icons_invite")] public string IconsInvite = string.Empty;

    [JsonProperty("icons_join")] public string IconsJoin = string.Empty;

    [JsonProperty("icons_kick")] public string IconsKick = string.Empty;

    [JsonProperty("icons_leave")] public string IconsLeave = string.Empty;

    [JsonProperty("icons_link")] public string IconsLink = string.Empty;

    [JsonProperty("icons_linked")] public string IconsLinked = string.Empty;

    [JsonProperty("icons_live")] public string IconsLive = string.Empty;

    [JsonProperty("icons_loading")] public string IconsLoading = string.Empty;

    [JsonProperty("icons_magicwand")] public string IconsMagicwand = string.Empty;

    [JsonProperty("icons_mashroomman")] public string IconsMashroomman = string.Empty;

    [JsonProperty("icons_mentalhealth")] public string IconsMentalhealth = string.Empty;

    [JsonProperty("icons_micmute")] public string IconsMicmute = string.Empty;

    [JsonProperty("icons_monitor")] public string IconsMonitor = string.Empty;

    [JsonProperty("icons_new1")] public string IconsNew1 = string.Empty;

    [JsonProperty("icons_new1a")] public string IconsNew1a = string.Empty;

    [JsonProperty("icons_new2")] public string IconsNew2 = string.Empty;

    [JsonProperty("icons_new2a")] public string IconsNew2a = string.Empty;

    [JsonProperty("icons_news1")] public string IconsNews1 = string.Empty;

    [JsonProperty("icons_news2")] public string IconsNews2 = string.Empty;

    [JsonProperty("icons_night")] public string IconsNight = string.Empty;

    [JsonProperty("icons_nitro")] public string IconsNitro = string.Empty;

    [JsonProperty("icons_nitroboost")] public string IconsNitroboost = string.Empty;

    [JsonProperty("icons_owner")] public string IconsOwner = string.Empty;

    [JsonProperty("icons_paintpadbrush")] public string IconsPaintpadbrush = string.Empty;

    [JsonProperty("icons_paypal")] public string IconsPaypal = string.Empty;

    [JsonProperty("icons_pen")] public string IconsPen = string.Empty;

    [JsonProperty("icons_people")] public string IconsPeople = string.Empty;

    [JsonProperty("icons_person")] public string IconsPerson = string.Empty;

    [JsonProperty("icons_photoshop")] public string IconsPhotoshop = string.Empty;

    [JsonProperty("icons_pin")] public string IconsPin = string.Empty;

    [JsonProperty("icons_ping")] public string IconsPing = string.Empty;

    [JsonProperty("icons_plant")] public string IconsPlant = string.Empty;

    [JsonProperty("icons_plus")] public string IconsPlus = string.Empty;

    [JsonProperty("icons_podcast")] public string IconsPodcast = string.Empty;

    [JsonProperty("icons_premiumchannel")] public string IconsPremiumchannel = string.Empty;

    [JsonProperty("icons_reminder")] public string IconsReminder = string.Empty;

    [JsonProperty("icons_repeat")] public string IconsRepeat = string.Empty;

    [JsonProperty("icons_repeatonce")] public string IconsRepeatonce = string.Empty;

    [JsonProperty("icons_reply")] public string IconsReply = string.Empty;

    [JsonProperty("icons_rightarrow")] public string IconsRightarrow = string.Empty;

    [JsonProperty("icons_saturn")] public string IconsSaturn = string.Empty;

    [JsonProperty("icons_screenshare")] public string IconsScreenshare = string.Empty;

    [JsonProperty("icons_search")] public string IconsSearch = string.Empty;

    [JsonProperty("icons_sentry")] public string IconsSentry = string.Empty;

    [JsonProperty("icons_servermute")] public string IconsServermute = string.Empty;

    [JsonProperty("icons_settings")] public string IconsSettings = string.Empty;

    [JsonProperty("icons_share")] public string IconsShare = string.Empty;

    [JsonProperty("icons_shine1")] public string IconsShine1 = string.Empty;

    [JsonProperty("icons_shine2")] public string IconsShine2 = string.Empty;

    [JsonProperty("icons_shine3")] public string IconsShine3 = string.Empty;

    [JsonProperty("icons_shuffle")] public string IconsShuffle = string.Empty;

    [JsonProperty("icons_splash")] public string IconsSplash = string.Empty;

    [JsonProperty("icons_spotify")] public string IconsSpotify = string.Empty;

    [JsonProperty("icons_stageleave")] public string IconsStageleave = string.Empty;

    [JsonProperty("icons_stagelocked")] public string IconsStagelocked = string.Empty;

    [JsonProperty("icons_stagemoderator")] public string IconsStagemoderator = string.Empty;

    [JsonProperty("icons_stagemoveaudience")]
    public string IconsStagemoveaudience = string.Empty;

    [JsonProperty("icons_stagemovespeaker")]
    public string IconsStagemovespeaker = string.Empty;

    [JsonProperty("icons_stagerequesttospeak")]
    public string IconsStagerequesttospeak = string.Empty;

    [JsonProperty("icons_stagerequesttospeaklist")]
    public string IconsStagerequesttospeaklist = string.Empty;

    [JsonProperty("icons_star")] public string IconsStar = string.Empty;

    [JsonProperty("icons_store")] public string IconsStore = string.Empty;

    [JsonProperty("icons_supportscommandsbadge")]
    public string IconsSupportscommandsbadge = string.Empty;

    [JsonProperty("icons_text1")] public string IconsText1 = string.Empty;

    [JsonProperty("icons_text2")] public string IconsText2 = string.Empty;

    [JsonProperty("icons_text3")] public string IconsText3 = string.Empty;

    [JsonProperty("icons_text4")] public string IconsText4 = string.Empty;

    [JsonProperty("icons_text5")] public string IconsText5 = string.Empty;

    [JsonProperty("icons_text6")] public string IconsText6 = string.Empty;

    [JsonProperty("icons_timeout")] public string IconsTimeout = string.Empty;

    [JsonProperty("icons_topgg")] public string IconsTopgg = string.Empty;

    [JsonProperty("icons_transferownership")]
    public string IconsTransferownership = string.Empty;

    [JsonProperty("icons_update1")] public string IconsUpdate1 = string.Empty;

    [JsonProperty("icons_update2")] public string IconsUpdate2 = string.Empty;

    [JsonProperty("icons_upvote")] public string IconsUpvote = string.Empty;

    [JsonProperty("icons_verified")] public string IconsVerified = string.Empty;

    [JsonProperty("icons_video")] public string IconsVideo = string.Empty;

    [JsonProperty("icons_wrong")] public string IconsWrong = string.Empty;

    [JsonProperty("icons_wumpus")] public string IconsWumpus = string.Empty;

    [JsonProperty("icons_xmarkwhite")] public string IconsXmarkwhite = string.Empty;

    [JsonProperty("eye_off")] public string EyeOff = string.Empty;

    [JsonProperty("eye")] public string Eye = string.Empty;

    [JsonProperty("icons_18")] public string Icons18 = string.Empty;

    [JsonProperty("icons_bigender")] public string IconsBigender = string.Empty;

    [JsonProperty("icons_calender")] public string IconsCalender = string.Empty;

    [JsonProperty("icons_calenderdate")] public string IconsCalenderdate = string.Empty;

    [JsonProperty("icons_cmd")] public string IconsCmd = string.Empty;

    [JsonProperty("icons_discordjs")] public string IconsDiscordjs = string.Empty;

    [JsonProperty("icons_female")] public string IconsFemale = string.Empty;

    [JsonProperty("icons_gay")] public string IconsGay = string.Empty;

    [JsonProperty("icons_gender")] public string IconsGender = string.Empty;

    [JsonProperty("icons_hetero")] public string IconsHetero = string.Empty;

    [JsonProperty("icons_jpg")] public string IconsJpg = string.Empty;

    [JsonProperty("icons_js")] public string IconsJs = string.Empty;

    [JsonProperty("icons_lesbian")] public string IconsLesbian = string.Empty;

    [JsonProperty("icons_male")] public string IconsMale = string.Empty;

    [JsonProperty("icons_moderationhig")] public string IconsModerationhig = string.Empty;

    [JsonProperty("icons_moderationhighest")]
    public string IconsModerationhighest = string.Empty;

    [JsonProperty("icons_moderationlow")] public string IconsModerationlow = string.Empty;

    [JsonProperty("icons_moderationmedium")]
    public string IconsModerationmedium = string.Empty;

    [JsonProperty("icons_moderationnone")] public string IconsModerationnone = string.Empty;

    [JsonProperty("icons_nodejs")] public string IconsNodejs = string.Empty;

    [JsonProperty("icons_png")] public string IconsPng = string.Empty;

    [JsonProperty("icons_radmins")] public string IconsRadmins = string.Empty;

    [JsonProperty("icons_rartists")] public string IconsRartists = string.Empty;

    [JsonProperty("icons_rboosters")] public string IconsRboosters = string.Empty;

    [JsonProperty("icons_rbots")] public string IconsRbots = string.Empty;

    [JsonProperty("icons_rcamera")] public string IconsRcamera = string.Empty;

    [JsonProperty("icons_rdevelopers")] public string IconsRdevelopers = string.Empty;

    [JsonProperty("icons_revents")] public string IconsRevents = string.Empty;

    [JsonProperty("icons_rfire")] public string IconsRfire = string.Empty;

    [JsonProperty("icons_rguardians")] public string IconsRguardians = string.Empty;

    [JsonProperty("icons_rhelpers")] public string IconsRhelpers = string.Empty;

    [JsonProperty("icons_rmembers")] public string IconsRmembers = string.Empty;

    [JsonProperty("icons_rmods")] public string IconsRmods = string.Empty;

    [JsonProperty("icons_rowner")] public string IconsRowner = string.Empty;

    [JsonProperty("icons_rpodcast")] public string IconsRpodcast = string.Empty;

    [JsonProperty("icons_rsdonator")] public string IconsRsdonator = string.Empty;

    [JsonProperty("icons_rspartner")] public string IconsRspartner = string.Empty;

    [JsonProperty("icons_rsstaffs")] public string IconsRsstaffs = string.Empty;

    [JsonProperty("icons_rstaff")] public string IconsRstaff = string.Empty;

    [JsonProperty("icons_rverification")] public string IconsRverification = string.Empty;

    [JsonProperty("icons_rverified")] public string IconsRverified = string.Empty;

    [JsonProperty("icons_rvip")] public string IconsRvip = string.Empty;

    [JsonProperty("icons_snowflake")] public string IconsSnowflake = string.Empty;

    [JsonProperty("icons_tiktok")] public string IconsTiktok = string.Empty;

    [JsonProperty("icons_transgender")] public string IconsTransgender = string.Empty;

    [JsonProperty("icons_twitch")] public string IconsTwitch = string.Empty;

    [JsonProperty("icons_vklogo")] public string IconsVklogo = string.Empty;

    [JsonProperty("icons_warning")] public string IconsWarning = string.Empty;

    [JsonProperty("icons_wave")] public string IconsWave = string.Empty;

    [JsonProperty("icons_webp")] public string IconsWebp = string.Empty;

    [JsonProperty("file_archive")] public string FileArchive = string.Empty;

    [JsonProperty("file_check")] public string FileCheck = string.Empty;

    [JsonProperty("file_cog")] public string FileCog = string.Empty;

    [JsonProperty("file_files")] public string FileFiles = string.Empty;

    [JsonProperty("file_minus")] public string FileMinus = string.Empty;

    [JsonProperty("file_plus")] public string FilePlus = string.Empty;

    [JsonProperty("file_x")] public string FileX = string.Empty;

    [JsonProperty("file")] public string File = string.Empty;

    [JsonProperty("cpu")] public string Cpu = string.Empty;

    [JsonProperty("ram")] public string Ram = string.Empty;

    [JsonProperty("ssd")] public string Ssd = string.Empty;

    [JsonProperty("wifi")] public string Wifi = string.Empty;

    [JsonProperty("folder_archive")] public string FolderArchive = string.Empty;

    [JsonProperty("folder_check")] public string FolderCheck = string.Empty;

    [JsonProperty("folder_folders")] public string FolderFolders = string.Empty;

    [JsonProperty("folder_minus")] public string FolderMinus = string.Empty;

    [JsonProperty("folder_plus")] public string FolderPlus = string.Empty;

    [JsonProperty("folder_x")] public string FolderX = string.Empty;

    [JsonProperty("folder")] public string Folder = string.Empty;

    [JsonProperty("headphone_off")] public string HeadphoneOff = string.Empty;

    [JsonProperty("headphone")] public string Headphone = string.Empty;

    [JsonProperty("image_download")] public string ImageDownload = string.Empty;

    [JsonProperty("image_minus")] public string ImageMinus = string.Empty;

    [JsonProperty("image_off")] public string ImageOff = string.Empty;

    [JsonProperty("image_plus")] public string ImagePlus = string.Empty;

    [JsonProperty("image_upload")] public string ImageUpload = string.Empty;

    [JsonProperty("image")] public string Image = string.Empty;

    [JsonProperty("lock_unlock")] public string LockUnlock = string.Empty;

    [JsonProperty("lock")] public string Lock = string.Empty;

    [JsonProperty("mail_check")] public string MailCheck = string.Empty;

    [JsonProperty("mail_minus")] public string MailMinus = string.Empty;

    [JsonProperty("mail_plus")] public string MailPlus = string.Empty;

    [JsonProperty("mail_x")] public string MailX = string.Empty;

    [JsonProperty("mail")] public string Mail = string.Empty;

    [JsonProperty("mic_off")] public string MicOff = string.Empty;

    [JsonProperty("mic")] public string Mic = string.Empty;

    [JsonProperty("phone_off")] public string PhoneOff = string.Empty;

    [JsonProperty("phone")] public string Phone = string.Empty;

    [JsonProperty("home")] public string Home = string.Empty;

    [JsonProperty("id")] public string Id = string.Empty;

    [JsonProperty("list")] public string List = string.Empty;

    [JsonProperty("other_brush")] public string OtherBrush = string.Empty;

    [JsonProperty("other_cable")] public string OtherCable = string.Empty;

    [JsonProperty("other_crown")] public string OtherCrown = string.Empty;

    [JsonProperty("other_dollar")] public string OtherDollar = string.Empty;

    [JsonProperty("other_earth")] public string OtherEarth = string.Empty;

    [JsonProperty("other_gauge")] public string OtherGauge = string.Empty;

    [JsonProperty("other_gear")] public string OtherGear = string.Empty;

    [JsonProperty("other_graduation")] public string OtherGraduation = string.Empty;

    [JsonProperty("other_heart")] public string OtherHeart = string.Empty;

    [JsonProperty("other_home")] public string OtherHome = string.Empty;

    [JsonProperty("other_save_off")] public string OtherSaveOff = string.Empty;

    [JsonProperty("other_save")] public string OtherSave = string.Empty;

    [JsonProperty("other_text")] public string OtherText = string.Empty;

    [JsonProperty("other_translate")] public string OtherTranslate = string.Empty;

    [JsonProperty("other_trash")] public string OtherTrash = string.Empty;

    [JsonProperty("other_truck")] public string OtherTruck = string.Empty;

    [JsonProperty("refresh")] public string Refresh = string.Empty;

    [JsonProperty("pause")] public string Pause = string.Empty;

    [JsonProperty("resume")] public string Resume = string.Empty;

    [JsonProperty("stop")] public string Stop = string.Empty;

    [JsonProperty("view")] public string View = string.Empty;

    [JsonProperty("shield_check")] public string ShieldCheck = string.Empty;

    [JsonProperty("shield_minus")] public string ShieldMinus = string.Empty;

    [JsonProperty("shield_off")] public string ShieldOff = string.Empty;

    [JsonProperty("shield_plus")] public string ShieldPlus = string.Empty;

    [JsonProperty("shield_x")] public string ShieldX = string.Empty;

    [JsonProperty("shield")] public string Shield = string.Empty;

    [JsonProperty("tag_tags")] public string TagTags = string.Empty;

    [JsonProperty("tag")] public string Tag = string.Empty;

    [JsonProperty("ticket_check")] public string TicketCheck = string.Empty;

    [JsonProperty("ticket_minus")] public string TicketMinus = string.Empty;

    [JsonProperty("ticket_plus")] public string TicketPlus = string.Empty;

    [JsonProperty("ticket_tickets")] public string TicketTickets = string.Empty;

    [JsonProperty("ticket_x")] public string TicketX = string.Empty;

    [JsonProperty("ticket")] public string Ticket = string.Empty;

    [JsonProperty("user_check")] public string UserCheck = string.Empty;

    [JsonProperty("user_cog")] public string UserCog = string.Empty;

    [JsonProperty("user_minus")] public string UserMinus = string.Empty;

    [JsonProperty("user_plus")] public string UserPlus = string.Empty;

    [JsonProperty("user_users")] public string UserUsers = string.Empty;

    [JsonProperty("user_x")] public string UserX = string.Empty;

    [JsonProperty("user")] public string User = string.Empty;

    [JsonProperty("timer_off")] public string TimerOff = string.Empty;

    [JsonProperty("timer_reset")] public string TimerReset = string.Empty;

    [JsonProperty("timer")] public string Timer = string.Empty;
}