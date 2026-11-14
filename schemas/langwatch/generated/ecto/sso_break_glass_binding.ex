defmodule LangWatch.SsoBreakGlassBinding do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          user_id: String.t(),
          granted_by_user_id: String.t(),
          granted_at: DateTime.t(),
          expires_at: DateTime.t(),
          superseded_at: DateTime.t() | nil,
          renewed_from_id: String.t() | nil,
          warned_days: [integer()],
          inserted_at: DateTime.t()
        }

  schema "SsoBreakGlassBinding" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    field(:granted_by_user_id, :string, source: :grantedByUserId)
    field(:granted_at, LangWatch.PrismaDateTime, source: :grantedAt)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:superseded_at, LangWatch.PrismaDateTime, source: :supersededAt)
    field(:renewed_from_id, :string, source: :renewedFromId)
    field(:warned_days, {:array, :integer}, source: :warnedDays)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end