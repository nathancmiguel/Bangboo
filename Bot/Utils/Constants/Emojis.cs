using Newtonsoft.Json;

namespace Bot.Utils.Constants;

public class Emojis
{
    [JsonProperty("static")] public Static Static;

    [JsonProperty("animated")] public Animated Animated;
}

public class Animated
{
    [JsonProperty("boost")] public string Boost;
}

public class Static
{
    [JsonProperty("add")] public string Add;

    [JsonProperty("hammer")] public string Hammer;

    [JsonProperty("minus")] public string Minus;

    [JsonProperty("action_check")] public string ActionCheck;

    [JsonProperty("action_help")] public string ActionHelp;

    [JsonProperty("action_info")] public string ActionInfo;

    [JsonProperty("action_warning")] public string ActionWarning;

    [JsonProperty("action_x")] public string ActionX;

    [JsonProperty("check")] public string Check;

    [JsonProperty("close")] public string Close;

    [JsonProperty("arrow_down")] public string ArrowDown;

    [JsonProperty("arrow_left")] public string ArrowLeft;

    [JsonProperty("arrow_refresh")] public string ArrowRefresh;

    [JsonProperty("arrow_right")] public string ArrowRight;

    [JsonProperty("arrow_up")] public string ArrowUp;

    [JsonProperty("back")] public string Back;

    [JsonProperty("next")] public string Next;

    [JsonProperty("instagram")] public string Instagram;

    [JsonProperty("linkedin")] public string Linkedin;

    [JsonProperty("youtube")] public string Youtube;

    [JsonProperty("bell_dot")] public string BellDot;

    [JsonProperty("bell_minus")] public string BellMinus;

    [JsonProperty("bell_off")] public string BellOff;

    [JsonProperty("bell_plus")] public string BellPlus;

    [JsonProperty("bell")] public string Bell;

    [JsonProperty("book_check")] public string BookCheck;

    [JsonProperty("book_minus")] public string BookMinus;

    [JsonProperty("book_plus")] public string BookPlus;

    [JsonProperty("book_x")] public string BookX;

    [JsonProperty("book")] public string Book;

    [JsonProperty("calendar_cog")] public string CalendarCog;

    [JsonProperty("calendar_days")] public string CalendarDays;

    [JsonProperty("calendar_minus")] public string CalendarMinus;

    [JsonProperty("calendar_plus")] public string CalendarPlus;

    [JsonProperty("camera_off")] public string CameraOff;

    [JsonProperty("camera")] public string Camera;

    [JsonProperty("clipboard_check")] public string ClipboardCheck;

    [JsonProperty("clipboard_minus")] public string ClipboardMinus;

    [JsonProperty("clipboard_plus")] public string ClipboardPlus;

    [JsonProperty("clipboard_x")] public string ClipboardX;

    [JsonProperty("clipboard")] public string Clipboard;

    [JsonProperty("clock_check")] public string ClockCheck;

    [JsonProperty("clock_minus")] public string ClockMinus;

    [JsonProperty("clock_off")] public string ClockOff;

    [JsonProperty("clock_plus")] public string ClockPlus;

    [JsonProperty("clock")] public string Clock;

    [JsonProperty("cloud_cog")] public string CloudCog;

    [JsonProperty("cloud_download")] public string CloudDownload;

    [JsonProperty("cloud_upload")] public string CloudUpload;

    [JsonProperty("cloud")] public string Cloud;

    [JsonProperty("code_braces")] public string CodeBraces;

    [JsonProperty("code_brackets")] public string CodeBrackets;

    [JsonProperty("code_bug")] public string CodeBug;

    [JsonProperty("code_file_binary")] public string CodeFileBinary;

    [JsonProperty("code_parentheses")] public string CodeParentheses;

    [JsonProperty("code_server_cog")] public string CodeServerCog;

    [JsonProperty("code_server_off")] public string CodeServerOff;

    [JsonProperty("code_server")] public string CodeServer;

    [JsonProperty("code_terminal")] public string CodeTerminal;

    [JsonProperty("code_window")] public string CodeWindow;

    [JsonProperty("code_wrench")] public string CodeWrench;

    [JsonProperty("database_backup")] public string DatabaseBackup;

    [JsonProperty("database")] public string Database;

    [JsonProperty("icons_bookmark")] public string IconsBookmark;

    [JsonProperty("icons_busy")] public string IconsBusy;

    [JsonProperty("icons_camera")] public string IconsCamera;

    [JsonProperty("icons_clouddown")] public string IconsClouddown;

    [JsonProperty("icons_code")] public string IconsCode;

    [JsonProperty("icons_control")] public string IconsControl;

    [JsonProperty("icons_downarrow")] public string IconsDownarrow;

    [JsonProperty("icons_education")] public string IconsEducation;

    [JsonProperty("icons_flag")] public string IconsFlag;

    [JsonProperty("icons_folder")] public string IconsFolder;

    [JsonProperty("icons_fword")] public string IconsFword;

    [JsonProperty("icons_games")] public string IconsGames;

    [JsonProperty("icons_gif")] public string IconsGif;

    [JsonProperty("icons_gift")] public string IconsGift;

    [JsonProperty("icons_heart")] public string IconsHeart;

    [JsonProperty("icons_hi")] public string IconsHi;

