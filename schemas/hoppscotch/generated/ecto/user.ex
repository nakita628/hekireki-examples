defmodule Hoppscotch.User do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          uid: String.t(),
          display_name: String.t() | nil,
          email: String.t() | nil,
          photo_url: String.t() | nil,
          is_admin: boolean(),
          refresh_token: String.t() | nil,
          current_restsession: map() | nil,
          current_gqlsession: map() | nil,
          created_on: DateTime.t(),
          last_logged_on: DateTime.t() | nil,
          last_active_on: DateTime.t() | nil,
          settings: Hoppscotch.UserSettings.t() | nil,
          provider_accounts: [Hoppscotch.Account.t()],
          invited_users: [Hoppscotch.InvitedUsers.t()],
          mock_servers: [Hoppscotch.MockServer.t()],
          personal_access_tokens: [Hoppscotch.PersonalAccessToken.t()],
          shortcodes: [Hoppscotch.Shortcode.t()],
          user_collections: [Hoppscotch.UserCollection.t()],
          environments: [Hoppscotch.UserEnvironment.t()],
          history: [Hoppscotch.UserHistory.t()],
          user_requests: [Hoppscotch.UserRequest.t()],
          verification_tokens: [Hoppscotch.VerificationToken.t()]
        }

  schema "User" do
    field(:uid, :string, primary_key: true)
    field(:display_name, :string, source: :displayName)
    field(:email, :string)
    field(:photo_url, :string, source: :photoURL)
    field(:is_admin, :boolean, default: false, source: :isAdmin)
    field(:refresh_token, :string, source: :refreshToken)
    field(:current_restsession, :map, source: :currentRESTSession)
    field(:current_gqlsession, :map, source: :currentGQLSession)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:last_logged_on, Hoppscotch.PrismaDateTime, source: :lastLoggedOn)
    field(:last_active_on, Hoppscotch.PrismaDateTime, source: :lastActiveOn)
    has_one(:settings, Hoppscotch.UserSettings, foreign_key: :user_uid, references: :uid)
    has_many(:provider_accounts, Hoppscotch.Account, foreign_key: :user_id, references: :uid)
    has_many(:invited_users, Hoppscotch.InvitedUsers, foreign_key: :admin_uid, references: :uid)
    has_many(:mock_servers, Hoppscotch.MockServer, foreign_key: :creator_uid, references: :uid)
    has_many(:personal_access_tokens, Hoppscotch.PersonalAccessToken, foreign_key: :user_uid, references: :uid)
    has_many(:shortcodes, Hoppscotch.Shortcode, foreign_key: :creator_uid, references: :uid)
    has_many(:user_collections, Hoppscotch.UserCollection, foreign_key: :user_uid, references: :uid)
    has_many(:environments, Hoppscotch.UserEnvironment, foreign_key: :user_uid, references: :uid)
    has_many(:history, Hoppscotch.UserHistory, foreign_key: :user_uid, references: :uid)
    has_many(:user_requests, Hoppscotch.UserRequest, foreign_key: :user_uid, references: :uid)
    has_many(:verification_tokens, Hoppscotch.VerificationToken, foreign_key: :user_uid, references: :uid)
  end
end