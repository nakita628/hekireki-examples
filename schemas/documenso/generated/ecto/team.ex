defmodule Documenso.Team do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          url: String.t(),
          inserted_at: DateTime.t(),
          avatar_image: Documenso.AvatarImage.t() | nil,
          organisation: Documenso.Organisation.t() | nil,
          team_global_settings: Documenso.TeamGlobalSettings.t() | nil,
          team_email: Documenso.TeamEmail.t() | nil,
          email_verification: Documenso.TeamEmailVerification.t() | nil,
          profile: Documenso.TeamProfile.t() | nil,
          envelopes: [Documenso.Envelope.t()],
          folders: [Documenso.Folder.t()],
          api_tokens: [Documenso.ApiToken.t()],
          webhooks: [Documenso.Webhook.t()],
          team_groups: [Documenso.TeamGroup.t()]
        }

  schema "Team" do
    field(:name, :string)
    field(:url, :string)
    field(:avatar_image_id, :string, source: :avatarImageId)
    field(:organisation_id, :string, source: :organisationId)
    field(:team_global_settings_id, :string, source: :teamGlobalSettingsId)
    belongs_to(:avatar_image, Documenso.AvatarImage, foreign_key: :avatar_image_id, define_field: false, type: :string)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    belongs_to(:team_global_settings, Documenso.TeamGlobalSettings, foreign_key: :team_global_settings_id, define_field: false, type: :string)
    has_one(:team_email, Documenso.TeamEmail, foreign_key: :team_id)
    has_one(:email_verification, Documenso.TeamEmailVerification, foreign_key: :team_id)
    has_one(:profile, Documenso.TeamProfile, foreign_key: :team_id)
    has_many(:envelopes, Documenso.Envelope, foreign_key: :team_id)
    has_many(:folders, Documenso.Folder, foreign_key: :team_id)
    has_many(:api_tokens, Documenso.ApiToken, foreign_key: :team_id)
    has_many(:webhooks, Documenso.Webhook, foreign_key: :team_id)
    has_many(:team_groups, Documenso.TeamGroup, foreign_key: :team_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end