    [JsonProperty("icons_id")] public string IconsId;

    [JsonProperty("icons_idle")] public string IconsIdle;

    [JsonProperty("icons_image")] public string IconsImage;

    [JsonProperty("icons_leftarrow")] public string IconsLeftarrow;

    [JsonProperty("icons_list")] public string IconsList;

    [JsonProperty("icons_loadingerror")] public string IconsLoadingerror;

    [JsonProperty("icons_message")] public string IconsMessage;

    [JsonProperty("icons_music")] public string IconsMusic;

    [JsonProperty("icons_notify")] public string IconsNotify;

    [JsonProperty("icons_off")] public string IconsOff;

    [JsonProperty("icons_offline")] public string IconsOffline;

    [JsonProperty("icons_on")] public string IconsOn;

    [JsonProperty("icons_online")] public string IconsOnline;

    [JsonProperty("icons_outage")] public string IconsOutage;

    [JsonProperty("icons_premium")] public string IconsPremium;

    [JsonProperty("icons_question")] public string IconsQuestion;

    [JsonProperty("icons_quotes")] public string IconsQuotes;

    [JsonProperty("icons_richpresence")] public string IconsRichpresence;

    [JsonProperty("icons_rules")] public string IconsRules;

    [JsonProperty("icons_slashcmd")] public string IconsSlashcmd;

    [JsonProperty("icons_spark")] public string IconsSpark;

    [JsonProperty("icons_speaker")] public string IconsSpeaker;

    [JsonProperty("icons_speakerlock")] public string IconsSpeakerlock;

    [JsonProperty("icons_speakerlow")] public string IconsSpeakerlow;

    [JsonProperty("icons_speakermute")] public string IconsSpeakermute;

    [JsonProperty("icons_stickers")] public string IconsStickers;

    [JsonProperty("icons_stream")] public string IconsStream;

    [JsonProperty("icons_ticket")] public string IconsTicket;

    [JsonProperty("icons_tilde")] public string IconsTilde;

    [JsonProperty("icons_todolist")] public string IconsTodolist;

    [JsonProperty("icons_uparrow")] public string IconsUparrow;

    [JsonProperty("icons_update")] public string IconsUpdate;

    [JsonProperty("icons_view")] public string IconsView;

    [JsonProperty("icons_vip")] public string IconsVip;

    [JsonProperty("device_laptop")] public string DeviceLaptop;

    [JsonProperty("device_pc")] public string DevicePc;

    [JsonProperty("device_smartphone")] public string DeviceSmartphone;

    [JsonProperty("device_tablet")] public string DeviceTablet;

    [JsonProperty("icons_1")] public string Icons1;

    [JsonProperty("icons_addreactions")] public string IconsAddreactions;

    [JsonProperty("icons_aka")] public string IconsAka;

    [JsonProperty("icons_behance")] public string IconsBehance;

    [JsonProperty("icons_beta")] public string IconsBeta;

    [JsonProperty("icons_bots")] public string IconsBots;

    [JsonProperty("icons_clean")] public string IconsClean;

    [JsonProperty("icons_defaultperms")] public string IconsDefaultperms;

    [JsonProperty("icons_discordbotdev")] public string IconsDiscordbotdev;

    [JsonProperty("icons_discordmod")] public string IconsDiscordmod;

    [JsonProperty("icons_discordnitro")] public string IconsDiscordnitro;

    [JsonProperty("icons_discordpartner")] public string IconsDiscordpartner;

    [JsonProperty("icons_discordstaff")] public string IconsDiscordstaff;

    [JsonProperty("icons_dislike")] public string IconsDislike;

    [JsonProperty("icons_earlysupporter")] public string IconsEarlysupporter;

    [JsonProperty("icons_fb")] public string IconsFb;

    [JsonProperty("icons_figma")] public string IconsFigma;

    [JsonProperty("icons_files")] public string IconsFiles;

    [JsonProperty("icons_friends")] public string IconsFriends;

    [JsonProperty("icons_github")] public string IconsGithub;

    [JsonProperty("icons_hoursglass")] public string IconsHoursglass;

    [JsonProperty("icons_hsbalance")] public string IconsHsbalance;

    [JsonProperty("icons_hsbravery")] public string IconsHsbravery;

    [JsonProperty("icons_hsbrilliance")] public string IconsHsbrilliance;

    [JsonProperty("icons_instagram")] public string IconsInstagram;

    [JsonProperty("icons_kicking")] public string IconsKicking;

    [JsonProperty("icons_kofi")] public string IconsKofi;

    [JsonProperty("icons_like")] public string IconsLike;

    [JsonProperty("icons_locked")] public string IconsLocked;

    [JsonProperty("icons_loop")] public string IconsLoop;

    [JsonProperty("icons_menu")] public string IconsMenu;

    [JsonProperty("icons_msvisualcode")] public string IconsMsvisualcode;

    [JsonProperty("icons_musicstop")] public string IconsMusicstop;

    [JsonProperty("icons_new")] public string IconsNew;

    [JsonProperty("icons_partner")] public string IconsPartner;

    [JsonProperty("icons_patreon")] public string IconsPatreon;

    [JsonProperty("icons_pause")] public string IconsPause;

    [JsonProperty("icons_pings")] public string IconsPings;

