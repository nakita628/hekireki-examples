defmodule Jitsu.UserProfile do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          email: String.t(),
          admin: boolean() | nil,
          login_provider: String.t(),
          external_username: String.t() | nil,
          external_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          password: Jitsu.UserPassword.t() | nil,
          tokens: [Jitsu.UserApiToken.t()],
          workspace_access: [Jitsu.WorkspaceAccess.t()],
          user_preferences: [Jitsu.UserPreferences.t()],
          workspace_user_properties: [Jitsu.WorkspaceUserProperties.t()]
        }

  schema "UserProfile" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:admin, :boolean, default: false)
    field(:login_provider, :string, source: :loginProvider)
    field(:external_username, :string, source: :externalUsername)
    field(:external_id, :string, source: :externalId)
    has_one(:password, Jitsu.UserPassword, foreign_key: :user_id, references: :id)
    has_many(:tokens, Jitsu.UserApiToken, foreign_key: :user_id, references: :id)
    has_many(:workspace_access, Jitsu.WorkspaceAccess, foreign_key: :user_id, references: :id)
    has_many(:user_preferences, Jitsu.UserPreferences, foreign_key: :user_id, references: :id)
    has_many(:workspace_user_properties, Jitsu.WorkspaceUserProperties, foreign_key: :user_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end