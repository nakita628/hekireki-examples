defmodule Dub.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          is_machine: boolean(),
          password_hash: String.t() | nil,
          invalid_login_attempts: integer(),
          locked_at: DateTime.t() | nil,
          default_workspace: String.t() | nil,
          source: String.t() | nil,
          sent_mail: boolean(),
          inserted_at: DateTime.t(),
          notification_preferences: Dub.UserNotificationPreferences.t() | nil,
          accounts: [Dub.Account.t()],
          sessions: [Dub.Session.t()],
          projects: [Dub.ProjectUsers.t()],
          links: [Dub.Link.t()],
          dashboards: [Dub.Dashboard.t()],
          tokens: [Dub.Token.t()],
          restricted_tokens: [Dub.RestrictedToken.t()],
          o_auth_codes: [Dub.OAuthCode.t()],
          integrations: [Dub.Integration.t()],
          installed_integrations: [Dub.InstalledIntegration.t()],
          folders: [Dub.FolderUser.t()],
          folder_access_requests: [Dub.FolderAccessRequest.t()],
          utm_templates: [Dub.UtmTemplate.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Dub.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    field(:is_machine, :boolean, default: false, source: :isMachine)
    field(:password_hash, :string, source: :passwordHash)
    field(:invalid_login_attempts, :integer, default: 0, source: :invalidLoginAttempts)
    field(:locked_at, Dub.PrismaDateTime, source: :lockedAt)
    field(:default_workspace, :string, source: :defaultWorkspace)
    field(:source, :string)
    field(:sent_mail, :boolean, default: false, source: :sentMail)
    has_one(:notification_preferences, Dub.UserNotificationPreferences, foreign_key: :user_id)
    has_many(:accounts, Dub.Account, foreign_key: :user_id)
    has_many(:sessions, Dub.Session, foreign_key: :user_id)
    has_many(:projects, Dub.ProjectUsers, foreign_key: :user_id)
    has_many(:links, Dub.Link, foreign_key: :user_id)
    has_many(:dashboards, Dub.Dashboard, foreign_key: :user_id)
    has_many(:tokens, Dub.Token, foreign_key: :user_id)
    has_many(:restricted_tokens, Dub.RestrictedToken, foreign_key: :user_id)
    has_many(:o_auth_codes, Dub.OAuthCode, foreign_key: :user_id)
    has_many(:integrations, Dub.Integration, foreign_key: :user_id)
    has_many(:installed_integrations, Dub.InstalledIntegration, foreign_key: :user_id)
    has_many(:folders, Dub.FolderUser, foreign_key: :user_id)
    has_many(:folder_access_requests, Dub.FolderAccessRequest, foreign_key: :user_id)
    has_many(:utm_templates, Dub.UtmTemplate, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end