defmodule Papermark.Brand do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          logo: String.t() | nil,
          hide_logo: boolean(),
          banner: String.t() | nil,
          brand_color: String.t() | nil,
          accent_color: String.t() | nil,
          accent_button_color: String.t() | nil,
          apply_accent_color_to_dataroom_view: boolean(),
          welcome_message: String.t() | nil,
          cta_label: String.t() | nil,
          cta_url: String.t() | nil,
          privacy_policy_url: String.t() | nil,
          card_layout: String.t(),
          show_folder_tree: boolean(),
          viewer_layout_preset: String.t(),
          viewer_header_style: String.t(),
          hide_folder_icons_in_main: boolean(),
          custom_link_preview_enabled: boolean(),
          link_preview_title: String.t() | nil,
          link_preview_description: String.t() | nil,
          link_preview_image: String.t() | nil,
          link_preview_favicon: String.t() | nil,
          default_language: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil,
          default_for_team: [Papermark.Team.t()],
          links: [Papermark.Link.t()],
          datarooms: [Papermark.Dataroom.t()]
        }

  schema "Brand" do
    field(:id, :string, primary_key: true)
    field(:logo, :string)
    field(:hide_logo, :boolean, default: false, source: :hideLogo)
    field(:banner, :string)
    field(:brand_color, :string, source: :brandColor)
    field(:accent_color, :string, source: :accentColor)
    field(:accent_button_color, :string, source: :accentButtonColor)
    field(:apply_accent_color_to_dataroom_view, :boolean, default: false, source: :applyAccentColorToDataroomView)
    field(:welcome_message, :string, source: :welcomeMessage)
    field(:cta_label, :string, source: :ctaLabel)
    field(:cta_url, :string, source: :ctaUrl)
    field(:privacy_policy_url, :string, source: :privacyPolicyUrl)
    field(:card_layout, :string, default: "LIST", source: :cardLayout)
    field(:show_folder_tree, :boolean, default: true, source: :showFolderTree)
    field(:viewer_layout_preset, :string, default: "STANDARD", source: :viewerLayoutPreset)
    field(:viewer_header_style, :string, default: "DEFAULT", source: :viewerHeaderStyle)
    field(:hide_folder_icons_in_main, :boolean, default: false, source: :hideFolderIconsInMain)
    field(:custom_link_preview_enabled, :boolean, default: false, source: :customLinkPreviewEnabled)
    field(:link_preview_title, :string, source: :linkPreviewTitle)
    field(:link_preview_description, :string, source: :linkPreviewDescription)
    field(:link_preview_image, :string, source: :linkPreviewImage)
    field(:link_preview_favicon, :string, source: :linkPreviewFavicon)
    field(:default_language, :string, default: "en", source: :defaultLanguage)
    field(:name, :string, default: "Default")
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:default_for_team, Papermark.Team, foreign_key: :default_brand_id)
    has_many(:links, Papermark.Link, foreign_key: :brand_id)
    has_many(:datarooms, Papermark.Dataroom, foreign_key: :brand_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end