    [JsonProperty("icons_play")] public string IconsPlay;

    [JsonProperty("icons_queue")] public string IconsQueue;

    [JsonProperty("icons_reddit")] public string IconsReddit;

    [JsonProperty("icons_serverpartner")] public string IconsServerpartner;

    [JsonProperty("icons_serververified")] public string IconsSerververified;

    [JsonProperty("icons_snapchat")] public string IconsSnapchat;

    [JsonProperty("icons_supportteam")] public string IconsSupportteam;

    [JsonProperty("icons_twitter")] public string IconsTwitter;

    [JsonProperty("icons_unlock")] public string IconsUnlock;

    [JsonProperty("icons_youtube")] public string IconsYoutube;

    [JsonProperty("icons_banmembers")] public string IconsBanmembers;

    [JsonProperty("icons_channelfollowed")]
    public string IconsChannelfollowed;

    [JsonProperty("icons_createcategory")] public string IconsCreatecategory;

    [JsonProperty("icons_createchannel")] public string IconsCreatechannel;

    [JsonProperty("icons_createchannels")] public string IconsCreatechannels;

    [JsonProperty("icons_createemoji")] public string IconsCreateemoji;

    [JsonProperty("icons_createintegration")]
    public string IconsCreateintegration;

    [JsonProperty("icons_createrole")] public string IconsCreaterole;

    [JsonProperty("icons_createsticker")] public string IconsCreatesticker;

    [JsonProperty("icons_createthread")] public string IconsCreatethread;

    [JsonProperty("icons_createwebhook")] public string IconsCreatewebhook;

    [JsonProperty("icons_deletechannel")] public string IconsDeletechannel;

    [JsonProperty("icons_deleteemoji")] public string IconsDeleteemoji;

    [JsonProperty("icons_deleteevent")] public string IconsDeleteevent;

    [JsonProperty("icons_deleteintegration")]
    public string IconsDeleteintegration;

    [JsonProperty("icons_deleterole")] public string IconsDeleterole;

    [JsonProperty("icons_deletesticker")] public string IconsDeletesticker;

    [JsonProperty("icons_deletethread")] public string IconsDeletethread;

    [JsonProperty("icons_deletewebhook")] public string IconsDeletewebhook;

    [JsonProperty("icons_disable")] public string IconsDisable;

    [JsonProperty("icons_discord")] public string IconsDiscord;

    [JsonProperty("icons_enable")] public string IconsEnable;

    [JsonProperty("icons_endstage")] public string IconsEndstage;

    [JsonProperty("icons_envelope")] public string IconsEnvelope;

    [JsonProperty("icons_generalinfo")] public string IconsGeneralinfo;

    [JsonProperty("icons_growth")] public string IconsGrowth;

    [JsonProperty("icons_linkadd")] public string IconsLinkadd;

    [JsonProperty("icons_linkrevoke")] public string IconsLinkrevoke;

    [JsonProperty("icons_linkupdate")] public string IconsLinkupdate;

    [JsonProperty("icons_notificationsettings")]
    public string IconsNotificationsettings;

    [JsonProperty("icons_oauth2")] public string IconsOauth2;

    [JsonProperty("icons_roles")] public string IconsRoles;

    [JsonProperty("icons_scheduleevent")] public string IconsScheduleevent;

    [JsonProperty("icons_serverinsight")] public string IconsServerinsight;

    [JsonProperty("icons_startstage")] public string IconsStartstage;

    [JsonProperty("icons_swardx")] public string IconsSwardx;

    [JsonProperty("icons_threadchannel")] public string IconsThreadchannel;

    [JsonProperty("icons_unbanmember")] public string IconsUnbanmember;

    [JsonProperty("icons_updatechannel")] public string IconsUpdatechannel;

    [JsonProperty("icons_updateemoji")] public string IconsUpdateemoji;

    [JsonProperty("icons_updateevent")] public string IconsUpdateevent;

    [JsonProperty("icons_updateintegration")]
    public string IconsUpdateintegration;

    [JsonProperty("icons_updatemember")] public string IconsUpdatemember;

    [JsonProperty("icons_updaterole")] public string IconsUpdaterole;

    [JsonProperty("icons_updateserver")] public string IconsUpdateserver;

    [JsonProperty("icons_updatestage")] public string IconsUpdatestage;

    [JsonProperty("icons_updatesticker")] public string IconsUpdatesticker;

    [JsonProperty("icons_updatethread")] public string IconsUpdatethread;

    [JsonProperty("icons_updatewebhook")] public string IconsUpdatewebhook;

    [JsonProperty("icons_0")] public string Icons0;

    [JsonProperty("icons_10")] public string Icons10;

    [JsonProperty("icons_2")] public string Icons2;

    [JsonProperty("icons_3")] public string Icons3;

    [JsonProperty("icons_4")] public string Icons4;

    [JsonProperty("icons_5")] public string Icons5;

    [JsonProperty("icons_6")] public string Icons6;

    [JsonProperty("icons_7")] public string Icons7;

    [JsonProperty("icons_8")] public string Icons8;

    [JsonProperty("icons_9")] public string Icons9;

    [JsonProperty("icons_a")] public string IconsA;

    [JsonProperty("icons_amogus")] public string IconsAmogus;

    [JsonProperty("icons_b")] public string IconsB;

