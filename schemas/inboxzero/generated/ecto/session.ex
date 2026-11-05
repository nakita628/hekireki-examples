defmodule InboxZero.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          email_otp: boolean(),
          email_otp_version: integer(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          active_organization: InboxZero.Organization.t() | nil,
          oauth_refresh_token_records: [InboxZero.OauthRefreshToken.t()],
          oauth_access_token_records: [InboxZero.OauthAccessToken.t()]
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, InboxZero.PrismaDateTime)
    field(:email_otp, :boolean, default: false, source: :emailOtp)
    field(:email_otp_version, :integer, default: 0, source: :emailOtpVersion)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:user_id, :string, source: :userId)
    field(:active_organization_id, :string, source: :activeOrganizationId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:active_organization, InboxZero.Organization, foreign_key: :active_organization_id, define_field: false, type: :string)
    has_many(:oauth_refresh_token_records, InboxZero.OauthRefreshToken, foreign_key: :session_id, references: :id)
    has_many(:oauth_access_token_records, InboxZero.OauthAccessToken, foreign_key: :session_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end