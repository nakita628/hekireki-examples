defmodule Papermark.LinkPreset do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          p_id: String.t() | nil,
          enable_custom_meta_tag: boolean() | nil,
          meta_title: String.t() | nil,
          meta_description: String.t() | nil,
          meta_image: String.t() | nil,
          meta_favicon: String.t() | nil,
          enable_notification: boolean() | nil,
          email_protected: boolean() | nil,
          email_authenticated: boolean() | nil,
          allow_download: boolean() | nil,
          enable_allow_list: boolean() | nil,
          allow_list: [String.t()],
          enable_deny_list: boolean() | nil,
          deny_list: [String.t()],
          expires_in: integer() | nil,
          enable_screenshot_protection: boolean() | nil,
          enable_confidential_view: boolean() | nil,
          expires_at: DateTime.t() | nil,
          enable_password: boolean() | nil,
          password: String.t() | nil,
          enable_watermark: boolean() | nil,
          watermark_config: map() | nil,
          enable_agreement: boolean() | nil,
          agreement_id: String.t() | nil,
          enable_custom_fields: boolean() | nil,
          custom_fields: map() | nil,
          show_banner: boolean() | nil,
          welcome_message: String.t() | nil,
          is_default: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil
        }

  schema "LinkPreset" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:p_id, :string, source: :pId)
    field(:enable_custom_meta_tag, :boolean, default: false, source: :enableCustomMetaTag)
    field(:meta_title, :string, source: :metaTitle)
    field(:meta_description, :string, source: :metaDescription)
    field(:meta_image, :string, source: :metaImage)
    field(:meta_favicon, :string, source: :metaFavicon)
    field(:enable_notification, :boolean, default: false, source: :enableNotification)
    field(:email_protected, :boolean, default: true, source: :emailProtected)
    field(:email_authenticated, :boolean, default: false, source: :emailAuthenticated)
    field(:allow_download, :boolean, default: false, source: :allowDownload)
    field(:enable_allow_list, :boolean, default: false, source: :enableAllowList)
    field(:allow_list, {:array, :string}, source: :allowList)
    field(:enable_deny_list, :boolean, default: false, source: :enableDenyList)
    field(:deny_list, {:array, :string}, source: :denyList)
    field(:expires_in, :integer, source: :expiresIn)
    field(:enable_screenshot_protection, :boolean, default: false, source: :enableScreenshotProtection)
    field(:enable_confidential_view, :boolean, default: false, source: :enableConfidentialView)
    field(:expires_at, Papermark.PrismaDateTime, source: :expiresAt)
    field(:enable_password, :boolean, default: false, source: :enablePassword)
    field(:password, :string)
    field(:enable_watermark, :boolean, default: false, source: :enableWatermark)
    field(:watermark_config, :map, source: :watermarkConfig)
    field(:enable_agreement, :boolean, default: false, source: :enableAgreement)
    field(:agreement_id, :string, source: :agreementId)
    field(:enable_custom_fields, :boolean, default: false, source: :enableCustomFields)
    field(:custom_fields, :map, source: :customFields)
    field(:show_banner, :boolean, default: false, source: :showBanner)
    field(:welcome_message, :string, source: :welcomeMessage)
    field(:is_default, :boolean, default: false, source: :isDefault)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end