    [JsonProperty("icons_bday")] public string IconsBday;

    [JsonProperty("icons_book")] public string IconsBook;

    [JsonProperty("icons_c")] public string IconsC;

    [JsonProperty("icons_d")] public string IconsD;

    [JsonProperty("icons_e")] public string IconsE;

    [JsonProperty("icons_f")] public string IconsF;

    [JsonProperty("icons_fingerprint")] public string IconsFingerprint;

    [JsonProperty("icons_g")] public string IconsG;

    [JsonProperty("icons_guardian")] public string IconsGuardian;

    [JsonProperty("icons_h")] public string IconsH;

    [JsonProperty("icons_he_him")] public string IconsHeHim;

    [JsonProperty("icons_i")] public string IconsI;

    [JsonProperty("icons_j")] public string IconsJ;

    [JsonProperty("icons_k")] public string IconsK;

    [JsonProperty("icons_l")] public string IconsL;

    [JsonProperty("icons_library")] public string IconsLibrary;

    [JsonProperty("icons_m")] public string IconsM;

    [JsonProperty("icons_n")] public string IconsN;

    [JsonProperty("icons_o")] public string IconsO;

    [JsonProperty("icons_p")] public string IconsP;

    [JsonProperty("icons_q")] public string IconsQ;

    [JsonProperty("icons_r")] public string IconsR;

    [JsonProperty("icons_s")] public string IconsS;

    [JsonProperty("icons_she_her")] public string IconsSheHer;

    [JsonProperty("icons_statsdown")] public string IconsStatsdown;

    [JsonProperty("icons_t")] public string IconsT;

    [JsonProperty("icons_tada")] public string IconsTada;

    [JsonProperty("icons_they_them")] public string IconsTheyThem;

    [JsonProperty("icons_translate")] public string IconsTranslate;

    [JsonProperty("icons_u")] public string IconsU;

    [JsonProperty("icons_v")] public string IconsV;

    [JsonProperty("icons_vpn")] public string IconsVpn;

    [JsonProperty("icons_w")] public string IconsW;

    [JsonProperty("icons_x")] public string IconsX;

    [JsonProperty("icons_y")] public string IconsY;

    [JsonProperty("icons_z")] public string IconsZ;

    [JsonProperty("eg_addemoji")] public string EgAddemoji;

    [JsonProperty("eg_addfile")] public string EgAddfile;

    [JsonProperty("eg_addons")] public string EgAddons;

    [JsonProperty("eg_announcement")] public string EgAnnouncement;

    [JsonProperty("eg_art")] public string EgArt;

    [JsonProperty("eg_ask")] public string EgAsk;

    [JsonProperty("eg_ban")] public string EgBan;

    [JsonProperty("eg_book")] public string EgBook;

    [JsonProperty("eg_bot")] public string EgBot;

    [JsonProperty("eg_calender")] public string EgCalender;

    [JsonProperty("eg_cautions")] public string EgCautions;

    [JsonProperty("eg_channels")] public string EgChannels;

    [JsonProperty("eg_cloud")] public string EgCloud;

    [JsonProperty("eg_clouddownload")] public string EgClouddownload;

    [JsonProperty("eg_cross")] public string EgCross;

    [JsonProperty("eg_developers")] public string EgDevelopers;

    [JsonProperty("eg_discovery")] public string EgDiscovery;

    [JsonProperty("eg_downarrow")] public string EgDownarrow;

    [JsonProperty("eg_emojis")] public string EgEmojis;

    [JsonProperty("eg_excl")] public string EgExcl;

    [JsonProperty("eg_female")] public string EgFemale;

    [JsonProperty("eg_files")] public string EgFiles;

    [JsonProperty("eg_fire")] public string EgFire;

    [JsonProperty("eg_gift")] public string EgGift;

    [JsonProperty("eg_globe")] public string EgGlobe;

    [JsonProperty("eg_hammer")] public string EgHammer;

    [JsonProperty("eg_heart")] public string EgHeart;

    [JsonProperty("eg_home")] public string EgHome;

    [JsonProperty("eg_hourclock")] public string EgHourclock;

    [JsonProperty("eg_inbox")] public string EgInbox;

    [JsonProperty("eg_link")] public string EgLink;

    [JsonProperty("eg_lock")] public string EgLock;

    [JsonProperty("eg_mail")] public string EgMail;

    [JsonProperty("eg_male")] public string EgMale;

    [JsonProperty("eg_member")] public string EgMember;

    [JsonProperty("eg_message")] public string EgMessage;

    [JsonProperty("eg_modadmin")] public string EgModadmin;

    [JsonProperty("eg_monitor")] public string EgMonitor;

    [JsonProperty("eg_music")] public string EgMusic;

    [JsonProperty("eg_netual")] public string EgNetual;

    [JsonProperty("eg_notification")] public string EgNotification;

    [JsonProperty("eg_openpage")] public string EgOpenpage;

    [JsonProperty("eg_pins")] public string EgPins;

    [JsonProperty("eg_premium")] public string EgPremium;

    [JsonProperty("eg_question")] public string EgQuestion;

    [JsonProperty("eg_refresh")] public string EgRefresh;

    [JsonProperty("eg_right")] public string EgRight;

    [JsonProperty("eg_setting")] public string EgSetting;

