defmodule LangWatch.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          identifier_id: String.t() | nil,
          amr: [String.t()],
          started_by_sign_up: boolean(),
          impersonating: map() | nil,
          actor_user_id: String.t() | nil,
          subject_user_id: String.t() | nil,
          impersonation_reason: String.t() | nil,
          impersonation_expires_at: DateTime.t() | nil,
          last_seen_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, LangWatch.PrismaDateTime)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:identifier_id, :string, source: :identifierId)
    field(:amr, {:array, :string})
    field(:started_by_sign_up, :boolean, default: false, source: :startedBySignUp)
    field(:impersonating, :map)
    field(:actor_user_id, :string, source: :actorUserId)
    field(:subject_user_id, :string, source: :subjectUserId)
    field(:impersonation_reason, :string, source: :impersonationReason)
    field(:impersonation_expires_at, LangWatch.PrismaDateTime, source: :impersonationExpiresAt)
    field(:last_seen_at, LangWatch.PrismaDateTime, source: :lastSeenAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end