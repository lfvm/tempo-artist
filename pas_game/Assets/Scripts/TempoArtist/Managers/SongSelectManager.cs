using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Linq;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using TempoArtist.Objects;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace TempoArtist.Managers
{
    public class SongSelectManager : MonoBehaviour
    {
        public static SongSelectManager Instance;
        public Beatmap selectedBeatmap { get; private set; }

        public AudioSource beatmapSong;

        [SerializeField] private BeatmapCard beatmapCard;
        
        [SerializeField] private MapInfoCard mapInfoCard;

        // Reference to the GameSetup instance

        private ManiaGameManager ManiaGameManager;
        private TaikoGameManager TaikoGameManager;

        [SerializeField] private Slider maniaScrollSpeedSlider;
        [SerializeField] private Slider taikoScrollSpeedSlider;
        [SerializeField] private Slider offsetSlider;
        
        private TMP_Text maniaScrollSpeedText;
        private TMP_Text taikoScrollSpeedText;
        private TMP_Text offsetText;

        private float maniaScrollSpeed;
        private float taikoScrollSpeed;
        private float offset;

        public float SettingsManiaSpeed;
        public float SettingsTaikoSpeed;
        public float SettingsOffset;
        
        private UIManager UIManager;
        
        private List<string> jsonBeatmapPaths;
        
        private List<Beatmap> beatmapList;
        
        private GameObject beatmapsPanel;
        
        private GameObject canvas;
        
        private BeatmapCard selectedBeatmapCard;

        private Button closeOptionsMenuButton;

        private bool beatmapCardSelected;

        public string selectedBeatmapName;
        public int selectedBeatmapMode;

        private GameObject settingsPanel;

        private void Awake()
        {
            Instance = this;

            jsonBeatmapPaths = new List<string>();
            beatmapList = new List<Beatmap>();

            closeOptionsMenuButton = GameObject.Find("CloseMenuButton").GetComponent<Button>();
            
            beatmapsPanel = GameObject.Find("BeatmapsPanel");
            canvas = GameObject.Find("Canvas");

            maniaScrollSpeedSlider = GameObject.Find("Mania Scroll Speed Slider").GetComponent<Slider>();
            taikoScrollSpeedSlider = GameObject.Find("Taiko Scroll Speed Slider").GetComponent<Slider>();
            offsetSlider = GameObject.Find("Offset Slider").GetComponent<Slider>();

            maniaScrollSpeedText = GameObject.Find("Mania Scroll Speed Value").GetComponent<TMP_Text>();
            taikoScrollSpeedText = GameObject.Find("Taiko Scroll Speed Value").GetComponent<TMP_Text>();
            offsetText =  GameObject.Find("Offset Value").GetComponent<TMP_Text>();

            settingsPanel = GameObject.Find("OptionsPanel");
        }

        void Start()
        {
            closeOptionsMenuButton.onClick.AddListener(CloseOptionsMenu);
            settingsPanel.SetActive(false);
            mapInfoCard = MapInfoCard.Instance;
            UIManager = UIManager.Instance;
            ManiaGameManager = ManiaGameManager.instance;
            TaikoGameManager = TaikoGameManager.instance;
            
            var beatmapFoldersPath = "./Assets/Resources/Beatmaps";
            
            CreateBeatmaps(beatmapFoldersPath);
            CreateBeatmapMapCards();
            PrintBeatmapList();
        }

        void Update()
        {
            if (beatmapCardSelected)
            {
                UpdateMapInfoCard(selectedBeatmapCard);
            }

            maniaScrollSpeed = maniaScrollSpeedSlider.value;
            taikoScrollSpeed = taikoScrollSpeedSlider.value;
            offset = offsetSlider.value;

            maniaScrollSpeedText.text = $"{maniaScrollSpeed.ToString()} ms";
            taikoScrollSpeedText.text = $"{taikoScrollSpeed.ToString()} ms";
            offsetText.text = $"{offset.ToString()} ms";

            Settings.ManiaScrollSpeed = maniaScrollSpeed;
            Settings.TaikoScrollSpeed = taikoScrollSpeed;
            Settings.Offset = offset;

            SettingsManiaSpeed = Settings.ManiaScrollSpeed;
            SettingsTaikoSpeed = Settings.TaikoScrollSpeed;
            SettingsOffset = Settings.Offset;
            
            CheckForOptionsKey();
        }

        private void PrintBeatmapList()
        {
            foreach (var str in HCBeatmaps.beatmapStrings)
            {
                Debug.Log(str);
            }
        }

        private void UpdateMapInfoCard(BeatmapCard card)
        {
            mapInfoCard.UpdateMapInfoText(selectedBeatmapCard);
        }

        private void CreateBeatmaps(string path)
        {
            // var beatmapFolders = Directory.GetDirectories(path);
            string[] beatmapFolders = new string[]
            {
                "Beatmaps/Akasha",
                "Beatmaps/BeastBassBomb",
                "Beatmaps/blue zenith",
                "Beatmaps/FInalBoss",
                "Beatmaps/Furioso Melodia",
                "Beatmaps/HoldinOn",
            };

            string[] beatmapJsonPaths;
            string[] beatmapSongPaths;
            
            foreach (var beatmapFolderPath in beatmapFolders)
            {
                // beatmapJsonPaths = Directory.GetFiles(beatmapFolderPath, "*.json");
                // beatmapSongPaths = Directory.GetFiles(beatmapFolderPath, "*.mp3");
                //
                // Beatmap beatmap = JsonParser.JsonToBeatmap(beatmapFolderPath + "/" + "beatmap.json");

                var beatmapJson = Resources.Load<TextAsset>(beatmapFolderPath + "/" + "beatmap");
                Debug.Log(beatmapJson);
                Beatmap beatmap = JsonUtility.FromJson<Beatmap>(beatmapJson.ToString());
                    
                //
                // var dividedPath = beatmapSongPaths[0].Split('/').Reverse().Take(3).Reverse().ToArray();
                // var finalPath = GetFullPathWithoutExtension(string.Join("/", dividedPath));

                beatmap.MusicSource = Resources.Load<AudioClip>(beatmapFolderPath + "/" + "audio");
                beatmapList.Add(beatmap);
                //HCBeatmaps.beatmapStrings.Add( JsonParser.BeatmapToJson(beatmap));
            }
        }

        private void CreateBeatmapMapCards()
        {
            foreach (var beatmap in beatmapList)
            {
                var card = Instantiate(beatmapCard, beatmapsPanel.transform, true);
                card.cardTitle.GetComponent<TMP_Text>().text = beatmap.metadata.Title;
                card.cardArtist.GetComponent<TMP_Text>().text = beatmap.metadata.Artist;
                card.Beatmap = beatmap;
            }
        }
        
        public void SetSelectedBeatmapCard(BeatmapCard card)
        {
            selectedBeatmapCard = card;
            beatmapCardSelected = true;
            selectedBeatmap = card.Beatmap;
            selectedBeatmapName = card.Beatmap.metadata.Title;
            selectedBeatmapMode = Int32.Parse(card.Beatmap.general.mode);
            PlayAudioSample();
        }

        public void playSelectedBeatmap()
        {
            if (beatmapCardSelected)
            {
                switch (selectedBeatmapMode)
                {
                    case 3:
                        SceneManager.LoadScene("Game");
                        break;
                    case 1:
                        SceneManager.LoadScene("Taiko");
                        break;
                }
            }
        }

        public void PlayAudioSample()
        {
            //Debug.Log("Playing audio sample");
            //Debug.Log(selectedBeatmap.MusicSource);
            beatmapSong.clip = selectedBeatmap.MusicSource;
            beatmapSong.Play();
        }

        private String GetFullPathWithoutExtension(String path)
        {
            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        }

        private void CheckForOptionsKey()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                settingsPanel.SetActive(true);
            }
        }

        private void CloseOptionsMenu()
        {
            settingsPanel.SetActive(false);
        }
    }
}