    [JsonProperty("eg_shield")] public string EgShield;

    [JsonProperty("eg_star")] public string EgStar;

    [JsonProperty("eg_stop")] public string EgStop;

    [JsonProperty("eg_study")] public string EgStudy;

    [JsonProperty("eg_support")] public string EgSupport;

    [JsonProperty("eg_thumbdown")] public string EgThumbdown;

    [JsonProperty("eg_thumbup")] public string EgThumbup;

    [JsonProperty("eg_ticket")] public string EgTicket;

    [JsonProperty("eg_tools")] public string EgTools;

    [JsonProperty("eg_trans")] public string EgTrans;

    [JsonProperty("eg_unlock")] public string EgUnlock;

    [JsonProperty("eg_uparrow")] public string EgUparrow;

    [JsonProperty("eg_upleft")] public string EgUpleft;

    [JsonProperty("eg_upload")] public string EgUpload;

    [JsonProperty("eg_upright")] public string EgUpright;

    [JsonProperty("eg_video")] public string EgVideo;

    [JsonProperty("eg_wave")] public string EgWave;

    [JsonProperty("eg_wrench")] public string EgWrench;

    [JsonProperty("eg_wrong")] public string EgWrong;

    [JsonProperty("iconslogo")] public string Iconslogo;

    [JsonProperty("icons_activedevbadge")] public string IconsActivedevbadge;

    [JsonProperty("icons_activities")] public string IconsActivities;

    [JsonProperty("icons_adventcalendar")] public string IconsAdventcalendar;

    [JsonProperty("icons_announce")] public string IconsAnnounce;

    [JsonProperty("icons_archive")] public string IconsArchive;

    [JsonProperty("icons_audiodisable")] public string IconsAudiodisable;

    [JsonProperty("icons_audioenable")] public string IconsAudioenable;

    [JsonProperty("icons_award")] public string IconsAward;

    [JsonProperty("icons_awardcup")] public string IconsAwardcup;

    [JsonProperty("icons_backforward")] public string IconsBackforward;

    [JsonProperty("icons_badping")] public string IconsBadping;

    [JsonProperty("icons_ban")] public string IconsBan;

    [JsonProperty("icons_bank")] public string IconsBank;

    [JsonProperty("icons_beta1")] public string IconsBeta1;

    [JsonProperty("icons_beta1a")] public string IconsBeta1a;

    [JsonProperty("icons_beta2")] public string IconsBeta2;

    [JsonProperty("icons_beta2a")] public string IconsBeta2a;

    [JsonProperty("icons_birdman")] public string IconsBirdman;

    [JsonProperty("icons_box")] public string IconsBox;

    [JsonProperty("icons_bright")] public string IconsBright;

    [JsonProperty("icons_bugs")] public string IconsBugs;

    [JsonProperty("icons_bulb")] public string IconsBulb;

    [JsonProperty("icons_calendar1")] public string IconsCalendar1;

    [JsonProperty("icons_callconnect")] public string IconsCallconnect;

    [JsonProperty("icons_calldecline")] public string IconsCalldecline;

    [JsonProperty("icons_calldisconnect")] public string IconsCalldisconnect;

    [JsonProperty("icons_channel")] public string IconsChannel;

    [JsonProperty("icons_clock")] public string IconsClock;

    [JsonProperty("icons_coin")] public string IconsCoin;

    [JsonProperty("icons_colorboostnitro")]
    public string IconsColorboostnitro;

    [JsonProperty("icons_colornitro")] public string IconsColornitro;

    [JsonProperty("icons_colorserverpartner")]
    public string IconsColorserverpartner;

    [JsonProperty("icons_colorserververified")]
    public string IconsColorserververified;

    [JsonProperty("icons_colorstaff")] public string IconsColorstaff;

    [JsonProperty("icons_connect")] public string IconsConnect;

    [JsonProperty("icons_correct")] public string IconsCorrect;

    [JsonProperty("icons_creditcard")] public string IconsCreditcard;

    [JsonProperty("icons_customstaff")] public string IconsCustomstaff;

    [JsonProperty("icons_dac")] public string IconsDac;

    [JsonProperty("icons_dblurple")] public string IconsDblurple;

    [JsonProperty("icons_delete")] public string IconsDelete;

    [JsonProperty("icons_dfuchsia")] public string IconsDfuchsia;

    [JsonProperty("icons_dgreen")] public string IconsDgreen;

    [JsonProperty("icons_discover")] public string IconsDiscover;

    [JsonProperty("icons_djoin")] public string IconsDjoin;

    [JsonProperty("icons_dleave")] public string IconsDleave;

    [JsonProperty("icons_dollar")] public string IconsDollar;

    [JsonProperty("icons_download")] public string IconsDownload;

    [JsonProperty("icons_downvote")] public string IconsDownvote;

    [JsonProperty("icons_dred")] public string IconsDred;

    [JsonProperty("icons_dwhite")] public string IconsDwhite;

    [JsonProperty("icons_dyellow")] public string IconsDyellow;

    [JsonProperty("icons_edit")] public string IconsEdit;

    [JsonProperty("icons_emojiguardian")] public string IconsEmojiguardian;

    [JsonProperty("icons_eventcolour")] public string IconsEventcolour;

    [JsonProperty("icons_exclamation")] public string IconsExclamation;

    [JsonProperty("icons_file")] public string IconsFile;

    [JsonProperty("icons_fire")] public string IconsFire;

    [JsonProperty("icons_forum")] public string IconsForum;

    [JsonProperty("icons_forumlocked")] public string IconsForumlocked;

    [JsonProperty("icons_forumnfsw")] public string IconsForumnfsw;

    [JsonProperty("icons_frontforward")] public string IconsFrontforward;

    [JsonProperty("icons_gitbranch")] public string IconsGitbranch;

    [JsonProperty("icons_gitcommit")] public string IconsGitcommit;

    [JsonProperty("icons_gitmerge")] public string IconsGitmerge;

    [JsonProperty("icons_gitpullrequest")] public string IconsGitpullrequest;

    [JsonProperty("icons_globe")] public string IconsGlobe;

    [JsonProperty("icons_goodping")] public string IconsGoodping;

    [JsonProperty("icons_hammer")] public string IconsHammer;

    [JsonProperty("icons_headphone")] public string IconsHeadphone;

    [JsonProperty("icons_headphonedeafen")]
    public string IconsHeadphonedeafen;

    [JsonProperty("icons_hyphen")] public string IconsHyphen;

    [JsonProperty("icons_idelping")] public string IconsIdelping;

    [JsonProperty("icons_illustrator")] public string IconsIllustrator;

    [JsonProperty("icons_info")] public string IconsInfo;

    [JsonProperty("icons_invite")] public string IconsInvite;

    [JsonProperty("icons_join")] public string IconsJoin;

    [JsonProperty("icons_kick")] public string IconsKick;

    [JsonProperty("icons_leave")] public string IconsLeave;

    [JsonProperty("icons_link")] public string IconsLink;

    [JsonProperty("icons_linked")] public string IconsLinked;

    [JsonProperty("icons_live")] public string IconsLive;

    [JsonProperty("icons_loading")] public string IconsLoading;

    [JsonProperty("icons_magicwand")] public string IconsMagicwand;

    [JsonProperty("icons_mashroomman")] public string IconsMashroomman;

    [JsonProperty("icons_mentalhealth")] public string IconsMentalhealth;

    [JsonProperty("icons_micmute")] public string IconsMicmute;

    [JsonProperty("icons_monitor")] public string IconsMonitor;

    [JsonProperty("icons_new1")] public string IconsNew1;

    [JsonProperty("icons_new1a")] public string IconsNew1a;

    [JsonProperty("icons_new2")] public string IconsNew2;

    [JsonProperty("icons_new2a")] public string IconsNew2a;

    [JsonProperty("icons_news1")] public string IconsNews1;

    [JsonProperty("icons_news2")] public string IconsNews2;

    [JsonProperty("icons_night")] public string IconsNight;

    [JsonProperty("icons_nitro")] public string IconsNitro;

    [JsonProperty("icons_nitroboost")] public string IconsNitroboost;

    [JsonProperty("icons_owner")] public string IconsOwner;

    [JsonProperty("icons_paintpadbrush")] public string IconsPaintpadbrush;

    [JsonProperty("icons_paypal")] public string IconsPaypal;

    [JsonProperty("icons_pen")] public string IconsPen;

    [JsonProperty("icons_people")] public string IconsPeople;

    [JsonProperty("icons_person")] public string IconsPerson;

    [JsonProperty("icons_photoshop")] public string IconsPhotoshop;

    [JsonProperty("icons_pin")] public string IconsPin;

    [JsonProperty("icons_ping")] public string IconsPing;

    [JsonProperty("icons_plant")] public string IconsPlant;

    [JsonProperty("icons_plus")] public string IconsPlus;

    [JsonProperty("icons_podcast")] public string IconsPodcast;

    [JsonProperty("icons_premiumchannel")] public string IconsPremiumchannel;

    [JsonProperty("icons_reminder")] public string IconsReminder;

    [JsonProperty("icons_repeat")] public string IconsRepeat;

    [JsonProperty("icons_repeatonce")] public string IconsRepeatonce;

    [JsonProperty("icons_reply")] public string IconsReply;

    [JsonProperty("icons_rightarrow")] public string IconsRightarrow;

    [JsonProperty("icons_saturn")] public string IconsSaturn;

    [JsonProperty("icons_screenshare")] public string IconsScreenshare;

    [JsonProperty("icons_search")] public string IconsSearch;

    [JsonProperty("icons_sentry")] public string IconsSentry;

    [JsonProperty("icons_servermute")] public string IconsServermute;

    [JsonProperty("icons_settings")] public string IconsSettings;

    [JsonProperty("icons_share")] public string IconsShare;

    [JsonProperty("icons_shine1")] public string IconsShine1;

    [JsonProperty("icons_shine2")] public string IconsShine2;

    [JsonProperty("icons_shine3")] public string IconsShine3;

    [JsonProperty("icons_shuffle")] public string IconsShuffle;

    [JsonProperty("icons_splash")] public string IconsSplash;

    [JsonProperty("icons_spotify")] public string IconsSpotify;

    [JsonProperty("icons_stageleave")] public string IconsStageleave;

    [JsonProperty("icons_stagelocked")] public string IconsStagelocked;

    [JsonProperty("icons_stagemoderator")] public string IconsStagemoderator;

    [JsonProperty("icons_stagemoveaudience")]
    public string IconsStagemoveaudience;

    [JsonProperty("icons_stagemovespeaker")]
    public string IconsStagemovespeaker;

    [JsonProperty("icons_stagerequesttospeak")]
    public string IconsStagerequesttospeak;

    [JsonProperty("icons_stagerequesttospeaklist")]
    public string IconsStagerequesttospeaklist;

    [JsonProperty("icons_star")] public string IconsStar;

    [JsonProperty("icons_store")] public string IconsStore;

    [JsonProperty("icons_supportscommandsbadge")]
    public string IconsSupportscommandsbadge;

    [JsonProperty("icons_text1")] public string IconsText1;

    [JsonProperty("icons_text2")] public string IconsText2;

    [JsonProperty("icons_text3")] public string IconsText3;

    [JsonProperty("icons_text4")] public string IconsText4;

    [JsonProperty("icons_text5")] public string IconsText5;

    [JsonProperty("icons_text6")] public string IconsText6;

    [JsonProperty("icons_timeout")] public string IconsTimeout;

    [JsonProperty("icons_topgg")] public string IconsTopgg;

    [JsonProperty("icons_transferownership")]
    public string IconsTransferownership;

    [JsonProperty("icons_update1")] public string IconsUpdate1;

    [JsonProperty("icons_update2")] public string IconsUpdate2;

    [JsonProperty("icons_upvote")] public string IconsUpvote;

    [JsonProperty("icons_verified")] public string IconsVerified;

    [JsonProperty("icons_video")] public string IconsVideo;

    [JsonProperty("icons_wrong")] public string IconsWrong;

    [JsonProperty("icons_wumpus")] public string IconsWumpus;

    [JsonProperty("icons_xmarkwhite")] public string IconsXmarkwhite;

    [JsonProperty("eye_off")] public string EyeOff;

    [JsonProperty("eye")] public string Eye;

    [JsonProperty("icons_18")] public string Icons18;

    [JsonProperty("icons_bigender")] public string IconsBigender;

    [JsonProperty("icons_calender")] public string IconsCalender;

    [JsonProperty("icons_calenderdate")] public string IconsCalenderdate;

    [JsonProperty("icons_cmd")] public string IconsCmd;

    [JsonProperty("icons_discordjs")] public string IconsDiscordjs;

    [JsonProperty("icons_female")] public string IconsFemale;

    [JsonProperty("icons_gay")] public string IconsGay;

    [JsonProperty("icons_gender")] public string IconsGender;

    [JsonProperty("icons_hetero")] public string IconsHetero;

    [JsonProperty("icons_jpg")] public string IconsJpg;

    [JsonProperty("icons_js")] public string IconsJs;

    [JsonProperty("icons_lesbian")] public string IconsLesbian;

    [JsonProperty("icons_male")] public string IconsMale;

    [JsonProperty("icons_moderationhig")] public string IconsModerationhig;

    [JsonProperty("icons_moderationhighest")]
    public string IconsModerationhighest;

    [JsonProperty("icons_moderationlow")] public string IconsModerationlow;

    [JsonProperty("icons_moderationmedium")]
    public string IconsModerationmedium;

    [JsonProperty("icons_moderationnone")] public string IconsModerationnone;

    [JsonProperty("icons_nodejs")] public string IconsNodejs;

    [JsonProperty("icons_png")] public string IconsPng;

    [JsonProperty("icons_radmins")] public string IconsRadmins;

    [JsonProperty("icons_rartists")] public string IconsRartists;

    [JsonProperty("icons_rboosters")] public string IconsRboosters;

    [JsonProperty("icons_rbots")] public string IconsRbots;

    [JsonProperty("icons_rcamera")] public string IconsRcamera;

    [JsonProperty("icons_rdevelopers")] public string IconsRdevelopers;

    [JsonProperty("icons_revents")] public string IconsRevents;

    [JsonProperty("icons_rfire")] public string IconsRfire;

    [JsonProperty("icons_rguardians")] public string IconsRguardians;

    [JsonProperty("icons_rhelpers")] public string IconsRhelpers;

    [JsonProperty("icons_rmembers")] public string IconsRmembers;

    [JsonProperty("icons_rmods")] public string IconsRmods;

    [JsonProperty("icons_rowner")] public string IconsRowner;

    [JsonProperty("icons_rpodcast")] public string IconsRpodcast;

    [JsonProperty("icons_rsdonator")] public string IconsRsdonator;

    [JsonProperty("icons_rspartner")] public string IconsRspartner;

    [JsonProperty("icons_rsstaffs")] public string IconsRsstaffs;

    [JsonProperty("icons_rstaff")] public string IconsRstaff;

    [JsonProperty("icons_rverification")] public string IconsRverification;

    [JsonProperty("icons_rverified")] public string IconsRverified;

    [JsonProperty("icons_rvip")] public string IconsRvip;

    [JsonProperty("icons_snowflake")] public string IconsSnowflake;

    [JsonProperty("icons_tiktok")] public string IconsTiktok;

    [JsonProperty("icons_transgender")] public string IconsTransgender;

    [JsonProperty("icons_twitch")] public string IconsTwitch;

    [JsonProperty("icons_vklogo")] public string IconsVklogo;

    [JsonProperty("icons_warning")] public string IconsWarning;

    [JsonProperty("icons_wave")] public string IconsWave;

    [JsonProperty("icons_webp")] public string IconsWebp;

    [JsonProperty("file_archive")] public string FileArchive;

    [JsonProperty("file_check")] public string FileCheck;

    [JsonProperty("file_cog")] public string FileCog;

    [JsonProperty("file_files")] public string FileFiles;

    [JsonProperty("file_minus")] public string FileMinus;

    [JsonProperty("file_plus")] public string FilePlus;

    [JsonProperty("file_x")] public string FileX;

    [JsonProperty("file")] public string File;

    [JsonProperty("cpu")] public string Cpu;

    [JsonProperty("ram")] public string Ram;

    [JsonProperty("ssd")] public string Ssd;

    [JsonProperty("wifi")] public string Wifi;

    [JsonProperty("folder_archive")] public string FolderArchive;

    [JsonProperty("folder_check")] public string FolderCheck;

    [JsonProperty("folder_folders")] public string FolderFolders;

    [JsonProperty("folder_minus")] public string FolderMinus;

    [JsonProperty("folder_plus")] public string FolderPlus;

    [JsonProperty("folder_x")] public string FolderX;

    [JsonProperty("folder")] public string Folder;

    [JsonProperty("headphone_off")] public string HeadphoneOff;

    [JsonProperty("headphone")] public string Headphone;

    [JsonProperty("image_download")] public string ImageDownload;

    [JsonProperty("image_minus")] public string ImageMinus;

    [JsonProperty("image_off")] public string ImageOff;

    [JsonProperty("image_plus")] public string ImagePlus;

    [JsonProperty("image_upload")] public string ImageUpload;

    [JsonProperty("image")] public string Image;

    [JsonProperty("lock_unlock")] public string LockUnlock;

    [JsonProperty("lock")] public string Lock;

    [JsonProperty("mail_check")] public string MailCheck;

    [JsonProperty("mail_minus")] public string MailMinus;

    [JsonProperty("mail_plus")] public string MailPlus;

    [JsonProperty("mail_x")] public string MailX;

    [JsonProperty("mail")] public string Mail;

    [JsonProperty("mic_off")] public string MicOff;

    [JsonProperty("mic")] public string Mic;

    [JsonProperty("phone_off")] public string PhoneOff;

    [JsonProperty("phone")] public string Phone;

    [JsonProperty("home")] public string Home;

    [JsonProperty("id")] public string Id;

    [JsonProperty("list")] public string List;

    [JsonProperty("other_brush")] public string OtherBrush;

    [JsonProperty("other_cable")] public string OtherCable;

    [JsonProperty("other_crown")] public string OtherCrown;

    [JsonProperty("other_dollar")] public string OtherDollar;

    [JsonProperty("other_earth")] public string OtherEarth;

    [JsonProperty("other_gauge")] public string OtherGauge;

    [JsonProperty("other_gear")] public string OtherGear;

    [JsonProperty("other_graduation")] public string OtherGraduation;

    [JsonProperty("other_heart")] public string OtherHeart;

    [JsonProperty("other_home")] public string OtherHome;

    [JsonProperty("other_save_off")] public string OtherSaveOff;

    [JsonProperty("other_save")] public string OtherSave;

    [JsonProperty("other_text")] public string OtherText;

    [JsonProperty("other_translate")] public string OtherTranslate;

    [JsonProperty("other_trash")] public string OtherTrash;

    [JsonProperty("other_truck")] public string OtherTruck;

    [JsonProperty("refresh")] public string Refresh;

    [JsonProperty("pause")] public string Pause;

    [JsonProperty("resume")] public string Resume;

    [JsonProperty("stop")] public string Stop;

    [JsonProperty("view")] public string View;

    [JsonProperty("shield_check")] public string ShieldCheck;

    [JsonProperty("shield_minus")] public string ShieldMinus;

    [JsonProperty("shield_off")] public string ShieldOff;

    [JsonProperty("shield_plus")] public string ShieldPlus;

    [JsonProperty("shield_x")] public string ShieldX;

    [JsonProperty("shield")] public string Shield;

    [JsonProperty("tag_tags")] public string TagTags;

    [JsonProperty("tag")] public string Tag;

    [JsonProperty("ticket_check")] public string TicketCheck;

    [JsonProperty("ticket_minus")] public string TicketMinus;

    [JsonProperty("ticket_plus")] public string TicketPlus;

    [JsonProperty("ticket_tickets")] public string TicketTickets;

    [JsonProperty("ticket_x")] public string TicketX;

    [JsonProperty("ticket")] public string Ticket;

    [JsonProperty("user_check")] public string UserCheck;

    [JsonProperty("user_cog")] public string UserCog;

    [JsonProperty("user_minus")] public string UserMinus;

    [JsonProperty("user_plus")] public string UserPlus;

    [JsonProperty("user_users")] public string UserUsers;

    [JsonProperty("user_x")] public string UserX;

    [JsonProperty("user")] public string User;

    [JsonProperty("timer_off")] public string TimerOff;

    [JsonProperty("timer_reset")] public string TimerReset;

    [JsonProperty("timer")] public string Timer;
}